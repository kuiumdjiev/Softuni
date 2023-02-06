using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Sum_of_Coins
{
    public class Program
    {
        static void Main(string[] args)
        {
            var coins = new Queue<int>(Console.ReadLine().Split(", ").Select(x=>int.Parse(x)).OrderByDescending(x=>x));

            var target = int.Parse(Console.ReadLine());
            var selectedCoins = new Dictionary<int, int>();
            var totalCoins = 0;
            while (target>0&& coins.Count>0)
            {
                var currnetCoin= coins.Dequeue();
                var count = target / currnetCoin;
                if (count == 0)
                {
                    continue;
                }

                selectedCoins[currnetCoin] = count;
                totalCoins+=count;
                target %= currnetCoin;
            }

            if (target !=0)
            {
                Console.WriteLine("Error");
            }
            else
            {
                Console.WriteLine($"Number of coins to take: {totalCoins}");
                foreach (var i in selectedCoins)
                {
                    Console.WriteLine($"{i.Value} coin(s) with value {i.Key}");
                }
            }
        }
    }
}
