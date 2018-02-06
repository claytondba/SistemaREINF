using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.Framework;
using DAL;
using Model;

namespace BLL
{
    public class SysProcParamBLL : Framework.BLL<SysProcParamModel>, Framework.IBLL
    {

        public bool FrameworkInsert(Model.Framework.Model modelo)
        {
            return new SysProcParamDAL().FrameworkInsert(modelo);
        }

        public object FrameworkGet(Model.Framework.Model modelo)
        {
            return new SysProcParamDAL().FrameworkGet(modelo);
        }

        public object FrameworkGetOne(Model.Framework.Model modelo, int recno)
        {
            SysProcParamDAL dal = new SysProcParamDAL();
            dal.CustomWhere = "id =" + recno;
            return dal.FrameworkGet(modelo).First();
        }

        public bool FrameworkUpdate(Model.Framework.Model modelo)
        {
            return new SysProcParamDAL().FrameworkUpdate(modelo);
        }

        public bool FrameworkDelete(Model.Framework.Model modelo)
        {
            return new SysProcParamDAL().FrameworkDelete(modelo);
        }

        public object FrameworkGetCustom(Model.Framework.Model modelo, string _customParam)
        {
            SysProcParamDAL dal = new SysProcParamDAL();
            dal.CustomWhere = _customParam ;
            return dal.FrameworkGet(modelo);
        }

        public string GetLockup(Model.Framework.Model modelo, int type)
        {
            throw new NotImplementedException();
        }
    }
}
