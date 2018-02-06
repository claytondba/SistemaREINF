using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Framework;

namespace Model
{
    public class SysNotifyModel: Framework.Model
    {
        public SysNotifyModel()
        {
            Entity = "sys_notify";
        }
        [PrimaryKey(isKey=true)]
        public Int64 id { get; set; }
        public string nome { get; set; }
        public bool ballon { get; set; }
        public string ballon_text { get; set; }
        public Int64 perfil_allow { get; set; }
        public int img_index { get; set; }
    }
}
