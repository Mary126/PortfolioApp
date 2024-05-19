using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace PortfolioApp.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<RegisterController> _logger;
        public RegisterController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<RegisterController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View("/Views/Admin/Register.cshtml", new IdentityUser());
        }
        public async Task<IActionResult> Register(IdentityUser model, string Password)
        {
            if (ModelState.IsValid)
            {
                var result = await _userManager.CreateAsync(model, Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(model, isPersistent: false);
                    return RedirectToAction("ProjectsPage", "Projects");
                }
                else
                {
                    var message = string.Join(", ", result.Errors.Select(x => "Code " + x.Code + " Description" + x.Description));
                    _logger.LogError(message);
                    return BadRequest();
                }
            }
            else
            {
                return View("/Views/Admin/Register.cshtml", model);
            }
        }
    }
}
