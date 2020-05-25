using Microsoft.AspNetCore.Identity;
using Prevent.Web.Data.Entities;
using Prevent.Web.Models;
using System.Threading.Tasks;

namespace Prevent.Web.Helpers
{
    public interface IUserHelper
    {
        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();

        Task<UserEntity> GetUserByEmailAsync(string email);

        Task<IdentityResult> AddUserAsync(UserEntity user, string password);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(UserEntity user, string roleName);

        Task<bool> IsUserInRoleAsync(UserEntity user, string roleName);
    }
}
