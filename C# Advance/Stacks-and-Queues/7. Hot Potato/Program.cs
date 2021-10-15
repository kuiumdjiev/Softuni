using System;
using System.Collections.Generic;

namespace _7._Hot_Potato
{
    class Program
    {
        static void Main(string[] args)
        {
           
           
            Queue<string> queue = new Queue<string>(Console.ReadLine().Split(" "));
            int count = 1;
            int firstKid = int.Parse(Console.ReadLine());
            while (queue.Count>1)
            {
                if (firstKid==count)
                {
                    Console.WriteLine($"Removed {queue.Dequeue()}");
                    count = 1;
                }
                else
                {
                    queue.Enqueue(queue.Dequeue());
             
                }
            }
            Console.WriteLine("Last kid is" + queue.Dequeue());
        }
    }
}
