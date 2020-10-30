using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FoodOrderApp.Web.Helpers
{
    public interface IBlobHelper
    {
        Task<string> UploadBlobAsync(IFormFile file, string folderName, string container);

    }
}
