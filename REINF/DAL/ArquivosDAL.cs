using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ArquivosDAL: Framework.DAL<ArquivosModel>
    {
        public DataTable GetLastUploads(int id_parceiro)
        {
            string sql = string.Format("select a.id, u.razao_social, a.nome_arquivo, a.data_evento, a.status " +
                                "from arquivos a " +
                                "join usuarios_reinf u on a.id_cliente = u.id " +
                                "where a.id_parceiro = 1;", id_parceiro);

            return DAL.Postgres.ConnectionManager.consultaDt(sql);

        }
    }
}
