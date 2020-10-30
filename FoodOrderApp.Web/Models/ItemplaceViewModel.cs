using Common.Web.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace FoodOrderApp.Web.Models
{
    public class ItemplaceViewModel : Itemplace
    {


        [Display(Name = "Category")]
        public string CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        [Display(Name = "Logo")]
        public IFormFile ImageFile { get; set; }

    }
}
