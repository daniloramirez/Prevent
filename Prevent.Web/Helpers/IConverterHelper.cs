using Prevent.Common.Models;
using Prevent.Web.Data.Entities;

namespace Prevent.Web.Helpers
{
    public interface IConverterHelper
    {
        PreventTypeResponse ToPreventResponse(PreventTypeEntity preventEntity);
    }
}
