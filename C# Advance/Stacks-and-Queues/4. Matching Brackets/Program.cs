using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace _4._Matching_Brackets
{
    class Program
    {
        static void Main(string[] args)
        {
            string mesage = Console.ReadLine();
            Stack<int> nd = new Stack<int>();
            for (int i = 0; i < mesage.Length; i++)
            {
                switch (mesage[i])
                {
                    case '(':
                        nd.Push(i);
                        break;
                    case ')':
                        int startIndex = nd.Pop();
                        string msg = mesage.Substring(startIndex,i-startIndex+1);
                        Console.WriteLine(msg);
                        break;
                }
            }
        }
    }
}
