using System;
using System.Collections.Generic;
using System.Linq;

namespace Path_Finder
{
    public class Program
    {
        private static List<int>[] graph;
        static void Main()
        {
            var n = int.Parse(Console.ReadLine());
            graph = new List<int>[n];
            for (int i = 0; i < n; i++)
            {
                graph[i] = new List<int>();
            }

            for (int i = 0; i < n; i++)
            {
                var  input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    var nums = input
                        .Split(' ')
                        .Select(int.Parse)
                        .ToList();
                    graph[i] = nums;
                }
            }

            var p = int.Parse(Console.ReadLine());
            for (int i = 0; i < p; i++)
            {
                var path = Console.ReadLine()
                    .Split(' ')
                    .Select(int.Parse)
                    .ToArray();
              if(  PathFinder(path , 0)) Console.WriteLine("yes");
              else Console.WriteLine("no");
            }
        }

        private static bool PathFinder(int[] path, int index)
        {
            if (path.Length-1==index)
            {
                return true;
            }

            if (graph[path[index]].Contains(path[index+1])&& PathFinder(path, index + 1))
            {
                return true ;
            }
            
                return false;
            
        }
    }
}
