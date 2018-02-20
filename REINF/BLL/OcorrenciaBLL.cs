using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class OcorrenciaBLL: Framework.BLL<OcorrenciaModel>
    {
        public DataTable GetOcorrenciasByParceiro(int id_parceiro)
        {
            return new OcorrenciaDAL().GetOcorrenciasByParceiro(id_parceiro);
        }
    }
}
