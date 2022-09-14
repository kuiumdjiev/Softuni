using System;
using System.Collections.Generic;
using System.Linq;

namespace Cheap_Town_Tour
{
    public class Edge
    {
        public int First { get; set; }

        public int Second { get; set; }

        public int Weight { get; set; }
    }
    public class Program
    {

        static void Main()
        {
            var nodes = int.Parse(Console.ReadLine());
            var edges = int.Parse(Console.ReadLine());

         var   graph = new List<Edge>();

         for (int i = 0; i < edges; i++)
         {
             var data = Console.ReadLine()
                 .Split(" - ")
                 .Select(int.Parse)
                 .ToArray();

                graph.Add(new  Edge
                {
                    First = data[0],
                    Second = data[1],
                    Weight = data[2]
                });

         }

         var parent = new int[nodes];
         for (int i = 0; i < nodes; i++)
         {
             parent[i] = i;
         }

         var totalCost = 0;

         foreach (var edge in graph.OrderBy(x=>x.Weight))
         {
             var firstNodeRoot = FindRoot(edge.First,parent);
             var secondNodeRoot = FindRoot(edge.Second,parent);
             if (firstNodeRoot== secondNodeRoot)
             {
                    continue;
             }

             parent[firstNodeRoot] = secondNodeRoot;
             totalCost+=edge.Weight;
         }

         Console.WriteLine($"Total cost: {totalCost}");
        }

        private static int FindRoot(int node , int[] parent)
        {
            while (node != parent[node])
            {
                node = parent[node];
            }

            return node;
        }
    }
}
