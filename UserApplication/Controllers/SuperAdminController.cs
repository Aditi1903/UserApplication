﻿using System;
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
            var listOfUsers = obj.Users.ToList();
            return View(listOfUsers);
        }
        //[HttpGet]
        //public ActionResult CreateUser()
        //{
        //    UserViewModel model = new UserViewModel();
        //    var courseList = obj.Courses.Select(x => new CourseModel
        //    {
        //        CourseName = x.CourseName,
        //        CourseId = x.CourseId
        //    }).ToList();

        //    var roleList = obj.Roles.Select(x => new RoleModel
        //    {
        //        RoleName = x.RoleName,
        //        RoleId = x.RoleId
        //    }).ToList();

        //    model.Roles = roleList;
        //    model.Courses = courseList;

        //    var countryList = obj.Countries.Select(x => new CountryModel
        //    {
        //        CountryName = x.CountryName,
        //        CountryId = x.CountryId
        //    }).ToList();

        //    model.Countries = countryList;

        //    var stateList = obj.States.Select(x => new StateModel
        //    {
        //        StateName = x.StateName,
        //        StateId = x.StateId
        //    }).ToList();

        //    model.States = stateList;

        //    var cityList = obj.Cities.Select(x => new CityModel
        //    {
        //        CityName = x.CityName,
        //        CityId = x.CityId
        //    }).ToList();

        //    model.Cities = cityList;

        //    return View(model);
        //}
        //[HttpPost]
        //public ActionResult CreateUser(UserViewModel objUserViewModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(objUserViewModel);
        //    }
        //    using (var transaction = obj.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            Address objAddress = new Address
        //            {
        //                CountryId = objUserViewModel.CountryId,
        //                StateId = objUserViewModel.StateId,
        //                CityId = objUserViewModel.CityId,
        //                Zipcode = objUserViewModel.Zipcode,
        //            };
        //            obj.Addresses.Add(objAddress);
        //            obj.SaveChanges();

        //            User objUser = new User
        //            {
        //                UserId = objUserViewModel.UserId,
        //                FirstName = objUserViewModel.FirstName,
        //                LastName = objUserViewModel.LastName,
        //                Gender = objUserViewModel.Gender,
        //                DOB = objUserViewModel.DOB,
        //                Hobbies = objUserViewModel.Hobbies,
        //                Email = objUserViewModel.Email,
        //                Password = objUserViewModel.Password,
        //                AddressLine1 = objUserViewModel.AddressLine1,
        //                AddressLine2 = objUserViewModel.AddressLine2,
        //                IsActive = objUserViewModel.IsActive,
        //                DateCreated = objUserViewModel.DateCreated,
        //                DateModified = objUserViewModel.DateModified,
        //                CourseId = objUserViewModel.CourseId,
        //                RoleId = objUserViewModel.RoleId,

        //                AddressId = objAddress.AddressId,
        //            };

        //            obj.Users.Add(objUser);
        //            obj.SaveChanges();

        //            UserInRole objUserInRole = new UserInRole
        //            {
        //                RoleId = objUserViewModel.RoleId,
        //                UserId = objUser.UserId
        //            };
        //            obj.UserInRoles.Add(objUserInRole);
        //            obj.SaveChanges();

        //            transaction.Commit();

        //            ViewBag.ResultMessage = objUserViewModel.FirstName + "" + objUserViewModel.LastName + "" + "is successfully registered.";
        //            ModelState.Clear();
        //        }

        //        catch(Exception)
        //        {
        //            transaction.Rollback();
        //            ViewBag.ResultMessage = "Error occurred in the registration process.Please register again.";
        //        }
        //    }
        //         return RedirectToAction("Login");
        //}
        /// <summary>
        /// GET:Super Admin can create user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateUser()
        {
            //Dropdown for Role List
            List<Role> List = obj.Roles.Where(u => u.RoleId != 1).ToList();
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
            List<Role> List = obj.Roles.Where(u => u.RoleId != 1 && u.RoleId != 2).ToList();
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
            user.DateCreated = userViewModel.DateCreated;
            user.DateModified = userViewModel.DateModified;
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
            List<Role> List = obj.Roles.ToList();
            ViewBag.RoleList = new SelectList(List, "RoleId", "RoleName");

            //Dropdown for Course List
            List<Course> Lists = obj.Courses.ToList();
            ViewBag.CourseLists = new SelectList(Lists, "CourseId", "CourseName");

            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Object of user table
            User objUser = obj.Users.Find(id);
            //Object of user view model table
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
        ///  POST:Super Admin can edit the user details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="objUserViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditUser(int id, UserViewModel objUserViewModel)
        {
            //Dropdown for Role List
            List<Role> objRoleList = obj.Roles.ToList();
            ViewBag.Role = new SelectList(obj.Users.ToList(), "RoleId", "RoleName");
            //Dropdown for Course List
            List<Course> objCourseList = obj.Courses.ToList();
            ViewBag.Course = objCourseList;
            try
            {
                User objUser = obj.Users.Find(id);
                if (ModelState.IsValid)
                {
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
                    objUser.DateCreated = objUserViewModel.DateCreated;
                    objUser.DateModified = objUserViewModel.DateModified;
                    objUser.AddressLine1 = objUserViewModel.AddressLine1;
                    objUser.AddressLine2 = objUserViewModel.AddressLine2;
                    //objUser.Address.CountryId = objUserViewModel.CountryId;
                    //objUser.Address.StateId = objUserViewModel.StateId;
                    // objUser.Address.CityId = objUserViewModel.CityId;
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
                    User objUser = obj.Users.Find(id);
                    obj.Users.Remove(objUser);
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
        /// Super Admin can see the details of user
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





        





