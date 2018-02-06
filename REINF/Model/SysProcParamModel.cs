using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Framework;

namespace Model
{
    public class SysProcParamModel : Framework.Model
    {
        public SysProcParamModel()
        {
            Entity = "sys_proc_parameter";
        }
        [PrimaryKey(isKey = true)]
        public Int64 id { get; set; }
        public Int64 sys_proc { get; set; }
        public int param_type { get; set; }
        public string column_search { get; set; }
        public string value_param { get; set; }
        public string caption_param { get; set; }
        public string value_type { get; set; }
        
    }
}
