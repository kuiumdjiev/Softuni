using System;
using System.Linq;

namespace Variations_with_Repetition
{
    public class Program
    {
        public static string[] variations;
        public static string[] elements;
        public static int count;
        static void Main(string[] args)
        {
            elements = Console.ReadLine().Split(' ').ToArray();
            count = int.Parse(Console.ReadLine());

            variations = new string[count];
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
                
                    variations[index] = elements[i];
                    Variations(index + 1);
            }
        }
    }
}
