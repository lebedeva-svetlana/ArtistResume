using Resume.Models;
using System.ComponentModel.DataAnnotations;

namespace Resume.ViewModels
{
    public class StorageViewModel : BaseViewModel
    {
        public IList<StorageFile> Files { get; set; }

        public IFormFile NewFile { get; set; }

        public int SelectFileId { get; set; }

        public string NewFileName { get; set; }
    }
}