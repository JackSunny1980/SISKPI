using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIS.Arithmetic;
using log4net;
using SIS.DataEntity;
using SIS.DataAccess;
using SIS.DBControl;

namespace SISKPI.SACalcService {

	internal class SACalculate : IDisposable {

		#region 私有成员

		private Parser m_Parser;
		private ILog m_Log;
		private KPI_SATagValueDal m_DataAccess;
		private List<KPI_UnitEntity> m_UnitList;
		private Dictionary<String, bool> m_Cache;

		#endregion

		#region 属性

		public DateTime BeginTime {
			get;
			set;
		}

		public DateTime EndTime {
			get;
			set;
		}

		private bool IsCalcShift {
			get;
			set;
		}

		#endregion

		#region 构造器

		internal SACalculate() {
			m_Log = LogHelper.Logger;
			m_Parser = new Parser();
			m_DataAccess = new KPI_SATagValueDal();
			m_Cache = new Dictionary<string, bool>();
			Initialize();
		}

		#endregion

		#region 公共方法

        /// <summary>
        /// 临时用户处理对历史数据补算
        /// </summary>
        /// <param name="BeginTime"></param>
        /// <param name="EndTime"></param>
        public void Calculate(DateTime BeginTime, DateTime EndTime) {
            IsCalcShift = false;
            CalculateSAScore(BeginTime, EndTime);
            IsCalcShift = true;
            DateTime ShiftStartDateTime = BeginTime, ShiftEndDateTime = EndTime.AddMinutes(-1);
            CalculateSAScore(ShiftStartDateTime, ShiftEndDateTime);
        }

		/// <summary>
		/// 计算值得分处理先列情况下安全得分计算错误问题(
		/// 1.前一值值班期间起报在本值值班期间未解报；
		/// 2.本值值班期间起报但未解报；
		/// 3.前一值值班期间起报在本值值班期间解报；
		/// 4.超限时长超过1小时的报警记录）该函数在交接班时调度
		/// </summary>
		public void CalcShiftSAScore() {
			IsCalcShift = true;
			DateTime CurrentTime = DateTime.Now;
			DateTime ShiftEndDateTime = CurrentTime.AddSeconds(-1 * CurrentTime.Second);
			DateTime ShiftStartDateTime = ShiftEndDateTime.AddHours(-8);
			//m_Log.Info("开始计算值安全得分");
			//m_Log.InfoFormat("开始时间:{0} 结束时间:{1}", ShiftStartDateTime.ToString("yyyy-MM-dd HH:mm:ss"),
			//			ShiftEndDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
			CalculateSAScore(ShiftStartDateTime, ShiftEndDateTime);
			//m_Log.Info("值安全得分计算结束");
		}

		/// <summary>
		/// 计算安全实时得分
		/// </summary>
		public void Calculation() {
			//DateTime EndTime = DateTime.Now;
			String format = "开始时间:{0} 结束时间:{1}";
			IsCalcShift = false;
			DateTime EndTime = DateTime.Now;
			DateTime StartTime = EndTime.AddHours(-1);
			decimal End = EndTime.Hour + EndTime.Minute / 60.0m;
			decimal Start = StartTime.Hour + StartTime.Minute / 60.0m;
			List<KPI_PeriodEntity> list = KPI_PeriodDal.GetPeriodEntityList();
			KPI_PeriodEntity item = list.Where(p => ((End - p.PeriodEndHour) <= 8) &&
				((End - p.PeriodEndHour) >= 0) && (p.PeriodHours > 0)).First();
			decimal PeriodEndHour = item.PeriodEndHour;
			decimal hours = End - PeriodEndHour;
			if ((hours > 0) && (hours < 1.0m)) {
				//EndTime = DateTime.Now;
				StartTime = EndTime.AddHours(-1 * Convert.ToDouble(hours));
                //Console.WriteLine(String.Format(format, StartTime.ToString("yyyy-MM-dd HH:mm:ss"),
                //        EndTime.ToString("yyyy-MM-dd HH:mm:ss")));
				CalculateSAScore(StartTime, EndTime);//计算当前值安全得分

				//计算前值安全得分
				EndTime = StartTime;
				hours = 1.0m - hours;
				StartTime = EndTime.AddHours(-1 * Convert.ToDouble(hours));
                //Console.WriteLine(String.Format(format, StartTime.ToString("yyyy-MM-dd HH:mm:ss"),
                //        EndTime.ToString("yyyy-MM-dd HH:mm:ss")));
				CalculateSAScore(StartTime, EndTime);
			}
			if ((hours >= 1.0m) || ((hours <= 0) && (hours >= -0.5m))) {
                //Console.WriteLine(String.Format(format, StartTime.ToString("yyyy-MM-dd HH:mm:ss"),
                //        EndTime.ToString("yyyy-MM-dd HH:mm:ss")));
				CalculateSAScore(StartTime, EndTime);//计算当前值安全得分
			}
			//m_Log.Info("Calculation()被调用");
		}

