using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using SIS.DataAccess;
using SIS.DataEntity;
using log4net;
using SIS.DBControl;

namespace SISKPI.AlarmService.Job {

    /// <summary>
    /// 各值监盘时长任务
    /// </summary>
    internal class MonitorDurationJob : IJob {

        private ILog m_Loger = LogHelper.Logger;

        public void Execute(IJobExecutionContext context) {
            MonitorDurationDal DataAccess = new MonitorDurationDal();
            List<KPI_UnitEntity> UnitList = KPI_UnitDal.GetAllEntity();
            foreach (KPI_UnitEntity Unit in UnitList) {
                if (String.IsNullOrEmpty(Unit.WorkID)) return;
                if (UnitIsRunning(Unit.UnitID)) {
                    m_Loger.InfoFormat("开始计算{0}操盘时间", Unit.UnitName);
                    MonitorDurationEntity MonitorDuration = new MonitorDurationEntity();
                    DateTime Now = DateTime.Now;
                    String strStartTime = "";
                    String strEndTime = "";
                    String Shift = "";
                    String Period = "";
                    String strCurrentMinute = Now.ToString("yyyy-MM-dd HH:mm:00");
                    KPI_WorkDal.GetShiftAndPeriod(Unit.WorkID, strCurrentMinute,
                        ref Shift, ref Period, ref strStartTime, ref strEndTime);
                    DateTime ShiftStartTime = Convert.ToDateTime(strStartTime);
                    DateTime ShiftEndTime = Convert.ToDateTime(strEndTime);
                    MonitorDuration.UnitID = Unit.UnitID;
                    MonitorDuration.Shift = Shift;
                    MonitorDuration.CheckDate = Now.Date;
                    MonitorDuration.Duration = GetDuration(ShiftStartTime, ShiftEndTime);
                    m_Loger.InfoFormat("{0}值值班开始时间{1}值班结束时间{2}当天操盘时长是{3}小时", Shift, ShiftStartTime, ShiftEndTime, MonitorDuration.Duration);
                    DataAccess.SaveMonitorDuration(MonitorDuration);
                    m_Loger.InfoFormat("{0}操盘时间计算结束", Unit.UnitName);
                }
            }
        }

        internal void RecalcMonitorDuration(DateTime CalcDate) {
            //CalcDate = CalcDate.AddMinutes(-2);
            MonitorDurationDal DataAccess = new MonitorDurationDal();
            List<KPI_UnitEntity> UnitList = KPI_UnitDal.GetAllEntity();
            foreach (KPI_UnitEntity Unit in UnitList) {
                if (String.IsNullOrEmpty(Unit.WorkID)) return;
                if (UnitIsRunning(Unit.UnitID)) {
                    m_Loger.InfoFormat(String.Format("开始计算{0}操盘时间", Unit.UnitName));
                    //m_Loger.InfoFormat("开始计算{0}操盘时间", Unit.UnitName);
                    MonitorDurationEntity MonitorDuration = new MonitorDurationEntity();
                    //DateTime Now = DateTime.Now;
                    String strStartTime = "";
                    String strEndTime = "";
                    String Shift = "";
                    String Period = "";
                    String strCurrentMinute = CalcDate.ToString("yyyy-MM-dd HH:mm:00");
                    KPI_WorkDal.GetShiftAndPeriod(Unit.WorkID, strCurrentMinute,
                        ref Shift, ref Period, ref strStartTime, ref strEndTime);
                    DateTime ShiftStartTime = Convert.ToDateTime(strStartTime);
                    DateTime ShiftEndTime = Convert.ToDateTime(strEndTime);
                    MonitorDuration.UnitID = Unit.UnitID;
                    MonitorDuration.Shift = Shift;
                    MonitorDuration.CheckDate = CalcDate.Date;
                    MonitorDuration.Duration = GetRecalcDuration(ShiftStartTime, ShiftEndTime, CalcDate);
                    m_Loger.InfoFormat(String.Format("{0}值值班开始时间{1}值班结束时间{2}当天操盘时长是{3}小时", Shift, ShiftStartTime, ShiftEndTime, MonitorDuration.Duration));
                    DataAccess.SaveMonitorDuration(MonitorDuration);
                    m_Loger.InfoFormat(String.Format("{0}操盘时间计算结束", Unit.UnitName));
                }
            }
        }

