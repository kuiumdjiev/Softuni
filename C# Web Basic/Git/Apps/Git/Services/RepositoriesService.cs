using System;
using System.Collections.Generic;
using System.Linq;
using Git.Data;
using Git.ViewModels.Repositories;
using SUS.MvcFramework;

namespace Git.Services
{
    public class RepositoriesService: IRepositoriesService
    {
        private readonly ApplicationDbContext db;

        public RepositoriesService( ApplicationDbContext db)
        {
            this.db = db;
        }

        public ICollection<RepositoryViewModel> GetAll()
        {
            return db.Repositories.Select(x => new RepositoryViewModel
            {
                Id= x.Id,
                Name = x.Name,
                Owner = x.Owner.Username,
                CreatedOn = x.CreatedOn,
                Commits = x.Commits.Count
            }).ToArray();
        }

        [HttpPost]
        public string Create(string name, string type, string userId)
        {
            var owner = this.db.Users.FirstOrDefault(x => x.Id == userId);
            var repositore = new Repository()
            {
                Name = name,
                IsPublic = type=="Public"?true:false,
                CreatedOn = DateTime.UtcNow,
                OwnerId = owner.Id,
                Commits = new HashSet<Commit>()
            };
            this.db.Repositories.Add(repositore);
            this.db.SaveChanges();

            return repositore.Id;
        }
    }
}