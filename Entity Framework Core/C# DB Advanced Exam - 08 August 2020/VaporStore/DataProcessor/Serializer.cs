using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using VaporStore.DataProcessor.Dto.Export;

namespace VaporStore.DataProcessor
{
	using System;
	using Data;

	public static class Serializer
	{
		public static string ExportGamesByGenres(VaporStoreDbContext context, string[] genreNames)
        {
            var genres = context
                .Genres
                .ToArray()
                .Where(g => genreNames.Contains(g.Name))
                .Select(g => new
                {
                    Id = g.Id,
                    Genre = g.Name,
                    Games = g.Games
                        .Where(ga => ga.Purchases.Any())
                        .Select(ga => new
                        {
                            Id = ga.Id,
                            Title = ga.Name,
                            Developer = ga.Developer.Name,
                            Tags = String.Join(", ", ga.GameTags
                                .Select(gt => gt.Tag.Name)
                                .ToArray()),
                            Players = ga.Purchases.Count
                        })
                        .OrderByDescending(ga => ga.Players)
                        .ThenBy(ga => ga.Id)
                        .ToArray(),
                    TotalPlayers = g.Games.Sum(ga => ga.Purchases.Count)
                })
                .OrderByDescending(g => g.TotalPlayers)
                .ThenBy(g => g.Id)
                .ToArray();

            string json = JsonConvert.SerializeObject(genres, Formatting.Indented);

            return json;
        }

		public static string ExportUserPurchasesByType(VaporStoreDbContext context, string storeType)
        {
            StringBuilder sb = new StringBuilder();
            var namespases = new XmlSerializerNamespaces();
            namespases.Add(string.Empty, string.Empty);
            var users = context.Users.ToArray().Where(x => x.Cards.Any(c => c.Purchases.Any())).
                    Select(x=>new ExportUser
                        {
                            Username = x.Username,
                        Purchases = context.Purchases.ToArray().Where(y => y.Card.Id != null && y.Card.Purchases.Any(z => z.Id != null) && y.Card.User.Username == x.Username)
                                .Select(y => new ExportPurchase
                                {
                                    Card = y.Card.Number,
                                    Cvc = y.Card.Cvc,
                                    Date = y.Date.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                                    Game = new ExportGameDetails
                                    {
                                        Title = y.Game.Name,
                                        Genre = y.Game.Genre.ToString(),
                                        Price = y.Game.Price
                                    }

                                }).OrderBy(y => y.Date).ToArray()
                    }
                    ).OrderByDescending(x=>x.Purchases.Sum(y=>y.Game.Price))
                    .ThenBy(x=>x.Username)
                    .ToArray();
            var xml = new XmlSerializer(typeof(ExportUser[]), new XmlRootAttribute("Users"));
            xml.Serialize(new StringWriter(sb), users, namespases);
            return sb.ToString().TrimEnd();
        }
	}
}