using Model.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Sys_filterModel : Framework.Model
    {
        public Sys_filterModel()
        {
            //Nome da Tabela
            Entity = "sys_filter";
            //Indica se a tabela tem outro indice de controle
            //Primitive = true;
            //ChangePrimitiveController - Troca a coluna de controle
            //NamePrimitiveController - Nome da coluna do indice de controle
        }
        [PrimaryKey(isKey = true)]
        public Int64 codigo { get; set; }
        public string filter_name { get; set; }
        public string filter_desc { get; set; }
        public string filter_str { get; set; }
        public Int64 form { get; set; }
        public bool public_ { get; set; }
        public Int64 user_ { get; set; }
    }
}