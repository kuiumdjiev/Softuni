using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Recursion_and_Combinatorial_Algorithms
{
    public class Program
    {
        public static string[] array;
        static void Main()
        {
            array = Console.ReadLine().Split(" ").ToArray();
            Print(0);
            Console.WriteLine(string.Join(" ", array));
        }

        public static void Print(int index)
        {
            if (array.Length / 2==index)
            {
                return ;
            }

            string a = array[index];
            array[index] = array[array.Length - index-1];
            array[array.Length - index-1] = a;

            Print(index+1);
        }
    }
}
