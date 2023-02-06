using System;
using System.Linq;

namespace Quicksort
{
    public class Program
    {
        static void Main(string[] args)
        {
           var  numbers =Console.ReadLine().Split(' ').Select(x=>int.Parse(x)).ToArray();
           Quicksort(numbers, 0 , numbers.Length-1);
           Console.WriteLine(string.Join(" ", numbers));
        }

        private static  void Quicksort(int[] numbers, int start, int end)
        {
            if (start >= end)
            {
                return;
            }
            var pivot = start;
            var left = start+1;
            var right = end;

            while (left<=right)
            {
                if (numbers[left]> numbers[pivot]&& numbers[right] < numbers[left])
                {
                    Swap(numbers, left, right);
                }

                if (numbers[left] <= numbers[pivot])
                {
                    left += 1;
                }

                if (numbers[right] >= numbers[pivot])
                {
                    right -= 1;
                }
            }
            Swap(numbers, pivot, right);

            Quicksort(numbers, start,right-1);
            Quicksort(numbers, right+1, end );

        }
        private static void Swap(int[] array, int i, int min)
        {
            var temp = array[i];
            array[i] = array[min];
            array[min] = temp;
        }
    }
}
