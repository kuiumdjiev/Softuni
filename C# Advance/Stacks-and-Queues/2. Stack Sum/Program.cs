using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace _2._Stack_Sum
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> inputNumber = Console.ReadLine().Split(" ").Select(int.Parse).ToList();

            Stack<int> numbers = new Stack<int>();
            for (int i = 0; i < inputNumber.Count; i++)
            {
                numbers.Push(inputNumber[i]);
            }
            while (true)
            {
                List<string> input = Console.ReadLine().Split(" ").ToList<string>();
                switch (input[0].ToLower())
                {
                    case "remove":
                        int count = int.Parse(input[1]);
                        if (numbers.Count-count>=0)
                        {
                            for (int i = 0; i < count; i++)
                            {
                                numbers.Pop();
                            }
                        }
                        break;
                    case "add":

                        int firstNumber = int.Parse(input[1]);
                        int secondNumber = int.Parse(input[2]);
                        numbers.Push(firstNumber);
                        numbers.Push(secondNumber);
                        break;
                    case "end":
                        Console.WriteLine("Sum : "+numbers.Sum());
                        return;
                }
            }
        }
    }
}
