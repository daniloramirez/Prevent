using Prism.Commands;
using Prism.Navigation;
using System.Text.RegularExpressions;
using Prevent.Common.Models;
using Prevent.Common.Services;
using Prevent.Prism.ViewModels;

namespace Prevent.Prism.ViewModels
{
    public class AdvicePageViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;
        private PreventTypeResponse _prevent;
        private DelegateCommand _checkPreventTypeCommand;

        public AdvicePageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _apiService = apiService;
            Title = "Advices";
        }

        public PreventTypeResponse PreventType
        {
            get => _prevent;
            set => SetProperty(ref _prevent, value);
        }

        public string PreventTypeId { get; set; }

        public DelegateCommand CheckPreventTypeCommand => _checkPreventTypeCommand ?? (_checkPreventTypeCommand = new DelegateCommand(CheckPreventAsync));

        private async void CheckPreventAsync()
        {
            if (string.IsNullOrEmpty(PreventTypeId))
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter a id.",
                    "Accept");
                return;
            }

            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.GetPreventAsync(PreventTypeId, url, "api", "/Prevents");
            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            PreventType = (PreventTypeResponse)response.Result;
        }
    }
}
