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
    [Authorize]
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
            return db.Installations.Include(p => p.Position);
        }

        // GET: api/Installation/5
        [ResponseType(typeof(Installation))]
        public IHttpActionResult GetInstallation(long id)
        {
            Installation installation = db.Installations.Include(p => p.Position).SingleOrDefault(i => i.Id == id);
            if (installation == null)
            {
                return NotFound();
            }

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