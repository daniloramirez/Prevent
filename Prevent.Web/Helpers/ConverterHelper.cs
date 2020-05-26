using System;
using System.Linq;
using Prevent.Common.Models;
using Prevent.Web.Data.Entities;


namespace Prevent.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        public PreventTypeResponse ToPreventResponse(PreventTypeEntity preventEntity)
        {
            return new PreventTypeResponse
            {
                Id = preventEntity.Id,
                Name = preventEntity.Name,
                Prevents = preventEntity.Prevents?.Select(t => new PreventResponse
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Date = t.Date,
                    User = ToUserResponse(t.User),
                    File = t.File
                }).ToList()
            };

        }

        private UserResponse ToUserResponse(UserEntity user)
        {
            if (user == null)
            {
                return null;
            }
            return new UserResponse
            {
                Address = user.Address,
                Document = user.Document,
                Email = user.Email,
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                PicturePath = user.PicturePath,
                UserType = user.UserType
            };
        }
    }
}
