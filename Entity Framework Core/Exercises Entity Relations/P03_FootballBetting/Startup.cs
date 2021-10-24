using System;
using P03_FootballBetting.Data;

namespace P03_FootballBetting
{
  public   class Startup
    {
        static void Main()
        {
            var context = new FootballBettingContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}
