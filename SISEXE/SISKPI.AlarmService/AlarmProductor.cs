using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIS.DBControl;
using SIS.DataAccess;
using SIS.DataEntity;
using log4net;

namespace SISKPI.AlarmService {

    /// <summary>
    /// 安全指标超限报警生成器
    /// </summary>
    public class AlarmProductor : IDisposable {
        private RTInterface m_DataAccess;
        private ILog m_Logger = LogHelper.Logger;
        private Dictionary<String, bool> m_Cache;

        private RTInterface RTDataAccess {
            get {
                if (m_DataAccess == null) m_DataAccess = DBAccess.GetRealTime();
                return m_DataAccess;
            }
        }


        internal int AlarmInterval {
            get;
            set;
        }

        internal AlarmProductor() {
            m_DataAccess = DBAccess.GetRealTime();
            m_Cache = new Dictionary<string, bool>();
        }

        internal void PorcessExceedLimit() {
            IKPI_OverLimitConfigDal dataAccess = new KPI_OverLimitConfigDal();
            try {
                //返回超限配置列表				
                List<OverLimitConfigEntity> faultConfigurations = dataAccess.GetOverLimitConfigs();
                if (faultConfigurations == null) {
                    m_Logger.Info("数据库不存在超限配置");
                    return;
                }
                DateTime startDate, endDate;
                endDate = DateTime.Now;
                endDate = endDate.AddSeconds(-1 * endDate.Second);
                startDate = endDate.AddMinutes(-1 * AlarmInterval);
                //startDate = new DateTime(2013, 11, 28, 4, 30, 0);
                //endDate = new DateTime(2013, 11, 28, 4, 40, 0);
                List<TagValue> tagAttributeList;
                //faultConfigurations = faultConfigurations.Where(p => p.TagCode == @"\mjdc\DCS1\TE_31251F_PV").ToList<KPI_OverLimitConfig>();
                foreach (OverLimitConfigEntity faultConfiguration in faultConfigurations) {
                    //StopLastShiftAlarm(faultConfiguration);//停止上一班未结束的报警
                    if (UnitIsRunning(faultConfiguration.UnitID) == false) continue;
                    tagAttributeList = RTDataAccess.GetHistoryDataList(faultConfiguration.TagCode, startDate, endDate);
                    if (tagAttributeList == null) {
                        m_Logger.InfoFormat("读取测点{0}{1}信息失败!", faultConfiguration.TagCode, faultConfiguration.TagDesc);
                        continue;
                    }
                    tagAttributeList = tagAttributeList.OrderBy(p => p.TimeStamp).ToList<TagValue>();
                    //StopPreviousAlarm(faultConfiguration, tagAttributeList);//停止前一轮起报的报警
                    CalcAlarm(faultConfiguration, tagAttributeList);//计算报警
                }
            }
            catch (Exception ex) {
                m_Logger.InfoFormat("PorcessExceedLimit安全指标超限报警计算错误，错误信息是:{0},调用栈信息是:{1}",
                    ex.Message, ex.StackTrace);
                m_Logger.Error(ex);
            }
        }

        internal void ReCalcuateOverLimit(DateTime StartDate, DateTime EndDate) {
            IKPI_OverLimitConfigDal dataAccess = new KPI_OverLimitConfigDal();
            try {
                //返回超限配置列表				
                List<OverLimitConfigEntity> faultConfigurations = dataAccess.GetOverLimitConfigs();
                if (faultConfigurations == null) {
                    m_Logger.Info("数据库不存在超限配置");
                    return;
                }

                //endDate = DateTime.Now;
                //endDate = endDate.AddSeconds(-1 * endDate.Second);
                //startDate = endDate.AddMinutes(-1 * AlarmInterval);
                //startDate = new DateTime(2013, 11, 28, 4, 30, 0);
                //endDate = new DateTime(2013, 11, 28, 4, 40, 0);
                List<TagValue> tagAttributeList;
                //faultConfigurations = faultConfigurations.Where(p => p.TagCode == @"\mjdc\DCS1\TE_31251F_PV").ToList<KPI_OverLimitConfig>();
                foreach (OverLimitConfigEntity faultConfiguration in faultConfigurations) {
                    //StopLastShiftAlarm(faultConfiguration);//停止上一班未结束的报警
                    if (UnitIsRunning(faultConfiguration.UnitID) == false) continue;
                    tagAttributeList = RTDataAccess.GetHistoryDataList(faultConfiguration.TagCode, StartDate, EndDate);
                    if (tagAttributeList == null) {
                        m_Logger.InfoFormat("读取测点{0}{1}信息失败!", faultConfiguration.TagCode, faultConfiguration.TagDesc);
                        continue;
                    }
                    tagAttributeList = tagAttributeList.OrderBy(p => p.TimeStamp).ToList<TagValue>();
                    //StopPreviousAlarm(faultConfiguration, tagAttributeList);//停止前一轮起报的报警
                    CalcAlarm(faultConfiguration, tagAttributeList);//计算报警
                }
            }
            catch (Exception ex) {
                m_Logger.InfoFormat("PorcessExceedLimit安全指标超限报警计算错误，错误信息是:{0},调用栈信息是:{1}",
                    ex.Message, ex.StackTrace);
                m_Logger.Error(ex);
            }
        }

