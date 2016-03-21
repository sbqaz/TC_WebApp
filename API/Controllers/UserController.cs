using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.DataAcces;
using Newtonsoft.Json.Linq;

namespace API.Controllers
{
    public class UserController : ApiController
    {
        public Dictionary<string, string> Post([FromBody] JObject u)
        {
            string username = u["username"].ToString();
            string password = u["password"].ToString();
            string name = u["name"].ToString();

            User myUser = new User(Email: username, Pass: password, Name:name);
        } 
    }
}
