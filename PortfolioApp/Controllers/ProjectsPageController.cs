using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Data;
using Projects.Data;

namespace PortfolioApp.Controllers
{
    public class ProjectsController : Controller
    {
        public ApplicationDbContext _context;

        public ProjectsController(ApplicationDbContext app)
        {
            _context = app;
        }
        public async Task<IActionResult> ProjectsPage()
        {
            if (_context is null) throw new InvalidDataException("Context is null");
            IEnumerable<ProjectCategory> categories = await _context.ProjectCategories.ToListAsync();
            foreach (ProjectCategory category in categories)
            {
                category.Projects = _context.Projects.Where(p => p.ProjectCategoryId == category.Id).Include(p => p.ProjectCategory).ToList();
                foreach (Project project in category.Projects)
                {
                    project.Images = _context.Images.Where(i => i.ProjectId == project.Id).Include(i => i.Project).ToList();
                }
            }
            if (categories is null) return NotFound();
            else return View(categories);
        }
    }
}