        private void CalcAlarm(OverLimitConfigEntity faultConfiguration, List<TagValue> tagAttributeList) {
            double high1, high2, high3, high4;
            double low1, low2, low3;
            high1 = double.MaxValue;
            high2 = double.MaxValue;
            high3 = double.MaxValue;
            high4 = double.MaxValue;
            low1 = double.MinValue;
            low2 = double.MinValue;
            low3 = double.MinValue;

            if (tagAttributeList.Count <= 0) return;
            if (faultConfiguration.LowLimit1Value != null) low1 = Convert.ToDouble(faultConfiguration.LowLimit1Value.Value);//低1限值
            if (faultConfiguration.LowLimit2Value != null) low2 = Convert.ToDouble(faultConfiguration.LowLimit2Value.Value);//低2限值
            if (faultConfiguration.LowLimit3Value != null) low3 = Convert.ToDouble(faultConfiguration.LowLimit3Value.Value);//低3限值

            if (faultConfiguration.FirstLimitingValue != null) high1 = Convert.ToDouble(faultConfiguration.FirstLimitingValue.Value);//高1限值
            if (faultConfiguration.SecondLimitingValue != null) high2 = Convert.ToDouble(faultConfiguration.SecondLimitingValue.Value);//高2限值
            if (faultConfiguration.ThirdLimitingValue != null) high3 = Convert.ToDouble(faultConfiguration.ThirdLimitingValue.Value);//高3限值
            if (faultConfiguration.FourthLimitingValue != null) high4 = Convert.ToDouble(faultConfiguration.FourthLimitingValue.Value);//高4限值

            if (high4 != double.MaxValue) H4(faultConfiguration, tagAttributeList, high4);
            if (high3 != double.MaxValue) H3(faultConfiguration, tagAttributeList, high3, high4);
            if (high2 != double.MaxValue) H2(faultConfiguration, tagAttributeList, high2, high3);
            if (high1 != double.MaxValue) H1(faultConfiguration, tagAttributeList, high1, high2);
            if (low1 != double.MinValue) L1(faultConfiguration, tagAttributeList, low2, low1);
            if (low2 != double.MinValue) L2(faultConfiguration, tagAttributeList, low3, low2);
            if (low3 != double.MinValue) L3(faultConfiguration, tagAttributeList, low3);

            //tagAttributeList = tagAttributeList.OrderBy(p => p.TimeStamp).ToList<TagValue>();					
            //超高4					
            /*var q = (from p in tagAttributeList where p.TagDoubleValue > high4 select p);
            if (q.Count() > 0) {
                H4(faultConfiguration, tagAttributeList, high4);
            }
            //超高3					
            q = (from p in tagAttributeList where p.TagDoubleValue > high3 && p.TagDoubleValue <= high4 select p);
            if (q.Count() > 0) {
                H3(faultConfiguration, tagAttributeList, high3, high4);
            }

            //超高2
            q = (from p in tagAttributeList where p.TagDoubleValue > high2 && p.TagDoubleValue <= high3 select p);
            if (q.Count() > 0) {
                H2(faultConfiguration, tagAttributeList, high2, high3);
            }

            //超高1
            q = (from p in tagAttributeList where p.TagDoubleValue > high1 && p.TagDoubleValue <= high2 select p);
            if (q.Count() > 0) {
                H1(faultConfiguration, tagAttributeList, high1, high2);
            }

            //超低1
            q = (from p in tagAttributeList where p.TagDoubleValue >= low2 && p.TagDoubleValue < low1 select p);
            if (q.Count() > 0) {
                L1(faultConfiguration, tagAttributeList, low2, low1);
            }

            //超低2
            q = (from p in tagAttributeList where p.TagDoubleValue >= low3 && p.TagDoubleValue < low2 select p);
            if (q.Count() > 0) {
                L2(faultConfiguration, tagAttributeList, low3, low2);
            }

            //超低3
            q = (from p in tagAttributeList where p.TagDoubleValue < low3 select p);
            if (q.Count() > 0) {
                L3(faultConfiguration, tagAttributeList, low3);
            }*/
        }

