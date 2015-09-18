using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;
using Quartz;
using SISKPI.SACalcService.Job;
using Quartz.Impl;

namespace SISKPI.SACalcService {

	static class Program {
		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		static void Main() {
            //ServiceBase[] ServicesToRun;
            //ServicesToRun = new ServiceBase[] 
            //{ 
            //    new SISKPISACalcService() 
            //};
            //ServiceBase.Run(ServicesToRun);
			//BuildJob();
			//SACalc();
			//Microsoft.VisualBasic.Information.IsNumeric("ssssssss");
            ReCalc();
		}

        /// <summary>
        /// 重算7月份安全得分
        /// </summary>
        static void ReCalc() {
            SACalculate Calc = new SACalculate();
            DateTime StartDateTime = new DateTime(2014, 8, 1, 0, 30, 0);
            DateTime EndDateTime = StartDateTime.AddHours(8);
            DateTime EndDate = new DateTime(2014, 9, 1, 0, 30, 0);
            while (EndDateTime <= EndDate) {
                Console.WriteLine("开始时间：{0},结束时间:{1} ", StartDateTime, EndDateTime);
                Calc.Calculate(StartDateTime, EndDateTime);               
                StartDateTime = EndDateTime;
                EndDateTime = StartDateTime.AddHours(8);
                //Console.WriteLine("开始时间：{0},结束时间:{1} ",StartDateTime, EndDateTime);
            }
            //Console.Read();
        }
		static void SACalc() {
			SACalculate Calc = new SACalculate();
			DateTime StartDateTime = new DateTime(2014, 3, 1,0,0,0);
			DateTime EndDateTime = new DateTime(2014, 3, 14);
			//Calc.ArchiveCalculation(StartDateTime, EndDateTime);
			Calc.Calculation();
			Calc.CalcShiftSAScore();

			//for (int i = 0; i < 1000; i++) {
			//    Calc.Calculation();
			//}
			Console.ReadLine();
		}



		static void BuildJob() {
			ISchedulerFactory sf = new StdSchedulerFactory();
			IScheduler m_Scheduler = sf.GetScheduler();
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
			m_Scheduler.Start();
		}
	}
}
