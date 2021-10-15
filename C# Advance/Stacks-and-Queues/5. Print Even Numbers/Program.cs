using System;
using System.Collections.Generic;
using System.Linq;

namespace _5._Print_Even_Numbers
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue<int> queue = new Queue<int>( Console.ReadLine().Split().Select(x => int.Parse(x)).Where(i=>i%2==0).ToArray());
            Console.WriteLine(string.Join(", ", queue));
        }
    }
}
