﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webREINF
{
    public partial class HomeParceiro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["active_page"] = "home_parceiro";

        }
    }
}