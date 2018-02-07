using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class UsuariosReinfModel : Framework.Model
    {
        public UsuariosReinfModel()
        {
            Entity = "usuarios_reinf";
        }

        public int id { get; set; }
        public string nome { get; set; }
        public string senha { get; set; }
        public DateTime data_cadastro { get; set; }
        public bool ativo { get; set; }
        public string razao_social { get; set; }
        public string email { get; set; }
        public string telefone { get; set; }
        public bool primeiro_acesso { get; set; }

    }
}
