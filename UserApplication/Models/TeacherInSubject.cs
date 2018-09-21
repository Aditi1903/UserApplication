using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UserApplication.Models
{
    public class TeacherInSubject
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeacherInSubjectId { get; set; }
        public int UserId { get; set; }
        public int SubjectId { get; set; }
    }
}