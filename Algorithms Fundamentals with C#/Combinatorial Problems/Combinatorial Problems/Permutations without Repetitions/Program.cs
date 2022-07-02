using System;
using System.Linq;

namespace Permutations_without_Repetitions
{
    public class Program
    {
        public static string[] permutations;
        public static string[] elements;
        public static bool[] used;

        static void Main(string[] args)
        {
            elements = Console.ReadLine().Split(' ').ToArray();
            permutations = new string[elements.Length];
         used = new bool[elements.Length];
            Permute(0);
        }

        private static void Permute(int index)
        {
            if (index >= permutations.Length)
            {
                Console.WriteLine(string.Join(" ", permutations));
                return;
            }
            
            for (int i = 0; i < elements.Length; i++)
            {
                if (!used[i])
                {
                    used[i] = true;
                    permutations[index] = elements[i];
                    Permute(index + 1);
                    used[i] = false;
                }
              
            }
        }
    }
}
