using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Framework;

namespace Model
{
    public class SessionModel : Framework.Model
    {
        public enum CommandType
        {
            Disconnect,
            Restart
        }

        public SessionModel()
        {
            Entity = "session";
        }
        [PrimaryKey(isKey = true)]
        public Int64 recno { get; set; }
        public Int64 usuario { get; set; }
        public string perfil { get; set; }
        public string hash { get; set; }
        public string origem { get; set; }
        public string tela_atual { get; set; }
        public DateTime login_inicio { get; set; }
        public DateTime pulse { get; set; }
        public int command { get; set; }
        public string msg { get; set; }
        public int usu_msg { get; set; }
        [NotField(isNotField = true)]
        public byte[] capt { get; set; }
        [NotField(isNotField = true)]
        public bool stop_session { get; set; }
        public int comp_last { get; set; }
        public int msg_last { get; set; }
        public int req_meta { get; set; }
        public int req_contas { get; set; }

        //Propriedades para conexão com o banco de dados
        [NotField(isNotField = true)]
        public string Server { get; set; }
        [NotField(isNotField = true)]
        public string User { get; set; }
        [NotField(isNotField = true)]
        public string Password { get; set; }
        [NotField(isNotField = true)]
        public string Database { get; set; }


        
    }
}