		/// <summary>
		/// 计算安全历史得分
		/// </summary>
		/// <param name="StartDateTime">开始时间</param>
		/// <param name="EndDateTime">结束时间</param>
		public void ArchiveCalculation(DateTime StartDateTime, DateTime EndDateTime) {
			IsCalcShift = false;
			DateTime StartTime, EndTime;
			decimal StartHour, EndHour, PeriodEndHour, Hours;
			List<KPI_PeriodEntity> list = KPI_PeriodDal.GetPeriodEntityList();
			KPI_PeriodEntity item;
			//String format = "开始时间:{0} 结束时间:{1}";
			while (StartDateTime < EndDateTime) {
				StartTime = StartDateTime;
				EndTime = StartDateTime.AddHours(1);
				StartHour = StartTime.Hour + StartTime.Minute / 60.0m;
				EndHour = EndTime.Hour + EndTime.Minute / 60.0m;
				item = list.Where(p => ((EndHour - p.PeriodEndHour) <= 8.0m) &&
				((EndHour - p.PeriodEndHour) >= -0.5m) && (p.PeriodHours > 0)).First();
				PeriodEndHour = item.PeriodEndHour;
				Hours = EndHour - PeriodEndHour;
				if ((Hours > 0) && (Hours < 1.0m)) {
					StartTime = EndTime.AddHours(-1 * Convert.ToDouble(Hours));
                    //Console.WriteLine(String.Format(format, StartTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    //    EndTime.ToString("yyyy-MM-dd HH:mm:ss")));
					CalculateSAScore(StartTime, EndTime);//计算当前值安全得分

					//计算前值安全得分
					EndTime = StartTime;
					Hours = 1.0m - Hours;
					StartTime = EndTime.AddHours(-1 * Convert.ToDouble(Hours));
                    //Console.WriteLine(String.Format(format, StartTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    //    EndTime.ToString("yyyy-MM-dd HH:mm:ss")));
					CalculateSAScore(StartTime, EndTime);
				}
				if ((Hours >= 1.0m) || ((Hours <= 0) && (Hours >= -0.5m))) {
					CalculateSAScore(StartTime, EndTime);//计算当前值安全得分
                    //Console.WriteLine(String.Format(format, StartTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    //    EndTime.ToString("yyyy-MM-dd HH:mm:ss")));
				}
				StartDateTime = StartDateTime.AddHours(1);
			}
		}

		#endregion

		#region 私有方法

