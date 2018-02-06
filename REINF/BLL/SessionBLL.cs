using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Model;
using System.Drawing;
using System.IO;

namespace BLL
{
    /// <summary>
    /// Classe de controle de sessão e iteração entre usuários
    /// Código de comandos enviados:
    /// 0 - Nada.
    /// 1 - Fecha o Sistema Imediatamente.
    /// 2 - Envia Mensagem para o Usuário com Direito de Resposta.
    /// 3 - Captura Bitmap da Tela Atual do Usuário.
    /// 4 - Notifica o Usuário sobre Pedidos de Compras
    /// 5 - Notifica o Usuário sobre Mensagens não respondidas
    /// </summary>
    public class SessionBLL : Framework.BLL<SessionModel>, Framework.IBLL
    {
        public bool StopedSession { get; set; }

        public SessionModel GetCurrentSession()
        {
            return new SessionDAL().GetCurrentSession();
        }
        public List<Exception> GetDebug(out int qtError)        
        {
            return new SessionDAL().GetDebug(out qtError);
        }

        //Conecta a sessão do usuário no banco de dados
        public bool Connect(SessionModel sessao)
        {
            return new SessionDAL().Connect(sessao);
        }
        public void RegisterSession(SessionModel sessao)
        {
            new SessionDAL().RegisterSession(sessao);
        }
        public int GetReqMsg(int usuario)
        {
            return new SessionDAL().GetReqMsg(usuario);
        }
        public int GetReqContas(int usuario)
        {
            return new SessionDAL().GetReqContas(usuario);
        }
        public int GetReqMeta(int usuario)
        {
            return new SessionDAL().GetReqMeta(usuario);
        }
        public int UpdateNumComp(int usuario, int num)
        {
            return new SessionDAL().UpdateNumComp(usuario, num);
        }
        public int GetNumComp(int usuario)
        {
            return new SessionDAL().GetNumComp(usuario);
        }
        public bool SaveCapture(int usuario, Byte[] image)
        {
            return new SessionDAL().SaveCapture(usuario, image);
        }
        public Image GetCapture(int usuario)
        {
            MemoryStream ms = new MemoryStream(new SessionDAL().GetCapture(usuario));
            return Image.FromStream(ms);
        }
        public string GetLockup(Model.Framework.Model modelo, int type)
        {
            throw new NotImplementedException();
        }
        public bool CheckUserSession(int usuario)
        {
            return new SessionDAL().CheckUserSession(usuario);
        }
        public bool DesconectAll()
        {
            return new SessionDAL().DesconectAll();
        }
        public bool CheckSessionDown(string hash)
        {
            return new SessionDAL().CheckSessionDown(hash);
        }
        public bool ReplyMessage(int fromUsu, int usu, string message)
        {
            return new SessionDAL().ReplyMessage(fromUsu, usu, message);
        }
        public bool Pulse(SessionModel sessao)
        {
            return new SessionDAL().Pulse(sessao);
        }
        public Int64 GetKey(string hash)
        {
            return new SessionDAL().GetKey(hash);
        }
        public bool FrameworkInsert(Model.Framework.Model modelo)
        {
            return new SessionDAL().FrameworkInsert(modelo);
        }
        public void GetCommand(SessionModel sessao)
        {
            new SessionDAL().GetCommand(sessao);
        }
        public object FrameworkGet(Model.Framework.Model modelo)
        {
            return new SessionDAL().FrameworkGet(modelo);
        }

        public object FrameworkGetOne(Model.Framework.Model modelo, int recno)
        {
            SessionDAL dal = new SessionDAL();
            dal.CustomWhere = "sr_recno =" + recno;
            return dal.FrameworkGet(modelo).First();
        }
        public bool FrameworkUpdate(Model.Framework.Model modelo)
        {            
            return new SessionDAL().FrameworkUpdate(modelo);
        }

        public bool FrameworkDelete(Model.Framework.Model modelo)
        {
            return new SessionDAL().FrameworkDelete(modelo);
        }

        public object FrameworkGetCustom(Model.Framework.Model modelo, string _customParam)
        {
            SessionDAL dal = new SessionDAL();
            dal.CustomWhere = _customParam;
            return dal.FrameworkGet(modelo);
        }
    }
}
