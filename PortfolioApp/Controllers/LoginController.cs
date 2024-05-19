using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace PortfolioApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<RegisterController> _logger;
        public LoginController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<RegisterController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }
        public IActionResult Index()
        {

            ViewBag.Error = "";
            return View("/Views/Admin/Login.cshtml");
        }
        public async Task<IActionResult> Login(string Username, string Password)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(Username, Password, true, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("ProjectsPage", "Projects");
                }
                else
                {
                    ViewBag.Error = "Username or password incorrect";
                    return View("/Views/Admin/Login.cshtml");
                }
            }
            else
            {
                ViewBag.Error = "Form is incorrect";
                return View("/Views/Admin/Login.cshtml");
            }
        }
    }
}
