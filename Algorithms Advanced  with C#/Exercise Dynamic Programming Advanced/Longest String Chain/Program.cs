using System;
using System.Collections.Generic;
using System.Linq;

namespace Longest_String_Chain
{
    public class Program
    {
        public static string[] strings;
        public static int[] lengths;
        public static int[] prev;
        static void Main()
        {
            strings = Console.ReadLine().Split().ToArray();
            lengths = new int[strings.Length];
            prev = new int[strings.Length];
            prev[0] = -1;
            lengths[0] = 1;
            int indexOfMaximumLength = -1, maxLen = 0;
            for (int i = 0; i < strings.Length; i++)
            {
                var bestLength = 1;
                var prevIndex = -1;
                var currentString = strings[i];
                for (int j = i - 1; j >= 0; j--)
                {
                    var previousString = strings[j];
                    if (previousString.Length < currentString.Length && bestLength <= lengths[j] + 1)
                    {
                        bestLength = lengths[j] + 1;
                        prevIndex = j;
                    }
                }

                lengths[i] = bestLength;
                prev[i] = prevIndex;
                if (bestLength > maxLen)
                {
                    maxLen = bestLength;
                    indexOfMaximumLength = i;
                }
            }
            List<string> chain = new List<string>();
            while (indexOfMaximumLength != -1)
            {
                chain.Add(strings[indexOfMaximumLength]);
                indexOfMaximumLength = prev[indexOfMaximumLength];
            }
            chain.Reverse();
            Console.WriteLine(string.Join(" ", chain));
        }
    }
}
