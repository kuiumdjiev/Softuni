using System;
using System.Linq;

namespace Variations_without_Repetitions
{
    public class Program
    {
        public static string[] variations;
        public static string[] elements;
        public static bool[] used;
        public static int count;
        static void Main(string[] args)
        {
            elements = Console.ReadLine().Split(' ').ToArray();
            count = int.Parse(Console.ReadLine());

            variations = new string[count];
            used = new bool[elements.Length];
            Variations(0);
        }

        private static void Variations(int index)
        {
            if (index >= count)
            {
                Console.WriteLine(string.Join(" ", variations));
                return;
            }

            for (int i = 0; i < elements.Length; i++)
            {
                if (!used[i])
                {
                    used[i] = true;
                    variations[index] = elements[i];
                    Variations(index + 1);
                    used[i] = false;
                }

            }
        }
    }
}