		private void CalculateSAScore(DateTime StartTime, DateTime EndTime) {
			KPI_SATagValueEntity SATagValue;
			List<KPI_SATagEntity> SATagList;
			this.BeginTime = StartTime; //开始时间			
			this.EndTime = EndTime;
			m_Log.Info("开始计算安全得分");
			m_Log.InfoFormat("开始时间:{0} 结束时间:{1}", BeginTime.ToString("yyyy-MM-dd HH:mm:ss"),
						EndTime.ToString("yyyy-MM-dd HH:mm:ss"));
			using (KPI_SATagDal DataAccess = new KPI_SATagDal()) {
				foreach (KPI_UnitEntity Unit in m_UnitList) {
					if (UnitIsRunning(Unit.UnitID) == false) continue;//机组停运不计算得分
					CurrentWorkInfo WorkInfo = GetWorkInfo(Unit.UnitID, BeginTime);//获取当前值次、班次相关信息
					SATagValue = new KPI_SATagValueEntity();
					SATagValue.Shift = WorkInfo.Shift;
					SATagValue.Period = WorkInfo.Period;
					SATagValue.CalcDateTime = EndTime;
					SATagList = DataAccess.GetSATagList(Unit.UnitID);
					foreach (KPI_SATagEntity SATag in SATagList) {
						SATagValue.SAID = SATag.SAID;
						SATagValue.SAScore = Convert.ToDecimal(m_Parser.Evaluate(SATag.SACalcExp));//计算安全得分
                        //Console.WriteLine(SATag.SACountExpression);
						SATagValue.TotalCount = Convert.ToInt32(m_Parser.Evaluate(SATag.SACountExpression));//计算超限次数
						SATagValue.TotalDuration = Convert.ToDecimal(m_Parser.Evaluate(SATag.SADurationExpression));//计算超限时长
						if (SATagValue.TotalCount > 0) m_DataAccess.SaveKPI_SATagValue(SATagValue);
					}
					SATagList.Clear();
				}
			}
            //Console.WriteLine("安全得分计算结束");
			m_Log.Info("安全得分计算结束");
		}

		private void Initialize() {
			using (KPI_UnitDal DataAccess = new KPI_UnitDal()) {
				m_UnitList = DataAccess.GetUnitIDs(string.Empty);
			}
			m_Parser.CustomFunction += new FunctionHandler(Parser_CustomFunction);
			m_Parser.OnError += new ErrorHandler(Parser_OnError);
			m_Parser.AddCustomFunction("SACOUNT", 1);//超限次数统计
			m_Parser.AddCustomFunction("SACOUNTBYTYPE", 2);//根据超限类型统计超限次数
			m_Parser.AddCustomFunction("SALONGCOUNT", 2);//超限次数统计（连续超限时长超过指定时长为超限1次)
			m_Parser.AddCustomFunction("SALONGCOUNTBYTYPE", 3);//根据超限类型统计超限次数（连续超限时长超过指定时长为超限1次)
			m_Parser.AddCustomFunction("SADURATION", 1);
			m_Parser.AddCustomFunction("SADURATIONBYTYPE", 2);
			m_Parser.AddCustomFunction("SALONGDURATION", 2);
			m_Parser.AddCustomFunction("SALONGDURATIONBYTYPE", 3);
		}

		private void Parser_OnError(object sender, ErrorEventArgs e) {
			m_Log.InfoFormat("表达式{0}存在错误，错误信息{1}", e.Expression, e.ErrorMessage);
			//e.ErrorMessage
		}

