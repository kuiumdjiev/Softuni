using System;
using System.Linq;

namespace BOX
{
    public class Box
    {
        public Box(string box)
        {
            var arg = box.Split(' ').Select(int.Parse).ToArray();
            Width = arg[0];
            Depth = arg[1];
            Height = arg[2];
        }

        public int Width { get; set; }

        public int Height { get; set; }

        public int Depth { get; set; }

        public bool IsBigger(Box box)
        {
            return this.Width > box.Width && this.Depth > box.Depth;
        }

        public override string ToString()
        {
            return $"{this.Width} {this.Depth} {this.Height}";
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int[] prev = new int[n];
            int[] length = new int[n];
            Box[] box = new Box[n];

            prev[0] = -1;
            length[0] = -1;

            for (int i = 0; i < n; i++)
            {
                var bestLenght = 1;
                var prevIndex = -1;
                var currBox = box[i];

                for (int j = i-1; j >=0; j--)
                {
                    var prevBox = box[j];
                    if (bestLenght <= length[j] && currBox.IsBigger(prevBox))
                    {
                        bestLenght = length[j] + 1;


                        prevIndex = j;

                    }
                }
                length[i] = bestLenght;
                prev[i] = prevIndex;
            }

        }
    }
}
