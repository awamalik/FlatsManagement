using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlatsManagement.Models;
using System.Data.SqlClient;

namespace FlatsManagement.Controllers
{
    public class CustomersController : Controller
    {
        //Data Base Connection
        static string conStr = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=FlatsDB;Data Source=DESKTOP-8S8DEUD\SQLEXPRESS";
        SqlConnection con = new SqlConnection(conStr);

        // GET: Customers

        public ActionResult AddCustomer()
        {
            if (Session["u_id"] != null && Session["u_name"] != null)
            {
                Session["f_id"] = Request.QueryString["f_id"];
                return View();
            }
            else
                return RedirectToAction("SignIn", "Users");
            
        }

        [HttpPost]
        public ActionResult AddCustomer(Customers a)
        {
            con.Open();
            string date = DateTime.UtcNow.ToString("MM-dd-yyyy");
            string c_sMonth = date.Split('-')[2] +"-"+date.Split('-')[0];

            int pmonth = int.Parse(date.Split('-')[0]) - 1;
            string pmValue;

            if (pmonth < 10 && pmonth>0)
                pmValue = "0" + pmonth.ToString();
            else if (pmonth == 0)
                pmValue = "12";
            else
                pmValue = pmonth.ToString();

            int pyear = int.Parse(date.Split('-')[2]);
            if (pmValue == "12")
                pyear = pyear-1;


            string c_status = pyear + "-" + pmValue;
            string query = "insert into Customers(c_name,c_cnic,c_phone,f_id,c_sMonth,c_rentStatus,u_id) Values('" + a.c_name + "','" + a.c_cnic + "','" + a.c_phone + "','" + int.Parse(Session["f_id"].ToString()) + "','" + c_sMonth + "','" + c_status + "','"+Session["u_id"]+"')";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            string query2 = "Update Flats Set f_status='" + "Rented" + "' where f_id='" + Session["f_id"] + "'";
            SqlCommand cmd2 = new SqlCommand(query2, con);
            cmd2.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("AllFlats","Flats");
        }

        

        
    }
}