using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentCourseManagement.Models
{
    public class Course
    {
        public Course()
        {
            this.Students = new HashSet<StudentUser>();
        }
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public float Credit { get; set; }
        public virtual ICollection<StudentUser> Students { get; set; }
    }
}