using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using API.Models;
using Microsoft.AspNet.Identity;
using WebLib.DependencyInjection;
using WebLib.Models;

namespace API.Controllers
{
    [Authorize]
    [RoutePrefix("api/Case")]
    public class CaseController : ApiController
    {
        private IAppContext db = new ApiContext();

        public CaseController() { }

        public CaseController(IAppContext context)
        {
            db = context;
        }

        /// <summary>
        /// Gives last 1000 cases
        /// </summary>
        public IQueryable<Case> GetCases()
        {
            return db.Cases.Take(1000);
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

        // GET: api/GetMyCases
        [Route("MyCases")]
        public IQueryable<Case> GetMyCases()
        {
            var @case = from c in db.Cases
                          where c.Worker == RequestContext.Principal.Identity.GetUserId()
                          select new Case()
                          {
                              Id = c.Id,
                              InstallationId = c.InstallationId,
                              Status = c.Status,
                              Worker = c.Worker,
                              Time = c.Time,
                              Observer = c.Observer,
                              ErrorDescription = c.ErrorDescription,
                              MadeRepair = c.MadeRepair
                          };

            return @case;
        }

        // PUT: api/Case/ClaimCase
        [Route("ClaimCase")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutClaimCase(long id)
        {
            Case temp = db.Cases.Find(id);
            temp.Worker = RequestContext.Principal.Identity.GetUserId();
            db.MarkAsModified(temp);

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
            
            // Check if case status changed
            if (@case.Status != db.Cases.Find(id).Status)
            {
                Notification noti = new Notification();
                noti.Msg = noti.BuildStatusChangedCase(db.Installations.Find(@case.InstallationId).Name,
                    db.Installations.Find(@case.InstallationId).Address, db.Cases.Find(@case.Id).Status, @case.Status);
            }

            SqlConnection con = new SqlConnection("DefaultConnection");
            switch (@case.Status)
            {
                case (int)Case.CaseStatus.created:
                    db.Installations.Find(@case.InstallationId).Status = (int)Installation.InstalStatus.Red;
                    break;
                case (int)Case.CaseStatus.started:
                    db.Installations.Find(@case.InstallationId).Status = (int)Installation.InstalStatus.Red;
                    break;
                case (int)Case.CaseStatus.pending:
                    SqlCommand cmd = new SqlCommand("SELECT Id FROM dbo.Cases WHERE InstallationId=@insId AND Status=1", con);

                    db.Installations.Find(@case.InstallationId).Status = (int)Installation.InstalStatus.Yellow;
                    break;
                case (int)Case.CaseStatus.done:
                    db.Installations.Find(@case.InstallationId).Status = (int)Installation.InstalStatus.Green;
                    break;
            }

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

            @case.Status = (int)Case.CaseStatus.created;
            db.Installations.Find(@case.InstallationId).Status = (int)Installation.InstalStatus.Red;

            @case.Time = DateTime.Now;
            db.Cases.Add(@case);
            
            Notification noti = new Notification();
            noti.Msg = noti.BuildNewCaseString(db.Installations.Find(@case.InstallationId).Name, db.Installations.Find(@case.InstallationId).Address);
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