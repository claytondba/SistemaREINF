using Model.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class SysKeyValueModel : Framework.Model
    {
        public SysKeyValueModel()
        {
            //Nome da Tabela
            Entity = "sys_key_value";
            NoAuditTable = true;
            //Indica se a tabela tem outro indice de controle
            //Primitive = true;
            //ChangePrimitiveController - Troca a coluna de controle
            //NamePrimitiveController - Nome da coluna do indice de controle
        }
        [PrimaryKey(isKey = true)]
        public Int64 codigo { get; set; }
        public string key_name { get; set; }
        public string key_descr { get; set; }
        public string key_value { get; set; }
        public Int64 key_group { get; set; }
    }
}