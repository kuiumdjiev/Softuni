using System.Linq;
using System.Security.Principal;
using CarShop.Data;
using CarShop.Data.Models;
using CarShop.ViewModels;
using CarShop.ViewModels.Issues;

namespace CarShop.Services
{
    public class Issues:IIssuesService
    {
        private ApplicationDbContext dbContext;

        public Issues(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void AddIssue(string description, string carId)
        {
            var issue = new Issue
            {
                Description = description,
                CarId = carId,
            };

            this.dbContext.Issues.Add(issue);
            this.dbContext.SaveChanges();
        }

        public IssueOverviewViewModel GetModelById(string carId)
        {
            var issues = this.dbContext.Issues.Where(x => x.CarId == carId)
                .Select(x => new IssueViewModel
                {
                    Id = x.Id,
                    Description = x.Description,
                    Status = x.IsFixed == true ? "Yes" : "Not yet",
                }).ToList();

            var model = new IssueOverviewViewModel()
            {
                CarId = carId,
                CarModel = GetCarModel(carId),
                Issues = issues,
            };

            return model;
        }

        public bool Delete(string issueId, string carId)
        {
            var issue = this.GetDetails(issueId, carId);

            if (issue == null)
            {
                return false;
            }

            this.dbContext.Issues.Remove(issue);
            this.dbContext.SaveChanges();
            return true;
        }

        public bool Fix(string issueId, string carId)
        {
            var issue = this.GetDetails(issueId, carId);

            if (issue == null)
            {
                return false;
            }

            issue.IsFixed = true;

            this.dbContext.Issues.Update(issue);
            this.dbContext.SaveChanges();
            return true;
        }

        private Issue GetDetails(string issueId, string carId)
            => this.dbContext.Issues.FirstOrDefault(x => x.Id == issueId && x.CarId == carId);

        private string GetCarModel(string carId)
            => this.dbContext.Cars.Where(x => x.Id == carId).Select(x => x.Year.ToString() + " " + x.Model).FirstOrDefault();
    }
}