using System;
using System.Linq;

namespace _4._Matrix_Shuffling
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] input = Console.ReadLine().Split(" ").Select(x => int.Parse(x)).ToArray();
            int r = input[0];
            int c = input[1];
            string[,] matrix = new string[r, c];
          
            for (int row = 0; row < r; row++)
            {
                string[] @char = Console.ReadLine().Split(" ").ToArray();
                for (int col = 0; col < c; col++)
                {

                    matrix[row, col] = @char[col];
                }
            }
            while (true)
            {
                string[] info = Console.ReadLine().Split(" ").ToArray();
                switch (info[0])
                {
                    case "swap":
                        if (info.Length == 5)
                        {


                            int firstRow = int.Parse(info[1]);
                            int firstCol = int.Parse(info[2]);
                            int secondRow = int.Parse(info[3]);
                            int secondCol = int.Parse(info[4]);
                            if (r >= firstRow && r >= secondRow && c >= firstCol && c >= secondCol)
                            {
                                string firstWord = matrix[firstRow, firstCol];
                                string secondWord = matrix[secondRow, secondCol];
                                matrix[firstRow, firstCol] = secondWord;
                                matrix[secondRow, secondCol] = firstWord;
                            }
                            else
                            {
                                Console.WriteLine("Invalid input!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input!");
                        }
                        break;
                    case "END":
                        for (int row = 0; row < r; row++)
                        {

                            for (int col = 0; col < c; col++)
                            {

                                Console.Write(matrix[row, col]);
                            }
                            Console.WriteLine();
                        }
                        return;
                    default:
                        Console.WriteLine("Invalid input!");
                        break;
                }
            }
            
           
           
        }

    }
}
