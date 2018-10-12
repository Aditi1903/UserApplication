using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserApplication.Models;

namespace UserApplication.Controllers
{
    public class SuperAdminController : Controller
    {
        private UserDbContext obj = new UserDbContext();
        // GET: SuperAdmin
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Display list of all users
        /// </summary>
        /// <returns></returns>
        public ActionResult UserList()
        {
            var list = obj.Users.ToList();
            return View(list);
        }
        //public ActionResult CreateUser()
        //{
        //    return View();
        //}
        //[HttpPost]

        //public ActionResult CreateUser(UserViewModel userViewModel)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here
        //        obj.Users.Add(userView);
        //        obj.SaveChanges();
        //        return RedirectToAction("ShowList");
        //    }
        //    catch
        //    {
        //        return View();
        //    }

    }
}




