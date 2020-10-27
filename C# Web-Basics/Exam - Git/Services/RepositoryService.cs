using System;
using System.Collections.Generic;
using System.Linq;
using Git.Data;
using Git.ViewModels.Repositories;

namespace Git.Services
{
    public class RepositoryService : IRepositoriesService
    {
        private readonly ApplicationDbContext db;

        public RepositoryService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public string Create(RepositoryInputModel model, string userId)
        {
            var entity = new Repository()
            {
                Name = model.Name,
                OwnerId = userId,
                CreatedOn = DateTime.Now,
                IsPublic = model.RepositoryType == "Public" ? true : false,
            };

            this.db.Repositories.Add(entity);
            this.db.SaveChanges();

            return entity?.Id;
        }

        public IEnumerable<RepositoryViewModel> GetAllPublicRepositories()
        {
            return this.db.Repositories
                .Where(x => x.IsPublic)
                .Select(x => new RepositoryViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    OwnerName = x.Owner.Username,
                    CommitsCount = x.Commits.Count(),
                    CreatedOn = x.CreatedOn,
                }).ToList();
        }

        public string GetNameById(string Id)
        {
            return this.db.Repositories.FirstOrDefault(x => x.Id == Id)?.Name;
        }
    }
}
