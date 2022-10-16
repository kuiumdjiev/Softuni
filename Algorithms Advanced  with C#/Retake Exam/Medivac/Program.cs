using System;
using System.Collections.Generic;
using System.Linq;

namespace Medivac
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

            for (int row = 1; row < dp.GetLength(0); row++)
            {
                var medavicIdx = row - 1;
                var medivac = medivacs[medavicIdx];

                for (int capacity = 1; capacity < dp.GetLength(1); capacity++)
                {
                    var excluding = dp[row - 1, capacity];

                    if (medivac.Capacity > capacity)
                    {
                        dp[row, capacity] = excluding;
                        continue;
                    }

                    var including = medivac.UrgencyRating + dp[row - 1, capacity - medivac.Capacity];

                    if (including > excluding)
                    {
                        dp[row, capacity] = including;
                        used[row, capacity] = true;
                    }
                    else
                    {
                        dp[row, capacity] = excluding;
                    }
                }
            }

            var currentCapacity = maxCapacity;

            var totalCapacity = 0;

            var usedMedavics = new SortedSet<int>();

            for (int row = dp.GetLength(0) - 1; row > 0; row--)
            {
                if (!used[row, currentCapacity])
                {
                    continue;
                }

                var medivac = medivacs[row - 1];
                totalCapacity += medivac.Capacity;
                currentCapacity -= medivac.Capacity;
                usedMedavics.Add(medivac.Unit);

                if (currentCapacity == 0)
                {
                    break;
                }
            }

            Console.WriteLine(totalCapacity);
            Console.WriteLine( dp[medivacs.Count, maxCapacity]);
            Console.WriteLine(String.Join(Environment.NewLine, usedMedavics));
        }
    }
}
