using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace BookstoreProject.Webpages
{
    public partial class AccountCreate : System.Web.UI.Page
    {
        HttpCookie c;

        protected void Page_Load(object sender, EventArgs e)
        {
            Master.FindControl("navLinks").Visible = false;

            c = Request.Cookies["userInfo"];

            //If user is logged in and clicks make an account
            //any previously saved log in information is erased 
            if (c != null)
            {
                c.Expires = DateTime.Now.AddMinutes(-20);
                Response.Cookies.Add(c);
            }


        }

        bool checkUniqueUsername()
        {
            // Create SQL connection object
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            // Create a SQL command Object
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            conn.Open();

            string cmdText = "Select username from userinfo where username like '" + tbxUsername.Text + "';";

            cmd.CommandText = cmdText;
            MySqlDataReader reader = cmd.ExecuteReader();

            //if any usernames match entered username, returns false
            if (reader.HasRows)
            {
                lblWarning.Text = "Username is already taken.";
                lblWarning.Visible = true;
                return false;
            }
            else
                return true;
        }

        bool checkRequiredFields()
        {
            if (tbxAddress.Text == "" || tbxEmail.Text == "" || tbxFirstName.Text == "" || tbxLastName.Text == "" || tbxPassword.Text == "" || tbxPhone.Text == "" || tbxUsername.Text == "")
            {
                lblWarning.Text = "Please fill out all fields.";
                lblWarning.Visible = true;
                return false;
            }
            else
                return true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Webpages/LoginPage.aspx");
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            if(checkRequiredFields())
                if (checkUniqueUsername())
                {
                    // Create SQL connection object
                    MySqlConnection conn = new MySqlConnection();
                    conn.ConnectionString = WebConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

                    // Create a SQL command Object
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = conn;
                    conn.Open();

                    //insert into userinfo
                    string cmdText = $"insert into customers value (0, '" + tbxFirstName.Text + "', '" + tbxLastName.Text + "');";
                    cmd.CommandText = cmdText;
                    cmd.ExecuteNonQuery();


                    cmdText = "select max(ID) as 'ID' from customers ";
                    cmd.CommandText = cmdText;
                    MySqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    int customerID = reader.GetInt32("ID");
                    reader.Close();
                    c = new HttpCookie("userInfo");
                    c["username"] = tbxUsername.Text;
                    c["password"] = tbxPassword.Text;
                    c["customerid"] = customerID.ToString();
                    c.Expires = DateTime.Now.AddMinutes(20);
                    Response.Cookies.Add(c);
                    //Add to customers table
                    cmdText = $"insert into userinfo value ({customerID},'" + tbxUsername.Text + "', '" + tbxPassword.Text + "', 0);";  
                    cmd.CommandText = cmdText;
                    cmd.ExecuteNonQuery();
                    
                    

                    /*
                    //Add to userinfo table
                    cmdText = "select max(ID) as 'ID' from customers where `First Name` = '" + tbxFirstName.Text + "' and `Last Name` = '" + tbxLastName.Text + "'";
                    cmd.CommandText = cmdText;
                    MySqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    int customerID = reader.GetInt32("ID");
                    reader.Close();
                    cmdText = "insert into userinfo value (" + customerID + ",'" + tbxUsername.Text + "', '" + tbxPassword.Text + "', 0);";
                    cmd.CommandText = cmdText;
                    cmd.ExecuteNonQuery();
                    */

                    //Add to customeremail
                    string[] emails = tbxEmail.Text.Split(',');

                    for(int i = 0; i < emails.Length; i++)
                    {
                        cmdText = "insert into customeremail value (" + customerID + ",'" + emails[i] + "');";
                        cmd.CommandText = cmdText;
                        cmd.ExecuteNonQuery();
                    }

                    //Add to customeraddress
                    string[] addresses = tbxAddress.Text.Split(',');

                    for (int i = 0; i < addresses.Length; i++)
                    {
                        cmdText = "insert into customeraddress value (" + customerID + ",'" + addresses[i] + "');";
                        cmd.CommandText = cmdText;
                        cmd.ExecuteNonQuery();
                    }

                    //Add to customerphones
                    string[] phones = tbxPhone.Text.Split(',');

                    for (int i = 0; i < phones.Length; i++)
                    {
                        cmdText = "insert into customerphones value (" + customerID + ",'" + phones[i] + "');";
                        cmd.CommandText = cmdText;
                        cmd.ExecuteNonQuery();
                    }

                    conn.Close();

                    /*
                    cmd.CommandText = "select * from userinfo where username like '" + tbxUsername.Text + "' and password like '" + tbxPassword.Text + "'";

                    conn.Open();
                    cmd.CommandText = cmdText;
                    reader = cmd.ExecuteReader();


                    if (reader.HasRows)
                    {
                        reader.Read();
                        c = new HttpCookie("userInfo");
                        c["username"] = tbxUsername.Text;
                        c["password"] = tbxPassword.Text;
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
                    */

                    Response.Redirect("~/Webpages/BrowseInventory.aspx");
                }
        }
    }
}