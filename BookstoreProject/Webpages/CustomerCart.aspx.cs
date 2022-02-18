using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using MySql.Data.MySqlClient;

namespace BookstoreProject.Webpages
{
    public partial class CustomerCart : System.Web.UI.Page
    {
        HttpCookie c;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Checks if the user is logged in, sends to login if not
            c = Request.Cookies["userInfo"];
            if(c == null)
            {
                Response.Redirect("~/Webpages/LoginPage.aspx");
            }
            c.Expires = DateTime.Now.AddMinutes(20);
            Response.Cookies.Add(c);

            refreshTable();


        }

        void refreshTable()
        {
            int custID = 0;
            if (c != null)
            {
                string s = c["customerid"];
                if (s == "null")
                    Response.Redirect("~/Webpages/BrowseInventory.aspx");
                custID = int.Parse(s);
            }
            else
                Response.Redirect("~/Webpages/LoginPage.aspx");
            // Create SQL connection object
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            // Create a SQL command Object
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;

            // Create SQL insert command
            string cmdText = $"SELECT orderitems.`Item Number`, books.ISBN, books.Title, orderitems.`Item Price` FROM books, orderitems, orders WHERE orders.`Placed By` = {custID} AND orderitems.`Order Number` = orders.ID AND orderitems.`Item ISBN` = books.ISBN and orders.`Order Date` is null";



            cmd.CommandText = cmdText;

            // Execute command
            conn.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            gvwCartItems.DataSource = reader;
            gvwCartItems.DataBind();

            conn.Close();
            updateTotal();
        }
        void updateTotal()
        {
            // Create SQL connection object
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            // Create a SQL command Object
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            conn.Open();
            string cmdText = "select ID from orders where `placed by` = " + c["customerid"] + " and `Order Date` is null";
            cmd.CommandText = cmdText;
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read()) {
                int orderID = reader.GetInt32("ID");
                reader.Close();
                cmdText = "select `item price` from orderitems where `order number` = " + orderID;
                double total = 0.0;
                cmd.CommandText = cmdText;
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        total += reader.GetDouble("Item Price");
                    }
                }
                lblTotal.Text = $"Total: ${total}";
            }
            else
                lblTotal.Text = "Total: $0.00";
            conn.Close();
        }
        protected void RemoveItem(object sender, GridViewDeleteEventArgs e)
        {
            TableRow row = gvwCartItems.Rows[e.RowIndex];

            string itemID = row.Cells[1].Text;

            // Create SQL connection object
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            // Create a SQL command Object
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;



            string cmdText = $"delete from orderitems where `item number` = {itemID}";

            cmd.CommandText = cmdText;
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            refreshTable();

        }

        protected void btnOrderAll_Click(object sender, EventArgs e)
        {
            // Create SQL connection object
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            // Create a SQL command Object
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            conn.Open();
            string cmdText = "select ID from orders where `placed by` = " + c["customerid"] + " and `Order Date` is null";
            cmd.CommandText = cmdText;
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                int orderID = reader.GetInt32("ID");
                reader.Close();
                cmdText = "select `item number` from orderitems where `order number` = " + orderID;
                cmd.CommandText = cmdText;
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Close();
                    cmdText = $"update orders set `order value` = {lblTotal.Text.Split('$')[1]}, `order date` = '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}  {DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}' where id = {orderID}";
                    cmd.CommandText = cmdText;
                    cmd.ExecuteNonQuery();
                    refreshTable();
                }
            }
        }
    }
}