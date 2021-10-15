using System;
using AquaShop.Models.Fish.Contracts;

namespace AquaShop.Models.Fish
{
    public abstract class Fish: IFish
    {
        private string name;
        private string special;

        private decimal price;

        protected Fish(string name, string species, decimal price)
        {
            Name = name;
            Species = species;
            Price = price;
        }
        public string Name
        {
            get=>name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException( "Fish name cannot be null or empty.");
                }

                name = value;
            }
        }

        public string Species
        {
            get=>special;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Fish species cannot be null or empty.");
                }

                special = value;
            }
        }
        public abstract int Size { get; protected set; }

        public decimal Price
        {
            get=>price;
            private set
            {
                if (value<=0)
                {
                    throw new ArgumentException("Fish price cannot be below or equal to 0.");
                }

                price = value;
            }
        }
        public virtual void Eat()
        {
            


        }
    }
}