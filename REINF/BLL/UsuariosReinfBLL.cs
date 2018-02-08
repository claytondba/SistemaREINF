using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class UsuariosReinfBLL : Framework.BLL<UsuariosReinfModel>, Framework.IBLL
    {
        public bool Logar(string login, string senha)
        {
            return new UsuariosReinfDAL().Logar(login, senha);
        }
        public UsuariosReinfModel LogarGetUser(string login, string senha)
        {
            return new UsuariosReinfDAL().LogarGetUser(login, senha);
        }
        public bool AtualizaPrimeiroAcesso(UsuariosReinfModel usu, string senha)
        {
            return new UsuariosReinfDAL().AtualizaPrimeiroAcesso(usu, senha);
        }
    }
}
