using Git.Services;
using Git.ViewModels.Commits;
using SUS.HTTP;
using SUS.MvcFramework;

namespace Git.Controllers
{
    public class CommitsController : Controller
    {
        private readonly IComitService comitService;

        public CommitsController(IComitService comitService)
        {
            this.comitService = comitService;
        }
        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = this.comitService.GetAll();
            return this.View(viewModel);
        }

        [HttpPost]
        public HttpResponse Create(string description, string id, string repoId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrWhiteSpace(description) || description.Length < 5)
            {
                return this.Error("Description should be at least 5 characters long.");
             }

            var userId = this.GetUserId();
            this.comitService.Create(description, id, userId, repoId);
            return this.Redirect("/Repositories/All");
        }

        public HttpResponse Create(string repoId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var repoName = this.comitService.GetNameById(repoId);

            var viewModel = new CommitInputModel()
            {
                Id = repoId,
                Name = repoName
            };

            return this.View(viewModel);
        }


        public HttpResponse Delete(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var userId = this.GetUserId();
            this.comitService.Delete(id, userId);
            return this.Redirect("/Commits/All");
        }
    }
}