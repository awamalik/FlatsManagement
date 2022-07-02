using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlatsManagement.Models;
using System.Data.SqlClient;

namespace FlatsManagement.Controllers
{
    public class RMCController : Controller
    {
        //Data Base Connection
        static string conStr = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=FlatsDB;Data Source=DESKTOP-8S8DEUD\SQLEXPRESS";
        SqlConnection con = new SqlConnection(conStr);
        //public static string r_stValue = "";

        private List<RMC> FlatsList(String req)
        {
            List<RMC> PaidFlatsList = new List<RMC>();
            List<RMC> PendingFlatsList = new List<RMC>();
            List<RMC> AllFlatsList = new List<RMC>();
            con.Open();
            string date = DateTime.UtcNow.ToString("MM-dd-yyyy");
            string currentMonth = date.Split('-')[2] + "-" + date.Split('-')[0];
            string q = "select * from Customers where u_id='"+Session["u_id"] +"'";
            SqlCommand cmd = new SqlCommand(q, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                RMC a = new RMC();
                a.f_id = int.Parse(sdr["f_id"].ToString());
                a.c_id = int.Parse(sdr["c_id"].ToString());
                a.c_name = sdr["c_name"].ToString();
                a.c_phone = sdr["c_phone"].ToString();
                string statusMonth = sdr["c_rentStatus"].ToString();
                if (int.Parse(statusMonth.Split('-')[1])==int.Parse(currentMonth.Split('-')[1]))
                {
                    a.r_status = "Paid";
                }
                else
                {
                    a.r_status = "Pending";
                }
                
                if (a.r_status == "Paid")
                {
                    PaidFlatsList.Add(a);
                }
                else
                    PendingFlatsList.Add(a);
                AllFlatsList.Add(a);
            }
            con.Close();
            sdr.Close();
            if (req == "Paid")
            {
                return PaidFlatsList;
            }
            else if (req == "Pending")
            {
                return PendingFlatsList;
            }
            else
                return AllFlatsList;
        }
        // GET: RMC
        public ActionResult ManageRents()
        {
            if (Session["u_id"] != null && Session["u_name"] != null)
            {
                return View(FlatsList("All"));
            }
            else
                return RedirectToAction("SignIn", "Users");
            
        }
        [HttpGet]
        public ActionResult Edit(int id, string status)
        {
            //int c_id = int.Parse(Request.QueryString["id"].ToString());
            con.Open();
            string q="Select * from Customers where c_id='"+id+"'";
            SqlCommand cmd = new SqlCommand(q, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            sdr.Read();
            RMC a = new RMC();
            a.c_id = id;
            TempData["c_id"] = a.c_id;
            a.f_id = int.Parse(sdr["f_id"].ToString());
            a.c_name = sdr["c_name"].ToString();
            a.c_phone = sdr["c_phone"].ToString();
            a.r_status = status;
            a.r_stValue = sdr["c_rentStatus"].ToString();
            TempData["r_stValue"] = a.r_stValue;
            if (status == "Pending")
            {
                string date = DateTime.UtcNow.ToString("MM-dd-yyyy");
                string statusMonth = sdr["c_rentStatus"].ToString();
                int cMonth = int.Parse(date.Split('-')[0]);
                int pMonth = int.Parse(statusMonth.Split('-')[1]);
                int rMonth = 0;
                if (cMonth > pMonth)
                {
                    rMonth=cMonth - pMonth;
                }
                else
                {
                    rMonth = (12 - pMonth) + cMonth;
                    TempData["rMonth"] = rMonth.ToString();
                }
            }
            else
            {
                int rMonth = 0;
                TempData["rMonth"] = rMonth.ToString();
            }
            
            sdr.Close();


            

            con.Close();
            return View(a);
        }



        [HttpPost]
        //public ActionResult Edit(string c_id,string r_stValue, string ainc)
        public ActionResult Edit(RMC a)
        {
            string put = TempData["r_stValue"].ToString();
            String getInc = a.inc.ToString();
            String getMonth = put.Split('-')[1].ToString();
            String getYear = put.Split('-')[0].ToString();
            int month = int.Parse(getMonth);
            if (month + int.Parse(getInc) > 12)
            {
                string addZero = "";
                int tyear = int.Parse(getYear);
                int tmonth = month + int.Parse(getInc);
                tyear++;
                tmonth = tmonth - 12;
                if (tmonth < 10)
                {
                    addZero = "0" + tmonth.ToString();
                    put = tyear.ToString() + "-" + addZero;
                }
                else
                {
                    put = tyear.ToString() + "-" + tmonth.ToString();
                }
                
            }
            else
            {
                int add = month + int.Parse(getInc);
                if (add < 10)
                {
                    string addZero = "0" + add.ToString();
                    put = getYear + "-" + add;
                }
                else
                {
                    put = getYear + "-" + month.ToString();
                }
                
            }
            string query = "update Customers set c_rentStatus='" + put.ToString() + "' from Customers where c_id= '" + int.Parse(TempData["c_id"].ToString()) + "'";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("ManageRents");
        }

            //    string query = "update Flats set f_type='" + a.f_type + "',f_rooms='" + a.f_rooms + "',f_baths='" + a.f_baths + "',,f_hall='" + a.f_hall + "',,f_kitchens='" + a.f_kitchens + "',,f_location='" + a.f_location + "' from Flats where f_id= '" + a.f_id + "'";
            //    con.Open();
            //    SqlCommand cmd = new SqlCommand(query, con);
            //    cmd.ExecuteNonQuery();
            //    con.Close();
            //    return RedirectToAction("AllFlats");
            //    //return View();
            //}

            //[HttpGet]
            //public ActionResult Update(int id, int inc, string r_stValue)
            //{
            //    Customers c = new Customers();
            //    c.c_id = id;
            //    c.c_inc = inc;
            //    c.c_rentStatus = r_stValue;
            //    return View(c);
            //}
            //[HttpPost]
            //public ActionResult Update(Customers c)
            //{
            //    int month = int.Parse(r_stValue.Split('-')[1].ToString());
            //    int add = month + inc;
            //    string put = r_stValue.Split('-')[0].ToString() + "-" + add.ToString();
            //    string query = "update Customers set c_rentStatus='" + put + "' from Customers where c_id= '" + id + "'";
            //    con.Open();
            //    SqlCommand cmd = new SqlCommand(query, con);
            //    cmd.ExecuteNonQuery();
            //    con.Close();
            //    return RedirectToAction("ManageRents","RMC");
            //}


        }
}