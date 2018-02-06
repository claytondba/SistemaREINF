using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using MySql.Data.MySqlClient;
using System.Data;

namespace DAL
{
    public class UsuariosDAL: Framework.DAL<UsuariosModel>
    {
        public DataTable GetPermissionFromUser(Int64 user)
        {
            string sql = "select p.id, p.descricao, p.nome, u.usuario " +
                            "from sys_user_permission u " +
                            "join sys_permission p on p.id = u.permission and usuario = " + user + ";";

            return MySql.ConnectionManager.consultaDt(sql);

        }
        public bool CheckUserPerfil(int codUsuario, int codPerfil)
        {
            MySql.ConnectionManager.NoLog = true;
            string sql = string.Format("select count(*) from perfil_usuarios where usuarios = {0} and perfil = {1};", codUsuario, codPerfil);
            return Convert.ToBoolean(Convert.ToInt32(MySql.ConnectionManager.executeScalar(sql)));
        }
        public bool CheckUserPermission(int codUsuario, int codPermission)
        {
            MySql.ConnectionManager.NoLog = true;
            string sql = string.Format("select count(*) from sys_user_permission where usuario = {0} and permission = {1};", codUsuario, codPermission);
            return Convert.ToBoolean(Convert.ToInt32(MySql.ConnectionManager.executeScalar(sql)));
        }

