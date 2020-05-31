using Prevent.Common.Services;
using Prevent.Prism.ViewModels;
using Prevent.Prism.Views;
using Prism;
using Prism.Ioc;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Prevent.Prism
{
    public partial class App
    {
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            //SyncfusionLicenseProvider.RegisterLicense("MjQyODIyQDMxMzgyZTMxMmUzMFFKdGZMbEdUc1NBSC9Xd1hub0cvd1RGbmpmN0c1YVc0eE5ZUVFjODZoeVE9");
            InitializeComponent();

            await NavigationService.NavigateAsync("/PreventMasterDetailPage/NavigationPage/HomePage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
            containerRegistry.RegisterForNavigation<PreventMasterDetailPage, PreventMasterDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<AdvicePage, AdvicePageViewModel>();
            containerRegistry.RegisterForNavigation<GuidePage, GuidePageViewModel>();
            containerRegistry.RegisterForNavigation<ModifyUserPage, ModifyUserPageViewModel>();
            containerRegistry.RegisterForNavigation<BackgroundPage, BackgroundPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
        }
    }
}
