using Model.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class SysTriggerParamModel : Framework.Model
    {
        public SysTriggerParamModel()
        {
            Entity = "sys_trigger_param";
        }
        [PrimaryKey(isKey = true)]
        public Int64 codigo { get; set; }
        public Int64 trigger_ { get; set; }
        public string field_ { get; set; }
        public Int64 order_ { get; set; }
        
    }
}
