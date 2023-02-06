using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Set_Cover
{
    public class Program
    {
        static void Main(string[] args)
        {
            var universe =Console.ReadLine().Split(", ").Select(x => int.Parse(x)).OrderByDescending(x => x).ToHashSet();
            var n = int.Parse(Console.ReadLine());
            var sets = new List<int[]>(); 
            for (int i = 0; i < n; i++)
            {
                var set = Console.ReadLine()
                    .Split(", ")
                    .Select(int.Parse)
                    .ToArray();
                sets.Add(set);
            }

            var selectedSets = new List<int[]>();
            while (universe.Count>0)
            {
                var set = sets.OrderByDescending(x => x.Count(e => universe.Contains(e)))
                    .FirstOrDefault();
                selectedSets.Add(set);
                sets.Remove(set);
                foreach (var element in set)
                {
                    universe.Remove(element);
                }
            }

            Console.WriteLine($"Sets to take ({selectedSets.Count}):");
            foreach (var selectedSet in selectedSets)
            {
                Console.WriteLine(string.Join(", ", selectedSet));
            }
        }
    }
}
