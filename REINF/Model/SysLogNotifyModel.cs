using Model.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class SysLogNotifyModel : Framework.Model
    {
        public SysLogNotifyModel()
        {
            Entity = "sys_log_notify";
            NoAuditTable = true;
        }

        [PrimaryKey(isKey = true)]
        public Int64 codigo { get; set; }
        public Int64 usuario { get; set; }
        public DateTime data_evento { get; set; }
        public string origem { get; set; }
        public string comando { get; set; }
        public string tipo { get; set; }
        public string obs { get; set; }
    }
}
