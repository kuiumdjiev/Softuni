using System;
using System.Collections.Generic;
using System.Linq;

namespace Cheap_Town_Tour
{
    public class Edge
    {
        public int First { get; set; }

        public int Second { get; set; }

        public int Weight { get; set; }
    }

    public class Program
    {
        private static HashSet<Edge> graph;
        private static int[] prev;
        static void Main(string[] args)
        {
            var nodes = int.Parse(Console.ReadLine());
            var edges = int.Parse(Console.ReadLine());
            

            graph = new HashSet<Edge>();
            prev = new int[nodes];

            for (int i = 0; i < nodes; i++)
            {
                prev[i] = i;
            }

            for (int  i = 0 ;i< edges; i++)
            {

                var data = Console.ReadLine()
                    .Split(" - ")
                    .Select(int.Parse)
                    .ToArray();

                graph.Add(new Edge
                {
                    First = data[0],
                    Second = data[1],
                    Weight = data[2]
                });
            }
            

            foreach (var edge in graph.OrderBy(x=>x.Weight))
            {
                var first = FindRoot(edge.First);
                var second = FindRoot(edge.Second);

                if (first==second)
                {
                    continue;
                }

                prev[first] =second;
            }
        }

        private static int FindRoot(int node)
        {
            while (node!= prev[node])
            {
                node = prev[node];
            }

            return node;
        }
    }
}
