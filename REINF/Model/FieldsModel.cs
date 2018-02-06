using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Framework;

namespace Model
{
    public class FieldsModel: Framework.Model
    {
        public FieldsModel()
        {
            Entity = "fields";
        }

        [PrimaryKey(isKey=true)]
        public System.Int64 codigo { get; set; }
        public System.Int64 formulario { get; set; }
        public System.String type { get; set; }
        public System.Boolean mask { get; set; }
        public System.String text_mask { get; set; }
        public System.String name { get; set; }
        public System.String descricao { get; set; }
        public System.String help { get; set; }
        public System.String default_ { get; set; }
        public System.Boolean system { get; set; }
        public int size { get; set; }
        public bool allownull { get; set; }
        public string combo_value { get; set; }
        public bool browse { get; set; }
        public Int64 order_ { get; set; }
        public bool pad_left{ get; set; }
        public string pad_carac{ get; set; }
        public int pad_number { get; set; }
        public bool to_upper { get; set; }
        public string lockup_name { get; set; }
        public bool read_only { get; set; }
        public int max_len { get; set; }
        public string format { get; set; }
        public bool pesquisa { get; set; }
        public System.String model_reference { get; set; }
        public int lockup_type { get; set; }
        public string model_lockup { get; set; }
        public string persist_lockup { get; set; }
        public string pesquisa_key { get; set; }

    }
}
