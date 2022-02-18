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
    public partial class RecordsUpdate : System.Web.UI.Page
    {
        HttpCookie c;
        string table, mode, id;
        protected void Page_Load(object sender, EventArgs e)
        {
            c = Request.Cookies["userInfo"];
            if (c == null)
                Response.Redirect("~/Webpages/LoginPage.aspx");
            else
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

                if (accesslevel == 0)
                {
                    Response.Redirect("~/Webpages/BrowseInventory.aspx");
                }
            }
            table = Request.QueryString.Get("table");
            mode = Request.QueryString.Get("mode");
            if(mode == "edit")
            {
                id = Request.QueryString.Get("id");
            }
            if (mode == null || table == null || (mode == "edit" && id == null))
                Response.Redirect("~/Webpages/BrowseInventory.aspx");
            
            if(!IsPostBack)
                setUpPage();
        }
        void setUpPage()
        {
            
            switch (table)
            {
                case "Books":
                    updateBooks();
                    break;
                case "Authors":
                    updateAuthors();
                    break;
                case "Customers":
                    updateCustomers();
                    break;
            }
        }

        void updateBooks()
        {
            lbl1.Text = "ISBN";
            lbl2.Text = "Title";
            lbl3.Text = "Publication Date (YYYY-MM-DD)  ";
            lbl4.Text = "Price";
            lbl5.Text = "Reviews";
            lbl9.Text = "Supplier";
            lbl10.Text = "Author";
            lbl11.Text = "Category";
            TableRow6.Visible = false;
            TableRow7.Visible = false;
            TableRow8.Visible = false;

            // Create SQL connection object
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            // Create a SQL command Object
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            conn.Open();
            cmd.CommandText = "select `supplier name`, ID from suppliers";
            MySqlDataReader reader = cmd.ExecuteReader();

            ddl1.DataSource = reader;
            ddl1.DataTextField = "supplier name";
            ddl1.DataValueField = "ID";
            ddl1.DataBind();
            reader.Close();
            
            cmd.CommandText = "select concat('(', authors.ID, ') ', authors.`First Name`, ' ', authors.`Last Name`) as Author, ID from authors";
            reader = cmd.ExecuteReader();
            ddl2.DataSource = reader;
            ddl2.DataTextField = "Author";
            ddl2.DataValueField = "ID";
            ddl2.DataBind();
            reader.Close();

            cmd.CommandText = "select `Category Code`, `Category Description` from bookcategories";
            reader = cmd.ExecuteReader();
            ddl3.DataSource = reader;
            ddl3.DataTextField = "Category Description";
            ddl3.DataValueField = "Category Code";
            ddl3.DataBind();
            reader.Close();

            if (mode == "edit")
            {
                tbx1.Text = id;
                cmd.CommandText = "select * from books where books.ISBN = " + id;
                reader = cmd.ExecuteReader();
                reader.Read();

                tbx2.Text = reader.GetString("Title");
                DateTime time = reader.GetDateTime("Publication Date");
                tbx3.Text = $"{time.Year}-{time.Month}-{time.Day}";
                tbx4.Text = reader.GetString("Price");
                tbx5.Text = reader.GetString("Reviews");
            }


        }
        void updateAuthors()
        {

            lbl1.Text = "First Name";
            lbl2.Text = "Last Name";
            lbl3.Text = "Gender";
            lbl4.Text = "DOB";
            lbl5.Text = "Email";
            lbl6.Text = "Phone #";
            lbl7.Text = "Address";
            TableRow8.Visible = false;
            TableRow9.Visible = false;
            TableRow10.Visible = false;
            TableRow11.Visible = false;

            if (mode == "edit")
            {
                // Create SQL connection object
                MySqlConnection conn = new MySqlConnection();
                conn.ConnectionString = WebConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

                // Create a SQL command Object
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                conn.Open();

                cmd.CommandText = "select * from authors where authors.id = " + id;
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                tbx1.Text = reader.GetString("First Name");
                tbx2.Text = reader.GetString("Last Name");
                tbx3.Text = reader.GetString("Gender");
                DateTime time = reader.GetDateTime("DOB");
                tbx4.Text = $"{time.Year}-{time.Month}-{time.Day}";
                reader.Close();

                string details = "";

                cmd.CommandText = "select * from authoremails where authcontactID = " + id;
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    details = reader.GetString("Email");
                    while (reader.Read())
                    {
                        details += ", " + reader.GetString("Email");
                    }
                    tbx5.Text = details;
                }
                reader.Close();

                details = "";
                cmd.CommandText = "select * from authorphones where authcontactID = " + id;
                reader = cmd.ExecuteReader();
                if (reader.HasRows) { 
                    reader.Read();
                    details = reader.GetString("Phone Number");
                    while (reader.Read())
                    {
                        details += ", " + reader.GetString("Phone Number");
                    }
                    tbx6.Text = details;
                }
                reader.Close();
                

                details = "";
                cmd.CommandText = "select * from authoraddress where authcontactID = " + id;
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {

                    reader.Read();
                    details = reader.GetString("Address");
                    while (reader.Read())
                    {
                        details += ", " + reader.GetString("Address");
                    }

                    tbx7.Text = details;
                }
                reader.Close();
                conn.Close();
            }

        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            switch (table)
            {
                case "Books":
                    submitBooks();
                    break;
                case "Authors":
                    submitAuthors();
                    break;
                case "Customers":
                    submitCustomers();
                    break;
            }
        }
        void submitBooks()
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            // Create a SQL command Object
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            if (tbx1.Text != "" && tbx2.Text != "" && tbx3.Text != "" && tbx4.Text != "" && tbx5.Text != "" && ddl1.SelectedIndex > -1 && ddl2.SelectedIndex > -1)
            {
                if (mode == "edit")
                {
                    conn.Open();
                    cmd.CommandText = $"update books set isbn = {tbx1.Text}, title = '{tbx2.Text}', `Publication Date` = '{tbx3.Text}', `Price` = {tbx4.Text}, Reviews = {tbx5.Text}, Supplier = {ddl1.SelectedValue} where isbn = " + id;
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = $"update writtenby set isbn = {tbx1.Text} where isbn = " + id;
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                else
                {
                    conn.Open();
                    cmd.CommandText = $"insert into books value ({tbx1.Text}, '{tbx2.Text}','{tbx3.Text}',{tbx4.Text},{tbx5.Text},{ddl1.SelectedValue},{ddl3.SelectedValue}";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = $"insert into writtenby value({tbx1.Text},{ddl2.SelectedValue})";
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                Response.Redirect("~/Webpages/RecordsView.aspx");
            }
        }
        void submitAuthors()
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            // Create a SQL command Object
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            if (tbx1.Text != "" && tbx2.Text != "" && tbx3.Text != "" && tbx4.Text != "" && tbx5.Text != "" && tbx6.Text != "" && tbx7.Text != "")
            {
                if (mode == "edit")
                {
                    conn.Open();
                    cmd.CommandText = $"update authors set `First Name` = '{tbx1.Text}', `Last Name` = '{tbx2.Text}', Gender = '{tbx3.Text}', DOB = '{tbx4.Text}' where id = " + id;
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                else
                {
                    conn.Open();

                    cmd.CommandText = $"insert into authors value (0, '{tbx1.Text}','{tbx2.Text}', '{tbx3.Text}', '{tbx4.Text}')";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "select max(ID) as 'ID' from authors";
                    MySqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    int authID = reader.GetInt32("ID");
                    reader.Close();
                    id = authID.ToString();
                    conn.Close();
                }
                conn.Open();
                string[] values = tbx5.Text.Split(',');
                cmd.CommandText = $"delete from authoremails where authContactID = {id}";
                cmd.ExecuteNonQuery();
                for (int i = 0; i < values.Length; i++)
                {
                    cmd.CommandText = $"insert into authoremails value ({id},'{values[i]}')";
                    cmd.ExecuteNonQuery();
                }
                values = tbx6.Text.Split(',');
                cmd.CommandText = $"delete from authorphones where authContactID = {id}";
                cmd.ExecuteNonQuery();
                for (int i = 0; i < values.Length; i++)
                {
                    cmd.CommandText = $"insert into authorphones value ({id},'{values[i]}')";
                    cmd.ExecuteNonQuery();
                }

                values = tbx7.Text.Split(',');
                cmd.CommandText = $"delete from authoraddress where authContactID = {id}";
                cmd.ExecuteNonQuery();
                for (int i = 0; i < values.Length; i++)
                {
                    cmd.CommandText = $"insert into authoraddress value ({id},'{values[i]}')";
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
                Response.Redirect("~/Webpages/RecordsView.aspx");
            }
        }
        void submitCustomers()
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            // Create a SQL command Object
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            if (tbx1.Text != "" && tbx2.Text != "" && tbx3.Text != "" && tbx4.Text != "" && tbx5.Text != "" && tbx6.Text != "" && tbx7.Text != "")
            {
                if (mode == "edit")
                {
                    conn.Open();
                    cmd.CommandText = $"update customers set `First Name` = '{tbx1.Text}', `Last Name` = '{tbx2.Text}' where id = " + id;
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = $"update userinfo set username = '{tbx6.Text}', password = '{tbx7.Text}' where customerid = {id}";
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                else if(checkUniqueUsername())
                {
                    conn.Open();

                    string cmdText = $"insert into customers value (0,'{tbx1.Text}', '{tbx2.Text}')";
                    cmd.CommandText = cmdText;
                    cmd.ExecuteNonQuery();


                    cmdText = "select max(ID) as 'ID' from customers";
                    cmd.CommandText = cmdText;
                    MySqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    int customerID = reader.GetInt32("ID");
                    reader.Close();
                    id = customerID.ToString();
                    cmd.CommandText = $"insert into userinfo value ({customerID},'{tbx6.Text}', '{tbx7.Text}', 0)";
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                conn.Open();
                string[] values = tbx3.Text.Split(',');
                cmd.CommandText = $"delete from customeremail where custContactID = {id}";
                cmd.ExecuteNonQuery();
                for(int i = 0; i < values.Length; i++)
                {
                    cmd.CommandText = $"insert into customeremail value ({id},'{values[i]}')";
                    cmd.ExecuteNonQuery();
                }
                values = tbx4.Text.Split(',');
                cmd.CommandText = $"delete from customeraddress where custContactID = {id}";
                cmd.ExecuteNonQuery();
                for (int i = 0; i < values.Length; i++)
                {
                    cmd.CommandText = $"insert into customeraddress value ({id},'{values[i]}')";
                    cmd.ExecuteNonQuery();
                }

                values = tbx5.Text.Split(',');
                cmd.CommandText = $"delete from customerphones where custContactID = {id}";
                cmd.ExecuteNonQuery();
                for (int i = 0; i < values.Length; i++)
                {
                    cmd.CommandText = $"insert into customerphones value ({id},'{values[i]}')";
                    cmd.ExecuteNonQuery();
                }




                conn.Close();
                Response.Redirect("~/Webpages/RecordsView.aspx");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Webpages/RecordsView.aspx");
        }

        void updateCustomers()
        {
            lbl1.Text = "First Name";
            lbl2.Text = "Last Name";
            lbl3.Text = "Email";
            lbl4.Text = "Address";
            lbl5.Text = "Phone Number";
            lbl6.Text = "Username";
            lbl7.Text = "Password";
            TableRow8.Visible = false;
            TableRow9.Visible = false;
            TableRow10.Visible = false;
            TableRow11.Visible = false;
            if (mode == "edit")
            {
                // Create SQL connection object
                MySqlConnection conn = new MySqlConnection();
                conn.ConnectionString = WebConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

                // Create a SQL command Object
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                conn.Open();

                cmd.CommandText = "select * from customers where ID = " + id;
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                tbx1.Text = reader.GetString("First Name");
                tbx2.Text = reader.GetString("Last Name");
                reader.Close();

                string details;
                cmd.CommandText = "select * from customeraddress where custContactID = " + id;
                reader = cmd.ExecuteReader();
                reader.Read();
                details = reader.GetString("Address");
                while (reader.Read())
                {
                    details += ", " + reader.GetString("Address");
                }
                tbx4.Text = details;
                reader.Close();

                cmd.CommandText = "select * from customeremail where custContactID = " + id;
                reader = cmd.ExecuteReader();
                reader.Read();
                details = reader.GetString("email");
                while (reader.Read())
                {
                    details += ", " + reader.GetString("email");
                }
                reader.Close();
                tbx3.Text = details;

                cmd.CommandText = "select * from customerphones where custContactID = " + id;
                reader = cmd.ExecuteReader();
                reader.Read();
                details = reader.GetString("Phone Number");
                while (reader.Read())
                {
                    details += ", " + reader.GetString("Phone Number");
                }
                reader.Close();
                tbx5.Text = details;

                cmd.CommandText = "select * from userinfo where customerid = " + id;
                reader = cmd.ExecuteReader();
                reader.Read();
                tbx6.Text = reader.GetString("username");
                tbx7.Text = reader.GetString("password");

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
    }
}