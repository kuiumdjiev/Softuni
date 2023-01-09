using System;
using System.Collections.Generic;
using System.Linq;

namespace Connected_Areas_in_a_Matrix
{
    public class Area
    {
        public int Row { get; set; }

        public int Col { get; set; }


        public int Size { get; set; }
    }

    public class Program
    {
        public static char[,] matrix;

        public static  int size;

        static void Main(string[] args)
        {
            int row = int.Parse(Console.ReadLine());
            int col = int.Parse(Console.ReadLine());
            matrix = new char[row, col];
            for (int r = 0; r < row; r++)
            {
                var colElements = Console.ReadLine();
                for (int c = 0; c < col; c++)
                {
                    matrix[r, c] = colElements[c];
                }
            }

            var areas = new HashSet<Area>();

            for (int r = 0; r < row; r++)
            {
                for (int c = 0; c < col; c++)
                {
                    size = 0;
                    ExploreArea(r ,c);
                    if (size!=0)
                    {
                        areas.Add(new Area {Row = r, Col = c, Size = size});
                    }
                }
            }

            var sorted = areas
                .OrderByDescending(a => a.Size)
                .ThenBy(a => a.Row)
                .ThenBy(a => a.Col)
                .ToList();
            Console.WriteLine($"Total areas found: {sorted.Count()}");

            for (int i = 0; i < sorted.Count(); i++)
            {
                var area = sorted[i] ;
                Console.WriteLine($"Area #{i+1} at ({area.Row}, {area.Col}), size: {area.Size}");
            }
        }

        private static void ExploreArea(int  r ,int c)
        {
            if (IsOutside(r, c) || IsWall(r ,c)||IsVisited(r ,c))
            {
                return;
            }

            size += 1;
            matrix[r, c] = 'v';
            ExploreArea(r-1 ,c);
            ExploreArea(r + 1, c);
            ExploreArea(r , c-1);
            ExploreArea(r, c+1);

        }

        private static bool IsVisited(int r, int c)
        {
            return matrix[r, c]=='v';
        }

        private static bool IsOutside(int r, int c)
        {
           return  r<0|| c<0|| r>=matrix.GetLength(0)|| c>= matrix.GetLength(1);
        }

        private static bool IsWall(int r, int c)
        {
            return matrix[r,c] == '*';
        }
    }
}
