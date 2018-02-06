using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Framework;

namespace Model
{
    public class FormularioModel: Framework.Model
    {

        public FormularioModel()
        {
            Entity = "formularios";
        }

        [PrimaryKey(isKey=true)]
        public Int64 codigo { get; set; }
        public System.String nome { get; set; }
        public System.String descricao { get; set; }
        [NotField(isNotField = true)]
        public List<FieldsModel> campos { get; set; }
        public string reference_model { get; set; }
        public string reference_persist { get; set; }
        public System.Boolean allowinsert { get; set; }
        public System.Boolean allowdelete { get; set; }
        public System.Boolean allowupdate { get; set; }
        public bool needdate { get; set; }
        public bool need_peca { get; set; }
        public bool re_call { get; set; }
        public bool maximize { get; set; }
        public bool needparam { get; set; }
        public string param_desc { get; set; }
        public bool need_lockup { get; set; }
        public string lockup_name { get; set; }
        [NotField(isNotField = true)]
        public List<SysFormTriggerModel> Triggers { get; set; }
        public string bti_control { get; set; }
        public bool fixed_param { get; set; }
        public bool show_modal { get; set; }
        public int active_filter { get; set; }
    }
}
