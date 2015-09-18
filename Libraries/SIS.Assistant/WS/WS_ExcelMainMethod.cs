using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

using SIS.DataAccess;
using SIS.Loger;

namespace SIS.Assistant.WS
{
    /// <summary>
    /// 用于百万机组竞赛(SISKPI.GH项目中使用)
    /// 供百万机组竞赛服务调用获取实时数据、补采数据和采集历史数据
    /// </summary>
    public class WS_ExcelMainMethod
    {
        /// <summary>
        /// 运行服务
        /// </summary>
        public static void ExcelSnapRun(string strExcel, string strSheet, 
                                        DateTime dtT, int nLength, int nSecond)
        {
            //获得需要补算的时间
            ArrayList archiveList = new ArrayList();
            WS_ExcelDatClient.ReadDatFile(out archiveList);
            try
            {                
                //检查数据库连接是否满足要求
                if (WS_ExcelSubMethod.CanRun())
                {
                    //实时采集
                    WS_ExcelSubMethod.SnapshotCalc(strExcel, strSheet, dtT, nLength, nSecond);
                    //历史数据补采
                    foreach (string strTime in archiveList)
                    {
                        DateTime dtArchive = DateTime.Parse(strTime);
                        WS_ExcelSubMethod.SnapshotCalc(strExcel, strSheet, dtArchive, nLength, nSecond);
                    }
                    //历史时间清空
                    archiveList.Clear();
                }
            }
            catch (Exception ex)
            {
                //如果出现错误，记录。
                archiveList.Add(dtT.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss"));
                LogUtil.LogMessage(ex);
            }
            WS_ExcelDatClient.WriteDatFile(archiveList); 
        }

        /// <summary>
        /// 采集机组历史数据
        /// </summary>
        public static void ExcelArchiveRun(string strExcel, string strSheet, 
                                           string strUnit, string strKPI,
                                           DateTime dtST, DateTime dtET, int nLength, int nOffset, int nSecond)
        {
            try
            {
                //检查数据库连接是否满足要求
                if (WS_ExcelSubMethod.CanRun())
                {
                    WS_ExcelSubMethod.HistoryCalc(strExcel, strSheet, strUnit, strKPI, dtST, dtET, nLength, nOffset,  nSecond);
                }
            }
            catch (Exception ex)
            {
                LogUtil.LogMessage(ex);               
            }
        }
    }
}
