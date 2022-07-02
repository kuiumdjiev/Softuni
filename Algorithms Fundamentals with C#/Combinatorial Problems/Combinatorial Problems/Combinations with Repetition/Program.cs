using System;
using System.Linq;

namespace Combinations_with_Repetition
{
    public class Program
    {
        public static string[] combinations;
        public static string[] elements;
        public static int count;

        static void Main(string[] args)
        {
            elements = Console.ReadLine().Split(' ').ToArray();
            count = int.Parse(Console.ReadLine());
            combinations = new string[count];
            Combinations(0, 0);
        }

        private static void Combinations(int index, int elementsStartIndex)
        {
            if (index >= combinations.Length)
            {
                Console.WriteLine(string.Join(" ", combinations));
                return;
            }

            for (int i = elementsStartIndex; i < elements.Length; i++)
            {

                combinations[index] = elements[i];
                Combinations(index + 1, i );

            }
        }
    }
}
