using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UserApplication.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }


        [Display(Name = "First Name")]
        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; }


        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "This field cannot be null")]
        public string Gender { get; set; }


        [Required(ErrorMessage = "Please select Hobbies")]
        public string Hobbies { get; set; }

        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[$@$!%*?&])[A-Za-z\\d$@$!%*?&]{6,}",
            ErrorMessage = "Password should be of minimum 6 characters with at least 1 Uppercase Alphabet, 1 Lowercase Alphabet, 1 Number and 1 Special Character")]

        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Please enter valid DOB")]
        public DateTime DOB { get; set; }


        [Display(Name = "Role")]
        [Required]
        public int RoleId { get; set; }
        //[ForeignKey("RoleId")]
        public virtual Role Role { get; set; }


        [Display(Name = "Course")]
        [Required]
        public int CourseId { get; set; }
        //[ForeignKey("CourseId")]
        public virtual Course Course { get; set; }


        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<TeacherInSubject> TeacherInSubjects { get; set; }
        public virtual ICollection<UserInRole> UserInRoles { get; set; }

    }
}