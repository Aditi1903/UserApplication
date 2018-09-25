using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserApplication.Models;

namespace UserApplication.Controllers
{
    public class StudentController : Controller
    {
        private UserDbContext obj = new UserDbContext();

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

    }
}