        private void H4(OverLimitConfigEntity faultConfiguration, List<TagValue> tagAttributeList, double standardValue) {
            double maxValue = tagAttributeList.Max(p => Convert.ToDouble(p.TagStringValue));
            foreach (TagValue item in tagAttributeList) {
                if (item.TagDoubleValue > standardValue) {
                    StartAlarm(faultConfiguration, 4, item.TimeStamp, standardValue, maxValue);
                }
                else {
                    StopAlarm(faultConfiguration, 4, item.TimeStamp);
                }
            }
        }

        private void H3(OverLimitConfigEntity faultConfiguration, List<TagValue> tagAttributeList,
            double lowValue, double hightValue) {
            double maxValue = tagAttributeList.Max(p => Convert.ToDouble(p.TagStringValue));
            foreach (TagValue item in tagAttributeList) {
                if ((item.TagDoubleValue > lowValue) && (item.TagDoubleValue <= hightValue)) {
                    StartAlarm(faultConfiguration, 3, item.TimeStamp, lowValue, maxValue);
                }
                else {
                    StopAlarm(faultConfiguration, 3, item.TimeStamp);
                }
            }
        }

        private void H2(OverLimitConfigEntity faultConfiguration, List<TagValue> tagAttributeList,
            double lowValue, double hightValue) {
            double maxValue = tagAttributeList.Max(p => Convert.ToDouble(p.TagStringValue));
            foreach (TagValue item in tagAttributeList) {
                if ((item.TagDoubleValue > lowValue) && (item.TagDoubleValue <= hightValue)) {
                    StartAlarm(faultConfiguration, 2, item.TimeStamp, lowValue, maxValue);
                }
                else {
                    StopAlarm(faultConfiguration, 2, item.TimeStamp);
                }
            }
        }

        private void H1(OverLimitConfigEntity faultConfiguration, List<TagValue> tagAttributeList,
            double lowValue, double hightValue) {
            double maxValue = tagAttributeList.Max(p => Convert.ToDouble(p.TagStringValue));
            foreach (TagValue item in tagAttributeList) {
                if ((item.TagDoubleValue > lowValue) && (item.TagDoubleValue <= hightValue)) {
                    StartAlarm(faultConfiguration, 1, item.TimeStamp, lowValue, maxValue);
                }
                else {
                    StopAlarm(faultConfiguration, 1, item.TimeStamp);
                }
            }
        }

        private void L1(OverLimitConfigEntity faultConfiguration, List<TagValue> tagAttributeList,
            double lowValue, double hightValue) {
            double minValue = tagAttributeList.Min(p => Convert.ToDouble(p.TagStringValue));
            foreach (TagValue item in tagAttributeList) {
                if ((item.TagDoubleValue < hightValue) && (item.TagDoubleValue >= lowValue)) {
                    StartAlarm(faultConfiguration, -1, item.TimeStamp, hightValue, minValue);
                }
                else {
                    StopAlarm(faultConfiguration, -1, item.TimeStamp);
                }
            }
        }

        private void L2(OverLimitConfigEntity faultConfiguration, List<TagValue> tagAttributeList,
            double lowValue, double hightValue) {
            double minValue = tagAttributeList.Min(p => Convert.ToDouble(p.TagStringValue));
            foreach (TagValue item in tagAttributeList) {
                if ((item.TagDoubleValue < hightValue) && (item.TagDoubleValue >= lowValue)) {
                    StartAlarm(faultConfiguration, -2, item.TimeStamp, hightValue, minValue);
                }
                else {
                    StopAlarm(faultConfiguration, -2, item.TimeStamp);
                }
            }
        }

        private void L3(OverLimitConfigEntity faultConfiguration, List<TagValue> tagAttributeList,
            double standardValue) {
            double minValue = tagAttributeList.Min(p => Convert.ToDouble(p.TagStringValue));
            foreach (TagValue item in tagAttributeList) {
                if (item.TagDoubleValue <= standardValue) {
                    StartAlarm(faultConfiguration, -3, item.TimeStamp, standardValue, minValue);
                }
                else {
                    StopAlarm(faultConfiguration, -3, item.TimeStamp);
                }
            }
        }

