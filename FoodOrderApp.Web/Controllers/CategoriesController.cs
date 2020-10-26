using Common.Web.Entities;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using FoodOrderApp.Web.Helpers;
using FoodOrderApp.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;


namespace FoodOrderApp.Web.Controllers
{
    public class CategoriesController : Controller
    {

        private readonly IBlobHelper _blobHelper;
        private readonly ICategoryConverterHelper _categoryconverterHelper;
        private readonly IHostingEnvironment _env;
        private string imageUrl;

        public CategoriesController(IBlobHelper blobHelper, ICategoryConverterHelper categoryconverterHelper, IHostingEnvironment env)
        {

            _blobHelper = blobHelper;
            _categoryconverterHelper = categoryconverterHelper;
            _env = env;

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            CategoryViewModel model = new CategoryViewModel();
            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Create(CategoryViewModel model)
        {
            FileStream fs=null;
            if (ModelState.IsValid)
            {
                var fileupload = model.ImageFile;
                
                if (fileupload != null)
                {
                   
                    string folderName = "fireBasefiles";
                    string path = Path.Combine(_env.WebRootPath, $"images/{folderName}");
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

                    imageUrl = await Task.Run(() => _blobHelper.UploadBlobAsync(fs, fileupload.FileName));
                }



                try
                {
                    Category category = _categoryconverterHelper.ToCategory(model, imageUrl);
                    AddStudentToFirebase(category);
                    //return RedirectToAction(nameof(Index));
                    ModelState.AddModelError(string.Empty, "Added Successfully");

                }

                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);

                }
            }

            return View(model);
        }

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "WGrhhQphcYKSODUzqJaK8Dor4HeMclTDMDBqDVyR",
            BasePath = "https://delevery-cb7c7.firebaseio.com/"
        };
        IFirebaseClient client;

        private void AddStudentToFirebase(Category category)
        {
            client = new FireSharp.FirebaseClient(config);
            var data = category;
            PushResponse response = client.Push("Category/", data);
            data.CategoryID = response.Result.name;
            SetResponse setResponse = client.Set("Category/" + data.CategoryID, data);
        }
    }
}
