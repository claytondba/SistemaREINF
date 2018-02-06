using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class SysFormTriggerBLL:Framework.BLL<SysFormTriggerModel>, Framework.IBLL
    {
        public bool GetBoolean(string proc_name)
        {
            return new SysFormTriggerDAL().GetBoolean(proc_name);
        }
    }
}
