using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UserApplication.Models
{
    public class Address
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      //  public int UserId { get; set; }
        public int AddressId { get; set; }

        [ForeignKey("CountryId")]
        [Display(Name = "Country Name")]
        [Required(ErrorMessage = "Please select your Country name")]
        public string CountryId { get; set; }

        [ForeignKey("StateId")]
        [Display(Name = "State Name")]
        [Required(ErrorMessage = "Please select your State name")]
        public string StateId { get; set; }

        [ForeignKey("CityId")]
        [Display(Name = "City Name")]
        [Required(ErrorMessage = "Please select your City name")]
        public string CityId { get; set; }
        [Display(Name = "Zip code")]
        [Required(ErrorMessage = "Please enter Zipcode")]
        public int Zipcode { get; set; }


        public virtual Country Countries { get; set; }
        public virtual City Cities { get; set; }
        public virtual State States { get; set; }

    }
}