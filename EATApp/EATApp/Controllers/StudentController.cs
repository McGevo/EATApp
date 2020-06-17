using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EATApp.Controllers
{
    public class StudentController : Controller
    {
        public ActionResult SignInView()
        {
            ViewBag.Message = "SignInView Page";

            return View();
        }
    }
}