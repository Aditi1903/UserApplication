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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AddressId { get; set; }

 
        public int UserId { get; set; }
       // [ForeignKey("UserId")]
        public virtual User User { get; set; }

        //[Display(Name = "Country Name")]
        //[Required(ErrorMessage = "Please select your Country")]
        public int CountryId { get; set; }
        //[ForeignKey("CountryId")]
        public virtual Country Country { get; set; }


        //[Display(Name = "State Name")]
        //[Required(ErrorMessage = "Please select your State")]
        public int StateId { get; set; }
        //[ForeignKey("StateId")]
        public virtual State State { get; set; }

        //[Display(Name = "City Name")]
        //[Required(ErrorMessage = "Please select your City name")]
        public int CityId { get; set; }
        //[ForeignKey("CityId")]
        public virtual City City { get; set; }

        ///[Display(Name = "Zip code")]
        //[Required(ErrorMessage = "Please enter Zipcode")]
        public int Zipcode { get; set; }





    }
}