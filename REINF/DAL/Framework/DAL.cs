using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Framework;
using System.Reflection;
using My = Npgsql;
using Model;
using System.Data;

namespace DAL.Framework
{
    public class DAL<T> where T : Model.Framework.Model
    {
        #region Fields
        private string _entityAlias;
        private const string _sr_deleted = " sr_deleted !='T'";
        private string _entityName;
        private List<My.NpgsqlParameter> _stateParams;
        private List<string> _fields;
        private List<string> _joinFields;
        private List<string> _values;
        private List<string> _functionFields;
        private List<string> _cleanFunctionFields;
        private string _pageSize;
        private string _where;
        private string _primaryKey;
        private string _group;
        private string _order;
        private int _count;
        private string _sql;
        private string _custonWhere;
        private string _custonOrderBy;
        private string _codLockup;
        private string _codLockupValue;
        private string _descLockup;
        private int _lastInsertID;
        private string _join;
        private string _aliasJoin;
        private string _joinField;
        private string _joinCondition;
        private string _fieldToJoin;
        private Int64 _recnoValue;


        //Public
        public int LastInsertID { get { return _lastInsertID; } }

        #endregion

        #region Properts
        public bool NoJoinsFunctions { get; set; }
        /// <summary>
        /// Caso setada como Verdadeira, retorna apenas o primeiro 
        /// resultado da consulta.
        /// </summary>
        public bool OnlyFirstResult { get; set; }
        /// <summary>
        /// Envia uma instrução sql como parametro
        /// </summary>
        public String CustomWhere { set { _custonWhere = value; } }
        /// <summary>
        /// Ordena os registros de forma customizada
        /// </summary>
        public String CustomOrder { set { _custonOrderBy = value; } }
        #endregion

