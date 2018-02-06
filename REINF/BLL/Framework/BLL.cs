using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;
using System.Reflection;
using Model;

namespace BLL.Framework
{
    public class BLL<T> where T: Model.Framework.Model
    {
        public bool NoJoinsFunctions { get; set; }
        private Model.UsuariosModel Usuarios { get; set; }

        public void GetUser(Model.UsuariosModel user)
        {
            this.Usuarios = user;
        }
        public bool FrameworkInsert(Model.Framework.Model modelo)
        {
            DAL.Framework.DAL<T> dal = new DAL.Framework.DAL<T>();           
            return dal.FrameworkInsert(modelo);
        }
        public Int64 FInsert(Model.Framework.Model modelo)
        {
            DAL.Framework.DAL<T> dal = new DAL.Framework.DAL<T>();
            return dal.FInsert(modelo);
        }
        public DataTable FrameworkGetTable(string sql)
        {
            sql = sql.ToLower();
            if (sql.Contains("delete") || sql.Contains("update"))
                throw new InvalidOperationException("Não é possível enviar uma instrução DELETE/UPDATE por GetTable");

            DataTable result = new DAL.Framework.DAL<T>().FrameworkGetTable(sql);
            result.TableName = "TableResult";
            //LastComand = sql;
            //API.LastCommand = LastComand;
            return result;
        }
        public object FrameworkGet(Model.Framework.Model modelo)
        {
            DAL.Framework.DAL<T> dal = new DAL.Framework.DAL<T>();
            dal.NoJoinsFunctions = this.NoJoinsFunctions;
            return dal.FrameworkGet(modelo);
        }

        /// <summary>
        /// Retorna lista de objetos tipados
        /// </summary>
        /// <returns></returns>
        public List<T> FGet()
        {
            DAL.Framework.DAL<T> dal = new DAL.Framework.DAL<T>();
            dal.NoJoinsFunctions = this.NoJoinsFunctions;
            return dal.FrameworkGet(Activator.CreateInstance<T>());
        }

        public object FrameworkGetOne(Model.Framework.Model modelo, int recno)
        {
            DAL.Framework.DAL<T> dal = new DAL.Framework.DAL<T>();
            dal.NoJoinsFunctions = true;
            dal.CustomWhere = modelo.FieldRecno + " = " + recno;
            return dal.FrameworkGet(modelo).First();
        }

        public bool FrameworkUpdate(Model.Framework.Model modelo)
        {
            SessionModel se = new SessionBLL().GetCurrentSession();

            //Model.Framework.Model antModel = modelo.Fileds.
            if (!modelo.NoAuditTable)
            {
                if (se != null)
                {
                    new SysLogNotifyBLL().FrameworkInsert(new SysLogNotifyModel()
                    {
                        usuario = Convert.ToInt64(se.usuario),
                        data_evento = DateTime.Now,
                        comando = string.Format("Registro da tabela \"{0}\" foi atualizado.", modelo.Entity),
                        origem = se.origem,
                        obs = string.Format("{0}", modelo.Serialize()),
                        tipo = "update"
                    });
                }
            }
            DAL.Framework.DAL<T> dal = new DAL.Framework.DAL<T>();
            return dal.FrameworkUpdate(modelo);
        }

        public bool FrameworkDelete(Model.Framework.Model modelo)
        {
            SessionModel se = new SessionBLL().GetCurrentSession();
            if (!modelo.NoAuditTable)
            {
                if (se != null)
                {
                    new SysLogNotifyBLL().FrameworkInsert(new SysLogNotifyModel()
                    {
                        usuario = Convert.ToInt64(se.usuario),
                        data_evento = DateTime.Now,
                        comando = string.Format("Registro da tabela \"{0}\" foi excluido.", modelo.Entity),
                        origem = se.origem,
                        obs = modelo.Serialize(),
                        tipo = "delete"
                    });
                }
            }
            DAL.Framework.DAL<T> dal = new DAL.Framework.DAL<T>();
            return dal.FrameworkDelete(modelo);
        }

        public object FrameworkGetCustom(Model.Framework.Model modelo, string _customParam)
        {
            DAL.Framework.DAL<T> dal = new DAL.Framework.DAL<T>();
            dal.NoJoinsFunctions = this.NoJoinsFunctions;
            dal.CustomWhere = _customParam;
            return dal.FrameworkGet(modelo);
        }

