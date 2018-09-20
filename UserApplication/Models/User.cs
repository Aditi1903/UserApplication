using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UserApplication.Models
{
    public class User
    {
        [Required]
        public int UserId { get; set; }
        [Display(Name = "First Name")]
        [Required(ErrorMessage ="FirstName is required")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required(ErrorMessage ="LastName is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage ="This field cannot be null")]
        public string Gender { get; set; }
        [Required(ErrorMessage ="Please select Hobbies")]
        public string Hobbies { get; set; }
        [RegularExpression("(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{6,15})$",
        ErrorMessage ="Password should contain atleast one alphabet and one number and should be of minimum 6 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
       // [Required]
        // public string ConfirmPassword { get; set; }
        [Required(ErrorMessage ="Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage="Please enter valid DOB")]
        public DateTime DOB { get; set; }
        [Required(ErrorMessage ="Please enter your address")]
        public int AddressId { get; set; }
        [Display(Name = "Course")]
        [Required(ErrorMessage ="Please select Course")]
        public string CourseId { get; set; }


    }
}