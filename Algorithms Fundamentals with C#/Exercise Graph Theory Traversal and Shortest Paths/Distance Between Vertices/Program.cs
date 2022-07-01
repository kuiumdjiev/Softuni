using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace Distance_Between_Vertices
{
    public class Program
    {
        private static List<int>[] graph;
        private static int[] parent;
        static void Main()
        {
            var n = int.Parse(Console.ReadLine());
            var e = int.Parse(Console.ReadLine());
            graph = new List<int>[n + 1];
            parent = new int[graph.Length];
            Array.Fill(parent, -1);

            for (int i = 0; i < graph.Length; i++)
            {
                graph[i] = new List<int>();
            }

            for (int i = 0; i < n; i++)
            {
                var line = Console.ReadLine()
                    .Split(":" )
                    .ToArray();

                var a =int.Parse( line[0]);
                var b = line[1];
                if (string.IsNullOrEmpty(b))
                {
                    graph[a] = new List<int>();
                }
                else
                {
                    graph[a] = b.Split(" ").Select(int.Parse).ToList();
                }
            }

            for (int i = 0; i < e; i++)
            {
                var line = Console.ReadLine()
                    .Split("-")
                    .Select(int.Parse)
                    .ToList();
                var start = line[0];
                var destination = line[1];
            BFS(start, destination);

            }
        }

        private static void BFS(int startNode, int destination)
        {
            var queue = new Queue<int>();
            queue.Enqueue(startNode);
            var visited= new HashSet<int> {startNode};
            var parent = new Dictionary<int, int> { {startNode,-1}};
        while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                if (node == destination)
                {
                    var path = GetPath(destination);
                    Console.WriteLine($"{{{startNode}, {destination}}} -> {path.Count-1}");
                }
                foreach (var child in graph[node])
                {
                    if (visited.Contains(child))
                    {
                    continue;
                    }

                    visited.Add(child);
                    parent[child] = node;
                    queue.Enqueue(child);
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
