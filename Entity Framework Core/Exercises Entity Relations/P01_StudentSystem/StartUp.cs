
using System;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem
{
   public class Startup
    {
     public  static void Main()
     {
         var context = new StudentSystemContext();
         context.Database.EnsureDeleted();
         context.Database.EnsureCreated();
     }
    }
}
