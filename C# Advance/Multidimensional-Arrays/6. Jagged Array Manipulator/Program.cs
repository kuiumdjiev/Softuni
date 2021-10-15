using System;
using System.Linq;

namespace _6._Jagged_Array_Manipulator
{
    class Program
    {
        static void Main(string[] args)
        {
            int r = int.Parse(Console.ReadLine());
            double[][] matrix = new double[r][];
            for (int Row = 0; Row < r; Row++)
            {
                matrix[Row] = Console.ReadLine().Split(" ").Select(x => double.Parse(x)).ToArray();
            }
            for (int row = 0;  row< r-1;row ++)
            {
                if (matrix[row].Length== matrix[row+1].Length)
                {
                    for (int cow = 0; cow < matrix[row].Length ; cow++)
                    {
                        matrix[row][cow] *= 2;
                        matrix[row+1][cow] *= 2;

                    }
                    continue;
                }
                for (int cow = 0; cow < matrix[row].Length; cow++)
                {
                    matrix[row][cow] /= 2;
                   

                }
                for (int cow = 0; cow < matrix[row+1].Length; cow++)
                {
                    
                    matrix[row + 1][cow] /= 2;

                }
            }
            string comand = string.Empty;
            while ((comand=Console.ReadLine())!="End")
            {
                string[] ArrInfo = comand.Split(" ").ToArray();
               
                int row = int.Parse(ArrInfo[1]);
                int col = int.Parse(ArrInfo[2]);
                int number = int.Parse(ArrInfo[3]);
                if (row >=0&& col>=0&&col< matrix[row].Length && row < matrix.Length)
                {


                    switch (ArrInfo[0])
                    {

                        case "Add":


                            matrix[row][col] += number;
                            break;
                        case "Subtract":

                            matrix[row][col] -= number;
                            break;
                        


                    }
                }
            }
            foreach (var item in matrix)
            {
                Console.WriteLine(string.Join(" ", item));
            }

        }

    }
}
