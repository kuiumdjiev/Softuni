using System;
using System.Collections.Generic;
using System.Linq;

namespace _4._Cities_by_Continent_and_Country
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, Dictionary<string, List<string>>> client = new Dictionary<string, Dictionary<string, List<string>>>();
            int input = int.Parse(Console.ReadLine());
            for (int i = 0; i < input; i++)
            {
                string[] info = Console.ReadLine().Split(' ').ToArray();
                string continents = info[0];
                string countries = info[1];
                string city = info[2];
                if (!client.ContainsKey(continents))
                {
                    client.Add(continents,new Dictionary<string, List<string>>());
                }
                if (!client[continents].ContainsKey(countries))
                {
                    client[continents].Add(countries,new List<string>());
                }
                client[continents][countries].Add(city);
            }
            foreach (var continent in client)
            {
                Console.WriteLine($"{continent.Key}:");
                foreach (var country in continent.Value)
                {
                    Console.WriteLine($"{country.Key} -> {string.Join(", ", country.Value)}");
                }
            }

        }
    }
}
