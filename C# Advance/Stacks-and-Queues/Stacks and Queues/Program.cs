using System;
using System.Collections.Generic;
using System.Linq;
namespace Stacks_and_Queues
{
    class Program
    {
        static void Main(string[] args)
        {
            List<char> input = Console.ReadLine().ToList();
            Stack<char> stack = new Stack<char>();
            for (int i =0; i < input.Count; i++)
            {
                stack.Push(input[i]);
            }
            foreach (var @char in stack)
            {
                Console.Write(@char);
            }
        }
    }
}
