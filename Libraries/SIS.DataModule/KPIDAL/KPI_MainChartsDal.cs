using SIS.DataModule.DAL;
using SIS.DataModule.KPIEntity;
using SIS.DBControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SIS.DataModule.KPIDAL
{
    /// <summary>
    /// 主页图标查询 
    /// Create by zhaojun 2015-1-19
    /// </summary>
    public class KPI_MainChartsDal : DalBase<KPI_MainChartsEntity>
    {
        /// <summary>
        /// 各值得当月分统计
        /// </summary>
        /// <returns></returns>
        public DataTable GetMonthTotalScore()
        {
            string sql = @" SELECT  Shift ,
                                    SUM(ISNULL(TotalScore, 0)) TotalScore
                            FROM    KPI_ShiftScore
                            WHERE   CONVERT(VARCHAR(7), CheckDate, 120) = CONVERT(VARCHAR(7), DATEADD(MONTH,
                                                                                            -5, GETDATE()), 120)
                            GROUP BY Shift;";
            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }
        /// <summary>
        /// 各值当月监盘时间统计
        /// </summary>
        /// <returns></returns>
        public DataTable GetMonthDurationTotal()
        {
            string sql = @" SELECT  Shift ,
                                    SUM(ISNULL(Duration, 0)) Duration
                            FROM    KPI_MonitorDuration
                            WHERE   CONVERT(VARCHAR(7), CheckDate, 120) = CONVERT(VARCHAR(7), DATEADD(MONTH,
                                                                                          -5, GETDATE()), 120)
                            GROUP BY Shift;";
            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }
        /// <summary>
        /// 各值得当年分统计
        /// </summary>
        /// <returns></returns>
        public DataTable GetYearTotalScore()
        {
            string sql = @" SELECT  Shift ,
                                    SUM(ISNULL(TotalScore, 0)) TotalScore
                            FROM    KPI_ShiftScore
                            WHERE   CONVERT(VARCHAR(4), CheckDate, 120) = CONVERT(VARCHAR(4), DATEADD(MONTH,
                                                                                          -5, GETDATE()), 120)
                            GROUP BY Shift;";
            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }
        /// <summary>
        /// 各值当年监盘时间统计
        /// </summary>
        /// <returns></returns>
        public DataTable GetYearDurationTotal()
        {
            string sql = @" SELECT  Shift ,
                                    SUM(ISNULL(Duration, 0)) Duration
                            FROM    KPI_MonitorDuration
                            WHERE   CONVERT(VARCHAR(4), CheckDate, 120) = CONVERT(VARCHAR(4), DATEADD(MONTH,
                                                                                          -5, GETDATE()), 120)
                            GROUP BY Shift;";
            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }
    }
}
