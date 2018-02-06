using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Framework
{
    public interface IBLL
    {
        bool FrameworkInsert(Model.Framework.Model modelo);
        object FrameworkGet(Model.Framework.Model modelo);
        object FrameworkGetOne(Model.Framework.Model modelo, int recno);
        bool FrameworkUpdate(Model.Framework.Model modelo);
        bool FrameworkDelete(Model.Framework.Model modelo);
        object FrameworkGetCustom(Model.Framework.Model modelo, string _customParam);
        string GetLockup(Model.Framework.Model modelo, int type);
        List<Model.LockUpModel> FLockup(Model.Framework.Model classModule, string parteNome);
        //Model.Framework.Model FGet();
        //void GetUser(Model.Framework.Model user);
        //void FrameworkGetRef(Model.Framework.Model modelo, object lista);
    }
}
