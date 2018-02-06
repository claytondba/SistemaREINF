using Model.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Model
{
    public class SysReportData: Framework.Model
    {
        public SysReportData()
        {
            Entity = "sys_report_data";
        }

        [PrimaryKey(isKey=true)]
        public System.Int32 recno { get; set; }
        public System.Int32 report { get; set; }
        public System.String sql_data { get; set; }
        public System.String data_name { get; set; }
        [NotField(isNotField = true)]
        public DataTable TableData { get; set; }
    }
}
