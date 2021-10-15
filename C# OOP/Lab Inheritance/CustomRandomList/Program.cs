using System;

namespace CustomRandomList
{
  public  class StartUp
    {
        static void Main(string[] args)
        {
            RandomList rl = new RandomList();
            rl.Add("Pesho");
            rl.Add("Ti");
            rl.Add("To");
            Console.WriteLine(rl.RandomString());
        }
    }
}
