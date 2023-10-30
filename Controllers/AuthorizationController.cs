using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Resume.Models;
using Resume.ViewModels;

namespace Resume.Controllers
{
    public class AuthorizationController : Controller
    {
        private DatabaseContext _context;
        private readonly IStringLocalizer<AuthorizationController> _localizer;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthorizationController(DatabaseContext context, SignInManager<User> signInManager, IStringLocalizer<AuthorizationController> localizer, UserManager<User> userManager)
        {
            _context = context;
            _localizer = localizer;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AuthorizationViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(viewModel.Email, viewModel.Password, true, false);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(HomeController.Portfolio), "Home");
            }
            else
            {
                ModelState.AddModelError("", _localizer["ErrorMessage"]);
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Portfolio), "Home");
        }
    }
}