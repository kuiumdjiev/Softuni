using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace Dijkstra_s_Algorithm
{
    public class Edge
    {
        public int First { get; set; }

        public int Second { get; set; }

        public int Weight { get; set; }
    }

    public class Program
    {
        private static Dictionary<int, List<Edge>> edgesByNodes;
        private static double[] distance;
        private static int[] parent;
        static void Main()
        {
            edgesByNodes = new Dictionary<int, List<Edge>>();
            var edgeCount = int.Parse(Console.ReadLine());

            for (int i = 0; i < edgeCount; i++)
            {
                var  edgeArgs= Console.ReadLine()
                    .Split(new []{", "}, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                var  firstNode= edgeArgs[0];
                var secondNode = edgeArgs[1];

                var edge = new Edge()
                {
                    First = edgeArgs[0],
                    Second = edgeArgs[1],
                    Weight = edgeArgs[2]
                };

                if (!edgesByNodes.ContainsKey(firstNode))
                {
                    edgesByNodes.Add(firstNode,new List<Edge>());
                }

                if (!edgesByNodes.ContainsKey(secondNode))
                {
                    edgesByNodes.Add(secondNode, new List<Edge>());
                }

                edgesByNodes[firstNode].Add(edge);
                edgesByNodes[secondNode].Add(edge);

            }

            var biggestNode = edgesByNodes.Keys.Max();

            distance = new double[biggestNode+1];
            for (int i = 0; i < distance.Length; i++)
            {
                distance[i] = double.PositiveInfinity;
            }

            parent = new int[biggestNode + 1];
            for (int i = 0; i < parent.Length; i++)
            {
                parent[i] = -1;
            }


            var startNode = int.Parse(Console.ReadLine());
            var endNode = int.Parse(Console.ReadLine());

            distance[startNode] = 0;

            var bag = new OrderedBag<int>(Comparer<int>.Create((f, s) => (int) (distance[f] - distance[s])));
            bag.Add(startNode);

            while (bag.Count>0)
            {
                var midNode = bag.RemoveFirst();
                if (double.IsPositiveInfinity(midNode))
                {
break;
                }
                foreach (var edge in edgesByNodes[midNode])
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
                            Comparer<int>.Create((f, s) => (int) (distance[f] - distance[s])));
                    }

                }

            }

            if (double.IsPositiveInfinity(distance[endNode]))
            {
                Console.WriteLine("There is no such path.");
            }
            else
            {

                Console.WriteLine(distance[endNode]);
                var currentNode = endNode;
                var path = new Stack<int>();
                while (currentNode != -1)
                {
                    path.Push(currentNode);
                    currentNode = parent[currentNode];
                }

                Console.WriteLine(string.Join(" ", path));
            }
        }
    }
}
