using System;
using System.Collections.Generic;

namespace Salaries
{
    public class Program
    {
        private  static List<int>[] graph;
        private static Dictionary<int ,int> visited;

        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            graph = new List<int>[n];
            visited= new Dictionary<int ,int>();
            for (int node = 0; node < graph.Length; node++)
            {
                graph[node] = new List<int>();
            }

            for (int node = 0; node < graph.Length; node++)
            {
                var input = Console.ReadLine();
                for (int child = 0; child < input.Length; child++)
                {
                    if (input[child]=='Y')
                    {
                        graph[node].Add(child);
                    }
                }
            }
            var  sum=0;
            for (int node = 0; node < graph.Length; node++)
            {
           sum+=     DFS(node);
            }

            Console.WriteLine(sum);
            
        }

        private static int DFS(int node)
        {
            if (visited.ContainsKey(node))
            {
                return visited[node];
            }

            int sum = 0;

            if (graph[node].Count == 0)
            {
                sum = 1;
            }
            else
            {
                foreach (var child in graph[node])
                {   
                    sum += DFS(child);
                }

            }

                 visited[node]=sum;
                return sum;
            
        }
    }
}
