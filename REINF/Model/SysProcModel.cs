using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Framework;

namespace Model
{
    public class SysProcModel : Framework.Model
    {
        public SysProcModel()
        {
            Entity = "sys_proc";
        }
        [PrimaryKey(isKey=true)]
        public Int64 id { get; set; }
        public string proc_name { get; set; }
        public string class_name { get; set; }
        public int param_count { get; set; }
        public string proc_descr { get; set; }
        public string func_name { get; set; }
        public bool confirm { get; set; }
        public string confirm_message { get; set; }
        public bool reload_grid { get; set; }
        public string short_key { get; set; }
        public string ok_msg { get; set; }
    }
}
