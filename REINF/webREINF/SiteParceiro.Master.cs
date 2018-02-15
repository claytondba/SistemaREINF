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
                    Literal1.Text = "<a href= \"CadastroCliente.aspx\">Cadastrar</a>" +
                                    "<a href= \"ConsultaClientes.aspx\">Consultar</a> " +
                                    "<a href= \"UltimosEnvios.aspx\">Últimos Envios</a> " +
                                    "<a href= \"ConfiguraParceiro.aspx\">Configurações</a> ";
                }
                else if (Session["active_page"].ToString() == "home_parceiro")
                {
                    Literal1.Text = "<a href= \"CadastroCliente.aspx\">Clientes</a>" +
                                    "<a href= \"UltimosEnvios.aspx\">Últimos Envios</a> " +
                                    "<a href= \"RelatoriosParceiros\">Relatórios</a> " +
                                    "<a href= \"ConfiguraParceiro.aspx\">Configurações</a> " +
                                    "<a href= \"AjudaParceiro.aspx\">Ajuda</a> ";
                }
                else
                {
                    Literal1.Text = "";
                }
            }
        }
    }
}