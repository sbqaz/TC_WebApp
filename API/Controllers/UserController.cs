using System;
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
using WebLib.Models;
using Microsoft.AspNet.Identity;
using WebLib.DependencyInjection;

namespace API.Controllers
{
    public class UserController : ApiController
    {
        private IAppContext db = new ApiContext();

        public UserController() { }

        public UserController(IAppContext context)
        {
            db = context;
        }

        // GET: api/User
        [ResponseType(typeof(UserDTO))]
        public IHttpActionResult GetUsers()
        {
            string UserId = RequestContext.Principal.Identity.GetUserId();
            var User = from u in db.Users
                where u.Id == UserId
                select new UserDTO()
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    PhoneNumber = u.PhoneNumber,
                    EmailNotification = u.EmailNotification,
                    SMSNotification = u.SMSNotification
                };
            return Ok(User);
        }

        /*
        // GET: api/User/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(string id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }*/

        // PUT: api/User/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string UserId = RequestContext.Principal.Identity.GetUserId();

            user.Id = UserId;

            //db.Entry(user).State = EntityState.Modified;
            db.MarkAsModified(user);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(UserId))
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

        // POST: api/User
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string UserId = RequestContext.Principal.Identity.GetUserId();

            user.Id = UserId;

            db.Users.Add(user);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        }

        // DELETE: api/User/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(string id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(string id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}