using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL
{
    public class SysReportsBLL : Framework.BLL<SysReports>, Framework.IBLL
    {
        public List<SysReports> GetReportsByForm(int codForm)
        {
            List<SysReports> reports =
                FrameworkGetCustom(new SysReports(), string.Format("formulario = {0}", codForm)) as List<SysReports>;

            foreach (var re in reports)
            {
                re.ReportData = new SysReportsDataBLL().FrameworkGetCustom
                    (new SysReportData(), string.Format("report = {0}", re.recno)) as List<SysReportData>;

                foreach (SysReportData rData in re.ReportData)
                {
                    rData.TableData = new SysReportsDAL().FrameworkGetTable(rData.sql_data);
                }

            }

            return reports;
        }
        public SysReports GetReportsByCod(int codReport)
        {
            SysReports reports = FrameworkGetOneModel(codReport);

            reports.ReportData = new SysReportsDataBLL().FrameworkGetCustom
                (new SysReportData(), string.Format("report = {0}", reports.recno)) as List<SysReportData>;

            foreach (SysReportData rData in reports.ReportData)
            {
                rData.TableData = new SysReportsDAL().FrameworkGetTable(rData.sql_data);
            }

            return reports;
        }


    }
}
