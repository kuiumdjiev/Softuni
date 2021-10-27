using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        static string inputUsers = "../../../Datasets/users.json";
        static string inputProducts = "../../../Datasets/products.json";
        static string inputCategories = "../../../Datasets/categories.json";
        private static string inputCategoriesProducts = "../../../Datasets/categories-products.json";
        public static void Main(string[] args)
        {
            var database = new ProductShopContext();
            database.Database.EnsureDeleted();
            database.Database.EnsureCreated();
            Console.WriteLine(GetUsersWithProducts(database));
        }

        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            User[] users = JsonConvert.DeserializeObject<User[]>(inputJson);
            context.Users.AddRange(users);
            context.SaveChanges();
            return $"Successfully imported {users.Length}";
        }

        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            Product[] products = JsonConvert.DeserializeObject<Product[]>(inputJson);
            context.Products.AddRange(products);
            context.SaveChanges();
           return $"Successfully imported {products.Length}";
        }

        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            Category[] categories = JsonConvert.DeserializeObject<Category[]>(inputJson)
                .Where(x=>x.Name!=null).ToArray();
            context.Categories.AddRange(categories);
            context.SaveChanges();
            return $"Successfully imported {categories.Length}";
        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            CategoryProduct[] categoryProducts = JsonConvert.DeserializeObject<CategoryProduct[]>(inputJson);
            context.CategoryProducts.AddRange(categoryProducts);
            context.SaveChanges();
            return $"Successfully imported {categoryProducts.Length}";
        }

        public static string GetProductsInRange(ProductShopContext context)
        {
            var product = context.Products
                .Where(x => x.Price >= 500 && x.Price <= 1000)
                .Select(x => new 
                {
                    name = x.Name,
                    price = x.Price,
                    seller = x.Seller.FirstName + " " + x.Seller.LastName
                })
                .OrderBy(x=>x.price)
                .ToArray();

            var result = JsonConvert.SerializeObject(product, Formatting.Indented);
            return result;
        }

        public static string GetSoldProducts(ProductShopContext context)
        {
            var users = context.Users
                .Where(x => x.ProductsSold.Any(u=>u.BuyerId!=null))
                .Select(x => new
                {
                    firstName = x.FirstName,
                    lastName= x.LastName,
                    soldProducts= x.ProductsSold.
                        Where(f=>f.Buyer !=null).Select(y=>new
                    {
                        name = y.Name,
                        price = y.Price,
                        buyerFirstName = y.Buyer.FirstName,
                        buyerLastName = y.Buyer.LastName
                    }).ToArray()
                })
                .OrderBy(z => z.lastName)
                .ThenBy(z => z.firstName)
                .ToArray();
            var result = JsonConvert.SerializeObject(users, Formatting.Indented);
            return result;
        }

        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categorys = context.CategoryProducts
                .Select(x => new
                {
                    category = x.Category.Name,
                    productsCount = x.Category.CategoryProducts.Select(z => z.ProductId).Count(),
                    averagePrice = x.Category.CategoryProducts.Average(s => s.Product.Price).ToString("F2"),
                    totalRevenue = x.Category.CategoryProducts.Sum(f => f.Product.Price).ToString()
                })
                .OrderByDescending(x => x.productsCount)
                .ToArray();
            var result = JsonConvert.SerializeObject(categorys, Formatting.Indented);
            return result;
        }

        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var users = context.Users.AsEnumerable().Where(x => x.ProductsSold.Any(y => y != null))
                .Select(g => new
                {
                    firstName = g.FirstName,
                    lastName = g.LastName,
                    age = g.Age,
                    soldProducts = g.ProductsSold.Select(y => new
                    {
                        count = g.ProductsSold.Count,
                        products = g.ProductsSold.Select(j => new
                        {
                            name = j.Name,
                            price = j.Price
                        }).ToList()
                    })
                }).ToList();

            var part2 = new
            {
                usersCount = users.Count,
                users = users
            };
            var result = JsonConvert.SerializeObject(part2, new JsonSerializerSettings{
                Formatting = Formatting.Indented,
                NullValueHandling= NullValueHandling.Ignore
        });
        return result;
        }
    }
}