using System;
using System.Collections.Generic;

namespace Fibonacci
{
    public class Program
    {
        private static Dictionary<int,long>  dp ;
        static void Main(string[] args)
        {
            dp = new Dictionary<int,long>();
            var n =int.Parse( Console.ReadLine());
            Console.WriteLine(Fib(n));
        }

        private static long Fib(int n)
        {
            if (n<2)
            {
                return n;
            }
            if (dp.ContainsKey(n))
            {
                return dp[n];
            }

            var result = Fib(n - 1) + Fib(n - 2);
            dp[n] = result;
            return  result;
        }
    }
}
