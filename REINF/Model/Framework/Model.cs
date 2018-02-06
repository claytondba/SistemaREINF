using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;

namespace Model.Framework
{
    [Serializable]
    public abstract class Model: IComparable, ICloneable
    {
        public Model()
        {
            //Preenchendo o valor do field com a chave primaria anotada no modelo
            foreach (PropertyInfo propriedade in this.GetType().GetProperties())
            {
                if (propriedade.GetCustomAttributes(typeof(NotField), false).Count() == 0)
                {
                    if (propriedade.GetCustomAttributes(typeof(PrimaryKey), false).Count() > 0)
                    {
                        FieldRecno = propriedade.Name;
                    }
                
                }
            }              

        }
        public class Structure2DataTable<T>
        {
            private DataTable dt = new DataTable();

            public DataTable GetDataTable(T t)
            {
                DataTable dt = new DataTable();

                Type tipo = t.GetType();

                if (tipo.IsArray)
                {
                    Array arr = (t as Array);

                    foreach (object obj in arr)
                    {
                        tipo = obj.GetType();
                        PropertyInfo[] propriedades = tipo.GetProperties();

                        if (propriedades.Length > 0)
                        {
                            DataRow row = dt.NewRow();

                            foreach (PropertyInfo p in propriedades)
                            {
                                if (!dt.Columns.Contains(p.Name)) dt.Columns.Add(p.Name, p.PropertyType);
                                row[p.Name] = p.GetValue(obj, null);
                            }

                            dt.Rows.Add(row);
                            dt.AcceptChanges();
                        }
                    }
                }
                else
                {
                    PropertyInfo[] propriedades = tipo.GetProperties();

                    if (propriedades.Length > 0)
                    {
                        DataRow row = dt.NewRow();

                        foreach (PropertyInfo p in propriedades)
                        {
                            if (!dt.Columns.Contains(p.Name)) dt.Columns.Add(p.Name, p.PropertyType);
                            row[p.Name] = p.GetValue(t, null);
                        }

                        dt.Rows.Add(row);
                        dt.AcceptChanges();
                    }
                }

                return dt;
            }
        }

        public string Serialize()
        {
            try
            {
                //XmlSerializer xml = new XmlSerializer(this.GetType());
                StringWriter retorno = new StringWriter();
                //xml.Serialize(retorno, this.GetType());
                //return retorno.ToString();

                XmlSerializer ser = new XmlSerializer(this.GetType(), new XmlRootAttribute("objeto"));

                //Serializar o list para o TextWriter e salvar os dados no XML
                ser.Serialize(retorno, this);

                return retorno.ToString();

            }
            catch(Exception ex)
            {
                return "";
            }
        }


        /// <summary>
        /// Clone de Instâncias!
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        [NotField(isNotField = true)]
        public Int64 RecnoValue { get; set; }
        [NotField(isNotField = true)]
        public string FieldRecno { get; set; }
        [NotField(isNotField = true)]
        ///Fields do banco de dados (Funciona quando acionado a propriedade LinkFields)
        public List<FieldsModel> Fileds { get; set; }
        [NotField(isNotField = true)]
        ///Flag que indica se os campos obrigatoriamente tem que mater uniformes Model X Fields
        public bool LinkFields { get; set; }
        [EntityConfiguration(EntityName=true)]
        ///Nome da tabela física no Banco de Dados
        public string Entity { get; set; }
        [NotField(isNotField = true)]
        ///Indica se o objeto é controlado por outra chave além do indi primario do banco de dados. 
        ///(Compatibilidade DBF)
        public bool Primitive { get; set; }
        [NotField(isNotField = true)]
        ///Informado quando o campo de controle é varchar, é completado com zeros (PadPrimitive = 4 => 97 = 0097)
        public int PadPrimitive { get; set; }
        [NotField(isNotField = true)]
        ///Condição para leitura do indice do DBF (Exemplo: 'campo=condição')
        public string PrimitiveCondition { get; set; }
        [NotField(isNotField = true)]
        [Lockup(isLockup= true)]
        public string LockUp { get; set; }
        [NotField(isNotField = true)]
        public string CustomOrder { get; set; }
        [NotField(isNotField = true)]
        public UsuariosModel Usuario { get; set; }
        //[NotField(isNotField = true)]
        //public bool AllowDelete { get; set; }
        //[NotField(isNotField = true)]
        //public bool AlloInsert { get; set; }
        //Campos Marcados não serão aouditados por sys_log_notify
        [NotField(isNotField = true)]
        public bool NoAuditTable { get; set; }
        //Campo indica a troca de campo de controle, por padrão o sistema usará o campo "codigo"
        //como controle padrão
        [NotField(isNotField = true)]
        public bool ChangePrimitiveController { get; set; }
        //Usado quando a flag 'ChangePrimitiveController' estiver ligada, inverterá o controlador pelo nome 
        //informado nesse campo
        [NotField(isNotField = true)]
        public string NamePrimitiveController { get; set; }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class EntityConfiguration : System.Attribute
    {        
        public bool EntityName { get; set; }

    }
    [AttributeUsage(AttributeTargets.Property)]
    public class Browsable : System.Attribute
    {
        public bool Visible { get; set; }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class NotField : System.Attribute
    {
        public bool isNotField { get; set; }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKey : System.Attribute
    {
        public bool isKey { get; set; }
    }
    [AttributeUsage(AttributeTargets.Class)]
    public class LogicDelete : System.Attribute
    {
        public bool isLogicDelete { get; set; }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class DescLockup : System.Attribute
    {
        public bool isDescLockup { get; set; }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class CodLockup : System.Attribute
    {
        public bool isCodLockup { get; set; }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class Lockup : System.Attribute
    {
        public bool isLockup { get; set; }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class ForeingKey : System.Attribute
    {
        public string Table { get; set; }
        public string FieldShow { get; set; }
        public string TableKey { get; set; }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class Function : System.Attribute
    {
        public string NameFunction { get; set; }
    }
}
