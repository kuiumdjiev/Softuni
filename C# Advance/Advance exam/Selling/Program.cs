using System;
using System.Linq;

namespace Selling
{
    class Program
    {
        static void Main(string[] args)
        {

            int n = int.Parse(Console.ReadLine());
            char[,] matrix = new char[n, n];

            int colStart = 0;
            int rowStart = 0;

            int colFirstO=-1;
            int rowFirstO=-1 ;

            int nextColO = -1;
            int nextRowO = -1;

            int money = 0;

            for (int Col = 0; Col < n; Col++)
            {
                char[] input = Console.ReadLine().ToArray();
                for (int Row = 0; Row < input.Length; Row++)
                {
                    
                    if (input[Row] == 'S')
                    {
                        colStart = Col;
                        rowStart = Row;
                    }
                    if (input[Row]=='O')
                    {
                        if (colFirstO == -1)
                        {
                            colFirstO = Col;
                            rowFirstO = Row;
                        }
                        else
                        {
                            nextColO = Col;
                            nextRowO = Row;
                        }
                    }
                    
                    matrix[Col, Row] = input[Row];
                }

            }
            while (true)
            {
                string comand = Console.ReadLine();
                try
                {
                    matrix[rowStart, colStart] = '-';
                    switch (comand)
                    {
                        case "right":
                            colStart++;
                            

                            break;
                        case "down":
                            rowStart++;
                            break;
                        case "up":
                            rowStart--;
                            break;
                        case "left":
                            colStart--;
                            break;
                        default:
                            break;

                    }
                    if (colStart == colFirstO && rowStart==rowFirstO)
                    {

                        colStart = nextColO;
                        rowStart =nextRowO;
                    }
                    if (colStart == nextColO && rowStart == nextRowO )
                    {
                        colStart = colFirstO;
                        rowStart = rowFirstO;
                    }
                    if (char.IsDigit(matrix[rowStart,colStart]))
                    {
                        int f = int.Parse(matrix[rowStart, colStart].ToString());
                        money += f ;
                        matrix[rowStart, colStart] = '-';
                    }
                    matrix[rowStart, colStart] = 'S';
                    if (money>50)
                    {
                        Console.WriteLine("Yes");
                        return;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Bad news, you are out of the bakery.");
                    return;
                }
            }
        }
    }
}
