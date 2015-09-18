using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using SIS.Exceler;
using SIS.Loger;
using SIS.DataAccess;
using SIS.DBControl;
using SIS.Evaluator;
using SIS.Assistant.WS;


namespace SISKPI.GHInt
{
    class Console
    {
        #region 变量

        //是否测试
        private static int RTest = 0;

        //是否自动运行
        private static int RAuto = 0;
        //是否自动隐藏
        private static bool RHide = false;

        //Excel表
        private static string strExcelName = "";
        private static string strExcel = "";
        private static string strSheet = "";

        //值际竞赛分析
        private static int RRun = 0;
        private static int RPeriod = 10;
        private static int ROffset = 5;
        private static int RSecond = 10;


        #endregion

        static void Main(string[] args)
        {
            /////////////////////////////////////////////////////////////////////////////
            #region Read INI Config
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

            #endregion

            ////////////////////////////////////////////////////////////////////////////

            /////////////////////////////////////////////////////////////////////////////
            #region 用户交互对话执行

            if (args.Length == 0)
            {
                do
                {
                    System.Console.WriteLine("-------------------");
                    System.Console.WriteLine("请输入运行类型:");
                    System.Console.WriteLine("[0] - 退出, 默认;!");
                    System.Console.WriteLine("[1] - 数据库读数测试;");
                    //
                    //
                    //System.Console.WriteLine("[9] - 报表补算;");
                    System.Console.WriteLine("-------------------");

                    string strinput = System.Console.ReadLine();

                    if (strinput == "")
                    {
                        strinput = "0";
                    }

                    if (strinput.ToLower() == "0")
                    {
                        return;

                    }
                    else if (strinput.ToLower() == "1")
                    {
                        #region 数据库读数测试

                        System.Console.WriteLine("[0] - Tag实时，默认;");
                        System.Console.WriteLine("[1] - Tag计算测试;");
                        System.Console.WriteLine("[2] - Tag All计算测试;");
                        System.Console.WriteLine("[3] - EXP计算测试;");

                        strinput = System.Console.ReadLine();

                        if (strinput == "")
                        {
                            strinput = "0";
                        }

                        if (strinput == "0")
                        {
                            System.Console.WriteLine("请输入标签点, 默认sinusoid:");

                            string strtag = System.Console.ReadLine();

                            if (strtag == "")
                            {
                                strtag = "sinusoid";
                            }

                            double dValue = DBAccess.GetRealTime().GetSnapshotValue(strtag);
                            //string dValue = DBAccess.GetRealTime().GetDigitalSnapshotValueName(strtag);

                            if (dValue == double.MinValue)
                            {
                                System.Console.WriteLine("计算结果出现错误!");
                            }
                            else
                            {
                                System.Console.WriteLine(strtag + "实时值:  " + dValue.ToString("0.000"));
                            }

                        }else if (strinput == "1")
                        {
                            System.Console.WriteLine("请输入'条件表达式'，表达式中的点需用''引用，默认，空");

                            string strfilter = System.Console.ReadLine();

                            System.Console.WriteLine("请选择统计方式：");
                            System.Console.WriteLine("[0] - 最近8小时平均值,默认;");
                            System.Console.WriteLine("[1] - 最近8小时最大值;");
                            System.Console.WriteLine("[2] - 最近8小时最小值;");

                            string strtype = System.Console.ReadLine();
                            string strinfor = "平均值";
                            SummaryType st = SummaryType.asAverage;

                            if (strtype != "")
                            {
                                switch (strtype.Trim().ToLower())
                                {
                                    case "0":
                                        st = SummaryType.asAverage;
                                        strinfor = "平均值";
                                        break;
                                    case "1":
                                        st = SummaryType.asMaximum;
                                        strinfor = "最大值";
                                        break;
                                    case "2":
                                        st = SummaryType.asMinimum;
                                        strinfor = "最小值";
                                        break;
                                    default:
                                        st = SummaryType.asAverage;
                                        strinfor = "平均值";
                                        break;
                                }
                            }

                            DateTime stime = DateTime.Now.AddHours(-8);
                            DateTime etime = DateTime.Now;

                            double dValue = DBAccess.GetRealTime().TagCalculatedData(strfilter, stime, etime, strfilter, st);

                            if (dValue == double.MinValue)
                            {
                                System.Console.WriteLine("计算结果出现错误!");
                            }
                            else
                            {
                                System.Console.WriteLine(strfilter + strinfor + ":  " + dValue.ToString("0.000"));
                            }

                        }else if (strinput == "2")
                        {
                            System.Console.WriteLine("请输入'条件表达式'，表达式中的点需用''引用，默认，空");

                            string strfilter = System.Console.ReadLine();                            

                            DateTime stime = DateTime.Now.AddHours(-8);
                            DateTime etime = DateTime.Now;

                            TagAllValue tav = new TagAllValue();
                            bool bSuccessed = DBAccess.GetRealTime().TagSummaryData(strfilter, stime, etime, strfilter, out tav);

                            if (bSuccessed && tav != null)
                            {
                                System.Console.WriteLine("标签点名：" + tav.TagName);
                                System.Console.WriteLine("标签描述：" + tav.TagDesc);
                                System.Console.WriteLine("标签单位：" + tav.TagEngunit);
                                System.Console.WriteLine("标签实时值：" + tav.TagSnapshot.ToString("0.000"));
                                System.Console.WriteLine("标签最大值：" + tav.TagMaximum.ToString("0.000"));
                                System.Console.WriteLine("标签最小值：" + tav.TagMinimum.ToString("0.000"));
                                System.Console.WriteLine("标签平均值：" + tav.TagAverage.ToString("0.000"));
                                System.Console.WriteLine("标签幅值：" + tav.TagRange.ToString("0.000"));
                                System.Console.WriteLine("标签标准偏差值：" + tav.TagStdDev.ToString("0.000"));
                                System.Console.WriteLine("标签总体标准差值：" + tav.TagPStdDev.ToString("0.000"));
                                System.Console.WriteLine("标签累计值：" + tav.TagTotal.ToString("0.000"));

                            }
                            else
                            {
                                System.Console.WriteLine("计算结果出现错误!");
                            }
                        }
                        else if (strinput == "3")
                        {                            
                            System.Console.WriteLine("请输入'条件表达式'，表达式中的点需用''引用，默认，空");

                            string strfilter = System.Console.ReadLine();

                            System.Console.WriteLine("请选择统计方式：");
                            System.Console.WriteLine("[0] - 最近8小时平均值, 默认;");
                            System.Console.WriteLine("[1] - 最近8小时最大值;");
                            System.Console.WriteLine("[2] - 最近8小时最小值;");

                            string strtype = System.Console.ReadLine();
                            string strinfor = "平均值";
                            SummaryType st = SummaryType.asAverage;

                            if (strtype != "")
                            {
                                switch (strtype.Trim().ToLower())
                                {
                                    case "0":
                                        st = SummaryType.asAverage;
                                        strinfor = "平均值";
                                        break;
                                    case "1":
                                        st = SummaryType.asMaximum;
                                        strinfor = "最大值";
                                        break;
                                    case "2":
                                        st = SummaryType.asMinimum;
                                        strinfor = "最小值";
                                        break;
                                    default:
                                        st = SummaryType.asAverage;
                                        strinfor = "平均值";
                                        break;
                                }
                            }

                            DateTime stime = DateTime.Now.AddHours(-8);
                            DateTime etime = DateTime.Now;

                            double dValue = DBAccess.GetRealTime().ExpCalculatedData(strfilter, stime, etime, strfilter, st);

                            if (dValue == double.MinValue)
                            {
                                System.Console.WriteLine("计算结果出现错误!");
                            }
                            else
                            {
                                System.Console.WriteLine(strfilter + strinfor + ":  " + dValue.ToString("0.000"));
                            }

                        }

                        #endregion
                    }

                    //else if (strinput.ToLower() == "9")
                    //{
                    //    //历史计算
                    //    System.Console.WriteLine("正在进行历史补算.......");

                    //    string strWeb = System.Configuration.ConfigurationManager.AppSettings["HWeb"];
                    //    string strHistory = System.Configuration.ConfigurationManager.AppSettings["HHistory"];
                    //    string[] strTime = strHistory.Split(new string[] { ";", "," }, StringSplitOptions.RemoveEmptyEntries);

                    //    //历史计算
                    //    if (strTime.Length == 2)
                    //    {
                    //        if (DateTime.Parse(strTime[0]) > DateTime.Parse(strTime[1]))
                    //        {
                    //            System.Console.WriteLine("开始时间必须小于结束时间!");

                    //        }
                    //        else if (DateTime.Parse(strTime[1]) > DateTime.Now)
                    //        {
                    //            System.Console.WriteLine("结束时间必须小于当前时间!");

                    //        }
                    //        else
                    //        {
                    //            if (!WS_RDLCMainMethod.RDLCArchiveRun(strWeb, strTime[0], strTime[1]))
                    //            {
                    //                System.Console.WriteLine("历史计算失败!");
                    //            }
                    //            else
                    //            {
                    //                System.Console.WriteLine("历史计算完成!");
                    //            }

                    //        }

                    //    }
                    //    else
                    //    {
                    //        System.Console.WriteLine("时间配置错误!");
                    //    }                   


                    //}
                    //else
                    //{
                    //    System.Console.WriteLine("请输入有效选项!");
                    //}
                    
                
               } while (true);


            }

            #endregion

            /////////////////////////////////////////////////////////////////////////////
            #region 带参数运行

            if (args.Length==0)
            {
                return;
            }else
            {
                #region 竞赛计算服务

                string strgcontents = args[0].Trim();

                if (strgcontents != "")
                {
                    //Excel
                    if (!GetExcel())
                    {
                        return;
                    }

                    //实时计算
                    DateTime dtCurrentTime = DateTime.Now;
                    int nCS = dtCurrentTime.Second;
                    int nCM = dtCurrentTime.Minute;
                    int nCMmod = nCM % RPeriod;

                    DateTime dtET = dtCurrentTime.AddSeconds(-1 * nCS);
                    dtET = dtET.AddMinutes(-1 * nCMmod);

                    System.Console.WriteLine("正在进行实时计算.......");
                    System.Console.WriteLine("计算时间为：" + dtCurrentTime.ToString());
                    
                    
                    Stopwatch timer = new Stopwatch();
                    timer.Start();

                    WS_ExcelMainMethod.ExcelSnapRun(strExcel, strSheet, dtET, RPeriod, RSecond);

                    
                    timer.Stop();

                    LogUtil.LogMessage("本次统计完成！耗时: " + timer.ElapsedMilliseconds + "毫秒");

                    //if (!)
                    //{
                    //    System.Console.WriteLine("实时计算失败!");
                    //}
                    //else
                    //{
                    //    System.Console.WriteLine("实时计算完成!");
                    //}

                }

                #endregion

                return ;
            }

            #endregion

            //string strfiletypea = strgcontents.Substring(strgcontents.Length-4, 4).ToLower();
            //string strfiletypeb = strgcontents.Substring(strgcontents.Length-5, 5).ToLower();


            //if (args.Length == 1 && (strfiletypea == ".xls" || strfiletypeb == ".xlsx"))
            //{
            //    #region Excel Driver Calc

            //    ExcelMacroHelper.RunExcelMacro(strgcontents, "MainCalc");

            //    #endregion

            //    return;                
            //}

        }

        /// <summary>
        ///  检查Excel表
        /// </summary>
        /// <returns></returns>
        private static bool GetExcel()
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
                     LogUtil.LogMessage("Excel文件不存在");
                    return false;
                }

                return true;
            }
            catch (System.Exception ex)
            {
                LogUtil.LogMessage("Excel文件配置错误");

                //System.Windows.Forms.MessageBox.Show("Excel文件配置错误");

                return false;
            }

        }

    }
}
