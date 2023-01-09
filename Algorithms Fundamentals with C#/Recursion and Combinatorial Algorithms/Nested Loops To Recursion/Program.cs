using System;

namespace Nested_Loops_To_Recursion
{
    public class Program
    {
        public static int[] combinations;


        static void Main(string[] args)
        {
            int index = int.Parse(Console.ReadLine());
            combinations = new int[index] ;
            Recursion(0);
        }

        public static void Recursion(int index)
        {
            if (index >= combinations.Length)
            {
                Console.WriteLine(string.Join(" ", combinations));
                return;
            }

            for (int i = 1; i <= combinations.Length; i++)
            {
                combinations[index] = i;
                Recursion(index+1 );
            }
        }
    }
}
