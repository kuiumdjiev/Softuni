using System;
using System.Collections.Generic;


namespace Big_Trip
{
    public class Edge
    {
        public int First { get; set; }

        public int Second { get; set; }

        public int Weight { get; set; }
    }


    public class Program
    {
        private static int[] distance;
        private static List<Edge>[] graph;
        static void Main(string[] args)
        {
            var nodes = int.Parse(Console.ReadLine());
            var edges = int.Parse(Console.ReadLine());

            distance = new int[nodes];

            var start = int.Parse(Console.ReadLine());
            var end = int.Parse(Console.ReadLine());

            Stack<int> topicalSorting = new Stack<int>();
            DFS(start, topicalSorting, new HashSet<int>());

            while (topicalSorting.Count>0)
            {
                var node = topicalSorting.Pop();
                foreach (var edge in graph[node])
                {
                    var newDistance = distance[edge.First] + edge.Weight;
                    if (newDistance> distance[edge.Second])
                    {
                        distance[edge.Second]= newDistance;
                    }
                }
            }
        }

        private static void DFS(int node, Stack<int> topicalSorting, HashSet<int> visited)
        {
            if (visited.Contains(node))
            {
                return;
            }
            visited.Add(node);

            foreach (var child in graph[node])
            {
                DFS(child.Second,topicalSorting, visited );
            }

            topicalSorting.Push(node);
        }
    }
}
