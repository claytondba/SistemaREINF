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
    public class ArquivosBLL: Framework.BLL<ArquivosModel>, Framework.IBLL
    {
        public DataTable GetLastUploads(int id_parceiro)
        {
            return new ArquivosDAL().GetLastUploads(id_parceiro);

        }
    }
}
