using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;
using VaporStore.Data.Models;
using VaporStore.Data.Models.Enums;
using VaporStore.DataProcessor.Dto.Import;

namespace VaporStore.DataProcessor
{
	using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Data;

	public static class Deserializer
	{

        private static  string  Error= "Invalid Data";
		public static string ImportGames(VaporStoreDbContext context, string jsonString)
		{

            StringBuilder sb = new StringBuilder();
            GamesImportModel[] Allgames = JsonConvert.DeserializeObject<GamesImportModel[]>(jsonString);
            List<Game> realGames = new List<Game>();
            foreach (var game in Allgames)
            {
                if (!IsValid(game))
                {
                    sb.AppendLine(Error);
                    continue;
                }

                if (game.Tags.Length == 0)
                {
                    sb.AppendLine(Error);
                    continue;
                }


                DateTime releaseDate;
                bool isValidReleaseDate = DateTime.TryParseExact(game.ReleaseDate, "yyyy-MM-dd",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out releaseDate);

                if (!isValidReleaseDate)
                {
                    sb.AppendLine(Error);
                    continue;
                }


                var devolper = context.Developers.FirstOrDefault(x => x.Name == game.Developer);
                if (devolper == null)
                {
                    devolper = new Developer()
                    {
                        Name = game.Developer,
                    };
                    context.Developers.Add(devolper);
                    
                }

                var genre = context.Genres.FirstOrDefault(x => x.Name == game.Genre);
                if (genre == null)
                {
                    genre = new Genre()
                    {
                        Name = game.Genre
                    };
                    context.Genres.Add(genre);
                    
                }

                Game realGame = new Game()
                {
                    Name = game.Name,
                    Price = game.Price,
                    ReleaseDate = releaseDate,
                    Developer = devolper,
                    Genre = genre
                };
                List<GameTag> realTags = new List<GameTag>();

                foreach (var tag in game.Tags)
                {

                    var realTag = context.GameTags.FirstOrDefault(x => x.Tag.Name == tag);
                    if (realTag == null)
                    {
                        realTag = new GameTag()
                        {
                            Tag = new Tag()
                            {
                                Name = tag
                            },
                            Game = realGame
                        };
                        context.GameTags.Add(realTag);
                        realTags.Add(realTag);
                    }

                }

              
                realGame.GameTags = realTags;
                realGames.Add(realGame);
                sb.AppendLine($"Added {realGame.Name} ({realGame.Genre.Name}) with {realGame.GameTags.Count} tags");
            }

            context.Games.AddRange(realGames);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

		public static string ImportUsers(VaporStoreDbContext context, string jsonString)
        {

            StringBuilder sb = new StringBuilder();
            ImportUser[] AllUser = JsonConvert.DeserializeObject<ImportUser[]>(jsonString);
            List<User> realUsers = new List<User>();

            foreach (var user in AllUser)
            {
                if (!IsValid(user))
                {
                    sb.AppendLine(Error);
                    continue;
                }

                var realUser = new User()
                {
                    FullName = user.FullName,
                    Age = user.Age,
                    Email = user.Email,
                    Username = user.Username
                };
                List<Card> realCards= new List<Card>();
                foreach (var card in user.Cards)
                {
                    if (!IsValid(card))
                    {
                        sb.AppendLine(Error);
                        continue;
                    }

                    CardType type;
                    switch (card.Type)
                    {
                        case "Debit":
                            type = CardType.Debit;
                        break;
                        case "Credit": 
                            type= CardType.Credit;
                            break;
                        default:
                            sb.AppendLine(Error);
                            continue;
                    }

                    var realCard = new Card()
                    {
                        Cvc = card.CVC,
                        Number = card.Number,
                        Type = type
                    };
                    realCards.Add(realCard);
                }

                realUser.Cards = realCards;
                realUsers.Add(realUser);

                sb.AppendLine($"Imported {realUser.Username} with {realUser.Cards.Count} cards");
            }

            context.Users.AddRange(realUsers);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

		public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
		{
            StringBuilder sb = new StringBuilder();
            var serializer = new XmlSerializer(typeof(ImportPurchases[]), new XmlRootAttribute("Purchases"));
            var textReader = new StringReader(xmlString);
            var allPurchase = serializer.Deserialize(textReader) as ImportPurchases[];
            var realPurchases = new List<Purchase>();
            foreach (var purchase in allPurchase)
            {
                if (!IsValid(purchase))
                {
                    sb.AppendLine(Error);
                    continue;
                }

                DateTime date;
                bool isValidReleaseDate = DateTime.TryParseExact(purchase.Date, "dd/MM/yyyy HH:mm",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out date);

                if (!isValidReleaseDate)
                {
                    sb.AppendLine(Error);
                    continue;
                }

                PurchaseType type;
                switch (purchase.Type)
                {
                    case "Digital":
                        type =PurchaseType.Digital;
                        break;
                    case "Retail":
                        type = PurchaseType.Retail;
                        break;
                    default:
                        sb.AppendLine(Error);
                        continue;
                }

                var realGame = context.Games.FirstOrDefault(x => x.Name == purchase.Title);
                if (realGame == null)
                {
                    sb.AppendLine(Error);
                    continue;
                }

                var realCard = context.Cards.FirstOrDefault(x => x.Number == purchase.Card);
                if (realCard == null)
                {
                    sb.AppendLine(Error);
                    continue;
                }

                var realPuchase = new Purchase()
                {
                    Type = type,
                    ProductKey = purchase.Card,
                    Card = realCard,
                    Date = date,
                    Game = realGame
                };
                realPurchases.Add(realPuchase);
                sb.AppendLine($"Imported {realPuchase.Game.Name} for {realPuchase.Card.User.Username}");

            }
            context.Purchases.AddRange(realPurchases);
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