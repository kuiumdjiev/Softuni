using System.Collections.Generic;
using System.Linq;
using CarShop.Data;
using CarShop.Data.Models;
using CarShop.ViewModels.Car;

namespace CarShop.Services
{
    public class CarsService:ICarsService
    {
        private ApplicationDbContext dbContext;

        public CarsService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public HashSet<CarViewModel> GetMyCars(string ownerId)
        {
           return dbContext.Cars.Where(x => x.OwnerId == ownerId).Select(x=>new CarViewModel()
           {
Id = x.Id,
Model = x.Model,
PlateNumber = x.PlateNumber,
ImageUrl = x.PictureUrl,
RemainingIssues = x.Issues.Where(y=>y.IsFixed==false).Count(),
FixedIssues = x.Issues.Where(y=>y.IsFixed==true).Count()
           }).ToHashSet();
        }
        public HashSet<CarViewModel> GetAllWithIssues()
        {
            var cars = this.dbContext.Cars
                .Select(x => new CarViewModel
                {
                    Id = x.Id,
                    Model= x.Model,
                    PlateNumber = x.PlateNumber,
                    ImageUrl = x.PictureUrl,
                    RemainingIssues = x.Issues.Where(i => i.IsFixed == false).Count(),
                    FixedIssues = x.Issues.Where(i => i.IsFixed == true).Count(),
                }).ToHashSet();

            return cars.Where(x => x.RemainingIssues > 0).ToHashSet();
        }
        public void CreateCar(CarAddModel model, string id)
        {
            Car car = new Car()
            {
                Model = model.Model,
                Year = model.Year,
                PictureUrl = model.Image,
                PlateNumber = model.PlateNumber,
                OwnerId = id
            };
            dbContext.Cars.Add(car);
            dbContext.SaveChanges();
        }

    }
}