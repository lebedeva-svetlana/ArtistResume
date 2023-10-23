using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resume.Models;

namespace Resume.ViewComponents
{
    public class SocialsViewComponent : ViewComponent
    {
        private readonly DatabaseContext _context;

        public SocialsViewComponent(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Social> socials = await _context.Socials.ToListAsync();
            return View(socials);
        }
    }
}