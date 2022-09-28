using System;
using System.Collections.Generic;
using System.Linq;

namespace Flow
{
    public class Program
    {
        static void Main(string[] args)
        {
            var m = int.Parse(Console.ReadLine());

            var nodesCount = int.Parse(Console.ReadLine());
            var graph = new int[m, m];
            var parents = new int[nodesCount];

            var data = Console.ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();
            //for (int i = 0; i < graph.Length; i++)
            //{
            //    graph[i] = new int[nodesCount];
            //}

            for (int i = 0; i < nodesCount; i++)
            {
                var elements = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();
                graph[elements[0], elements[1]] = elements[2];
            }

            var source = data[0];
            var destination = data[1];
            var maxFlow = 0;
            for (int i = 0; i < parents.Length; i++)
            {
                parents[i] = -1;
            }

            while (BFS(graph,parents,source,destination))
            {
                var minFlow = int.MaxValue;

                var s = source;
                var d = destination;

                while (s!=-1&& d!=-1)
                {
                    minFlow = Math.Min(minFlow, graph[s, d]);
                    s = parents[s];
                    d = parents[d];
                }

                maxFlow += minFlow;

                s = d;
                d = parents[s];

                while (s != -1 && d != -1)
                {
                    graph[s,d] -= minFlow;
                    s = parents[s];
                    d = parents[d];
                }

            }
        }

        private static bool BFS(int[,] graph, int[] parents, int source, int destination)
        {
            var visited = new bool[graph.GetLength(0)];
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(source);
            while (queue.Count>0)
            {
                var node = queue.Dequeue();

                for (int i = 0; i < graph.GetLength(0); i++)
                {
                    if (graph[node, i] >0 && !visited[i])
                    {
                        parents[i] = node;
                        visited[i] = true;
                        queue.Enqueue(i);
                    }
                }
            }

            return visited[destination];
        }
    }
}
