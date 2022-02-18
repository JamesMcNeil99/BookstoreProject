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
    public partial class AccountHome : System.Web.UI.Page
    {
        HttpCookie c;
        int customerID;
        protected void Page_Load(object sender, EventArgs e)
        {
            c = Request.Cookies["userInfo"];
            if (c == null)
            {
                Response.Redirect("~/Webpages/LoginPage.aspx");
            }
            c.Expires = DateTime.Now.AddMinutes(20);
            Response.Cookies.Add(c);
            if (c["customerid"] == "null")
                Response.Redirect("~/Webpages/BrowseInventory.aspx");
            customerID = int.Parse(c["customerid"]);

            if (!IsPostBack)
            {
                pageLoad();
                setUpTable();
            }
        }

        void setUpTable()
        {
            // Create SQL connection object
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            // Create a SQL command Object
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            conn.Open();
            
            cmd.CommandText = $"select ID, `Order Date`, `Order Value` from orders where `placed by` = {customerID}";

            MySqlDataReader reader = cmd.ExecuteReader();

            gvwYourOrders.DataSource = reader;
            gvwYourOrders.DataBind(); 
            reader.Close();
            conn.Close();

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

            string cmdText = "Select username from userinfo where username like '" + tbx6.Text + "';";

            cmd.CommandText = cmdText;
            MySqlDataReader reader = cmd.ExecuteReader();

            //if any usernames match entered username, returns false
            if (reader.HasRows)
            {
                return false;
            }
            else
                return true;
        }

        void pageLoad()
        {
            lbl1.Text = "First Name";
            lbl2.Text = "Last Name";
            lbl3.Text = "Email";
            lbl4.Text = "Address";
            lbl5.Text = "Phone Number";
            lbl6.Text = "Username";
            lbl7.Text = "Password";
            // Create SQL connection object
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            // Create a SQL command Object
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            conn.Open();

            cmd.CommandText = "select * from customers where ID = " + customerID;
            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            tbx1.Text = reader.GetString("First Name");
            tbx2.Text = reader.GetString("Last Name");
            reader.Close();

            string details;
            cmd.CommandText = "select * from customeraddress where custContactID = " + customerID;
            reader = cmd.ExecuteReader();
            reader.Read();
            details = reader.GetString("Address").Trim();
            while (reader.Read())
            {
                details += ", " + reader.GetString("Address").Trim();
            }
            tbx4.Text = details;
            reader.Close();

            cmd.CommandText = "select * from customeremail where custContactID = " + customerID;
            reader = cmd.ExecuteReader();
            reader.Read();
            details = reader.GetString("email").Trim();
            while (reader.Read())
            {
                details += ", " + reader.GetString("email").Trim();
            }
            reader.Close();
            tbx3.Text = details;

            cmd.CommandText = "select * from customerphones where custContactID = " + customerID;
            reader = cmd.ExecuteReader();
            reader.Read();
            details = reader.GetString("Phone Number").Trim();
            while (reader.Read())
            {
                details += ", " + reader.GetString("Phone Number").Trim();
            }
            reader.Close();
            tbx5.Text = details;

            cmd.CommandText = "select * from userinfo where customerid = " + customerID;
            reader = cmd.ExecuteReader();
            reader.Read();
            tbx6.Text = reader.GetString("username").Trim();
            tbx7.Text = reader.GetString("password").Trim();
            conn.Close();

            btnUpdate.Visible = true;
            btnCancel.Visible = false;
            btnSubmit.Visible = false;

            tbx1.Attributes.Add("readonly", "readonly");
            tbx2.Attributes.Add("readonly", "readonly");
            tbx3.Attributes.Add("readonly", "readonly");
            tbx4.Attributes.Add("readonly", "readonly");
            tbx5.Attributes.Add("readonly", "readonly");
            tbx6.Attributes.Add("readonly", "readonly");
            tbx7.Attributes.Add("readonly", "readonly");
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            // Create a SQL command Object
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            if (tbx1.Text != "" && tbx2.Text != "" && tbx3.Text != "" && tbx4.Text != "" && tbx5.Text != "" && tbx6.Text != "" && tbx7.Text != "")
            {

                conn.Open();
                cmd.CommandText = $"update customers set `First Name` = '{tbx1.Text}', `Last Name` = '{tbx2.Text}' where ID = " + customerID;
                cmd.ExecuteNonQuery();

                if (c["username"] != tbx6.Text && checkUniqueUsername())
                {
                    cmd.CommandText = $"update userinfo set username = '{tbx6.Text}', password = '{tbx7.Text}' where customerid = {customerID}";
                    cmd.ExecuteNonQuery();
                }


                string[] values = tbx3.Text.Split(',');
                cmd.CommandText = $"delete from customeremail where custContactID = {customerID}";
                cmd.ExecuteNonQuery();
                for (int i = 0; i < values.Length; i++)
                {
                    cmd.CommandText = $"insert into customeremail value ({customerID},'{values[i]}')";
                    cmd.ExecuteNonQuery();
                }
                values = tbx4.Text.Split(',');
                cmd.CommandText = $"delete from customeraddress where custContactID = {customerID}";
                cmd.ExecuteNonQuery();
                for (int i = 0; i < values.Length; i++)
                {
                    cmd.CommandText = $"insert into customeraddress value ({customerID},'{values[i]}')";
                    cmd.ExecuteNonQuery();
                }

                values = tbx5.Text.Split(',');
                cmd.CommandText = $"delete from customerphones where custContactID = {customerID}";
                cmd.ExecuteNonQuery();
                for (int i = 0; i < values.Length; i++)
                {
                    cmd.CommandText = $"insert into customerphones value ({customerID},'{values[i]}')";
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
                Response.Redirect("~/Webpages/BrowseInventory.aspx");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            btnUpdate.Visible = true;
            btnCancel.Visible = false;
            btnSubmit.Visible = false;

            tbx1.Attributes.Add("readonly", "readonly");
            tbx2.Attributes.Add("readonly", "readonly");
            tbx3.Attributes.Add("readonly", "readonly");
            tbx4.Attributes.Add("readonly", "readonly");
            tbx5.Attributes.Add("readonly", "readonly");
            tbx6.Attributes.Add("readonly", "readonly");
            tbx7.Attributes.Add("readonly", "readonly");
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            btnUpdate.Visible = false;
            btnCancel.Visible = true;
            btnSubmit.Visible = true;

            tbx1.Attributes.Remove("readonly");
            tbx2.Attributes.Remove("readonly");
            tbx3.Attributes.Remove("readonly");
            tbx4.Attributes.Remove("readonly");
            tbx5.Attributes.Remove("readonly");
            tbx6.Attributes.Remove("readonly");
            tbx7.Attributes.Remove("readonly");
        }
    }
}