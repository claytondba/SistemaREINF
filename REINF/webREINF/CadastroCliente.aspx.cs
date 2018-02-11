using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Model;

namespace webREINF
{
    public partial class CadastroCliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["active_page"] = "cadastro_cliente";
            }
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //if(string.IsNullOrEmpty(nomeTextBox.Text))


            UsuariosReinfModel usu = new UsuariosReinfModel();
            usu.nome = nomeTextBox.Text;
            usu.razao_social = razaoTextBox.Text;
            usu.primeiro_acesso = true;
            usu.ativo = true;
            usu.cnpj = cnpjTextBox.Text;
            usu.email = emailTextBox.Text;
            usu.telefone = telefoneTextBox.Text;

            usu.parceiro = false;
            usu.login = loginTextBox.Text;
            usu.senha = "12345";
            usu.data_cadastro = DateTime.Now;
            new UsuariosReinfBLL().FInsert(usu);

            Response.Redirect("CadastroCliente.aspx");
        }
    }
}