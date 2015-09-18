using log4net;
using SIS.DataAccess;
using SIS.DataEntity;
using SIS.DBControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SISKPI.AlarmService.AlarmHand
{
    public abstract class AbstractOverLimitExecutor : IOverLimitExecutor
    {
        #region Fields

        private ILog m_Logger = LogHelper.Logger;
        private static Dictionary<string, bool> m_Cache = new Dictionary<string, bool>();

        private RTInterface m_DataAccess;
        protected RTInterface RTDataAccess
        {
            get { return m_DataAccess; }
        }

        #endregion

        #region Constructions

        public AbstractOverLimitExecutor()
        {
        }

        #endregion

        public void InitCalculate(RTInterface mdataAccess, OverLimitConfigEntity overLimitConfig, DateTime startDate, DateTime endDate)
        {
            m_DataAccess = mdataAccess;

            if (CheckUnitIsRunning(overLimitConfig.UnitID) == false) return;

            List<TagValue> tagAttributeList = RTDataAccess.GetHistoryDataList(overLimitConfig.TagCode, startDate, endDate);
            if (tagAttributeList == null || tagAttributeList.Count <= 0)
            {
                m_Logger.InfoFormat("读取测点{0}{1}信息失败!", overLimitConfig.TagCode, overLimitConfig.TagDesc);
                return;
            }
            tagAttributeList = tagAttributeList.OrderBy(p => p.TimeStamp).ToList<TagValue>();

            Calculate(overLimitConfig, tagAttributeList);
        }

        protected abstract void Calculate(OverLimitConfigEntity overLimitConfig, List<TagValue> tagAttributeList);

        #region Private Methods

        #region 数据库更新

        /// <summary>
        /// 启动报警，向KPI_OverLimitRecord表中添加数据
        /// </summary>
        /// <param name="faultConfiguration">报警配置信息</param>
        /// <param name="alarmType">报警类型 1=超高限，2=超高高限,3=超更高</param>
        /// <param name="alarmTime">开始报警时间</param>
        /// <param name="maxValue">超标极值</param>
        /// <param name="standardValue">标准值</param>
        protected void AddOrUpdateOverLimitRecordToDB(OverLimitConfigEntity faultConfiguration, int alarmType,
            DateTime alarmTime, double standardValue, double maxValue)
        {
            try
            {
                KPI_OverLimitRecordEntity entity = new KPI_OverLimitRecordEntity();
                entity.AlarmID = Guid.NewGuid() + "";
                entity.UnitID = faultConfiguration.UnitID;
                entity.TagID = faultConfiguration.TagName;
                entity.AlarmType = alarmType;
                entity.AlarmStartTime = alarmTime;
                entity.StandardValue = Convert.ToDecimal(standardValue);
                entity.Offset = Convert.ToDecimal(maxValue - standardValue);
                entity.Duration = 0;
                entity.AlarmValue = Convert.ToDecimal(maxValue);
                String Period = "";
                String Shift = "";
                GetShiftAndPeriod(faultConfiguration.UnitID, out Shift, out Period, alarmTime);
                entity.Shift = Shift;
                entity.Period = Period;

                using (KPI_OverLimitRecordDal DataAccess = new KPI_OverLimitRecordDal())
                {
                    if (DataAccess.ExistsOverLimitRecord(entity))
                    {
                        DataAccess.UpdateAlarmMaxValue(entity);
                        return;
                    }
                    DataAccess.AddOverLimitRecord(entity);
                }
            }
            catch (Exception ex)
            {
                m_Logger.InfoFormat("AddOrUpdateOverLimitRecordToDB安全指标超限报警计算错误，错误信息是:{0},调用栈信息是:{1}",
                    ex.Message, ex.StackTrace);
                m_Logger.Error(ex.Message, ex);
            }
        }

        /// <summary>
        /// 解除报警，更新KPI_OverLimitRecord表中数据
        /// </summary>
        /// <param name="faultConfiguration">报警配置信息</param>
        /// <param name="alarmType">报警类型 1=超高限，2=超高高限,3=超更高；</param>
        /// <param name="alarmTime">解除报警时间</param>
        protected void UpdateExistOverLimitRecordToDB(OverLimitConfigEntity faultConfiguration, int alarmType, DateTime alarmTime)
        {
            try
            {
                KPI_OverLimitRecordEntity entity = new KPI_OverLimitRecordEntity();
                entity.TagID = faultConfiguration.TagName;
                entity.AlarmEndTime = alarmTime;
                entity.AlarmType = alarmType;
                using (KPI_OverLimitRecordDal DataAccess = new KPI_OverLimitRecordDal())
                {
                    DataAccess.UpdateOverLimitRecord(entity);
                }
            }
            catch (Exception ex)
            {
                m_Logger.InfoFormat("UpdateExistOverLimitRecordToDB安全指标超限报警计算错误，错误信息是:{0},调用栈信息是:{1}",
                    ex.Message, ex.StackTrace);
                m_Logger.Error(ex.Message, ex);
            }
        }

        #endregion

        private bool CheckUnitIsRunning(String UnitID)
        {
            bool Result = false;
            //m_Logger.InfoFormat("机组编码{0}", UnitID);
            if (String.IsNullOrEmpty(UnitID)) return Result;
            if (m_Cache.ContainsKey(UnitID)) return m_Cache[UnitID];
            KPI_UnitEntity item = KPI_UnitDal.GetEntity(UnitID);
            if (item != null) Result = TryGetExpressionCurrentValue(item.UnitCondition) > 0;
            m_Cache.Add(UnitID, Result);
            m_Logger.InfoFormat("机组{0}的运行条件是:{1}", UnitID, item.UnitCondition);
            return Result;
        }

        /// <summary>
        /// 获取班次与值次
        /// </summary>
        /// <param name="UnitID">机组编码</param>
        /// <param name="Shift">当前值</param>
        /// <param name="Period">当前班</param>
        private void GetShiftAndPeriod(String UnitID, out String Shift, out String Period, DateTime CalcTime)
        {
            String strStartTime = "";
            String strEndTime = "";
            Shift = "";
            Period = "";
            KPI_UnitEntity Entity = KPI_UnitDal.GetEntity(UnitID);
            if (Entity == null) return;
            try
            {
                String strWorkID = Entity.WorkID;
                String strCurrentMinute = CalcTime.ToString("yyyy-MM-dd HH:mm:00");
                KPI_WorkDal.GetShiftAndPeriod(strWorkID, strCurrentMinute,
                    ref Shift, ref Period, ref strStartTime, ref strEndTime);
            }
            catch (Exception ex)
            {
                m_Logger.Error(ex.Message, ex);
            }
        }

        private Double TryGetExpressionCurrentValue(String expression)
        {
            try
            {
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
            catch
            {
                return double.MinValue;
            }
        }

        #endregion
    }
}
