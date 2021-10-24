using System.Collections.Generic;

namespace P03_FootballBetting.Data.Models
{
    public class Color
    {
        public Color()
        {
            PrimaryKitTeams = new HashSet<Color>();
            SecondaryKitTeams = new HashSet<Color>();
        }
        public int ColorId { get; set; }
        public string Name { get; set; }
        public ICollection<Color> PrimaryKitTeams { get; set; }
        public ICollection<Color> SecondaryKitTeams { get; set; }
    }
}