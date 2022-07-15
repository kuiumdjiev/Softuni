using System;
using System.Threading;

namespace TBC
{
    public class Program
    {
        private static char[,] matrix;
        private static bool isT=false;
        static void Main()
        {
            var row = int.Parse(Console.ReadLine());
            var col = int.Parse(Console.ReadLine());
            int count=0;
            matrix = new char[row, col];
            for (int i = 0; i < row; i++)
            {
                var str = Console.ReadLine();
                for (int j = 0; j < col; j++)
                {
                    matrix[i, j] = str[j];
                }
            }

            for (int r = 0; r < row; r++)
            {
                for (int c = 0; c < col; c++)
                {

                    Slove(r,c);
                    if (isT == true)
                    {
                        count++;
                        isT = false;
                    }
                }
            }

            Console.WriteLine(count
            );
        }

        private static void Slove(int r, int c)
        {
            if (r<0 || r>= matrix.GetLength(0)|| c<0|| c>= matrix.GetLength(1))
            {
                return;
            }

            if (matrix[r,c]=='d'|| matrix[r,c]=='v')
            {
                return;
            }

            matrix[r, c] = 'v';
            isT=true;

            Slove(r + 1, c);
            Slove(r - 1, c);
            Slove(r + 1, c + 1);
            Slove(r + 1, c - 1);
            Slove(r, c + 1);
            Slove(r - 1, c + 1);

        }
    }
}
