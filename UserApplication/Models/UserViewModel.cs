﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UserApplication.Models
{
    public class UserViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please select gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Please select Hobbies")]
        public string Hobbies { get; set; }

        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[$@$!%*?&])[A-Za-z\\d$@$!%*?&]{6,}",
            //ErrorMessage = "Password should be of minimum 6 characters with at least 1 Uppercase Alphabet, 1 Lowercase Alphabet, 1 Number and 1 Special Character"
             ErrorMessage = "Incorrect Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[$@$!%*?&])[A-Za-z\\d$@$!%*?&]{6,}",
         ErrorMessage = "Password should be of minimum 6 characters with at least 1 Uppercase Alphabet, 1 Lowercase Alphabet, 1 Number and 1 Special Character"
          )]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter valid DOB")]
        [Display(Name = "DOB")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "Please select the course")]
        [DisplayName("Role")]
        public int RoleId { get; set; }

        [Required(ErrorMessage = "Please select the course")]
        [DisplayName("Course")]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Enter your permanent address")]
        [DisplayName("Permanent Address")]
        public string AddressLine1 { get; set; }

        [Required(ErrorMessage = "Enter your current address")]
        [DisplayName("Temporary Address")]
        public string AddressLine2 { get; set; }

        [Required(ErrorMessage = "This field cannot be null")]
        [DisplayName("Address")]
        public int AddressId { get; set; }

        [DisplayName("Country Name")]
        [Required(ErrorMessage = "Please select your Country")]
        public int CountryId { get; set; }

        [DisplayName("State Name")]
        [Required(ErrorMessage = "Please select your State")]
        public int StateId { get; set; }

        [DisplayName("City Name")]
        [Required(ErrorMessage = "Please select your City name")]
        public int CityId { get; set; }

        [DisplayName("Zip code")]
        [Required(ErrorMessage = "Please enter Zipcode")]
        [RegularExpression(@"^(\d{6})$", ErrorMessage = "ZipCode is not valid.")]
        public int Zipcode { get; set; }

        [Required]
        [DisplayName("Date Created")]
        public DateTime DateCreated { get; set; }

        [Required]
        [DisplayName("Date Modified")]
        public DateTime DateModified { get; set; }

        [Required]
        [DisplayName("Is Active")]
        public bool IsActive { get; set; }

        //[Required]
        //[DisplayName("Subject")]
        public int SubjectId { get; set; }

        [DisplayName("Country")]
        public string CountryName { get; set; }

        [DisplayName("State")]
        public string StateName { get; set; }

        [DisplayName("City")]
        public string CityName { get; set; }

        [DisplayName("Course")]
        public string CourseName { get; set; }


    }
}

       
    
