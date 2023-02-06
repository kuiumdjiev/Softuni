using System;
using System.Linq;

namespace Selection_Sort
{
    public class Program
    {
        static void Main(string[] args)
        {
            var array = Console.ReadLine().Split(" ").Select(x=>int.Parse(x)).ToArray();

            for (int i = 0; i < array.Length; i++)
            {
                var min = i;
                for(int j =  i +1 ; j <array.Length; j++)
                {
                    if ( array[j] < array[min] )
                    {
                        min = j;
                    }
                }

                Swap(array ,  i , min);
            }
            Console.WriteLine(string.Join(" ",array));
        }

        private static void Swap(int[] array, int i, int min)
        {
            var temp = array[i];
            array[i] = array[min];
            array[min] = temp;
        }
    }
}
