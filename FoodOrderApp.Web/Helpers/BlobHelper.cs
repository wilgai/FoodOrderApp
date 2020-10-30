
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
namespace FoodOrderApp.Web.Helpers
{
    public class BlobHelper : IBlobHelper
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _configuration;
        public BlobHelper(IHostingEnvironment env, IConfiguration configuration)
        {
            _env = env;
            _configuration = configuration;
        }
        private string link;
        public async Task<string> UploadBlobAsync(IFormFile file,string folderName,string firebaseContainer)
        {
            string apiKey = _configuration["FirebaseConfig:ApiKey"];
            string bucket = _configuration["FirebaseConfig:Bucket"];
            string authEmail = _configuration["FirebaseConfig:AuthEmail"];
            string authPassword = _configuration["FirebaseConfig:AuthPassword"];

            var auth = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
            var a = await auth.SignInWithEmailAndPasswordAsync(authEmail, authPassword);
            var cancelation = new CancellationTokenSource();

            FileStream fs = null;
            var fileupload = file;
            if (fileupload != null)
            {
                //string LocalFirebasefolder = "fireBasefiles";
                string path = Path.Combine(_env.WebRootPath, $"images/fireBasefiles/{folderName}");
                if (Directory.Exists(path))
                {
                    using (fs = new FileStream(Path.Combine(path, fileupload.FileName), FileMode.Create))
                    {
                        await fileupload.CopyToAsync(fs);
                    }

                    fs = new FileStream(Path.Combine(path, fileupload.FileName), FileMode.Open);
                }
                else
                {
                    Directory.CreateDirectory(path);
                }
                var task = new FirebaseStorage(
                    bucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true
                    })
                    .Child(firebaseContainer)
                    .Child(fileupload.FileName)
                  .PutAsync(fs, cancelation.Token);
                try
                {
                    link = await task;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception was thrown: {0} ", e);
                }  
            }

            return link;
        }
    }
}
