using System;
using System.Linq;

namespace Insertion_Sort
{
    public class Program
    {
        static void Main(string[] args)
        {
            var array = Console.ReadLine().Split(" ").Select(x => int.Parse(x)).ToArray();
            for (int i = 1; i < array.Length; i++)
            {
                var j = i;
                while (j>0&& array[j] < array[j-1])
                {
                    Swap(array, j, j - 1);
                    j--;
                }
            }

            Console.WriteLine(string.Join(" ", array));
        }

        private static void Swap(int[] array, int i, int min)
        {
            var temp = array[i];
            array[i] = array[min];
            array[min] = temp;
        }

    }
}
