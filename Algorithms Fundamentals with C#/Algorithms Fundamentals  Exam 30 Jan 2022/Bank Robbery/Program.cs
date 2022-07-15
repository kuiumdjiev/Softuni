using System;
using System.Collections.Generic;
using System.Linq;

namespace Bank_Robbery
{
    public class Program
    {
        private static HashSet<int> a;
        private static HashSet<int> b;

        static void Main(string[] args)
        {
            a= new HashSet<int>();
            b= new HashSet<int>();
            var input = Console.ReadLine().Split(' ').Select(int.Parse).OrderByDescending(x=>x).ToArray();
         
            ////sorting
            //for (int i = 0; i < input.Length; i++)
            //{
            //    var min = i;
            //    for (int j = i + 1; j < input.Length; j++)
            //    {
            //        if (input[j] < input[min])
            //        {
            //            min = j;
            //        }
            //    }

            //    Swap(input, i, min);
            //}
            //slove
            a.Add(input[input.Length-1]);
            for (int i = input.Length-2; i >= 0; i--)
            {
                if (a.Sum() < b.Sum())
                {
                    a.Add(input[i]);
                }
                else
                {
                    b.Add(input[i]);
                }
            }

            Console.WriteLine(string.Join(" ", a));
            Console.WriteLine(string.Join(" ", b));

        }
        private static void Swap(int[] array, int i, int min)
        {
            var temp = array[i];
            array[i] = array[min];
            array[min] = temp;
        }
    }
}