        /// <summary>
        /// Executa StoreProcedure existente no BD
        /// </summary>
        /// <param name="proc_name"></param>
        /// <returns></returns>
        public bool FrameworkExecuteProcedure(string proc_name)
        {
            if (proc_name.StartsWith("update"))
                Postgres.ConnectionManager.executeScalar(proc_name);
            else
                Postgres.ConnectionManager.executeScalar("select " + proc_name);
            return true;
        }
        /// <summary>
        /// Método de persistência genérico, ultiliza reflection para leitura das propriedades
        /// </summary>
        /// <param name="classModule">Pode ser qualquer classe de modelo herdada de 
        /// Model.Framework.Model 
        /// </param>
        /// <returns>ID do registro inserido</returns>
        public Int64 FInsert(Model.Framework.Model classModule)
        {
            FrameworkInsert(classModule);
            return LastInsertID;
        }
        /// <summary>
        /// Método de persistência genérico, ultiliza reflection para leitura das propriedades
        /// </summary>
        /// <param name="classModule">Pode ser qualquer classe de modelo herdada de 
        /// Model.Framework.Model 
        /// </param>
        /// <returns>Resultado da Operação</returns>
        public bool FrameworkInsert(Model.Framework.Model classModule)
        {
            try
            {
                _fields = new List<string>();
                _values = new List<string>();
                _stateParams = new List<My.NpgsqlParameter>();
                var tipo = classModule.GetType();
                //Retornando os fields da classe
                var fields = tipo.GetFields();


                foreach (PropertyInfo propriedade in classModule.GetType().GetProperties())
                {
                    if (propriedade.GetCustomAttributes(typeof(NotField), false).Count() == 0)
                    {
                        if (propriedade.GetCustomAttributes(typeof(EntityConfiguration), false).Count() > 0)
                        {
                            _entityName = propriedade.GetValue(classModule, null).ToString();


                        }
                        else if (propriedade.GetCustomAttributes(typeof(PrimaryKey), false).Count() > 0)
                        {
                            _primaryKey = propriedade.Name;
                        }
                        else
                        {
                            _fields.Add(propriedade.Name);
                            if (propriedade.GetValue(classModule, null) != null)
                            {
                                _values.Add(propriedade.GetValue(classModule, null).ToString());
                                _stateParams.Add(new My.NpgsqlParameter(propriedade.Name, propriedade.GetValue(classModule, null)));
                            }
                            else
                            {
                                _values.Add("");
                                if (propriedade.Name == "sr_deleted")
                                    _stateParams.Add(new My.NpgsqlParameter(propriedade.Name, ""));
                                else
                                    _stateParams.Add(new My.NpgsqlParameter(propriedade.Name, null));
                            }
                        }
                    }
                }
                StringBuilder sb = new StringBuilder();
                sb.Append("insert into " + _entityName);
                sb.AppendLine(" (");

                _count = 0;
                foreach (string s in _fields)
                {
                    _count++;
                    sb.Append(" " + s + " ");
                    if (_count != _fields.Count)
                        sb.Append(", ");
                }
                sb.AppendLine(") values(");

                _count = 0;
                foreach (string s in _fields)
                {
                    _count++;
                    sb.Append("@" + s);
                    if (_count != _fields.Count)
                        sb.Append(", ");
                }
                sb.AppendLine(") ");
                if (_where == null)
                {

                }
                try
                {
                    /*   Verificação de tipo primitivo
                       * Os tipo considerados primitivos, são aqueles onde as tabelas 'Entidades' possui um campo chamado 'codigo'
                       * Porem eles não são chave primaria no banco, não são auto-incremento e são do tipo string.
                       * Essa situção exige esse controle para gravação dos código sequenciais.
                       */


                    if (classModule.Primitive)
                    {
                        if (classModule.ChangePrimitiveController)
                        {
                            //Removendo o parametro antigo
                            string cod = "";
                            var result = from c in this._stateParams
                                         where c.ParameterName == classModule.NamePrimitiveController
                                         select c;
                            _stateParams.Remove(result.First());
                            //Verificando a condição para leitura do indice
                            if (string.IsNullOrEmpty(classModule.PrimitiveCondition))
                                cod = Postgres.ConnectionManager.executeScalar("select " + classModule.NamePrimitiveController + " from " + _entityName + " where sr_deleted !='T' order by sr_recno desc limit 1").ToString();
                            else
                                cod = Postgres.ConnectionManager.executeScalar(string.Format("select " + classModule.NamePrimitiveController + " from " + _entityName + " where sr_deleted !='T' and {0} order by sr_recno desc limit 1", classModule.PrimitiveCondition)).ToString();

                            //cod = (Convert.ToInt32(cod) + 1).ToString().PadLeft(classModule.PadPrimitive, '0');

                            PropertyInfo p = classModule.GetType().GetProperty(classModule.NamePrimitiveController);
                            p.SetValue(classModule, (Convert.ToInt32(cod) + 1).ToString().PadLeft(classModule.PadPrimitive, '0'), null);
                            _fields.Add(p.Name);
                            _values.Add((Convert.ToInt32(cod) + 1).ToString().PadLeft(classModule.PadPrimitive, '0'));
                            _stateParams.Add(new My.NpgsqlParameter(p.Name, (Convert.ToInt32(cod) + 1).ToString().PadLeft(classModule.PadPrimitive, '0')));

                            //Atualizando o arquivo de controle
                            Postgres.ConnectionManager.update(string.Format("update controle set numero ={0} + 1 where arquivo = '{1}'", cod, _entityName.ToUpper()));
                        }
                        else
                        {
                            //Removendo o parametro antigo
                            string cod = "";
                            var result = from c in this._stateParams
                                         where c.ParameterName == "codigo"
                                         select c;
                            _stateParams.Remove(result.First());
                            //Verificando a condição para leitura do indice
                            if (string.IsNullOrEmpty(classModule.PrimitiveCondition))
                                cod = Postgres.ConnectionManager.executeScalar("select codigo from " + _entityName + " where sr_deleted !='T' order by sr_recno desc limit 1").ToString();
                            else
                                cod = Postgres.ConnectionManager.executeScalar(string.Format("select codigo from " + _entityName + " where sr_deleted !='T' and {0} order by sr_recno desc limit 1", classModule.PrimitiveCondition)).ToString();

                            //cod = (Convert.ToInt32(cod) + 1).ToString().PadLeft(classModule.PadPrimitive, '0');

                            PropertyInfo p = classModule.GetType().GetProperty("codigo");
                            p.SetValue(classModule, (Convert.ToInt32(cod) + 1).ToString().PadLeft(classModule.PadPrimitive, '0'), null);
                            _fields.Add(p.Name);
                            _values.Add((Convert.ToInt32(cod) + 1).ToString().PadLeft(classModule.PadPrimitive, '0'));
                            _stateParams.Add(new My.NpgsqlParameter(p.Name, (Convert.ToInt32(cod) + 1).ToString().PadLeft(classModule.PadPrimitive, '0')));

                            //Atualizando o arquivo de controle
                            Postgres.ConnectionManager.update(string.Format("update controle set numero ={0} + 1 where arquivo = '{1}'", cod, _entityName.ToUpper()));
                        }
                    }

                    _lastInsertID = Postgres.ConnectionManager.InsertWitchKey(sb.ToString(), _stateParams);
                    //Informando a Chave Primária do registro no Modelo
                    PropertyInfo pPrimary = classModule.GetType().GetProperty(_primaryKey);
                    pPrimary.SetValue(classModule, _lastInsertID, null);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;

        }
        /// <summary>
        ///  Método de update genérico, ultiliza reflection para leitura das propriedades
        /// </summary>
        /// <param name="classModule">Pode ser qualquer classe de modelo herdada de 
        /// Model.Framework.Model </param>
        /// <returns></returns>
        public bool FrameworkUpdate(Model.Framework.Model classModule)
        {
            try
            {
                _fields = new List<string>();
                _values = new List<string>();
                _stateParams = new List<My.NpgsqlParameter>();
                var tipo = classModule.GetType();
                //Retornando os fields da classe
                var fields = tipo.GetFields();


                foreach (PropertyInfo propriedade in classModule.GetType().GetProperties())
                {
                    if (propriedade.GetCustomAttributes(typeof(NotField), false).Count() == 0)
                    {
                        if (propriedade.GetCustomAttributes(typeof(EntityConfiguration), false).Count() > 0)
                        {
                            _entityName = propriedade.GetValue(classModule, null).ToString();
                        }
                        else if (propriedade.GetCustomAttributes(typeof(PrimaryKey), false).Count() > 0)
                        {
                            if (propriedade.GetValue(classModule, null) != null)
                            {

                                _values.Add(propriedade.GetValue(classModule, null).ToString());
                                _stateParams.Add(new My.NpgsqlParameter(propriedade.Name, propriedade.GetValue(classModule, null)));
                                _where = String.Format("{0}=@{0}", propriedade.Name);
                            }
                        }
                        else
                        {
                            //Verificar campos não existentes na tabelas 'fields'.
                            //Essa verificação evita: 
                            //1 - Instruções Sql grandes desnecessárias.
                            //2 - Remoção de Valores no banco quando o field existe no Model e Não existe na tabela 'fields'
                            if (classModule.LinkFields)
                            {
                                var fCampo = from c in classModule.Fileds
                                             where c.model_reference == propriedade.Name
                                             select c;

                                if (fCampo.Count() == 0)
                                    continue;

                            }

                            _fields.Add(propriedade.Name);
                            if (propriedade.GetValue(classModule, null) != null)
                            {
                                _values.Add(propriedade.GetValue(classModule, null).ToString());
                                _stateParams.Add(new My.NpgsqlParameter(propriedade.Name, propriedade.GetValue(classModule, null)));
                            }
                            else
                            {
                                _values.Add("");
                                _stateParams.Add(new My.NpgsqlParameter(propriedade.Name, null));
                            }
                        }
                    }
                }
                StringBuilder sb = new StringBuilder();
                sb.Append("update " + _entityName);
                sb.AppendLine(" set ");

                _count = 0;
                foreach (string s in _fields)
                {
                    _count++;
                    sb.Append(String.Format("`{0}`=@{0}", s));
                    if (_count != _fields.Count)
                        sb.Append(", ");
                }
                _count = 0;
                if (!string.IsNullOrEmpty(_where))
                {
                    sb.AppendLine(" where " + _where);
                }
                else
                {
                    throw new InvalidOperationException("Você não pode enviar uma instrução 'UPDATE' sem a cláusula 'WHERE'");
                }

                _sql = sb.ToString();

                //if (_sql.Contains("@desc"))
                //    _sql = _sql.Replace("desc=", "`desc`=");
                //if (_sql.Contains("desc,"))
                //    _sql = _sql.Replace("desc,", "`desc`,");
                //if (_sql.Contains("out,"))
                //    _sql = _sql.Replace("out,", "`out`,");
                //if (_sql.Contains("use,"))
                //    _sql = _sql.Replace("use,", "`use`,");
                //if (_sql.Contains("local,"))
                //    _sql = _sql.Replace("local,", "`local`,");
                //if (_sql.Contains("real,"))
                //    _sql = _sql.Replace("real,", "`real`,");

                return Postgres.ConnectionManager.update(_sql, _stateParams);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable FrameworkGetTable(string sql)
        {
            return MySql.ConnectionManager.consultaDt(sql);
        }
        /// <summary>
        ///  Método de delete genérico, ultiliza reflection para leitura das propriedades
        /// </summary>
        /// <param name="classModule">Pode ser qualquer classe de modelo herdada de 
        /// Model.Framework.Model </param>
        /// <returns></returns>
        public bool FrameworkDelete(Model.Framework.Model classModule)
        {
            try
            {
                _fields = new List<string>();
                _values = new List<string>();
                _stateParams = new List<My.NpgsqlParameter>();
                var tipo = classModule.GetType();
                //Retornando os fields da classe
                var fields = tipo.GetFields();

                foreach (PropertyInfo propriedade in classModule.GetType().GetProperties())
                {
                    if (propriedade.GetCustomAttributes(typeof(NotField), false).Count() == 0)
                    {
                        if (propriedade.GetCustomAttributes(typeof(EntityConfiguration), false).Count() > 0)
                        {
                            _entityName = propriedade.GetValue(classModule, null).ToString();
                        }
                        else if (propriedade.GetCustomAttributes(typeof(PrimaryKey), false).Count() > 0)
                        {
                            if (propriedade.GetValue(classModule, null) != null)
                            {
                                _values.Add(propriedade.GetValue(classModule, null).ToString());
                                _stateParams.Add(new My.NpgsqlParameter(propriedade.Name, propriedade.GetValue(classModule, null)));
                                _where = String.Format("{0}=@{0}", propriedade.Name);
                            }
                        }
                        else
                            _fields.Add(propriedade.Name);

                    }
                }
                StringBuilder sb = new StringBuilder();

                if (_fields.Contains("sr_deleted"))
                {
                    sb.Append("update " + _entityName);
                    sb.AppendLine(" set sr_deleted ='T'");
                    if (!string.IsNullOrEmpty(_where))
                    {
                        sb.AppendLine(" where " + _where);
                        return Postgres.ConnectionManager.update(sb.ToString(), _stateParams);
                    }
                    else
                    {
                        throw new InvalidOperationException("Você não pode enviar uma instrução 'DELETE LÓGICO' sem a cláusula 'WHERE'");
                    }
                }
                else
                {
                    sb.Append("delete from " + _entityName);
                    if (!string.IsNullOrEmpty(_where))
                    {
                        sb.AppendLine(" where " + _where);
                        return Postgres.ConnectionManager.update(sb.ToString(), _stateParams);
                    }
                    else
                    {
                        throw new InvalidOperationException("Você não pode enviar uma instrução 'DELETE FÍSICO' sem a cláusula 'WHERE'");
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        /// <summary>
        ///  Método de leitura, ultiliza reflection para preenchimento das propriedades
        /// </summary>
        /// <param name="classModule">Pode ser qualquer classe de modelo herdada de 
        /// Model.Framework.Model </param>
        /// <returns></returns>
        public List<T> FrameworkGet(Model.Framework.Model classModule)
        {

            _fields = new List<string>();
            _values = new List<string>();
            _joinFields = new List<string>();
            _functionFields = new List<string>();
            _functionFields = new List<string>();
            _cleanFunctionFields = new List<string>();
            _stateParams = new List<My.NpgsqlParameter>();
            Type Tp = classModule.GetType();

            //Configurando tamanho da paginação
            if (OnlyFirstResult)
                _pageSize = " limit 1";

            try
            {
                //Criando apelido para a Tabela
                _entityAlias = classModule.Entity.Substring(0, 1);
            }
            catch (Exception ex)
            {
                throw new Exception("Não definido um nome de entidade no Modelo");
            }

            try
            {
                foreach (PropertyInfo propriedade in classModule.GetType().GetProperties())
                {
                    if (propriedade.GetCustomAttributes(typeof(NotField), false).Count() == 0)
                    {
                        if (propriedade.GetCustomAttributes(typeof(EntityConfiguration), false).Count() > 0)
                        {
                            _entityName = propriedade.GetValue(classModule, null).ToString();

                        }
                        else if (propriedade.GetCustomAttributes(typeof(PrimaryKey), false).Count() > 0)
                        {
                            if (propriedade.GetValue(classModule, null) != null && propriedade.GetValue(classModule, null).ToString() != "0")
                            {
                                _values.Add(propriedade.GetValue(classModule, null).ToString());
                                _stateParams.Add(new My.NpgsqlParameter(propriedade.Name, propriedade.GetValue(classModule, null)));
                                _where = String.Format("{1}.`{0}`=@{0}", propriedade.Name, _entityAlias);
                                _primaryKey = propriedade.Name;
                                _fields.Add(propriedade.Name);
                            }
                            else
                            {
                                _primaryKey = propriedade.Name;
                                _fields.Add(propriedade.Name);
                            }
                        }
                        else if (propriedade.GetCustomAttributes(typeof(ForeingKey), false).Count() > 0)
                        {
                            if (!NoJoinsFunctions)
                            {
                                object[] pKey = propriedade.GetCustomAttributes(typeof(ForeingKey), false);
                                ForeingKey Key = pKey[0] as ForeingKey;
                                _fieldToJoin = propriedade.Name;
                                _joinField = Key.FieldShow;
                                _join = Key.Table;
                                _aliasJoin = Key.Table.Substring(0, 1);
                                if (_entityAlias == _aliasJoin)
                                    _aliasJoin = Key.Table.Substring(1, 2);
                                if (_entityAlias == _aliasJoin)
                                    _aliasJoin = Key.Table.Substring(2, 3);

                                //Montando o Sql com o Join
                                _joinFields.Add(_joinField);
                                _joinCondition = string.Format("{0}.`{1}` = {2}.`{3}`", _entityAlias, propriedade.Name,
                                                    _aliasJoin, Key.TableKey);
                            }
                            else // Nesse caso, é uma consulta de edição... Aqui o modelo será retornado como no BD, sem joins
                            {
                                _fields.Add(propriedade.Name);
                                if (propriedade.GetValue(classModule, null) != null)
                                {
                                    _values.Add(propriedade.GetValue(classModule, null).ToString());
                                    //_stateParams.Add(new My.MySqlParameter(propriedade.Name, propriedade.GetValue(classModule, null)));
                                }
                                else
                                {
                                    _values.Add("");
                                }
                            }
                        }
                        else if (propriedade.GetCustomAttributes(typeof(Function), false).Count() > 0)
                        {
                            if (!NoJoinsFunctions)
                            {
                                object[] pFunc = propriedade.GetCustomAttributes(typeof(Function), false);
                                Function f = pFunc[0] as Function;
                                _functionFields.Add(string.Format(f.NameFunction + " as {1}",
                                    _entityAlias + ".`" + propriedade.Name + "`", propriedade.Name));
                                _cleanFunctionFields.Add(propriedade.Name);

                                //Where com valores de funções
                                //if (propriedade.GetValue(classModule, null) != null && propriedade.GetValue(classModule, null).ToString() != "0")
                                //{
                                //    _values.Add(propriedade.GetValue(classModule, null).ToString());
                                //    _stateParams.Add(new My.MySqlParameter(propriedade.Name, propriedade.GetValue(classModule, null)));
                                //    _where = String.Format(f.NameFunction + " as {1}",
                                //    _entityAlias + ".`" + propriedade.Name + "`", propriedade.Name);
                                //    _primaryKey = propriedade.Name;
                                //    _fields.Add(propriedade.Name);
                                //}
                            }
                            else // Nesse caso, é uma consulta de edição... Aqui o modelo será retornado como no BD, sem joins
                            {
                                _fields.Add(propriedade.Name);
                                if (propriedade.GetValue(classModule, null) != null)
                                {
                                    _values.Add(propriedade.GetValue(classModule, null).ToString());
                                    //_stateParams.Add(new My.MySqlParameter(propriedade.Name, propriedade.GetValue(classModule, null)));
                                }
                                else
                                {
                                    _values.Add("");
                                }
                            }
                        }
                        else
                        {
                            _fields.Add(propriedade.Name);
                            if (propriedade.GetValue(classModule, null) != null)
                            {
                                _values.Add(propriedade.GetValue(classModule, null).ToString());
                                //_stateParams.Add(new My.MySqlParameter(propriedade.Name, propriedade.GetValue(classModule, null)));
                            }
                            else
                            {
                                _values.Add("");
                            }
                        }
                    }
                }


            }
            catch (Exception ex)
            {

            }

            //Verificando ordem dos registros
            if (!string.IsNullOrEmpty(classModule.CustomOrder))
            {
                _custonOrderBy = classModule.CustomOrder;
            }

            if (string.IsNullOrEmpty(_entityName))
                throw new InvalidOperationException("O modelo informado não possui o nome de sua entidade física.");

            StringBuilder sb = new StringBuilder();
            sb.Append("select ");
            _count = 0;

            foreach (string s in _functionFields)
            {
                sb.Append(s);
                sb.Append(", ");
            }
            foreach (string s in _joinFields)
            {
                sb.Append(_aliasJoin);
                sb.Append(".");
                sb.Append(s);
                sb.Append(" ");
                sb.Append(" as " + _fieldToJoin);
                sb.Append(", ");

                _fields.Add(_fieldToJoin);
            }
            foreach (string s in _fields)
            {
                _count++;
                sb.Append(_entityAlias);
                sb.Append(".");
                sb.Append(s);
                sb.Append("");
                if (_count != _fields.Count)
                    sb.Append(", ");
            }

            //Adicionando o fields com funções depois da montagem do Sql
            _fields.AddRange(_cleanFunctionFields);

            sb.AppendLine(" from " + _entityName + " " + _entityAlias);

            //Verificando o Join
            if (_joinFields.Count > 0)
            {
                sb.AppendLine(" left join " + _join + " " + _aliasJoin + " on " + _joinCondition);
            }

            DataTable dt;

            if (!string.IsNullOrEmpty(_where))
            {
                sb.AppendLine(" where " + _where);
                if (_fields.Contains("sr_deleted"))
                    sb.AppendLine(" and " + _entityAlias + "." + _sr_deleted);
                //Atribuindo tamanho da página
                sb.AppendLine(_pageSize);
                //Verifica exixtencia de palavras reservadas
                _sql = sb.ToString();
                if (_sql.Contains(" desc,"))
                    _sql = _sql.Replace("desc,", "`desc`,");
                if (_sql.Contains("out,"))
                    _sql = _sql.Replace("out,", "`out`,");
                if (_sql.Contains("use,"))
                    _sql = _sql.Replace("use,", "`use`,");
                if (_sql.Contains("local,"))
                    _sql = _sql.Replace("local,", "`local`,");
                if (_sql.Contains("real,"))
                    _sql = _sql.Replace("real,", "`real`,");

                if (!string.IsNullOrEmpty(_custonOrderBy))
                {
                    sb.AppendLine(" order by " + _custonOrderBy);
                }

                dt = Postgres.ConnectionManager.consultaDt(_sql, _stateParams);
            }
            else if (!string.IsNullOrEmpty(_custonWhere))
            {
                //sb.AppendLine(" where " + _entityAlias + "." + _custonWhere);

                //Tratando tokens da condição where customizada
                if (!_custonWhere.Contains("between") && !_custonWhere.Contains("("))
                {
                    string[] whereTokens = _custonWhere.Split(new string[] { " and " }, StringSplitOptions.None);
                    string _newWhere = "";

                    foreach (string sTok in whereTokens)
                    {
                        _newWhere += _entityAlias + "." + sTok + " and ";
                    }
                    _newWhere = _newWhere.Remove(_newWhere.LastIndexOf(" and "));

                    sb.AppendLine(" where " + _newWhere);
                }
                else
                {
                    sb.AppendLine(" where " + _custonWhere);
                }
                //sb.AppendLine(" where " + _entityAlias + "." +_custonWhere);
                if (_fields.Contains("sr_deleted"))
                    sb.AppendLine(" and " + _entityAlias + "." + _sr_deleted);
                //Atribuindo tamanho da página
                sb.AppendLine(_pageSize);
                //Verifica exixtencia de palavras reservadas
                _sql = sb.ToString();
                if (_sql.Contains("desc,"))
                    _sql = _sql.Replace(" desc,", "`desc`,");
                if (_sql.Contains("out,"))
                    _sql = _sql.Replace("out,", "`out`,");
                if (_sql.Contains("use,"))
                    _sql = _sql.Replace("use,", "`use`,");
                if (_sql.Contains("local,"))
                    _sql = _sql.Replace("local,", "`local`,");
                if (_sql.Contains("real,"))
                    _sql = _sql.Replace("real,", "`real`,");

                if (!string.IsNullOrEmpty(_custonOrderBy))
                {
                    _sql += " order by " + _custonOrderBy;
                }

                dt = Postgres.ConnectionManager.consultaDt(_sql, _stateParams);

            }
            else
            {
                if (_fields.Contains("sr_deleted"))
                    sb.AppendLine(" where " + _entityAlias + "." + _sr_deleted);
                //At ribuindo tamanho da página
                sb.AppendLine(_pageSize);
                //Verifica exixtencia de palavras reservadas
                _sql = sb.ToString();
                if (_sql.Contains("desc,"))
                    _sql = _sql.Replace(" desc,", "`desc`,");
                if (_sql.Contains("out,"))
                    _sql = _sql.Replace("out,", "`out`,");
                if (_sql.Contains("use,"))
                    _sql = _sql.Replace("use,", "`use`,");
                if (_sql.Contains("local,"))
                    _sql = _sql.Replace("local,", "`local`,");
                if (_sql.Contains("real,"))
                    _sql = _sql.Replace("real,", "`real`,");

                if (!string.IsNullOrEmpty(_custonOrderBy))
                {
                    _sql += " order by " + _entityAlias + "." + _custonOrderBy;
                }
                else
                {
                    //Todas consultas enviadas sem clausula where tem pagina máxima de 100 registros.
                    _sql += " order by " + _entityAlias + "." + classModule.FieldRecno + " desc limit 100";
                }
                dt = MySql.ConnectionManager.consultaDt(_sql);
            }

            DataTableReader dr = new DataTableReader(dt);
            List<T> lista = new List<T>();
            while (dr.Read())
            {
                T _p = Activator.CreateInstance<T>();
                try
                {
                    //Gravando chave primária
                    PropertyInfo prop = _p.GetType().GetProperty(_primaryKey);
                    prop.SetValue(_p, dr[_primaryKey], null);
                }
                catch (Exception ex)
                {

                    throw new InvalidOperationException("Não foi registrado um atributo de chave primaria neste modelo.");
                }
                //Recuperandos os fileds
                foreach (string s in _fields)
                {
                    try
                    {
                        Type _tp = dr[s].GetType();
                        if (_tp != typeof(System.DBNull))
                        {
                            if (_tp == typeof(System.Byte[]))
                            {
                                PropertyInfo p = _p.GetType().GetProperty(s);
                                p.SetValue(_p, System.Text.ASCIIEncoding.Default.GetString((Byte[])dr[s]), null);
                            }
                            else if (_tp == typeof(System.UInt32))
                            {
                                PropertyInfo p = _p.GetType().GetProperty(s);
                                p.SetValue(_p, Convert.ToUInt32(dr[s]), null);
                            }
                            else if (_tp == typeof(System.String))
                            {
                                PropertyInfo p = _p.GetType().GetProperty(s);
                                p.SetValue(_p, dr[s].ToString(), null);
                            }
                            else if (_tp == typeof(System.Int32))
                            {
                                PropertyInfo p = _p.GetType().GetProperty(s);
                                p.SetValue(_p, Convert.ToInt32(dr[s]), null);
                            }
                            else if (_tp == typeof(System.Decimal))
                            {
                                try
                                {
                                    PropertyInfo p = _p.GetType().GetProperty(s);
                                    p.SetValue(_p, Convert.ToDecimal(dr[s]), null);
                                }
                                catch (Exception)
                                {
                                    PropertyInfo p = _p.GetType().GetProperty(s);
                                    p.SetValue(_p, Convert.ToDouble(dr[s]), null);
                                }

                            }
                            else
                            {
                                PropertyInfo p = _p.GetType().GetProperty(s);
                                p.SetValue(_p, dr[s], null);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(String.Format("Erro ao associar a meta-informação com valor.\nCampo: {0}, tipo do valor: {1}, propriedade destino: {2}",
                                                s,
                                                dr[s].GetType(),
                                                s));
                    }

                }
                lista.Add(_p);
            }

            return lista;
        }



        public string GetLockup(Model.Framework.Model classModule, int type)
        {
            try
            {
                foreach (PropertyInfo propriedade in classModule.GetType().GetProperties())
                {
                    if (propriedade.GetCustomAttributes(typeof(NotField), false).Count() == 0)
                    {
                        if (propriedade.GetCustomAttributes(typeof(EntityConfiguration), false).Count() > 0)
                        {
                            _entityName = propriedade.GetValue(classModule, null).ToString();
                        }
                        else if (propriedade.GetCustomAttributes(typeof(PrimaryKey), false).Count() > 0)
                        {

                        }
                        else if (propriedade.GetCustomAttributes(typeof(CodLockup), false).Count() > 0)
                        {
                            _codLockup = propriedade.Name;
                            if (propriedade.GetValue(classModule, null) != null)
                            {
                                _codLockupValue = propriedade.GetValue(classModule, null).ToString();
                                //_stateParams.Add(new My.MySqlParameter(propriedade.Name, propriedade.GetValue(classModule, null)));
                            }

                        }
                        else if (propriedade.GetCustomAttributes(typeof(DescLockup), false).Count() > 0)
                        {
                            _descLockup = propriedade.Name;

                        }
                    }
                }
                _codLockupValue = classModule.LockUp;

                StringBuilder sb = new StringBuilder();

                switch (type)
                {
                    case 1:
                        sb.AppendFormat("select `{1}` from `{2}` where `{0}`= '{4}'", _codLockup, _descLockup, _entityName, _codLockup, _codLockupValue);
                        break;
                    case 2:
                        sb.AppendFormat("select concat(`{0}`,' - ', `{1}`) from `{2}` where `{0}`= '{4}'", _codLockup, _descLockup, _entityName, _codLockup, _codLockupValue);
                        break;
                    default:
                        sb.AppendFormat("select `{1}` from `{2}` where `{0}`= '{4}'", _codLockup, _descLockup, _entityName, _codLockup, _codLockupValue);
                        break;
                }

                return MySql.ConnectionManager.executeScalar(sb.ToString()).ToString();
            }
            catch (Exception ex)
            {

                return "";
            }


        }
    }
}
