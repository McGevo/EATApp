using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EATApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
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

        public ActionResult ClassView()
        {
            ViewBag.Message = "ClassView Page";

            return View();
        }

        public ActionResult StudentView()
        {
            ViewBag.Message = "StudentView Page";

            return View();
        }
    }
}