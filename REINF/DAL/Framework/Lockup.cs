using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using My = MySql.Data.MySqlClient;
using Model;
using System.Data;
using Model.Framework;

namespace DAL.Framework
{
    public abstract class Lockup<T> where T : Model.Framework.Model
    {
        #region Fields
        private const string _sr_deleted = " sr_deleted !='T'";
        private string _entityName;
        private List<My.MySqlParameter> _stateParams;
        private List<string> _fields;
        private List<string> _values;
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
        #endregion

       // public string GetName(Model.Framework.Model classModule)
        //{
            //foreach (PropertyInfo propriedade in classModule.GetType().GetProperties())
            //{
            //    if (propriedade.GetCustomAttributes(typeof(NotField), false).Count() == 0)
            //    {
            //        if (propriedade.GetCustomAttributes(typeof(EntityConfiguration), false).Count() > 0)
            //        {
            //            _entityName = propriedade.GetValue(classModule, null).ToString();
            //        }
            //        else if (propriedade.GetCustomAttributes(typeof(PrimaryKey), false).Count() > 0)
            //        {
            //            if (propriedade.GetValue(classModule, null) != null && propriedade.GetValue(classModule, null).ToString() != "0")
            //            {
            //                _values.Add(propriedade.GetValue(classModule, null).ToString());
            //                _stateParams.Add(new My.MySqlParameter(propriedade.Name, propriedade.GetValue(classModule, null)));
            //                _where = String.Format("{0}=@{0}", propriedade.Name);
            //                _primaryKey = propriedade.Name;
            //                _fields.Add(propriedade.Name);
            //            }
            //            else
            //            {
            //                _primaryKey = propriedade.Name;
            //                _fields.Add(propriedade.Name);
            //            }
            //        }
            //        else if (propriedade.GetCustomAttributes(typeof(CodLockup), false).Count() > 0)
            //        {
            //            _codLockup = propriedade.Name;
            //            if (propriedade.GetValue(classModule, null) != null)
            //            {
            //                _codLockupValue = propriedade.GetValue(classModule, null).ToString();
            //                //_stateParams.Add(new My.MySqlParameter(propriedade.Name, propriedade.GetValue(classModule, null)));
            //            }
                       
            //        }
            //        else if (propriedade.GetCustomAttributes(typeof(DescLockup), false).Count() > 0)
            //        {
            //            _descLockup = propriedade.Name;
                       
            //        }
            //    }
            //}

            //StringBuilder sb = new StringBuilder();
            //sb.AppendFormat("select {0},{1} from {2} where {0}= '{3}'", _codLockup, _descLockup, _entityName, _codLockup, _codLockupValue);

        }
    
}
