using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webREINF
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["usuario"] = null;
            Session["active_page"] = null;
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
    }
}