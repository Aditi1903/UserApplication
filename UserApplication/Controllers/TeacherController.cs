using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UserApplication.Models;

namespace UserApplication.Controllers
{
    public class TeacherController : Controller
    {
        private UserDbContext obj = new UserDbContext();
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Teacher can view the list of students
        /// </summary>
        /// <returns></returns>
        public ActionResult UserList(int id)
        {
            if (Session["Login"] == null && Session["User"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            var studentList = obj.Users.Where(u => u.RoleId == 4 && u.CourseId == id).ToList();
            return View(studentList);
        }
        /// <summary>
        /// Teacher can view the details of students
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UserDetails(int? id)
        {
            if (Session["Login"] == null && Session["User"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                User user = obj.Users.Find(id);
                UserViewModel objUserViewModel = new UserViewModel();
                //Course objCourse = new Course();

                objUserViewModel.FirstName = user.FirstName;
                objUserViewModel.LastName = user.LastName;
                objUserViewModel.Gender = user.Gender;
                objUserViewModel.Hobbies = user.Hobbies;
                objUserViewModel.Email = user.Email;
                objUserViewModel.Password = user.Password;
                objUserViewModel.DOB = user.DOB;
                objUserViewModel.RoleId = user.RoleId;
                objUserViewModel.CourseId = user.CourseId;
                objUserViewModel.IsActive = user.IsActive;
                objUserViewModel.DateCreated = user.DateCreated;
                objUserViewModel.DateModified = user.DateModified;
                objUserViewModel.AddressLine1 = user.AddressLine1;
                objUserViewModel.AddressLine2 = user.AddressLine2;
                objUserViewModel.CountryId = user.Address.CountryId;
                objUserViewModel.StateId = user.Address.StateId;
                objUserViewModel.CityId = user.Address.CityId;
                objUserViewModel.Zipcode = user.Address.Zipcode;
                objUserViewModel.CountryName = user.Address.Country.CountryName;
                objUserViewModel.StateName = user.Address.State.StateName;
                objUserViewModel.CityName = user.Address.City.CityName;
                objUserViewModel.CourseName = user.Course.CourseName;

                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(objUserViewModel);
            }
        }
        /// <summary>
        /// Teacher can view self details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult TeacherDetail(int? id)
        {
            if (Session["Login"] == null && Session["User"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            User user = (User)Session["User"];
            var usr = obj.Users.Find(user.UserId);

            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                //User user = obj.Users.Find(id);
                UserViewModel objUserViewModel = new UserViewModel();

                objUserViewModel.UserId = usr.UserId;
                objUserViewModel.FirstName = usr.FirstName;
                objUserViewModel.LastName = usr.LastName;
                objUserViewModel.Gender = usr.Gender;
                objUserViewModel.Hobbies = usr.Hobbies;
                objUserViewModel.Email = usr.Email;
                objUserViewModel.Password = usr.Password;
                objUserViewModel.DOB = usr.DOB;
                objUserViewModel.RoleId = usr.RoleId;
                objUserViewModel.CourseId = usr.CourseId;
                //objUserViewModel.AddressId = user.AddressId;
                objUserViewModel.IsActive = usr.IsActive;
                objUserViewModel.DateCreated = usr.DateCreated;
                objUserViewModel.DateModified = usr.DateModified;
                objUserViewModel.AddressLine1 = usr.AddressLine1;
                objUserViewModel.AddressLine2 = usr.AddressLine2;
                objUserViewModel.CountryId = usr.Address.CountryId;
                objUserViewModel.StateId = usr.Address.StateId;
                objUserViewModel.CityId = usr.Address.CityId;
                objUserViewModel.Zipcode = usr.Address.Zipcode;
                objUserViewModel.CountryName = usr.Address.Country.CountryName;
                objUserViewModel.StateName = usr.Address.State.StateName;
                objUserViewModel.CityName = usr.Address.City.CityName;
                objUserViewModel.CourseName = usr.Course.CourseName;

                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(objUserViewModel);
            }
        }
        /// <summary>
        /// GET:Teacher can edit details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditTeacherProfile(int id)
        {
            if (Session["Login"] == null && Session["User"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            //Dropdown for Role List
            List<Role> List = obj.Roles.Where(u=>u.RoleId==3).ToList();
            ViewBag.RoleList = new SelectList(List, "RoleId", "RoleName");

            //Dropdown for Course List
            List<Course> Lists = obj.Courses.ToList();
            ViewBag.CourseLists = new SelectList(Lists, "CourseId", "CourseName");

            List<Country> CountryList = obj.Countries.ToList();
            ViewBag.CountryLists = new SelectList(CountryList, "CountryId", "CountryName");

            List<State> StateList = obj.States.ToList();
            ViewBag.StateLists = new SelectList(StateList, "StateId", "StateName");

            List<City> CityList = obj.Cities.ToList();
            ViewBag.CityLists = new SelectList(CityList, "CityId", "CityName");

            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User objUser = obj.Users.Find(id);
            UserViewModel objUserViewModel = new UserViewModel
            {
                UserId = objUser.UserId,
                FirstName = objUser.FirstName,
                LastName = objUser.LastName,
                Gender = objUser.Gender,
                Hobbies = objUser.Hobbies,
                Email = objUser.Email,
                Password = objUser.Password,
                DOB = objUser.DOB,
                RoleId = objUser.RoleId,
                CourseId = objUser.CourseId,
                IsActive = objUser.IsActive,
                //bjUser.DateModified = DateTime.Now;
                AddressId = objUser.AddressId,
                AddressLine1 = objUser.AddressLine1,
                AddressLine2 = objUser.AddressLine2,
                CountryId = objUser.Address.CountryId,
                StateId = objUser.Address.StateId,
                CityId = objUser.Address.CityId,
                Zipcode = objUser.Address.Zipcode,
                ConfirmPassword = objUser.Password
            };

            if (objUser == null)
            {
                return HttpNotFound();
            }
            return View(objUserViewModel);
        }
        /// <summary>
        ///  POST:Teacher can edit details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="objUserViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditTeacherProfile(int id, UserViewModel objUserViewModel)
        {
            if (Session["Login"] == null && Session["User"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            //Dropdown for Role List
            List<Role> List = obj.Roles.ToList();
            ViewBag.RoleList = new SelectList(List, "RoleId", "RoleName");

            //Dropdown for Course List
            List<Course> Lists = obj.Courses.ToList();
            ViewBag.CourseLists = new SelectList(Lists, "CourseId", "CourseName");

            List<Country> CountryList = obj.Countries.ToList();
            ViewBag.CountryLists = new SelectList(CountryList, "CountryId", "CountryName");

            List<State> StateList = obj.States.ToList();
            ViewBag.StateLists = new SelectList(StateList, "StateId", "StateName");

            List<City> CityList = obj.Cities.ToList();
            ViewBag.CityLists = new SelectList(CityList, "CityId", "CityName");
            try
            {
                User objUser = obj.Users.Find(id);
                 if (ModelState.IsValid)
                 {
                    objUser.UserId = objUserViewModel.UserId;
                    objUser.FirstName = objUserViewModel.FirstName;
                    objUser.LastName = objUserViewModel.LastName;
                    objUser.Gender = objUserViewModel.Gender;
                    objUser.Hobbies = objUserViewModel.Hobbies;
                    objUser.Email = objUserViewModel.Email;
                    objUser.Password = objUserViewModel.Password;
                    objUser.DOB = objUserViewModel.DOB;
                    objUser.RoleId = objUserViewModel.RoleId;
                    objUser.CourseId = objUserViewModel.CourseId;
                    objUser.IsActive = objUserViewModel.IsActive;
                    objUser.DateModified = DateTime.Now;
                    objUser.AddressLine1 = objUserViewModel.AddressLine1;
                    objUser.AddressLine2 = objUserViewModel.AddressLine2;
                    objUser.Address.CountryId = objUserViewModel.CountryId;
                    objUser.Address.StateId = objUserViewModel.StateId;
                    objUser.Address.CityId = objUserViewModel.CityId;
                    objUser.Address.Zipcode = objUserViewModel.Zipcode;

                    //  obj.Users.Add(objUser);
                    obj.SaveChanges();     //Save data in database
                    return RedirectToAction("TeacherDetail",new { id = objUser.UserId });

                 }
                  return View(objUserViewModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}