using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlatsManagement.Models;
using System.Data.SqlClient;
using System.IO;

namespace FlatsManagement.Controllers
{
    public class FlatsController : Controller
    {
        //Data Base Connection
        static string conStr = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=FlatsDB;Data Source=DESKTOP-8S8DEUD\SQLEXPRESS";
        SqlConnection con = new SqlConnection(conStr);

        // GET: Flats
        public ActionResult FlatsEntry()
        {
            if (Session["u_id"] != null && Session["u_name"] != null)
            {
                return View();
            }
            else
                return RedirectToAction("SignIn", "Users");
        }
        [HttpPost]
        public ActionResult FlatsEntry(Flats a)
        {
            if (Session["u_id"] != null && Session["u_name"] != null)
            {
                var f_status = "Available";
                var u_id = Session["u_id"].ToString();

                var fname = Path.GetFileName(a.imageFile.FileName);
                var allowExt = new[] { ".jpg", ".png", ".bmp", ".gif" };
                var fileExt = Path.GetExtension(fname);
                if (allowExt.Contains(fileExt))
                {
                    var filePath = Path.Combine(Server.MapPath("~/Images"), fname);
                    a.imageFile.SaveAs(filePath);
                    String dbPath = "/Images/" + fname;
                    con.Open();
                    string query = "insert into Flats(f_type,f_rooms,f_baths,f_hall,f_kitchens,f_location,f_image,f_status,u_id) Values('" + a.f_type + "','" + a.f_rooms + "','" + a.f_baths + "','" + a.f_hall + "','" + a.f_kitchens + "','" + a.f_location + "','" + dbPath + "','" + f_status + "','" + u_id + "')";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteReader();
                    con.Close();
                }
                //return View();
                return RedirectToAction("AllFlats", "Flats");
            }
            else
                return RedirectToAction("SignIn", "Users");
        }
        private List<Flats> FlatsList(String req)
        {
            List<Flats> AvailableFlatsList = new List<Flats>();
            List<Flats> RentedFlatsList = new List<Flats>();
            List<Flats> AllFlatsList = new List<Flats>();
            con.Open();
            string q = "select * from Flats where u_id='"+Session["u_id"]+"'";
            SqlCommand cmd = new SqlCommand(q, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                Flats a = new Flats();
                a.f_id = int.Parse(sdr["f_id"].ToString());
                a.f_type = sdr["f_type"].ToString();
                a.f_rooms = int.Parse(sdr["f_rooms"].ToString());
                a.f_baths = int.Parse(sdr["f_baths"].ToString());
                a.f_hall = int.Parse(sdr["f_hall"].ToString());
                a.f_kitchens = int.Parse(sdr["f_kitchens"].ToString());
                a.f_type = sdr["f_type"].ToString();
                a.f_location = sdr["f_location"].ToString();
                a.f_image = sdr["f_image"].ToString();
                a.f_status = sdr["f_status"].ToString();
                AllFlatsList.Add(a);
                if (sdr["f_status"].ToString() == "Rented")
                {
                    RentedFlatsList.Add(a);
                }
                else
                    AvailableFlatsList.Add(a);
            }
            con.Close();
            sdr.Close();
            if (req == "rented")
            {
                return RentedFlatsList;
            }
            else if (req == "available")
            {
                return AvailableFlatsList;
            }
            else
                return AllFlatsList;
        }

        public ActionResult Details(int f_id)
        {
            con.Open();
            string query = "select * from Flats where f_id='"+ f_id + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            sdr.Read();
            Flats a = new Flats();
            a.f_id = int.Parse(sdr["f_id"].ToString());
            a.f_type = sdr["f_type"].ToString();
            a.f_rooms = int.Parse(sdr["f_rooms"].ToString());
            a.f_baths = int.Parse(sdr["f_baths"].ToString());
            a.f_hall = int.Parse(sdr["f_hall"].ToString());
            a.f_kitchens = int.Parse(sdr["f_kitchens"].ToString());
            a.f_type = sdr["f_type"].ToString();
            a.f_location = sdr["f_location"].ToString();
            a.f_image = sdr["f_image"].ToString();
            a.f_status = sdr["f_status"].ToString();
            sdr.Close();
            con.Close();
            return View(a);
        }

        public ActionResult Edit()
        {
            con.Open();
            string query = "select * from Flats";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            sdr.Read();
            Flats a = new Flats();
            a.f_id = int.Parse(sdr["f_id"].ToString());
            a.f_type = sdr["f_type"].ToString();
            a.f_rooms = int.Parse(sdr["f_rooms"].ToString());
            a.f_baths = int.Parse(sdr["f_baths"].ToString());
            a.f_hall = int.Parse(sdr["f_hall"].ToString());
            a.f_kitchens = int.Parse(sdr["f_kitchens"].ToString());
            a.f_type = sdr["f_type"].ToString();
            a.f_location = sdr["f_location"].ToString();
            a.f_image = sdr["f_image"].ToString();
            a.f_status = sdr["f_status"].ToString();
            sdr.Close();
            con.Close();
            return View(a);
        }

        [HttpPost]
        public ActionResult Edit(Flats a)
        {
            string query = "update Flats set f_type='" + a.f_type + "',f_rooms='" + a.f_rooms + "',f_baths='" + a.f_baths + "',,f_hall='" + a.f_hall + "',,f_kitchens='" + a.f_kitchens + "',,f_location='" + a.f_location + "' from Flats where f_id= '" + a.f_id + "'";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("AllFlats");
            //return View();
        }

        [HttpGet]
        public ActionResult Delete(int f_id)
        {
            con.Open();
            string query = "Delete from Flats where f_id='" + f_id + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("AllFlats");
        }

        public ActionResult AllFlats()
        {
            if (Session["u_id"] != null && Session["u_name"] != null)
            {
                return View(FlatsList("all"));
            }
            else
                return RedirectToAction("SignIn", "Users");
            
        }

        public ActionResult AvailableFlats()
        {
            if (Session["u_id"] != null && Session["u_name"] != null)
            {
                return View(FlatsList("available"));
            }
            else
                return RedirectToAction("SignIn", "Users");
            
        }
        
        public ActionResult RentedFlats()
        {
            if (Session["u_id"] != null && Session["u_name"] != null)
            {
                return View(FlatsList("rented"));
            }
            else
                return RedirectToAction("SignIn", "Users");
            
        }

        public ActionResult DeleteCustomer(int f_id)
        {
            con.Open();
            string query = "Delete from Customers where f_id='" + f_id + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            string query2 = "Update Flats Set f_status ='" + "Available" + "' where f_id ='" + f_id + "'";
            SqlCommand cmd2 = new SqlCommand(query2, con);
            cmd2.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("RentedFlats");
        }

        //public ActionResult RemoveCustomer()
        //{
        //    Session["f_id"] = Request.QueryString["f_id"];
        //    con.Open();
        //    string query = "Delete from Customers where f_id='" + Session["f_id"] + "'";
        //    SqlCommand cmd = new SqlCommand(query, con);
        //    cmd.ExecuteNonQuery();
        //    string query2 = "Update Flats Set f_status='" + "Available" + "' where f_id='" + Session["f_id"] + "'";
        //    SqlCommand cmd2 = new SqlCommand(query2, con);
        //    cmd2.ExecuteNonQuery();
        //    con.Close();
        //    return RedirectToAction("RentedFlats", "Flats");
        //}

    }
}