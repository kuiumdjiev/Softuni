using System;
using System.Collections.Generic;
using System.Dynamic;

namespace Binomial_Coefficients
{
    public class Program
    {
        private static Dictionary<string, long> dp;
        static void Main()
        {
            dp = new Dictionary<string, long>();

            int row = int.Parse(Console.ReadLine());
            int col = int.Parse(Console.ReadLine());

            Console.WriteLine(BinomialCoefficients(row,col));
        }

        private static long BinomialCoefficients(int row, int col)
        {
            if (col==0||row==col)
            {
                return 1;
            }

            var key = $"{row}-{col}";

            if (dp.ContainsKey(key))
            {
                return dp[key];
            }
            var  result= BinomialCoefficients(row - 1, col - 1) + BinomialCoefficients(row - 1, col);
            dp[key]=result;
            return result;
        }
    }
}
