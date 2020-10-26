using System;
using System.ComponentModel.DataAnnotations;

namespace Common.Web.Entities
{
    public class Category
    {
        public string CategoryID { get; set; }
        [MaxLength(50)]
        [Required]
        public string CategoryName { get; set; }
        public string CategoryPoster { get; set; }
        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        
        
    }
}
