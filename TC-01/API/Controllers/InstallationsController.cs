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

namespace API.Controllers
{
    [Authorize]
    public class InstallationsController : ApiController
    {
        private APIContext db = new APIContext();

        // GET: api/Installations
        public IQueryable<Installation> GetInstallations()
        {
            return db.Installations;
        }

        // GET: api/Installations/5
        [ResponseType(typeof(Installation))]
        public async Task<IHttpActionResult> GetInstallation(long id)
        {
            Installation installation = await db.Installations.FindAsync(id);
            if (installation == null)
            {
                return NotFound();
            }

            return Ok(installation);
        }

        // PUT: api/Installations/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutInstallation(long id, Installation installation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != installation.Id)
            {
                return BadRequest();
            }

            db.Entry(installation).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
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

        // POST: api/Installations
        [ResponseType(typeof(Installation))]
        public async Task<IHttpActionResult> PostInstallation(Installation installation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Installations.Add(installation);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = installation.Id }, installation);
        }

        // DELETE: api/Installations/5
        [ResponseType(typeof(Installation))]
        public async Task<IHttpActionResult> DeleteInstallation(long id)
        {
            Installation installation = await db.Installations.FindAsync(id);
            if (installation == null)
            {
                return NotFound();
            }

            db.Installations.Remove(installation);
            await db.SaveChangesAsync();

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