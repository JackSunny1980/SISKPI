using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;

using SIS.Loger;
using SIS.Assistant.WS;

namespace SISKPI.Performance
{
    public partial class MainService : ServiceBase
    {
        //实时的使用 线程timer
        //运行实时绩效考核系统计时器
        System.Threading.Timer KPITimer;
		WS_KPIMainMethod m_KPIMainMethod = new WS_KPIMainMethod();
        //运行实时绩效考核系统
        int KRun = 0;
        public MainService()
        {
            InitializeComponent();
            //运行状态
            KRun = int.Parse(System.Configuration.ConfigurationManager.AppSettings["KRun"]);
        }

        public void KPIThreadMethod(Object state)
        {
            //运行实时绩效管理系统
            if (KRun == 1)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
				m_KPIMainMethod.KIsTest = int.Parse(System.Configuration.ConfigurationManager.AppSettings["KTest"]);
				m_KPIMainMethod.KPIAppRunForPerformance(true);
                timer.Stop();
                //记录内存容量
                double usedTime = timer.ElapsedMilliseconds / 1000; //秒
                double usedMemory = Process.GetCurrentProcess().WorkingSet64 / 1024.0 / 1024.0; //MB
                LogUtil.LogMessage("本次实时绩效考核完成！耗时: " + usedTime.ToString() + "秒, 内存: " + usedMemory.ToString());
            }            
        }
        

        protected override void OnStart(string[] args)
        {
            ////////////////////////////////////////////////////////////////////////////////////////
            //服务启动
            LogUtil.LogMessage("启动.运行实时绩效管理系统.实时服务");
            ////////////////////////////////////////////////////////////////////////////////////////
            //机组稳态判定
            if (KRun == 1)            {
                ///////////////////////////////////////////////////////////////////////////////////////
                //Timer
                LogUtil.LogMessage("服务运行周期：60秒！");
                //时间单位由 秒 转换为 毫秒
                int nInterval = 60 * 1000;
                KPITimer = new System.Threading.Timer(new System.Threading.TimerCallback(KPIThreadMethod), null, nInterval, nInterval);
            }
            else
            {
                LogUtil.LogMessage("运行实时绩效管理系统.不需要启动!");  
            }
                   
        }    

        protected override void OnStop()
        {
            if (KPITimer != null)
            {
                KPITimer.Change(System.Threading.Timeout.Infinite, 0);
            }
            LogUtil.LogMessage("停止.运行实时绩效管理系统.实时服务!");
        }
    }
}