        /// <summary>
        /// 停止前一轮起报的报警
        /// </summary>
        private void StopPreviousAlarm(OverLimitConfigEntity faultConfiguration, List<TagValue> tagAttributeList) {
            double high1, high2, high3, high4;
            double low1, low2, low3;
            double FirstItemValue;
            high1 = double.MaxValue;
            high2 = double.MaxValue;
            high3 = double.MaxValue;
            high4 = double.MaxValue;
            low1 = double.MinValue;
            low2 = double.MinValue;
            low3 = double.MinValue;

            if (tagAttributeList.Count <= 0) return;
            if (faultConfiguration.LowLimit1Value != null) low1 = Convert.ToDouble(faultConfiguration.LowLimit1Value.Value);//低1限值
            if (faultConfiguration.LowLimit2Value != null) low2 = Convert.ToDouble(faultConfiguration.LowLimit2Value.Value);//低2限值
            if (faultConfiguration.LowLimit3Value != null) low3 = Convert.ToDouble(faultConfiguration.LowLimit3Value.Value);//低3限值

            if (faultConfiguration.FirstLimitingValue != null) high1 = Convert.ToDouble(faultConfiguration.FirstLimitingValue.Value);//高1限值
            if (faultConfiguration.SecondLimitingValue != null) high2 = Convert.ToDouble(faultConfiguration.SecondLimitingValue.Value);//高2限值
            if (faultConfiguration.ThirdLimitingValue != null) high3 = Convert.ToDouble(faultConfiguration.ThirdLimitingValue.Value);//高3限值
            if (faultConfiguration.FourthLimitingValue != null) high4 = Convert.ToDouble(faultConfiguration.FourthLimitingValue.Value);//高4限值
            FirstItemValue = tagAttributeList.First().TagDoubleValue;

            //停高4			
            if ((FirstItemValue <= high4) && (high4 != double.MaxValue)) {
                StopAlarm(faultConfiguration, 4, tagAttributeList.First().TimeStamp);
            }

            //停高3			
            if ((FirstItemValue <= high3) && (high3 != double.MaxValue)) {
                StopAlarm(faultConfiguration, 3, tagAttributeList.First().TimeStamp);
            }

            //停高2			
            if ((FirstItemValue <= high2) && (high2 != double.MaxValue)) {
                StopAlarm(faultConfiguration, 2, tagAttributeList.First().TimeStamp);
            }

            //停高1			
            if ((FirstItemValue <= high1) && (high1 != double.MaxValue)) {
                StopAlarm(faultConfiguration, 1, tagAttributeList.First().TimeStamp);
            }

            //停低1
            if ((FirstItemValue >= low1) && (low1 != double.MinValue)) {
                StopAlarm(faultConfiguration, -1, tagAttributeList.First().TimeStamp);
            }

            //停低2			
            if ((FirstItemValue >= low2) && (low2 != double.MinValue)) {
                StopAlarm(faultConfiguration, -2, tagAttributeList.First().TimeStamp);
            }

            //停低3			
            if ((FirstItemValue >= low3) && (low3 != double.MinValue)) {
                StopAlarm(faultConfiguration, -3, tagAttributeList.First().TimeStamp);
            }
        }

