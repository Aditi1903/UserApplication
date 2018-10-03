using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UserApplication.Models
{
    public class Country
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int CountryId { get; set; }
        [Display(Name="Country Name")]
        public string CountryName { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<State> States { get; set; }
    }
}