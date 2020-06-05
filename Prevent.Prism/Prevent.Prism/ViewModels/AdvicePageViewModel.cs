using Prevent.Common.Models;
using Prevent.Common.Services;
using Prism.Navigation;
using System.Collections.Generic;
using System.Linq;

namespace Prevent.Prism.ViewModels
{
    public class AdvicePageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private PreventTypeResponse _prevent;
        private bool _isRunning;
        private List<PreventItemViewModel> _details;
        public string PreventTypeId { get; set; }

        public AdvicePageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Advices";
            PreventTypeId = "1";
            CheckPreventAsync();
        }
        public List<PreventItemViewModel> Details
        {
            get => _details;
            set => SetProperty(ref _details, value);
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

        private async void CheckPreventAsync()
        {

            IsRunning = true;
            string url = App.Current.Resources["UrlAPI"].ToString();
            bool connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert("Error", "Check the internet connection.", "Accept");
                CheckPreventAsync();
                return;
            }
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
            Details = PreventType.Prevents.Select(t => new PreventItemViewModel(_navigationService)
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Date = t.Date,
                File = $"{url}{t.File.Replace("~", "")}",
                User = t.User
            }).ToList();
        }
    }
}
