using BLL;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
                        int idSelecionado = int.Parse(linha.Cells[0].Text);
                        IdSelecionados.Add(idSelecionado);
                    }
                }
            }

            List<ArquivoTempBLL> XlsEventos = ObjConsumoWebServiceBLL.ArquivosXlsSelecionados(IdSelecionados);

            for (int countArquivosRetorno = 0; countArquivosRetorno < XlsEventos.Count(); countArquivosRetorno++)
            {
                var byteEvento = XlsEventos[countArquivosRetorno].arquivo_xls;
                var nomeArquivo = XlsEventos[countArquivosRetorno].nome_arquivo;
                DataTable Identifica = new DataTable();
                DataRow Identificalinha = Identifica.NewRow();

                MemoryStream ObjMemoryStream = new MemoryStream(byteEvento);
                ExcelLibrary.SpreadSheet.Workbook wb = ExcelLibrary.SpreadSheet.Workbook.Load(ObjMemoryStream);
                ExcelLibrary.SpreadSheet.Worksheet ws = wb.Worksheets[0];

                ExcelLibrary.SpreadSheet.Row Headers = ws.Cells.GetRow(0);
                ExcelLibrary.SpreadSheet.Row linhaVerifica = ws.Cells.GetRow(1);

                Identifica.Columns.Add(new DataColumn(Headers.GetCell(Headers.LastColIndex).StringValue, typeof(string)));
                Identificalinha[Headers.GetCell(Headers.LastColIndex).StringValue] = linhaVerifica.GetCell(linhaVerifica.LastColIndex);
                Identifica.Rows.Add(Identificalinha);
                var numeroEvento = Identifica.Rows[0]["evento"];

            }

            //var bytao = XlsEventos[0].arquivo_xls;
            //var nomeByte = XlsEventos[0].nome_arquivo;
            //DataTable dft = new DataTable();                                  

            //MemoryStream ObjMemoryStream = new MemoryStream(bytao);                        
            //ExcelLibrary.SpreadSheet.Workbook wb = ExcelLibrary.SpreadSheet.Workbook.Load(ObjMemoryStream);
            //ExcelLibrary.SpreadSheet.Worksheet ws = wb.Worksheets[0];

            //ExcelLibrary.SpreadSheet.Row Headers = ws.Cells.GetRow(0);

            //dft.TableName = "Contribuinte";
            //dft.Columns.Add(new System.Data.DataColumn(Headers.GetCell(0).StringValue, typeof(string)));
            //dft.Columns.Add(new DataColumn(Headers.GetCell(1).StringValue, typeof(int)));
            //dft.Columns.Add(new DataColumn(Headers.GetCell(2).StringValue, typeof(string)));
            //dft.Columns.Add(new DataColumn(Headers.GetCell(3).StringValue, typeof(string)));
            //dft.Columns.Add(new DataColumn(Headers.GetCell(4).StringValue, typeof(string)));
            //dft.Columns.Add(new DataColumn(Headers.GetCell(5).StringValue, typeof(int)));
            //dft.Columns.Add(new DataColumn(Headers.GetCell(6).StringValue, typeof(int)));
            //dft.Columns.Add(new DataColumn(Headers.GetCell(7).StringValue, typeof(int)));
            //dft.Columns.Add(new DataColumn(Headers.GetCell(8).StringValue, typeof(int)));
            //dft.Columns.Add(new DataColumn(Headers.GetCell(9).StringValue, typeof(string)));
            //dft.Columns.Add(new DataColumn(Headers.GetCell(10).StringValue, typeof(string)));
            //dft.Columns.Add(new DataColumn(Headers.GetCell(11).StringValue, typeof(string)));
            //dft.Columns.Add(new DataColumn(Headers.GetCell(12).StringValue, typeof(string)));
            //dft.Columns.Add(new DataColumn(Headers.GetCell(13).StringValue, typeof(string)));



            //dft.Columns.Add(new System.Data.DataColumn(Headers.GetCell(14).StringValue, typeof(string)));
            //dft.Columns.Add(new DataColumn(Headers.GetCell(15).StringValue, typeof(string)));
            //dft.Columns.Add(new DataColumn(Headers.GetCell(16).StringValue, typeof(string)));
            //dft.Columns.Add(new DataColumn(Headers.GetCell(17).StringValue, typeof(int)));
            //dft.Columns.Add(new DataColumn(Headers.GetCell(18).StringValue, typeof(string)));

            //dft.Columns.Add(new DataColumn(Headers.GetCell(19).StringValue, typeof(string)));
            //dft.Columns.Add(new DataColumn(Headers.GetCell(20).StringValue, typeof(string)));            

            //ExcelLibrary.SpreadSheet.Row Linha2 = ws.Cells.GetRow(1);

            //DataRow novalinha2 = dft.NewRow();
            //novalinha2[Headers.GetCell(0).StringValue] = Linha2.GetCell(0).StringValue;
            //novalinha2[Headers.GetCell(1).StringValue] = Linha2.GetCell(1).StringValue;
            //novalinha2[Headers.GetCell(2).StringValue] = Linha2.GetCell(2).StringValue;
            //novalinha2[Headers.GetCell(3).StringValue] = Linha2.GetCell(3).StringValue;
            //novalinha2[Headers.GetCell(4).StringValue] = Linha2.GetCell(4).StringValue;
            //novalinha2[Headers.GetCell(5).StringValue] = Linha2.GetCell(5).StringValue;
            //novalinha2[Headers.GetCell(6).StringValue] = Linha2.GetCell(6).StringValue;
            //novalinha2[Headers.GetCell(7).StringValue] = Linha2.GetCell(7).StringValue;
            //novalinha2[Headers.GetCell(8).StringValue] = Linha2.GetCell(8).StringValue;
            //novalinha2[Headers.GetCell(9).StringValue] = Linha2.GetCell(9).StringValue;
            //novalinha2[Headers.GetCell(10).StringValue] = Linha2.GetCell(10).StringValue;
            //novalinha2[Headers.GetCell(11).StringValue] = Linha2.GetCell(11).StringValue;
            //novalinha2[Headers.GetCell(12).StringValue] = Linha2.GetCell(12).StringValue;
            //novalinha2[Headers.GetCell(13).StringValue] = Linha2.GetCell(13).StringValue;


            //novalinha2[Headers.GetCell(14).StringValue] = Linha2.GetCell(14).StringValue;
            //novalinha2[Headers.GetCell(15).StringValue] = Linha2.GetCell(15).StringValue;
            //novalinha2[Headers.GetCell(16).StringValue] = Linha2.GetCell(16).StringValue;
            //novalinha2[Headers.GetCell(17).StringValue] = Linha2.GetCell(17).StringValue;
            //novalinha2[Headers.GetCell(18).StringValue] = Linha2.GetCell(18).StringValue;

            //novalinha2[Headers.GetCell(19).StringValue] = Linha2.GetCell(19).StringValue;
            //novalinha2[Headers.GetCell(20).StringValue] = Linha2.GetCell(20).StringValue;

            //dft.Rows.Add(novalinha2);

            //for (int countindex = 2; countindex < ws.Cells.Rows.Count(); countindex++)
            //{
            //    ExcelLibrary.SpreadSheet.Row Linha = ws.Cells.GetRow(countindex);

            //    DataRow novalinha = dft.NewRow();

            //    novalinha[Headers.GetCell(14).StringValue] = Linha.GetCell(14).StringValue;
            //    novalinha[Headers.GetCell(15).StringValue] = Linha.GetCell(15).StringValue;
            //    novalinha[Headers.GetCell(16).StringValue] = Linha.GetCell(16).StringValue;
            //    novalinha[Headers.GetCell(17).StringValue] = Linha.GetCell(17).StringValue;
            //    novalinha[Headers.GetCell(18).StringValue] = Linha.GetCell(18).StringValue;

            //    dft.Rows.Add(novalinha);
            //}            


            //DirectoryInfo ObjDiretorioApp = new DirectoryInfo($"{Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)}\\GerenciamentoExcel");
            //ObjDiretorioApp.Create();
            //DirectoryInfo ObjDiretorioContribuinte = ObjDiretorioApp.CreateSubdirectory("R1000");
            //string diretorioFinal = ObjDiretorioContribuinte.FullName + "\\" + nomeByte;

            //FileStream fileStream = new FileStream(diretorioFinal, FileMode.Create);
            //fileStream.Write(bytao,0, bytao.Length);



        }
    }
}