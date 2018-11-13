using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UserApplication.Models;
using static UserApplication.Models.UserViewModel;

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
           var listOfUsers = obj.Users.Where(u => u.RoleId != 1).ToList();
           return View(listOfUsers);
        }
        /// <summary>
        /// GET:Super Admin can create user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateUser()
        {
            //Dropdown for Role List
            List<Role> List = obj.Roles.Where(u => u.RoleId != 1 ).ToList();
            ViewBag.RoleList = new SelectList(List, "RoleId", "RoleName");

            //Dropdown for the Course List
            List<Course> Lists = obj.Courses.ToList();
            ViewBag.CourseLists = new SelectList(Lists, "CourseId", "CourseName");

            //Dropdown for the Country List
            List<Country> List1 = obj.Countries.ToList();
            ViewBag.CountryList1 = new SelectList(List1, "CountryId", "CountryName");

            return View();
        }
        /// <summary>
        /// POST:Super Admin can create user
        /// </summary>
        /// <param name="userViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateUser(UserViewModel userViewModel)
        {

            //Dropdown for Role List
            List<Role> List = obj.Roles.Where(u => u.RoleId != 1).ToList();
            ViewBag.RoleList = new SelectList(List, "RoleId", "RoleName");

            //Dropdown for Course List
            List<Course> Lists = obj.Courses.ToList();
            ViewBag.CourseLists = new SelectList(Lists, "CourseId", "CourseName");

            //Dropdown for Country List
            List<Country> List1 = obj.Countries.ToList();
            ViewBag.CountryList1 = new SelectList(List1, "CountryId", "CountryName");

            //Object of address table
            Address address = new Address();
            //Binding the fields of address table
            address.AddressId = userViewModel.AddressId;
            address.CountryId = userViewModel.CountryId;
            address.StateId = userViewModel.StateId;
            address.CityId = userViewModel.CityId;
            address.Zipcode = userViewModel.Zipcode;

            obj.Addresses.Add(address);  //Insert data in address table
            obj.SaveChanges();           //Save data in database
            int latestAddressId = address.AddressId;

            //Object of user table 
            User user = new User();
            //Binding the fields 
            user.UserId = userViewModel.UserId;
            user.FirstName = userViewModel.FirstName;
            user.LastName = userViewModel.LastName;
            user.Gender = userViewModel.Gender;
            user.Hobbies = userViewModel.Hobbies;
            user.Password = userViewModel.Password;
            user.Email = userViewModel.Email;
            user.DOB = userViewModel.DOB;
            user.DateCreated = DateTime.Now;
            user.DateModified = DateTime.Now;
            user.IsActive = userViewModel.IsActive;
            user.RoleId = userViewModel.RoleId;
            user.CourseId = userViewModel.CourseId;
            user.AddressLine1 = userViewModel.AddressLine1;
            user.AddressLine2 = userViewModel.AddressLine2;
            user.AddressId = latestAddressId;

            obj.Users.Add(user);      //Insert data in user table
            obj.SaveChanges();         //Save data in database

            int latestUserId = user.UserId;

            UserInRole userInRole = new UserInRole();
            userInRole.UserId = latestUserId;
            userInRole.RoleId = userViewModel.RoleId;

            obj.UserInRoles.Add(userInRole);
            obj.SaveChanges();

            return RedirectToAction("UserList");
        }
        /// <summary>
        /// Get all countries
        /// </summary>
        SqlConnection UserDbContext = new SqlConnection(ConfigurationManager.ConnectionStrings["UserDbContext"].ConnectionString);
        public DataSet Get_Country()
        {
            SqlCommand com = new SqlCommand("Select * from Countries", UserDbContext);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        /// <summary>
        /// Get all states
        /// </summary>
        /// <param name="CountryId"></param>
        /// <returns></returns>
        public DataSet Get_State(string CountryId)
        {
            SqlCommand com = new SqlCommand("Select * from States where CountryId=@countryid", UserDbContext);
            com.Parameters.AddWithValue("@countryid", CountryId);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        /// <summary>
        /// Get all cities
        /// </summary>
        /// <param name="StateId"></param>
        /// <returns></returns>
        public DataSet Get_City(string StateId)
        {
            SqlCommand com = new SqlCommand("Select * from Cities where StateId=@stateid", UserDbContext);
            com.Parameters.AddWithValue("@stateid", StateId);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        /// <summary>
        /// Binding the country
        /// </summary>
        public void Country_Bind()
        {
            DataSet ds = Get_Country();
            List<SelectListItem> countrylist = new List<SelectListItem>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                countrylist.Add(new SelectListItem { Text = dr["CountryName"].ToString(), Value = dr["CountryId"].ToString() });

            }
            ViewBag.Country = countrylist;
        }
        /// <summary>
        /// Binding the state
        /// </summary>
        /// <param name="CountryId"></param>
        /// <returns></returns>
        public JsonResult State_Bind(string CountryId)
        {
            DataSet ds = Get_State(CountryId);
            List<SelectListItem> statelist = new List<SelectListItem>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                statelist.Add(new SelectListItem { Text = dr["StateName"].ToString(), Value = dr["StateId"].ToString() });
            }
            return Json(statelist, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Binding the city
        /// </summary>
        /// <param name="StateId"></param>
        /// <returns></returns>
        public JsonResult City_Bind(string StateId)
        {
            DataSet ds = Get_City(StateId);
            List<SelectListItem> citylist = new List<SelectListItem>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                citylist.Add(new SelectListItem { Text = dr["CityName"].ToString(), Value = dr["CityId"].ToString() });
            }
            return Json(citylist, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// GET:Super Admin can edit the user details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditUser(int id)
        {
            //Dropdown for Role List
            List<Role> List = obj.Roles.Where(u => u.RoleId != 1).ToList();
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
            //Object of user table
            User objUser = obj.Users.Find(id);
            //Object of user view model table
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
        ///  POST:Super Admin can edit the user details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="objUserViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditUser(int id, UserViewModel objUserViewModel)
        {
            //Dropdown for Role List
            List<Role> List = obj.Roles.Where(u => u.RoleId != 1).ToList();
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

                    obj.SaveChanges();  //Save data in database
                    return RedirectToAction("UserList");

                }
                return View(objUserViewModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// GET:Super Admin can remove user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeleteUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User objUser = obj.Users.Find(id);
            UserViewModel objUserViewModel = new UserViewModel();

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
            objUserViewModel.DateCreated = objUser.DateCreated;
            objUserViewModel.DateModified = objUser.DateModified;
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
        /// POST:Super Admin can remove user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteUser(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    UserInRole objUserInRole = obj.UserInRoles.Where(m => m.UserId == id).FirstOrDefault();
                    User objUser = obj.Users.Where(m => m.UserId == id).FirstOrDefault();
                    Address objAddress = obj.Addresses.Where(m => m.AddressId == objUser.AddressId).FirstOrDefault();

                    //To remove address of user from address table
                    obj.Addresses.Remove(objAddress);
                    //To Remove User from User Table
                    obj.Users.Remove(objUser);

                    // To remove User from UserInRole table.
                    obj.UserInRoles.Remove(objUserInRole);

                    obj.SaveChanges();

                }
                return RedirectToAction("UserList");

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Super admin can view user details
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
        /// GET : Super Admin can create course
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateCourse()
        {
            return View();
        }
        /// <summary>
        /// POST :Super Admin can create course
        /// </summary>
        /// <param name="objCourse"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateCourse(Course objCourse)
        {
            if (objCourse.CourseName == null)
            {
                Console.WriteLine("Course cannot be null");
            }
            else
            {
                obj.Courses.Add(objCourse);      //Insert data 
                obj.SaveChanges();               //Save data
                return RedirectToAction("CourseList");
            }
            return View(objCourse);
        }
        /// <summary>
        /// GET : Super Admin can create subject
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateSubject()
        {
            return View();
        }
        /// <summary>
        /// POST: Super Admin can create subject
        /// </summary>
        /// <param name="objSubject"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateSubject(Subject objSubject)
        {
            if (objSubject.SubjectName == null)
            {
                Console.WriteLine("Subject cannot be null");
            }
            else
            {
                obj.Subjects.Add(objSubject);
                obj.SaveChanges();
                return RedirectToAction("SubjectList");
            }
            return View(objSubject);
        }
        /// <summary>
        /// GET: Super Admin can assign subject to course
        /// </summary>
        /// <returns></returns>

        public ActionResult AssignSubjectForCourse()
        {
            List<Course> List = obj.Courses.ToList();
            ViewBag.CourseList = new SelectList(List, "CourseId", "CourseName");

            List<Subject> Lists = obj.Subjects.ToList();
            ViewBag.SubjectList = new SelectList(Lists, "SubjectId", "SubjectName");

            return View();
        }
        /// <summary>
        /// POST:Super Admin can assign subject to course
        /// </summary>
        /// <param name="objSubjectInCourse"></param>
        /// <returns></returns>           
        [HttpPost]
        public ActionResult AssignSubjectForCourse(SubjectInCourse objSubjectInCourse)
        {
            List<Course> List = obj.Courses.ToList();
            ViewBag.CourseList = new SelectList(List, "CourseId", "CourseName", objSubjectInCourse.CourseId);

            List<Subject> Lists = obj.Subjects.ToList();
            ViewBag.SubjectList = new SelectList(Lists, "SubjectId", "SubjectName", objSubjectInCourse.SubjectId);

            obj.SubjectsInCourses.Add(objSubjectInCourse);
            obj.SaveChanges();


            //return View(objSubjectInCourse);
            return RedirectToAction("CourseAndSubjectList");
        }
        /// <summary>
        /// Super Admin after creating course can view course list
        /// </summary>
        /// <returns></returns>
        public ActionResult CourseList()
        {
            var listOfCourse = obj.Courses.ToList();
            return View(listOfCourse);
        }
        /// <summary>
        /// Super Admin after creating subject can view subject list
        /// </summary>
        /// <returns></returns>
        public ActionResult SubjectList()
        {
            var listOfSubject = obj.Subjects.ToList();
            return View(listOfSubject);
        }
        /// <summary>
        /// GET : Super admin can delete course
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeleteCourse(int id)
        {
            var removeCourse = obj.Courses.Single(x => x.CourseId == id);

            return View(removeCourse);
        }
        /// <summary>
        /// POST: Super admin can delete course
        /// </summary>
        /// <param name="id"></param>
        /// <param name="objCourse"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteCourse(int id, Course objCourse)
        {
            try
            {
                // TODO: Add delete logic here
                var deleteCourse = obj.Courses.Single(x => x.CourseId == id);
                obj.Courses.Remove(deleteCourse);

                obj.SaveChanges();

                return RedirectToAction("CourseList");
            }
            catch
            {
                return View();
            }
        }
        /// <summary>
        /// GET : Super admin can delete subject
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeleteSubject(int id)
        {
            var removeSubject = obj.Subjects.Single(x => x.SubjectId == id);

            return View(removeSubject);
        }
        /// <summary>
        /// POST : Super admin can delete subject
        /// </summary>
        /// <param name="id"></param>
        /// <param name="objSubject"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteSubject(int id, Subject objSubject)
        {
            try
            {
                // TODO: Add delete logic here
                var deleteSubject = obj.Subjects.Single(x => x.SubjectId == id);
                obj.Subjects.Remove(deleteSubject);

                obj.SaveChanges();

                return RedirectToAction("SubjectList");
            }
            catch
            {
                return View();
            }
        }
        /// <summary>
        /// Super admin can view list of courses with subjects
        /// </summary>
        /// <returns></returns>
        public ActionResult CourseAndSubjectList()
        {
            var listOfCourseAndSubject = obj.SubjectsInCourses.ToList();
            return View(listOfCourseAndSubject);
        }
        /// <summary>
        /// GET:Super Admin can assign subjects to teachers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AssignSubjectToTeacher()
        {
            List<User> List = obj.Users.Where(u => u.RoleId != 1 && u.RoleId != 2 && u.RoleId != 4).ToList();
            ViewBag.TeacherList = new SelectList(List, "UserId", "FirstName");

            List<Subject> Lists = obj.Subjects.ToList();
            ViewBag.SubjectList = new SelectList(Lists, "SubjectId", "SubjectName");

            return View();
        }
        /// <summary>
        ///POST:Super Admin can assign subjects to teachers
        /// </summary>
        /// <param name="objUserViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AssignSubjectToTeacher(TeacherInSubject objTeacherInSubject)
        {
            List<User> List = obj.Users.Where(u => u.RoleId != 1 && u.RoleId != 2 && u.RoleId != 4).ToList();
            ViewBag.TeacherList = new SelectList(List, "UserId", "FirstName", objTeacherInSubject.UserId);

            List<Subject> Lists = obj.Subjects.ToList();
            ViewBag.SubjectList = new SelectList(Lists, "SubjectId", "SubjectName", objTeacherInSubject.SubjectId);

            obj.TeacherInSubjects.Add(objTeacherInSubject);  //Insert data 
            obj.SaveChanges();           //Save data in database

            //return View(objTeacherInSubject);
            return RedirectToAction("TeacherSubjectList");
        }
        /// <summary>
        /// Super Admin can view list of teachers with their subjects
        /// </summary>
        /// <returns></returns>
        public ActionResult TeacherSubjectList()
        {
            var listOfTeachersSubject = obj.TeacherInSubjects.ToList();
            return View(listOfTeachersSubject);
        }
       
    }
}
        


    






    






        





