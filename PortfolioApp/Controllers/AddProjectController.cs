using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PortfolioApp.Data;
using Projects.Data;

namespace PortfolioApp.Controllers
{
    public class AddProjectController : Controller
    {
        public ApplicationDbContext _context;
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly ILogger<AddProjectController> logger;

        public AddProjectController(ApplicationDbContext app, IWebHostEnvironment hostEnvironment, ILogger<AddProjectController> logger)
        {
            _context = app;
            hostingEnvironment = hostEnvironment;
            this.logger = logger;
        }
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Categories = new SelectList(_context.ProjectCategories.ToList(), "Id", "Title");
            var Project = new Project();
            return View("/Views/Admin/AddProject.cshtml", Project);
        }
        [HttpPost]
        public IActionResult Create(IFormFile? fileObject, Project model, IEnumerable<IFormFile> Images)
        {
            if (ModelState.IsValid)
            {
                var uploadDirectory = Path.Combine(hostingEnvironment.WebRootPath, "Database/Files/");
                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }
                if (fileObject != null) {
                    var projectName = GetUniqueFileName(fileObject.FileName);
                    uploadDirectory = Path.Combine(uploadDirectory, projectName);
                    fileObject.CopyTo(new FileStream(uploadDirectory, FileMode.Create));
                    model.FileUrl = "/Database/Images/" + projectName;
                }
                model.Id = Guid.NewGuid();
                List<Image> imagesToUpload = new List<Image>();
                foreach (var imageFile in Images)
                {
                    var uploadImageDirectory = Path.Combine(hostingEnvironment.WebRootPath, "Database/Images/");
                    if (!Directory.Exists(uploadImageDirectory)) Directory.CreateDirectory(uploadImageDirectory);
                    var imageName = GetUniqueFileName(imageFile.FileName);
                    uploadImageDirectory = Path.Combine(uploadImageDirectory, imageName);
                    imageFile.CopyTo(new FileStream(uploadImageDirectory, FileMode.Create));
                    var image = new Image() { Id = Guid.NewGuid(), FileUrl = "/Database/Images/" + imageName};
                    imagesToUpload.Add(image);
                }
                model.Images = imagesToUpload;
                _context.Projects.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index", "Account");
            }
            else
            {
                ViewBag.Categories = new SelectList(_context.ProjectCategories.ToList(), "Id", "Title");
                return View("/Views/Admin/AddProject.cshtml", model);
            }
                
        }
        private string GetUniqueFileName(string fileName)
        {
            return Guid.NewGuid().ToString().Substring(0, 6)
                      + Path.GetExtension(fileName);
        }
    }
}
