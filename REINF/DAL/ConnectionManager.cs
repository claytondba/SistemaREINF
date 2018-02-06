using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Npgsql;
using System.Data;
using System.Xml;
using System.Data.OleDb;
using Model;

namespace DAL.Postgres
{
    /// <summary>
    /// Gerenciador de Conexões com o Banco de Dados
    /// </summary>
    public static class ConnectionManager
    {
        public static NpgsqlConnection cn { get; set; }
        private static bool Log { get; set; }

        public static int ErrosQt { get; set; }
        public static List<Exception> ErrorColection { get; set; }
        public static UsuariosModel Usuario { get; set; }
        public static SessionModel Sessao { get; set; }
        public static StringBuilder LogEvents { get; set; }
        public static bool NoLog { get; set; }

        public static void Inicializar()
        {
            LogEvents = new StringBuilder();
            ErrorColection = new List<Exception>();
        }

        private enum LogLevel
        {
            Max
        }
        /// <summary>
        /// ConectionString Atual
        /// </summary>
        public static string ConnectionString
        {
            get { return getConnectionString(); }
        }
        /// <summary>
        /// Conexao com o banco
        /// </summary>
        /// <returns></returns>
        private static bool conectar()
        {
            if (cn == null)
                throw new InvalidOperationException("Nehuma conexão com o BD está ativada.");
            //10.0.0.199     189.38.23.215
            //cn = new MySqlConnection("Server=10.0.0.199;Database=nova;Uid=root;Pwd=beleza;");
            try
            {
                if (cn.State != System.Data.ConnectionState.Open)
                    cn.Open();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao conectar com a base de Dados, " + ex.Message);
            }
        }
        private static bool desconectar()
        {
            if (cn.State != System.Data.ConnectionState.Closed)
            {
                cn.Close();
                return true;
            }
            return false;
        }
        /// <summary>
        /// Metodo Insert Generico
        /// </summary>
        /// <param name="p_sql">String com o comando SQL</param>
        /// <param name="p_listParams">Lista com os parametros</param>
        /// <returns></returns>
        public static bool insert(string p_sql, List<NpgsqlParameter> p_listParams)
        {
            if (conectar())
            {
                NpgsqlCommand cmd = new NpgsqlCommand(p_sql, cn);
                foreach (NpgsqlParameter param in p_listParams)
                {
                    cmd.Parameters.Add(param);
                }
                try
                {
                    int result = cmd.ExecuteNonQuery();
                    return result > 0 ? true : false;
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao executar comando no Banco de Dados! " + ex.Message);
                }
                finally
                {
                    desconectar();
                }
            }
            return false;

        }
        /// <summary>
        /// Metodo Insert Generico com Retorno de Chave Primária
        /// </summary>
        /// <param name="p_sql">String com o comando SQL</param>
        /// <param name="p_listParams">Lista com os parametros</param>
        /// <returns></returns>
        public static int InsertWitchKey(string p_sql, List<NpgsqlParameter> p_listParams)
        {
            if (conectar())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(p_sql, cn))
                {
                    foreach (NpgsqlParameter param in p_listParams)
                        cmd.Parameters.Add(param);
                    try
                    {
                        int result = cmd.ExecuteNonQuery();
                        //LogEvent(cmd.CommandText);
                        cmd.CommandText = "select LAST_INSERT_ID()";
                        //LogEvent(cmd.CommandText);
                        cmd.Parameters.Clear();
                        return 1;//Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Erro ao executar comando no Banco de Dados! " + ex.Message);
                    }
                    finally
                    {
                        desconectar();
                    }
                }
            }
            return 0;

        }
        /// <summary>
        /// Metodo delete Generico
        /// </summary>
        /// <param name="p_sql">String com o comando SQL</param>
        /// <param name="p_listParam">Lista com os parametros</param>
        /// <returns></returns>
        public static bool delete(string p_sql, List<NpgsqlParameter> p_listParam)
        {
            if (conectar())
            {
                NpgsqlCommand cmd = new NpgsqlCommand(p_sql, cn);
                int result = 0;
                foreach (NpgsqlParameter param in p_listParam)
                {
                    cmd.Parameters.Add(param);
                }
                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("O registro não pode ser excluido: " + ex.Message);
                }
                finally
                {
                    desconectar();
                }
                return Convert.ToBoolean(result);
            }
            return false;
        }
        public static bool update(string p_sql, List<NpgsqlParameter> p_listParam)
        {
            if (conectar())
            {
                NpgsqlCommand cmd = new NpgsqlCommand(p_sql, cn);
                int result = 0;
                foreach (NpgsqlParameter param in p_listParam)
                {
                    cmd.Parameters.Add(param);
                }
                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("O registro não pode ser atualizado: " + ex.Message);
                }
                finally
                {
                    desconectar();
                }
                return Convert.ToBoolean(result);

            }
            return false;
        }
        public static bool update(string p_sql)
        {
            if (conectar())
            {
                NpgsqlCommand cmd = new NpgsqlCommand(p_sql, cn);
                int result = 0;
                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("O registro não pode ser atualizado: " + ex.Message);
                }
                finally
                {
                    desconectar();
                }
                return Convert.ToBoolean(result);

            }
            return false;
        }
        /// <summary>
        /// Metodo que retorna uma consulta especifica
        /// </summary>
        /// <param name="p_sql">Comando SQL</param>
        /// <returns>DataTable com resultado</returns>
        public static DataTable consultaDt(string p_sql)
        {
            if (conectar())
            {
                NpgsqlCommand cmd = new NpgsqlCommand(p_sql, cn);
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                try
                {
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao executar comando no Banco de Dados! " + ex.Message);
                }
                desconectar();
                return dt;

            }
            return null;
        }
        /// <summary>
        /// Metodo que retorna uma consulta especifica
        /// </summary>
        /// <param name="p_sql">Comando SQL</param>
        /// <param name="p_params">Parametros</param>
        /// <returns>DataTable com resultado</returns>
        public static DataTable consultaDt(string p_sql, List<NpgsqlParameter> p_params)
        {
            if (conectar())
            {
                NpgsqlCommand cmd = new NpgsqlCommand(p_sql, cn);
                foreach (NpgsqlParameter param in p_params)
                {
                    cmd.Parameters.Add(param);
                }
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                try
                {
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao executar comando no Banco de Dados! " + ex.Message);
                }
                desconectar();
                return dt;

            }
            return null;
        }
        /// <summary>
        /// Metodo Obrigatorio para Login do Usuario no sistema
        /// </summary>
        /// <param name="p_sql">Comando SQL</param>
        /// <param name="list">Parametros</param>
        /// <returns></returns>
        public static bool logar(string p_sql, List<NpgsqlParameter> list)
        {
            if (conectar())
            {
                bool check = false;
                NpgsqlCommand cmd = new NpgsqlCommand(p_sql, cn);
                foreach (NpgsqlParameter param in list)
                {
                    cmd.Parameters.Add(param);
                }
                try
                {
                    check = Convert.ToBoolean(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao executar comando no Banco de Dados! " + ex.Message);
                }
                finally
                {
                    desconectar();
                }
                return check;

            }
            return false;
        }
        /// <summary>
        /// Obter ConnectionString a partir de um XML
        /// </summary>
        /// <returns>Connection String</returns>
        public static string getConnectionString()
        {
            try
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(@"initConfig.xml");

                XmlNodeList nodeList = xDoc.GetElementsByTagName("connection");
                string host = nodeList[0].ChildNodes[0].InnerText;
                string port = nodeList[0].ChildNodes[1].InnerText;
                string user = nodeList[0].ChildNodes[2].InnerText;
                string pass = nodeList[0].ChildNodes[3].InnerText;
                string database = nodeList[0].ChildNodes[4].InnerText;

                string conexao = "Server=" + host + ";Port=" + port + ";Database=" + database + ";User Id=" + user + ";Password=" + pass + ";Timeout=30";

                return conexao;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro: " + ex.Message);
            }

        }
        /// <summary>
        /// Metodo que testa a conexao com o banco, e retorna todos os BDs
        /// </summary>
        /// <param name="connectionString">String de Conexao</param>
        /// <returns>Listaa com nome dos Bds</returns>
        public static List<string> testConnection(string connectionString)
        {
            NpgsqlConnection cnt;
            try
            {
                cnt = new NpgsqlConnection(connectionString);
                NpgsqlCommand cmd = new NpgsqlCommand("select datname from pg_database where datistemplate = false", cnt);
                cnt.Open();
                List<string> lista = new List<string>();
                NpgsqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string database = dr.GetString(0);
                    lista.Add(database);
                }
                cnt.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao conectar no servidor.... " + ex.Message);
            }


        }
        /// <summary>
        /// Executa um comando e retorna apenas um Object
        /// </summary>
        /// <param name="p_sql">Comando SQL</param>
        /// <returns></returns>
        public static object executeScalar(string p_sql)
        {
            if (conectar())
            {
                NpgsqlCommand cmd = new NpgsqlCommand(p_sql, cn);
                object obj = cmd.ExecuteScalar();
                desconectar();
                return obj;
            }
            return null;
        }
        /// <summary>
        /// Executa um comando e retorna apenas um Object
        /// </summary>
        /// <param name="p_sql">Comando SQL</param>
        /// <param name="p_params">Parametro</param>
        /// <returns></returns>
        public static object executeScalar(string p_sql, List<NpgsqlParameter> p_params)
        {
            if (conectar())
            {
                NpgsqlCommand cmd = new NpgsqlCommand(p_sql, cn);
                foreach (NpgsqlParameter param in p_params)
                {
                    cmd.Parameters.Add(param);
                }

                object obj = cmd.ExecuteScalar();
                desconectar();
                return obj;
            }
            return null;
        }

    }
}

