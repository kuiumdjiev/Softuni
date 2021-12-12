using System.Collections.Generic;
using CarShop.ViewModels.Car;

namespace CarShop.Services
{
    public interface ICarsService
    {
        HashSet<CarViewModel> GetMyCars(string ownerId);

        void CreateCar( CarAddModel model , string id);

        HashSet<CarViewModel> GetAllWithIssues();
    }
}