using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentCourseManagement.Models
{
    public class StudentUser : IdentityUser
    {
        public StudentUser()
        {
            Courses = new HashSet<Course>();
        }
        public int RollNo { get; set; }
        public string Address { get; set; }
        public string Sex { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}