using Prevent.Common.Models;
using Prevent.Common.Services;
using Prism.Commands;
using Prism.Navigation;

namespace Prevent.Prism.ViewModels
{
    public class AdvicePageViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;
        private PreventTypeResponse _prevent;
        private bool _isRunning;
        private DelegateCommand _checkPreventTypeCommand;
        public string PreventTypeId { get; set; }

        public AdvicePageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _apiService = apiService;
            Title = "Advices";
            PreventTypeId = "1";
            CheckPreventAsync();
        }
        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }
        public PreventTypeResponse PreventType
        {
            get => _prevent;
            set => SetProperty(ref _prevent, value);
        }        

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

            IsRunning = true;
            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.GetPreventAsync(PreventTypeId, url, "api", "/Prevents");
            IsRunning = false;
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
