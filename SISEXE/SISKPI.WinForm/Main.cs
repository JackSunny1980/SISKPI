using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using SIS.Loger;
using SIS.Assistant.WS;
using System.Threading;

namespace SISKPI.WinForm {
	public partial class Main : Form {
		//实时绩效考核系统计时器
		private System.Timers.Timer KPITimer;
		private WS_KPIMainMethod m_KPICalculate = new WS_KPIMainMethod();
		private WS_RaceMainMethod m_RaceCalculate = new WS_RaceMainMethod();
		private bool IsStopCalculate = true;
		

		//是否测试
		private int KTest = 0;
		//是否自动运行
		private int KAuto = 0;
		//是否自动隐藏
		private bool KHide = false;
		//运行标志
		private int KRun = 0;
		private int RRun = 0;
		//值际竞赛周期
		//int RPeriod = 10;
		//程序循环次数，初始化为-1, RPeriod次数后自动清零.
		//int KCount = -1;

		public Main() {
			Main.CheckForIllegalCrossThreadCalls = false;

			InitializeComponent();
		}

		private void Main_Load(object sender, EventArgs e) {
			lblTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			btnStop.Enabled = false;
			//是否测试
			KTest = int.Parse(System.Configuration.ConfigurationManager.AppSettings["KTest"]);
			KAuto = int.Parse(System.Configuration.ConfigurationManager.AppSettings["KAuto"]);
			//运行状态
			KRun = int.Parse(System.Configuration.ConfigurationManager.AppSettings["KRun"]);
			RRun = int.Parse(System.Configuration.ConfigurationManager.AppSettings["RRun"]);

			//
			//RPeriod = int.Parse(System.Configuration.ConfigurationManager.AppSettings["RPeriod"]);

			//if (RPeriod < 5) {
			//    RPeriod = 5;
			//}

			//模拟启动并隐藏
			if (KAuto == 1) {
				KHide = true;
				IsStopCalculate = false;
				this.btnStart.PerformClick();
			}
		}

		#region 按钮

		private void DisplayMenuItem_Click(object sender, EventArgs e) {
			KHide = false;

			this.Show();
			this.WindowState = FormWindowState.Normal;//窗体恢复正常

			notifyIcon1.Visible = true;
		}

		private void HideMenuItem_Click(object sender, EventArgs e) {
			KHide = true;

			this.Hide();
			this.WindowState = FormWindowState.Minimized;

			notifyIcon1.Visible = true;
		}

		private void ExitMenuItem_Click(object sender, EventArgs e) {
			if (DialogResult.Yes == System.Windows.Forms.MessageBox.Show("确认退出", "操作确认", MessageBoxButtons.YesNo)) {
				if (KPITimer != null) {
					//OPMTimer.Change(System.Threading.Timeout.Infinite, 0);
					KPITimer.Stop();
				}
				LogUtil.LogMessage("退出系统!");
				this.Dispose();
				this.Close();
			}

		}



		/// <summary>
		/// SISKPI 启动
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnStart_Click(object sender, EventArgs e) {
			////////////////////////////////////////////////////////////////////////////////////////
			//服务启动
			LogUtil.LogMessage("启动运行实时绩效管理系统");
			///////////////////////////////////////////////////////////////////////////////////////
			//Timer
			//初始1秒执行: 秒 转换为 毫秒
			/*int nInterval = 60 * 1000;
			KPITimer = new System.Timers.Timer(nInterval);
			KPITimer.Elapsed += new System.Timers.ElapsedEventHandler(KPIThreadMethod);			
			KPITimer.AutoReset = true;  //设置是执行一次（false）还是一直执行(true)；
			//KPITimer.Enabled = true;    //是否执行System.Timers.Timer.Elapsed事件；
			KPITimer.Start();*/
			if (KRun == 1 || RRun == 1) {
				btnStart.Enabled = false;
				btnStop.Enabled = true;
			}
			IsStopCalculate = false;
			Thread t = new Thread(new ThreadStart(KPICalculate));
			t.Start();	
		}

		private void KPICalculate() {
			DateTime StartTime, EndTime;
			TimeSpan span;
			while (IsStopCalculate == false) {
				StartTime = DateTime.Now;
				m_KPICalculate.KPIAppRunForPerformance(true);
				EndTime = DateTime.Now;
				span = EndTime - StartTime;
				LogUtil.LogMessage(String.Format("{0} 本次实时绩效考核完成！耗时:{1}秒",
					DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), span.TotalSeconds));
				GC.Collect();
				Thread.Sleep(60000);
			}
		}