namespace DAL.MySql
{
    /// <summary>
    /// Classe de Gerenciamento de Conexões com o Banco de Dados.
    /// </summary>
    public static class ConnectionManager
    {
        private static bool Log { get; set; }

        public static int ErrosQt { get; set; }
        public static List<Exception> ErrorColection { get; set; }
        public static UsuariosModel Usuario { get; set; }
        public static SessionModel Sessao { get; set; }
        public static StringBuilder LogEvents { get; set; }
        public static bool NoLog { get; set; }

        private enum LogLevel
        {
            High,
            Medium,
            Low
        }
        public static string[] GetObjectFromTable(string tableName)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("desc {0}", tableName);
            DataTable dt = MySql.ConnectionManager.consultaDt(sb.ToString());
            DataTableReader dr = new DataTableReader(dt);
            string className = "";

            string[] splNames = tableName.Split('-');

            if (splNames.Count() > 1)
            {
                foreach (string nn in splNames)
                {
                    className += nn.ToUpper().Substring(0, 1) + nn.Substring(1, nn.Length - 1);
                }
                className += "Model";
            }
            else
                className = tableName.ToUpper().Substring(0, 1) + tableName.Substring(1, tableName.Length - 1) + "Model";


            FormularioModel formulario = new FormularioModel();

