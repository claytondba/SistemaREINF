using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL
{
    public class SysLogNotifyBLL: Framework.BLL<SysLogNotifyModel>, Framework.IBLL
    {
        public DataTable GetMetas(DateTime inicio, DateTime fim)
        {
            return new SysLogNotifyDAL().GetMetas(inicio, fim);
        }
    }
}
