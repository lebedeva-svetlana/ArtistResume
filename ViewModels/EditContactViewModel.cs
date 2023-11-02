using Resume.Models;

namespace Resume.ViewModels
{
    public class EditContactViewModel : BaseViewModel
    {
        public Contact Contact { get; set; }

        public List<Social> Socials { get; set; }
    }
}