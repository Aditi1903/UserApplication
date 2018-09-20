using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UserApplication.Models
{
    public class State
    {
        public int StateId { get; set; }
        [Display(Name ="State Name")]
        public string StateName { get; set; }
        public int CountryId { get; set; }
    }
}