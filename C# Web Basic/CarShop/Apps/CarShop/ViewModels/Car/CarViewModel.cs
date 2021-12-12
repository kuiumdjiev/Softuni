namespace CarShop.ViewModels.Car
{
    public class CarViewModel
    {
        public string Id { get; set; }

        public string Model { get; set; }

        public string PlateNumber { get; set; }

        public string ImageUrl { get; set; }

        public int RemainingIssues { get; set; }

        public int FixedIssues { get; set; }

    }
}