            formulario.nome = "frm" + className.Replace("Model", "");
            formulario.descricao = "Formulário Tabela " + tableName;
            formulario.reference_model = "Model." + className;
            formulario.allowdelete = true;
            formulario.allowinsert = true;
            formulario.allowupdate = true;
            formulario.reference_persist = "BLL." + className.Replace("Model", "BLL");

            //Gerando Classe BLL
            StringBuilder sbBll = new StringBuilder();
            sbBll.AppendLine("using System;");
            sbBll.AppendLine("using System.Collections.Generic;");
            sbBll.AppendLine("using System.Linq;");
            sbBll.AppendLine("using System.Text;");
            sbBll.AppendLine("");
            sbBll.AppendLine("namespace BLL");
            sbBll.AppendLine("{");
            sbBll.AppendLine("    public class " + className.Replace("Model", "BLL") + ":Framework.BLL<" + className + ">, Framework.IBLL");
            sbBll.AppendLine("    {");
            sbBll.AppendLine("");
            sbBll.AppendLine("    }");
            sbBll.AppendLine("}");


            //Gerando Classe DAL
            StringBuilder sbDal = new StringBuilder();
            sbDal.AppendLine("using System;");
            sbDal.AppendLine("using System.Collections.Generic;");
            sbDal.AppendLine("using System.Linq;");
            sbDal.AppendLine("using System.Text;");
            sbDal.AppendLine("");
            sbDal.AppendLine("namespace DAL");
            sbDal.AppendLine("{");
            sbDal.AppendLine("    public class " + className.Replace("Model", "BLL") + ":Framework.DAL<" + className + ">");
            sbDal.AppendLine("    {");
            sbDal.AppendLine("");
            sbDal.AppendLine("    }");
            sbDal.AppendLine("}");



