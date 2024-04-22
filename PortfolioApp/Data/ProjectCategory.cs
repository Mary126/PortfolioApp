using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Projects.Data
{
    public class ProjectCategory
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }

        private readonly ILazyLoader _lazyLoader;

        public ProjectCategory()
        {
        }

        public ProjectCategory(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }

        private List<Project> _projects;

        public List<Project> Projects
        {
            get => _lazyLoader.Load(this, ref _projects);
            set => _projects = value;
        }
    }
}
