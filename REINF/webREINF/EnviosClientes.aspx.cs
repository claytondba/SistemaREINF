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
    public partial class EnviosClientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["usuario"] == null)
                    Response.Redirect("Login.aspx");

                if (Session["id_cliente"] == null)
                    Response.Redirect("HomeParceiro.aspx");

                UsuariosReinfModel usu = (UsuariosReinfModel)Session["usuario"];
                ArquivosBLL usuBll = new ArquivosBLL();

                GridView1.DataSource =
                usuBll.FGetCustom(string.Format(string.Format("id_cliente ={0}", (int)Session["id_cliente"])));

                GridView1.DataBind();
                

                UsuariosReinfModel usuCli = new UsuariosReinfBLL().FGetCustom(string.Format("id ={0}", (int)Session["id_cliente"])).First();
                Label1.Text = usuCli.razao_social;
                Session["id_cliente"] = null;

            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];
                string key = row.Cells[0].Text;
                ArquivosBLL arqBll = new ArquivosBLL();
                ArquivosModel arquivo = arqBll.FGetCustom(string.Format(string.Format("id ={0}", key))).First();

                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + arquivo.nome_arquivo);
                Response.AddHeader("Content-Length", arquivo.arquivo_xls.Length.ToString());
                Response.ContentType = "application/vnd.ms-excel";
                Response.BinaryWrite(arquivo.arquivo_xls);
                Response.End();
            }
            else if (e.CommandName == "seleciona")
            {
                

                int indexa = Convert.ToInt32(e.CommandArgument);
                GridViewRow linha = GridView1.Rows[indexa];
                var chave = linha.Cells[0].Text;
                //var linhaSelecionada = GridView1.SelectedRow.DataItemIndex;
                // var valor = GridView1.DataKeys[linhaSelecionada];



                string selecao = e.CommandName;

            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ConsumoWebServiceBLL ObjConsumoWebServiceBLL = new ConsumoWebServiceBLL();
            List<int> IdSelecionados = new List<int>();            

            foreach (GridViewRow linha in GridView1.Rows)
            {
                CheckBox cbx = linha.FindControl("checks") as CheckBox;

                if (cbx != null)
                {
                    if (cbx.Checked)
                    {
                        //sb.AppendLine(string.Format("Produto {0}", linha.Cells[1].Text));
                        int idSelecionado = int.Parse(linha.Cells[0].Text);
                        IdSelecionados.Add(idSelecionado);
                    }
                }
            }

            List<ArquivoTempBLL> XlsEventos = ObjConsumoWebServiceBLL.ArquivosXlsSelecionados(IdSelecionados);



        }
    }
}