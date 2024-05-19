using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Data;
using Projects.Data;

namespace PortfolioApp.Controllers
{
    public class AccountController : Controller
    {
        public ApplicationDbContext _context;

        public AccountController(ApplicationDbContext app)
        {
            _context = app;
        }
        public async Task<IActionResult> Index()
        {
            if (_context is null) return NotFound();
            IEnumerable<ProjectCategory> categories = await _context.ProjectCategories.ToListAsync();
            foreach (ProjectCategory category in categories)
            {
                category.Projects = _context.Projects.Where(p => p.ProjectCategoryId == category.Id).Include(p => p.ProjectCategory).ToList();
            }
            if (categories is null) return NotFound();
            return View("/Views/Admin/AccountPage.cshtml", categories);
        }
        public async Task<IActionResult> DeleteProject(Guid Id)
        {
            var project = await _context.Projects.FindAsync(Id);
            if (project == null) return NotFound();
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeleteCategory(Guid Id)
        {
            var category = await _context.ProjectCategories.FindAsync(Id);
            if (category == null) return NotFound();
            _context.ProjectCategories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
