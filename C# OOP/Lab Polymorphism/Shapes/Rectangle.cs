namespace Shapes
{
    public class Rectangle:Shape
    {
        public Rectangle(double height,double width)
        {
            Height = height;
            Width = width;
        }
        public double   Height  { get;private set; }
        public double Width { get;private set; }
        public override double CalculatePerimeter()
        {
            return Width * 2 + Height * 2;
        }

        public override double CalculateArea()
        {
            return Width * Height;
        }

        public override string Draw()
        {
            return base.Draw()+ "Rectangle";
        }
    }
}