            FormularioDAL fDal = new FormularioDAL();
            fDal.FrameworkInsert(formulario);

            sb = new StringBuilder();
            sb.AppendLine("using Model.Framework;");
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using System.Text;");
            sb.AppendLine("");
            sb.AppendLine("namespace Model");
            sb.AppendLine("{");
            sb.AppendLine("    public class " + className + ":Framework.Model");
            sb.AppendLine("    {");
            sb.AppendLine("         public " + className + "()");
            sb.AppendLine("         {");
            sb.AppendLine("             //Nome da Tabela");
            sb.AppendLine("             Entity = \"" + tableName + "\";");
            sb.AppendLine("             //Indica se a tabela tem outro indice de controle");
            sb.AppendLine("             Primitive = true;");
            sb.AppendLine("             //ChangePrimitiveController - Troca a coluna de controle");
            sb.AppendLine("             //NamePrimitiveController - Nome da coluna do indice de controle");
            sb.AppendLine("         }");

            int contador = 10;

            while (dr.Read())
            {
                string tt = dr["Type"].ToString();
                string campo = dr["Field"].ToString();
                string tipoModel = "";

                FieldsModel fil = new FieldsModel();

                if (campo == "sr_recno")
                {
                    sb.AppendLine("        [PrimaryKey(isKey=true)]");
                }
                if (tt.StartsWith("char"))
                {
                    tipoModel = "string";
                    fil.type = "text";
                    fil.size = Convert.ToInt32(tt.Replace("char(", "").Replace(")", ""));

                    if (fil.size <= 2)
                        fil.size += 2;
                }
                else if (tt.StartsWith("varchar"))
                {
                    tipoModel = "string";
                    fil.type = "text";
                    fil.size = Convert.ToInt32(tt.Replace("varchar(", "").Replace(")", ""));

                    if (fil.size <= 2)
                        fil.size += 2;
                }
                else if (tt.StartsWith("double"))
                {
                    tipoModel = "double";
                    fil.type = "decimal";
                    fil.size = 10;
                }
                else if (tt.StartsWith("mediumblob"))
                {
                    tipoModel = "byte[]";
                    fil.type = "memo";
                    fil.size = 40;
                }
                else if (tt.StartsWith("date"))
                {
                    tipoModel = "DateTime";
                    fil.type = "datatime";
                    fil.size = 20;
                }
                else if (tt.StartsWith("tinyint(1)"))
                {
                    tipoModel = "bool";
                    fil.type = "bool";
                    fil.size = 10;
                }
                else if (tt.StartsWith("tinyint"))
                {
                    tipoModel = "double";
                    fil.type = "decimal";
                    fil.size = 10;
                }
                else if (tt.StartsWith("int(10)"))
                {
                    tipoModel = "Int64";
                    fil.size = 10;

                    if (dr["Key"].ToString() == "PRI")
                    {
                        sb.AppendLine("        [PrimaryKey(isKey=true)]");
                        fil.type = "recno";
                        fil.system = true;

                    }
                }
                else if (tt.StartsWith("int"))
                {
                    tipoModel = "double";
                    fil.type = "decimal";
                    fil.size = 10;
                }

                else if (tt.StartsWith("bigint"))
                {
                    tipoModel = "Int64";
                    fil.size = 10;

                    if (campo == "sr_recno")
                    {
                        fil.type = "recno";
                        fil.system = true;
                    }
                }
                else
                {
                    fil.type = tt;
                    fil.size = 10;
                }

                sb.AppendLine("        public " + tipoModel + " " + campo + " { get; set; }");

                fil.formulario = fDal.LastInsertID;
                fil.name = campo;
                fil.descricao = campo.ToUpper().Substring(0, 1) + campo.Substring(1, campo.Length - 1);
                fil.help = campo.ToUpper().Substring(0, 1) + campo.Substring(1, campo.Length - 1);
                fil.model_reference = campo;
                fil.allownull = true;
                fil.browse = false;

                contador = contador + 10;

                fil.order_ = contador;
                fil.pad_left = false;

                new FieldDAL().FrameworkInsert(fil);
            }

            sb.AppendLine("    }");
            sb.AppendLine("}");

            string[] result = { sb.ToString(), sbBll.ToString(), sbDal.ToString() };

