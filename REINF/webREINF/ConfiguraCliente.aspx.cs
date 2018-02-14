using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webREINF
{
    public partial class ConfiguraCliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if(senhaTextBox.Text != senhaTextBox2.Text)
            {
                errorLabel.Text = "Senhas não conferem!";
                errorLabel.ForeColor = Color.Red;
                return;

            }

            UsuariosReinfModel usuario = (UsuariosReinfModel)Session["usuario"];
            UsuariosReinfBLL usuBll = new UsuariosReinfBLL();

            usuBll.AtualizaPrimeiroAcesso(usuario, senhaTextBox.Text);

            Response.Redirect("HomeCliente.aspx");
        }
    }
}