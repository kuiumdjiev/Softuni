using System;
using System.Collections.Generic;
using System.Linq;

namespace Numbers
{
    public class Program
    {

        private static List<long> nums;
        private static List<string> output;
        private static Dictionary<string , string> dp;


        static void Main()
        {
            var n = int.Parse(Console.ReadLine());
            nums = new List<long>();
            output = new List<string>();
            dp = new Dictionary<string, string>();

            Solution(1, n);

            if (output.Count>1)
            {
                foreach (var line in output.OrderByDescending(x => x).Select(x => x.Split(" "))
                             .OrderByDescending(x => int.Parse(x[0])).Select(x => string.Join(" + ", x)).ToHashSet())
                {
                    Console.WriteLine(line);
                }

            }
        }
        

        private static void Solution(int index, int n)
        {


            if (n == 0)
            {

                var str = string.Join(" ", nums.OrderByDescending(x => x));
                output.Add(str);
            }

            for (int j = index; j <= n; j++)
            {
                nums.Add(j);
                Solution(j, n - j);
                nums.RemoveAt(nums.Count - 1);
            }
        }
    }
}