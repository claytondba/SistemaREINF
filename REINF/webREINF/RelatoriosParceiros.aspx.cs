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
    public partial class RelatoriosParceiros : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["usuario"] == null)
                    Response.Redirect("Login.aspx");

                UsuariosReinfModel usu = (UsuariosReinfModel)Session["usuario"];
                OcorrenciaBLL ocoBll = new OcorrenciaBLL();

                GridView1.DataSource =
                ocoBll.GetOcorrenciasByParceiro(usu.id);

                GridView1.DataBind();
            }
        }
    }
}