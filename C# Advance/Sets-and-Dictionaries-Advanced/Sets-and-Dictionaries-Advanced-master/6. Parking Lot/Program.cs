using System;
using System.Collections.Generic;
using System.Linq;

namespace _6._Parking_Lot
{
    class Program
    {
        static void Main(string[] args)
        {
            HashSet<string> cars = new HashSet<string>();
            string comand = string.Empty;
            while ((comand=Console.ReadLine())!= "END")
            {
                string[] info = comand
                     .Split(", ")
                     .ToArray();
                string options = info[0];
                string car = info[1];
                if (options=="IN")
                {
                    cars.Add(car);
                    continue;
                }
                cars.Remove(car);
            }
            if (cars.Any())
            {
                Console.WriteLine(string.Join(" ", cars));
            }
            else
            {
                Console.WriteLine("Parking Lot is Empty");
            }
            
        }
    }
}
