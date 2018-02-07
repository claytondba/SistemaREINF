using System;
using Model;
using BLL;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webREINF
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                new SessionBLL().Connect();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            UsuariosReinfBLL usu = new UsuariosReinfBLL();

            try
            {
                if (usu.Logar(loginTextBox.Text, senhaTextBox.Text))
                {

                }
                else
                {
                    Label1.Text = "Usuário e/ou Senha Incorretos.";
                }

            }
            catch (Exception ex)
            {
                Label1.Text = ex.Message;   
            }
            
        }
    }
}