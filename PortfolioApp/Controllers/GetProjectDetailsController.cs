using Azure.Core.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Data;
using Projects.Data;

namespace PortfolioApp.Controllers
{
    public class GetProjectDetailsController : Controller
    {
        public ApplicationDbContext _context;

        public GetProjectDetailsController(ApplicationDbContext app)
        {
            _context = app;
        }
        [HttpGet]
        public IActionResult Get(Guid Id)
        {
            Project? project = _context.Projects.Find(Id);
            if (project == null)
            {
                return NotFound();
            }
            else
            {
                project.Images = _context.Images.Where(i => i.ProjectId == project.Id).Include(i => i.Project).ToList();
                return PartialView("/Views/Shared/ProjectDetail.cshtml", project);
            }
        }
    }
}
