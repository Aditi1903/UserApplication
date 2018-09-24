using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UserApplication.Models
{
    public class Subject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int SubjectId { get; set; }
       // [Display(Name ="Subject Name")]
        public string SubjectName { get; set; }
        public virtual ICollection<SubjectInCourse> SubjectInCourses { get; set; }
        public virtual ICollection<TeacherInSubject> TeacherInSubjects { get; set; }

    }
}