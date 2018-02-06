using Model.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class SysReports: Framework.Model
    {
        public SysReports()
        {
            Entity = "sys_reports";
        }
        [PrimaryKey(isKey=true)]
        public System.Int32 recno { get; set; }
        public System.String report_name { get; set; }
        public System.String desc_report { get; set; }
        public System.SByte public_ { get; set; }
        public System.Int32 usuario { get; set; }
        public System.Int32 formulario { get; set; }
        public System.String file_name { get; set; }
        public System.String modo_pd { get; set; }
        [NotField(isNotField=true)]
        public List<SysReportData> ReportData { get; set; }
    }
}
