using Resume.Models;
namespace Resume.ViewModels
{
    public class StorageViewModel : BaseViewModel
    {
        public IList<StorageFile> Files { get; set; }

        public IFormFile NewFile { get; set; }

        public string SelectFileName { get; set; }
    }
}