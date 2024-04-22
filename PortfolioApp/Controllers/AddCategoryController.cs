using Microsoft.AspNetCore.Mvc;
using PortfolioApp.Data;
using Projects.Data;
using System.ComponentModel.DataAnnotations;

namespace PortfolioApp.Controllers
{
    public class CreateCategory
    {
        [Required]
        public string? Title { get; set; }
    }
    public class AddCategoryController : Controller
    {
        public ApplicationDbContext _context;
        private readonly ILogger _logger;

        public AddCategoryController(ApplicationDbContext app, ILogger<AddCategoryController> logger)
        {
            _context = app;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View("/Views/Shared/AddCategory.cshtml", new CreateCategory());
        }
        [HttpPost]
        public IActionResult Create(CreateCategory model)
        {
            if (ModelState.IsValid)
            {

                _context.ProjectCategories.Add(new ProjectCategory() { Id = Guid.NewGuid(), Title = model.Title});
                _context.SaveChanges();
                return RedirectToAction("ProjectsPage", "Projects");
            }
            else return View("/Views/Shared/AddCategory.cshtml", model);
        }
    }
}
