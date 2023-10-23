using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Resume.Models;
using Resume.ViewModels;
using System.Security.Claims;

namespace Resume.Controllers
{
    public class AuthorizationController : Controller
    {
        private DatabaseContext _context;
        private readonly IStringLocalizer<AuthorizationController> _localizer;
        //private readonly UserManager<User> _userManager;
        //private readonly SignInManager<User> _signInManager;

        //public AuthorizationController(DatabaseContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        //{
        //    _context = context;
        //    _userManager = userManager;
        //    _signInManager = signInManager;
        //}

        public AuthorizationController(DatabaseContext context, IStringLocalizer<AuthorizationController> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        //public IActionResult Index()
        //{
        //    //ViewData["AuthorName"] = _context.Contacts.Select(e => e.Name).FirstOrDefault();

        //    //_works = _context.Works.ToList();

        //    return View();
        //}
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

            User? user = await _context.Users.FirstOrDefaultAsync(e => e.Email == viewModel.Email);

            //if (user is null || !await _userManager.CheckPasswordAsync(user, viewModel.Password))
            //{
            // TODO: Шифрование
            if (user is null || user.Password != viewModel.Password)
            {
                ModelState.AddModelError("", _localizer["ErrorMessage"]);
                return View();
            }

            ClaimsIdentity identity = new(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            ClaimsPrincipal principal = new(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction(nameof(HomeController.Portfolio), "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Portfolio), "Home");
        }
    }
}