		/// <summary>
		/// SISKPI停止
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnStop_Click(object sender, EventArgs e) {
			/*if (KPITimer != null) {
				//KPITimer.Change(System.Threading.Timeout.Infinite, 0);
				KPITimer.Stop();
			}*/
			IsStopCalculate = true;//设置停止计算标志为真
			btnStart.Enabled = true;
			btnStop.Enabled = false;
			LogUtil.LogMessage("停止运行实时绩效管理系统!");
		}

		private void btnHide_Click(object sender, EventArgs e) {
			KHide = true;
			this.Hide();
			this.WindowState = FormWindowState.Minimized;
			notifyIcon1.Visible = true;
		}

		private void btnExit_Click(object sender, EventArgs e) {
			if (DialogResult.Yes == System.Windows.Forms.MessageBox.Show("确认退出", "操作确认", MessageBoxButtons.YesNo)) {
				if (KPITimer != null) {
					//OPMTimer.Change(System.Threading.Timeout.Infinite, 0);
					KPITimer.Stop();
				}
				LogUtil.LogMessage("退出运行实时绩效管理系统!");
				this.Dispose();
				this.Close();
			}
		}

		private void btnLog_Click(object sender, EventArgs e) {
			//System.Diagnostics.Process.Start("txt路径");

			//打开Word文档的代码
			//Word.Application appWord = new Word.Application();
			//Word.Document docWord = new Word.Document();

			//docWord = appWord.Documents.Open("L:\\SWPPP\\SWPPPBookMerge.doc");

			Process.Start("log\\log1.txt");
			//info.            

		}

		private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Left) {
				KHide = false;
				this.Show();
				this.WindowState = FormWindowState.Normal;//窗体恢复正常
				notifyIcon1.Visible = true;
			}
		}

		#endregion

		#region 实时运行绩效考核\值际竞赛系统

		public void KPIThreadMethod(object source, System.Timers.ElapsedEventArgs e) {
			//需要在构造函数中添加
			//Main.CheckForIllegalCrossThreadCalls = false;
			//if (KHide) {
			//    this.Hide();
			//    this.WindowState = FormWindowState.Minimized;
			//    notifyIcon1.Visible = true;
			//}

			//后期1分钟周期执行: 秒 转换为 毫秒
			//int nInterval = 60 * 1000;
			//KPITimer.Interval = nInterval;

			//////////////////////////////////////////////////
			//这样就会在每次执行完了才开始计时，所以不准确。
			//KPITimer.Enabled = false;

			/////////////////////////////////////////////////////////
			//KPI
			if (KRun == 1) {
				//Stopwatch timer = new Stopwatch();
				//timer.Start();
				m_KPICalculate.KIsTest = KTest;
				m_KPICalculate.KPIAppRunForPerformance(true);
				//timer.Stop();
				////记录内存容量
				//double usedTime = timer.ElapsedMilliseconds / 1000; //秒
				//double usedMemory = Process.GetCurrentProcess().WorkingSet64 / 1024.0 / 1024.0; //MB
				//LogUtil.LogMessage("本次实时绩效考核完成！耗时: " + usedTime.ToString() + "秒, 内存: " + usedMemory.ToString());

			}

			//////////////////////////////////////////////////////
			//Race
			//int RExcute = (KCount + 1) % RPeriod;
			//if (RRun == 1 && RExcute == 0) {
			//    KCount = 0;
			//    Stopwatch timer = new Stopwatch();
			//    timer.Start();
			//    m_RaceCalculate.RaceAppRun(true);
			//    timer.Stop();
			//    //记录内存容量
			//    double usedTime = timer.ElapsedMilliseconds / 1000; //秒
			//    double usedMemory = Process.GetCurrentProcess().WorkingSet64 / 1024.0 / 1024.0; //MB
			//    LogUtil.LogMessage("本次值际竞赛统计完成！耗时: " + usedTime.ToString() + "秒, 内存: " + usedMemory.ToString());
			//}
			//KCount += 1;
			//KPITimer.Enabled = true;
			return;
		}


		#endregion


		#region 值际竞赛系统

		public void RCEThreadMethod(Object state) {
			//需要在构造函数中添加
			//Main.CheckForIllegalCrossThreadCalls = false;
			if (KHide) {
				this.Hide();

				this.WindowState = FormWindowState.Minimized;
				notifyIcon1.Visible = true;
			}


			return;
		}

		#endregion
	}
}
