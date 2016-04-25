using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebSite.Models
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [Required]
        [Display(Name = "RoleName")]
        public string Name { get; set; }   
    }
}