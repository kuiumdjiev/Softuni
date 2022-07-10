using System;
using System.Linq;

namespace Move_Down_Right
{
    public class Program
    {
        private static int[,] matrix;
        static void Main(string[] args)
        {
            var row = int.Parse(Console.ReadLine());
            var col = int.Parse(Console.ReadLine());

            matrix = new int[row, col];
            for (int r = 0; r < row; r++)
            {
                var line = Console.ReadLine().Split().Select(int.Parse).ToArray();
                for (int c = 0; c < col; c++)
                {
                    matrix[r, c] = line[c];
                } 
            }

            Console.WriteLine(Slove(0,0));
        }
        static int Slove(int  r ,int c)
        {
            if (r<0 || r >= matrix.GetLength(0)-1|| c <0 || c>= matrix.GetLength(1)-1)
            {
                return matrix[r,c];
            }

            if (matrix[r+1,c] > matrix[r ,c+1])
            {
                return Slove(r + 1, c);
            }
            else
            {
                return Slove(r , c+1);

            }
        }
    }
}