            return result;

        }
        public static void LogEvent(string cmdText)
        {
            //if (NoLog)
            //{
            //    NoLog = false;
            //    return;
            //}

            //if (conectar())
            //{
            //    string command =
            //        "insert into sys_log (usuario, data_evento, origem, command) " +
            //        "values (@usuario, @data_evento, @origem, @command)";

            //    using (MySqlCommand cmd = new MySqlCommand(command, cn))
            //    {                    
            //        try
            //        {
            //            cmd.Parameters.Add("usuario", Sessao.usuario);
            //            cmd.Parameters.Add("data_evento", DateTime.Now);
            //            cmd.Parameters.Add("origem", Sessao.origem);
            //            cmd.Parameters.Add("command", cmdText);
            //            LogEvents.AppendLine(cmdText);
            //            int result = cmd.ExecuteNonQuery();
            //        }
            //        catch (Exception ex)
            //        {
            //            ErrosQt++;
            //            Exception e = new Exception(DateTime.Now.ToShortTimeString() + " - Erro logando evento do usuário...." +
            //                "\nSql:" + command, ex);

            //            if (ErrorColection == null)
            //                ErrorColection = new List<Exception>();
            //            e.Source = command;
            //            ErrorColection.Add(e);
            //        }
            //        finally
            //        {
            //            desconectar();
            //        }
            //    }
            //}
        }
        public static MySqlConnection cn { get; set; }

