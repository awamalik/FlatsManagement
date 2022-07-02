using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlatsManagement.Models;
using System.Data.SqlClient;


namespace FlatsManagement.Controllers
{
    public class UsersController : Controller
    {
        //Data Base Connection
        static string conStr = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=FlatsDB;Data Source=DESKTOP-8S8DEUD\SQLEXPRESS";
        SqlConnection con = new SqlConnection(conStr);

        // GET: Users
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(Users a)
        {
            string u_id = a.u_email.Split('@')[0];
            con.Open();
            string query = "insert into Users Values('" + u_id + "','" + a.u_name + "','" + a.u_email + "','" + a.u_password + "','" + a.online + "')";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteReader();
            con.Close();
            TempData["success"] = "Signed up Successfully! SignIn here!";
            //return View();
            return RedirectToAction("SignIn");
        }

        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(Users a)
        {
            con.Open();
            string query = " select u_id,u_name from Users where u_id='" + a.u_id + "' and u_password='" + a.u_password + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            sdr.Read();
            if (sdr.HasRows)
            {
                Session["u_id"] = sdr["u_id"].ToString();
                Session["u_name"] = sdr["u_name"].ToString();

            }
            else
            {
                TempData["failed"] = "Incorrect ID or Password! Try Again.";
                return RedirectToAction("SignIn");
            }
            sdr.Close();
            con.Close();
            return RedirectToAction("UserHome");
        }
        public ActionResult UserHome()
        {
            if (Session["u_id"] != null && Session["u_name"] != null)
            {
                return View();

            }
            return RedirectToAction("SignIn");
        }
    }
}