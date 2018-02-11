using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using BLL;

namespace webREINF
{
    public partial class ConsultaClientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["usuario"] == null)
                    Response.Redirect("Login.aspx");

                UsuariosReinfModel usu = (UsuariosReinfModel)Session["usuario"];
                UsuariosReinfBLL usuBll = new UsuariosReinfBLL();

                GridView1.DataSource =
                usuBll.FGetCustom(string.Format(string.Format("cadastrante ={0}", usu.id)));

                GridView1.DataBind();
            }
            
        }
    }
}