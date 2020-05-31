using Prevent.Common.Models;
using System.Threading.Tasks;


namespace Prevent.Common.Services
{
    public interface IApiService
    {
        Task<Response> GetPreventAsync(string preventType, string urlBase, string servicePrefix, string controller);
    }
}
