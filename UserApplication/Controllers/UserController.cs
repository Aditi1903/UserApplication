using System;
using System.Collections.Generic;
using System.Linq;
//using System.Reflection;
using System.Web;
using System.Web.Mvc;
using UserApplication.Models;

namespace UserApplication.Controllers
{
    public class UserController : Controller
    {
        private UserDbContext obj = new UserDbContext();

        // Registration form
        [HttpGet]                                                 
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registration(User user)
        {
            if (ModelState.IsValid)
            {
                using (UserDbContext db = new UserDbContext())
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = user.FirstName + "" + user.LastName + "" + user.Gender + "" + user.Hobbies + "" + user.Password + "" + user.Email + "" + user.DOB + "" + user.Role + "" + user.Course + "Succesfully Registered.";

            }
            return View();
        }

        // Coding for role and course dropdown
        [HttpGet]
        public ActionResult Index()
        {
            List<Role> List = obj.Roles.ToList();
            ViewBag.RoleList = new SelectList(List, "RoleId", "RoleName");
            List<Course> Lists = obj.Courses.ToList();
            ViewBag.CourseLists = new SelectList(Lists, "CourseId", "CourseName");
            return View();
        }
        [HttpPost]
        public ActionResult Index(User user)
        {
            List<Role> List = obj.Roles.ToList();
            ViewBag.RoleList = new SelectList(List, "RoleId", "RoleName");
            List<Course> Lists = obj.Courses.ToList();
            ViewBag.CourseLists = new SelectList(Lists, "CourseId", "CourseName");

            User select = new User();
            select.UserId = user.UserId;
            select.FirstName = user.FirstName;
            select.LastName = user.LastName;
            select.Gender = user.Gender;
            select.Hobbies = user.Hobbies;
            select.Password = user.Password;
            select.Email = user.Email;
            select.DOB = user.DOB;
            select.RoleId = user.RoleId;
            select.CourseId = user.CourseId;
            select.AddressLine1 = user.AddressLine1;
            select.AddressLine2 = user.AddressLine2;
            select.AddressId = user.AddressId;

            obj.Users.Add(user);
            obj.SaveChanges();

            int latestUserId = user.UserId;

            UserInRole userInRole = new UserInRole();
            userInRole.UserId = latestUserId;
            userInRole.RoleId = user.RoleId;

            obj.UserInRoles.Add(userInRole);
            obj.SaveChanges();


            return View(user);
        }
        //Country,City,State dropdown
        public List <Country> GetCountryList()
        {
            List<Country> countries = obj.Countries.ToList();
            return countries;
        }
        public List<State> GetStateList(int CountryId)
        {
            List<State> stateList = obj.States.Where(x => x.CountryId == CountryId).ToList();
            return stateList;
        }
    }
}