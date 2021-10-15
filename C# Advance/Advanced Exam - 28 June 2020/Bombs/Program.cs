using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Bombs
{
    class Program
    {
        public const int Dature = 40;
        public const int Cherry = 60;
        public const int Smoke = 120;
        static void Main(string[] args)
        {
            int DaturaCount = 0;
            int CherryCount = 0;
            int SmokeCount = 0;
            bool isFull = false;
            Queue<int> bombEfects = new Queue<int>(Console.ReadLine().Split(", ").Select(int.Parse).ToList());
            Stack<int> bombCasinigs = new Stack<int>(Console.ReadLine().Split(", ").Select(int.Parse).ToList());
            while (bombEfects.Count()>0&&bombCasinigs.Count()>0)
            {
                if (DaturaCount>=3&&CherryCount>=3&&SmokeCount>=3)
                {
                    isFull = true;
                    break;
                }
                int bombEfect = bombEfects.Peek();
                int bombCasing = bombCasinigs.Peek();
                int sum = bombCasing + bombEfect;
                if (sum==Dature)
                {
                    bombEfects.Dequeue();
                    bombCasinigs.Pop();
                    DaturaCount++;
                    continue;
                }
                if (sum==Cherry)
                {
                    bombEfects.Dequeue();
                    bombCasinigs.Pop();
                    CherryCount++;
                    continue;
                }
                if (sum==Smoke)
                {
                    bombEfects.Dequeue();
                    bombCasinigs.Pop();
                    SmokeCount++;
                    continue;
                }
                else
                {
                    bombCasinigs.Pop();
                    bombCasinigs.Push(bombCasing - 5);
                }
            }
            if (isFull)
            {
                Console.WriteLine("Bene! You have successfully filled the bomb pouch!");
            }
            else
            {
                Console.WriteLine( "You don't have enough materials to fill the bomb pouch.");
            }
            if (bombEfects.Count>0)
            {
                Console.Write("Bomb Effects: ");
                Console.WriteLine(string.Join(", ", bombEfects));
            }
            else 
            {
                Console.WriteLine("Bomb Effects: empty");
            }
            if (bombCasinigs.Count > 0)
            {
                Console.Write("Bomb Casings: ");
                Console.WriteLine(string.Join(", ", bombCasinigs));
            }
            else
            {
                Console.WriteLine("Bomb Casings: empty");
            }
            Console.WriteLine( $"Cherry Bombs: {CherryCount}");
            Console.WriteLine($"Datura Bombs: {DaturaCount}");
            Console.WriteLine($"Smoke Decoy Bombs: {SmokeCount}");
        }
    }
}
