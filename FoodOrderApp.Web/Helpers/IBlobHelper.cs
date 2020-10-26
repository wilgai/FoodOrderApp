using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FoodOrderApp.Web.Helpers
{
    public interface IBlobHelper
    {
        Task<string> UploadBlobAsync(FileStream file, string containerName);

    }
}
