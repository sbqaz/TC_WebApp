using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TC_Login_EmptyPRJ.Controllers
{
    [Authorize(Users = "1234")]

	public class HomeController : Controller
    {
        // GET: Home

        public ActionResult Index()
        {
            return View();
        }


    }
}