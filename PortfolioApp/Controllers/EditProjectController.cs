using Microsoft.AspNetCore.Mvc;
using PortfolioApp.Data;
using Projects.Data;

namespace PortfolioApp.Controllers
{
    public class EditProjectController : Controller
    {
        public ApplicationDbContext _context;

        public EditProjectController(ApplicationDbContext app)
        {
            _context = app;
        }
        public IActionResult Index(Guid Id)
        {
            var project = _context.Projects.Find(Id);
            return View("/Views/Admin/EditProject.cshtml", project);
        }
        public IActionResult Edit(Project project)
        {

            return View();
        }
    }
}
