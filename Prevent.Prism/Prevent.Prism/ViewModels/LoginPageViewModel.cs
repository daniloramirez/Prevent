using Prism.Commands;
using Prism.Navigation;

namespace Prevent.Prism.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private bool _isRunning;
        private bool _isEnabled;
        private string _password;
        //private DelegateCommand _loginCommand;
        //private DelegateCommand _registerCommand;
        public string EmailPlaceHolder;
        public string PasswordPlaceHolder;


        public LoginPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Login";
            IsEnabled = true;
            EmailPlaceHolder = "Enter Email...";
            PasswordPlaceHolder = "Enter Password...";
        }

        //public DelegateCommand LoginCommand => _loginCommand ?? (_loginCommand = new DelegateCommand(LoginAsync));

        //public DelegateCommand RegisterCommand => _registerCommand ?? (_registerCommand = new DelegateCommand(RegisterAsync));

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        public string Email { get; set; }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }
    }
}
