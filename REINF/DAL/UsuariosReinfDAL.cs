﻿using Model;
using System;
using System.Collections.Generic;
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
    }

}