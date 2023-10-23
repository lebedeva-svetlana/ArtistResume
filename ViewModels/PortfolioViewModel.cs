using Resume.Models;

namespace Resume.ViewModels
{
    public class PortfolioViewModel : BaseViewModel
    {
        public IList<Work> Works { get; set; }
    }
}