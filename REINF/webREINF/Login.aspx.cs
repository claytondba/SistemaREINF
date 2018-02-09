using System;
using Model;
using BLL;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace webREINF
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if(Session["usuario"] != null)
                {
                    UsuariosReinfModel usuario = (UsuariosReinfModel)Session["usuario"];
                    if (usuario.parceiro)
                    {
                        if (usuario.primeiro_acesso)
                            Response.Redirect("PerceiroAcesso.aspx", true);
                        else
                            Response.Redirect("HomeParceiro.aspx", true);
                    }
                    else
                    {
                        if (usuario.primeiro_acesso)
                            Response.Redirect("ClienteAcesso.aspx", true);
                        else
                            Response.Redirect("HomeCliente.aspx", true);
                    }
                }
                new SessionBLL().Connect();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            UsuariosReinfBLL usu = new UsuariosReinfBLL();


            if (usu.Logar(loginTextBox.Text, senhaTextBox.Text))
            {
                //FormsAuthentication.RedirectFromLoginPage(loginTextBox.Text, true);
                UsuariosReinfModel usuario = usu.LogarGetUser(loginTextBox.Text, senhaTextBox.Text);
                Session["usuario"] = usuario;

                if (usuario.parceiro)
                {
                    if (usuario.primeiro_acesso)
                        Response.Redirect("PerceiroAcesso.aspx", true);
                    else
                    {
                        Session["active_page"] = "home_parceiro";
                        Response.Redirect("HomeParceiro.aspx", true);                       
                    }
                }
                else
                {
                    if (usuario.primeiro_acesso)
                        Response.Redirect("ClienteAcesso.aspx", true);
                    else
                    {
                        Session["active_page"] = "home_cliente";
                        Response.Redirect("HomeCliente.aspx", true);                      
                    }
                }

            }
            else
            {
                Label1.Text = "Usuário e/ou Senha Incorretos.";
            }


        }
    }
}