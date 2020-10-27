using Git.Services;
using Git.ViewModels.Repositories;
using SUS.HTTP;
using SUS.MvcFramework;

namespace Git.Controllers
{
    public class RepositoriesController : Controller
    {
        private readonly IRepositoriesService repositoryService;

        public RepositoriesController(IRepositoriesService repositoryService)
        {
            this.repositoryService = repositoryService;
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var repositoryViewModels = this.repositoryService.GetAllPublicRepositories();
            return this.View(repositoryViewModels);
        }

        public HttpResponse Create()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(RepositoryInputModel model)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrEmpty(model.Name) || model.Name.Length < 3 || model.Name.Length > 10)
            {
                return this.Error("Repository name should be between 3 and 10 characters.");
            }

            if (string.IsNullOrEmpty(model.RepositoryType))
            {
                return this.Error("Privacy setting is required.");
            }

            this.repositoryService.Create(model, this.GetUserId());
            return this.Redirect("/Repositories/All");
        }
    }
}

