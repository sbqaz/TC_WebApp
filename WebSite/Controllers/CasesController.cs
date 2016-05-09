using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebLib.Models;
using WebSite.Identity;

namespace WebSite.Controllers
{
    public class CasesController : Controller
    {																		 
		private readonly ApplicationDbContext _context;


		public CasesController(ApplicationDbContext context)
		{
			this._context = context;
		}

		// GET: Cases
		public ActionResult Index()
        {
            return View(_context.Cases.ToList());
        }

        // GET: Cases/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Case @case = _context.Cases.Find(id);
            if (@case == null)
            {
                return HttpNotFound();
            }
            return View(@case);
        }

        // GET: Cases/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,InstallationId.Id,Worker,Status,Observer,ErrorDescription,MadeRepair,UserComment")] Case @case)
        {
            if (ModelState.IsValid)
            {
				@case.InstallationId.Status = Installation.InstalStatus.Red;
				@case.Time = DateTime.Now;
                _context.Cases.Add(@case);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(@case);
        }

        // GET: Cases/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Case @case = _context.Cases.Find(id);
            if (@case == null)
            {
                return HttpNotFound();
            }
            return View(@case);
        }

        // POST: Cases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,InstallationId,Worker,Time,Status,Observer,ErrorDescription,MadeRepair,UserComment")] Case @case)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(@case).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(@case);
        }

        // GET: Cases/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Case @case = _context.Cases.Find(id);
            if (@case == null)
            {
                return HttpNotFound();
            }
            return View(@case);
        }

        // POST: Cases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Case @case = _context.Cases.Find(id);
            _context.Cases.Remove(@case);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
