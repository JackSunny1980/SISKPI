using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.ServiceProcess;
using SIS.DataAccess;
using SIS.DataEntity;
using Quartz;
using SISKPI.AlarmService.Job;
using Quartz.Impl;

namespace SISKPI.AlarmService {

    public class Program {

        public static void Main(String[] args) {
            //ServiceBase[] ServicesToRun;
            //ServicesToRun = new ServiceBase[] 
            //{ 
            //    new SISKPIAlarmService() 
            //};
            //ServiceBase.Run(ServicesToRun);

            //BuildJob();
            ReCalcMonitorDuration();
        }


        private static void ProcessAlarm() {
            Console.WriteLine("开始计算超限报警");
            //String configFile = AppDomain.CurrentDomain.BaseDirectory + "SISKPI.AlarmService.exe.config";
            //RemotingConfiguration.Configure(configFile, false);
            AlarmProductor alarmProductor = new AlarmProductor();
            alarmProductor.AlarmInterval = 10;
            //Console.WriteLine(string.Format("{0}开始执行超限处理", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            alarmProductor.PorcessExceedLimit();
            Console.WriteLine("超限报警计算结束");
        }


        private static void ReCalcuateOverLimit() {
            DateTime StartDate = new DateTime(2014, 6, 1, 0, 0, 0);
            DateTime EndDate = new DateTime(2014, 7, 1, 0, 0, 0);
            AlarmProductor alarmProductor = new AlarmProductor();
            while (StartDate < EndDate) {
                Console.WriteLine(String.Format("计算开始时间{0} 结束时间{1} ",
                    StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    StartDate.AddMinutes(30).ToString("yyyy-MM-dd HH:mm:ss")));
                alarmProductor.ReCalcuateOverLimit(StartDate, StartDate.AddMinutes(30));
                StartDate = StartDate.AddMinutes(30);
            }
        }

        private static void ReCalcMonitorDuration() {
            DateTime StartDate = new DateTime(2015, 8, 1, 0, 0, 0);
            DateTime EndDate = new DateTime(2015, 8, 21, 0, 0, 0);
            MonitorDurationJob MonitorDuration = new MonitorDurationJob();
            while (StartDate < EndDate) {
                Console.WriteLine(String.Format("计算开始时间{0}操盘时间。 ",
                    StartDate.ToString("yyyy-MM-dd HH:mm:ss")));
                MonitorDuration.RecalcMonitorDuration(StartDate);
                StartDate = StartDate.AddHours(1);
            }

        }

        static void BuildJob() {
            ISchedulerFactory sf = new StdSchedulerFactory();
            IScheduler Scheduler = sf.GetScheduler();
            IJobDetail job;
            ICronTrigger trigger;

            job = JobBuilder.Create<AlarmJob>()
                    .WithIdentity("SafetyAlarmJob", "SafetyAlarmGroup")
                    .Build();

            //每10分钟执行一次
            trigger = (ICronTrigger)TriggerBuilder.Create()
                                                          .WithIdentity("SafetyAlarmTrigger", "SafetyAlarmGroup")
                                                          .WithCronSchedule("0 0/10 * * * ?")
                                                          .Build();
            Scheduler.ScheduleJob(job, trigger);

            //统计监盘时间
            job = JobBuilder.Create<MonitorDurationJob>()
                    .WithIdentity("MonitorDurationJob", "MonitorDurationJobGroup")
                    .Build();

            //每小时执行一次
            trigger = (ICronTrigger)TriggerBuilder.Create()
                      .WithIdentity("MonitorDurationTrigger", "MonitorDurationTriggerGroup")
                      .WithCronSchedule("0 0 * * * ?")
                      .Build();
            Scheduler.ScheduleJob(job, trigger);
            Scheduler.Start();
        }


    }
}
