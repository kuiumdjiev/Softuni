using System;
using System.Linq;

namespace _3._Maximal_Sum
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] input = Console.ReadLine().Split(" ").Select(x => int.Parse(x)).ToArray();
            int r = input[0];
            int c = input[1];
            int[,] matrix = new int[r, c];
            int superSum = 0;
            for (int row = 0; row < r; row++)
            {
                int[] @char = Console.ReadLine().Split(" ").Select(x => int.Parse(x)).ToArray();
                for (int col = 0; col < c; col++)
                {

                    matrix[row, col] = @char[col];
                }
            }
            for (int row = 0; row < matrix.GetLength(0)-2; row++)
            {
                for (int col = 0; col < matrix.GetLength(1)-2; col++)
                {
                    int sum = 0;
                    sum += matrix[row, col];
                    sum += matrix[row+1, col];
                    sum += matrix[row+2, col];
                    sum += matrix[row, col+1];
                    sum += matrix[row+1, col+1];
                    sum += matrix[row+2, col+1];
                    sum += matrix[row, col+2];
                    sum += matrix[row+1, col+2];
                    sum += matrix[row+2, col+2];

                    if (sum>superSum)
                    {
                        superSum = sum;
                    }


                }
            }
            Console.WriteLine(superSum);
        }
    }
}
