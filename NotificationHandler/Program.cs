using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Mail;


namespace NotificationHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection con = new SqlConnection(@"Data Source=db.trafficcontrol.dk;Initial Catalog=Identity;Integrated Security=False;User ID=API;Password=phantom161;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            SqlCommand cmd = new SqlCommand("SELECT Email FROM dbo.AspNetUsers WHERE dbo.AspNetUsers.EmailNotification='1'", con);
            
            List<string> reciver = new List<string>();
            List<long> msgToDelete = new List<long>();

            // Mail setup
            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = "smtp.mandrillapp.com";
            client.Credentials = new System.Net.NetworkCredential("jn@wnb.dk", "CuXf9bDJdD0L3E8eWcXQ4w");


            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();

            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    reciver.Add(rdr["Email"].ToString());
                }
            }
            rdr.Close();

            cmd = new SqlCommand("SELECT * FROM dbo.Notifications", con);
            rdr = cmd.ExecuteReader();
            bool sendtStatus = false;
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    foreach (var r in reciver)
                    {
                        sendtStatus = true;
                        MailMessage mail = new MailMessage("TrafficControl <noreply@trafficcontrol.dk>", r);
                        mail.Subject = "Ny sag oprettet";
                        mail.Body = rdr["Msg"].ToString();
                        try
                        {
                            client.Send(mail);
                        }
                        catch (Exception e)
                        {
                            MailMessage errorMail = new MailMessage("TrafficControl <noreply@trafficcontrol.dk>", "mb@wnb.dk");
                            errorMail.Subject = "Fejl TrafficControl";
                            errorMail.Body = e.ToString();
                            try
                            {
                                client.Send(errorMail);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.ToString());
                            }
                            sendtStatus = false;
                        }
                    }
                    if (sendtStatus)
                    {
                        msgToDelete.Add((long)rdr["Id"]);
                    }
                    
                }
            }
            rdr.Close();

            foreach (var msg in msgToDelete)
            {
                cmd = new SqlCommand("DELETE FROM dbo.Notifications WHERE Id=@Id", con);
                cmd.Parameters.AddWithValue("@Id", msg);
                cmd.ExecuteNonQuery();
            }

            con.Close();

        }
    }

}
