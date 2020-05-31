using Prism.Navigation;

namespace Prevent.Prism.ViewModels
{
    public class GuidePageViewModel : ViewModelBase
    {
        public GuidePageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Guides";
        }
    }
}
