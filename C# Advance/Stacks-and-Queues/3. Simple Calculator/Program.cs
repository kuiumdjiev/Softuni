using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace _3._Simple_Calculator
{
    class Program
    {
        static void Main(string[] args)
        {

            List<string> input = Console.ReadLine().Split(" ").ToList();
            Stack<string> stack = new Stack<string>(input);
            while (true)
            {
                if (stack.Count==1)
                {
                    break;
                }
                
                    int firstNumber = int.Parse(stack.Pop());
                    string rezult = stack.Pop();
                    int secondNumber = int.Parse(stack.Pop());
                    if (rezult=="+")
                    {
                        stack.Push((firstNumber+secondNumber).ToString());
                    }
                    else
                    {
                        stack.Push((firstNumber - secondNumber).ToString());
                    }
                
               
            }
            Console.WriteLine(stack.Peek());

        } 


    }
}
