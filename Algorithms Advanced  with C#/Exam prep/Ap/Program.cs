using System;
using System.Collections.Generic;

namespace Ap
{
    public class Program
    {
        private static List<int>[] graph;
        private static int[] lowpoint;
        private static int[] deepPoint;
        private static bool[] isVsited;
        private static int[] parent;
        private static List<int> Ap;
        static void Main(string[] args)
        {

            int n = int.Parse(Console.ReadLine());
            Array.Fill(parent,-1);
            for (int i = 0 ; i < n;i++)
            {
                if (isVsited[i])
                {
                    continue;
                }

                FindArticulationPoints( i , 1);
            }
        }

        private static void FindArticulationPoints(int node, int d)
        {
            isVsited[node] = true;
            deepPoint[node] = d;
            lowpoint[node] = d;
            var isAP = false;
            var childCount = 0;


            foreach (var child in graph[node])
            {
                if (!isVsited[child])
                {
                    parent[child] = node;

                    FindArticulationPoints(child, d+1);

                    childCount++;

                    if (lowpoint[child] >= deepPoint[child])
                    {
                        isAP = true;
                    }
                    lowpoint[node] = Math.Min(lowpoint[node], lowpoint[child]);

                }
                else if (parent[child]!=node)
                {
                    lowpoint[node] = Math.Min(lowpoint[node], deepPoint[child]);
                }
            }

            if ((parent[node]==-1 &&childCount>1)
                || parent[node]!=-1 && isAP==true)
            {
                Ap.Add(node);
            }
        }
    }
}
