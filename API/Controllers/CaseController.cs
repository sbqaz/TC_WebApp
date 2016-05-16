using System;
using System.Collections.Generic;
using System.Configuration;
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
using Microsoft.Ajax.Utilities;
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
        /// <returns></returns>
        // GET: api/Case
        public IQueryable<Case> GetCases()
        {
            return db.Cases.Include(i => i.InstallationId).Include(p => p.InstallationId.Position).Take(1000);
        }

        /// <summary>
        /// Gives 1 case
        /// </summary>
        /// <param name="id">Id for the case</param>
        /// <returns>Case whit the given id</returns>
        // GET: api/Case/5
        [ResponseType(typeof(Case))]
        public IHttpActionResult GetCase(long id)
        {
            Case @case = db.Cases.Include(i => i.InstallationId)
                .Include(p => p.InstallationId.Position)
                .SingleOrDefault(c => c.Id == id);

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
            var uID = RequestContext.Principal.Identity.GetUserId();
            var @case = db.Cases.Include(i => i.InstallationId)
                .Include(p => p.InstallationId.Position)
                .Where(c => c.Worker == uID);
           
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
        [ResponseType(typeof(CaseDTO))]
        public IHttpActionResult PutCase(long id, CaseDTO @case)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != @case.Id)
            {
                return BadRequest();
            }

            if (db.Cases.Count(c => c.Status != @case.Status && c.InstallationId.Id == @case.Installation) > 0)
            {
                Notification noti = new Notification();
                noti.Msg = noti.BuildStatusChangedCase(db.Installations.SingleOrDefault(i => i.Id == @case.Installation).Name, db.Cases.SingleOrDefault(c => c.Id == id).Status, @case.Status);
                db.Notifications.Add(noti);
                db.SaveChanges();
            }

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            SqlDataReader rdr = null;
            SqlCommand cmd = null;

            cmd = new SqlCommand(@"UPDATE dbo.Cases SET Worker=@Worker, Status=@Status, Observer=@Observer, ErrorDescription=@ErrorDescription, MadeRepair=@MadeRepair, UserComment=@UserComment " +
                                "WHERE Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Worker", @case.Worker);
            cmd.Parameters.AddWithValue("@Status", @case.Status);
            cmd.Parameters.AddWithValue("@Observer", (int)@case.Observer);
            cmd.Parameters.AddWithValue("@ErrorDescription", @case.ErrorDescription);
            cmd.Parameters.AddWithValue("@MadeRepair", @case.MadeRepair);
            cmd.Parameters.AddWithValue("@UserComment", @case.UserComment);
            con.Open();
            rdr = cmd.ExecuteReader();
            rdr.Close();

            cmd = new SqlCommand("SELECT Id FROM dbo.Cases WHERE InstallationId_Id=@insId AND (Status=@status1 OR Status=@status2)", con);
            cmd.Parameters.AddWithValue("@insId", @case.Installation);
            cmd.Parameters.AddWithValue("@status1", (int)Case.CaseStatus.started);
            cmd.Parameters.AddWithValue("@status2", (int)Case.CaseStatus.created);

            Installation tmpInstallation = db.Installations.SingleOrDefault(i => i.Id == @case.Installation);

            switch (@case.Status)
            {
                case Case.CaseStatus.created:
                    tmpInstallation.Status = Installation.InstalStatus.Red;
                    break;
                case Case.CaseStatus.started:
                    tmpInstallation.Status = Installation.InstalStatus.Red;
                    break;
                case Case.CaseStatus.pending:
                    rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                        break;
                    tmpInstallation.Status = Installation.InstalStatus.Yellow;
                    break;
                case Case.CaseStatus.done:
                    rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                        break;
                    tmpInstallation.Status = Installation.InstalStatus.Green;
                    break;
            }
            db.MarkAsModified(tmpInstallation);
            db.SaveChanges();

            con.Close();
            return CreatedAtRoute("DefaultApi", new { id = @case.Id }, @case);
        }

        // POST: api/Case
        [ResponseType(typeof(CaseDTO))]
        public IHttpActionResult PostCase(CaseDTO @case)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            @case.Time=DateTime.Now;
            
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand("INSERT INTO dbo.Cases (Worker, Time, Status, Observer, ErrorDescription, MadeRepair, UserComment, InstallationId_Id) " +
                                            "OUTPUT INSERTED.Id "+
                                            "VALUES (@Worker, @Time, @Status, @Observer, @ErrorDescription, @MadeRepair, @UserComment, @InstallationId_Id)", con);
            cmd.Parameters.AddWithValue("@Worker", @case.Worker ?? "");
            cmd.Parameters.AddWithValue("@Status", (int)Case.CaseStatus.created);
            cmd.Parameters.AddWithValue("@Time", @case.Time);
            cmd.Parameters.AddWithValue("@Observer", (int)@case.Observer);
            cmd.Parameters.AddWithValue("@ErrorDescription", @case.ErrorDescription ?? "");
            cmd.Parameters.AddWithValue("@MadeRepair", @case.MadeRepair ?? "");
            cmd.Parameters.AddWithValue("@UserComment", @case.UserComment ?? "");
            cmd.Parameters.AddWithValue("@InstallationId_Id", (long)@case.Installation);
            con.Open();
            @case.Id = (long)cmd.ExecuteScalar();

            cmd = new SqlCommand(@"UPDATE dbo.Installations SET Status=@Status WHERE Id=@Id", con);
            cmd.Parameters.AddWithValue("@Status", (int) Installation.InstalStatus.Red);
            cmd.Parameters.AddWithValue("Id", @case.Installation);
            SqlDataReader rdr = cmd.ExecuteReader();
            con.Close();

            Notification noti = new Notification();
            noti.Msg = noti.BuildNewCaseString(db.Installations.Find(db.Cases.Find(@case.Id).InstallationId).Name);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = @case.Id }, @case);
        }
        /*
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
        */
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