using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UserApplication.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        [Display(Name ="Role Name")]
        public string RoleName { get; set; }

    }
}