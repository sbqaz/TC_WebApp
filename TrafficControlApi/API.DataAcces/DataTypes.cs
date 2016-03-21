using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.DataAcces
{
    public class User
    {
        public long id {get; set; }

        public string email { get; set; }

        public string pass { get; set; }

        public string name { get; set; }

        public User(long ID=0, string Email="default", string Pass="defalut", string Name="default")
        {
            id = ID;
            email = Email;
            pass = Pass;
            name = Name;
        }
    }
}
