using System;
using System.Collections.Generic;
using System.Linq;

namespace DP
{
    public class Medivac
    {
        public int Unit { get; set; }

        public int Capacity { get; set; }

        public int UrgencyRating { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var maxCapacity = int.Parse(Console.ReadLine());

            var medivacs = new List<Medivac>();

            while (true)
            {
                var line = Console.ReadLine();

                if (line == "Launch")
                {
                    break;
                }

                var medivacParts = line.Split()
                    .Select(int.Parse).ToArray();

                medivacs.Add(new Medivac
                {
                    Unit = medivacParts[0],
                    Capacity = medivacParts[1],
                    UrgencyRating = medivacParts[2],
                });
            }

            var dp = new int[medivacs.Count + 1, maxCapacity + 1];
            var used = new bool[medivacs.Count + 1, maxCapacity + 1];
            for (int i = 1; i < dp.GetLength(0); i++)
            {
                var medivacsIndex = i - 1;
                var medivac = medivacs[medivacsIndex];
                for (int j = 1; j < dp.GetLength(1); j++)
                {
                    var excluding = dp[medivacsIndex, j];

                    if (excluding + medivac.Capacity> maxCapacity)
                    {
                        dp[i, j] = excluding; 
                        continue;
                    }

                    var including = medivac.Capacity + dp[medivacsIndex, j-medivac.Capacity];

                    if (including>excluding)
                    {
                        dp[i, j] = including;
                        used[i, j] = true;
                    }
                    else
                    {
                        dp[i, j] = excluding;
                    }
                }
            }

            var currentCapacity = maxCapacity;

            var totalCapacity = 0;

            var usedMedavics = new SortedSet<int>();

            for (int i = dp.GetLength(0)-1; i >=0 ; i--)
            {
                if (!used[i,currentCapacity])
                {
                    continue;
                }

                var med = medivacs[i-1];
                currentCapacity -= med.Capacity;
                usedMedavics.Add(med.Capacity);
                totalCapacity += med.Capacity;

                if (currentCapacity==0)
                {
                    break;
                }
            }
        }
    }
}
