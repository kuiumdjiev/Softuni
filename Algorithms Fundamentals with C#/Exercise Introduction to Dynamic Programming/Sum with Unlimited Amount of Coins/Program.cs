﻿
using System;
using System.Linq;

namespace Sum_with_Unlimited_Amount_of_Coins
{
    public class Program
    {
        static void Main(string[] args)
        {
            var  numbers = Console.ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();

            var target = int.Parse(Console.ReadLine());
            Console.WriteLine(CountSums(numbers , target));
        }

        private static int CountSums(int[] numbers, int target)
        {
            var sums = new int[target + 1];
            sums[0] = 1;
            foreach (var number in numbers)
            {
                for (int sum = number; sum <= target; sum++)
                {
                    sums[sum] += sums[sum - number];
                }
            }

            return sums[target];
        }
    }
}
