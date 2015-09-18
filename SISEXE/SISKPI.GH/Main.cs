using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

using SIS.Loger;
using SIS.Assistant.WS;
 
namespace SISKPI.GH
{
    public partial class Main : Form
    {
        #region 变量
        //系统计时器
        System.Timers.Timer OPMTimer;

        //是否测试
        int RTest = 0;

        //是否自动运行
        int RAuto = 0;
        //是否自动隐藏
        bool RHide = false;

        //Excel表
        string strExcelName = "";
        string strExcel = "";
        string strSheet = "";

        //值际竞赛分析
        int RRun = 0;
        int RPeriod = 10;
        int ROffset = 5;
        int RSecond = 10;

        string RUnit = "";
        string RKPI = "";

        #endregion

        public Main() 
        {
            Main.CheckForIllegalCrossThreadCalls = false;

            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            btnStop.Enabled = false;

            //运行状态
            RTest = int.Parse(System.Configuration.ConfigurationManager.AppSettings["RTest"]);
            RAuto = int.Parse(System.Configuration.ConfigurationManager.AppSettings["RAuto"]);

            //Excel
            strExcelName = System.Configuration.ConfigurationManager.AppSettings["RExcel"];

            RRun = int.Parse(System.Configuration.ConfigurationManager.AppSettings["RRun"]);
            RPeriod = int.Parse(System.Configuration.ConfigurationManager.AppSettings["RPeriod"]);
            ROffset = int.Parse(System.Configuration.ConfigurationManager.AppSettings["ROffset"]);
            RSecond = int.Parse(System.Configuration.ConfigurationManager.AppSettings["RSecond"]);

            //////////////////////////////////////////////////////////////////////////
            //判断
            if (RPeriod <= 0)
            {
                RPeriod = 1;
            }

            if (ROffset < 0 || ROffset > RPeriod)
            {
                ROffset = 0;
            }

            if (RSecond < 0)
            {
                RSecond = 0;
            }

            //模拟启动并隐藏
            if (RAuto == 1)
            {
                this.btnStart.PerformClick();
                RHide = true;
            }
            
        }

        #region 界面按钮

        private void DisplayMenuItem_Click(object sender, EventArgs e)
        {
            RHide = false;

            this.Show();
            WindowState = FormWindowState.Normal;//窗体恢复正常

            notifyIcon1.Visible = true;
        }

        private void HideMenuItem_Click(object sender, EventArgs e)
        {
            RHide = true;

            this.Hide();
            WindowState = FormWindowState.Minimized;

            notifyIcon1.Visible = true;
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == System.Windows.Forms.MessageBox.Show("确认退出", "操作确认", MessageBoxButtons.YesNo))
            {    
                if (OPMTimer != null)
                {
                    //OPMTimer.Change(System.Threading.Timeout.Infinite, 0);
                    OPMTimer.Stop();
                }

                LogUtil.LogMessage("退出系统!");

                this.Dispose();

                this.Close();
            
            }

        }
        
        /// <summary>
        /// SISOPM 启动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            ////////////////////////////////////////////////////////////////////////////////////////         
            
            //
            if (RAuto==0 && System.Windows.Forms.MessageBox.Show("此界面只提供历史计算功能，实时计算由任务计划完成!", "国华绩效考核", MessageBoxButtons.OK) 
                == System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            if (!GetExcel())
            {
                return;
            }

            //服务启动
            LogUtil.LogMessage("启动系统");

            ///////////////////////////////////////////////////////////////////////////////////////
            //Timer

            //初始1秒执行: 秒 转换为 毫秒
            int nInterval = 1 * 1000;

            OPMTimer = new System.Timers.Timer(nInterval);
            OPMTimer.Elapsed += new System.Timers.ElapsedEventHandler(RaceThreadMethod);
            OPMTimer.AutoReset = true;  //设置是执行一次（false）还是一直执行(true)；
            //OPMTimer.Enabled = true;    //是否执行System.Timers.Timer.Elapsed事件；
            OPMTimer.Start();

            btnStart.Enabled = false;
            btnStop.Enabled = true;
        }

        /// <summary>
        /// SISOPM停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            if (OPMTimer != null)
            {
                //OPMTimer.Change(System.Threading.Timeout.Infinite, 0);
                OPMTimer.Stop();
            }

            btnStart.Enabled = true;
            btnStop.Enabled = false;

