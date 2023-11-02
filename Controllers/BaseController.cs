using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Resume.Models;
using Resume.ViewModels;
using System.Data;

namespace Resume.Controllers
{
    public class BaseController : Controller
    {
        protected DatabaseContext _context;

        public BaseController(DatabaseContext context)
        {
            _context = context;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var actionExecutedContext = await next();

            var model = (context.Controller as Controller).ViewData.Model as BaseViewModel;

            model ??= new();

            model.AuthorName = await _context.Contacts.Select(e => e.Name).FirstAsync();
        }
    }
}