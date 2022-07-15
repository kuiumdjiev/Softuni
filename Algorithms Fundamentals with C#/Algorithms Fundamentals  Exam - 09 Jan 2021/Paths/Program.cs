using System;
using System.Collections.Generic;
using System.Linq;

namespace Paths
{
    public class Program
    {
        private static Dictionary<int, List<int>> graph;
        private static HashSet<int> visited;

        static void Main()
        {
          graph= new Dictionary<int, List<int>>();
          visited= new HashSet<int>();
          int n = int.Parse(Console.ReadLine());

          for (int i = 0; i < n ; i++)
          {
                graph[i]= new List<int>();
          }

          for (int i = 0; i < n-1; i++)
          {
              var child = Console.ReadLine()
                  .Split(" ")
                  .Select(int.Parse)
                  .ToArray();

                graph[i].AddRange(child);
          }

          int destination = graph.Keys.Last();

          foreach (var node in graph.Keys)
          {
              if(destination==node)break;
              DFS(node ,destination);
          }
        }

        private static void DFS(int node, int destination)
        {
            if (node==destination)
            {
                    visited.Add(node);
                Console.WriteLine(String.Join(" ",visited));
                visited.Remove(node);

                return;
            }

            if (visited.Contains(node))
            {
                return;
            }
            visited.Add(node);
            foreach (var child in graph[node])
            {
                DFS(child,destination);
            }
            visited.Remove(node);
        }
    }
}
