namespace AquaShop.Models.Fish
{
    public class SaltwaterFish:Fish
    {
        //Can only live in SaltwaterAquarium!
        public SaltwaterFish(string name, string species, decimal price)
            : base(name, species, price)
        {
            this.Size = 2;
        }

        public override int Size { get; protected set; }

        public override void Eat()
        {
            this.Size += 2;
        }
    }
}