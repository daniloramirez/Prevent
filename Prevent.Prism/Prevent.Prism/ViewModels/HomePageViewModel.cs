using Prism.Navigation;

namespace Prevent.Prism.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        public HomePageViewModel(INavigationService navigationService) 
            : base(navigationService)
        {
            Title = "Prevent";
        }
    }
}
