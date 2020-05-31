using Prism.Navigation;

namespace Prevent.Prism.ViewModels
{
    public class BackgroundPageViewModel : ViewModelBase
    {
        public BackgroundPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Your Background";
        }
    }
}
