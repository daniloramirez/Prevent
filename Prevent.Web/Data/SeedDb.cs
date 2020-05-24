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
            var Consejo = CheckPreventType("Consejo");
            var Guia = CheckPreventType("Guía de información");
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
                    Title = "CONSUMO DE ALCOHOL",
                    Description = "Cuanta mayor cantidad de alcohol consumas, mayor riesgo de padecer cáncer de mama tienes.",
                    Date = DateTime.UtcNow,
                    PreventTypeId = Consejo.Id,
                    PreventType = Consejo,
                    User = Admin
                });

                _dataContext.Prevents.Add(new PreventEntity
                {
                    Title = "CONTROLAR EL PESO",
                    Description = "La gordura y la obesidad aumentan el riesgo de padecer cáncer de mama y más todavía cuando ocurren a mayor edad, particularmente después de la menopausia.",
                    Date = DateTime.UtcNow,
                    PreventTypeId = Guia.Id,
                    PreventType = Guia,
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