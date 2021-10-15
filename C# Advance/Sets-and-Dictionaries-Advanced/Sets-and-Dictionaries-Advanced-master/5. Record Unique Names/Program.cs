using System;
using System.Collections.Generic;

namespace _5._Record_Unique_Names
{
    class Program
    {
        static void Main(string[] args)
        {

            
            HashSet<string> names = new HashSet<string>();
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < 8; i++)
            {
                string name = Console.ReadLine();
                names.Add(name);
            }
            Console.Write(string.Join(" ", names));
        }
    }
}
