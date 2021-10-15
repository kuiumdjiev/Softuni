using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AquaShop.Core.Contracts;
using AquaShop.Models.Aquariums;
using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Fish;
using AquaShop.Models.Fish.Contracts;
using AquaShop.Repositories;
using AquaShop.Repositories.Contracts;

namespace AquaShop.Core
{
    public class Controller: IController
    {
        private IRepository<IDecoration> decorations;
        private ICollection<IAquarium> aquarias;

        public Controller()
        {
            decorations = new DecorationRepository();
            aquarias = new List<IAquarium>();
        }
        public string AddAquarium(string aquariumType, string aquariumName)
        {
            IAquarium aquarium = null;
            switch (aquariumType)
            {
                case "FreshwaterAquarium":
                    aquarium = new FreshwaterAquarium(aquariumName);
                    break;
                case "SaltwaterAquarium":
                    aquarium = new SaltwaterAquarium(aquariumName);
                    break;
                default:
                    throw new InvalidOperationException("Invalid aquarium type.");
            }
            aquarias.Add(aquarium);
            return  $"Successfully added {aquariumType}.";
        }

        public string AddDecoration(string decorationType)
        {
            IDecoration decoration = null;
            switch (decorationType)
            {

                case "Ornament":
                    decoration = new Ornament();
                    break;
                case "Plant":
                    decoration = new Plant(); 
                    break;
                default:
                    throw new InvalidOperationException(	"Invalid decoration type.");
            }
            decorations.Add(decoration);
            return $"Successfully added {decorationType}.";
        }

        public string InsertDecoration(string aquariumName, string decorationType)
        {
            IAquarium aquarium = aquarias.FirstOrDefault(x => x.Name == aquariumName);
            IDecoration decoration = decorations.FindByType(decorationType);
            if (decoration==null)
            {
                throw new InvalidOperationException(	$"There isn't a decoration of type {decorationType}.");
            }

            decorations.Remove(decoration);
            aquarium.AddDecoration(decoration);
            return $"Successfully added {decorationType} to {aquariumName}.";
        }

        public string AddFish(string aquariumName, string fishType, string fishName, string fishSpecies, decimal price)
        {
            IFish fish = null;
            IAquarium aquarium = null;
            string typeOfWater = null;
            switch (fishType)
            {
                case "FreshwaterFish":
                    typeOfWater = "Freshwate";
                    fish = new FreshwaterFish(fishName, fishSpecies, price);
                    break;
                case "SaltwaterFish":
                    typeOfWater = "Saltwater";
                    fish = new SaltwaterFish(fishName, fishSpecies, price);
                    break;
                default:
                    throw new InvalidOperationException("Invalid fish type.");
            }

            aquarium = aquarias.FirstOrDefault(x => x.Name == aquariumName);
            string msg = aquarium.GetType().Name;
            if (msg.StartsWith(typeOfWater))
            {
                aquarium.AddFish(fish);
                return $"Successfully added {fishType} to {aquariumName}.";
            }
            return  "Water not suitable.";
        }

        public string FeedFish(string aquariumName)
        {
            IAquarium aquarium = aquarias.FirstOrDefault(x => x.Name == aquariumName);
            aquarium.Feed();
            return $"Fish fed: {aquarium.Fish.Count}";
        }

        public string CalculateValue(string aquariumName)
        {
            IAquarium aquarium = aquarias.FirstOrDefault(x => x.Name == aquariumName);
            decimal sum = aquarium.Fish.Sum(x => x.Price) + aquarium.Decorations.Sum(x => x.Price);
            return $"The value of Aquarium {aquariumName} is {sum:F2}.";
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
       
            foreach (var aquaria in aquarias)
            {
             
                sb.AppendLine(aquaria.GetInfo());
          
             
            }

            return sb.ToString().TrimEnd();
        }
    }
}