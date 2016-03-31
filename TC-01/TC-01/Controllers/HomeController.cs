using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TC_01.CustomFilter;

namespace TC_01.Controllers
{
    public class HomeController : Controller
    {
        [AuthLog(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }
        [AuthLog(Roles = "Admin")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

    }
}