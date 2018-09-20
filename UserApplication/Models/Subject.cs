using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UserApplication.Models
{
    public class Subject
    {
        public int SubjectId { get; set; }
        [Display(Name ="Subject Name")]
        public string SubjectName { get; set; }
    }
}