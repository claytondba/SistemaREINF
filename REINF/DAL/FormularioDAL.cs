using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;

namespace DAL
{
    public class FormularioDAL: Framework.DAL<FormularioModel>
    {
        public List<LockUpModel> LoockUp(string parteNome)
        {
            string sql = "SELECT coalesce(concat('(', nome ,') ',descricao),'')as nome, coalesce(codigo,0) as codigo FROM formularios WHERE ";
            sql += " nome LIKE '%" + parteNome.ToUpper() + "%' or descricao like '%" + parteNome.ToUpper() + "%'";
            sql += " ORDER BY nome";

            DataTable dt = MySql.ConnectionManager.consultaDt(sql);
            DataTableReader dr = new DataTableReader(dt);
            List<LockUpModel> list = new List<LockUpModel>();
            LockUpModel loc = new LockUpModel();

            while (dr.Read())
            {
                loc = new LockUpModel();
                loc.Descricao = dr.GetString(0);
                loc.Codigo = Convert.ToUInt32(dr[1]).ToString();

                list.Add(loc);
            }

            return list;
        }
        public bool InsertFormPerfil(int codPerfil, int codFormulario)
        {
            string sql = string.Format("insert into perfil_formularios (perfil, formulario) values({0},{1})", codPerfil, codFormulario);
            return MySql.ConnectionManager.update(sql);
        }
        public bool RemoveFormPerfil(int codPerfil, int codFormulario)
        {
            string sql = string.Format("delete from perfil_formularios where perfil ={0} and formulario ={1}", codPerfil, codFormulario);
            return MySql.ConnectionManager.delete(sql);
        }
        public List<FormularioModel> GetFormsByPerfil(int perfil)
        {
            string sql = string.Format("select distinct codigo, nome, descricao from formularios f " +
                            "left join perfil_formularios pf on pf.formulario = f.codigo " +
                            "left join perfil_usuarios pu on pu.perfil = pf.perfil " +
                            "where pf.perfil = {0};", perfil);

            DataTable dt = MySql.ConnectionManager.consultaDt(sql);
            DataTableReader dr = new DataTableReader(dt);
            List<FormularioModel> lista = new List<FormularioModel>();
            FormularioModel form;

            while (dr.Read())
            {
                form = new FormularioModel();
                form.codigo = Convert.ToUInt32(dr[0]);
                form.nome = dr.GetString(1);
                form.descricao = dr.GetString(2);

                lista.Add(form);
            }

            return lista;
        }
        public bool CheckSysPermissiom(int codUser, string form)
        {
            string sql = string.Format("select count(*) from formularios f " +
                            "left join perfil_formularios pf on pf.formulario = f.codigo " +
                            "left join perfil_usuarios pu on pu.perfil = pf.perfil " +
                            "where pu.usuarios ={0} and f.nome = '{1}';", codUser, form);

            return Convert.ToBoolean(MySql.ConnectionManager.executeScalar(sql));
        }
        public List<FormularioModel> Get(int codForm)
        {
            //this.CustomWhere = "where formulario =" + CodForm;
            List<FormularioModel> lista = this.FrameworkGet(new FormularioModel() { codigo = Convert.ToInt64(codForm) });
            FieldDAL fDal = new FieldDAL();
            fDal.CustomWhere = "formulario =" + lista.First().codigo;
            fDal.CustomOrder = "order_, codigo";
            lista.First().campos = fDal.FrameworkGet(new FieldsModel());

            return lista;
        }
        public List<FormularioModel> Get(string formName)
        {
            this.CustomWhere = "nome ='" + formName.Replace("NovaMC.", "") + "'";

            List<FormularioModel> lista = this.FrameworkGet(new FormularioModel());

            return lista;
        }
    }
}
