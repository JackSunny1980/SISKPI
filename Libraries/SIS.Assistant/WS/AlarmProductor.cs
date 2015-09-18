using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIS.DBControl;
using SIS.DataAccess;
using SIS.DataEntity;
using SIS.Loger;

namespace SIS.Assistant.WS {

	/// <summary>
	/// 安全指标超限报警生成器
	/// </summary>
	public class AlarmProductor : IDisposable {
		private RTInterface m_DataAccess;//= DBAccess.GetRealTime();

		internal AlarmProductor() {
			m_DataAccess = DBAccess.GetRealTime();
		}

		internal void PorcessExceedLimit() {
			IKPI_OverLimitConfigDal dataAccess = new KPI_OverLimitConfigDal();
			try {
				m_DataAccess.Connection();
				//返回超限配置列表				
                List<OverLimitConfigEntity> faultConfigurations = dataAccess.GetOverLimitConfigs();
				DateTime startDate, endDate;
				endDate = DateTime.Now;
				endDate = endDate.AddSeconds(-1 * endDate.Second);
				startDate = endDate.AddMinutes(-1 * 10);
				List<TagValue> tagAttributeList;
				double high1, high2, high3;
                foreach (OverLimitConfigEntity faultConfiguration in faultConfigurations) {
					StopLastShiftAlarm(faultConfiguration);//停止上一班未结束的报警
					tagAttributeList = m_DataAccess.GetHistoryDataList(faultConfiguration.TagCode, startDate, endDate);
					high1 = double.MaxValue;
					high2 = double.MaxValue;
					high3 = double.MaxValue;

					if (tagAttributeList.Count <= 0) continue;
					if (faultConfiguration.FirstLimitingValue != null) high1 = Convert.ToDouble(faultConfiguration.FirstLimitingValue.Value);//高1限值
					if (faultConfiguration.SecondLimitingValue != null) high2 = Convert.ToDouble(faultConfiguration.SecondLimitingValue.Value);//高2限值
					if (faultConfiguration.ThirdLimitingValue != null) high3 = Convert.ToDouble(faultConfiguration.ThirdLimitingValue.Value);//高3限值

					//超高3
					var q = (from p in tagAttributeList where p.TagDoubleValue > high3 select p);
					if (q.Count() > 0) {
						H3(faultConfiguration, tagAttributeList, high3);
					}
					q = (from p in tagAttributeList where p.TagDoubleValue <= high3 select p);
					if (q.Count() > 0)
					 {
						StopAlarm(faultConfiguration, 3, tagAttributeList.First().TimeStamp);
					}
					//超高2
					q = (from p in tagAttributeList where p.TagDoubleValue > high2 && p.TagDoubleValue <= high3 select p);
					if (q.Count() > 0) {
						H2(faultConfiguration, tagAttributeList, high2, high3);
					}
					q = (from p in tagAttributeList where p.TagDoubleValue <= high2 select p);
					if (q.Count() > 0)
					{
						StopAlarm(faultConfiguration, 2, tagAttributeList.First().TimeStamp);
					}

					//超高1
					q = (from p in tagAttributeList where p.TagDoubleValue > high1 && p.TagDoubleValue <= high2 select p);
					if (q.Count() > 0) {
						H1(faultConfiguration, tagAttributeList, high1, high2);
					}
					q = (from p in tagAttributeList where p.TagDoubleValue <= high1 select p);
					if (q.Count() > 0) {
						StopAlarm(faultConfiguration, 1, tagAttributeList.First().TimeStamp);
					}					
				}
			}
			catch (Exception ex) {				
				LogUtil.LogMessage(string.Format("安全指标超限报警计算错误，错误信息是:{0},调用栈信息是:{1}",
					ex.Message,ex.StackTrace));			
			}
		}

		/// <summary>
		/// 超高三限
		/// </summary>
		/// <param name="faultConfiguration"></param>
		/// <param name="tagAttributeList"></param>
		/// <param name="standardValue"></param>
        private void H3(OverLimitConfigEntity faultConfiguration, List<TagValue> tagAttributeList, double standardValue) {
			double maxValue = tagAttributeList.Max(p => Convert.ToDouble(p.TagStringValue));
			foreach (TagValue item in tagAttributeList) {
				if (item.TagDoubleValue > standardValue) {
					StartAlarm(faultConfiguration, 3, item.TimeStamp, standardValue, maxValue);
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
				GetShiftAndPeriod(faultConfiguration.UnitID, out Shift, out Period);
				entity.Shift = Shift;
				entity.Period = Period;
				using (KPI_OverLimitRecordDal DataAccess = new KPI_OverLimitRecordDal()) {
					if (DataAccess.ExistsOverLimitRecord(entity)) return;
					DataAccess.AddOverLimitRecord(entity);
				}
			}
			catch (Exception ex) {
				LogUtil.LogMessage(string.Format("安全指标超限报警计算错误，错误信息是:{0},调用栈信息是:{1}", 
					ex.Message, ex.StackTrace));	
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
				entity.AlarmStartTime = alarmTime;
				entity.AlarmType = alarmType;
				using (KPI_OverLimitRecordDal DataAccess = new KPI_OverLimitRecordDal()) {
					DataAccess.UpdateOverLimitRecord(entity);
				}
			}
			catch (Exception ex) {
				LogUtil.LogMessage(string.Format("安全指标超限报警计算错误，错误信息是:{0},调用栈信息是:{1}", 
					ex.Message, ex.StackTrace));	
			}
		}

		/// <summary>
		/// 获取班次与值次
		/// </summary>
		/// <param name="UnitID">机组编码</param>
		/// <param name="Shift">当前值</param>
		/// <param name="Period">当前班</param>
		private void GetShiftAndPeriod(String UnitID, out String Shift, out String Period) {
			String strStartTime = "";
			String strEndTime = "";
			Shift = "";
			Period = "";
			KPI_UnitEntity Entity = KPI_UnitDal.GetEntity(UnitID);
			if (Entity == null) return;
			String strWorkID = Entity.WorkID;
			String strCurrentMinute = DateTime.Now.ToString("yyyy-MM-dd HH:mm:00");
			KPI_WorkDal.GetShiftAndPeriod(strWorkID, strCurrentMinute,
				ref Shift, ref Period, ref strStartTime, ref strEndTime);
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
					LogUtil.LogMessage(string.Format("安全指标超限报警计算错误，错误信息是:{0},调用栈信息是:{1}",
						ex.Message, ex.StackTrace));
				}
			}
		}
		public void Dispose() {
			m_DataAccess.Dispose();
			m_DataAccess = null;
		}
	}
}
