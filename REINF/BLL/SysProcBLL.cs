using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.Framework;
using DAL;
using System.Data;
using Model;

namespace BLL
{
    public class SysProcBLL : Framework.BLL<SysProcModel>, Framework.IBLL
    {
        public DataTable GetProcByForm(Int64 codForm)
        {
            return new SysProcDAL().GetProcByForm(codForm);
        }
        public bool FrameworkInsert(Model.Framework.Model modelo)
        {
            return new SysProcDAL().FrameworkInsert(modelo);
        }

        public object FrameworkGet(Model.Framework.Model modelo)
        {
            return new SysProcDAL().FrameworkGet(modelo);
        }

        public object FrameworkGetOne(Model.Framework.Model modelo, int recno)
        {
            SysProcDAL dal = new SysProcDAL();
            dal.CustomWhere = "id =" + recno;
            return dal.FrameworkGet(modelo).First();
        }

        public bool FrameworkUpdate(Model.Framework.Model modelo)
        {
            return new SysProcDAL().FrameworkUpdate(modelo);
        }

        public bool FrameworkDelete(Model.Framework.Model modelo)
        {
            return new SysProcDAL().FrameworkDelete(modelo);
        }

        public object FrameworkGetCustom(Model.Framework.Model modelo, string _customParam)
        {
            SysProcDAL dal = new SysProcDAL();
            dal.CustomWhere = _customParam;
            return dal.FrameworkGet(modelo);
        }

        public string GetLockup(Model.Framework.Model modelo, int type)
        {
            throw new NotImplementedException();
        }
    }
}
