using System;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Configuration;
using log4net;
using Quartz.Impl;
using Quartz;
using SISKPI.AlarmService.Job;

namespace SISKPI.AlarmService {

	partial class SISKPIAlarmService : ServiceBase {

		IScheduler m_Scheduler;
		//private int m_AlarmInterval;	

		public SISKPIAlarmService() {
			InitializeComponent();			
			//m_AlarmInterval = Convert.ToInt32(ConfigurationManager.AppSettings.Get("AlarmInterval"));
			ISchedulerFactory sf = new StdSchedulerFactory();
			m_Scheduler = sf.GetScheduler();
			BuildJob();			
		}

		protected override void OnStop() {
			m_Scheduler.Shutdown();		
		}

		protected override void OnStart(string[] args) {
			base.OnStart(args);
			m_Scheduler.Start();
		}
		internal void BuildJob() {		
			IJobDetail job;			
			ICronTrigger trigger;

			job = JobBuilder.Create<AlarmJob>()
					.WithIdentity("SafetyAlarmJob", "SafetyAlarmGroup")
					.Build();

			
			trigger = (ICronTrigger)TriggerBuilder.Create()
														  .WithIdentity("Trigger1", "SafetyAlarmGroup")
                                                          .WithCronSchedule("0 0/10 * * * ?")
														  .Build();
			m_Scheduler.ScheduleJob(job, trigger);          


			//统计监盘时间
			job = JobBuilder.Create<MonitorDurationJob>()
					.WithIdentity("MonitorDurationJob", "MonitorDurationJobGroup")
					.Build();

			//每30分钟执行一次
			trigger = (ICronTrigger)TriggerBuilder.Create()
					  .WithIdentity("MonitorDurationTrigger", "MonitorDurationTriggerGroup")					 
					  .WithCronSchedule("1 0/30 * * * ?")
					  .Build();
			m_Scheduler.ScheduleJob(job, trigger);	
			
		}
	}
}
