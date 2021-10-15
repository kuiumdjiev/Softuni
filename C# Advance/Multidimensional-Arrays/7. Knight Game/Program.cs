using System;
using System.Linq;

namespace _7._Knight_Game
{
    class Program
    {
        static void Main(string[] args)
        {
          
            int r = int.Parse( Console.ReadLine());
           
            char[,] matrix = new char[r, r];
            int knightCount = 0;
            int killerRow = 0;
            int killerCol = 0;
            for (int row = 0; row < r; row++)
            {
                char[] @char = Console.ReadLine().ToArray();
                for (int col = 0; col < r; col++)
                {

                    matrix[row, col] = @char[col];
                }
            }
            while (true)
            {
                int max = 0;
                for (int row = 0; row < r; row++)
                {
                    
                    for (int col = 0; col < r; col++)
                    {

                        if (matrix[row,col]=='K')
                        {
                            int curnet = 0;
                            if (Check(matrix, row +1, col -2) )
                            {
                                curnet++;
                            }
                            if (Check(matrix, row +1, col +2) )
                            {
                                curnet++;
                            }
                            if ( Check(matrix, row + 2, col - 1)  )
                            {
                                curnet++;
                            }
                            if (  Check(matrix, row + 2, col + 1))
                            {
                                curnet++;
                            }
                            if(Check(matrix, row - 2, col + 1)  )
                            {
                                curnet++;
                            }
                            if ( Check(matrix, row - 2, col - 1) )
                            {
                                curnet++;
                            }
                            if (Check(matrix, row - 1, col + 2) )
                            { 
                                curnet++;
                            }
                            if ( Check(matrix, row - 1, col - 2) )
                            {
                                curnet++;
                            }
                            if ( curnet > max)
                            {
                                max = curnet;
                                killerCol = col;
                                killerRow = row;
                            }
                        }
                    }
                }
                if (max > 0)
                {
                    matrix[killerRow, killerCol] = '0';
                    knightCount++;
                }
                else
                {
                    Console.WriteLine(knightCount);
                    break;
                }
            }
      
        }
        public static bool Check(char[,] Matrix, int row,int col )
        {
            if (row >= 0 && row < Matrix.GetLength(0) && col >= 0 && col < Matrix.GetLength(1))
            {
                if ( Matrix[row, col] == 'K')
                {
                    return true;
                }
                
            }
           
                return false;
            


        }

    }

}
