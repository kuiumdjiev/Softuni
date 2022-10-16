using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace Reaper_Man
{
    public class Edge
    {
        public int First { get; set; }

        public int Second { get; set; }

        public int Weight { get; set; }
    }

    public class Program
    {
        private static Dictionary<int, List<Edge>> graph;
        private static double[] distance;
        private static int[] parent;
        static void Main()
        {
            graph = new Dictionary<int, List<Edge>>();
            var n = int.Parse(Console.ReadLine());
            var m = int.Parse(Console.ReadLine());

            var data = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            var start = data[0];
            var end = data[1];
            
            for (int i = 0; i < m; i++)
            {
                var input = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();

                var first = input[0];
                var second = input[1];

                var edge = new Edge()
                {
                    First = first,
                    Second = second,
                    Weight = input[2]
                };

                if (!graph.ContainsKey(first))
                {
                    graph.Add(first, new List<Edge>());
                }

                if (!graph.ContainsKey(second))
                {
                    graph.Add(second, new List<Edge>());
                }

                graph[first].Add(edge);
                graph[second].Add(edge);

            }

            var biggestNode = graph.Keys.Max();

            distance = new double[biggestNode + 1];

            for (int i = 0; i < distance.Length; i++)
            {
                distance[i] = double.PositiveInfinity;
            }

            parent = new int[biggestNode + 1];
            for (int i = 0; i < parent.Length; i++)
            {
                parent[i] = -1;
            }


           

            distance[start] = 0;

            var bag = new OrderedBag<int>(Comparer<int>.Create((f, s) => (int)(distance[f] - distance[s])));
            bag.Add(start);

            while (bag.Count > 0)
            {
                var midNode = bag.RemoveFirst();
                if (double.IsPositiveInfinity(midNode))
                {
                    break;
                }
                foreach (var edge in graph[midNode])
                {
                    var otherNode = edge.First == midNode ? edge.Second : edge.First;



                    if (double.IsPositiveInfinity(distance[otherNode]))
                    {
                        bag.Add(otherNode);
                    }

                    var newDistance = distance[midNode] + edge.Weight;

                    if (newDistance < distance[otherNode])
                    {
                        parent[otherNode] = midNode;
                        distance[otherNode] = Math.Min(distance[otherNode], newDistance);


                        bag = new OrderedBag<int>(bag,
                            Comparer<int>.Create((f, s) => (int)(distance[f] - distance[s])));
                    }

                }

            }

            
            var path =FindPath(end);
            Console.WriteLine(string.Join(" ", path));
            Console.WriteLine(distance[end]);
        }

        private static Stack<int> FindPath(int node)
        {
            var path = new Stack<int>();

            while (node != -1)
            {
                path.Push(node);
                node = parent[node];
            }
            return path;
        }
    }
}
