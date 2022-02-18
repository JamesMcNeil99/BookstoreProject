using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace BookstoreProject.Webpages
{
    public partial class BrowseInventory : System.Web.UI.Page
    {
        HttpCookie c;
        protected void Page_Load(object sender, EventArgs e)
        {
            refreshTable();
            c = Request.Cookies["userInfo"];

            if(c == null)
            {
                Response.Redirect("~/Webpages/LoginPage.aspx");
            }
            else
            {
                c.Expires = DateTime.Now.AddMinutes(20);
                Response.Cookies.Add(c);
            }

        }

        protected void btnRefresh(object sender, EventArgs e)
        {
            refreshTable();
            
        }

        protected void refreshTable()
        {
            
            // Create SQL connection object
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            // Create a SQL command Object
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;

            // Create SQL insert command
            String cmdText = "SELECT books.ISBN, books.Title, authors.`First Name`, authors.`Last Name`, books.Price, books.Reviews, books.`Publication Date`, `Category Description`, suppliers.`Supplier Name` as Supplier FROM books INNER JOIN writtenby ON books.ISBN = writtenby.ISBN INNER JOIN authors ON writtenby.Author = authors.ID INNER JOIN suppliers ON books.Supplier = suppliers.ID inner join bookcategories on books.category = bookcategories.`category code`";

            // add title filter Should look like " WHERE books.Title Like "Search Name"
            cmdText += " WHERE lower(books.Title) LIKE '%" + tbxBookName.Text.ToLower() + "%'";

            // add author filter. Should look like " AND First Last LIKE Search Name"
            cmdText += " AND lower(concat(authors.`First Name`, ' ', authors.`Last Name`)) LIKE '%" + tbxAuthorName.Text.ToLower() + "%'";

            // add supplier filter. Should look like " WHERE suppliers.Name Like "Search Name"
            cmdText += " AND lower(suppliers.`Supplier Name`) LIKE '%" + tbxSupplierName.Text.ToLower() + "%'";





            cmd.CommandText = cmdText;

            // Execute command
            conn.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            gvwBooks.DataSource = reader;
            gvwBooks.DataBind();
            conn.Close();

        }

        protected void btnReset(object sender, EventArgs e)
        {
            tbxAuthorName.Text = "";
            tbxBookName.Text = "";
            tbxSupplierName.Text = "";

            refreshTable();
        }

        protected void AddToCart(object sender, GridViewSelectEventArgs e)
        {
            TableRow row = gvwBooks.Rows[e.NewSelectedIndex];
            string isbn = row.Cells[1].Text;
            string price = row.Cells[5].Text;

            if (c != null)
            {

                //--------------------This SQL query block prevents admins from adding to cart-------------------------
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
                reader.Close();
                conn.Close();

                //--------------------------------Add to cart function-----------------------------
                if (accesslevel == 0)
                {
                    conn.Open();
                    //Checks for open orders by the current customer
                    cmdText = "select ID from orders where `placed by` = " + c["customerid"] + " and `Order Date` is null";
                    cmd.CommandText = cmdText;
                    reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        int openOrderID = reader.GetInt32("ID");
                        reader.Close();
                        cmdText = $"insert into orderitems value (0, {price}, {isbn}, {openOrderID});";
                        cmd.CommandText = cmdText;
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        reader.Close();
                        cmdText = $"insert into orders value (0, null, null, {c["customerid"]})";
                        cmd.CommandText = cmdText;
                        cmd.ExecuteNonQuery();

                        cmdText = "select ID from orders where `placed by` = " + c["customerid"] + " and `Order Date` is null";
                        cmd.CommandText = cmdText;
                        reader = cmd.ExecuteReader();
                        int openOrderID= 0;
                        reader.Read();
                        openOrderID = reader.GetInt32("ID");
                        reader.Close();
                        cmdText = $"insert into orderitems value (0, {price}, {isbn}, {openOrderID});";
                        cmd.CommandText = cmdText;
                        cmd.ExecuteNonQuery();

                    }
                    conn.Close();

                }
            }
            else
                Response.Redirect("~/Webpages/LoginPage.aspx");
        }
    }
}