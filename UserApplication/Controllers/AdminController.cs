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

namespace UserApplication.Controllers
{
    public class AdminController : Controller
    {
        private UserDbContext obj = new UserDbContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UserList()
        {
            //var list = obj.Users.Where(u =>u.RoleId == 2 && u.RoleId == 3 && u.RoleId == 4).ToList();
            //return View(list);
            var list = obj.Users.Where(u => u.RoleId != 1).ToList();
            return View(list);
        }
        /// <summary>
        /// GET:Admin can create user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateUser()
        {
            //Dropdown for Role List
            List<Role> List = obj.Roles.ToList();
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
        /// POST:Admin can create user
        /// </summary>
        /// <param name="userViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateUser(UserViewModel userViewModel)
        {

            //Dropdown for Role List
            List<Role> List = obj.Roles.ToList();
            ViewBag.RoleList = new SelectList(List, "RoleId", "RoleName");

            //Dropdown for Course List
            List<Course> Lists = obj.Courses.ToList();
            ViewBag.CourseLists = new SelectList(Lists, "CourseId", "CourseName");

            //Dropdown for Country List
            List<Country> List1 = obj.Countries.ToList();
            ViewBag.CountryList1 = new SelectList(List1, "CountryId", "CountryName");

            userViewModel.AddressId = 1;
            userViewModel.UserId = 1;

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
            //Binding the fields of user table
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
        /// Get all country
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
        /// Get all state
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
        /// Get all city
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
        ///  GET:Admin can edit the user details
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
        ///  POST:Admin can edit the user details
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

                    //  obj.Users.Add(objUser);
                    obj.SaveChanges();
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
        /// Admin can see the details of user
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
        /// <summary>
        /// GET:Admin can remove user
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
        /// POST:Admin can remove user
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


    }
}