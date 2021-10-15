namespace Shapes
{
    public class Circle:Shape
    {
        public Circle(double radius)
        {
           
            
        }
        public double Radius { get; private set; }
       
        public override double CalculatePerimeter()
        {
            return 2 * 3.14 * Radius;
        }

        public override double CalculateArea()
        {
            return Radius * Radius * 3.14;
        }

        public override string Draw()
        {
            return base.Draw() + "Circle";
        }
    }
}