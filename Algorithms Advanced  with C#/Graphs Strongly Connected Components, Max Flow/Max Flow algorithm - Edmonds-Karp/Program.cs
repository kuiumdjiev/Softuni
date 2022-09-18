
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Linq;

namespace Max_Flow_algorithm___Edmonds_Karp
{
    public class Program
    {
        private static int[,] graph;
        private static int[] parent;
       public static void Main()
        {
            var nodes = int.Parse(Console.ReadLine());
             graph = new int[nodes, nodes];
             parent = new int[nodes];
             Array.Fill(parent,-1);

            for (int i = 0; i < nodes; i++)
            {
                var rows = Console.ReadLine()
                    .Split(", ")
                    .Select(int.Parse)
                    .ToArray();
                for (int j = 0; j < nodes; j++)
                {
                    graph[j,j] = rows[i];
                }
            }

            var source = int.Parse(Console.ReadLine());
            var target = int.Parse(Console.ReadLine());
            var maxFlow = 0;

            while (BFS(source,target))
            {
                var minFlow = GetMinFlow(target);
                ApplyFlow(target, minFlow);
                maxFlow += minFlow;
            }

            Console.WriteLine($"Max flow = {maxFlow}");
        }

        private static void ApplyFlow(int node, int minFlow)
        {
            while (parent[node] != -1)
            {
                var prev = parent[node];
                 graph[prev, node]-=minFlow;
                node = prev;
            }
        }

        private static int GetMinFlow(int node)
        {
            int minFlow = int.MaxValue;
            while (parent[node]!=-1)
            {
                var prev = parent[node];
                var flow = graph[prev, node];
                if (flow < minFlow)
                {
                    minFlow = flow;
                }
                node = prev;
            }

            return minFlow;
        }

        private static bool BFS( int source, int target)
        {
            var visited = new bool[graph.GetLength(0)];
            visited[source] = true;

            var queue = new Queue<int>();
            queue.Enqueue(source);

            while (queue.Count>0)
            {
                var node = queue.Dequeue();

                for (int child = 0; child < graph.GetLength(1); child++)
                {
                    if (!visited[child]&& graph[node,child]>0)
                    {
                        visited[child] = true;
                        queue.Enqueue(child);
                        parent[child] = node;
                    }
                }
            }

            return visited[target];
        }
    }
}
