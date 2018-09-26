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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubjectInCourseId { get; set; }



        [Required]
        public int SubjectId { get; set; }
        //[ForeignKey("SubjectId")]
        public virtual Subject Subject { get; set; }



        [Required]
        public int CourseId { get; set; }
        //[ForeignKey("CourseId")]
        public virtual Course Course { get; set; }
    }
}