        /// <summary>
        /// 返回排班表中当前班的值班时长
        /// </summary>
        /// <returns></returns>
        private decimal GetDuration(DateTime ShiftStartTime, DateTime ShiftEndTime) {
            decimal Result = 0;
            try {
                DateTime Current = DateTime.Now.AddMinutes(10);
                decimal Hour = Current.Hour + Current.Minute / 60.0m;
                Result = 0;
                if ((Hour >= 1) && (Hour < 8)) Result = 7;
                if ((Hour >= 8) && (Hour < 16)) Result = 8;
                if ((Hour >= 16) && (Hour <= 23)) Result = 9;
            }
            catch (Exception ex) {
                m_Loger.InfoFormat("值班开始时间{0}值班结束时间{1}当天操盘时长是{2}小时",
                    ShiftStartTime.ToString("dd日HH时mm分ss秒"), ShiftEndTime.ToString("dd日HH时mm分ss秒"), Result);
                m_Loger.Error("计算操盘时长处发生错误", ex);
            }
            return Result;
        }

        /// <summary>
        /// 返回排班表中当前班的值班时长
        /// </summary>
        /// <returns></returns>
        private decimal GetRecalcDuration(DateTime ShiftStartTime, DateTime ShiftEndTime, DateTime RecalcDate) {
            decimal Result = 0;
            try {
                //DateTime Current = DateTime.Now.AddMinutes(10);
                decimal Hour = RecalcDate.Hour + RecalcDate.Minute / 60.0m;
                Result = 0;
                if ((Hour >= 1) && (Hour < 8)) Result = 7;
                if ((Hour >= 8) && (Hour < 16)) Result = 8;
                if ((Hour >= 16) && (Hour <= 23)) Result = 9;
            }
            catch (Exception ex) {
                m_Loger.InfoFormat("值班开始时间{0}值班结束时间{1}当天操盘时长是{2}小时",
                    ShiftStartTime.ToString("dd日HH时mm分ss秒"), ShiftEndTime.ToString("dd日HH时mm分ss秒"), Result);
                m_Loger.Error("计算操盘时长处发生错误", ex);
            }
            return Result;
        }

        private String GetWorkID() {
            String Result = "";
            List<KPI_UnitEntity> UnitList = KPI_UnitDal.GetAllEntity();
            if (UnitList.Count > 0) {
                Result = UnitList.First().WorkID;
            }
            return Result;
        }

        private bool UnitIsRunning(String UnitID) {
            //bool Result = false;
            //if (String.IsNullOrEmpty(UnitID)) return Result;
            //KPI_UnitEntity item = KPI_UnitDal.GetEntity(UnitID);
            //if (item != null) Result = ExpCurrentValue(item.UnitCondition) > 0;
            //return Result;           
            return true;
        }

        private Double ExpCurrentValue(String expression) {
            RTInterface RTDataAccess = DBAccess.GetRealTime();
            try {
                if (String.IsNullOrEmpty(expression)) return Double.MinValue;
                int pos = expression.LastIndexOf("'") - 1;
                if (pos <= 0) return double.MinValue;
                String tagName = expression.Substring(1, pos);
                double tagValue = RTDataAccess.GetSnapshotValue(tagName);
                String Token = expression.Substring(pos + 2, 1);
                pos = expression.IndexOf(Token) + 1;
                String Val = expression.Substring(pos, expression.Length - pos);
                double targetValue = Convert.ToDouble(Val);
                return tagValue - targetValue;
            }
            catch (Exception ex) {
                m_Loger.Error("解析机组运行条件处发生错误", ex);
                return double.MinValue;
            }
        }
    }
}
