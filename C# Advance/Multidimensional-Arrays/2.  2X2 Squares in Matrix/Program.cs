using System;
using System.Linq;

namespace _2.__2X2_Squares_in_Matrix
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] input = Console.ReadLine().Split(" ").Select(x => int.Parse(x)).ToArray();
            int r =input[0];
            int c = input[1];
            string[,] matrix = new string[r, c];
            int count = 0;
            for (int row = 0;row < r; row++)
            {
                string[] @char = Console.ReadLine().Split(" ").ToArray();
                for (int col = 0; col <c; col++)
                {
                    
                    matrix[row, col] = @char[col];
                }
            }
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    if (col==c-1|| row==r-1)
                    {

                    }
                    else
                    {
                        string center = matrix[row, col];
                        string rightUp = matrix[row, col + 1];
                        string downCenter = matrix[row + 1, col];
                        string rightDown = matrix[row + 1, col + 1];
                        if ( downCenter==center&&center==rightDown&&rightDown==rightUp)
                        {
                            count++;
                        }                     
                    }
                  
                       
                }
            }
            Console.WriteLine(count);
        }
    }
}
