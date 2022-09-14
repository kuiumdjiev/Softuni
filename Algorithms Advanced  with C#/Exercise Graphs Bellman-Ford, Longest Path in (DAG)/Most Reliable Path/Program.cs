
using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace Most_Reliable_Path
{

    public class Edge
    {
        public int First { get; set; }

        public int Second { get; set; }

        public int Weight { get; set; }
    }

    public class Program
    {
        private static List< Edge>[] graph;
        static void Main()
        {
            var nodes = int.Parse(Console.ReadLine());
            var edges = int.Parse(Console.ReadLine());

            graph = new List<Edge>[nodes];
            for (int i = 0; i < nodes; i++)
            {
                graph[i] = new List<Edge>();
            }


            for (int i = 0; i < edges; i++)
            {
                var data = Console.ReadLine()
                    .Split(' ')
                    .Select(int.Parse)
                    .ToArray();

                var first = data[0];
                var second = data[1];
                var weight = data[2];

                var edge = new Edge
                {
                    First = first,
                    Second = second,
                    Weight = weight
                };

                graph[first].Add(edge);
                graph[second].Add(edge);
            }

            var source = int.Parse(Console.ReadLine());
            var destination = int.Parse(Console.ReadLine());

            var reliability = new double[graph.Length];
            var prev = new int[graph.Length];

            for (int i = 0; i < graph.Length; i++)
            {
                reliability[i] = double.NegativeInfinity;
                prev[i] = -1;
            }

            reliability[source] = 100;

            var bag = new OrderedBag<int>(
                Comparer<int>.Create((f, s) => reliability[s].CompareTo(reliability[f])));

            bag.Add(source);
            while (bag.Count>0)
            {
                var node = bag.RemoveFirst();

                if (node == destination)
                {
                    break;
                }

                foreach (var edge in graph[node])
                {
                    var  child = edge.First==node?edge.Second:edge.First;

                    if (double.IsNegativeInfinity(reliability[child]))
                    {
                        bag.Add(child);
                    }

                    var newReliability = reliability[node] * edge.Weight / 100.0;

                    if (newReliability > reliability[child])
                    {
                        reliability[child] = newReliability;
                        prev[child] = node;

                        bag = new OrderedBag<int>(bag,
                            Comparer<int>.Create((f, s) => reliability[s].CompareTo(reliability[f])));

                    }
                }

             
            }
            Console.WriteLine($"Most reliable path reliability: {reliability[destination]:F2}%");

            var currentNode = destination;
            var path = new Stack<int>();
            while (currentNode != -1)
            {
                path.Push(currentNode);
                currentNode = prev[currentNode];
            }

            Console.WriteLine(string.Join(" -> ", path));
        }
    }
}
