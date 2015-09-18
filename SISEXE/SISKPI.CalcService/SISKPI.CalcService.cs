using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using SIS.Assistant.WS;
using System.Timers;
using System.Runtime.Remoting;
//using System.Threading;

namespace SISKPI.CalcService {

	public partial class SISKPICalcService : ServiceBase {	
	
		private WS_KPIMainMethod m_KPICalc;
		private Timer m_Timer;
		private Timer m_ReCalcTimer;

		public SISKPICalcService() {
			InitializeComponent();
			String configFile = AppDomain.CurrentDomain.BaseDirectory + "SISKPI.CalcService.exe.config";
			RemotingConfiguration.Configure(configFile, false);
			m_KPICalc = new WS_KPIMainMethod();
			m_Timer = new Timer();
			m_Timer.Interval = 60000;
			m_Timer.Elapsed += new ElapsedEventHandler(m_Timer_Elapsed);

			m_ReCalcTimer = new Timer();
			m_ReCalcTimer.Interval = 60000 * 30;
			m_ReCalcTimer.Elapsed += new ElapsedEventHandler(m_ReCalcTimer_Elapsed);
		}

		void m_ReCalcTimer_Elapsed(object sender, ElapsedEventArgs e) {
			m_KPICalc.KPIAppRunForRecalculate();//执行历史重算
			GC.Collect();
		}		

		protected override void OnStart(string[] args) {
			m_Timer.Enabled = true;
			m_Timer.Start();
			m_ReCalcTimer.Enabled = true;
			m_ReCalcTimer.Start();
		}

		private void m_Timer_Elapsed(object sender, ElapsedEventArgs e) {
			m_KPICalc.KPIAppRunForPerformance(true);
			GC.Collect();
		}

		protected override void OnStop() {
			m_Timer.Enabled = false;
			m_Timer.Start();	
		}
	}
}
