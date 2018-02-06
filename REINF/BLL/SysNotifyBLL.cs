using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.Framework;
using DAL;
using Model;

namespace BLL
{
    public class SysNotifyBLL : Framework.BLL<SysNotifyModel>, Framework.IBLL
    {
        public bool FrameworkInsert(Model.Framework.Model modelo)
        {
            return new SysNotifyDAL().FrameworkInsert(modelo);
        }

        public object FrameworkGet(Model.Framework.Model modelo)
        {
            return new SysNotifyDAL().FrameworkGet(modelo);
        }

        public object FrameworkGetOne(Model.Framework.Model modelo, int recno)
        {
            SysNotifyDAL dal = new SysNotifyDAL();
            dal.CustomWhere = "id =" + recno;
            return dal.FrameworkGet(modelo).First();
        }

        public bool FrameworkUpdate(Model.Framework.Model modelo)
        {
            return new SysNotifyDAL().FrameworkUpdate(modelo);
        }

        public bool FrameworkDelete(Model.Framework.Model modelo)
        {
            return new SysNotifyDAL().FrameworkDelete(modelo);
        }

        public object FrameworkGetCustom(Model.Framework.Model modelo, string _customParam)
        {
            SysNotifyDAL dal = new SysNotifyDAL();
            dal.CustomWhere = _customParam;
            return dal.FrameworkGet(modelo);
        }

        public string GetLockup(Model.Framework.Model modelo, int type)
        {
            throw new NotImplementedException();
        }
    }
}
