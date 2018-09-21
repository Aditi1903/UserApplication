using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UserApplication.Models
{
    public class SubjectInCourse
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubjectInCourseId { get; set; }
        public int SubjectId { get; set; }
        public int CourseId { get; set; }
    }
}