using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Framework;

namespace Model
{
    public class SysMenusModel : Framework.Model
    {
        public SysMenusModel()
        {
            Entity = "sys_menus"; 
        }
        [PrimaryKey(isKey=true)]
        public UInt32 codigo { get; set; }
        public String descricao { get; set; }
        public String title { get; set; }
        public String resource { get; set; }
        public UInt32 master { get; set; }
        public UInt32 can_data { get; set; }
        public String can_param { get; set; }
        public UInt32 lockup { get; set; }
        public String type_lockup { get; set; }
        public UInt32 usuario { get; set; }
        public UInt32 read_only { get; set; }
        public String patch { get; set; }
        public String root { get; set; }
        public String child { get; set; }
        public string child_parent { get; set; }
        public UInt32 formulario { get; set; }
        public UInt32 order_ { get; set; }
    }
}