        public static void Inicializar()
        {
            LogEvents = new StringBuilder();
            ErrorColection = new List<Exception>();
        }
        /// <summary>
        /// ConectionString Atual
        /// </summary>
        public static string ConnectionString
        {
            get { return getConnectionString(); }
        }
        /// <summary>
        /// Conexao com o banco
        /// </summary>
        /// <returns></returns>
        private static bool conectar()
        {
            if (cn == null)
                throw new InvalidOperationException("Nehuma conexão com o BD está ativada.");
            //10.0.0.199     189.38.23.215
            //cn = new MySqlConnection("Server=10.0.0.199;Database=nova;Uid=root;Pwd=beleza;");
            try
            {
                if (cn.State != System.Data.ConnectionState.Open)
                    cn.Open();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao conectar com a base de Dados, " + ex.Message);
            }
        }
        private static bool desconectar()
        {
            if (cn.State != System.Data.ConnectionState.Closed)
            {
                cn.Close();
                return true;
            }
            return false;
        }
        /// <summary>
        /// Metodo Insert Generico com Retorno de Chave Primária
        /// </summary>
        /// <param name="p_sql">String com o comando SQL</param>
        /// <param name="p_listParams">Lista com os parametros</param>
        /// <returns></returns>
        public static int InsertWitchKey(string p_sql, List<MySqlParameter> p_listParams)
        {
            if (conectar())
            {
                using (MySqlCommand cmd = new MySqlCommand(p_sql, cn))
                {
                    foreach (MySqlParameter param in p_listParams)
                        cmd.Parameters.Add(param);
                    try
                    {
                        int result = cmd.ExecuteNonQuery();
                        LogEvent(cmd.CommandText);
                        cmd.CommandText = "select LAST_INSERT_ID()";
                        LogEvent(cmd.CommandText);
                        cmd.Parameters.Clear();
                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Erro ao executar comando no Banco de Dados! " + ex.Message);
                    }
                    finally
                    {
                        desconectar();
                    }
                }
            }
            return 0;

        }
        /// <summary>
        /// Metodo Insert Generico
        /// </summary>
        /// <param name="p_sql">String com o comando SQL</param>
        /// <param name="p_listParams">Lista com os parametros</param>
        /// <returns></returns>
        public static bool insert(string p_sql, List<MySqlParameter> p_listParams)
        {
            if (conectar())
            {
                using (MySqlCommand cmd = new MySqlCommand(p_sql, cn))
                {
                    foreach (MySqlParameter param in p_listParams)
                        cmd.Parameters.Add(param);
                    try
                    {
                        int result = cmd.ExecuteNonQuery();
                        LogEvent(cmd.CommandText);
                        return result > 0 ? true : false;
                    }
                    catch (Exception ex)
                    {
                        ErrosQt++;
                        Exception e = new Exception(DateTime.Now.ToShortTimeString() + " - Erro executando método insert(string p_sql)" +
                            "\nSql:" + p_sql, ex);

                        if (ErrorColection == null)
                            ErrorColection = new List<Exception>();
                        e.Source = p_sql;
                        ErrorColection.Add(e);
                    }
                    finally
                    {
                        desconectar();
                    }
                }
            }
            return false;

        }
        /// <summary>
        /// Metodo delete Generico
        /// </summary>
        /// <param name="p_sql">String com o comando SQL</param>
        /// <param name="p_listParam">Lista com os parametros</param>
        /// <returns></returns>
        public static bool delete(string p_sql, List<MySqlParameter> p_listParam)
        {
            if (conectar())
            {
                MySqlCommand cmd = new MySqlCommand(p_sql, cn);
                int result = 0;
                foreach (MySqlParameter param in p_listParam)
                {
                    cmd.Parameters.Add(param);
                }
                try
                {
                    result = cmd.ExecuteNonQuery();
                    LogEvent(cmd.CommandText);
                }
                catch (Exception ex)
                {
                    throw new Exception("O registro não pode ser excluido: " + ex.Message);
                }
                finally
                {
                    desconectar();
                }
                return Convert.ToBoolean(result);
            }
            return false;
        }
        public static bool delete(string p_sql)
        {
            if (conectar())
            {
                MySqlCommand cmd = new MySqlCommand(p_sql, cn);
                int result = 0;
                try
                {
                    result = cmd.ExecuteNonQuery();
                    LogEvent(cmd.CommandText);
                }
                catch (Exception ex)
                {
                    throw new Exception("O registro não pode ser excluido: " + ex.Message);
                }
                finally
                {
                    desconectar();
                }
                return Convert.ToBoolean(result);
            }
            return false;
        }
        public static bool update(string p_sql, List<MySqlParameter> p_listParam)
        {
            if (conectar())
            {
                MySqlCommand cmd = new MySqlCommand(p_sql, cn);
                int result = 0;
                foreach (MySqlParameter param in p_listParam)
                {
                    cmd.Parameters.Add(param);
                }
                try
                {
                    result = cmd.ExecuteNonQuery();
                    LogEvent(cmd.CommandText);
                }
                catch (Exception ex)
                {
                    ErrosQt++;
                    Exception e = new Exception(DateTime.Now.ToShortTimeString() + " - Erro executando método update(string p_sql)" +
                        "\nSql:" + p_sql, ex);

                    if (ErrorColection == null)
                        ErrorColection = new List<Exception>();
                    e.Source = p_sql;
                    ErrorColection.Add(e);
                }
                finally
                {
                    desconectar();
                }
                return Convert.ToBoolean(result);

            }
            return false;
        }
        public static bool update(string p_sql)
        {
            if (conectar())
            {
                MySqlCommand cmd = new MySqlCommand(p_sql, cn);
                int result = 0;
                try
                {
                    result = cmd.ExecuteNonQuery();
                    LogEvent(cmd.CommandText);
                }
                catch (Exception ex)
                {
                    ErrosQt++;
                    Exception e = new Exception(DateTime.Now.ToShortTimeString() + " - Erro executando método update(string p_sql)" +
                        "\nSql:" + p_sql, ex);

                    if (ErrorColection == null)
                        ErrorColection = new List<Exception>();
                    e.Source = p_sql;
                    ErrorColection.Add(e);
                }
                finally
                {
                    desconectar();
                }
                return Convert.ToBoolean(result);

            }
            return false;
        }

        /// <summary>
        /// Executa comandos dentro de Transações no BD
        /// </summary>
        /// <param name="p_sql"></param>
        /// <param name="p_listParam"></param>
        /// <returns></returns>
        public static bool Transaction(string p_sql, List<MySqlParameter> p_listParam)
        {
            if (conectar())
            {
                MySqlCommand cmd = new MySqlCommand(p_sql, cn);
                int result = 0;
                MySqlTransaction trans = cn.BeginTransaction();
                cmd.Transaction = trans;
                foreach (MySqlParameter param in p_listParam)
                {
                    cmd.Parameters.Add(param);
                }
                try
                {
                    result = cmd.ExecuteNonQuery();
                    LogEvent(cmd.CommandText);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception("O registro não pode ser atualizado (RollBack Efetuado):\n " + ex.Message);
                }
                finally
                {
                    desconectar();
                }
                return Convert.ToBoolean(result);

            }
            return false;
        }
        public static bool Transaction(string p_sql)
        {
            if (conectar())
            {
                MySqlCommand cmd = new MySqlCommand(p_sql, cn);
                MySqlTransaction trans = cn.BeginTransaction();
                cmd.Transaction = trans;
                int result = 0;
                try
                {
                    result = cmd.ExecuteNonQuery();
                    LogEvent(cmd.CommandText);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception("O registro não pode ser atualizado: " + ex.Message);
                }
                finally
                {
                    desconectar();
                }
                return Convert.ToBoolean(result);

            }
            return false;
        }


