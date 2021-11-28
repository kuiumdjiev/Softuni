using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.ConstrainedExecution;
using Git.Data;
using Git.ViewModels.Commits;

namespace Git.Services
{
    public class ComitService:IComitService
    {
        private readonly ApplicationDbContext db;

        public ComitService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public ICollection<ComitViewModel> GetAll()
        {
return db.Commits.Select(x=>new ComitViewModel()
{
    Description = x.Description,
    CreatedOn = x.CreatedOn,
    Id = x.Id,
    Repository = x.Repository.Name
}).ToArray();
        }

        public string Create(string description, string id, string userId, string reportoryId)
        {
            var user = db.Users.FirstOrDefault(x => x.Id == userId);
            var reportory = db.Repositories.FirstOrDefault(x => x.Id == reportoryId);
            var comit = new Commit()
            {
                Description = description,
                CreatedOn = DateTime.UtcNow,
                Creator = user,
                Repository = reportory
            };
            db.Commits.Add(comit);
            db.SaveChanges();
            return comit.Id;

        }

        public void Delete(string id , string userId)
        {
            var commit = this.db.Commits.Where(x => x.Id == id).FirstOrDefault();

            if (commit.CreatorId == userId)
            {
                this.db.Commits.Remove(commit);
                this.db.SaveChanges();
            }
        }

        public string GetNameById(string id)
        {
            return db.Repositories.FirstOrDefault(x => x.Id == id).Name;
        }
    }
}