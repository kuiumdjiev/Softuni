using System;

namespace N_Choose_K_Count
{
    public class Program
    {
       

        static void Main(string[] args)
        {
         int   n = int.Parse(Console.ReadLine());
          int  k = int.Parse(Console.ReadLine());
          Console.WriteLine(NK(n  ,k ));
        }

        public static int NK(int n, int k)
        {
            if (n == 0|| k==0|| n==k )
            {
                return 1;
            }


            return NK(n- 1 , k-1)+ NK(n-1, k);
        }
    }
}
