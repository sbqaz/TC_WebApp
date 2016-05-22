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
    public class PositionController : ApiController
    {
        private IAppContext db = new ApiContext();

        public PositionController() { }

        public PositionController(IAppContext context)
        {
            db = context;
        }

        /// <summary>
        /// Give all positions in the system
        /// </summary>
        /// <returns></returns>
        // GET: api/Position
        public IQueryable<Position> GetPositions()
        {
            return db.Positions;
        }

        /// <summary>
        /// Give the posion the the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Position/5
        [ResponseType(typeof(Position))]
        public IHttpActionResult GetPosition(long id)
        {
            Position position = db.Positions.Find(id);
            if (position == null)
            {
                return NotFound();
            }

            return Ok(position);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PositionExists(long id)
        {
            return db.Positions.Count(e => e.Id == id) > 0;
        }
    }
}