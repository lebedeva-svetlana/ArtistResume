using Markdig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resume.Models;
using Resume.ViewModels;

namespace Resume.Controllers
{
    public class HomeController : BaseLanguageController
    {
        // TODO: Добавить кеширование
        public HomeController(DatabaseContext context) : base(context)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Portfolio()
        {
            PortfolioViewModel viewModel = new()
            {
                Works = await _context.Works.ToListAsync()
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Work(int id)
        {
            WorkViewModel viewModel = new()
            {
                Work = await _context.Works.FirstAsync(e => e.Id == id)
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Biography()
        {
            BiographyViewModel viewModel = new();

            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            string markdown = await _context.Biographies.Select(e => e.Markdown).FirstAsync();

            viewModel.HTML = Markdown.ToHtml(markdown, pipeline);

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Contact()
        {
            ContactViewModel viewModel = new()
            {
                Contact = await _context.Contacts.FirstAsync()
            };

            return View(viewModel);
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View();
        //}
    }
}