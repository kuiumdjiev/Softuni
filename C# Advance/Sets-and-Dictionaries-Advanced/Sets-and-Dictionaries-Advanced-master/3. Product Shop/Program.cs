using System;
using System.Collections.Generic;
using System.Linq;

namespace _3._Product_Shop
{
    class Program
    {
        static void Main(string[] args)
        {
            SortedDictionary<string, Dictionary<string, double>> food = new SortedDictionary<string, Dictionary<string, double>>();
            string comand = string.Empty;
            while ((comand = Console.ReadLine()) != "Revision")
            {
                string[] info = comand
                    .Split(' ')
                    .ToArray();
                string shop = info[0];
                string product = info[1];
                double price = double.Parse(info[2]);
                if (!food.ContainsKey(shop))
                {
                    food.Add(shop, new Dictionary<string, double>());
                  
                }
                food[shop].Add(product, price);
            }
            foreach (var shop in food)
            {
                Console.WriteLine(shop.Key);
                foreach (var product in shop.Value)
                {
                    Console.WriteLine($"{product.Key} ->{product.Value}");
                }
            }
        }
    }
}
