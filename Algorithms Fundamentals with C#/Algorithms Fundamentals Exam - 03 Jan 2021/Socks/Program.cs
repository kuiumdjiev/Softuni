using System;
using System.Linq;

namespace Socks
{
    public class Program
    {
        private static int[,] matrix;
        static void Main(string[] args)
        {
            var str1 = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();
            var str2 = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();
            matrix = new int[str1.Length+1, str2.Length+1];

            for (int r = 1; r < matrix.GetLength(0); r++)
            {
                for (int c = 1; c < matrix.GetLength(1); c++)
                {
                    if (str1[r - 1] == str2[c-1])
                    {
                        matrix[r,c] = matrix[r-1, c-1]+1;
                    }
                    else
                    {
                        matrix[r, c] = Math.Max(matrix[r - 1, c], matrix[r, c - 1]);
                    }
                }
            }

            Console.WriteLine(matrix[str1.Length, str2.Length]);
        }
    }
}
