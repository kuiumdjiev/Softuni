using System.Collections.Generic;
using Git.ViewModels.Commits;

namespace Git.Services
{
    public interface IComitService
    {
        ICollection<ComitViewModel> GetAll();
        
       string Create(string description, string id, string userId, string repoId);

        void Delete(string id, string userId);

        string GetNameById(string id);


    }
}