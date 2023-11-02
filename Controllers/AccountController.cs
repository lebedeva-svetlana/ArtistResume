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
                Works = await _context.Works.Include(e => e.StorageFile).ToListAsync()
            };

            return View(viewModel);
        }

        public async Task<IActionResult> EditWork(int id)
        {
            Work work = await _context.Works.Where(e => e.Id == id).Include(e => e.StorageFile).FirstAsync();
            EditWorkViewModel viewModel = new()
            {
                Id = id,
                Name = work.Name,
                Description = work.Description,
                Comment = work.Comment,
                ImageSrc = $"{id}.{work.StorageFile.Extension}"
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditWork(EditWorkViewModel viewModel)
        {
            Work work = await _context.Works.Where(e => e.Id == viewModel.Id).FirstAsync();

            work.Name = viewModel.Name;
            work.Description = viewModel.Description;
            work.Comment = viewModel.Comment;

            await _context.SaveChangesAsync();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteWork(int id)
        {
            Work work = await _context.Works.Where(e => e.Id == id).FirstAsync();
            _context.Remove(work);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Portfolio), "Account");
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
            EditContactViewModel viewModel = new()
            {
                Contact = await _context.Contacts.FirstAsync(),
                Socials = await _context.Socials.ToListAsync()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveContacts(EditContactViewModel viewModel)
        {
            Contact? contact = await _context.Contacts.FirstAsync();

            if (contact is not null)
            {
                contact.Name = viewModel.Contact.Name;
                contact.TelephoneNumber = viewModel.Contact.TelephoneNumber;
                contact.Email = viewModel.Contact.Email;
            }

            List<Social>? socials = await _context.Socials.OrderBy(e => e.Id).ToListAsync();

            if (socials is not null)
            {
                for (int i = 0; i < viewModel.Socials.Count; ++i)
                {
                    if (socials[i].Url != viewModel.Socials[i].Url)
                    {
                        socials[i].Url = viewModel.Socials[i].Url;
                    }
                }
            }

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

        //#error при наведении во время редактирования изменяется селектед, можно поменять имя другой картинки

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadFile(StorageViewModel viewModel)
        {
            if (viewModel.NewFile is null)
            {
                return RedirectToAction(nameof(Storage), "Account");
            }

            string extension = Path.GetExtension(viewModel.NewFile.FileName);

            StorageFile image = new()
            {
                Name = Path.GetFileNameWithoutExtension(viewModel.NewFile.FileName),
                Extension = extension[1..]
            };

            await _context.StorageFiles.AddAsync(image);
            _context.SaveChanges();

            try
            {
                string fileName = image.Id + extension;
                string path = Path.Combine(_environment.WebRootPath, "images", fileName);

                using FileStream fileStream = new(path, FileMode.Create);
                await viewModel.NewFile.CopyToAsync(fileStream);
            }
            catch
            {
                _context.StorageFiles.Remove(image);
                _context.SaveChanges();
            }

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