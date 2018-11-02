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

        ///// <summary>
        /// Show the list of teachers with courses assigned to them
        /// </summary>
        /// <returns></returns>
        public ActionResult TeachersCourse()
        {
            var listOfTeacherCourse = obj.Users.Where(u => u.RoleId == 3).ToList();
            return View(listOfTeacherCourse);

        }
        
        /// <summary>
        /// List of subjects in courses
        /// </summary>
        /// <returns></returns>
        public ActionResult SubjectsInCourse(int id)
        {
            var listOfSubjectsInCourse = obj.SubjectsInCourses.Where(u => u.CourseId == id).ToList();
            return View(listOfSubjectsInCourse);
        }
        /// <summary>
        /// Student details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult StudentDetail(int? id)                 
        {
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                User user = obj.Users.Find(id);
                UserViewModel objUserViewModel = new UserViewModel();

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
                objUserViewModel.UserId = user.UserId;

                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(objUserViewModel);
            }
            
        }
        /// <summary>
        /// GET:Student can edit details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditStudentProfile(int id)
        {
            //Dropdown for Role List
            List<Role> List = obj.Roles.Where(u => u.RoleId == 4).ToList();
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
        ///  POST:Student can edit details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="objUserViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditStudentProfile(int id, UserViewModel objUserViewModel)
        {
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

                    obj.SaveChanges();    // //Save data in database
                    return RedirectToAction("StudentDetail",new { id = objUser.UserId });
                   
                }
                return View(objUserViewModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///// <summary>
        ///// Show the list of teachers with subjects against that particular course
        ///// </summary>
        ///// <returns></returns>
        //public ActionResult TeachersSubject()
        //{
        //    var listOfTeachersSubject = obj.TeacherInSubjects.ToList();
        //    return View(listOfTeachersSubject);
        //}
        ///// <summary>
        ///// List of students with their course
        ///// </summary>
        ///// <returns></returns>
        //public ActionResult StudentCourse()
        //{
        //    var listOfStudentCourse = obj.Users.Where(u => u.RoleId == 4).ToList();
        //    return View(listOfStudentCourse);
        //}

    }
}



        

    

            
        
     

        


    

