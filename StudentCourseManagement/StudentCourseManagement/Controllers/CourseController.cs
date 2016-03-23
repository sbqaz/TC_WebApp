using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StudentCourseManagement.Models;
using StudentCourseManagement.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace StudentCourseManagement.Controllers
{
    public class CourseController : Controller
    {
        private StudentCourseManagementDbContext db = null;
        private UserManager<StudentUser> userManager;
        public CourseController()
        {
            db = new StudentCourseManagementDbContext();
            userManager = new UserManager<StudentUser>(new UserStore<StudentUser>(db));
        }

        // GET: /Course/
        [Authorize]
        public ActionResult Index(string message)
        {
            ViewBag.Message = message;
            return View(db.Courses.ToList());
        }

        // GET: /Course/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        [Authorize]
        public ActionResult Assign()
        {
            List<Course> totalCourse =null;
            List<Course> assignedCourse = new List<Course>();
            List<CourseViewModel> CoursesVM = new List<CourseViewModel>();

            var currentUser = userManager.FindById(User.Identity.GetUserId());

            assignedCourse = db.Users.Include("Courses").Where(u => u.Id == currentUser.Id).FirstOrDefault().Courses.ToList();

            ViewBag.UserName = currentUser.UserName;
            
            totalCourse=db.Courses.ToList();

            foreach (Course course in totalCourse)
            {
                if (!assignedCourse.Contains(course))
                {
                    CourseViewModel viewModel = new CourseViewModel();
                    viewModel.Id = course.Id;
                    viewModel.Title = course.Title;
                    viewModel.Description = course.Description;
                    viewModel.Credit = course.Credit;
                    viewModel.IsSelected = false;

                    CoursesVM.Add(viewModel);
                }
            }

            return View(CoursesVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Assign(IEnumerable<CourseViewModel> assignVeiwModel)
        {
            var currentUser = userManager.FindById(User.Identity.GetUserId());
            if(currentUser !=null)
            {
                var user = db.Users.Include("Courses").Where(u => u.Id == currentUser.Id).FirstOrDefault();
                foreach (CourseViewModel courseVM in assignVeiwModel)
                {
                    if (courseVM.IsSelected)
                    {
                        Course course = user.Courses.Where(c => c.Id == courseVM.Id).FirstOrDefault();
                        if(course == null)
                        {
                            Course courseAdd = db.Courses.Where(c => c.Id == courseVM.Id).FirstOrDefault();
                            currentUser.Courses.Add(courseAdd);
                        }
                    }
                }
                db.SaveChanges();
            }
            return RedirectToAction("AssignedCourses");
        }
        [Authorize]
        public ActionResult AssignedCourses()
        {
            var currentUser = userManager.FindById(User.Identity.GetUserId());
            if (currentUser != null)
            {
                var user = db.Users.Include("Courses").Where(u => u.Id == currentUser.Id).FirstOrDefault();
                return View(user.Courses);
            }
            return RedirectToAction("Assign");
        }

        // GET: /Course/Create
        [Authorize(Users = "sb")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Course/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include="Id,Title,Description,Credit")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(course);
        }

        // GET: /Course/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: /Course/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Title,Description,Credit")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(course);
        }

        // GET: /Course/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: /Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Include("Students").Where(c => c.Id == id).FirstOrDefault();
            if (course.Students.Count == 0)
            {
                db.Courses.Remove(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {

                return RedirectToAction("Index", new { message="The Course is Assigned, can't delete" });
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