        /// <summary>
        /// Retorna uma lista tipada de objetos (não necessita de cast)
        /// </summary>
        /// <param name="_customParam">Parametros para consulta SQL</param>
        /// <returns></returns>
        public List<T> FGetCustom(string _customParam)
        {
            DAL.Framework.DAL<T> dal = new DAL.Framework.DAL<T>();
            dal.NoJoinsFunctions = this.NoJoinsFunctions;
            dal.CustomWhere = _customParam;
            return dal.FrameworkGet(Activator.CreateInstance<T>());
        }
        public List<Model.LockUpModel> FLockup(string lockupName, string parteNome)
        {
            string _entityName = lockupName;

            List<SysLockupModel> lup = new SysLockupBLL().FGetCustom(
                string.Format("lockup_name ='{0}'", _entityName));

            if (lup == null)
            {
                throw new Exception("Sem lockup criado para esse modelo!!");
            }
            else if (lup.Count == 0)
            {
                throw new Exception("Sem lockup criado para esse modelo!!");
            }

            DAL.Framework.DAL<T> dal = new DAL.Framework.DAL<T>();
            DataTable dt = new DataTable();
            if (lup.First().init_load && parteNome.Length == 0)
            {
                dt = dal.FrameworkGetTable(string.Format(lup.First().sql_init_load));
            }
            else
            {
                dt = dal.FrameworkGetTable(string.Format(lup.First().sql_source, parteNome));
            }
            DataTableReader dr = new DataTableReader(dt);
            List<Model.LockUpModel> list = new List<Model.LockUpModel>();
            Model.LockUpModel loc = new Model.LockUpModel();

            while (dr.Read())
            {
                loc = new Model.LockUpModel();
                loc.Codigo = dr[0].ToString();

                if (dr[1].GetType().ToString() != "System.DBNull")
                    loc.Descricao = dr.GetString(1);

                list.Add(loc);
            }

            return list;


        }
        public List<Model.LockUpModel> FLockup(Model.Framework.Model classModule, string parteNome)
        {
            string _entityName = "";
            foreach (PropertyInfo propriedade in classModule.GetType().GetProperties())
            {
                if (propriedade.GetCustomAttributes(typeof(Model.Framework.NotField), false).Count() == 0)
                {
                    if (propriedade.GetCustomAttributes(typeof(Model.Framework.EntityConfiguration), false).Count() > 0)
                    {
                        _entityName = propriedade.GetValue(classModule, null).ToString();
                    }
                }
            }

            if(string.IsNullOrEmpty(_entityName))
            {
                throw new Exception("Este modelo não tem chave primária definida!");
            }

            List<SysLockupModel> lup = new SysLockupBLL().FGetCustom(
                string.Format("lockup_name ='{0}'", _entityName));

            if (lup == null)
            {
                throw new Exception("Sem lockup criado para esse modelo!!");
            }
            else if (lup.Count == 0)
            {
                throw new Exception("Sem lockup criado para esse modelo!!");
            }

            DAL.Framework.DAL<T> dal = new DAL.Framework.DAL<T>();

            DataTable dt = dal.FrameworkGetTable(string.Format(lup.First().sql_source,parteNome));
            DataTableReader dr = new DataTableReader(dt);
            List<Model.LockUpModel> list = new List<Model.LockUpModel>();
            Model.LockUpModel loc = new Model.LockUpModel();

            while (dr.Read())
            {
                loc = new Model.LockUpModel();
                loc.Codigo = dr[0].ToString();

                loc.Descricao = dr[1].ToString();

                list.Add(loc);
            }

            return list;
            

        }
        public string GetLockup(Model.Framework.Model modelo, int type)
        {
            DAL.Framework.DAL<T> dal = new DAL.Framework.DAL<T>();
            return dal.GetLockup(modelo, type);
        }
        public DataTable FLockUp()
        {
            return new DataTable();
        }
        public T FrameworkGetOneModel(int recno)
        {
            DAL.Framework.DAL<T> dal = new DAL.Framework.DAL<T>();
            Model.Framework.Model modelo = Activator.CreateInstance<T>();
            try
            {
                dal.NoJoinsFunctions = true;
                dal.CustomWhere = modelo.FieldRecno + " = " + recno;
                return dal.FrameworkGet(modelo).First();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro tentando recuperar o registro!\n" + ex.Message);
            }

        }
    }
}
