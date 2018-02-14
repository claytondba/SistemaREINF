using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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

                if((bool)Session["edit_reg"])
                {
                    UsuariosReinfModel usu = (UsuariosReinfModel)Session["edit_usuario"];

                    nomeTextBox.Text = usu.nome;
                    razaoTextBox.Text = usu.razao_social;
                    cnpjTextBox.Text = usu.cnpj;
                    emailTextBox.Text = usu.email;
                    telefoneTextBox.Text = usu.telefone;
                    loginTextBox.Text = usu.login;
                    CheckBox1.Checked = usu.ativo;

                    Button1.Text = "Atualizar";
                }
            }
            
        }
        public static string GerarHashMd5(string input)
        {
            MD5 md5Hash = MD5.Create();

            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            //if(string.IsNullOrEmpty(nomeTextBox.Text))
            UsuariosReinfModel usuLogado = (UsuariosReinfModel)Session["usuario"];
            if ((bool)Session["edit_reg"])
            {
                UsuariosReinfModel usu = (UsuariosReinfModel)Session["edit_usuario"];
                
                usu.nome = nomeTextBox.Text;
                usu.razao_social = razaoTextBox.Text;
                //usu.primeiro_acesso = true;
                usu.ativo = CheckBox1.Checked;
                usu.cnpj = cnpjTextBox.Text;
                usu.email = emailTextBox.Text;
                usu.telefone = telefoneTextBox.Text;

                //usu.parceiro = false;
                usu.login = loginTextBox.Text;
                usu.senha = GerarHashMd5(senhaTextBox.Text);
                //usu.data_cadastro = DateTime.Now;
                new UsuariosReinfBLL().FrameworkUpdate(usu);
                Session["edit_reg"] = false;
                Session["edit_usuario"] = null;
                Response.Redirect("ConsultaClientes.aspx");
            }
            else
            {
                UsuariosReinfModel usu = new UsuariosReinfModel();
                usu.cadastrante = usuLogado.id;
                usu.nome = nomeTextBox.Text;
                usu.razao_social = razaoTextBox.Text;
                usu.primeiro_acesso = true;
                usu.ativo = CheckBox1.Checked;
                usu.cnpj = cnpjTextBox.Text;
                usu.email = emailTextBox.Text;
                usu.telefone = telefoneTextBox.Text;

                usu.parceiro = false;
                usu.login = loginTextBox.Text;
                usu.senha = GerarHashMd5(senhaTextBox.Text);
                usu.data_cadastro = DateTime.Now;
                new UsuariosReinfBLL().FInsert(usu);

                //new UsuariosReinfBLL().AtualizaSenha(usu, "Candinho2018");

                Response.Redirect("CadastroCliente.aspx");
            }
        }
    }
}