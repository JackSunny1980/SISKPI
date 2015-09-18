using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using SIS.DataAccess;
using SIS.DataEntity;
using log4net;


namespace SISKPI.SACalcService.Job {

	/// <summary>
	/// 各值监盘时长任务
	/// </summary>
	internal class MonitorDurationJob:IJob {

		private readonly String WorkID = "48883e53-0647-47f1-98d6-fa6b4f8464cb";
		private ILog m_Log = LogHelper.Logger;

		public void Execute(IJobExecutionContext context) {
			MonitorDurationEntity MonitorDuration = new MonitorDurationEntity();
			DateTime Now = DateTime.Now;
			decimal Hours = Now.Hour + Now.Minute / 60.0m;			
			String strStartTime = "";
			String strEndTime = "";
			String Shift = "";
			String Period = "";
			String strCurrentMinute = Now.AddMinutes(-2).ToString("yyyy-MM-dd HH:mm:00");
			try {
				KPI_WorkDal.GetShiftAndPeriod(WorkID, strCurrentMinute,
					ref Shift, ref Period, ref strStartTime, ref strEndTime);
				DateTime ShiftStartTime = Convert.ToDateTime(strStartTime);
				DateTime CurrentTime = DateTime.Now;
				TimeSpan Span = CurrentTime - ShiftStartTime;
				MonitorDuration.Shift = Shift;
                MonitorDuration.CheckDate = Now.AddMinutes(-1);
				MonitorDuration.Duration = 0.5m;
				//if ((Hours > 16.5m) && (Hours <= 24.0m)) MonitorDuration.Duration = 7.5m;//SET @Duration=7.5
				//if ((Hours >= 0.0m) && (Hours <= 0.5m)) MonitorDuration.Duration = 0.5m;
                List<KPI_UnitEntity> Units = KPI_UnitDal.GetAllEntity();
                
				using (MonitorDurationDal DataAccess = new MonitorDurationDal()) {
                    foreach (KPI_UnitEntity Unit in Units) {
                        MonitorDuration.UnitID = Unit.UnitID;
                        DataAccess.SaveMonitorDuration(MonitorDuration);
                    }
				}
			}
			catch (Exception ex) {
				m_Log.Error("各值监盘时间计算错误", ex);
			}
		}
	}
}
