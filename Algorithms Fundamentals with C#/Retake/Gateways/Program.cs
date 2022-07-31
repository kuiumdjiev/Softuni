using System;
using System.Collections.Generic;
using System.Linq;

namespace Gateways
{
    public class Program
    {


        private static List<int>[] graph;
        private static bool[] used;
        private static int[] parent;

        static void Main()
        {
            var n = int.Parse(Console.ReadLine());
            var m= int.Parse(Console.ReadLine());
            graph = new List<int>[n + 1];
            used = new bool[graph.Length];
            parent = new int[graph.Length];
            Array.Fill(parent, -1);
            for (int i = 0; i < graph.Length; i++)
            {
                graph[i] = new List<int>();
            }
            for (int i = 0; i < m; i++)
            {
                var edge = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();
                var firstNode = edge[0];
                var secondNode = edge[1];

                graph[firstNode].Add(secondNode);

            }
            var s = int.Parse(Console.ReadLine());
            var t = int.Parse(Console.ReadLine());
            BFS(s, t);
        }

        private static void BFS(int startNode, int destination)
        {
            var queue = new Queue<int>();
            queue.Enqueue(startNode);

            used[startNode] = true;
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                if (node == destination)
                {
                    var path = GetPath(destination);
                    if (path.Count != 0)
                    {
                        Console.WriteLine(string.Join(" ", path));
                    }

                    break;
                }

                foreach (var child in graph[node])
                {
                    if (!used[child])
                    {
                        parent[child] = node;
                        used[child] = true;
                        queue.Enqueue(child);
                    }
                }
            }
        }

        private static Stack<int> GetPath(int destination)
        {
            var path = new Stack<int>();
            var node = destination;

            while (node != -1)
            {
                path.Push(node);
                node = parent[node];
            }
            return path;
        }
    }
}
