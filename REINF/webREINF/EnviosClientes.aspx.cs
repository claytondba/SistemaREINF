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
        }
    }
}