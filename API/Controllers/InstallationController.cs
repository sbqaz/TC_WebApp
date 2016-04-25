using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using API.Models;
using WebLib.DependencyInjection;
using WebLib.Models;

namespace API.Controllers
{
    public class InstallationController : ApiController
    {
        private IAppContext db = new ApiContext();

        public InstallationController() { }

        public InstallationController(IAppContext context)
        {
            db = context;
        }

        // GET: api/Installation
        public IQueryable<Installation> GetInstallations()
        {
            return db.Installations;
        }

        // GET: api/Installation/5
        [ResponseType(typeof(Installation))]
        public IHttpActionResult GetInstallation(long id)
        {
            Installation installation = db.Installations.Find(id);
            if (installation == null)
            {
                return NotFound();
            }

            return Ok(installation);
        }

        // PUT: api/Installation/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInstallation(long id, Installation installation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != installation.Id)
            {
                return BadRequest();
            }

           // db.Entry(installation).State = EntityState.Modified;
            db.MarkAsModified(installation);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstallationExists(id))
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

        // POST: api/Installation
        [ResponseType(typeof(Installation))]
        public IHttpActionResult PostInstallation(Installation installation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Installations.Add(installation);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = installation.Id }, installation);
        }

        // DELETE: api/Installation/5
        [ResponseType(typeof(Installation))]
        public IHttpActionResult DeleteInstallation(long id)
        {
            Installation installation = db.Installations.Find(id);
            if (installation == null)
            {
                return NotFound();
            }

            db.Installations.Remove(installation);
            db.SaveChanges();

            return Ok(installation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InstallationExists(long id)
        {
            return db.Installations.Count(e => e.Id == id) > 0;
        }
    }
}