        /// <summary>
        /// Metodo que retorna uma consulta especifica
        /// </summary>
        /// <param name="p_sql">Comando SQL</param>
        /// <returns>DataTable com resultado</returns>
        public static DataTable consultaDt(string p_sql)
        {
            if (conectar())
            {
                MySqlCommand cmd = new MySqlCommand(p_sql, cn);
                cmd.CommandTimeout = 300;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                try
                {
                    da.Fill(dt);
                    LogEvent(cmd.CommandText);
                }
                catch (Exception ex)
                {
                    ErrosQt++;
                    Exception e = new Exception(DateTime.Now.ToShortTimeString() + " - Erro executando método consultaDt(string p_sql)" +
                        "\nSql:" + p_sql, ex);

                    if (ErrorColection == null)
                        ErrorColection = new List<Exception>();
                    e.Source = p_sql;
                    ErrorColection.Add(e);

                }
                finally
                {
                    desconectar();
                }

                return dt;

            }
            return null;
        }
        /// <summary>
        /// Metodo que retorna uma consulta especifica
        /// </summary>
        /// <param name="p_sql">Comando SQL</param>
        /// <param name="p_params">Parametros</param>
        /// <returns>DataTable com resultado</returns>

        public static DataTable consultaDt(string p_sql, List<MySqlParameter> p_params)
        {
            if (conectar())
            {
                using (MySqlCommand cmd = new MySqlCommand(p_sql, cn))
                {
                    foreach (MySqlParameter param in p_params)
                        cmd.Parameters.Add(param);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    try
                    {
                        da.Fill(dt);
                        LogEvent(cmd.CommandText);
                    }
                    catch (Exception ex)
                    {
                        ErrosQt++;
                        Exception e = new Exception(DateTime.Now.ToShortTimeString() + " - Erro executando método consultaDt(string p_sql)" +
                            "\nSql:" + p_sql, ex);

                        if (ErrorColection == null)
                            ErrorColection = new List<Exception>();
                        e.Source = p_sql;
                        ErrorColection.Add(e);
                        throw new Exception("Erro recuperando dados!! " + ex.Message);
                    }
                    finally
                    {
                        desconectar();
                    }

                    return dt;
                }
            }
            return null;
        }
        /// <summary>
        /// Metodo Obrigatorio para Login do Usuario no sistema
        /// </summary>
        /// <param name="p_sql">Comando SQL</param>
        /// <param name="list">Parametros</param>
        /// <returns></returns>
        public static bool logar(string p_sql, List<MySqlParameter> list)
        {
            if (conectar())
            {
                bool check = false;
                MySqlCommand cmd = new MySqlCommand(p_sql, cn);
                foreach (MySqlParameter param in list)
                {
                    cmd.Parameters.Add(param);
                }
                try
                {
                    check = Convert.ToBoolean(cmd.ExecuteScalar());
                    LogEvent(cmd.CommandText);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao executar comando no Banco de Dados! " + ex.Message);
                }
                finally
                {
                    desconectar();
                }
                return check;

            }
            return false;
        }
        /// <summary>
        /// Obter ConnectionString a partir de um XML
        /// </summary>
        /// <returns>Connection String</returns>
        public static string getConnectionString()
        {
            try
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(@"initConfig.xml");

                XmlNodeList nodeList = xDoc.GetElementsByTagName("connection");
                string host = nodeList[0].ChildNodes[0].InnerText;
                string port = nodeList[0].ChildNodes[1].InnerText;
                string user = nodeList[0].ChildNodes[2].InnerText;
                string pass = nodeList[0].ChildNodes[3].InnerText;
                string database = nodeList[0].ChildNodes[4].InnerText;

                string conexao = "Server=" + host + ";Port=" + port + ";Database=" + database + ";User Id=" + user + ";Password=" + pass + "; convert zero datetime=True";

                return conexao;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro: " + ex.Message);
            }

        }
        /// <summary>
        /// Metodo que testa a conexao com o banco, e retorna todos os BDs
        /// </summary>
        /// <param name="connectionString">String de Conexao</param>
        /// <returns>Lista com nome dos Bds</returns>
        public static List<string> testConnection(string connectionString)
        {
            MySqlConnection cnt;
            try
            {
                cnt = new MySqlConnection(connectionString);
                MySqlCommand cmd = new MySqlCommand("select datname from pg_database where datistemplate = false", cnt);
                cnt.Open();
                List<string> lista = new List<string>();
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string database = dr.GetString(0);
                    lista.Add(database);
                }
                cnt.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao conectar no servidor.... " + ex.Message);
            }


        }
        /// <summary>
        /// Executa um comando e retorna apenas um Object
        /// </summary>
        /// <param name="p_sql">Comando SQL</param>
        /// <returns></returns>
        public static object executeScalar(string p_sql)
        {
            if (conectar())
            {
                MySqlCommand cmd = new MySqlCommand(p_sql, cn);
                object obj = new object();
                try
                {
                    int cmdTempo = cmd.CommandTimeout;
                    cmd.CommandTimeout = 10000;
                    obj = cmd.ExecuteScalar();
                    //LogEvent(cmd.CommandText);
                    cmd.Dispose();
                }
                catch (Exception)
                {

                    throw;
                }

                desconectar();
                return obj;
            }
            return null;
        }
        /// <summary>
        /// Executa um comando e retorna apenas um Object
        /// </summary>
        /// <param name="p_sql">Comando SQL</param>
        /// <param name="p_params">Parametro</param>
        /// <returns></returns>
        public static object executeScalar(string p_sql, List<MySqlParameter> p_params)
        {
            if (conectar())
            {
                object obj = null;
                try
                {
                    MySqlCommand cmd = new MySqlCommand(p_sql, cn);
                    foreach (MySqlParameter param in p_params)
                    {
                        cmd.Parameters.Add(param);
                    }

                    obj = cmd.ExecuteScalar();
                    LogEvent(cmd.CommandText);

                }
                catch (Exception)
                {

                }
                finally
                {
                    desconectar();
                }
                return obj;

            }
            return null;
        }

    }
}

