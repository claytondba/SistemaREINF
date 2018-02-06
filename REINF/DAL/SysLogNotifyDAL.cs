using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL
{
    public class SysLogNotifyDAL: Framework.DAL<SysLogNotifyModel>
    {
        public DataTable GetMetas(DateTime inicio, DateTime fim)
        {
            string sql = string.Format(
                "select u.nome, s.data_evento, s.comando from sys_log_notify s "+
                    "join usuarios u on u.codigo = s.usuario  " +
                    "where s.tipo = 'meta' and data_evento BETWEEN '{0:yyyy-MM-dd}' and '{1:yyyy-MM-dd}'",
                inicio, fim);

            return MySql.ConnectionManager.consultaDt(sql);

        }
    }
}