		private void Parser_CustomFunction(object sender, FunctionEventArgs e) {
			string FunctionName = e.FunctionName;
			if (e.FunctionName == "SACOUNT") {
				string Tags = m_Parser.GetStringParameter(0);
				double Result = SACount(Tags);
				m_Parser.CFSetResult(Result);
			}

			if (e.FunctionName == "SALONGCOUNT") {
				string Tags = m_Parser.GetStringParameter(0);
				double Duration = m_Parser.GetDoubleParameter(1);
				double Result = SALongCount(Tags, Duration);
				m_Parser.CFSetResult(Result);
			}

			if (e.FunctionName == "SACOUNTBYTYPE") {
				string Tags = m_Parser.GetStringParameter(0);
				int AlarmType = Convert.ToInt32(m_Parser.GetDoubleParameter(1));
				double Result = SACountByType(Tags, AlarmType);
				m_Parser.CFSetResult(Result);
			}

			if (e.FunctionName == "SALONGCOUNTBYTYPE") {
				string Tags = m_Parser.GetStringParameter(0);
				double Duration = m_Parser.GetDoubleParameter(1);
				int AlarmType = Convert.ToInt32(m_Parser.GetDoubleParameter(2));
				double Result = SALongCountByType(Tags, Duration, AlarmType);
				m_Parser.CFSetResult(Result);
			}

			if (e.FunctionName == "SADURATION") {
				string Tags = m_Parser.GetStringParameter(0);
				double Result = SADuration(Tags);
				m_Parser.CFSetResult(Result);
			}

			if (e.FunctionName == "SADURATIONBYTYPE") {
				string Tags = m_Parser.GetStringParameter(0);
				int AlarmType = Convert.ToInt32(m_Parser.GetDoubleParameter(1));
				double Result = SADurationByType(Tags, AlarmType);
				m_Parser.CFSetResult(Result);
			}

			if (e.FunctionName == "SALONGDURATION") {
				string Tags = m_Parser.GetStringParameter(0);
				double Duration = m_Parser.GetDoubleParameter(1);
				double Result = SALongDuration(Tags, Duration);
				m_Parser.CFSetResult(Result);
			}

			if (e.FunctionName == "SALONGDURATIONBYTYPE") {
				string Tags = m_Parser.GetStringParameter(0);
				int AlarmType = Convert.ToInt32(m_Parser.GetDoubleParameter(1));
				double Duration = m_Parser.GetDoubleParameter(2);
				double Result = SALongDurationByType(Tags, AlarmType, Duration);
				m_Parser.CFSetResult(Result);
			}
		}

		private double SACount(string tags) {
			double Result = 0.0f;
			String[] Tags = tags.Split(',');
			List<int> Durations = new List<int>();
			using (KPI_OverLimitRecordDal DataAccess = new KPI_OverLimitRecordDal()) {
				foreach (String TagID in Tags) {
					if (!IsCalcShift) Durations.Add(DataAccess.GetOverLimitRecords(BeginTime, EndTime, TagID).Count());
					if (IsCalcShift) Durations.Add(DataAccess.GetShiftOverLimitRecords(BeginTime, EndTime, TagID).Count());
				}
			}
			Result = Durations.Sum();
			return Result;
		}

		private double SACountByType(string tags, int alarmType) {
			double Result = 0.0f;
			String[] Tags = tags.Split(',');
			List<int> Durations = new List<int>();
			using (KPI_OverLimitRecordDal DataAccess = new KPI_OverLimitRecordDal()) {
				foreach (String TagID in Tags) {
					if (!IsCalcShift) Durations.Add(DataAccess.GetOverLimitRecords(BeginTime, EndTime, TagID, alarmType).Count());
					if (IsCalcShift) Durations.Add(DataAccess.GetShiftOverLimitRecords(BeginTime, EndTime, TagID, alarmType).Count());
				}
			}
			Result = Durations.Sum();
			return Result;
		}

		private double SALongCount(string tags, double alarmDuration) {
			//double Result = 0.0f;
			String[] Tags = tags.Split(',');
			List<int> Counts = new List<int>();
			int Count;
			/*List<int?> Durations = new List<int?>();
			int? Duration = 0;
			using (KPI_OverLimitRecordDal DataAccess = new KPI_OverLimitRecordDal()) {
				foreach (String TagID in Tags) {
					Duration = (from p in DataAccess.GetOverLimitRecords(BeginTime, EndTime, TagID)
								where p.Duration >= alarmDuration
								select p.Duration).Sum();
					Durations.Add(Duration);
				}
			}
			foreach (int? duration in Durations) {
				if (duration.HasValue) Result += duration.Value / alarmDuration;
			}*/
			List<KPI_OverLimitRecordEntity> OverRecords;
			using (KPI_OverLimitRecordDal DataAccess = new KPI_OverLimitRecordDal()) {
				foreach (String TagID in Tags) {
					if (!IsCalcShift) {
						OverRecords = DataAccess.GetOverLimitRecords(BeginTime, EndTime, TagID);
						Count = (from p in OverRecords
								 where p.Duration >= alarmDuration
								 select p.Duration).Count();
						Counts.Add(Count);
					}
					if (IsCalcShift) {
						OverRecords = DataAccess.GetShiftOverLimitRecords(BeginTime, EndTime, TagID);
						Count = (from p in OverRecords
								 where p.Duration >= alarmDuration
								 select p.Duration).Count();
						Counts.Add(Count);
					}
				}
			}
			return Counts.Sum();
		}

