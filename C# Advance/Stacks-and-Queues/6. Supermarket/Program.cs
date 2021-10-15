using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace _6._Supermarket
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = 0;
            Queue<string> queue = new Queue<string>();
            while (true)
            {
                string name = Console.ReadLine();
                if (name=="Paid")
                {
                    break;
                }
                queue.Enqueue(name);
            }
            while (true)
            {
                string other = Console.ReadLine();
                if (other =="End")
                {
                    break;
                }
                count++;
            }
            Console.WriteLine(string.Join("\r\n" , queue));
            Console.WriteLine(count);
        }
    }
}
