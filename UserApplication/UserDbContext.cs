using System;
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
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserInRole> UserInRoles { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<SubjectInCourse> SubjectsInCourses { get; set; }
        public DbSet<TeacherInSubject> TeacherInSubjects { get; set; }
        public DbSet<Subject> Subjects { get; set; }
       
    }
}
