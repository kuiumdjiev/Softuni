using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Undefined
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

            while (BFS(graph, parents, source,destination))
            {
                var minFlow = int.MaxValue;
                var to = destination;
                var from = parents[to];

                while (to!=-1&& from !=-1)
                {
                    minFlow = Math.Min(minFlow, graph[from,to]);

                    to = parents[to];
                    from = parents[to];
                }

                maxFlow += minFlow;
                to = destination;
                from = parents[from];
                while (to!=-1&&from!=1)
                {
                    graph[from,to] -= minFlow;
                    to = parents[to];
                    from = parents[from];
                }
            }
        }

        private static bool BFS(int[,] graph, int[] parents, int source, int destination)
        {
            var visited = new bool[graph.Length];
            var queue = new Queue<int>();

            visited[source] = true;
            queue.Enqueue(source);
            while (queue.Count>0)
            {
                var node = queue.Dequeue();
                for (int i = 0; i < graph.GetLength(0); i++)
                {
                    if (!visited[i] && graph[node,i]>0)
                    {
                        queue.Enqueue(i);
                        visited[i] = true;
                        parents[i] = node;
                    }
                }
            }
        }
    }
}
