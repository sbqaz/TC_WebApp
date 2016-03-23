using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentCourseManagement.ViewModels
{
    public class CourseViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public float Credit { get; set; }
        public bool IsSelected { get; set; }
    }
}