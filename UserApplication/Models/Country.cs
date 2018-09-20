using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UserApplication.Models
{
    public class Country
    {
        public int ContryId { get; set; }
        [Display(Name="Country Name")]
        public string CountryName { get; set; }
    }
}