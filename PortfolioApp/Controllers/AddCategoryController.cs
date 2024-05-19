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

        public AddCategoryController(ApplicationDbContext app)
        {
            _context = app;
        }
        public IActionResult Index()
        {
            return View("/Views/Admin/AddCategory.cshtml", new CreateCategory());
        }
        [HttpPost]
        public IActionResult Create(CreateCategory model)
        {
            if (ModelState.IsValid)
            {
                _context.ProjectCategories.Add(new ProjectCategory() { Id = Guid.NewGuid(), Title = model.Title});
                _context.SaveChanges();
                return RedirectToAction("Index", "Account");
            }
            else return View("/Views/Admin/AddCategory.cshtml", model);
        }
    }
}