		private double SALongCountByType(string tags, double alarmDuration, int alarmType) {
			//double Result = 0.0f;
			String[] Tags = tags.Split(',');
			List<int> Counts = new List<int>();
			int Count;
			/*List<int?> Durations = new List<int?>();
			int? Duration = 0;
			using (KPI_OverLimitRecordDal DataAccess = new KPI_OverLimitRecordDal()) {
				foreach (String TagID in Tags) {
					Duration = (from p in DataAccess.GetOverLimitRecords(BeginTime, EndTime, TagID, alarmType)
								where p.Duration >= alarmDuration
								select p.Duration).Sum();
					Durations.Add(Duration);
				}
			}
			foreach (int? duration in Durations) {
				if (duration.HasValue) Result += duration.Value / alarmDuration;
			}*/
			using (KPI_OverLimitRecordDal DataAccess = new KPI_OverLimitRecordDal()) {
				foreach (String TagID in Tags) {
					if (!IsCalcShift) {
						Count = (from p in DataAccess.GetOverLimitRecords(BeginTime, EndTime, TagID, alarmType)
								 where p.Duration >= alarmDuration
								 select p.Duration).Count();
						Counts.Add(Count);
					}
					if (IsCalcShift) {
						Count = (from p in DataAccess.GetShiftOverLimitRecords(BeginTime, EndTime, TagID, alarmType)
								 where p.Duration >= alarmDuration
								 select p.Duration).Count();
						Counts.Add(Count);
					}
				}
			}
			return Counts.Sum();
		}

		private double SADuration(String tags) {
			double Result = 0.0f;
			String[] Tags = tags.Split(',');
			List<int?> Durations = new List<int?>();
			using (KPI_OverLimitRecordDal DataAccess = new KPI_OverLimitRecordDal()) {
				foreach (String TagID in Tags) {
					if (!IsCalcShift) Durations.Add(DataAccess.GetOverLimitRecords(BeginTime, EndTime, TagID).Sum(p => p.Duration));
					if (IsCalcShift) Durations.Add(DataAccess.GetShiftOverLimitRecords(BeginTime, EndTime, TagID).Sum(p => p.Duration));
				}
			}
			Result = Convert.ToDouble(Durations.Sum());//超限时长单位：秒
			Result = Math.Ceiling(Result / 60);//超限时长转换为分钟
			return Result;
		}

		private double SADurationByType(String tags, int alarmType) {
			double Result = 0.0f;
			String[] Tags = tags.Split(',');
			List<int?> Durations = new List<int?>();
			using (KPI_OverLimitRecordDal DataAccess = new KPI_OverLimitRecordDal()) {
				foreach (String TagID in Tags) {
					if (!IsCalcShift) Durations.Add(DataAccess.GetOverLimitRecords(BeginTime, EndTime, TagID, alarmType).Sum(p => p.Duration));
					if (IsCalcShift) Durations.Add(DataAccess.GetShiftOverLimitRecords(BeginTime, EndTime, TagID, alarmType).Sum(p => p.Duration));
				}
			}
			Result = Convert.ToDouble(Durations.Sum());//超限时长单位：秒
			Result = Math.Ceiling(Result / 60);//超限时长转换为分钟
			return Result;
		}

		private double SALongDuration(String tags, double alarmDuration) {
			double Result = 0.0f;
			String[] Tags = tags.Split(',');
			List<int?> Durations = new List<int?>();
			int? Duration = 0;
			using (KPI_OverLimitRecordDal DataAccess = new KPI_OverLimitRecordDal()) {
				foreach (String TagID in Tags) {
					if (!IsCalcShift) {
						Duration = (from p in DataAccess.GetOverLimitRecords(BeginTime, EndTime, TagID)
									where p.Duration >= alarmDuration
									select p.Duration).Sum();
						Durations.Add(Duration);
					}
					if (IsCalcShift) {
						Duration = (from p in DataAccess.GetShiftOverLimitRecords(BeginTime, EndTime, TagID)
									where p.Duration >= alarmDuration
									select p.Duration).Sum();
						Durations.Add(Duration);
					}
				}
			}
			Result = Convert.ToDouble(Durations.Sum());//超限时长单位：秒
			Result = Result / 60;//超限时长转换为分钟
			return Result;
		}

