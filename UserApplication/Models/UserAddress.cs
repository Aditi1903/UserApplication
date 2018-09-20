using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UserApplication.Models
{
    public class UserAddress
    {
        public int UserId { get; set; }
        public int AddressId { get; set; }
        [Display(Name = "Country Name")]
        [Required(ErrorMessage = "Please select your Country name")]
        public string CountryId { get; set; }
        [Display(Name = "State Name")]
        [Required(ErrorMessage = "Please select your State name")]
        public string StateId { get; set; }
        [Display(Name = "City Name")]
        [Required(ErrorMessage = "Please select your City name")]
        public string CityId { get; set; }
        [Display(Name = "Zip code")]
        [Required(ErrorMessage = "Please enter Zipcode")]
        public int Zipcode { get; set; }
    }
}