using System;
using System.Collections.Generic;
using System.Linq;

namespace Rod_Cutting​
{
    public class Program
    {

        private static int[] prices;
        private static int[] bestPrices;
        private static int[] combo;

        static void Main()
        {
            prices = Console.ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();

            bestPrices = new int[prices.Length];
            combo = new int[prices.Length];
            int cutRoad = int.Parse(Console.ReadLine());
            Console.WriteLine(CutRod(cutRoad));
            while (cutRoad!=0)
            {
                Console.Write($"{combo[cutRoad]} ");
                cutRoad -= combo[cutRoad];
            }
        }

     public   static int CutRod(int length)
     {

         if (length == 0)
         {
             return 0;
         }

         if (bestPrices[length] != 0)
         {
             return bestPrices[length];
         }
            var bestPrice = prices[length];

            var bestCombo = length;

            for (int i = 1; i < length; i++)
            {

                var price = prices[i] + CutRod(length - i);

                if (price > bestPrice)
                {
                    bestPrice = price;
                    bestCombo = i;
                }
            }

            bestPrices[length] = bestPrice;

            combo[length] = bestCombo;

            return bestPrice;

        }
    }
}
