using System;
using System.Globalization;

namespace Git.ViewModels.Commits
{
    public class CommitsViewModel
    {
        public string Id { get; set; }

        public string RepoName { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedOnToString => this.CreatedOn.ToString(CultureInfo.InvariantCulture);

        public string Description { get; set; }
    }
}
