using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UserApplication.Models
{
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CityId { get; set; }
        public string CityName { get; set; }

    
        //[Required]
        public int StateId { get; set; }
        //[ForeignKey("StateId")]
        public  State State { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }

    }
}