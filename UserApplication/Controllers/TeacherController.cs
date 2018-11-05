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
            User user = (User)Session["User"];
            var usr = obj.Users.Find(user.UserId);

            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                //User user = obj.Users.Find(id);
                UserViewModel objUserViewModel = new UserViewModel();

                objUserViewModel.UserId = user.UserId;
                objUserViewModel.FirstName = user.FirstName;
                objUserViewModel.LastName = user.LastName;
                objUserViewModel.Gender = user.Gender;
                objUserViewModel.Hobbies = user.Hobbies;
                objUserViewModel.Email = user.Email;
                objUserViewModel.Password = user.Password;
                objUserViewModel.DOB = user.DOB;
                objUserViewModel.RoleId = user.RoleId;
                objUserViewModel.CourseId = user.CourseId;
                //objUserViewModel.AddressId = user.AddressId;
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
        /// GET:Teacher can edit details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditTeacherProfile(int id)
        {
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
            UserViewModel objUserViewModel = new UserViewModel();

            objUserViewModel.UserId = objUser.UserId;
            objUserViewModel.FirstName = objUser.FirstName;
            objUserViewModel.LastName = objUser.LastName;
            objUserViewModel.Gender = objUser.Gender;
            objUserViewModel.Hobbies = objUser.Hobbies;
            objUserViewModel.Email = objUser.Email;
            objUserViewModel.Password = objUser.Password;
            objUserViewModel.DOB = objUser.DOB;
            objUserViewModel.RoleId = objUser.RoleId;
            objUserViewModel.CourseId = objUser.CourseId;
            objUserViewModel.IsActive = objUser.IsActive;
            objUser.DateModified = DateTime.Now;
            objUserViewModel.AddressId = objUser.AddressId;
            objUserViewModel.AddressLine1 = objUser.AddressLine1;
            objUserViewModel.AddressLine2 = objUser.AddressLine2;
            objUserViewModel.CountryId = objUser.Address.CountryId;
            objUserViewModel.StateId = objUser.Address.StateId;
            objUserViewModel.CityId = objUser.Address.CityId;
            objUserViewModel.Zipcode = objUser.Address.Zipcode;

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
                    obj.SaveChanges();    // //Save data in database
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