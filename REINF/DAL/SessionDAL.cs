using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using My = MySql.Data.MySqlClient;

namespace DAL
{
    public class SessionDAL : Framework.DAL<SessionModel>
    {
        public SessionModel GetCurrentSession()
        {
            return MySql.ConnectionManager.Sessao;
        }
        public SessionDAL()
        {
            MySql.ConnectionManager.NoLog = true;
        }
        public List<Exception> GetDebug(out int qtError)
        {
            MySql.ConnectionManager.NoLog = true;
            qtError = MySql.ConnectionManager.ErrosQt;
            return MySql.ConnectionManager.ErrorColection;
        }
        public bool Connect(SessionModel sessao)
        {
            MySql.ConnectionManager.NoLog = true;
            MySql.ConnectionManager.cn = new My.MySqlConnection(
                string.Format("Server={0};Database={1};Uid={2};Pwd={3};ConnectionTimeout=120;convert zero datetime=True;", sessao.Server, sessao.Database, sessao.User, sessao.Password));
            MySql.ConnectionManager.Inicializar();

            return true;
        }
        public void RegisterSession(SessionModel sessao)
        {
            MySql.ConnectionManager.Sessao = sessao;
            MySql.ConnectionManager.Usuario = sessao.Usuario;
        }
        public int GetReqMsg(int usuario)
        {
            MySql.ConnectionManager.NoLog = true;
            string sql = string.Format("select coalesce(msg_last,0) from session where usuario = {0} limit 1", usuario);
            return Convert.ToInt32(MySql.ConnectionManager.executeScalar(sql));
        }
        public int GetReqMeta(int usuario)
        {
            MySql.ConnectionManager.NoLog = true;
            string sql = string.Format("select coalesce(req_meta,0) from session where usuario = {0} limit 1", usuario);
            return Convert.ToInt32(MySql.ConnectionManager.executeScalar(sql));
        }
        public int GetReqContas(int usuario)
        {
            MySql.ConnectionManager.NoLog = true;
            string sql = string.Format("select coalesce(req_contas,0) from session where usuario = {0} limit 1", usuario);
            return Convert.ToInt32(MySql.ConnectionManager.executeScalar(sql));
        }
        public int GetNumComp(int usuario)
        {
            MySql.ConnectionManager.NoLog = true;
            string sql = string.Format("select coalesce(comp_last,0) from session where usuario = {0} limit 1", usuario);
            return Convert.ToInt32(MySql.ConnectionManager.executeScalar(sql));
        }
        public int UpdateNumComp(int usuario, int num)
        {
            MySql.ConnectionManager.NoLog = true;
            string sql = string.Format("update comp_last set comp_last= {0} where usuario ={1};",num, usuario);
            return Convert.ToInt32(MySql.ConnectionManager.executeScalar(sql));
        }
        public bool SaveCapture(int usuario, Byte[] image)
        {
            string sql = "update session set capt =@capt where usuario = @usuario";

            List<My.MySqlParameter> lista = new List<My.MySqlParameter>();
            lista.Add(new My.MySqlParameter("capt",image));
            lista.Add(new My.MySqlParameter("usuario", usuario));
            MySql.ConnectionManager.NoLog = true;
            return MySql.ConnectionManager.update(sql, lista);
        }
        public Byte[] GetCapture(int usuario)
        {
            MySql.ConnectionManager.NoLog = true;
            string sql = string.Format("select capt from session where usuario = {0}", usuario);
            return (Byte[])(MySql.ConnectionManager.executeScalar(sql));
        }
        public bool CheckUserSession(int usuario)
        {
            MySql.ConnectionManager.NoLog = true;
            string sql = string.Format("select count(*) from session where usuario = {0}", usuario);
            return Convert.ToBoolean(MySql.ConnectionManager.executeScalar(sql));
        }
        public bool DesconectAll()
        {
            MySql.ConnectionManager.NoLog = true;
            return MySql.ConnectionManager.update("update `session` set command =1");
        }
        public bool CheckSessionDown(string hash)
        {
            MySql.ConnectionManager.NoLog = true;
            string sql = string.Format("select pulse < (now() - interval 1 minute) from `session` where hash='{0}'", hash);
            return Convert.ToBoolean(MySql.ConnectionManager.executeScalar(sql));
        }
        public bool ReplyMessage(int fromUsu, int usu, string message)
        {

            string sql = string.Format("update `session` set command =2, usu_msg ={0}, msg='{1}' where usuario ={2}", usu, message, fromUsu);
            return MySql.ConnectionManager.update(sql);
        }
        public Int64 GetKey(string hash)
        {
            MySql.ConnectionManager.NoLog = true;
            return Convert.ToInt64(MySql.ConnectionManager.executeScalar(
                string.Format("select recno from `session` where hash ='{0}'", hash)));
        }
        public bool Pulse(SessionModel session)
        {
            MySql.ConnectionManager.NoLog = true;
            return Convert.ToBoolean(MySql.ConnectionManager.executeScalar(
                string.Format("update `session` set pulse ='{0:yyyy-MM-dd HH:mm:ss}', tela_atual = '{2}' where hash ='{1}'", 
                                DateTime.Now,  session.hash, session.tela_atual)));
        }
        public void GetCommand(SessionModel sessao)
        {
            try
            {
                MySql.ConnectionManager.NoLog = true;
                sessao.command = Convert.ToInt32(MySql.ConnectionManager.executeScalar(string.Format("select `command` from `session` where hash ='{0}'", sessao.hash)));
                MySql.ConnectionManager.NoLog = true;
                sessao.msg = MySql.ConnectionManager.executeScalar(string.Format("select `msg` from `session` where hash ='{0}'", sessao.hash)).ToString();
                MySql.ConnectionManager.NoLog = true;
                sessao.usu_msg = Convert.ToInt32(MySql.ConnectionManager.executeScalar(string.Format("select `usu_msg` from `session` where hash ='{0}'", sessao.hash)));
            }
            catch (Exception)
            {
                                
            }
            
        }
    }
}
