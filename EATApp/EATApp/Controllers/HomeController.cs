using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EATApp.Controllers
{
    public class HomeController : Controller
    {
        

        public ActionResult LoginView()
        {
            ViewBag.Message = "LoginView Page";

            return View();
        }

    }
}