using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentCourseManagement.Models
{
    public class StudentCourseManagementDbContext : IdentityDbContext<StudentUser>
    {
        public StudentCourseManagementDbContext() : base("StudentCourseManagementCS")
        {
            Configuration.ProxyCreationEnabled = false;
        }

        public System.Data.Entity.DbSet<Course> Courses { get; set; }
    }
}