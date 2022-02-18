using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace BookstoreProject
{
	public partial class LoginPage : System.Web.UI.Page
	{
        HttpCookie c;
        protected void Page_Load(object sender, EventArgs e)
        { 

			Master.FindControl("navLinks").Visible = false; //Hides the nav items at the top so users can't just leave the login page

            c = Request.Cookies["userInfo"];
            //if user is already logged in, send to browse page
            if(c != null)
            {
                Response.Redirect("~/Webpages/BrowseInventory.aspx");
            }


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // Create SQL connection object
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            // Create a SQL command Object
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;

            string cmdText = "select * from userinfo where username = '" + txtUsername.Text + "' and password = '" + txtPassword.Text + "'";

            conn.Open();
            cmd.CommandText = cmdText;
            MySqlDataReader reader = cmd.ExecuteReader();


            if (reader.HasRows)
            {
                reader.Read();
                c = new HttpCookie("userInfo");
                c["username"] = txtUsername.Text;
                c["password"] = txtPassword.Text;
                if (reader.GetInt32("AccessLevel") == 0)
                {
                    c["customerid"] = reader.GetString("customerid");
                    c["accesslevel"] = "0";
                }
                else
                {
                    c["customerid"] = "null";
                    c["accesslevel"] = "1";
                }
                c.Expires = DateTime.Now.AddMinutes(20);
                Response.Cookies.Add(c);
                Response.Redirect("~/Webpages/BrowseInventory.aspx");

            }
            else
            {
                lblInvalid.Text = "Username and/or password incorrect.";
                lblInvalid.Visible = true;
            }
            conn.Close();
        }
    }
}