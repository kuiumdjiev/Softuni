using System;
using System.Linq;

namespace Super_Set
{

    public class Program
    {
        public static int[] combinations;
        public static int[] elements;


        static void Main()
        {
            elements = Console.ReadLine().Split(", ").Select(int.Parse).ToArray();
        
            for (int i = 0; i <= elements.Length; i++)
            {
                combinations = new int[i];
                CombinationsWithoutRepetition(0, 0, i);
            }
        }

        private static void CombinationsWithoutRepetition(int index, int elementsStartIndex ,int k)
        {
            if (index >= k)
            {
                Console.WriteLine(string.Join(" ", combinations));
                return;
            }

            for (int i = elementsStartIndex; i < elements.Length; i++)
            {

                combinations[index] = elements[i];
                CombinationsWithoutRepetition(index + 1, i + 1,k);

            }
        }
    }
}
