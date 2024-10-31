﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Kruskal_s_Algorithm
{

    public class Edge
    {
        public int First { get; set; }

        public int Second { get; set; }

        public int Weight { get; set; }
    }

    public class Program
    {
        private static List<Edge> forest;
        private static List<Edge> edges ;
        private static int[] parent;

        static void Main()
        {
            forest= new List<Edge>();
            edges = new List<Edge>();

            var count = int.Parse(Console.ReadLine());
            var maxNode = -1;

            for (int i = 0; i < count; i++)
            {
                var edgeData = Console.ReadLine()
                    .Split(", ")
                    .Select(int.Parse)
                    .ToArray();

                var firstNode = edgeData[0];
                var secondNode = edgeData[1];
                
                if (firstNode > maxNode)
                {
                    maxNode = firstNode;
                }

                if (secondNode > maxNode)
                {
                    maxNode = secondNode;
                }

                edges.Add(new Edge
                {
                    First = edgeData[0],
                    Second = edgeData[1],
                    Weight = edgeData[2]
                });
            }

            parent = new int[maxNode + 1];
                for (int j = 0; j < parent.Length; j++)
                {
                    parent[j] = j;
                }
                var sortedEdges = edges
                    .OrderBy(e => e.Weight)
                    .ToArray();

                foreach (var edge in sortedEdges)
                {
                    var firstNodeRoot = FindRoot(edge.First);
                    var secondNodeRoot = FindRoot(edge.Second);
                    if (firstNodeRoot==secondNodeRoot)
                    {
                        continue;
                    }

                    parent[firstNodeRoot] = secondNodeRoot;
                    forest.Add(edge);
                }

                foreach (var edge in forest)
                {
                    Console.WriteLine($"{edge.First} - {edge.Second}"); 
                }

            
        }

        private static int FindRoot(int node)
        {
            while (node != parent[node])
            {
                node = parent[node];
            }

            return node;
        }
    }
}
