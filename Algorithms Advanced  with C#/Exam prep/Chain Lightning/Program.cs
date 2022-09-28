using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace Chain_Lightning
{
    public class Edge
    {
        public int First { get; set; }

        public int Second { get; set; }

        public int Weight { get; set; }
    }

    public class Program
    {
        private static List<Edge>[] graph;
        private static int[] dmgs;
        private static int[] jumps;
        static void Main()
        {
            int nodes = int.Parse(Console.ReadLine());
            int edges = int.Parse(Console.ReadLine());
            int lightning  = int.Parse(Console.ReadLine());

            dmgs = new int[nodes];
            graph = new List<Edge>[nodes];
            jumps= new int[nodes];

            for (int i = 0; i < nodes; i++)
            {
                graph[i]= new List<Edge>();
            }

            for (int i = 0; i < edges; i++)
            {
                int[] data = Console.ReadLine()
                    .Split(' ')
                    .Select(int.Parse)
                    .ToArray();

                graph[data[0]].Add(new Edge
                {
                    First = data[0],
                    Second = data[1],
                    Weight = data[2]
                });

                graph[data[1]].Add(new Edge
                {
                    First = data[1],
                    Second = data[0],
                    Weight = data[2]
                });
            }


            for (int i = 0; i < lightning; i++)
            {
                var  data = Console.ReadLine()
                    .Split(" ")
                    .Select(int.Parse)
                    .ToArray();
                //jumps[data[0]] = 1;
                Prim(data[0], data[1]);
                jumps = new int[graph.Length];
            }

            Console.WriteLine(dmgs.Max());
        }

        private static void Prim(int startNode, int dmg)
        {
            var tree = new HashSet<int>();
            var bag = new OrderedBag<Edge>(
                Comparer<Edge>.Create((f, s) => f.Weight - s.Weight));
            bag.AddMany(graph[startNode]);
            tree.Add(startNode);
            dmgs[startNode] += dmg;
            while (bag.Count>0)
            {
                var minNode = bag.RemoveFirst();
                var nonTreeNode = -1;
                var treeNode = 1;

                if (tree.Contains(minNode.First)&&
                    !tree.Contains(minNode.Second))
                {
                    nonTreeNode = minNode.Second;
                    treeNode = minNode.First;
                }

                if (tree.Contains(minNode.Second) &&
                    !tree.Contains(minNode.First))
                {
                    nonTreeNode= minNode.First;
                    treeNode = minNode.Second;
                }

                if (nonTreeNode==-1)
                {
                    continue;
                }

                tree.Add(nonTreeNode);
                jumps[nonTreeNode] = jumps[treeNode] + 1;
                dmgs[nonTreeNode] += CalculateDmg(dmg, jumps[nonTreeNode]);
                bag.AddMany(graph[nonTreeNode]);
            }
        }

        private static int CalculateDmg(int dmg , int stack)
        {
           // if(stack==1) return dmg/2;

            for (int i = 0; i < stack; i++)
            {
                dmg /= 2;
            }

            return dmg;
        }

    }
}
