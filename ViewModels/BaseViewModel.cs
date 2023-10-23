using Resume.Models;

namespace Resume.ViewModels
{
    public class BaseViewModel
    {
        public string AuthorName { get; set; }

        public IList<Social> Socials { get; set; }

        public CultureViewModel CultureViewModel { get; set; }
    }
}