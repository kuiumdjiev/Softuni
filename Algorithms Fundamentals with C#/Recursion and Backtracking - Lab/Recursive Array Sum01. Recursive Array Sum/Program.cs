using System;
using System.Linq;

namespace Recursive_Array_Sum01._Recursive_Array_Sum
{

    public class Program
    {
        static void Main(string[] args)
        {
            int[] arr = Console.ReadLine().Split(" ").Select(x => int.Parse(x)).ToArray();
            int sum=    Sum(arr, 0);
            Console.WriteLine(sum);
        }

        private static int Sum(int[] arr, int index)
        {
            if (index == arr.Length) return 0;
            return arr[index] + Sum(arr , index+1);
        }
    }
}
