using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebLib.Models;

namespace WebSite.Models
{
    public class UserViewModel
    {
        public ApplicationUser User;
        public IList<string> Role;
    }
}