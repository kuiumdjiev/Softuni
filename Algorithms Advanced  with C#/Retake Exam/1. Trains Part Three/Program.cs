using System;
using System.Collections.Generic;
using System.Linq;


namespace _1._Trains_Part_Three
{
    public class Program
    {

        static void Main()
        {
            var m = int.Parse(Console.ReadLine());

            var nodesCount = int.Parse(Console.ReadLine());
            var graph = new int[m, m];
            var parents = new int[m];

            var data = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();


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
            parents[source] = -1;
         


            while (BFS(graph, parents, source, destination, m))
            {
                var minFlow = int.MaxValue;
                var to = destination;
                var from = parents[to];

                while (to != -1 && from != -1)
                {
                    minFlow = minFlow> graph[from, to]?graph[from, to]: minFlow;

                    to = parents[to];
                    from = parents[to];
                }

                maxFlow += minFlow;
                to = destination;
                from = parents[to];
                while (to != -1 && from != -1)
                {
                    graph[from, to] -= minFlow;
                    to = parents[to];
                    from = parents[to];
                }
            }

            BFS(graph, parents, source, destination, m);
            Console.WriteLine(maxFlow);
        }

        private static bool BFS(int[,] graph, int[] parents, int source, int destination, int m)
        {
            var visited = new bool[graph.Length];
            var queue = new HashSet<int>();

            visited[source] = true;
            queue.Add(source);

            while (queue.Count > 0)
            {
                var node = queue.First();
                    queue.Remove(queue.First());
                for (int child = 0; child < m; child++)
                {
                    if (!visited[child] && graph[node, child] > 0)
                    {
                        queue.Add(child);
                        visited[child] = true;
                        parents[child] = node;
                    }
                }
            }

            return visited[destination];
        }
    }
}