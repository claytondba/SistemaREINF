using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class SysFormTriggerDAL : Framework.DAL<SysFormTriggerModel>
    {
        public bool GetBoolean(string proc_name)
        {
            if (!proc_name.StartsWith("select"))
                return Convert.ToBoolean(MySql.ConnectionManager.executeScalar("select " + proc_name));
            else
                return Convert.ToBoolean(MySql.ConnectionManager.executeScalar(proc_name));
        }
    }
}