namespace DAL.Access
{
    public static class ConnectionManager
    {
        public static OleDbConnection cn { get; set; }

        private static bool conectar()
        {
            //if (cn == null)
            //    throw new InvalidOperationException("Nehuma conexão com o BD está ativada.");
            //10.0.0.199     189.38.23.215
            cn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=mecanicas.mdb");
            try
            {
                if (cn.State != System.Data.ConnectionState.Open)
                    cn.Open();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao conectar com a base de Dados, " + ex.Message);
            }
        }
        private static bool desconectar()
        {
            if (cn.State != System.Data.ConnectionState.Closed)
            {
                cn.Close();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Metodo Insert Generico
        /// </summary>
        /// <param name="p_sql">String com o comando SQL</param>
        /// <param name="p_listParams">Lista com os parametros</param>
        /// <returns></returns>
        public static bool insert(string p_sql, List<OleDbParameter> p_listParams)
        {
            if (conectar())
            {
                using (OleDbCommand cmd = new OleDbCommand(p_sql, cn))
                {
                    foreach (OleDbParameter param in p_listParams)
                        cmd.Parameters.Add(param);
                    try
                    {
                        int result = cmd.ExecuteNonQuery();
                        return result > 0 ? true : false;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Erro ao executar comando no Banco de Dados! " + ex.Message);
                    }
                    finally
                    {
                        desconectar();
                    }
                }
            }
            return false;

        }
        public static DataTable consultaDt(string p_sql)
        {
            if (conectar())
            {
                OleDbCommand cmd = new OleDbCommand(p_sql, cn);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                DataTable dt = new DataTable();
                try
                {
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao executar comando no Banco de Dados! " + ex.Message);
                }
                finally
                {
                    desconectar();
                }

                return dt;

            }
            return null;
        }
    }
}
