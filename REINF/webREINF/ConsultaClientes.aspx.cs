using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using BLL;

namespace webREINF
{
    public partial class ConsultaClientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["usuario"] == null)
                    Response.Redirect("Login.aspx");

                UsuariosReinfModel usu = (UsuariosReinfModel)Session["usuario"];
                UsuariosReinfBLL usuBll = new UsuariosReinfBLL();

                GridView1.DataSource =
                usuBll.FGetCustom(string.Format(string.Format("cadastrante ={0}", usu.id)));

                GridView1.DataBind();

                Session["edit_reg"] = false;
                Session["edit_usuario"] = null;
            }
            
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["edit_usuario"] = null;
            string key = GridView1.SelectedRow.Cells[0].Text;
            Session["edit_usuario"] = new UsuariosReinfBLL().FGetCustom(string.Format(string.Format("id ={0}", key))).First();
            Session["edit_reg"] = true;

            Response.Redirect("CadastroCliente.aspx");
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "Delete")
            {
                string key = e.CommandSource.ToString();
            }
            else if(e.CommandName =="excluir")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];
                string key = row.Cells[0].Text;
                UsuariosReinfBLL usuBll = new UsuariosReinfBLL();
                UsuariosReinfModel usu = usuBll.FGetCustom(string.Format(string.Format("id ={0}", key))).First();

                usuBll.FrameworkDelete(usu);
                Response.Redirect("ConsultaClientes.aspx");

            }
            else if (e.CommandName == "download")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];
                string key = row.Cells[0].Text;
                UsuariosReinfBLL usuBll = new UsuariosReinfBLL();
                UsuariosReinfModel usu = usuBll.FGetCustom(string.Format(string.Format("id ={0}", key))).First();

                Session["id_cliente"] = usu.id;

                Response.Redirect("EnviosClientes.aspx");

            }
        }
    }
}