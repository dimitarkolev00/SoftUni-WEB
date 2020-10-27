using System.Collections.Generic;
using Git.ViewModels.Repositories;

namespace Git.Services
{
    public  interface IRepositoriesService
    {
        string Create(RepositoryInputModel model, string userId);

        IEnumerable<RepositoryViewModel> GetAllPublicRepositories();

        string GetNameById(string Id);
    }
}
