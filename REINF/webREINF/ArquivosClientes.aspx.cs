using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webREINF
{
    public partial class ArquivosClientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                if (Session["usuario"] == null)
                    Response.Redirect("Login.aspx");

                UsuariosReinfModel usu = (UsuariosReinfModel)Session["usuario"];
                ArquivosBLL usuBll = new ArquivosBLL();

                GridView1.DataSource =
                usuBll.FGetCustom(string.Format(string.Format("id_cliente ={0}", usu.id)));

                GridView1.DataBind();

            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            if (FileUpload1.PostedFile == null || string.IsNullOrEmpty(FileUpload1.PostedFile.FileName) || FileUpload1.PostedFile.InputStream == null)
            {
                errorLabel.Text = "<br />Erro - Não foi possível enviar o arquivo.<br />";
                return;
            }
            else
            {

                string extensao = Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower();
                string tipoArquivo = "";

                switch (extensao)
                {
                    case ".xls":
                        tipoArquivo = "application/vnd.ms-excel";
                        break;
                    case ".xlsx":
                        tipoArquivo = "application/vnd.ms-excel";
                        break;
                    default:
                        errorLabel.Text = "<br />Erro - tipo de arquivo inválido.<br />";
                        return;
                }

                byte[] arquivoBytes = new byte[FileUpload1.PostedFile.InputStream.Length + 1];
                FileUpload1.PostedFile.InputStream.Read(arquivoBytes, 0, arquivoBytes.Length);

                ArquivosModel arquivo = new ArquivosModel();
                UsuariosReinfModel usu = (UsuariosReinfModel)Session["usuario"];

                arquivo.arquivo_xls = arquivoBytes;
                arquivo.id_parceiro = usu.cadastrante;
                arquivo.id_cliente = usu.id;
                arquivo.data_evento = DateTime.Now;
                arquivo.status = "P";
                arquivo.tipo_evento = 1;
                arquivo.resposta_rfb = "";
                arquivo.envio_xml = "";

                ArquivosBLL aBll = new ArquivosBLL();

                aBll.FInsert(arquivo);

                Response.Redirect("ArquivosClientes.aspx");

            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "excluir")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];
                string key = row.Cells[0].Text;
                ArquivosBLL arqBll = new ArquivosBLL();
                ArquivosModel arquivo = arqBll.FGetCustom(string.Format(string.Format("id ={0}", key))).First();

                if(arquivo.status != "P")
                {
                    erroGridLabel.Text = "Esse arquivo já foi processsado, impossível remover!";
                    return;
                }
                
                arqBll.FrameworkDelete(arquivo);
                Response.Redirect("ArquivosClientes.aspx");
            }
        }
    }
}