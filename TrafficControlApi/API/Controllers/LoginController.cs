using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;
using API.DataAcces;
using Newtonsoft.Json.Linq;

namespace API.Controllers
{
    public class LoginController : ApiController
    {
        
        public Dictionary<string, string> Post([FromBody] JObject u)
        {
            string username = u["username"].ToString();
            string password = u["password"].ToString();
            
            DataAccess DA = new DataAccess();
            User myuser = DA.GetUser(email: username, pass: password);
            var Dictionary = new Dictionary<string, string>();

            if (myuser.id != 0)
            {
                Dictionary.Add("Succes", "True");
                Dictionary.Add("Name", myuser.name);
            }
            else
            {
                Dictionary.Add("Succes", "False");
            }
            return Dictionary;
        }
    }
}
