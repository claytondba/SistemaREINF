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
    public partial class PerceiroAcesso : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                infoLabel.Text = "Você precisa cadastrar uma nova senha para continuar.";

            Session["active_page"] = "";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (senhaTextBox.Text != senhaTextBox2.Text)
            {
                infoLabel.Text = "Senhas informadas não conferem!!";
                return;
            }

            UsuariosReinfModel usuario = (UsuariosReinfModel)Session["usuario"];
            UsuariosReinfBLL usuBll = new UsuariosReinfBLL();

            usuBll.AtualizaPrimeiroAcesso(usuario, senhaTextBox.Text);

            Response.Redirect("HomeParceiro.aspx");


        }
    }
}