using System.Collections.Generic;

namespace CarShop.ViewModels.Issues
{
    public class IssueOverviewViewModel
    {
        public string CarModel { get; set; }

        public string CarId { get; set; }

        public IEnumerable<IssueViewModel> Issues { get; set; }
    }
}