            LogUtil.LogMessage("停止系统!");
        }
        
        private void btnHide_Click(object sender, EventArgs e)
        {
            RHide = true;

            this.Hide();
            WindowState = FormWindowState.Minimized;

            notifyIcon1.Visible = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == System.Windows.Forms.MessageBox.Show("确认退出", "操作确认", MessageBoxButtons.YesNo))
            {
                if (OPMTimer != null)
                {
                    //OPMTimer.Change(System.Threading.Timeout.Infinite, 0);
                    OPMTimer.Stop();
                }

                LogUtil.LogMessage("退出系统!");

                this.Dispose();

                this.Close();

            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                RHide = false;

                this.Show();

                WindowState = FormWindowState.Normal;//窗体恢复正常
                notifyIcon1.Visible = true;
            }
        }

        #endregion

        #region 实时计算

        public void RaceThreadMethod(object source, System.Timers.ElapsedEventArgs e)
　　    {
            if (RHide)
            {
                this.Hide();
                WindowState = FormWindowState.Minimized;
            }

            //后期60秒执行: 秒 转换为 毫秒
            int nInterval = 6000 * 1000;
            OPMTimer.Interval = nInterval;

            //当前时间
            bool bCalc = false;
            DateTime dtCurrentTime = DateTime.Now;
            int nCS = dtCurrentTime.Second;
            int nCM = dtCurrentTime.Minute;
            int nCMmod =nCM%RPeriod;

            if(ROffset<=0 )
            {
                if (nCMmod == 0)
                {
                    bCalc = true;
                }
                else
                {
                    bCalc = false;
                }

            }else
            {
                if (nCMmod == ROffset)
                {
                    bCalc = true;
                }
                else
                {
                    bCalc = false;
                }
            }

            //不用计算，直接返回
            if (!bCalc)
            {
                //LogUtil.LogMessage("系统正常循环!");
                return;
            }

            //
            OPMTimer.Enabled = false;
            
            if (RRun == 1)
            {
                DateTime dtST = dtCurrentTime.AddMinutes(-1 * (RPeriod + ROffset)).AddSeconds(-1 * nCS);
                DateTime dtET = dtST.AddMinutes(RPeriod);

                if (strExcelName != "")
                {
                    Stopwatch timer = new Stopwatch();
                    timer.Start();

                    WS_ExcelMainMethod.ExcelSnapRun(strExcel, strSheet, dtET, RPeriod, RSecond);

                    timer.Stop();


                    LogUtil.LogMessage("本次统计完成！耗时: " + timer.ElapsedMilliseconds + "毫秒");
                }
            }

            OPMTimer.Enabled = true;

            return;      
　　    }        

        #endregion

        #region 历史补算
   
        /// <summary>
        /// 值际竞赛统计历史补算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRCalc_Click(object sender, EventArgs e)
        {
            if (!GetExcel())
            {
                return;
            }


            //历史补算
            string strUnit = System.Configuration.ConfigurationManager.AppSettings["RUnit"];
            string strKPI = System.Configuration.ConfigurationManager.AppSettings["RKPI"];
            
            string strHistory = System.Configuration.ConfigurationManager.AppSettings["RHistory"];
            string [] strTime = strHistory.Split(new string[] { ";", "," }, StringSplitOptions.RemoveEmptyEntries);

            //历史计算
            if (strTime.Length == 2)
            {
                if (DateTime.Parse(strTime[0]) > DateTime.Parse(strTime[1]))
                {
                    System.Windows.Forms.MessageBox.Show("开始时间不能小于结束时间!");

                    return;
                }

                if (DialogResult.Yes == System.Windows.Forms.MessageBox.Show("确认进行历史计算，\n开始时间：" + strTime[0] + "\n结束时间：" + strTime[1], "操作确认", MessageBoxButtons.YesNo))
                {
                    DateTime dtST = DateTime.Parse(strTime[0]);
                    DateTime dtET = DateTime.Parse(strTime[1]);

                    dtST = dtST.AddSeconds(dtST.Second * -1);
                    dtET = dtET.AddSeconds(dtET.Second * -1);

                    WS_ExcelMainMethod.ExcelArchiveRun(strExcel, strSheet, strUnit, strKPI, dtST, dtET, RPeriod, ROffset, RSecond);

                    System.Windows.Forms.MessageBox.Show("历史计算完成!");
                }

            }
            else
            {
                System.Windows.Forms.MessageBox.Show("时间配置错误!");
            }
            
        }
        
        #endregion
        
        /// <summary>
        ///  检查Excel表
        /// </summary>
        /// <returns></returns>
        private bool GetExcel()
        {
            try
            {
                string[] strArray = strExcelName.Split(new string[] { ";", "," }, StringSplitOptions.RemoveEmptyEntries);
                if (strArray.Length == 2)
                {
                    strExcel = strArray[0].Trim();
                    strSheet = strArray[1].Trim();
                }

                if (!File.Exists(strExcel))
                {
                    System.Windows.Forms.MessageBox.Show("Excel文件不存在");
                    return false;
                }

                return true;
            }
            catch (System.Exception ex)
            {
                LogUtil.LogMessage("Excel文件配置错误");

                System.Windows.Forms.MessageBox.Show("Excel文件配置错误");

                return false;
            }

        }

    }
}
