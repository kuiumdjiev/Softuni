using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Strongly_Connected_Components__SCC_
{
    public class Program
    {
        static void Main()
        {
            var nodes = int.Parse(Console.ReadLine());
            var lines = int.Parse(Console.ReadLine());

            var graph = new List<int>[nodes];
            var revarseGraph = new List<int>[nodes];


            for (int i = 0; i < graph.Length; i++)
            {
                graph[i] = new List<int>();
                revarseGraph[i] = new List<int>();
            }

            for (int i = 0; i < lines; i++)
            {
                var line = Console.ReadLine()
                    .Split(", ")
                    .Select(int.Parse)
                    .ToArray();

                var node = line[0];

                for (int j = 1; j < line.Length; j++)
                {
                    var child = line[j];

                    graph[node].Add(child);
                    revarseGraph[child].Add(node);

                }

            }

            var visited = new bool[graph.Length];
            var sorted = new Stack<int>();


            for (int node = 0; node < graph.Length; node++)
            {
                DFS(graph, node, visited, sorted);
            }

          visited = new bool[graph.Length];
          Console.WriteLine("Strongly Connected Components:");
            while (sorted.Count > 0)
            {
                var node = sorted.Pop();
                var commponent = new Stack<int>();
                if (visited[node])
                {
                    continue;
                }

                DFS(revarseGraph, node, visited, commponent);
                Console.Write("{");
                Console.Write(string.Join(", ", commponent));
                Console.WriteLine("}");
            }
        }

     private   static void DFS(List<int>[] graph, int node, bool[] visited, Stack<int> sorted)
            {
                if (visited[node])
                {
                    return;
                }

                visited[node] = true;
                foreach (var child in graph[node])
                {
                    DFS(graph, child, visited, sorted);
                }

                sorted.Push(node);
            }
        }
    }

