using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webREINF
{
    public partial class SiteParceiro : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                UsuariosReinfModel usuario = (UsuariosReinfModel)Session["usuario"];
                //Label1.Text = string.Format(
                 //   "Olá {0}, bem vindo! Escolha a operação desejada no menu ao lado.", usuario.nome);
            }
        }
    }
}