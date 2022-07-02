using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlatsManagement.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if(Session["u_id"]!=null && Session["u_name"] != null)
            {
                Session["u_id"] = null;
                Session["u_name"] = null;
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}