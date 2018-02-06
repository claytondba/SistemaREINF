using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;

namespace DAL
{
    public class SysProcDAL: Framework.DAL<SysProcModel>
    {
        public DataTable GetProcByForm(Int64 codForm)
        {
            string sql = string.Format("select p.proc_name, p.id, p.short_key from sys_proc p " +
                                "join sys_proc_form r on p.id = r.sys_proc " +
                                "join formularios t on t.codigo = r.form " +
                                "where t.codigo = {0};", codForm);
            return MySql.ConnectionManager.consultaDt(sql);


        }
    }
}
