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
                if (Session["usuario"] == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }

                UsuariosReinfModel usuario = (UsuariosReinfModel)Session["usuario"];
                //Label1.Text = string.Format(
                //   "Olá {0}, bem vindo! Escolha a operação desejada no menu ao lado.", usuario.nome);

                MontaMenuLateral();
            }
            else
            {
                MontaMenuLateral();
            }
            
        }

        private void MontaMenuLateral()
        {
            if(Session["active_page"] == null)
            {
                Literal1.Text = "";
                return;
            }

            if (!string.IsNullOrEmpty(Session["active_page"].ToString()))
            {
                if (Session["active_page"].ToString() == "cadastro_cliente")
                {
                    Literal1.Text = "<a href= \"#about\">Cadastrar</a>" +
                                    "<a href= \"#services\">Consultar</a> " +
                                    "<a href= \"#clients\">Últimos Envios</a> " +
                                    "<a href= \"#contact\">Configurações</a> ";
                }
                else if (Session["active_page"].ToString() == "home_parceiro")
                {
                    Literal1.Text = "<a href= \"#about\">Clientes</a>" +
                                    "<a href= \"#services\">Relatórios</a> " +
                                    "<a href= \"#clients\">Configurações</a> " +
                                    "<a href= \"#contact\">Ajuda</a> ";
                }
                else
                {
                    Literal1.Text = "";
                }
            }
        }
    }
}