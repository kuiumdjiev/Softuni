using System;
using System.Text.RegularExpressions;
using CarShop.Services;
using CarShop.ViewModels.Car;
using SUS.HTTP;
using SUS.MvcFramework;

namespace CarShop.Controllers
{
    public class CarsController:Controller
    {
        private IUsersService usersService;
        private  ICarsService carsService;

        public CarsController(
             ICarsService carsService,
             IUsersService usersService)
        {
            this.carsService = carsService;
            this.usersService = usersService;
        }

        public HttpResponse Add()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var userId = this.GetUserId();

            if (this.usersService.IsUserMechanic(userId))
            {
                return this.Error("Cannot add a car! You are not a client.");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(CarAddModel input)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var userId = this.GetUserId();
            if (this.usersService.IsUserMechanic(userId))
            {
                return this.Error("Cannot add a car! You are not a client.");
            }

            if (string.IsNullOrEmpty(input.Model) || input.Model.Length < 5 || input.Model.Length > 20)
            {
                return this.Error("Model is required and should be between 5 and 20 characters long.");
            }

            if (input.Year < 1980)
            {
                return this.Error("Car year too old.");
            }

            if (string.IsNullOrEmpty(input.Image) || !Uri.TryCreate(input.Image, UriKind.Absolute, out _))
            {
                return this.Error("Image url should be valid.");
            }

            if (string.IsNullOrEmpty(input.PlateNumber) || !Regex.IsMatch(input.PlateNumber, @"^[A-Z]{2}[0-9]{4}[A-Z]{2}$"))
            {
                return this.Error(
                    "Plate Number is required and should contain 2 Capital English letters, followed by 4 digits, followed by 2 Capital English letters.");
            }

            this.carsService.CreateCar(input, userId);
            return this.Redirect("/Cars/All");
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var userId = this.GetUserId();

            if (!this.usersService.IsUserMechanic(userId))
            {
                var cars = this.carsService.GetMyCars(userId);
                return this.View(cars);
            }

            var carsWithIssues = this.carsService.GetAllWithIssues();
            return this.View(carsWithIssues);
        }


    }
}