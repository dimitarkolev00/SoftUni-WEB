using System.Collections.Generic;
using Git.ViewModels.Commits;

namespace Git.Services
{
    public interface ICommitsService
    {
        string Create(string description, string userId, string repoId);

        IEnumerable<CommitsViewModel> GetAll(string userId);

        bool IsUserACreator(string userId, string commitId);

        bool Delete(string Id);

    }
}
