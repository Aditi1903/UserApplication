using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UserApplication.Models
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int CourseId { get; set; }
        [Display(Name ="Course Name")]
        public string CourseName { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<SubjectInCourse> SubjectInCourses { get; set; }
    }
}