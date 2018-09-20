using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UserApplication.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        [Display(Name ="Course Name")]
        public string CourseName { get; set; }
    }
}