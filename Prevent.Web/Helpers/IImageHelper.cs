using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Prevent.Web.Helpers
{
    public interface IImageHelper
    {
        Task<string> UploadImageAsync(IFormFile imageFile, string folder);
    }

}
