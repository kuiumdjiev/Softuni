using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Rod_Cutting​
{
    public class Program
    {

        private static List<int> price;
        private static int[] bestPrices;

        static void Main()
        {
            price= new List<int>{0}; 
                price.AddRange( Console.ReadLine()
                .Split(' ')
                .Select(int.Parse)
                );

            bestPrices = new int[price.Count];

            int connectorPrice = int.Parse(Console.ReadLine());

CutRod(price.Count-1 , connectorPrice);

Console.WriteLine(string.Join(" ",bestPrices.Skip(1)));

        }

        public static int CutRod(int length, int connectorPrice)
        {

            if (length == 0)
            {
                return 0;
            }

            if (bestPrices[length] != 0)
            {
                return bestPrices[length];
            }

            var bestPrice = price[length];


            for (int i = 1; i < length; i++)
            {

                var price = Program.price[i] + CutRod(length - i, connectorPrice) -2*connectorPrice;

                if (price > bestPrice)
                {
                    bestPrice = price;
                }
            }

            bestPrices[length] = bestPrice;


            return bestPrice;

        }
    }
}