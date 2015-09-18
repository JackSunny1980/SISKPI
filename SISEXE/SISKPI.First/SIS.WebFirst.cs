using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using SIS.Exceler;
//using SIS.Loger;

using SIS.DataAccess;
using SIS.DBControl;

//using SIS.Assistant.WS;

namespace SIS.WebFirst
{
    class Console
    {
        static void Main(string[] args)
        {
            /////////////////////////////////////////////////////////////////////////////
            //用户交互对话执行
            if (args.Length == 0)
            {
                do
                {
                    System.Console.WriteLine("-------------------");
                    System.Console.WriteLine("本程序为SIS发布模块初始化数据库信息使用!");
                    System.Console.WriteLine("请确认SQLServer数据库配置正确!");
                    System.Console.WriteLine("主要：SQLServer数据库历史数据将被清空!");

                    System.Console.WriteLine("请输入运行类型:");
                    System.Console.WriteLine("[0] - 退出, 默认;!");
                    System.Console.WriteLine("[1] - 初始化发布数据库;");
                    //System.Console.WriteLine("[2] - 数据库读数测试;");
                    //System.Console.WriteLine("[3] - 指标考核历史计算;");
                    //System.Console.WriteLine("[4] - Excel驱动计算;");
                    //
                    //

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
                        #region 初始化SISWeb各表

                        ////初始化权限模块
                        //System.Console.WriteLine("初始化权限组表开始......");
                        //SYS_GroupDal.InitilizeSYS_Group();
                        //System.Console.WriteLine("创建sisadmin、sisdemo组.");
                        //System.Console.WriteLine("初始化权限组表成功......");
                        
                        ////初始化用户模块
                        //System.Console.WriteLine("初始化用户表开始......");
                        //SYS_UserDal.InitilizeSYS_User();
                        //System.Console.WriteLine("创建sisadmin、sisdemo用户.");
                        //System.Console.WriteLine("初始化用户表成功......");

                        ////初始化菜单模块
                        //System.Console.WriteLine("初始化菜单表开始......");
                        //SYS_MenuDal.InitilizeSYS_Menu();
                        //System.Console.WriteLine("创建菜单：系统管理.");
                        //System.Console.WriteLine("初始化菜单表成功......");
                        

                        #endregion
                    }
                    //else if (strinput.ToLower() == "2")
                    //{
                    //    #region 数据库读数测试

                    //    System.Console.WriteLine("[0] - Tag实时，默认;");
                    //    System.Console.WriteLine("[1] - Tag计算测试;");
                    //    System.Console.WriteLine("[2] - Tag All计算测试;");
                    //    System.Console.WriteLine("[3] - EXP计算测试;");

                    //    strinput = System.Console.ReadLine();

                    //    if (strinput == "")
                    //    {
                    //        strinput = "0";
                    //    }

                    //    if (strinput == "0")
                    //    {
                    //        System.Console.WriteLine("请标签点，默认,3_load:");

                    //        string strtag = System.Console.ReadLine();

                    //        if (strtag == "")
                    //        {
                    //            strtag = "3_load";
                    //        }

                    //        double dValue = DBAccess.GetRealTime().GetSnapshotValue(strtag);
                    //        //string dValue = DBAccess.GetRealTime().GetDigitalSnapshotValueName(strtag);

                    //        if (dValue == double.MinValue)
                    //        {
                    //            System.Console.WriteLine("计算结果出现错误!");
                    //        }
                    //        else
                    //        {
                    //            System.Console.WriteLine(strtag + "实时:  " + dValue.ToString("0.000"));
                    //        }

                    //    }else if (strinput == "1")
                    //    {

                    //        System.Console.WriteLine("请标签点，默认,'3_load':");

                    //        string strtag = System.Console.ReadLine();

                    //        if (strtag == "")
                    //        {
                    //            strtag = "3_load";
                    //        }

                    //        System.Console.WriteLine("请输入'条件表达式'，表达式中的点需用''引用，默认，空");

                    //        string strfilter = System.Console.ReadLine();

                    //        System.Console.WriteLine("请选择统计方式：");
                    //        System.Console.WriteLine("[0] - 平均,默认;");
                    //        System.Console.WriteLine("[1] - 最大值;");
                    //        System.Console.WriteLine("[2] - 最小值;");

                    //        string strtype = System.Console.ReadLine();
                    //        string strinfor = "平均值";
                    //        SummaryType st = SummaryType.asAverage;

                    //        if (strtype != "")
                    //        {
                    //            switch (strtype.Trim().ToLower())
                    //            {
                    //                case "0":
                    //                    st = SummaryType.asAverage;
                    //                    strinfor = "平均值";
                    //                    break;
                    //                case "1":
                    //                    st = SummaryType.asMaximum;
                    //                    strinfor = "最大值";
                    //                    break;
                    //                case "2":
                    //                    st = SummaryType.asMinimum;
                    //                    strinfor = "最小值";
                    //                    break;
                    //                default:
                    //                    st = SummaryType.asAverage;
                    //                    strinfor = "平均值";
                    //                    break;
                    //            }
                    //        }

                    //        DateTime stime = DateTime.Now.AddHours(-8);
                    //        DateTime etime = DateTime.Now;

                    //        double dValue = DBAccess.GetRealTime().TagCalculatedData(strtag, stime, etime, strfilter, st);

                    //        if (dValue == double.MinValue)
                    //        {
                    //            System.Console.WriteLine("计算结果出现错误!");
                    //        }
                    //        else
                    //        {
                    //            System.Console.WriteLine(strtag + strinfor + ":  " + dValue.ToString("0.000"));
                    //        }

                    //    }else if (strinput == "2")
                    //    {

                    //        System.Console.WriteLine("请标签点，默认,'3_load':");

                    //        string strtag = System.Console.ReadLine();

                    //        if (strtag == "")
                    //        {
                    //            strtag = "3_load";
                    //        }

                    //        System.Console.WriteLine("请输入'条件表达式'，表达式中的点需用''引用，默认，空");

                    //        string strfilter = System.Console.ReadLine();                            

                    //        DateTime stime = DateTime.Now.AddHours(-8);
                    //        DateTime etime = DateTime.Now;

                    //        TagAllValue tav = new TagAllValue();
                    //        bool bSuccessed = DBAccess.GetRealTime().TagSummaryData(strtag, stime, etime, strfilter, out tav);

                    //        if (bSuccessed && tav != null)
                    //        {
                    //            System.Console.WriteLine("标签点名：" + tav.TagName);
                    //            System.Console.WriteLine("标签描述：" + tav.TagDesc);
                    //            System.Console.WriteLine("标签单位：" + tav.TagEngunit);
                    //            System.Console.WriteLine("标签实时值：" + tav.TagSnapshot.ToString("0.000"));
                    //            System.Console.WriteLine("标签最大值：" + tav.TagMaximum.ToString("0.000"));
                    //            System.Console.WriteLine("标签最小值：" + tav.TagMinimum.ToString("0.000"));
                    //            System.Console.WriteLine("标签平均值：" + tav.TagAverage.ToString("0.000"));
                    //            System.Console.WriteLine("标签幅值：" + tav.TagRange.ToString("0.000"));
                    //            System.Console.WriteLine("标签标准偏差值：" + tav.TagStdDev.ToString("0.000"));
                    //            System.Console.WriteLine("标签总体标准差值：" + tav.TagPStdDev.ToString("0.000"));
                    //            System.Console.WriteLine("标签累计值：" + tav.TagTotal.ToString("0.000"));

                    //        }
                    //        else
                    //        {
                    //            System.Console.WriteLine("计算结果出现错误!");
                    //        }
                    //    }
                    //    else if (strinput == "3")
                    //    {

                    //        System.Console.WriteLine("请输入'计算表达式'，点需用''引用，默认,'3_load':");

                    //        string strexp = System.Console.ReadLine();

                    //        if (strexp == "")
                    //        {
                    //            strexp = "'3_load'";
                    //        }

                    //        System.Console.WriteLine("请输入'条件表达式'，表达式中的点需用''引用，默认，空");

                    //        string strfilter = System.Console.ReadLine();

                    //        System.Console.WriteLine("请选择统计方式：");
                    //        System.Console.WriteLine("[0] - 平均， 默认;");
                    //        System.Console.WriteLine("[1] - 最大值;");
                    //        System.Console.WriteLine("[2] - 最小值;");

                    //        string strtype = System.Console.ReadLine();
                    //        string strinfor = "平均值";
                    //        SummaryType st = SummaryType.asAverage;

                    //        if (strtype != "")
                    //        {
                    //            switch (strtype.Trim().ToLower())
                    //            {
                    //                case "0":
                    //                    st = SummaryType.asAverage;
                    //                    strinfor = "平均值";
                    //                    break;
                    //                case "1":
                    //                    st = SummaryType.asMaximum;
                    //                    strinfor = "最大值";
                    //                    break;
                    //                case "2":
                    //                    st = SummaryType.asMinimum;
                    //                    strinfor = "最小值";
                    //                    break;
                    //                default:
                    //                    st = SummaryType.asAverage;
                    //                    strinfor = "平均值";
                    //                    break;
                    //            }
                    //        }

                    //        DateTime stime = DateTime.Now.AddHours(-8);
                    //        DateTime etime = DateTime.Now;

                    //        double dValue = DBAccess.GetRealTime().ExpCalculatedData(strexp, stime, etime, strfilter, st);

                    //        if (dValue == double.MinValue)
                    //        {
                    //            System.Console.WriteLine("计算结果出现错误!");
                    //        }
                    //        else
                    //        {
                    //            System.Console.WriteLine(strexp + strinfor + ":  " + dValue.ToString("0.000"));
                    //        }

                    //    }

                    //    #endregion
                    //}
                    //else if (strinput.ToLower() == "3")
                    //{
                    //    #region 三河指标考核历史计算,需配置指标文件

                    //    System.Console.WriteLine("正在准备进行历史计算.......");

                    //    string HUnitName = System.Configuration.ConfigurationSettings.AppSettings["HUnitName"];
                    //    string HStartName = System.Configuration.ConfigurationSettings.AppSettings["HStartTime"];
                    //    string HEndTime = System.Configuration.ConfigurationSettings.AppSettings["HEndTime"];

                    //    //if (HUnitName.LastIndexOf(',') > 0)
                    //    //{
                    //    //    HUnitName = HUnitName.Remove(HUnitName.LastIndexOf(','));
                    //    //}

                    //    string[] AllUnit = HUnitName.Split(',');

                    //    if (AllUnit.Length <= 0)
                    //    {
                    //        System.Console.WriteLine("无有效的机组进行历史计算.......");
                    //    }
                    //    else
                    //    {
                    //        if (DateTime.Parse(HStartName) > DateTime.Parse(HEndTime))
                    //        {
                    //            System.Console.WriteLine("历史计算的开始时间和结束时间无效.......");
                    //        }
                    //        else
                    //        {
                    //            for (int i = 0; i < AllUnit.Length; i++)
                    //            {
                    //                System.Console.WriteLine(AllUnit[i] + "正在进行历史计算.......");
                    //                System.Console.WriteLine("历史计算的开始时间为：" + HStartName);
                    //                System.Console.WriteLine("历史计算的结束时间为：" + HEndTime);

                    //                //if (!WS_KPISubMethod.HistoryCalc(AllUnit[i], HStartName, HEndTime))
                    //                //{
                    //                //    System.Console.WriteLine(AllUnit[i] + "历史计算失败!");
                    //                //}
                    //                //else
                    //                //{
                    //                //    System.Console.WriteLine(AllUnit[i] + "历史计算完成!");
                    //                //}
                    //            }
                    //        }
                    //    }

                    //    #endregion
                    //}
                    //else if (strinput.ToLower() == "4")
                    //{
                    //    #region Excel驱动计算

                    //    System.Console.WriteLine("请输入Excel文件，必须在当前目录:");

                    //    strinput = System.Console.ReadLine();

                    //    if (strinput == "")
                    //    {
                    //        strinput = "KPIClac.xls";
                    //    }



                    //    #endregion
                    //}
                    else
                    {
                        System.Console.WriteLine("请输入有效选项!");
                    }

                } while (true);

            }

