﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UserApplication.Models;

namespace UserApplication
{
    public class UserDbContext : DbContext
    {
        public UserDbContext() : base("UserDbContext")
        { }
        

        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserInRole> UserInRoles { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<SubjectInCourse> SubjectsInCourses { get; set; }
        public virtual DbSet<TeacherInSubject> TeacherInSubjects { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }

        //public System.Data.Entity.DbSet<UserApplication.ViewModel.UserViewModel> UserViewModels { get; set; }
    }
}
