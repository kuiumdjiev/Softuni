using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace Le_Tour_de_Sofia
{
    public class Edge
    {
        public int From { get; set; }

        public int To { get; set; }

        public int Weight { get; set; }
    }

    public class Program
    {
        private static List<Edge>[] graph;
        private static int[] prev;
        private static double[] distance;

        static void Main()
        {
            var n = int.Parse(Console.ReadLine());
            var m = int.Parse(Console.ReadLine());
            var startNode = int.Parse(Console.ReadLine());
            graph = new List<Edge>[n];
            for (int i = 0; i < n; i++)
            {
                graph[i] = new List<Edge>();
            }

            for (int i = 0; i < m; i++)
            {
                var input = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();
                var edge = new Edge
                {
                    From = input[0],
                    To = input[1],
                    Weight = input[2]
                };


                graph[edge.From].Add(edge);
            }

            prev = new int[n];
            distance = new double[n ];

            for (int i = 0; i < prev.Length; i++)
            {
                prev[i] = -1;
            }
            for (int i = 0; i < distance.Length; i++)
            {
                distance[i] = double.PositiveInfinity;
            }
            
            //distance[s] = 0;
            var bag = new OrderedBag<int>(Comparer<int>.Create((f, s) => (int) (distance[f] - distance[s])));
            foreach (var edge in graph[startNode])
            {
                distance[edge.To] = edge.Weight;
                bag.Add(edge.To);
            }

            while (bag.Count > 0)
            {
                var midNode = bag.RemoveFirst();
                if (midNode == startNode)
                {
                    break;
                }

                foreach (var edge in graph[midNode])
                {
                    // var otherNode = edge.From == midNode ? edge.From : edge.To;
                    if (double.IsPositiveInfinity(distance[edge.To]))
                    {
                        bag.Add(edge.To);
                    }

                    var newDistance = distance[midNode] + edge.Weight;
                    if (newDistance < distance[edge.To])
                    {
                        distance[edge.To] = newDistance;
                        prev[edge.To] = midNode;

                        bag = new OrderedBag<int>(bag,
                            Comparer<int>.Create((f, s) => (int) (distance[f] - distance[s])));
                    }
                }
            }

            if (double.IsPositiveInfinity(distance[startNode]))
            {
                Console.WriteLine(distance.Count(x=>!double.IsPositiveInfinity(x))+1);
            }
            else
            {
                Console.WriteLine(distance[startNode]);

            }
        }
    }
}
