using Prevent.Common.Models;
using Prism.Navigation;

namespace Prevent.Prism.ViewModels
{
    public class ItemDetailPageViewModel : ViewModelBase
    {
        private PreventResponse _prevent;
        public ItemDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
        }
        public PreventResponse Prevent
        {
            get => _prevent;
            set => SetProperty(ref _prevent, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("prevent"))
            {
                Prevent = parameters.GetValue<PreventResponse>("prevent");
                if(Prevent.Id == 1)
                {
                    Prevent.File = "alcohol";
                }
            }
        }
    }
}
