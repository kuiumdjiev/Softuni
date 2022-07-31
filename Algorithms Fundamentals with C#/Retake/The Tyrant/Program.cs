using System;

namespace The_Tyrant
{
    using System.Linq;

    public class Program
    {
        private static int[] dp;

        private static int[] input;
        static void Main()
        {
            input = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            dp = new int[input.Length];
            Console.WriteLine(FindSum());
        }

    private  static int FindSum( )
        {


            if (input.Length == 1)
            {
                return input[0];
            }


            if (input.Length == 2)
            {
                return Math.Min(input[0], input[1]);
            }


            if (input.Length == 3)
            {
                return Math.Min(input[0], Math.Min(input[1], input[2]));
            }

            if (input.Length == 4)
            {
                return Math.Min(Math.Min(input[0], input[1]), Math.Min(input[2], input[3]));
            }

            dp[0] = input[0];
            dp[1] = input[1];
            dp[2] = input[2];
            dp[3] = input[3];

            for (int i = 4; i < input.Length; i++)
            {
                dp[i] = input[i] + Math.Min(Math.Min(dp[i - 1], dp[i - 2]), Math.Min(dp[i - 3], dp[i - 4]));
            }

            return Math.Min(Math.Min(dp[input.Length - 1], dp[input.Length - 2]), Math.Min(dp[input.Length - 4], dp[input.Length - 3]));
        }

    }
}
