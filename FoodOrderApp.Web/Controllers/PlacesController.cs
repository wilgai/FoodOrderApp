using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Web.Entities;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using FoodOrderApp.Web.Data;
using FoodOrderApp.Web.Helpers;
using FoodOrderApp.Web.Models;
using FoodOrderApp.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FoodOrderApp.Web.Controllers
{
    public class PlacesController : Controller
    {
        private readonly IBlobHelper _blobHelper;
        private readonly IItemplaceConverter _iItemplaceConverter;
        private readonly ICategoryCombos _categorycombos;
        private readonly ItemPlaceDataService _itemPlaceDataService;
        private readonly Datacontext _context;
        private string imageUrl;

        IFirebaseClient client;
        public PlacesController(IBlobHelper blobHelper, IItemplaceConverter iItemplaceConverter, ICategoryCombos categorycombos, ItemPlaceDataService itemPlaceDataService, Datacontext context)
        {
            _blobHelper = blobHelper;
            _iItemplaceConverter = iItemplaceConverter;
            _categorycombos = categorycombos;
            _itemPlaceDataService = itemPlaceDataService;
            _context = context;
        }
       

        public IActionResult Index()
        {
            client = new FireSharp.FirebaseClient(_context.GetFirebaseConnection());
            FirebaseResponse response = client.Get("Itemplaces");
            dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);
            var list = new List<Itemplace>();
            foreach (var item in data)
            {
                list.Add(JsonConvert.DeserializeObject<Itemplace>(((JProperty)item).Value.ToString()));
            }
            return View(list);
        }

        public IActionResult Create()
        {
            ItemplaceViewModel model = new ItemplaceViewModel 
            {
                Categories = _categorycombos.GetComboCategories()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ItemplaceViewModel model)
        {

            if (ModelState.IsValid)
            {
                var  checkName = await _itemPlaceDataService.CheckName(model.Name);

               if (checkName.Count >0)
                {
                    ModelState.AddModelError(string.Empty, "This name already exist");
                }
               else
                {
                    imageUrl = await Task.Run(() => _blobHelper.UploadBlobAsync(model.ImageFile, "localPlaces", "places"));
                    try
                    {
                        Itemplace itemplace = _iItemplaceConverter.ToItemplace(model, imageUrl);
                        AddPlaceToFirebase(itemplace);
                        ModelState.AddModelError(string.Empty, "Added Successfully");
                        return RedirectToAction(nameof(Index));
                    }

                    catch (Exception exception)
                    {
                        ModelState.AddModelError(string.Empty, exception.Message);

                    }

                }
 
            }
            model.Categories = _categorycombos.GetComboCategories();
            return View(model);
        }

        private void AddPlaceToFirebase(Itemplace itemplace)
        {
            client = new FireSharp.FirebaseClient(_context.GetFirebaseConnection());
            var data = itemplace;
            PushResponse response = client.Push("Itemplaces/", data);
            data.Id = response.Result.name;
            SetResponse setResponse = client.Set("Itemplaces/" + data.Id, data);
        }

        
        [HttpGet]
       
        public ActionResult Edit(string id)
        {
            


            if (id == null)
            {
                return NotFound();
            }
            client = new FireSharp.FirebaseClient(_context.GetFirebaseConnection());
            FirebaseResponse response = client.Get("Itemplaces/" + id);
            Itemplace itemplace = JsonConvert.DeserializeObject<Itemplace>(response.Body);
            if (itemplace == null)
            {
                return NotFound();
            }
            ItemplaceViewModel model = _iItemplaceConverter.ToItemplaceViewModel(itemplace);
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(ItemplaceViewModel model)
        {

            if (ModelState.IsValid)
            {
                var checkName = await _itemPlaceDataService.CheckName(model.Name);

                if (checkName.Count > 1)
                {
                    ModelState.AddModelError(string.Empty, "This name already exist");
                }
                else
                {
                    string imageUrl = model.ImageFullPath;
                    if (model.ImageFile != null)
                    {
                        imageUrl = await Task.Run(() => _blobHelper.UploadBlobAsync(model.ImageFile, "localPlaces", "places"));
                    }

                    try
                    {
                        client = new FireSharp.FirebaseClient(_context.GetFirebaseConnection());
                        Itemplace itemplace = _iItemplaceConverter.ToItemplace(model, imageUrl);
                        SetResponse response = client.Set("Itemplaces/" + itemplace.Id, itemplace);
                        ModelState.AddModelError(string.Empty, "Edited Successfully");
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception exception)
                    {
                        ModelState.AddModelError(string.Empty, exception.Message);
                    }

                }

            }
            model.Categories = _categorycombos.GetComboCategories();
            return View(model);
        }

        [HttpGet]
        public ActionResult Details(string id)
        {
            client = new FireSharp.FirebaseClient(_context.GetFirebaseConnection());
            FirebaseResponse response = client.Get("Itemplaces/" + id);
            Itemplace data = JsonConvert.DeserializeObject<Itemplace>(response.Body);
            return View(data);
        }

        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                client = new FireSharp.FirebaseClient(_context.GetFirebaseConnection());
                FirebaseResponse response = client.Delete("Itemplaces/" + id);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return RedirectToAction(nameof(Index));

        }
    }
}
