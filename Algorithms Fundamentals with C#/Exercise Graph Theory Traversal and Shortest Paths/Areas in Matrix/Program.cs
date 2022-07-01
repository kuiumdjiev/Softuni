using System;
using System.Collections.Generic;

namespace Areas_in_Matrix
{
    public class Program
    {
        private static char[,] graph;
        private static bool[,] used;
        private static SortedDictionary<char,int> areas;

        static void Main(string[] args)
        {
            var rows = int.Parse(Console.ReadLine());
            var cols = int.Parse(Console.ReadLine());
            var areasCount = 0;
            graph = new char[rows, cols];
            used = new bool[rows, cols];
            areas = new SortedDictionary<char,int>();
            for (int r = 0; r < rows; r++)
            {
                var line  = Console.ReadLine();
                for (int c = 0; c < cols; c++)
                {
                    graph[r,c] = line[c];
                }
            }
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (used[r, c])
                    {
                        continue;
                    }
                    var value = graph[r,c];
                    DFC(r, c, value);

                    if (areas.ContainsKey(graph[r,c]))
                    {
                        areas[value] += 1;
                    }
                    else
                    {
                        areas[value] = 1;
                    }
                    areasCount++;
                }
            }

            Console.WriteLine($"Areas: {areasCount}");
            foreach (var area in areas)
            {
                Console.WriteLine($"Letter '{area.Key}' -> {area.Value}");
            }
        }

        private static void DFC(int r, int c , char parentNode)
        {
            if (r<0 ||r>= graph.GetLength(0)|| c<0||c>=graph.GetLength(1))
            {
                return;
            }
            if (used[r, c])
            {
                return;
            }

            if (graph[r, c] != parentNode)
            {
                return;
            }

            used[r, c] = true;
            DFC(r, c-1, parentNode);
            DFC(r, c+1 , parentNode);
            DFC(r-1, c, parentNode);
            DFC(r+1, c , parentNode);

        }
    }
}
