using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Common.Web.Entities
{
    public class PlaceQualification
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}")]
        public DateTime Date { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}")]
        public DateTime DateLocal => Date.ToLocalTime();

        [JsonIgnore]
        public Itemplace Restaurant { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        public float Score { get; set; }

        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }
    }

}