		private double SALongDurationByType(String tags, int alarmType, double alarmDuration) {
			double Result = 0.0f;
			String[] Tags = tags.Split(',');
			List<int?> Durations = new List<int?>();
			int? Duration = 0;
			using (KPI_OverLimitRecordDal DataAccess = new KPI_OverLimitRecordDal()) {
				foreach (String TagID in Tags) {
					if (!IsCalcShift) {
						Duration = (from p in DataAccess.GetOverLimitRecords(BeginTime, EndTime, TagID, alarmType)
									where p.Duration >= alarmDuration
									select p.Duration).Sum();
						Durations.Add(Duration);
					}
					if (IsCalcShift) {
						Duration = (from p in DataAccess.GetShiftOverLimitRecords(BeginTime, EndTime, TagID, alarmType)
									where p.Duration >= alarmDuration
									select p.Duration).Sum();
						Durations.Add(Duration);
					}
				}
			}
			Result = Convert.ToDouble(Durations.Sum());//超限时长单位：秒
			Result = Result / 60;//超限时长转换为分钟
			return Result;
		}

		private CurrentWorkInfo GetWorkInfo(String unitID, DateTime CalcDateTime) {
			CurrentWorkInfo Result = new CurrentWorkInfo();
			KPI_UnitEntity Entity = KPI_UnitDal.GetEntity(unitID);
			Result.WorkID = Entity.WorkID;
			String strStartTime = "";
			String strEndTime = "";
			String Shift = "";
			String Period = "";
			String strCurrentMinute = CalcDateTime.ToString("yyyy-MM-dd HH:mm:00");
			KPI_WorkDal.GetShiftAndPeriod(Result.WorkID, strCurrentMinute,
				ref Shift, ref Period, ref strStartTime, ref strEndTime);
			Result.Shift = Shift;
			Result.Period = Period;
			Result.StartTime = Convert.ToDateTime(strStartTime);
			Result.EndTime = Convert.ToDateTime(strEndTime);
			return Result;
		}


		private bool UnitIsRunning(String UnitID) {
			bool Result = false;
			//m_Logger.InfoFormat("机组编码{0}", UnitID);
			if (String.IsNullOrEmpty(UnitID)) return Result;
			if (m_Cache.ContainsKey(UnitID)) return m_Cache[UnitID];
			KPI_UnitEntity item = KPI_UnitDal.GetEntity(UnitID);
			if (item != null) Result = ExpCurrentValue(item.UnitCondition) > 0;
			m_Cache.Add(UnitID, Result);
			m_Log.InfoFormat("机组{0}的运行条件是:{1}", UnitID, item.UnitCondition);
			return Result;			
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
			catch {
				return double.MinValue;
			}
		}

		#endregion

		#region Dispose

		public void Dispose() {
			m_DataAccess.Dispose();
		}

		#endregion

		/// <summary>
		/// 当前班组工作信息
		/// </summary>
		internal class CurrentWorkInfo {

			/// <summary>
			/// 排班ID
			/// </summary>
			internal String WorkID {
				get;
				set;
			}

			/// <summary>
			/// 值开始时间
			/// </summary>
			internal DateTime StartTime {
				get;
				set;
			}

			/// <summary>
			/// 值结束时间
			/// </summary>
			internal DateTime EndTime {
				get;
				set;
			}

			/// <summary>
			/// 值次
			/// </summary>
			internal String Shift {
				get;
				set;
			}

			/// <summary>
			/// 班次
			/// </summary>
			internal String Period {
				get;
				set;
			}
		}

	}
}
