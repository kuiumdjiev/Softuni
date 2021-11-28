using System.Collections.Generic;
using Git.ViewModels.Repositories;

namespace Git.Services
{
    public interface IRepositoriesService
    {
        ICollection<RepositoryViewModel> GetAll();


        string Create(string name, string type, string userId);
    }
}