﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Common.Web.Entities
{
    public class Itemplace
    {
        public string Id { get; set; }
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
        [MaxLength(100)]
        [Required]
        public string Address { get; set; }
       
        public Category Category { get; set; }
        

        [Display(Name = "Image")]
        public string ImageFullPath { get; set; }
        public ICollection<PlaceQualification> Qualifications { get; set; }

        [DisplayName("Product Qualifications")]
        public int RestaurantQualifications => Qualifications == null ? 0 : Qualifications.Count;

        [DisplayFormat(DataFormatString = "{0:N2}")]
        public float Qualification => Qualifications == null || Qualifications.Count == 0 ? 0 : Qualifications.Average(q => q.Score);

    }
}
