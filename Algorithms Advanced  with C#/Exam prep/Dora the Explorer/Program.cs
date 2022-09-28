using System;
using System.Collections.Generic;
using System.Linq;

namespace Dora_the_Explorer
{
    public class Edge
    {
        public int First { get; set; }

        public int Second { get; set; }

        public int Weight { get; set; }
    }

    public class Program
    {
        private static List<Edge> graph;

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

          

            graph = new List<Edge>();


            for (int i = 0; i < n; i++)
            {
                var data = Console.ReadLine()
                    .Split(", ")
                    .Select(int.Parse)
                    .ToArray();
                graph.Add( new Edge
                {
                    First = data[0],
                    Second = data[1],
                    Weight = data[2]
                });

                graph.Add(new Edge
                {
                    First = data[1],
                    Second = data[0],
                    Weight = data[2]
                });
            }

            int e = graph.Max(x => x.First)+1;

            var prev = new int[e];
            var distance = new double[e];

            Array.Fill(prev, -1);
            Array.Fill(distance, double.PositiveInfinity);

            int x = int.Parse(Console.ReadLine());

            int start = int.Parse(Console.ReadLine());
            int end = int.Parse(Console.ReadLine());

            distance[start] = 0;

            for (int i = 0; i < n - 1; i++)
            {
                var check = false;
                foreach (var edge in graph)
                {
                    if ( double.IsPositiveInfinity(distance[edge.First]))
                    {
                        continue;
                    }

                    var newDistance = edge.Weight + distance[edge.First] + x;
                    if (newDistance < distance[edge.Second])
                    {
                        distance[edge.Second] = newDistance;
                        check = true;
                    }
                }

                if (check==false)
                {
                    break;
                }
            }

            Console.WriteLine(distance[end]);
        }
    }
}
