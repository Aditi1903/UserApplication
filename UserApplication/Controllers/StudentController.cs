using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UserApplication.Models;

namespace UserApplication.Controllers
{
    public class StudentController : Controller
    {
        private UserDbContext obj = new UserDbContext();
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }
       
        public ActionResult StudentDetails(UserViewModel objuserViewModels)
        {
            var form = obj.UserViewModels.Where(u => u.Email == objuserViewModels.Email && u.Password == objuserViewModels.Password).FirstOrDefault();
            if (form != null)
            {
                Session["UserId"] = objuserViewModels.UserId.ToString();
                Session["FirstName"] = objuserViewModels.FirstName.ToString();
                //return RedirectToAction("UserDetails", "SuperAdmin");
            }
             else
             {
                return HttpNotFound();
             }
            return View();
        }
        /// <summary>
        /// Show the list of teachers with courses assigned to them
        /// </summary>
        /// <returns></returns>
        public ActionResult TeachersCourse()
        {
            var list = obj.Users.Where(u => u.RoleId == 3).ToList();
            return View(list);
        }
        /// <summary>
        /// Show the list of teachers with subjects against that particular subject
        /// </summary>
        /// <returns></returns>
        public ActionResult TeachersSubject()
        {
            var list = obj.TeacherInSubjects.ToList();
            return View(list);
        }
    }
}