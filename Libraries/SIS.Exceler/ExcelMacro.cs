using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using SIS.Loger;

//添加引用COM: Microsoft Excel Object 14.0
using Microsoft.Office.Core; 
using Excel = Microsoft.Office.Interop.Excel; 
  
namespace SIS.Exceler
{
    /// <summary>
    /// 执行Excel VBA宏帮助类
    /// </summary>
    public class ExcelMacroHelper
    {
        /// <summary>  
        /// 执行Excel中的宏  
        /// </summary>  
        /// <param name="excelFilePath">Excel文件路径</param>  
        /// <param name="macroName">宏名称</param>
        public static void RunExcelMacro(string excelFilePath, string macroName)
        {
            try
            {
                #region 检查输入参数
                //检查文件是否存在  
                if (!File.Exists(excelFilePath))
                {
                    LogUtil.LogMessage(excelFilePath + " 文件不存在");
                    //return;
                }

                // 检查是否输入宏名称  
                if (string.IsNullOrEmpty(macroName))
                {
                    LogUtil.LogMessage("请输入宏的名称");
                    //return;
                }
                #endregion

                #region 调用宏处理

                // 准备打开Excel文件时的缺省参数对象  
                object oMissing = System.Reflection.Missing.Value;                

                // 创建Excel对象示例  
                //Excel.ApplicationClass oExcel = new Excel.ApplicationClass();
                Excel.Application oExcel = new Excel.Application();
                oExcel.Visible = false;

                // 创建Workbooks对象  
                Excel.Workbooks oBooks = oExcel.Workbooks;

                // 创建Workbook对象  
                Excel._Workbook oBook = null;

                //System.AppDomain.CurrentDomain.BaseDirectory.ToString());
                //System.Console.WriteLine(System.Environment.CurrentDirectory.ToString());

                string appPath = System.Environment.CurrentDirectory;

                excelFilePath = appPath + "\\" + excelFilePath;

                LogUtil.LogMessage("执行Excel文件:" + excelFilePath);

                // 打开指定的Excel文件 
                oBook = oBooks.Open(excelFilePath,
                    oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing,
                    oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing);

                object[] paraObjects = new object[] { macroName };
                object rtnValue;

                LogUtil.LogMessage("***********运行宏命令!");

                // 执行Excel中的宏  
                rtnValue = RunMacro(oExcel, paraObjects);

                LogUtil.LogMessage("***********结束宏命令!");

                // 保存更改  
                oBook.Save();


                // 退出Workbook  
                oBook.Close(false, oMissing, oMissing);

                #endregion

                #region 释放对象
                // 释放Workbook对象  
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oBook);
                oBook = null;

                // 释放Workbooks对象  
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oBooks);
                oBooks = null;

                // 关闭Excel  
                oExcel.Quit();
                // 释放Excel对象  
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel);
                oExcel = null;

                // 调用垃圾回收  
                GC.Collect();

                #endregion
            }
            catch (Exception exp)
            {
                LogUtil.LogMessage(exp.Message);
            }
        }
        
        /// <summary>  
        /// 执行Excel中的宏  
        /// </summary>  
        /// <param name="excelFilePath">Excel文件路径</param>  
        /// <param name="macroName">宏名称</param>  
        /// <param name="parameters">宏参数组</param>  
        /// <param name="rtnValue">宏返回值</param>  
        /// <param name="isShowExcel">执行时是否显示Excel</param>         
        public static void RunExcelMacro(string excelFilePath, string macroName, object[] parameters, object rtnValue, bool isShowExcel)
        {
            try
            {
                #region  检查输入参数
                //检查文件是否存在  
                if (!File.Exists(excelFilePath))
                {
                    LogUtil.LogMessage(excelFilePath + " 文件不存在");
                    //return;
                }

                // 检查是否输入宏名称  
                if (string.IsNullOrEmpty(macroName))
                {
                    LogUtil.LogMessage("请输入宏的名称");
                    //return;
                }  
                #endregion

                #region 调用宏处理
                // 准备打开Excel文件时的缺省参数对象  
                object oMissing = System.Reflection.Missing.Value;

                // 根据参数组是否为空，准备参数组对象  
                object[] paraObjects;
                if (parameters == null)
                {
                    paraObjects = new object[] { macroName };
                }
                else
                {
                    // 宏参数组长度  
                    int paraLength = parameters.Length;
                    paraObjects = new object[paraLength + 1];
                    paraObjects[0] = macroName;
                    for (int i = 0; i < paraLength; i++)
                    {
                        paraObjects[i + 1] = parameters[i];
                    }
                }

                // 创建Excel对象示例  
                //Excel.ApplicationClass oExcel = new Excel.ApplicationClass();
                Excel.Application oExcel = new Excel.Application();
                // 判断是否要求执行时Excel可见  
                if (isShowExcel)
                {
                    // 使创建的对象可见  
                    oExcel.Visible = true;
                }
                // 创建Workbooks对象  
                Excel.Workbooks oBooks = oExcel.Workbooks;
                // 创建Workbook对象  
                Excel._Workbook oBook = null;

                // 打开指定的Excel文件 
                oBook = oBooks.Open(excelFilePath,
                    oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing,
                    oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing);
                
                 // 执行Excel中的宏  
                 rtnValue = RunMacro(oExcel, paraObjects);

                 // 保存更改  
                 oBook.Save();

                 // 退出Workbook  

                 oBook.Close(false, oMissing, oMissing);
                #endregion

                #region 释放对象
                // 释放Workbook对象 
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oBook);
                oBook = null;
                // 释放Workbooks对象 
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oBooks);
                oBooks = null;
                
                // 关闭Excel 
                oExcel.Quit();
                // 释放Excel对象
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel);
                oExcel = null;
                // 调用垃圾回收  
                //GC.Collect();

                #endregion
            }
            catch (Exception exp)
            {
                LogUtil.LogMessage(exp.Message);
            }
        }
        
        /// <summary>  
        /// 执行宏  
        /// </summary>  
        /// <param name="oApp">Excel对象</param>  
        /// <param name="oRunArgs">参数（第一个参数为指定宏名称，后面为指定宏的参数值）</param>  
        /// <returns>宏返回值</returns>  
        private static object RunMacro(object oApp, object[] oRunArgs)
        {
            // 声明一个返回对象  
            object objRtn;

            try
            {
                // 反射方式执行宏  
                objRtn = oApp.GetType().InvokeMember("Run",
                    System.Reflection.BindingFlags.Default|System.Reflection.BindingFlags.InvokeMethod,
                    null,
                    oApp,
                    oRunArgs);

                // 返回值  
                return objRtn;
            }
            catch (Exception exp)
            {
                LogUtil.LogMessage(exp.Message);

                objRtn = exp.GetHashCode();

                return objRtn;
            }
        }
    }

}