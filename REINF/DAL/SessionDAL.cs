using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using My = Npgsql;

namespace DAL
{
    public class SessionDAL : Framework.DAL<SessionModel>
    {
        public SessionModel GetCurrentSession()
        {
            return Postgres.ConnectionManager.Sessao;
        }
        public SessionDAL()
        {
            Postgres.ConnectionManager.NoLog = true;
        }
        public List<Exception> GetDebug(out int qtError)
        {
            Postgres.ConnectionManager.NoLog = true;
            qtError = Postgres.ConnectionManager.ErrosQt;
            return Postgres.ConnectionManager.ErrorColection;
        }
        public bool Connect(SessionModel sessao)
        {
            Postgres.ConnectionManager.NoLog = true;
            Postgres.ConnectionManager.cn = new My.NpgsqlConnection(
                string.Format("Server={0};Port=5432;Database={1};User Id={2};Password={3};CommandTimeout=120;", sessao.Server, sessao.Database, sessao.User, sessao.Password));
            //string.Format("Server={0};Database={1};Uid={2};Pwd={3};ConnectionTimeout=120;convert zero datetime=True;", sessao.Server, sessao.Database, sessao.User, sessao.Password));
            Postgres.ConnectionManager.Inicializar();

            return true;
        }
        public void RegisterSession(SessionModel sessao)
        {
            Postgres.ConnectionManager.Sessao = sessao;
            Postgres.ConnectionManager.Usuario = sessao.Usuario;
        }
        public int GetReqMsg(int usuario)
        {
            Postgres.ConnectionManager.NoLog = true;
            string sql = string.Format("select coalesce(msg_last,0) from session where usuario = {0} limit 1", usuario);
            return Convert.ToInt32(Postgres.ConnectionManager.executeScalar(sql));
        }
        public int GetReqMeta(int usuario)
        {
            Postgres.ConnectionManager.NoLog = true;
            string sql = string.Format("select coalesce(req_meta,0) from session where usuario = {0} limit 1", usuario);
            return Convert.ToInt32(Postgres.ConnectionManager.executeScalar(sql));
        }
        public int GetReqContas(int usuario)
        {
            Postgres.ConnectionManager.NoLog = true;
            string sql = string.Format("select coalesce(req_contas,0) from session where usuario = {0} limit 1", usuario);
            return Convert.ToInt32(Postgres.ConnectionManager.executeScalar(sql));
        }
        public int GetNumComp(int usuario)
        {
            Postgres.ConnectionManager.NoLog = true;
            string sql = string.Format("select coalesce(comp_last,0) from session where usuario = {0} limit 1", usuario);
            return Convert.ToInt32(Postgres.ConnectionManager.executeScalar(sql));
        }
        public int UpdateNumComp(int usuario, int num)
        {
            Postgres.ConnectionManager.NoLog = true;
            string sql = string.Format("update comp_last set comp_last= {0} where usuario ={1};",num, usuario);
            return Convert.ToInt32(Postgres.ConnectionManager.executeScalar(sql));
        }
        public bool SaveCapture(int usuario, Byte[] image)
        {
            string sql = "update session set capt =@capt where usuario = @usuario";

            List<My.NpgsqlParameter> lista = new List<My.NpgsqlParameter>();
            lista.Add(new My.NpgsqlParameter("capt",image));
            lista.Add(new My.NpgsqlParameter("usuario", usuario));
            Postgres.ConnectionManager.NoLog = true;
            return Postgres.ConnectionManager.update(sql, lista);
        }
        public Byte[] GetCapture(int usuario)
        {
            Postgres.ConnectionManager.NoLog = true;
            string sql = string.Format("select capt from session where usuario = {0}", usuario);
            return (Byte[])(Postgres.ConnectionManager.executeScalar(sql));
        }
        public bool CheckUserSession(int usuario)
        {
            Postgres.ConnectionManager.NoLog = true;
            string sql = string.Format("select count(*) from session where usuario = {0}", usuario);
            return Convert.ToBoolean(Postgres.ConnectionManager.executeScalar(sql));
        }
        public bool DesconectAll()
        {
            Postgres.ConnectionManager.NoLog = true;
            return Postgres.ConnectionManager.update("update `session` set command =1");
        }
        public bool CheckSessionDown(string hash)
        {
            Postgres.ConnectionManager.NoLog = true;
            string sql = string.Format("select pulse < (now() - interval 1 minute) from `session` where hash='{0}'", hash);
            return Convert.ToBoolean(Postgres.ConnectionManager.executeScalar(sql));
        }
        public bool ReplyMessage(int fromUsu, int usu, string message)
        {

            string sql = string.Format("update `session` set command =2, usu_msg ={0}, msg='{1}' where usuario ={2}", usu, message, fromUsu);
            return Postgres.ConnectionManager.update(sql);
        }
        public Int64 GetKey(string hash)
        {
            Postgres.ConnectionManager.NoLog = true;
            return Convert.ToInt64(Postgres.ConnectionManager.executeScalar(
                string.Format("select recno from `session` where hash ='{0}'", hash)));
        }
        public bool Pulse(SessionModel session)
        {
            Postgres.ConnectionManager.NoLog = true;
            return Convert.ToBoolean(Postgres.ConnectionManager.executeScalar(
                string.Format("update `session` set pulse ='{0:yyyy-MM-dd HH:mm:ss}', tela_atual = '{2}' where hash ='{1}'", 
                                DateTime.Now,  session.hash, session.tela_atual)));
        }
        public void GetCommand(SessionModel sessao)
        {
            try
            {
                Postgres.ConnectionManager.NoLog = true;
                sessao.command = Convert.ToInt32(Postgres.ConnectionManager.executeScalar(string.Format("select `command` from `session` where hash ='{0}'", sessao.hash)));
                Postgres.ConnectionManager.NoLog = true;
                sessao.msg = Postgres.ConnectionManager.executeScalar(string.Format("select `msg` from `session` where hash ='{0}'", sessao.hash)).ToString();
                Postgres.ConnectionManager.NoLog = true;
                sessao.usu_msg = Convert.ToInt32(Postgres.ConnectionManager.executeScalar(string.Format("select `usu_msg` from `session` where hash ='{0}'", sessao.hash)));
            }
            catch (Exception)
            {
                                
            }
            
        }
    }
}
