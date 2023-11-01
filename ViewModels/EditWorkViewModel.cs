using Resume.Models;

namespace Resume.ViewModels
{
    public class EditWorkViewModel : BaseViewModel
    {
        public int Id { get; set; }

        public string ImageSrc { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public string? Comment { get; set; }
    }
}