using System;
using System.Collections.Generic;
using System.Linq;
using Git.Data;
using Git.ViewModels.Commits;

namespace Git.Services
{
    public class CommitsService: ICommitsService
    {
        private readonly ApplicationDbContext db;

        public CommitsService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public string Create(string description, string userId, string repoId)
        {
            var entity = new Commit()
            {
                Description = description,
                CreatedOn = DateTime.Now,
                CreatorId = userId,
                RepositoryId = repoId,
            };

            this.db.Commits.Add(entity);
            this.db.SaveChanges();

            return entity?.Id;
        }

        public bool Delete(string Id)
        {
            var entity = this.db.Commits.FirstOrDefault(x => x.Id == Id);

            if (entity == null)
            {
                return false;
            }

            this.db.Commits.Remove(entity);
            this.db.SaveChanges();

            return true;
        }

        public IEnumerable<CommitsViewModel> GetAll(string userId)
        {
            return this.db.Commits
                .Where(x => x.CreatorId == userId)
                .Select(x => new CommitsViewModel()
                {
                    Id = x.Id,
                    CreatedOn = x.CreatedOn,
                    RepoName = x.Repository.Name,
                    Description = x.Description,
                }).ToList();
        }

        public bool IsUserACreator(string userId, string commitId)
        {
            var entity = this.db.Commits.FirstOrDefault(x => x.Id == commitId);

            if (entity == null || entity.CreatorId != userId) return false;
            else return true;
        }
    }
}

