using Books.Model;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Books.Web
{
    public class NotificationComponents
    {
        public void RegisterNotification(DateTime currentTime)
        {
            string conStr = ConfigurationManager.ConnectionStrings["NotificationConnection"].ConnectionString;
            string sqlCommand = 
                @"Select [BookID],[Title],[Publisher] from [dbo].[Books] where [AddOn] > @AddOn";
            using(SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand(sqlCommand,con);
                cmd.Parameters.AddWithValue("@AddOn", currentTime);
                if(con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }
                cmd.Notification = null;
                SqlDependency sqlDep = new SqlDependency(cmd);
                sqlDep.OnChange += sqlDep_Onchange;
                using(SqlDataReader reader = cmd.ExecuteReader())
                {

                }
            }
        }

        void sqlDep_Onchange(object sender, SqlNotificationEventArgs e)
        {
            if(e.Type == SqlNotificationType.Change)
            {
                SqlDependency sqlDep = sender as SqlDependency;
                sqlDep.OnChange -= sqlDep_Onchange;

                var notificationHub = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
                notificationHub.Clients.All.notify("added");

                RegisterNotification(DateTime.Now);
            }
        }
        public List<Book> GetBooks(DateTime afterDate)
        {
            using(BookSampleEntities dc = new BookSampleEntities())
            {
                return dc.Books.Where(x => x.AddOn > afterDate).OrderByDescending(x=>x.AddOn).ToList();
            }
        }
    }
}