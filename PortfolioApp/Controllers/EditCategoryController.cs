using Microsoft.AspNetCore.Mvc;
using PortfolioApp.Data;
using Projects.Data;

namespace PortfolioApp.Controllers
{
    public class EditCategoryController : Controller
    {
        public ApplicationDbContext _context;

        public EditCategoryController(ApplicationDbContext app)
        {
            _context = app;
        }
        public IActionResult Index(Guid Id)
        {
            var category = _context.ProjectCategories.Find(Id);
            return View("/Views/Admin/EditCategory.cshtml", category);
        }
        public IActionResult Edit(String Title, Guid Id)
        {
            var categoryObject = _context.ProjectCategories.Find(Id);
            if (categoryObject is null) return NotFound();
            categoryObject.Title = Title;
            _context.ProjectCategories.Update(categoryObject);
            _context.SaveChanges();
            return RedirectToAction("Index", "Account");
        }
    }
}
