using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EATApp.Controllers
{
    public class TeacherController : Controller
    {
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