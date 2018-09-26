using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UserApplication.Models
{
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }
        //[Display(Name = "Role Name")]
        public string RoleName { get; set; }
        public virtual ICollection<User> Users {get;set;}
        public virtual ICollection<UserInRole> UserInRoles { get; set; }

    }
}