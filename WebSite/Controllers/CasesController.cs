using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.EnterpriseServices;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using WebLib.Models;
using WebSite.Identity;

namespace WebSite.Controllers
{
	[Authorize]
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

			var cases = _context.Cases.Include(i => i.InstallationId).Take(1000).ToList();
			SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
			SqlDataReader rdr = null;
			SqlCommand cmd = null;
			con.Open();
			foreach (var c in cases)
			{
				if (c.Worker != "")
				{
					cmd = new SqlCommand(@"SELECT FirstName, LastName FROM dbo.AspNetUsers WHERE Id=@Id", con);
					cmd.Parameters.AddWithValue("@Id", c.Worker);
					rdr = cmd.ExecuteReader();
					if (rdr.HasRows && rdr.Read())
					{
						c.Worker = rdr["FirstName"].ToString() + " " + rdr["LastName"].ToString();
					}
					rdr.Close();
				}
            

			}
			con.Close();


			return View(cases);
        }

        // GET: Cases/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			Case @case = _context.Cases.Include(i => i.InstallationId)
				.Include(p => p.InstallationId.Position)
				.SingleOrDefault(c => c.Id == id);

			if (@case == null)
			{
				return HttpNotFound();
			}

			if (@case.Worker != "")
			{
				SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
				SqlDataReader rdr = null;
				SqlCommand cmd = null;
				con.Open();

				cmd = new SqlCommand(@"SELECT FirstName, LastName FROM dbo.AspNetUsers WHERE Id=@Id", con);
				cmd.Parameters.AddWithValue("@Id", @case.Worker);
				rdr = cmd.ExecuteReader();
				if (rdr.HasRows && rdr.Read())
				{
					@case.Worker = rdr["FirstName"].ToString() + " " + rdr["LastName"].ToString();
				}
				con.Close();
			}
            return View(@case);
        }

        // GET: Cases/Create
        public ActionResult Create()
        {
			ViewBag.Installation = new SelectList(_context.Installations, "Id", "Id");
			return View();
        }

        // POST: Cases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InstallationId,Worker,Status,Observer,ErrorDescription,MadeRepair,UserComment")] Case @case)
        {
			if (ModelState.IsValid)
            {
				SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
				SqlCommand cmd = new SqlCommand("INSERT INTO dbo.Cases (Worker, Time, Status, Observer, ErrorDescription, MadeRepair, UserComment, InstallationId_Id) " +
												"OUTPUT INSERTED.Id " +
												"VALUES (@Worker, @Time, @Status, @Observer, @ErrorDescription, @MadeRepair, @UserComment, @InstallationId_Id)", con);
				cmd.Parameters.AddWithValue("@Worker", "");
				cmd.Parameters.AddWithValue("@Status", (int)Case.CaseStatus.created);
				cmd.Parameters.AddWithValue("@Time", DateTime.Now);
				cmd.Parameters.AddWithValue("@Observer", (int)@case.Observer);
				cmd.Parameters.AddWithValue("@ErrorDescription", @case.ErrorDescription ?? "");
				cmd.Parameters.AddWithValue("@MadeRepair", "");
				cmd.Parameters.AddWithValue("@UserComment", "");
				cmd.Parameters.AddWithValue("@InstallationId_Id", @case.InstallationId.Id);
				con.Open();
				@case.Id = (long)cmd.ExecuteScalar();

				cmd = new SqlCommand(@"UPDATE dbo.Installations SET Status=@Status WHERE Id=@Id", con);
				cmd.Parameters.AddWithValue("@Status", (int)Installation.InstalStatus.Red);
				cmd.Parameters.AddWithValue("@Id", @case.InstallationId.Id);
				SqlDataReader rdr = cmd.ExecuteReader();
				rdr.Close();

				Notification noti = new Notification();
				cmd = new SqlCommand(@"SELECT Name FROM dbo.Installations WHERE Id=@Id", con);
				cmd.Parameters.AddWithValue("@ID", @case.InstallationId.Id);
				rdr = cmd.ExecuteReader();

				if (rdr.HasRows && rdr.Read())
				{
					noti.Msg = noti.BuildNewCaseString(rdr["Name"].ToString());
				}
				_context.Notifications.Add(noti);
				_context.SaveChanges();
				con.Close();
                return RedirectToAction("Index");
            }

			ViewBag.Installation = new SelectList(_context.Installations, "Id", "Id", @case.InstallationId);

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
			ViewBag.Installation = new SelectList(_context.Installations, "Id", "Id");

			return View(@case);
        }

        // POST: Cases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Case @case)
        {
			if (ModelState.IsValid)
	        {

		        if (_context.Cases.Count(c => c.Status != @case.Status && c.InstallationId.Id == @case.InstallationId.Id) > 0)
		        {
			        Notification noti = new Notification();
			        noti.Msg =
				        noti.BuildStatusChangedCase(
					        _context.Installations.SingleOrDefault(i => i.Id == @case.InstallationId.Id).Name,
					        _context.Cases.SingleOrDefault(c => c.Id == @case.Id).Status, @case.Status);
			        _context.Notifications.Add(noti);
			        _context.SaveChanges();
		        }

		        SqlConnection con =
			        new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
		        SqlDataReader rdr = null;
		        SqlCommand cmd = null;

		        if (@case.Worker == "")
		        {
			        cmd =
				        new SqlCommand(
					        @"UPDATE dbo.Cases SET Worker=@Worker, Status=@Status, Observer=@Observer, ErrorDescription=@ErrorDescription, MadeRepair=@MadeRepair, UserComment=@UserComment " +
					        "WHERE Id=@Id", con);
			        cmd.Parameters.AddWithValue("@Worker", "");
		        }
		        else
		        {
			        cmd =
				        new SqlCommand(
					        @"UPDATE dbo.Cases SET Status=@Status, Observer=@Observer, ErrorDescription=@ErrorDescription, MadeRepair=@MadeRepair, UserComment=@UserComment " +
					        "WHERE Id=@Id", con);
		        }

		        cmd.Parameters.AddWithValue("@Id", @case.Id);
		        cmd.Parameters.AddWithValue("@Status", (int) @case.Status);
		        cmd.Parameters.AddWithValue("@Observer", (int) @case.Observer);
		        cmd.Parameters.AddWithValue("@ErrorDescription", @case.ErrorDescription ?? "");
		        cmd.Parameters.AddWithValue("@MadeRepair", @case.MadeRepair ?? "");
		        cmd.Parameters.AddWithValue("@UserComment", @case.UserComment ?? "");
		        con.Open();
		        rdr = cmd.ExecuteReader();
		        rdr.Close();

		        cmd =
			        new SqlCommand(
				        "SELECT Id FROM dbo.Cases WHERE InstallationId_Id=@insId AND (Status=@status1 OR Status=@status2)", con);
		        cmd.Parameters.AddWithValue("@insId", @case.InstallationId.Id);
		        cmd.Parameters.AddWithValue("@status1", (int) Case.CaseStatus.started);
		        cmd.Parameters.AddWithValue("@status2", (int) Case.CaseStatus.created);

		        Installation tmpInstallation = _context.Installations.SingleOrDefault(i => i.Id == @case.InstallationId.Id);

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
		        _context.Entry(tmpInstallation).State = EntityState.Modified;
		        _context.SaveChanges();

		        con.Close();

		        return RedirectToAction("Index");
	        }

			ViewBag.Installation = new SelectList(_context.Installations, "Id", "Id", @case.InstallationId);
			return View(@case);
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
