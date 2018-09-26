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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeacherInSubjectId { get; set; }



        [Required]
        public int UserId { get; set; }
        //[ForeignKey("UserId")]
        public virtual User User { get; set; }



        [Required]
        public int SubjectId { get; set; }
        //[ForeignKey("SubjectId")]
        public virtual Subject Subject { get; set; }
    }
}