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

        public ActionResult StudentList()
        {
            var listOfStudent = obj.Users.Where(u => u.RoleId == 4).ToList();
            return View(listOfStudent);
        }
        /// <summary>
        /// Show the list of teachers with courses assigned to them
        /// </summary>
        /// <returns></returns>
        public ActionResult TeachersCourse()
        {
            var listOfTeacherCourse = obj.Users.Where(u => u.RoleId == 3).ToList();
            return View(listOfTeacherCourse);
        }
        /// <summary>
        /// Show the list of teachers with subjects against that particular course
        /// </summary>
        /// <returns></returns>
        public ActionResult TeachersSubject()
        {
            var listOfTeachersSubject = obj.TeacherInSubjects.ToList();
            return View(listOfTeachersSubject);
        }
        public ActionResult StudentCourse()
        {
            var listOfStudentCourse = obj.Users.Where(u => u.RoleId == 4).ToList();
            return View(listOfStudentCourse);
        }
        public ActionResult SubjectsInCourse()
        {
            var listOfSubjectsInCourse = obj.SubjectsInCourses.ToList();
            return View(listOfSubjectsInCourse);
        }
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

                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(objUserViewModel);
            }
        }
    }
}
