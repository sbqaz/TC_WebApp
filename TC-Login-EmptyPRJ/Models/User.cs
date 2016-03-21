using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TrafficControl.DAL;

namespace TC_Login_EmptyPRJ.Models
{
    public class User
    {

        //private ITCApi ValidateLogin;

        //public User(ITCApi validateLogin)
        //{
        //    ValidateLogin = validateLogin;
        //}


        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember on this computer")]
        public bool RememberMe { get; set; }

        public bool IsValid(string _username, string _password)
        {
            TCApi ValidateLogin = new TCApi();
            if (ValidateLogin.LogIn(_username, _password))
                return true;
            else
                return false;
        }
    }
}