        /// <summary>
        /// 启动报警，向KPI_OverLimitRecord表中添加数据
        /// </summary>
        /// <param name="faultConfiguration">报警配置信息</param>
        /// <param name="alarmType">报警类型 1=超高限，2=超高高限,3=超更高</param>
        /// <param name="alarmTime">开始报警时间</param>
        /// <param name="maxValue">超标极值</param>
        /// <param name="standardValue">标准值</param>
        private void StartAlarm(OverLimitConfigEntity faultConfiguration, int alarmType,
            DateTime alarmTime, double standardValue, double maxValue) {
            //Console.WriteLine(String.Format("测点{0}的报警类型是:{1}报警值是:{2}", faultConfiguration.TagDesc, alarmType, maxValue));
            try {
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
                using (KPI_OverLimitRecordDal DataAccess = new KPI_OverLimitRecordDal()) {
                    if (DataAccess.ExistsOverLimitRecord(entity)) {
                        DataAccess.UpdateAlarmMaxValue(entity);
                        return;
                    }
                    DataAccess.AddOverLimitRecord(entity);
                }
            }
            catch (Exception ex) {
                m_Logger.InfoFormat("StartAlarm安全指标超限报警计算错误，错误信息是:{0},调用栈信息是:{1}",
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
        private void StopAlarm(OverLimitConfigEntity faultConfiguration, int alarmType, DateTime alarmTime) {
            try {
                KPI_OverLimitRecordEntity entity = new KPI_OverLimitRecordEntity();
                entity.TagID = faultConfiguration.TagName;
                entity.AlarmEndTime = alarmTime;
                entity.AlarmType = alarmType;
                using (KPI_OverLimitRecordDal DataAccess = new KPI_OverLimitRecordDal()) {
                    DataAccess.UpdateOverLimitRecord(entity);
                }
            }
            catch (Exception ex) {
                m_Logger.InfoFormat("StopAlarm安全指标超限报警计算错误，错误信息是:{0},调用栈信息是:{1}",
                    ex.Message, ex.StackTrace);
                m_Logger.Error(ex.Message, ex);
            }
        }

        /// <summary>
        /// 获取班次与值次
        /// </summary>
        /// <param name="UnitID">机组编码</param>
        /// <param name="Shift">当前值</param>
        /// <param name="Period">当前班</param>
        private void GetShiftAndPeriod(String UnitID, out String Shift, out String Period, DateTime CalcTime) {
            String strStartTime = "";
            String strEndTime = "";
            Shift = "";
            Period = "";
            KPI_UnitEntity Entity = KPI_UnitDal.GetEntity(UnitID);
            if (Entity == null) return;
            try {
                String strWorkID = Entity.WorkID;
                String strCurrentMinute = CalcTime.ToString("yyyy-MM-dd HH:mm:00");
                KPI_WorkDal.GetShiftAndPeriod(strWorkID, strCurrentMinute,
                    ref Shift, ref Period, ref strStartTime, ref strEndTime);
            }
            catch (Exception ex) {
                m_Logger.Error(ex.Message, ex);
            }
        }

        /// <summary>
        /// 交接时关闭上一班报警
        /// </summary>
        /// <param name="faultConfiguration"></param>
        private void StopLastShiftAlarm(OverLimitConfigEntity faultConfiguration) {
            KPI_UnitEntity Entity = KPI_UnitDal.GetEntity(faultConfiguration.UnitID);
            if (Entity == null) return;
            String strWorkID = Entity.WorkID;
            String strStartTime = "";
            String strEndTime = "";
            String Shift = "";
            String Period = "";
            String strCurrentMinute = DateTime.Now.ToString("yyyy-MM-dd HH:mm:00");
            KPI_WorkDal.GetShiftAndPeriod(strWorkID, strCurrentMinute,
                ref Shift, ref Period, ref strStartTime, ref strEndTime);
            DateTime ShiftStartTime = Convert.ToDateTime(strStartTime);
            DateTime CurrentTime = DateTime.Now;
            TimeSpan Span = CurrentTime - ShiftStartTime;
            double TotalMinutes = Span.TotalMinutes;
            if (TotalMinutes <= 10) {
                try {
                    using (KPI_OverLimitRecordDal DataAccess = new KPI_OverLimitRecordDal()) {
                        KPI_OverLimitRecordEntity entity = new KPI_OverLimitRecordEntity();
                        for (int i = 1; i < 4; i++) {
                            entity.TagID = faultConfiguration.TagName;
                            entity.AlarmStartTime = ShiftStartTime;
                            entity.AlarmType = i;
                            DataAccess.UpdateOverLimitRecord(entity);
                        }
                    }
                }
                catch (Exception ex) {
                    m_Logger.InfoFormat("StopLastShiftAlarm安全指标超限报警计算错误，错误信息是:{0},调用栈信息是:{1}",
                        ex.Message, ex.StackTrace);
                    m_Logger.Error(ex);
                }
            }
        }


        private bool UnitIsRunning(String UnitID) {
            bool Result = false;
            //m_Logger.InfoFormat("机组编码{0}", UnitID);
            if (String.IsNullOrEmpty(UnitID)) return Result;
            if (m_Cache.ContainsKey(UnitID)) return m_Cache[UnitID];
            KPI_UnitEntity item = KPI_UnitDal.GetEntity(UnitID);
            if (item != null) Result = ExpCurrentValue(item.UnitCondition) > 0;
            m_Cache.Add(UnitID, Result);
            m_Logger.InfoFormat("机组{0}的运行条件是:{1}", UnitID, item.UnitCondition);
            return Result;
        }


        private Double ExpCurrentValue(String expression) {
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
            catch {
                return double.MinValue;
            }
        }


        public void Dispose() {
            m_DataAccess.Dispose();
            m_DataAccess = null;
        }
    }
}
