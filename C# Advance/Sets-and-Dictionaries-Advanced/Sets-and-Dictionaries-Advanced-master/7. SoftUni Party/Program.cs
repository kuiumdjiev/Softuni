using System;
using System.Collections.Generic;

namespace _7._SoftUni_Party
{
    class Program
    {
        static void Main(string[] args)
        {
            HashSet<string> vip = new HashSet<string>();
            HashSet<string> normal = new HashSet<string>();
            HashSet<string> noneOnVip = new HashSet<string>();
            HashSet<string> noneOnNormal = new HashSet<string>();
            string comand = string.Empty;
            while ((comand=Console.ReadLine())!="PARTY" )
            {
                
                if (comand.Length==8)
                {
                    int number;
                    string check = comand[0].ToString();
                    if (int.TryParse(check,out number))
                    {
                        vip.Add(comand);
                    }
                    else
                    {
                        normal.Add(comand);
                    }
                }
            }
            int count = 0;
            while ((comand = Console.ReadLine()) != "END")
            {

                if (comand.Length == 8)
                {

                    if (vip.Contains(comand))
                    {
                        vip.Remove(comand);
                        continue;
                    }
                    normal.Remove(comand);

                }
               
            }
            Console.WriteLine(normal.Count+vip.Count);
            Console.WriteLine(string.Join(" ", vip));
            Console.WriteLine(string.Join(" ", normal));
        }
    }
}
