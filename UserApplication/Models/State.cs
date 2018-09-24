using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UserApplication.Models
{
    public class State 
    {
        //public State()
        //{
        //    this.Addresses = new HashSet<Address>();
        //    this.Cities = new HashSet<City>();
        //}
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StateId { get; set; }
        [Display(Name ="State Name")]
        public string StateName { get; set; }

      
        public int CountryId { get; set; }
        //[ForeignKey("CountryId")]    
        public virtual Country Country { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<City> Cities { get; set; }

    }
}