using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prevent.Common.Enums;
using Prevent.Web.Data.Entities;
using Prevent.Web.Helpers;




namespace Prevent.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;

        public SeedDb(
            DataContext dataContext,
            IUserHelper userHelper
)
        {
            _dataContext = dataContext;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _dataContext.Database.EnsureCreatedAsync();
            await CheckRolesAsync();
            var Consejo = CheckPreventType("Advice");
            var Guia = CheckPreventType("Guide");
            var Admin = await CheckUserAsync("1010", "Danilo", "Ramirez", "daniloramirez0818@gmail.com", "350 634 2747", "Calle Luna Calle Sol", UserType.Admin);
            await CheckUserAsync("2020", "Danilo", "Ramirez", "daniloramirez0818@hotmail.com", "350 634 2747", "Calle Luna Calle Sol", UserType.User);
            await CheckUserAsync("3030", "Danilo", "Ramirez", "daniloramirez187435@correo.itm.edu.co", "350 634 2747", "Calle Luna Calle Sol", UserType.User);
            await CheckPreventAsync(Consejo, Guia, Admin);
        }

        private async Task<UserEntity> CheckUserAsync(
           string document,
           string firstName,
           string lastName,
           string email,
           string phone,
           string address,
           UserType userType)
        {
            var user = await _userHelper.GetUserByEmailAsync(email);
            if (user == null)
            {
                user = new UserEntity
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    UserType = userType
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());
            }

            return user;
        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }

        private async Task CheckPreventAsync(PreventTypeEntity Consejo, PreventTypeEntity Guia, UserEntity Admin)
        {
            if (!_dataContext.Prevents.Any())
            {
                _dataContext.Prevents.Add(new PreventEntity
                {
                    Title = "ALCOHOL CONSUMPTION",
                    Description = "The greater amount of alcohol you consume, the higher risk you have of breast cancer.",
                    Date = DateTime.UtcNow,
                    PreventTypeId = Consejo.Id,
                    PreventType = Consejo,
                    File = "~/images/prevents/11c52af4-1311-47bb-98b3-91a8334f3590.jpg",
                    User = Admin
                });

                _dataContext.Prevents.Add(new PreventEntity
                {
                    Title = "CONTROL WEIGHT",
                    Description = "Fat and obesity increase the risk of breast cancer and even more so when they occur at an older age, particularly after menopause.",
                    Date = DateTime.UtcNow,
                    PreventTypeId = Guia.Id,
                    PreventType = Guia,
                    File = "~/images/prevents/46da3641-08ae-457d-9c2b-ca7d2cd61be7.jpg",
                    User = Admin
                });
                await _dataContext.SaveChangesAsync();
            }
        }

        private PreventTypeEntity CheckPreventType(string Name)
        {
            return new PreventTypeEntity
            {
                Name = Name
            };
        }
    }
}