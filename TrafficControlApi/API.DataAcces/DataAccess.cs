using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.DataAcces
{
    public class DataAccess
    {
        private string _SQLCon = @"Data Source=60.sv2.dk;Initial Catalog=TrafficControl;Integrated Security=False;User ID=API;Password=phantom161;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private SqlConnection con;
        public DataAccess()
        {
            //con = new SqlConnection(_SQLCon);
            //con.Open();
        }

        public User GetUser(string email="default", string pass="default", long id = 0)
        {
            User user = null;
            SqlDataReader rdr = null;
            SqlConnection con = new SqlConnection(_SQLCon);
            try
            {
                con.Open();
                if (id != 0)
                {
                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM User WHERE id=@id", con);
                    cmd.Parameters.AddWithValue("@id", id);
                    rdr = cmd.ExecuteReader();
                }
                else if (email != "default" && pass != "default")
                {
                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM [dbo].[User] WHERE email=@email and pass=@pass", con);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@pass", pass);
                    rdr = cmd.ExecuteReader();
                }

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        var idFromDB = long.Parse(rdr["id"].ToString());
                        var emailFromDB = rdr["email"].ToString();
                        var passFromDB = rdr["pass"].ToString();
                        var nameFromDB = rdr["name"].ToString();
                        user = new User(idFromDB, emailFromDB, passFromDB, nameFromDB);
                    }
                }
                else
                {
                    user = new User(0, "default", "default", "default");
                }

            }
            catch (Exception e)
            {

                throw;
            }
            finally
            {
                con.Close();
            }
            return user;
        }

        public long CreateUser(User u)
        {
            long UserID = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("INSERT [dbo].[User](email, pass, name) OUTPUT INSERTED.id VALUES(@email, @pass, @name)", con);
                cmd.Parameters.AddWithValue("@email", u.email);
                cmd.Parameters.AddWithValue("@pass", u.pass);
                cmd.Parameters.AddWithValue("@name", u.name);
                UserID = (long) cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                
                throw;
            }
            return UserID;
        }

        public void UpdateUser(User u)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("UPDATE [dbo].[User] SET email=@email, pass=@pass, name=@name WHERE id=@id", con);
                cmd.Parameters.AddWithValue("@email", u.email);
                cmd.Parameters.AddWithValue("@pass", u.pass);
                cmd.Parameters.AddWithValue("@name", u.name);
                cmd.Parameters.AddWithValue("@id", u.id);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                
                throw;
            }
        }
    }
}
