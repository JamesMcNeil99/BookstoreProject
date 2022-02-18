using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Web.Configuration;

namespace BookstoreProject
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            adminLink.Visible = false;

            HttpCookie c = Request.Cookies["userInfo"];

            if(c != null)
            {
                // Create SQL connection object
                MySqlConnection conn = new MySqlConnection();
                conn.ConnectionString = WebConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

                // Create a SQL command Object
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                conn.Open();

                string cmdText = "select accesslevel from userinfo where username = '" + c["username"] + "'";

                cmd.CommandText = cmdText;

                MySqlDataReader reader = cmd.ExecuteReader();
                int accesslevel = 0;
                if (reader.Read())
                {
                    accesslevel = reader.GetInt32("accesslevel");
                }

                if(accesslevel != 0)
                {
                    adminLink.Visible = true;
                    accountLink.Visible = false;
                    cartLink.Visible = false;
                }

                reader.Close();
                conn.Close();
            }
        }
    }
}