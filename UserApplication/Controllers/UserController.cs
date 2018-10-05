using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
//using System.Reflection;
using System.Web;
using System.Web.Mvc;
using UserApplication.Models;

namespace UserApplication.Controllers
{
    public class UserController : Controller
    {
        private UserDbContext obj = new UserDbContext();
       

        // Registration form
        [HttpGet]                                                 
        public ActionResult Registration()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Registration(User user)
        {
            if (ModelState.IsValid)
            {
                using (UserDbContext db = new UserDbContext())
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = user.FirstName + "" + user.LastName + "" + user.Gender + "" + user.Hobbies + "" + user.Password + "" + user.Email + "" + user.DOB + "" + user.Role + "" + user.Course + "Succesfully Registered.";

            }
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            Country_Bind();
            List<Role> List = obj.Roles.ToList();
            ViewBag.RoleList = new SelectList(List, "RoleId", "RoleName");
            List<Course> Lists = obj.Courses.ToList();
            ViewBag.CourseLists = new SelectList(Lists, "CourseId", "CourseName");
            ViewBag.CountryList = new SelectList(obj.Countries, "CountryId", "CountryName");
           

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userValue"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(User userValue)
        {
           
            List<Role> List = obj.Roles.ToList();
            ViewBag.RoleList = new SelectList(List, "RoleId", "RoleName");
            List<Course> Lists = obj.Courses.ToList();
            ViewBag.CourseLists = new SelectList(Lists, "CourseId", "CourseName");

           // string strDDLValue = Request.Form["Country"].ToString();

            User select = new User();
            select.UserId = userValue.UserId;
            select.FirstName = userValue.FirstName;
            select.LastName = userValue.LastName;
            select.Gender = userValue.Gender;
            select.Hobbies = userValue.Hobbies;
            select.Password = userValue.Password;
            select.Email = userValue.Email;
            select.DOB = userValue.DOB;
            select.RoleId = userValue.RoleId;
            select.CourseId = userValue.CourseId;
            select.AddressLine1 = userValue.AddressLine1;
            select.AddressLine2 = userValue.AddressLine2;
            select.AddressId = userValue.AddressId;

            obj.Users.Add(userValue);
            obj.SaveChanges();

            int latestUserId = userValue.UserId;

            UserInRole userInRole = new UserInRole();
            userInRole.UserId = latestUserId;
            userInRole.RoleId = userValue.RoleId;

            obj.UserInRoles.Add(userInRole);
            obj.SaveChanges();

        

            
            return View(userValue);
        }


        public int GetAdressId()
        { return 0; }
        
        SqlConnection UserDbContext = new SqlConnection(ConfigurationManager.ConnectionStrings["UserDbContext"].ConnectionString);
        //Get all country
        public DataSet Get_Country()
        {

            SqlCommand com = new SqlCommand("Select * from Countries", UserDbContext);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        //Get all state
        public DataSet Get_State(string CountryId)
        {
            SqlCommand com = new SqlCommand("Select * from States where CountryId=@countryid", UserDbContext);
            com.Parameters.AddWithValue("@countryid", CountryId);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        //Get all city
        public DataSet Get_City(string StateId)
        {
            SqlCommand com = new SqlCommand("Select * from Cities where StateId=@stateid", UserDbContext);
            com.Parameters.AddWithValue("@stateid", StateId);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

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
        //public JsonResult GetStateById(int CountryId)
        //{
        //    obj.Configuration.ProxyCreationEnabled = false;
        //    return Json(obj.States.Where(p => p.CountryId == CountryId), JsonRequestBehavior.AllowGet);
        //}

    }
}