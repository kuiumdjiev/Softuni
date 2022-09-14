using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace Cable_Network
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
            var buget = int.Parse(Console.ReadLine());
            var nodes = int.Parse(Console.ReadLine());
            var edges = int.Parse(Console.ReadLine());

            var graph = new List<Edge>[nodes];
            var spannigTree = new HashSet<int>();

            for (int i = 0; i < graph.Length; i++)
            {
                graph[i] = new List<Edge>();
            }

            for (int i = 0; i < edges; i++)
            {
                var data = Console.ReadLine()
                    .Split()
                
                    .ToArray();

                var edge = new Edge
                {
                    First =int.Parse( data[0]),
                    Second = int.Parse(data[1]),
                    Weight= int.Parse( data[2])
                };
                graph[edge.First].Add(edge);
                graph[edge.Second].Add(edge);

                if (data.Length==4)
                {
                    spannigTree.Add(edge.First);
                    spannigTree.Add(edge.Second);
                }
            }

       var      usedBuget=  Prim(graph ,spannigTree, buget);

       Console.WriteLine($"Budget used: {usedBuget}");
        }

        private static int Prim(List<Edge>[] graph, HashSet<int> spannigTree, int buget)
        {
            var bag = new OrderedBag<Edge>(
                Comparer<Edge>.Create((f, s) => f.Weight.CompareTo(f.Weight)));
            var usedBuget = 0;

                foreach (var node in spannigTree)
            {
                bag.AddMany(graph[node]);
            }

                while (bag.Count>0)
                {
                    var minEdge = bag.RemoveFirst();

                    var nonTreeNode = -1;

                    if (spannigTree.Contains(minEdge.First)&&
                        !spannigTree.Contains(minEdge.Second))
                    {
                        nonTreeNode = minEdge.Second;
                    }

                    if (spannigTree.Contains(minEdge.Second) &&
                        !spannigTree.Contains(minEdge.First))
                    {
                        nonTreeNode = minEdge.First;
                    }

                    if (nonTreeNode == -1)
                    {
                        continue;
                    }

                    if (usedBuget+ minEdge.Weight>buget)
                    {
                    break;
                    }

                    spannigTree.Add(nonTreeNode);
                    usedBuget += minEdge.Weight;
                bag.AddMany(graph[nonTreeNode]);
            }

                return usedBuget;
        }
    }
}
