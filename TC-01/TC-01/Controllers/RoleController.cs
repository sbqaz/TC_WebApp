using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//Added below - For Role creation
using Microsoft.AspNet.Identity.EntityFramework;
using TC_01.Models;

namespace TC_01.Controllers
{
    public class RoleController : Controller
    {
        ApplicationDbContext _context;

        public RoleController()
        {
            _context = new ApplicationDbContext();
        }

        //GET: All Roles
        public ActionResult RoleIndex()
        {
            var Roles = _context.Roles.ToList();
            return View(Roles);
        }
        
        //Create a New Role
        public ActionResult Create()
        {
            var Role = new IdentityRole();
            return View(Role);
        }

        //Create a New Role - POST
        [HttpPost]
        public ActionResult Create(IdentityRole Role)
        {
            _context.Roles.Add(Role);
            _context.SaveChanges();
            return RedirectToAction("RoleIndex");
        }
    }
}