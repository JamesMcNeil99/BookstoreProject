using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookstoreProject
{
    public partial class logoutRedirect : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie c = Request.Cookies["userInfo"];
            if (c != null)
            {
                c.Expires = DateTime.Now.AddDays(-1);
                Response.SetCookie(c);
            }
            Response.Redirect("~/Webpages/LoginPage.aspx");
        }
    }
}