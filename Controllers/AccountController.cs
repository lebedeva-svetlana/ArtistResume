using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resume.Models;
using Resume.ViewModels;

namespace Resume.Controllers
{
    [Authorize]
    public class AccountController : BaseLanguageController
    {
        private IWebHostEnvironment _environment;

        public AccountController(DatabaseContext context, IWebHostEnvironment environment) : base(context)
        {
            _environment = environment;
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

        [HttpGet]
        public async Task<IActionResult> Biography()
        {
            EditBiographyViewModel viewModel = new()
            {
                Markdown = await _context.Biographies.Select(e => e.Markdown).FirstAsync()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveBiography(EditBiographyViewModel viewModel)
        {
            Biography biography = await _context.Biographies.FirstAsync();
            biography.Markdown = viewModel.Markdown;
            _context.SaveChanges();

            return RedirectToAction(nameof(Biography), "Account");
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(ContactViewModel viewModel)
        {
            Contact? contact = await _context.Contacts.FirstAsync();

            contact.Name = viewModel.Contact.Name;
            contact.TelephoneNumber = viewModel.Contact.TelephoneNumber;
            contact.Email = viewModel.Contact.Email;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Contact), "Account");
        }

        [HttpGet]
        public async Task<IActionResult> Storage()
        {
            StorageViewModel viewModel = new()
            {
                Files = await _context.StorageFiles.ToListAsync()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteFile(StorageViewModel viewModel)
        {
            StorageFile file = await _context.StorageFiles.FirstAsync(e => e.Id == viewModel.SelectFileId);

            string path = Path.Combine(_environment.WebRootPath, "images", $"{file.Id}.{file.Extension}");
            System.IO.File.Delete(path);

            _context.Remove(file);
            _context.SaveChanges();

            return RedirectToAction(nameof(Storage), "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFileName(StorageViewModel viewModel)
        {
            if (viewModel.SelectFileId == 0 || string.IsNullOrEmpty(viewModel.NewFileName))
            {
                return RedirectToAction(nameof(Storage), "Account");
            }

            StorageFile file = await _context.StorageFiles.FirstAsync(e => e.Id == viewModel.SelectFileId);
            file.Name = viewModel.NewFileName;
            _context.SaveChanges();

            return RedirectToAction(nameof(Storage), "Account");
        }
    }
}