        public bool InsertPerfil(int codPerfil, int codUsuario)
        {
            string sql = string.Format("insert into perfil_usuarios (perfil, usuarios) values({0},{1})", codPerfil, codUsuario);
            return MySql.ConnectionManager.update(sql);
        }
        public bool RemovePerfil(int codPerfil, int codUsuario)
        {
            string sql = string.Format("delete from perfil_usuarios where perfil ={0} and usuarios ={1}", codPerfil, codUsuario);
            return MySql.ConnectionManager.delete(sql);
        }
        public List<UsuariosModel> GetUsers()
        {
            string sql = "select codigo, coalesce(nome,''), ativada, cast(validade as date), coalesce(classe,''), coalesce(inclusao,0), coalesce(email,''), ver_custo, ver_preco, coalesce(vend,0), " +
                                "coalesce(login,''), coalesce(usu,''), coalesce(tipo,''), coalesce(cracha,''), coalesce(op,''), `super`, setup, insp, coalesce(depto,'') from usuarios order by nome;";

            DataTable dt = MySql.ConnectionManager.consultaDt(sql);
            DataTableReader dr = new DataTableReader(dt);
            UsuariosModel usuario = null;
            List<UsuariosModel> lista = new List<UsuariosModel>();

            while (dr.Read())
            {
                try
                {
                    usuario = new UsuariosModel();
                    usuario.codigo = Convert.ToUInt64(dr[0]);
                    usuario.nome = dr.GetString(1);
                    usuario.ativada = dr.GetInt32(2);
                    if (dr[3].GetType().ToString() != "System.DBNull")
                    usuario.validade = dr.GetDateTime(3);
                    usuario.classe = dr.GetString(4);
                    usuario.inclusao = Convert.ToInt32(dr.GetInt64(5));
                    usuario.email = dr.GetString(6);
                    usuario.ver_custo = dr.GetInt32(7);
                    usuario.ver_preco = dr.GetInt32(8);
                    usuario.vend = dr.GetInt64(9);
                    usuario.login = dr.GetString(10);
                    usuario.usu = dr.GetString(11);
                    usuario.tipo = dr.GetString(12);
                    usuario.cracha = dr.GetString(13);
                    usuario.op = dr.GetString(14);
                    usuario.super = Convert.ToUInt32(dr[15]);
                    usuario.setup = Convert.ToUInt32(dr[16]);
                    usuario.insp = Convert.ToUInt32(dr[17]);
                    usuario.depto = dr.GetString(18);


                    lista.Add(usuario);
                }
                catch (Exception ex)
                {
                   
                }
               
            }

            return lista;
        }
        public List<LockUpModel> LoockUp(string parteNome)
        {
            string sql = "SELECT coalesce(nome,'') as nome, codigo FROM usuarios";
            sql += " where nome LIKE '%" + parteNome.ToUpper() + "%'";
            sql += " ORDER BY nome";

            DataTable dt = MySql.ConnectionManager.consultaDt(sql);
            DataTableReader dr = new DataTableReader(dt);
            List<LockUpModel> list = new List<LockUpModel>();
            LockUpModel loc = new LockUpModel();

            while (dr.Read())
            {
                loc = new LockUpModel();
                loc.Descricao = dr.GetString(0);
                loc.Codigo = dr[1].ToString();

                list.Add(loc);
            }

            return list;

        }
        /// <summary>
        /// Retorna o código do Vendedor
        /// </summary>
        /// <param name="parteNome"></param>
        /// <returns></returns>
        public List<LockUpModel> LoockUpVend(string parteNome)
        {
            string sql = "SELECT coalesce(nome,'') as nome, vend as codigo FROM usuarios";
            sql += " where nome LIKE '%" + parteNome.ToUpper() + "%'";
            sql += " ORDER BY nome";

            DataTable dt = MySql.ConnectionManager.consultaDt(sql);
            DataTableReader dr = new DataTableReader(dt);
            List<LockUpModel> list = new List<LockUpModel>();
            LockUpModel loc = new LockUpModel();

            while (dr.Read())
            {
                loc = new LockUpModel();
                loc.Descricao = dr.GetString(0);
                loc.Codigo = dr[1].ToString();

                list.Add(loc);
            }

            return list;

        }
        public string GetName(Int32 codUser)
        {
            string sql = "select nome from usuarios where codigo =" + codUser;
            return MySql.ConnectionManager.executeScalar(sql).ToString();
        }
        public bool TrocaSenha(int codUser, string antiga, string nova)
        {
            string sql = "UPDATE usuarios SET senha2 = senha1, senha1 = senha, senha =md5(@senha), troca_senha = '" + DateTime.Now.AddDays(7).ToString("yyyy-MM-dd") + "'" +
                            "WHERE codigo =@codigo AND senha =md5(@antiga)";

            List<MySqlParameter> lista = new List<MySqlParameter>();

            lista.Add(new MySqlParameter("codigo", codUser));
            lista.Add(new MySqlParameter("senha", nova));
            lista.Add(new MySqlParameter("antiga", antiga));

            return MySql.ConnectionManager.update(sql, lista);

        }
        public bool TrocaSenhaQuick(int codUser, string nova)
        {
            string sql = "UPDATE usuarios SET senha2 = senha1, senha1 = senha, senha =md5(@senha), troca_senha = '" + DateTime.Now.AddDays(7).ToString("yyyy-MM-dd") + "'" +
                            "WHERE codigo =@codigo";

            List<MySqlParameter> lista = new List<MySqlParameter>();

            lista.Add(new MySqlParameter("codigo", codUser));
            lista.Add(new MySqlParameter("senha", nova));

            return MySql.ConnectionManager.update(sql, lista);

        }
        public UsuariosModel GetUser(string login, string senha)
        {
            string sql = "SELECT codigo, nome, senha, coalesce(senha1,''), coalesce(senha2,''), ativada, validade, coalesce(classe,''), inclusao, alteracao, impressao, exclusao, recupera, consulta, troca_senha, email, ver_custo, ver_preco, coalesce(vend,0), coalesce(formularios,''), coalesce(usu,''), coalesce(tipo,'RE'), coalesce(depto,''), ver_especial, form_linhas, ver_compra_user, login " +
                            "FROM usuarios " +
                            "WHERE login ='" + login.Trim().Replace("'", "") + "' AND senha = md5('" + senha.Trim().Replace("'", "") + "')";

            DataTable dt = MySql.ConnectionManager.consultaDt(sql);
            DataTableReader dr = new DataTableReader(dt);
            UsuariosModel usuario = null;

            while (dr.Read())
            {
                usuario = new UsuariosModel();
                usuario.codigo = Convert.ToUInt64(dr[0]);
                usuario.nome = dr.GetString(1);
                usuario.senha = dr.GetString(2);
                usuario.senha1 = dr.GetString(3);
                usuario.senha2 = dr.GetString(4);
                usuario.ativada = dr.GetInt32(5);
                usuario.validade = dr.GetDateTime(6);
                usuario.classe = dr.GetString(7);
                usuario.inclusao = dr.GetInt32(8);
                if (dr[9].GetType().ToString() != "System.DBNull")
                    usuario.alteracao = dr.GetInt32(9);
                usuario.impressao = dr.GetInt32(10);
                usuario.exclusao = dr.GetInt32(11);
                usuario.recupera = dr.GetInt32(12);
                usuario.consulta = dr.GetInt32(13);
                usuario.troca_senha = dr.GetDateTime(14);
                usuario.email = dr.GetString(15);
                usuario.ver_custo = dr.GetInt32(16);
                usuario.ver_preco = dr.GetInt32(17);
                usuario.vend = dr.GetInt64(18);
                usuario.formularios = dr.GetString(19);
                usuario.usu = dr.GetString(20);
                usuario.tipo = dr.GetString(21);
                usuario.depto = dr.GetString(22);
                usuario.ver_especial = dr.GetBoolean(23);
                usuario.form_linhas = Convert.ToInt32(dr[24].ToString());
                if (dr[25].GetType().ToString() != "System.DBNull")
                    usuario.ver_compra_user = dr.GetString(25);
                usuario.login = dr.GetString(26);
            }

            if (usuario == null)
                return new UsuariosModel();


            return usuario;

        }
    }
}
