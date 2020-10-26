

using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
namespace FoodOrderApp.Web.Helpers
{
    public class BlobHelper : IBlobHelper
    {

        private static string ApiKey = "AIzaSyAic4O0k-5_YRv6ql1-RroMHib6NeEHQ1I";
        private static string Bucket = "delevery-cb7c7.appspot.com";
        private static string AuthEmail = "delevery1052020@gmail.com";
        private static string AuthPassword = "Seigneur231";
        private string link;

        public async Task<string> UploadBlobAsync(FileStream file, string fileName)
        {
            var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
            var a = await auth.SignInWithEmailAndPasswordAsync(AuthEmail,AuthPassword);

            var cancelation = new CancellationTokenSource();

            var task = new FirebaseStorage(
                Bucket,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true
                })
                .Child("categories")
                .Child(fileName)
              .PutAsync(file, cancelation.Token);

            try
            {
               link= await task;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception was thrown: {0 } ", e);
            }

            return link;
        }

        
    }
}
