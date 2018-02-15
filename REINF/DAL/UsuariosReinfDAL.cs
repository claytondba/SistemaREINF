using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UsuariosReinfDAL: Framework.DAL<UsuariosReinfModel>
    {

        public bool Logar(string login, string senha)
        {
            string sql = string.Format(
                "select count(*) from usuarios_reinf where login ='{0}' and senha =md5('{1}')", login, senha);

            return Convert.ToBoolean(Postgres.ConnectionManager.executeScalar(sql));
        }
        public UsuariosReinfModel LogarGetUser(string login, string senha)
        {
            this.CustomWhere = string.Format("login ='{0}' and senha =md5('{1}')", login, senha);
            return this.FrameworkGet(new UsuariosReinfModel()).First();
        }
        public bool AtualizaPrimeiroAcesso(UsuariosReinfModel usu, string senha)
        {
            string sql = string.Format(
                "update usuarios_reinf set senha =md5('{0}'), primeiro_acesso = false where id={1}",
                senha, usu.id);

            return Postgres.ConnectionManager.update(sql);

        }
        public bool AtualizaSenha(UsuariosReinfModel usu, string senha)
        {
            string sql = string.Format(
                "update usuarios_reinf set senha =md5('{0}') where id={1}",
                senha, usu.id);

            return Postgres.ConnectionManager.update(sql);

        }
    }

}
