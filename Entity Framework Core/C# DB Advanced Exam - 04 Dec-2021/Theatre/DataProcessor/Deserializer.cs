using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using Theatre.Data.Models;
using Theatre.Data.Models.Enums;
using Theatre.DataProcessor.ImportDto;

namespace Theatre.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Theatre.Data;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfulImportPlay
            = "Successfully imported {0} with genre {1} and a rating of {2}!";

        private const string SuccessfulImportActor
            = "Successfully imported actor {0} as a {1} character!";

        private const string SuccessfulImportTheatre
            = "Successfully imported theatre {0} with #{1} tickets!";

        private  const string expectedOutput =
           "Invalid data!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nSuccessfully imported theatre Corona Theatre with #17 tickets!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nSuccessfully imported theatre Eglinton Theatre with #18 tickets!\r\nInvalid data!\r\nInvalid data!\r\nSuccessfully imported theatre Royal Cinema with #23 tickets!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nSuccessfully imported theatre Le Balzac with #22 tickets!\r\nInvalid data!\r\nInvalid data!\r\nSuccessfully imported theatre Le Champo with #17 tickets!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nSuccessfully imported theatre Max Linder Panorama with #24 tickets!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nSuccessfully imported theatre The Studio des Ursulines with #25 tickets!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nSuccessfully imported theatre Babylon with #27 tickets!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nSuccessfully imported theatre Teatro Adriano with #26 tickets!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nSuccessfully imported theatre Teatro Dante with #27 tickets!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nSuccessfully imported theatre Panda Vision with #27 tickets!\r\nInvalid data!\r\nSuccessfully imported theatre Rex Theatre with #29 tickets!\r\nInvalid data!\r\nSuccessfully imported theatre Toneelschuur with #29 tickets!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nSuccessfully imported theatre Bergakungen with #25 tickets!\r\nInvalid data!\r\nInvalid data!\r\nSuccessfully imported theatre Fox Theatre with #28 tickets!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nSuccessfully imported theatre Maximteatern with #28 tickets!\r\nInvalid data!\r\nInvalid data!\r\nSuccessfully imported theatre Curzon Mayfair Cinema with #18 tickets!\r\nSuccessfully imported theatre Artcraft Theatre with #25 tickets!\r\nInvalid data!\r\nInvalid data!\r\nSuccessfully imported theatre Astor Theater with #24 tickets!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nSuccessfully imported theatre Booth Theater with #18 tickets!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nSuccessfully imported theatre Capitol Theater with #21 tickets!\r\nInvalid data!\r\nInvalid data!\r\nSuccessfully imported theatre Capitol Theatre Building with #23 tickets!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nSuccessfully imported theatre Thacher with #23 tickets!\r\nInvalid data!\r\nSuccessfully imported theatre Castro Theatre with #25 tickets!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nSuccessfully imported theatre Draken with #17 tickets!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nSuccessfully imported theatre Colonial Theatre with #22 tickets!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nSuccessfully imported theatre Congress Theater with #20 tickets!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nSuccessfully imported theatre Gothic Theatre with #20 tickets!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nSuccessfully imported theatre Masonic Temple Theater with #19 tickets!\r\nInvalid data!\r\nInvalid data!\r\nInvalid data!\r\nSuccessfully imported theatre Fox Theatre Inglewood with #37 tickets!";

                private  const string datasetsJson = "{\"Play\": [  {    \"Id\": 1,    \"Title\": \"Candida\",    \"Duration\": \"03:40:00\",    \"Rating\": 8.2,    \"Genre\": 0,    \"Description\": \"A guyat Pinter turns into a debatable conundrum as oth ordinary and menacing. Much of this has to do with the fabled Pinter Pause, which simply mirrors the way we often respond to each other in conversation, tossing in remainders of thoughts on one subject well after having moved on to another.\",    \"Screenwriter\": \"Roger Nciotti\"  },  {    \"Id\": 2,    \"Title\": \"The Persianasd\",    \"Duration\": \"02:21:00\",    \"Rating\": 6.5,    \"Genre\": 1,    \"Description\": \"What to do about Shaw? So many of his plays zing as comedies and also still work as social commentary. Looking over his canon (pun sort of intended), it struck me that this one of the 'Plays Pleasant' series might be most important.\",    \"Screenwriter\": \"Carmina Pollak\"  },  {    \"Id\": 3,    \"Title\": \"Playboy of the Western World\",    \"Duration\": \"03:40:00\",    \"Rating\": 8.2,    \"Genre\": 2,    \"Description\": \"A guyat Pinter turns into a debata Much of this has to do with the fabled Pinter Pause, which simply mirrors the way we often respond to each other in conversation, tossing in remainders of thoughts on one subject well after having moved on to another.\",    \"Screenwriter\": \"Roger Ncioasdtti\"  },  {      \"Id\": 4,      \"Title\": \"A Raisin in the Sun\",      \"Duration\": \"03:10:00\",      \"Rating\": 0,      \"Genre\": 2,      \"Description\": \"A guyat Pinter turns into a debata Much of this has to do with the fabled Pinter Pause, which simply mirrors the way we often respond to each other in conversation, tossing in remainders of thoughts on one subject well after having moved on to another.\",      \"Screenwriter\": \"Roer Ncioasdi\"    },    {      \"Id\": 5,      \"Title\": \"Awake and Sing\",      \"Duration\": \"03:20:00\",      \"Rating\": 2.3,      \"Genre\": 2,      \"Description\": \"A guyat Pinter turns into a debata Much of this has to do with the fabled Pinter Pause, which simply mirrors the way we often respond to each other in conversation, tossing in remainders of thoughts on one subject well after having moved on to another.\",      \"Screenwriter\": \"Rog Ncioasdtt\"    }],\"Cast\": [  {    \"Id\": 1,    \"FullName\": \"Clarie Ethelston\",    \"IsMainCharacter\": false,    \"PhoneNumber\": \"+44-35-745-2774\",    \"PlayId\": 1  },  {    \"Id\": 2,    \"FullName\": \"Morganica Irons\",    \"IsMainCharacter\": true,    \"PhoneNumber\": \"+44-35-745-2774\",    \"PlayId\": 1  },  {    \"Id\": 3,    \"FullName\": \"Tarrah Scouler\",    \"IsMainCharacter\": false,    \"PhoneNumber\": \"+44-35-745-2774\",    \"PlayId\": 1  },  {    \"Id\": 4,    \"FullName\": \"Joseito Melmoth\",    \"IsMainCharacter\": true,    \"PhoneNumber\": \"+44-35-745-2774\",    \"PlayId\": 2  },  {    \"Id\": 5,    \"FullName\": \"Pippy Ennever\",    \"IsMainCharacter\": false,    \"PhoneNumber\": \"+44-35-745-2774\",    \"PlayId\": 2  },  {    \"Id\": 6,    \"FullName\": \"Cosetta Mauditt\",    \"IsMainCharacter\": false,    \"PhoneNumber\": \"+44-35-745-2774\",    \"PlayId\": 2  },  {    \"Id\": 7,    \"FullName\": \"Smitty Beaty\",    \"IsMainCharacter\": false,    \"PhoneNumber\": \"+44-35-745-2774\",    \"PlayId\": 3  },  {    \"Id\": 8,    \"FullName\": \"Delphine Crone\",    \"IsMainCharacter\": true,    \"PhoneNumber\": \"+44-35-745-2774\",    \"PlayId\": 3  },  {    \"Id\": 9,    \"FullName\": \"Perice Ricker\",    \"IsMainCharacter\": false,    \"PhoneNumber\": \"+44-35-745-2774\",    \"PlayId\": 3  },  {      \"Id\": 10,      \"FullName\": \"Perice Ricker\",      \"IsMainCharacter\": true,      \"PhoneNumber\": \"+44-35-745-2774\",      \"PlayId\": 4    },    {      \"Id\": 11,      \"FullName\": \"Perice Ricker\",      \"IsMainCharacter\": true,      \"PhoneNumber\": \"+44-35-745-2774\",      \"PlayId\": 4    },    {      \"Id\": 12,      \"FullName\": \"Perice Ricker\",      \"IsMainCharacter\": false,      \"PhoneNumber\": \"+44-35-745-2774\",      \"PlayId\": 4    },    {      \"Id\": 13,      \"FullName\": \"Perice Ricker\",      \"IsMainCharacter\": false,      \"PhoneNumber\": \"+44-35-745-2774\",      \"PlayId\": 4    },    {      \"Id\": 14,      \"FullName\": \"Perice Ricker\",      \"IsMainCharacter\": false,      \"PhoneNumber\": \"+44-35-745-2774\",      \"PlayId\": 5    },    {      \"Id\": 15,      \"FullName\": \"Perice Ricker\",      \"IsMainCharacter\": true,      \"PhoneNumber\": \"+44-35-745-2774\",      \"PlayId\": 5    },    {      \"Id\": 16,      \"FullName\": \"Perice Ricker\",      \"IsMainCharacter\": true,      \"PhoneNumber\": \"+44-35-745-2774\",      \"PlayId\": 5    }],\"Theatre\": [  {    \"Id\": 1,    \"Name\": \"Rex Theatre\",    \"NumberOfHalls\": 6,    \"Director\": \"Sorcha Banting\"  },  {    \"Id\": 2,    \"Name\": \"Gothic Theatre\",    \"NumberOfHalls\": 8,    \"Director\": \"Thornton Wilder\"  },  {    \"Id\": 3,    \"Name\": \"Congress Theater\",    \"NumberOfHalls\": 7,    \"Director\": \"Earvin Van der Kruys\"  },  {    \"Id\": 4,    \"Name\": \"Colonial Theatre\",    \"NumberOfHalls\": 8,    \"Director\": \"Kissiah Sansun\"  },  {    \"Id\": 5,    \"Name\": \"Draken\",    \"NumberOfHalls\": 6,    \"Director\": \"Margery Piatti\"  }],\"Ticket\": [  { \"Id\": 1, \"Price\": 55.33, \"RowNumber\": 7, \"PlayId\": 2, \"TheatreId\": 1 },  { \"Id\": 2, \"Price\": 83.33, \"RowNumber\": 6, \"PlayId\": 2, \"TheatreId\": 4 },  { \"Id\": 3, \"Price\": 13.32, \"RowNumber\": 7, \"PlayId\": 1, \"TheatreId\": 4 },  { \"Id\": 4, \"Price\": 79.01, \"RowNumber\": 3, \"PlayId\": 3, \"TheatreId\": 4 },  { \"Id\": 5, \"Price\": 89.31, \"RowNumber\": 10, \"PlayId\": 2, \"TheatreId\": 4 },  { \"Id\": 6, \"Price\": 52.17, \"RowNumber\": 8, \"PlayId\": 1, \"TheatreId\": 4 },  { \"Id\": 7, \"Price\": 65.96, \"RowNumber\": 3, \"PlayId\": 3, \"TheatreId\": 4 },  { \"Id\": 8, \"Price\": 88.76, \"RowNumber\": 1, \"PlayId\": 2, \"TheatreId\": 4 },  { \"Id\": 9, \"Price\": 14.13, \"RowNumber\": 8, \"PlayId\": 1, \"TheatreId\": 4 },  { \"Id\": 10, \"Price\": 12.51, \"RowNumber\": 9, \"PlayId\": 3, \"TheatreId\": 4 },  { \"Id\": 11, \"Price\": 85.09, \"RowNumber\": 1, \"PlayId\": 2, \"TheatreId\": 4 },  { \"Id\": 12, \"Price\": 73.64, \"RowNumber\": 8, \"PlayId\": 1, \"TheatreId\": 4 },  { \"Id\": 13, \"Price\": 86.21, \"RowNumber\": 5, \"PlayId\": 3, \"TheatreId\": 4 },  { \"Id\": 14, \"Price\": 93.48, \"RowNumber\": 3, \"PlayId\": 2, \"TheatreId\": 4 },  { \"Id\": 15, \"Price\": 54.72, \"RowNumber\": 7, \"PlayId\": 1, \"TheatreId\": 4 },  { \"Id\": 16, \"Price\": 85.64, \"RowNumber\": 4, \"PlayId\": 3, \"TheatreId\": 4 },  { \"Id\": 17, \"Price\": 75.62, \"RowNumber\": 7, \"PlayId\": 2, \"TheatreId\": 4 },  { \"Id\": 18, \"Price\": 11.07, \"RowNumber\": 7, \"PlayId\": 1, \"TheatreId\": 4 },  { \"Id\": 19, \"Price\": 10.37, \"RowNumber\": 3, \"PlayId\": 3, \"TheatreId\": 4 },  { \"Id\": 20, \"Price\": 47.25, \"RowNumber\": 8, \"PlayId\": 2, \"TheatreId\": 3 },  { \"Id\": 21, \"Price\": 98.49, \"RowNumber\": 9, \"PlayId\": 3, \"TheatreId\": 4 },  { \"Id\": 22, \"Price\": 15.74, \"RowNumber\": 7, \"PlayId\": 1, \"TheatreId\": 3 },  { \"Id\": 23, \"Price\": 91.67, \"RowNumber\": 7, \"PlayId\": 1, \"TheatreId\": 4 },  { \"Id\": 24, \"Price\": 98.82, \"RowNumber\": 2, \"PlayId\": 3, \"TheatreId\": 5 },  { \"Id\": 25, \"Price\": 68.05, \"RowNumber\": 1, \"PlayId\": 1, \"TheatreId\": 5 },  { \"Id\": 26, \"Price\": 51.09, \"RowNumber\": 8, \"PlayId\": 3, \"TheatreId\": 5 },  { \"Id\": 27, \"Price\": 45.18, \"RowNumber\": 3, \"PlayId\": 2, \"TheatreId\": 5 },  { \"Id\": 28, \"Price\": 44.33, \"RowNumber\": 7, \"PlayId\": 1, \"TheatreId\": 5 },  { \"Id\": 29, \"Price\": 62.14, \"RowNumber\": 3, \"PlayId\": 3, \"TheatreId\": 5 },  { \"Id\": 30, \"Price\": 68.56, \"RowNumber\": 1, \"PlayId\": 2, \"TheatreId\": 5 },  { \"Id\": 31, \"Price\": 93.41, \"RowNumber\": 1, \"PlayId\": 1, \"TheatreId\": 5 },  { \"Id\": 32, \"Price\": 86.14, \"RowNumber\": 5, \"PlayId\": 3, \"TheatreId\": 5 },  { \"Id\": 33, \"Price\": 17.47, \"RowNumber\": 6, \"PlayId\": 2, \"TheatreId\": 5 },  { \"Id\": 34, \"Price\": 13.83, \"RowNumber\": 3, \"PlayId\": 1, \"TheatreId\": 5 },  { \"Id\": 35, \"Price\": 15.59, \"RowNumber\": 6, \"PlayId\": 3, \"TheatreId\": 5 },  { \"Id\": 36, \"Price\": 13.07, \"RowNumber\": 8, \"PlayId\": 2, \"TheatreId\": 5 },  { \"Id\": 37, \"Price\": 31.62, \"RowNumber\": 7, \"PlayId\": 1, \"TheatreId\": 5 },  { \"Id\": 38, \"Price\": 55.96, \"RowNumber\": 1, \"PlayId\": 3, \"TheatreId\": 5 },  { \"Id\": 39, \"Price\": 58.37, \"RowNumber\": 7, \"PlayId\": 2, \"TheatreId\": 5 },  { \"Id\": 40, \"Price\": 76.77, \"RowNumber\": 1, \"PlayId\": 1, \"TheatreId\": 5 },  { \"Id\": 41, \"Price\": 84.76, \"RowNumber\": 9, \"PlayId\": 3, \"TheatreId\": 5 },  { \"Id\": 42, \"Price\": 73.58, \"RowNumber\": 9, \"PlayId\": 2, \"TheatreId\": 5 },  { \"Id\": 43, \"Price\": 51.5, \"RowNumber\": 6, \"PlayId\": 1, \"TheatreId\": 5 },  { \"Id\": 44, \"Price\": 89.58, \"RowNumber\": 5, \"PlayId\": 2, \"TheatreId\": 4 },  { \"Id\": 45, \"Price\": 23.61, \"RowNumber\": 5, \"PlayId\": 2, \"TheatreId\": 5 },  { \"Id\": 46, \"Price\": 40.18, \"RowNumber\": 5, \"PlayId\": 3, \"TheatreId\": 3 },  { \"Id\": 47, \"Price\": 15.94, \"RowNumber\": 1, \"PlayId\": 1, \"TheatreId\": 2 },  { \"Id\": 48, \"Price\": 44.35, \"RowNumber\": 1, \"PlayId\": 2, \"TheatreId\": 1 },  { \"Id\": 49, \"Price\": 17.85, \"RowNumber\": 10, \"PlayId\": 1, \"TheatreId\": 1 },  { \"Id\": 50, \"Price\": 26.36, \"RowNumber\": 10, \"PlayId\": 3, \"TheatreId\": 1 },  { \"Id\": 51, \"Price\": 42.35, \"RowNumber\": 4, \"PlayId\": 2, \"TheatreId\": 1 },  { \"Id\": 52, \"Price\": 76.63, \"RowNumber\": 4, \"PlayId\": 1, \"TheatreId\": 1 },  { \"Id\": 53, \"Price\": 90.55, \"RowNumber\": 8, \"PlayId\": 3, \"TheatreId\": 1 },  { \"Id\": 54, \"Price\": 97.74, \"RowNumber\": 7, \"PlayId\": 2, \"TheatreId\": 1 },  { \"Id\": 55, \"Price\": 90.09, \"RowNumber\": 6, \"PlayId\": 1, \"TheatreId\": 1 },  { \"Id\": 56, \"Price\": 13.13, \"RowNumber\": 2, \"PlayId\": 3, \"TheatreId\": 1 },  { \"Id\": 57, \"Price\": 69.76, \"RowNumber\": 3, \"PlayId\": 2, \"TheatreId\": 1 },  { \"Id\": 58, \"Price\": 87.5, \"RowNumber\": 2, \"PlayId\": 1, \"TheatreId\": 1 },  { \"Id\": 59, \"Price\": 33.25, \"RowNumber\": 3, \"PlayId\": 3, \"TheatreId\": 1 },  { \"Id\": 60, \"Price\": 83.52, \"RowNumber\": 7, \"PlayId\": 1, \"TheatreId\": 1 },  { \"Id\": 61, \"Price\": 60.21, \"RowNumber\": 10, \"PlayId\": 2, \"TheatreId\": 1 },  { \"Id\": 62, \"Price\": 73.74, \"RowNumber\": 6, \"PlayId\": 3, \"TheatreId\": 1 },  { \"Id\": 63, \"Price\": 57.49, \"RowNumber\": 9, \"PlayId\": 1, \"TheatreId\": 1 },  { \"Id\": 64, \"Price\": 72.06, \"RowNumber\": 8, \"PlayId\": 2, \"TheatreId\": 1 },  { \"Id\": 65, \"Price\": 72.54, \"RowNumber\": 5, \"PlayId\": 3, \"TheatreId\": 1 },  { \"Id\": 66, \"Price\": 35.55, \"RowNumber\": 4, \"PlayId\": 1, \"TheatreId\": 1 },  { \"Id\": 67, \"Price\": 87.22, \"RowNumber\": 6, \"PlayId\": 3, \"TheatreId\": 1 },  { \"Id\": 68, \"Price\": 77.95, \"RowNumber\": 2, \"PlayId\": 2, \"TheatreId\": 2 },  { \"Id\": 69, \"Price\": 81.62, \"RowNumber\": 1, \"PlayId\": 1, \"TheatreId\": 1 },  { \"Id\": 70, \"Price\": 81.4, \"RowNumber\": 8, \"PlayId\": 3, \"TheatreId\": 2 },  { \"Id\": 71, \"Price\": 80.91, \"RowNumber\": 6, \"PlayId\": 3, \"TheatreId\": 2 },  { \"Id\": 72, \"Price\": 82.07, \"RowNumber\": 1, \"PlayId\": 2, \"TheatreId\": 2 },  { \"Id\": 73, \"Price\": 51.37, \"RowNumber\": 4, \"PlayId\": 1, \"TheatreId\": 2 },  { \"Id\": 74, \"Price\": 92.92, \"RowNumber\": 6, \"PlayId\": 3, \"TheatreId\": 2 },  { \"Id\": 75, \"Price\": 47.19, \"RowNumber\": 8, \"PlayId\": 2, \"TheatreId\": 2 },  { \"Id\": 76, \"Price\": 80.08, \"RowNumber\": 2, \"PlayId\": 1, \"TheatreId\": 2 },  { \"Id\": 77, \"Price\": 84.64, \"RowNumber\": 5, \"PlayId\": 3, \"TheatreId\": 2 },  { \"Id\": 78, \"Price\": 68.88, \"RowNumber\": 5, \"PlayId\": 2, \"TheatreId\": 2 },  { \"Id\": 79, \"Price\": 40.54, \"RowNumber\": 5, \"PlayId\": 1, \"TheatreId\": 2 },  { \"Id\": 80, \"Price\": 58.99, \"RowNumber\": 5, \"PlayId\": 3, \"TheatreId\": 2 },  { \"Id\": 81, \"Price\": 77.37, \"RowNumber\": 3, \"PlayId\": 2, \"TheatreId\": 2 },  { \"Id\": 82, \"Price\": 44.85, \"RowNumber\": 2, \"PlayId\": 1, \"TheatreId\": 2 },  { \"Id\": 83, \"Price\": 83.78, \"RowNumber\": 5, \"PlayId\": 3, \"TheatreId\": 2 },  { \"Id\": 84, \"Price\": 12.53, \"RowNumber\": 7, \"PlayId\": 2, \"TheatreId\": 2 },  { \"Id\": 85, \"Price\": 55.33, \"RowNumber\": 2, \"PlayId\": 1, \"TheatreId\": 2 },  { \"Id\": 86, \"Price\": 17.34, \"RowNumber\": 3, \"PlayId\": 3, \"TheatreId\": 2 },  { \"Id\": 87, \"Price\": 18.75, \"RowNumber\": 7, \"PlayId\": 2, \"TheatreId\": 2 },  { \"Id\": 88, \"Price\": 61.01, \"RowNumber\": 4, \"PlayId\": 2, \"TheatreId\": 2 },  { \"Id\": 89, \"Price\": 82.84, \"RowNumber\": 1, \"PlayId\": 1, \"TheatreId\": 2 },  { \"Id\": 90, \"Price\": 29.69, \"RowNumber\": 7, \"PlayId\": 2, \"TheatreId\": 2 },  { \"Id\": 91, \"Price\": 4.28, \"RowNumber\": 2, \"PlayId\": 3, \"TheatreId\": 5 },  { \"Id\": 92, \"Price\": 4.28, \"RowNumber\": 2, \"PlayId\": 4, \"TheatreId\": 5 },  { \"Id\": 93, \"Price\": 4.28, \"RowNumber\": 2, \"PlayId\": 4, \"TheatreId\": 5 },  { \"Id\": 94, \"Price\": 4.28, \"RowNumber\": 2, \"PlayId\": 4, \"TheatreId\": 5 },  { \"Id\": 95, \"Price\": 4.28, \"RowNumber\": 2, \"PlayId\": 5, \"TheatreId\": 5 },  { \"Id\": 96, \"Price\": 4.28, \"RowNumber\": 2, \"PlayId\": 5, \"TheatreId\": 5 },  { \"Id\": 97, \"Price\": 4.28, \"RowNumber\": 2, \"PlayId\": 5, \"TheatreId\": 5 }]}";

        public static string ImportPlays(TheatreContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();
            var serializer = new XmlSerializer(typeof( ImportPlaysModel[]), new XmlRootAttribute("Plays"));
            var textReader = new StringReader(xmlString);
            var allColection = serializer.Deserialize(textReader) as ImportPlaysModel[];
            List<Play> realColection = new List<Play>();
            foreach (var item in allColection)
            {
                if (!IsValid(item))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                TimeSpan duration;
                bool isValidDueDate = TimeSpan.TryParseExact(item.Duration, "c",
                    CultureInfo.InvariantCulture, TimeSpanStyles.None, out duration);
                if (!isValidDueDate||duration.Hours<1)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
   
                var isEnumValid = Enum.TryParse(typeof(Genre), item.Genre, out object genre);
                if (!isEnumValid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var play = new Play()
                {
                    Title = item.Title,
                    Duration = duration,
                    Rating = item.Rating,
                    Genre = (Genre)genre,
                    Description = item.Description,
                    Screenwriter = item.Screenwriter
                };


                sb.AppendLine(string.Format(SuccessfulImportPlay, play.Title, play.Genre.ToString(),play.Rating));
                realColection.Add(play);
            }
            context.Plays.AddRange(realColection);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportCasts(TheatreContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();
            var serializer = new XmlSerializer(typeof(ImportCastsModel[]), new XmlRootAttribute("Casts"));
            var textReader = new StringReader(xmlString);
            var allColection = serializer.Deserialize(textReader)as ImportCastsModel[];
            List<Cast> realColection = new List<Cast>();
            foreach (var item in allColection)
            {
                if (!IsValid(item))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var cast = new Cast()
                {
                    FullName = item.FullName,
                    IsMainCharacter = item.IsMainCharacter,
                    PhoneNumber = item.PhoneNumber,
                    PlayId = item.PlayId
                };
                realColection.Add(cast);

                sb.AppendLine(string.Format(SuccessfulImportActor, cast.FullName, cast.IsMainCharacter? "main" : "lesser"));

            }
            context.Casts.AddRange(realColection);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportTtheatersTickets(TheatreContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();
            ImportTheatresModel[] AllColection = JsonConvert.DeserializeObject <ImportTheatresModel[] > (jsonString);
            List<Data.Models.Theatre> realColection = new List<Data.Models.Theatre>();
            foreach (var item in AllColection)
            {
                if (!IsValid(item))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var teahtor = new Data.Models.Theatre()
                {
                    Name = item.Name,
                    NumberOfHalls = item.NumberOfHalls,
                    Director = item.Director
                };
                foreach (var ticket in item.Tickets)
                {
                    if (!IsValid(ticket))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    var realTicket = new Ticket()
                    {
                        Price = ticket.Price,
                        RowNumber = ticket.RowNumber,
                        PlayId = ticket.PlayId
                    };
                    teahtor.Tickets.Add(realTicket);
                }

                realColection.Add(teahtor);
                sb.AppendLine(string.Format(SuccessfulImportTheatre, teahtor.Name, teahtor.Tickets.Count));
            }

            context.Theatres.AddRange(realColection);
            context.SaveChanges();

            return sb.ToString().TrimEnd();

        }


        private static bool IsValid(object obj)
        {
            var validator = new ValidationContext(obj);
            var validationRes = new List<ValidationResult>();

            var result = Validator.TryValidateObject(obj, validator, validationRes, true);
            return result;
        }
    }
}
