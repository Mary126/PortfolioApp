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
            var categories = _context.ProjectCategories.ToList();
            if (categories.Count == 0) return RedirectToAction("Index", "AddCategory");
            ViewBag.Categories = new SelectList(categories, "Id", "Title");
            var Project = new Project();
            return View("/Views/Admin/AddProject.cshtml", Project);
        }
        [HttpPost]
        public IActionResult Create(IFormFile? fileObject, Project model, IEnumerable<IFormFile> Images)
        {
            // Check if the model state is valid
            if (ModelState.IsValid)
            {
                // Set the upload directory
                var uploadDirectory = Path.Combine(hostingEnvironment.WebRootPath, "Database/Files/");

                // Create the upload directory if it doesn't exist
                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }

                // Save the project file if it exists
                if (fileObject != null)
                {
                    var projectName = GetUniqueFileName(fileObject.FileName);
                    uploadDirectory = Path.Combine(uploadDirectory, projectName);
                    fileObject.CopyTo(new FileStream(uploadDirectory, FileMode.Create));
                    model.FileUrl = "/Database/Images/" + projectName;
                }

                // Generate a new ID for the project
                model.Id = Guid.NewGuid();

                // Create a list to store the images to be uploaded
                List<Image> imagesToUpload = [];

                // Save each image file
                foreach (var imageFile in Images)
                {
                    var uploadImageDirectory = Path.Combine(hostingEnvironment.WebRootPath, "Database/Images/");

                    // Create the image directory if it doesn't exist
                    if (!Directory.Exists(uploadImageDirectory))
                    {
                        Directory.CreateDirectory(uploadImageDirectory);
                    }

                    var imageName = GetUniqueFileName(imageFile.FileName);
                    uploadImageDirectory = Path.Combine(uploadImageDirectory, imageName);
                    imageFile.CopyTo(new FileStream(uploadImageDirectory, FileMode.Create));

                    var image = new Image() { Id = Guid.NewGuid(), FileUrl = "/Database/Images/" + imageName };
                    imagesToUpload.Add(image);
                }

                // Set the images for the project
                model.Images = imagesToUpload;

                // Add the project to the database
                _context.Projects.Add(model);
                _context.SaveChanges();

                // Redirect to the "Index" action of the "Account" controller
                return RedirectToAction("Index", "Account");
            }
            else
            {
                // Set the project categories for the view bag
                ViewBag.Categories = new SelectList(_context.ProjectCategories.ToList(), "Id", "Title");

                // Return the view with the project model
                return View("/Views/Admin/AddProject.cshtml", model);
            }
        }
        private string GetUniqueFileName(string fileName)
        {
            // Generate a unique identifier using GUID
            string uniqueId = Guid.NewGuid().ToString()[..6];

            // Get the file extension
            string fileExtension = Path.GetExtension(fileName);

            // Return the unique file name
            return uniqueId + fileExtension;
        }
    }
}
