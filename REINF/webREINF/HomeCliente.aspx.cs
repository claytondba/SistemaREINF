using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webREINF
{
    public partial class HomeCliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["active_page"] = "home_parceiro";
            if (!Page.IsPostBack)
            {
                ConfigModel cfg = new ConfigBLL().FGet().First();
                titleLabel.Text = cfg.home_cliente_title;
                Literal1.Text = cfg.home_cliente_info;

            }
        }
    }
}