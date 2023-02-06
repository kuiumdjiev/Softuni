using System;
using System.Linq;

namespace Bubble_Sort
{
    public class Program
    {
        static void Main(string[] args)
        {
            var array = Console.ReadLine().Split(" ").Select(x => int.Parse(x)).ToArray();
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 1; j < array.Length-i; j++)
                {
                    if (array[j - 1] > array[j])
                    {
                        Swap(array, j, j-1);

                    }
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
