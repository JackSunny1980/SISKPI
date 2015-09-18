using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Timers;

using SIS.Loger;
using SIS.Assistant.WS;
using System.Configuration;

namespace SISKPI.Recalculator {
	public partial class MainService : ServiceBase {		
		private Timer KPITimer;
		private WS_KPIMainMethod m_KPIMainMethod = new WS_KPIMainMethod();
		private int KRun = 0;//运行实时绩效考核系统		

		public MainService() {
			InitializeComponent();			
			KRun = int.Parse(ConfigurationManager.AppSettings["KRun"]);
		}
		

		private void KPIThreadMethod(object source, System.Timers.ElapsedEventArgs e) {			
			if (KRun == 1) {
				m_KPIMainMethod.KIsTest = int.Parse(ConfigurationManager.AppSettings["KTest"]);
				KPITimer.Enabled = false;
				m_KPIMainMethod.KPIAppRunForRecalculate();
				KPITimer.Enabled = true;			
			}
		}

		protected override void OnStart(string[] args) {
			////////////////////////////////////////////////////////////////////////////////////////
			//服务启动
			LogUtil.LogMessage("启动.运行实时绩效管理系统.历史服务");
			////////////////////////////////////////////////////////////////////////////////////////
			//机组稳态判定
			if (KRun == 1) {
				///////////////////////////////////////////////////////////////////////////////////////
				//Timer
				LogUtil.LogMessage("运行实时绩效管理系统.历史服务运行周期：60秒！");
				int nInterval = 60 * 1000;
				KPITimer = new System.Timers.Timer(nInterval);
				KPITimer.Elapsed += new System.Timers.ElapsedEventHandler(KPIThreadMethod);
				KPITimer.AutoReset = true;  //设置是执行一次（false）还是一直执行(true)；				
				KPITimer.Start();				
			}
			else {
				LogUtil.LogMessage("运行实时绩效管理系统.不需要启动!");
			}
		}

		protected override void OnStop() {
			if (KPITimer != null) {				
				KPITimer.Stop();
			}
			LogUtil.LogMessage("停止.运行实时绩效管理系统.历史服务!");
		}
	}
}
