using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookstoreProject
{
    public partial class ExampleForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }


        protected void btnExampleOnClick(object sender, EventArgs e)
        {
            if (lblExample.Visible == false)
            {
                lblExample.Visible = true;
            }
            else
            {
                lblExample.Visible = false;
            }
        }
    }
}