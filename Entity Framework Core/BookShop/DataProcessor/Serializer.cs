using BookShop.DataProcessor.ExportDto;

namespace BookShop.DataProcessor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportMostCraziestAuthors(BookShopContext context)
        {
            var author = context.Authors.Select(x => new
            {
                AuthorName= x.FirstName+" "+ x.LastName,
                Books = x.AuthorsBooks.
                    OrderByDescending(y => y.Book.Price)
                    .Select(y=>new
                {
                    BookName= y.Book.Name,
                    BookPrice= y.Book.Price.ToString("F2")
                })
                    .ToArray()
            })
                .ToArray()
                .OrderByDescending(x=>x.Books.Length)
                .ThenBy(x=>x.AuthorName)
                .ToArray();
            string json = JsonConvert.SerializeObject(author, Formatting.Indented);

            return json;
        }

        public static string ExportOldestBooks(BookShopContext context, DateTime date)
        {
            StringBuilder sb = new StringBuilder();
            var namespases = new XmlSerializerNamespaces();
            namespases.Add(string.Empty, string.Empty);

            var books = context.Books.Where(x => x.PublishedOn < date&& x.Genre.ToString()== "Science")
                .ToArray()
                .Select(x => new ExportBookModel
                {
                    Name = x.Name,
                    Date = x.PublishedOn.ToString("d", CultureInfo.InvariantCulture),
                    Pages = x.Pages
                })
                .OrderByDescending(x => x.Pages)
                .ThenByDescending(x => x.Date)
                .Take(10)
                .ToArray();

            var xml = new XmlSerializer(typeof(ExportBookModel[]), new XmlRootAttribute("Books"));
            xml.Serialize(new StringWriter(sb), books, namespases);
            return sb.ToString().TrimEnd();
        }
    }
}