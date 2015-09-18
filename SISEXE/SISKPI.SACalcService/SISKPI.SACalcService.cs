using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using Quartz;
using SISKPI.SACalcService.Job;
using System.Runtime.Remoting;
using System.Configuration;
using Quartz.Impl;

namespace SISKPI.SACalcService {

	partial class SISKPISACalcService : ServiceBase {

		IScheduler m_Scheduler;

		public SISKPISACalcService() {
			InitializeComponent();
			//String configFile = AppDomain.CurrentDomain.BaseDirectory + "SISKPI.SACalcService.exe.config";
			//RemotingConfiguration.Configure(configFile, false);		
			ISchedulerFactory sf = new StdSchedulerFactory();
			m_Scheduler = sf.GetScheduler();
			BuildJob();
		}

		protected override void OnStart(string[] args) {
			m_Scheduler.Start();
		}

		protected override void OnStop() {
			m_Scheduler.Shutdown();			
		}

		internal void BuildJob() {
			//DateTimeOffset date = DateTime.Now;
			//DateTimeOffset HourStartTime = DateBuilder.NextGivenSecondDate(date, 5);
			IJobDetail job;
			//ISimpleTrigger trigger;
			ICronTrigger CronTrigger;

			job = JobBuilder.Create<SACalcJob>()
					.WithIdentity("SafetyCalcJob", "SafetyCalcGroup")
					.Build();

			//每小时执行一次
			CronTrigger = (ICronTrigger)TriggerBuilder.Create()
														  .WithIdentity("SafetyCalcTrigger", "SafetyCalcGroup")
														  .WithCronSchedule("0 0 0/1 * * ?")
														  .Build();
			m_Scheduler.ScheduleJob(job, CronTrigger);

			//date = new DateTime(date.Year, date.Month, date.Day, date.Hour, 20, 0);
			//DateTimeOffset MonitorStartTime = new DateTime(date.Year, date.Month, date.Day, date.Hour, 20, 0);
			//统计监盘时间
			job = JobBuilder.Create<MonitorDurationJob>()
					.WithIdentity("MonitorDurationJob", "MonitorDurationGroup")
					.Build();

			//每30分钟执行一次
			CronTrigger = (ICronTrigger)TriggerBuilder.Create()
														  .WithIdentity("MonitorDurationTrigger", "MonitorDurationGroup")
														  .WithCronSchedule("0 0/30 * * * ?")
														  .Build();
			m_Scheduler.ScheduleJob(job, CronTrigger);

			//每天8时，16时执行
			job = JobBuilder.Create<ShiftSACalcJob>()
				.WithIdentity("MorningShift", "ShiftJob")
				.Build();

			CronTrigger = (ICronTrigger)TriggerBuilder.Create()
					  .WithIdentity("MorningTrigger", "ShiftTrigger")
					  .WithCronSchedule("0 0 8,16 * * ?")
					  .Build();
			m_Scheduler.ScheduleJob(job, CronTrigger);

			job = JobBuilder.Create<ShiftSACalcJob>()
				.WithIdentity("MorningShift1", "ShiftJob")
				.Build();

			//每天0时30分执行
			CronTrigger = (ICronTrigger)TriggerBuilder.Create()
					  .WithIdentity("MorningTrigger1", "ShiftTrigger")
					  .WithCronSchedule("0 30 0 * * ?")
					  .Build();
			m_Scheduler.ScheduleJob(job, CronTrigger);
		}
	}
}
