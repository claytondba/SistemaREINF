using Model.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class SysFormTriggerModel: Framework.Model
    {
        public SysFormTriggerModel()
        {
            Entity = "sys_form_trigger";
        }

        [PrimaryKey(isKey=true)]
        public Int64 codigo { get; set; }
        public Int64 form { get; set; }
        public string trigger_name { get; set; }
        public string type_call { get; set; }
        public string proc_name { get; set; }
        public Int64 type_trigger { get; set; }
        public string descricao { get; set; }
        public Int64 param_count { get; set; }
        [NotField(isNotField = true)]
        public List<SysTriggerParamModel> TriggersParam { get; set; }
        public bool show_message { get; set; }
        public string message { get; set; }
        public bool cause_validate { get; set; }
        public Int64 ordem { get; set; }
    }
}
