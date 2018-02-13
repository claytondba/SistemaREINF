using Model.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ArquivosModel : Framework.Model
    {
        public ArquivosModel()
        {
            Entity = "arquivos";
        }
        [PrimaryKey(isKey =true)]
        public int id { get; set; }
        public int id_parceiro { get; set; }
        public int id_cliente { get; set; }
        public byte[] arquivo_xls { get; set; }
        public int tipo_evento { get; set; }
        public string envio_xml { get; set; }
        public string resposta_rfb { get; set; }
    }
}
