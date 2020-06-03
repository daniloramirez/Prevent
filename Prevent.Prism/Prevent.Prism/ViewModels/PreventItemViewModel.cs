using Prism.Commands;
using Prism.Navigation;
using Prevent.Common.Models;
using Prevent.Prism.Views;

namespace Prevent.Prism.ViewModels
{
    public class PreventItemViewModel : PreventResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectItemCommand;

        public PreventItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectItemCommand => _selectItemCommand ?? (_selectItemCommand = new DelegateCommand(SelectItemAsync));

        private async void SelectItemAsync()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { "prevent", this }
            };
            await _navigationService.NavigateAsync(nameof(ItemDetailPage), parameters);
        }
    }
}
