using System;
using System.Collections.Generic;
using System.Linq;

namespace School_Teams
{
    public class Program
    {

        static void Main()
        {
          var  grils = Console.ReadLine().Split(", ").ToArray();
          var grilsComb = new string[3];
          var grilsCombs = new List<string[]>();
          GenCombs(0,0 , grils, grilsComb,grilsCombs);
         
          var boys = Console.ReadLine().Split(", ").ToArray();
          var boysComb = new string[2];
          var boysCombs = new List<string[]>();
          GenCombs(0, 0, boys, boysComb, boysCombs);

        Print(boysCombs , grilsCombs);
        }

        private static void Print(List<string[]> boysCombs, List<string[]> grilsCombs)
        {
            foreach (var grils in grilsCombs)
            {
                foreach (var  boys in boysCombs)
                {
                    Console.WriteLine($"{string.Join(", ", grils )}, {string.Join(", ",boys)}");
                }
            }
        }

        private static void GenCombs(int idex, int elementsStartIndex, string[] elements, string[] comb , List<string[]> combs)
        {
            if (idex >= comb.Length)
            {
                combs.Add(comb.ToArray());
                return;
            }

            for (int i = elementsStartIndex; i < comb.Length; i++)
            {
                comb[i] = elements[i];
                GenCombs(idex + 1, elementsStartIndex + 1, elements,comb,combs);
            }
        }
    }
}
