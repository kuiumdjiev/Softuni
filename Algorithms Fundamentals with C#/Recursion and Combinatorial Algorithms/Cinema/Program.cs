using System;
using System.Collections.Generic;
using System.Linq;

namespace Cinema
{
    public class Program
    {
        public static bool[] locked;
        public static List<string> nonStaticPeople;
        public static string[] people;

        static void Main(string[] args)
        {
             nonStaticPeople = Console.ReadLine().Split(", ").ToList();
            people = new string[nonStaticPeople.Count];
            locked= new bool[nonStaticPeople.Count];
            while (true)
            {
          var      line = Console.ReadLine();

                if (line == "generate")
                {
                    break;
                }

                var parts = line.Split(" - ");
                var name = parts[0];
                var  number =int.Parse( parts[1])-1;

                people[number]= name;
                locked[number]=true;
                nonStaticPeople.Remove(name);
            }

            Permute(0);
        }

        private static void Permute(int idex)
        {
            if (idex>=nonStaticPeople.Count)
            {
                var peopleIdx = 0;
                for (int i = 0; i < people.Length; i++)
                {
                    if (i==people.Length-1)
                    {
                        if (locked[i])
                        {
                            Console.Write($"{people[i]}");
                        }
                        else
                        {
                            Console.Write($"{nonStaticPeople[peopleIdx++]}");
                        }
                    }
                    else
                    {
                        if (locked[i])
                        {
                            Console.Write($"{people[i]} ");
                        }
                        else
                        {
                            Console.Write($"{nonStaticPeople[peopleIdx++]} ");
                        }
                    }
                   

                }
                Console.WriteLine();

                return;
            }
            Permute(idex + 1);

            for (int i = idex + 1; i < nonStaticPeople.Count; i++)
            {
                Swap(idex, i);
                Permute(idex+1);
                Swap(idex, i);

            }
        }

        private static void Swap(int idex, int i)
        {
            var temp = nonStaticPeople[i];
            nonStaticPeople[i] = nonStaticPeople[idex];
            nonStaticPeople[idex] = temp;
        }
    }
}
