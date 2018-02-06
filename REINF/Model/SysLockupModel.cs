using Model.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class SysLockupModel : Framework.Model
    {
        public SysLockupModel()
        {
            //Nome da Tabela
            Entity = "sys_lockup";
            //Indica se a tabela tem outro indice de controle
            //Primitive = true;
            //ChangePrimitiveController - Troca a coluna de controle
            //NamePrimitiveController - Nome da coluna do indice de controle
        }
        [PrimaryKey(isKey = true)]
        public Int64 codigo { get; set; }
        public string lockup_name { get; set; }
        public string sql_source { get; set; }
        public bool init_load { get; set; }
        public string sql_init_load { get; set; }
    }
}
