using System;
using System.Linq;

namespace Binary_Search
{
    public class Program
    {
        static void Main(string[] args)
        {

            var elements = Console.ReadLine().Split(" ").Select(x=>int.Parse(x)).ToArray();
            var num = int.Parse(Console.ReadLine());

            Console.WriteLine(BinarySearch(elements , num));
        }

        public static int BinarySearch(int[] elements , int num)
        {
            int left = 0;
            int right = elements.Length-1;
            int mid = (left + right) / 2;
            while (elements[mid] != num)
            {
                if (elements[mid] < num)
                {
                     left= mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
                 mid = (left + right) / 2;
            }

            return mid;
        }
    }
}
