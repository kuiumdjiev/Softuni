using System;
using System.Collections.Generic;
using System.Linq;

namespace _1._Count_Same_Values_in_Array
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<double, int> value = new Dictionary<double, int>();
            double[] input = Console.ReadLine().Split(' ').Select(double.Parse).ToArray();
            for (int i = 0; i < input.Length; i++)
            {
                if (!value.ContainsKey(input[i]))
                {
                    value.Add(input[i], 0);
                }
                value[input[i]]++;
            }
            foreach (var number in value)
            {
                Console.WriteLine(number.Key +" "+ number.Value);
            }
        }
    }
}
