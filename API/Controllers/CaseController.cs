﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using API.Models;
using WebLib.DependencyInjection;
using WebLib.Models;

namespace API.Controllers
{
    public class CaseController : ApiController
    {
        private IAppContext db = new ApiContext();

        public CaseController() { }

        public CaseController(IAppContext context)
        {
            db = context;
        }

       // GET: api/Case
        public IQueryable<Case> GetCases()
        {
            return db.Cases;
        }

        // GET: api/Case/5
        [ResponseType(typeof(Case))]
        public IHttpActionResult GetCase(long id)
        {
            Case @case = db.Cases.Find(id);
            if (@case == null)
            {
                return NotFound();
            }

            return Ok(@case);
        }

        // PUT: api/Case/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCase(long id, Case @case)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != @case.Id)
            {
                return BadRequest();
            }

            //db.Entry(@case).State = EntityState.Modified;
            db.MarkAsModified(@case);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CaseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Case
        [ResponseType(typeof(Case))]
        public IHttpActionResult PostCase(Case @case)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cases.Add(@case);
            db.SaveChanges();

            Notification noti = new Notification();
            noti.Msg = "Der er oprettet en ny sag på lyskrydet: \n" + db.Installations.Find(@case.InstallationId).Name + " \n" + db.Installations.Find(@case.InstallationId).Address;

            db.Notifications.Add(noti);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = @case.Id }, @case);
        }

        // DELETE: api/Case/5
        [ResponseType(typeof(Case))]
        public IHttpActionResult DeleteCase(long id)
        {
            Case @case = db.Cases.Find(id);
            if (@case == null)
            {
                return NotFound();
            }

            db.Cases.Remove(@case);
            db.SaveChanges();

            return Ok(@case);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CaseExists(long id)
        {
            return db.Cases.Count(e => e.Id == id) > 0;
        }
    }
}