            /////////////////////////////////////////////////////////////////////////////
            //带参数运行
            //string strgcontents = args[0];
                        
            //strgcontents = strgcontents.Trim();

            //if (args.Length==1 && strgcontents == "sanhekpi")
            //{
            //    #region 三河指标考核实时计算

            //    //实时计算
            //    System.Console.WriteLine("正在进行实时计算.......");
            //    System.Console.WriteLine("计算时间为：" + DateTime.Now.ToString());

            //    //if (!WS_KPISubMethod.SnapshotCalc())
            //    //{
            //    //    System.Console.WriteLine("实时计算失败!");
            //    //}
            //    //else
            //    //{
            //    //    System.Console.WriteLine("实时计算完成!");
            //    //}

            //    #endregion

            //    return;
            //}

            //string strfiletypea = strgcontents.Substring(strgcontents.Length-4, 4).ToLower();
            //string strfiletypeb = strgcontents.Substring(strgcontents.Length-5, 5).ToLower();


            //if (args.Length == 1 && (strfiletypea == ".xls" || strfiletypeb == ".xlsx"))
            //{
            //    #region Excel Driver Calc

            //    //ExcelMacroHelper.RunExcelMacro(strgcontents, "MainCalc");

            //    #endregion

            //    return;                
            //}


        }
    }
}
