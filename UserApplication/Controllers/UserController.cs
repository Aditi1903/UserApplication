using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
//using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UserApplication.Models;

namespace UserApplication.Controllers
{
    public class UserController : Controller
    {
        private UserDbContext obj = new UserDbContext();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Registration form
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult RegistrationForm()
        {
            //Dropdown for Role List
            List<Role> List = obj.Roles.Where(u => u.RoleId != 1 && u.RoleId != 2).ToList();
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
        /// Registration Form
        /// </summary>
        /// <param name="userViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RegistrationForm(UserViewModel userViewModel)
        {

            ////Dropdown for Role List
            //List < Role > List = obj.Roles.Where(u => u.RoleId != 1 && u.RoleId != 2).ToList();
            //ViewBag.RoleList = new SelectList(List, "RoleId", "RoleName");

            ////Dropdown for Course List
            //List<Course> Lists = obj.Courses.ToList();
            //ViewBag.CourseLists = new SelectList(Lists, "CourseId", "CourseName");

            ////Dropdown for Country List
            //List<Country> List1 = obj.Countries.ToList();
            //ViewBag.CountryList1 = new SelectList(List1, "CountryId", "CountryName");

            userViewModel.AddressId = 1;

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
           // user.Password = userViewModel.Password;
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

            user.IsActive = true;

            obj.UserInRoles.Add(userInRole);
            obj.SaveChanges();

            return RedirectToAction("Login");
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
        /// Bind the country
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
        /// Bind the state
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
        /// Bind the city
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
        /// GET:Login form
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// POST:Login form
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(User user)
        {
            var LoginDetails = obj.Users.Where(u => u.Email == user.Email && u.Password == user.Password).FirstOrDefault();
            Session["Login"] = LoginDetails;
            
                if (LoginDetails != null)
                if (LoginDetails.RoleId == 1)
                {
                    return RedirectToAction("UserList", "SuperAdmin");
                }
                else if (LoginDetails.RoleId == 2)
                {
                    return RedirectToAction("UserList", "Admin");
                }
                else if (LoginDetails.RoleId == 3)
                {
                    Session["User"] = LoginDetails;
                    return RedirectToAction("TeacherDetail", "Teacher",new { id = LoginDetails.UserId });
                }
                else
                {
                    Session["User"] = LoginDetails;
                    return RedirectToAction("StudentDetail", "Student",new { id = LoginDetails.UserId });
                }

                 return View("Login");
        }
        /// <summary>
        /// Logout
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOut()
        {

            Response.AddHeader("Cache-Control", "no-cache, no-store,must-revalidate");
            Response.AddHeader("Pragma", "no-cache");
            Response.AddHeader("Expires", "0");
            Session.Abandon();
            Session.Clear();
            Response.Cookies.Clear();
            Session.RemoveAll();
            Session["Login"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }
        
    }
}

        

    
    