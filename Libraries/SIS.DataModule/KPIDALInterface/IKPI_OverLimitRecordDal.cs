using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIS.DataEntity;

namespace SIS.DataAccess
{
    public interface IKPI_OverLimitRecordDal {
        List<KPI_OverLimitRecordEntity> GetOverLimitRecordEntityList(DateTime startTime, DateTime endTime, int startIndex, int pageSize, out int totalCount);
        List<KPI_OverLimitRecordEntity> GetOverLimitRecordEntityList(String UnitID, String KPIID, String Shift, DateTime startTime, DateTime endTime, int PageIndex, int PageSize, out int totalCount);
        List<KPI_UnitEntity> GetUnitEntityList();
        List<KPI_OverLimitRecordEntity> SearchOverLimitRecord(int startIndex, int pageSize, string kpiName, string unityCode, String Shift, DateTime beginTime, DateTime endTime, out int totalCount);
        List<KPI_OverLimitRecordEntity> SearchOverLimitRecord(int startIndex, int pageSize, string kpiName, string unityCode, string beginTime, string endTime, bool isStatistics, ref int totalCount);
        List<KPI_OverLimitRecordEntity> GetOverLimitRecords(DateTime beginTime, DateTime endTime,
            String tagCode);
        List<KPI_OverLimitRecordEntity> GetOverLimitRecords(DateTime beginTime, DateTime endTime, String tagCode, int alarmType);
    }
}
