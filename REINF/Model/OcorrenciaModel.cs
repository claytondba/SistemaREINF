using Model.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class OcorrenciaModel : Framework.Model
    {
        public OcorrenciaModel()
        {
            Entity = "ocorrencia";
        }
        [PrimaryKey(isKey = true)]
        public int id { get; set; }
        public string cnpj { get; set; }
        public string razaosocial { get; set; }
        public string evento { get; set; }
        public string tipo { get; set; }
        public string localiza { get; set; }
        public string codigo { get; set; }
        public string descricao { get; set; }
        public string idevento { get; set; }
        public int id_usuario { get; set; }
        public string recibo { get; set; }

    }
}
