using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using ProductShop.Data;
using ProductShop.Dtos.Export;
using ProductShop.Dtos.Import;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        private static string usersXml = File.ReadAllText("../../../Datasets/users.xml");
        private static string productsXml = File.ReadAllText("../../../Datasets/products.xml");
        private static string categoriesXml = File.ReadAllText("../../../Datasets/categories.xml");
        private static string categoriesProductsXml = File.ReadAllText("../../../Datasets/categories-products.xml");

        public static void Main(string[] args)
        {
            var database = new ProductShopContext();
       //     database.Database.EnsureDeleted();
         //   database.Database.EnsureCreated();
            Console.WriteLine(GetCategoriesByProductsCount(database));
        }

        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(UsersImportModel[]), new XmlRootAttribute("Users"));
            var textRead = new StringReader(inputXml);
            var userConvert = serializer.Deserialize(textRead) as UsersImportModel[];
            var users = userConvert.Select(x => new User
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Age = x.Age
            }).ToList();

            context.Users.AddRange(users);
            context.SaveChanges();
            return $"Successfully imported {users.Count()}";
        }

        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(ProductImportModel[]), new XmlRootAttribute("Products"));
            var textReader = new StringReader(inputXml);
            var productConvert = serializer.Deserialize(textReader) as ProductImportModel[];
            var products = new List<Product>();
            foreach (var productImport in productConvert)
            {
                Product product = new Product()
                {
                    Name = productImport.Name,
                    Price = productImport.Price,
                    SellerId = productImport.SellerId,
                };
                if (productImport.BuyerId != 0)
                {
                    product.BuyerId = productImport.BuyerId;
                }

                products.Add(product);

            }

            context.Products.AddRange(products);
            context.SaveChanges();
            return $"Successfully imported {products.Count}";

        }

        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(CategoryImportModel[]), new XmlRootAttribute("Categories"));
            var textReader = new StringReader(inputXml);
            var categoryConvert = serializer.Deserialize(textReader) as CategoryImportModel[];
            var categories = categoryConvert.Where(x=>x.Name !=null).Select(x=>new Category
            {
                Name=x.Name
            }).ToList();

            context.Categories.AddRange(categories);
            context.SaveChanges();
         
            return $"Successfully imported {categories.Count}";
        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(CategoryProductImportModel[]),
                new XmlRootAttribute("CategoryProducts"));
            var textReader = new StringReader(inputXml);
            var categoryProductConvert = serializer.Deserialize(textReader) as CategoryProductImportModel[];
            var categoriesProducts = categoryProductConvert.Where(x => x.ProductId != null && x.CategoryId != null)
                .Select(x => new CategoryProduct
                {
                    CategoryId = x.CategoryId,
                    ProductId = x.ProductId
                }).ToList();

            context.CategoryProducts.AddRange(categoriesProducts);
            context.SaveChanges();
            return $"Successfully imported {categoriesProducts.Count}";
        }
         public static string GetProductsInRange(ProductShopContext context)
         {
             StringBuilder sb = new StringBuilder();
             var namespases = new XmlSerializerNamespaces();
             namespases.Add(string.Empty, string.Empty);

             var product = context.Products.Where(x => x.Price >= 500 && x.Price <= 1000)
                 .Select(x => new ExportProductInRange
                 {
                     Name = x.Name,
                     Price = x.Price,
                     Buyer = x.Buyer.FirstName + " " + x.Buyer.LastName
                 })
                 .OrderBy(y=>y.Price)
                 .Take(10)
                 .ToArray();
             var xml = new XmlSerializer(typeof(ExportProductInRange[]), new XmlRootAttribute("Products"));
            xml.Serialize(new  StringWriter(sb),product , namespases);
             return sb.ToString().TrimEnd();
         }

         public static string GetSoldProducts(ProductShopContext context)
         {
             StringBuilder sb = new StringBuilder();
             var namespases = new XmlSerializerNamespaces();
             namespases.Add(string.Empty, string.Empty);

             var soldProduct = context.Users.Where(x => x.ProductsSold.Any(y => y.BuyerId != null))
                 .Select(x => new ExportUsers
                 {
                     FirstName = x.FirstName,
                     LastName = x.LastName,
                     SoldProducts = x.ProductsSold.Select(y => new ExportUsersSoldProducts
                     {
                         Name = y.Name,
                         Price = y.Price
                     }).ToArray()
                 })
                 .OrderBy(x => x.LastName)
                 .ThenBy(x => x.FirstName)
                 .Take(5)
                 .ToArray();

             var xml = new XmlSerializer(typeof(ExportUsers[]), new XmlRootAttribute("Users"));
             xml.Serialize(new StringWriter(sb), soldProduct, namespases);
             return sb.ToString().TrimEnd();
         }

         public static string GetCategoriesByProductsCount(ProductShopContext context)
         {
             StringBuilder sb = new StringBuilder();
             var namespases = new XmlSerializerNamespaces();
             namespases.Add(string.Empty, string.Empty);

             var categorys = context.Categories
                 .Select(x => new ExportCategory
                 {
                     Name = x.Name,
                     Count = x.CategoryProducts.Count,
                     AveragePrice = x.CategoryProducts.Average(y => y.Product.Price),
                     TotalRevenue = x.CategoryProducts.Sum(y => y.Product.Price)
                 })
                 .OrderByDescending(x => x.Count)
                 .ThenBy(x => x.TotalRevenue)
                 .ToArray();

             var xml = new XmlSerializer(typeof(ExportCategory[]), new XmlRootAttribute("Categories"));
             xml.Serialize(new StringWriter(sb), categorys, namespases);
             return sb.ToString().TrimEnd();
        }

    }
}