using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Cycles_in_a_Graph
{
    public class Program
    {
        private static Dictionary<string, List<string>> graph;
        private static HashSet<string> visited;
        private static HashSet<string> cycles;

        static void Main(string[] args)
        {
            graph =new Dictionary<string, List<string>>();
            visited = new HashSet<string>();
            cycles = new HashSet<string>();
            while (true)
            {
                var line = Console.ReadLine();
                if (line == "End")
                {
                    break;
                }

                var input = line.Split("-").ToArray();
                var a = input[0];
                var b =  input[1];
                if (!graph.ContainsKey(a))
                {
                    graph.Add(a, new List<string>());
                }

                if (!graph.ContainsKey(b))
                {
                    graph.Add(b, new List<string>());
                }
                graph[a].Add(b);
            }

            try
            {
                foreach (var node in graph.Keys)
                {
                    DFS(node);
                }

                Console.WriteLine("Acyclic: Yes");
            }
            catch(InvalidDataException)
            {
                Console.WriteLine("Acyclic: No");
            }
            
        }

        private static void DFS(string node)
        {
            if (cycles.Contains(node))
            {
                throw new InvalidDataException();
            }

            if(visited.Contains(node))
            {
                return;
            }

            cycles.Add(node);
            visited.Add(node);
            foreach (var child in graph[node])
            {
                DFS(child);
            }

            cycles.Remove(node);
        }
    }
}
