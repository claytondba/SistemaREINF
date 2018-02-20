using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class OcorrenciaDAL : Framework.DAL<OcorrenciaModel>
    {
        public DataTable GetOcorrenciasByParceiro(int id_parceiro)
        {
            string sql = string.Format("select id, cnpj, razaosocial, codigo, descricao, recibo from ocorrencia where id_usuario in( " +
                            "select id from usuarios_reinf where cadastrante = {0});", id_parceiro);

            return Postgres.ConnectionManager.consultaDt(sql);
        }
    }
}
