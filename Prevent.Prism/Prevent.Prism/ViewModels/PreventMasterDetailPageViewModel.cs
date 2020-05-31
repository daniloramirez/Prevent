using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Prevent.Common.Models;

namespace Prevent.Prism.ViewModels
{
    public class PreventMasterDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public PreventMasterDetailPageViewModel(INavigationService navigationService) 
            : base(navigationService)
        {
            _navigationService = navigationService;
            LoadMenus();
        }

        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        private void LoadMenus()
        {
            List<Menu> menus = new List<Menu>
            {
                new Menu
                {
                    Icon = "ic_accessibility",
                    PageName = "HomePage",
                    Title = "Home"
                },
                new Menu
                {
                    Icon = "ic_comment",
                    PageName = "AdvicePage",
                    Title = "Advices"
                },
                new Menu
                {
                    Icon = "ic_directions_walk",
                    PageName = "GuidePage",
                    Title = "Guides"
                },
                new Menu
                {
                    Icon = "ic_account_circle",
                    PageName = "ModifyUserPage",
                    Title = "Modify User"
                },
                new Menu
                {
                    Icon = "ic_list",
                    PageName = "BackgroundPage",
                    Title = "Your Background"
                },
                new Menu
                {
                    Icon = "ic_exit_to_app",
                    PageName = "LoginPage",
                    Title = "Log in"
                }
            };

            Menus = new ObservableCollection<MenuItemViewModel>(
                menus.Select(m => new MenuItemViewModel(_navigationService)
                {
                    Icon = m.Icon,
                    PageName = m.PageName,
                    Title = m.Title
                }).ToList());
        }
    }
}
