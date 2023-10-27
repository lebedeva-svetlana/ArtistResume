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
        public AccountController(DatabaseContext context) : base(context)
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
            StorageFile file = await _context.StorageFiles.FirstAsync(e => e.Name == viewModel.SelectFileName);
            _context.Remove(file);
            _context.SaveChanges();

            return RedirectToAction(nameof(Storage), "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFileName(StorageViewModel viewModel)
        {
            //StorageFile file = await _context.StorageFiles.FirstAsync(e => e.Path == viewModel.SelectFilePath);
            //_context.Remove(file);
            //_context.SaveChanges();

            return RedirectToAction(nameof(Storage), "Account");
        }
    }
}