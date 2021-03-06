﻿using Common.Web.Entities;
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
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace FoodOrderApp.Web.Controllers
{
    public class CategoriesController : Controller
    {

        private readonly IBlobHelper _blobHelper;
        private readonly ICategoryConverterHelper _categoryconverterHelper;
        private readonly CategoryDataService _categoryDataService;
        private readonly Datacontext _context;
        private string imageUrl;
        IFirebaseClient client;
        public CategoriesController(IBlobHelper blobHelper, ICategoryConverterHelper categoryconverterHelper, CategoryDataService categoryDataService, Datacontext context)
        {
            _blobHelper = blobHelper;
            _categoryconverterHelper = categoryconverterHelper;
            _categoryDataService = categoryDataService;
            _context = context;
        }
        public IActionResult Index()
        {
            client = new FireSharp.FirebaseClient(_context.GetFirebaseConnection());
            FirebaseResponse response = client.Get("Categories");
            dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);
            var list = new List<Category>();
            foreach (var item in data)
            {
                list.Add(JsonConvert.DeserializeObject<Category>(((JProperty)item).Value.ToString()));
            }
            return View(list);

        }

        public IActionResult Create()
        {
            CategoryViewModel model = new CategoryViewModel();
            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Create(CategoryViewModel model)
        {

            if (ModelState.IsValid)
            {
                var checkName = await _categoryDataService.CheckName(model.CategoryName);
                if (checkName.Count > 0)
                {
                    ModelState.AddModelError(string.Empty, "This name already exist");
                }
                else
                {
                    imageUrl = await Task.Run(() => _blobHelper.UploadBlobAsync(model.ImageFile, "localCategories", "categories"));
                    try
                    {
                        Category category = _categoryconverterHelper.ToCategory(model, imageUrl);
                        AddStudentToFirebase(category);
                        ModelState.AddModelError(string.Empty, "Added Successfully");
                        return RedirectToAction(nameof(Index));
                    }

                    catch (Exception exception)
                    {
                        ModelState.AddModelError(string.Empty, exception.Message);

                    }
                }

                   
            }

            return View(model);
        }


        private void AddStudentToFirebase(Category category)
        {
            client = new FireSharp.FirebaseClient(_context.GetFirebaseConnection());
            var data = category;
            PushResponse response = client.Push("Categories/", data);
            data.CategoryID = response.Result.name;
            SetResponse setResponse = client.Set("Categories/" + data.CategoryID, data);
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            client = new FireSharp.FirebaseClient(_context.GetFirebaseConnection());
            FirebaseResponse response = client.Get("Categories/" + id);
            Category category = JsonConvert.DeserializeObject<Category>(response.Body);
            if (category == null)
            {
                return NotFound();
            }
            CategoryViewModel model = _categoryconverterHelper.ToCategoryViewModel(category);
            return View(model);

        }
        [HttpPost]
        
        public async Task<IActionResult> Edit(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {

                var checkName = await _categoryDataService.CheckName(model.CategoryName);
                if (checkName.Count > 1)
                {
                    ModelState.AddModelError(string.Empty, "This name already exist");
                }
                else
                {
                    string imageUrl = model.ImageUrl;
                    if (model.ImageFile != null)
                    {
                        imageUrl = await Task.Run(() => _blobHelper.UploadBlobAsync(model.ImageFile, "localCategories", "categories"));
                    }

                    try
                    {
                        client = new FireSharp.FirebaseClient(_context.GetFirebaseConnection());
                        Category category = _categoryconverterHelper.ToCategory(model, imageUrl);
                        SetResponse response = client.Set("Categories/" + category.CategoryID, category);
                        ModelState.AddModelError(string.Empty, "Edited Successfully");
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception exception)
                    {
                        ModelState.AddModelError(string.Empty, exception.Message);
                    }
                }
                    
            }

            return View(model);

            
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
                FirebaseResponse response = client.Delete("Categories/" + id);
              
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return RedirectToAction(nameof(Index));

        }
    }
}
