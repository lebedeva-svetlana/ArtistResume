using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Resume.Models;
using Resume.ViewModels;

namespace Resume.Controllers
{
    public class BaseLanguageController : BaseController
    {
        public BaseLanguageController(DatabaseContext context) : base(context)
        {
        }

        [HttpPost]
        public IActionResult SetLanguage(CultureViewModel viewModel, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(viewModel.Culture)),
                new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1)
                }
            );

            return LocalRedirect(returnUrl);
        }
    }
}