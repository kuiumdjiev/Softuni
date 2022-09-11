
using System;
using System.Collections.Generic;
using System.Linq;

namespace Longest_Path
{
    public class Edge
    {
        public int From { get; set; }

        public int To { get; set; }

        public int Weight { get; set; }
    }

    public class Program
    {
        private static Dictionary<int, List<Edge>> edgesByNode;

        static void Main()
        {
            var nodes = int.Parse(Console.ReadLine());
            var edges = int.Parse(Console.ReadLine());

            edgesByNode = new Dictionary<int, List<Edge>>();

            for (int i = 0; i < edges; i++)
            {
                var edgeData = Console.ReadLine()
                    .Split(" ")
                    .Select(int.Parse)
                    .ToArray();

                var from = edgeData[0];
                var to = edgeData[1];

                if (!edgesByNode.ContainsKey(from))
                {
                    edgesByNode.Add(from, new List<Edge>());
                }

                if (!edgesByNode.ContainsKey(to))
                {
                    edgesByNode.Add(to , new List<Edge>());
                }

                edgesByNode[from].Add(new Edge
                {
                    From = from,
                    To = to,
                    Weight = edgeData[2]
                });
            }

            var source = int.Parse(Console.ReadLine());
            var destination = int.Parse(Console.ReadLine());

            var distance = new double[nodes + 1];
            Array.Fill(distance, double.NegativeInfinity);

            distance[source] = 0;

            var sorted = TopicalSorting();

            while (sorted.Count>0)
            {
                var node = sorted.Pop();
                foreach (var edge in edgesByNode[node])
                {
                    var newDistance = distance[edge.From] + edge.Weight;

                    if (newDistance> distance[edge.To])
                    {
                        distance[edge.To] = newDistance;
                    }
                }
            }

            Console.WriteLine(distance[destination]);
        }

        private static Stack<int> TopicalSorting()
        {
            var result = new Stack<int>();
            var  visited= new HashSet<int>();

            foreach (var node in edgesByNode.Keys)
            {
                DFS(node, visited, result);
            }
            return result ;
        }

        private static void DFS(int node, HashSet<int> visited, Stack<int> result)
        {
            if (visited.Contains(node))
            {
                return;
            }

            visited.Add(node);

            foreach (var edge in edgesByNode[node])
            {
                DFS(edge.To,
                     visited, result);
            }
            result.Push(node);
        }
    }
}
