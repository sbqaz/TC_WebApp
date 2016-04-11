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
    public class CasesController : ApiController
    {
        private APIContext db = new APIContext();

        // GET: api/Cases
        public IQueryable<Case> GetCases()
        {
            return db.Cases;
        }

        // GET: api/Cases/5
        [ResponseType(typeof(Case))]
        public async Task<IHttpActionResult> GetCase(long id)
        {
            Case @case = await db.Cases.FindAsync(id);
            if (@case == null)
            {
                return NotFound();
            }

            return Ok(@case);
        }

        // PUT: api/Cases/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCase(long id, Case @case)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != @case.Id)
            {
                return BadRequest();
            }

            db.Entry(@case).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
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

        // POST: api/Cases
        [ResponseType(typeof(Case))]
        public async Task<IHttpActionResult> PostCase(Case @case)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cases.Add(@case);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = @case.Id }, @case);
        }

        // DELETE: api/Cases/5
        [ResponseType(typeof(Case))]
        public async Task<IHttpActionResult> DeleteCase(long id)
        {
            Case @case = await db.Cases.FindAsync(id);
            if (@case == null)
            {
                return NotFound();
            }

            db.Cases.Remove(@case);
            await db.SaveChangesAsync();

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