using BookShop.Data.Models;
using BookShop.Data.Models.Enums;
using BookShop.DataProcessor.ImportDto;

namespace BookShop.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedBook
            = "Successfully imported book {0} for {1:F2}.";

        private const string SuccessfullyImportedAuthor
            = "Successfully imported author - {0} with {1} books.";

        public static string ImportBooks(BookShopContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();
            var serializer = new XmlSerializer(typeof(ImportBookModel[]), new XmlRootAttribute("Books"));
            var textReader = new StringReader(xmlString);
            var allColection = serializer.Deserialize(textReader) as ImportBookModel[];
            List<Book> realColection = new List<Book>();
            foreach (var item in allColection)
            {
                if (!IsValid(item))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                DateTime date;
                bool isValidDate = DateTime.TryParseExact(item.PublishedOn, "MM/dd/yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out date);

                if (!isValidDate)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var realBook = new Book()
                {
                    Name = item.Name,
                    Genre = (Genre)item.Genre,
                    Price = item.Price,
                    Pages = item.Pages,
                    PublishedOn = date
                };

                realColection.Add(realBook);
                sb.AppendLine($"Successfully imported book {realBook.Name} for {realBook.Price:F2}.");

            }
            context.Books.AddRange(realColection);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
         
        }

        public static string ImportAuthors(BookShopContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();
            var AllColection = JsonConvert.DeserializeObject<ImportAuthorModel[]>(jsonString);
            List<Author> realColection = new List<Author>();
            foreach (var item in AllColection)
            {
                if (!IsValid(item))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var realAuthor = new Author()
                {
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Email = item.Email,
                    Phone = item.Phone
                };

                foreach (var bookID in item.Books)
                {
                    var book = context.Books.FirstOrDefault(x => x.Id == bookID.Id);
                    //if (bookID.Id == null)
                    //{
                    //    sb.AppendLine(ErrorMessage);
                    //    continue;
                    //}
                    if (book==null)
                    {
                        continue;
                    }
                    realAuthor.AuthorsBooks.Add(new AuthorBook()
                    {
                        Author = realAuthor,
                        Book = book
                    });
                }

                if (realAuthor.AuthorsBooks.Count==0)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                realColection.Add(realAuthor);
                sb.AppendLine($"Successfully imported author - {realAuthor.FirstName} {realAuthor.LastName} with {realAuthor.AuthorsBooks.Count} books.");
            }

            context.Authors.AddRange(realColection);
            context.SaveChanges();
            return sb.ToString().TrimEnd();

        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}