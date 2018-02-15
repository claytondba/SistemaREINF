using Model.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ConfigModel : Framework.Model
    {
        public ConfigModel()
        {
            Entity = "config";
        }
        [PrimaryKey(isKey = true)]
        public int id { get; set; }
        public string home_cliente_title { get; set; }
        public string home_cliente_info { get; set; }
        public string home_parceiro_title { get; set; }
        public string home_parceiro_info { get; set; }
    }
}
