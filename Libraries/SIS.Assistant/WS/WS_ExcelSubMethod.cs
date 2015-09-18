using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;

using SIS.Loger;
using SIS.DataAccess;
using SIS.DataEntity;
using SIS.DBControl;

using System.Data.OleDb;
using System.Data.SqlClient;


namespace SIS.Assistant.WS
{
    /// <summary>
    /// 用于百万机组竞赛，从Excel中读取点清单然后根据点清单从PI数据库读取数据写入关系数据库
    /// </summary>
    public class WS_ExcelSubMethod
    {
        //各机组值点列表
        private static List<WS_ExcelTag> ltExcelTags = new List<WS_ExcelTag>();
        
        /// <summary>
        /// 是否满足计算条件
        /// </summary>
        /// <returns></returns>
        public static bool CanRun()
        {
            if (!DBAccess.GetRealTime().Connection())
            {
                LogUtil.LogMessage("实时库连接异常，终止计算");
                return false;
            }
            if (!WS_ExcelDBClient.Connection())
            {
                LogUtil.LogMessage("关系库连接异常，终止计算");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 从实时数据库抽取实时数据进行处理，处理结束后存入关系型数据库
        /// </summary>
        /// <param name="strExcel">各机组值点配置文件</param>
        /// <param name="strSheet">工作簿</param>
        /// <param name="dtTime">开始时间</param>
        /// <param name="nLength"></param>
        /// <param name="nSecond"></param>
        /// <returns></returns>
        public static bool SnapshotCalc(string strExcel, string strSheet, DateTime dtTime, int nLength, int nSecond)
        {
            //获取ExcelTag
            if (ltExcelTags == null || ltExcelTags.Count<=0)
            {
                if (!ImportExcelToList(strExcel, strSheet))
                {
                    return false;
                }
            }

            if (ltExcelTags.Count <= 0)
            {
                LogUtil.LogMessage("无有效数据!");
                return true;
            }
            ReadAndWrite(ltExcelTags, dtTime, nLength, nSecond);
            return true;
        }


        /// <summary>
        /// 从实时数据库抽取历史数据进行处理，处理结束后存入关系型数据库
        /// </summary>
        /// <param name="strExcel">各机组值点配置文件</param>
        /// <param name="strSheet">工作簿</param>
        /// <param name="dtSTime">开始时间</param>
        /// <param name="dtETime">截止时间</param>
        /// <param name="nLength"></param>
        /// <param name="nSecond"></param>
        /// <returns></returns>
        public static bool HistoryCalc(string strExcel, string strSheet,
                                       string strUnit, string strKPI, 
                                       DateTime dtSTime, DateTime dtETime, int nLength, int nOffset, int nSecond)
        {
            //获取ExcelTag
            if (ltExcelTags == null || ltExcelTags.Count <= 0)
            {
                if (!ImportExcelToList(strExcel, strSheet))
                {
                    return false;
                }
            }

            if (ltExcelTags.Count <= 0)
            {
                LogUtil.LogMessage("无有效数据!");
                return true;
            }

            //////////////////////////////////////////////////////////////////////////
            while (dtSTime <= dtETime)
            {
                bool bCalc = false;
                int nCS = dtSTime.Second;
                int nCM = dtSTime.Minute;
                int nCMmod = nCM % nLength;

                if (nOffset <= 0)
                {
                    if (nCMmod == 0)
                    {
                        bCalc = true;
                    }
                    else
                    {
                        bCalc = false;
                    }

                }
                else
                {
                    if (nCMmod == nOffset)
                    {
                        bCalc = true;
                    }
                    else
                    {
                        bCalc = false;
                    }
                }

                //自动一分钟循环
                DateTime dtTime = dtSTime.AddMinutes(-1 * nOffset);
                dtSTime = dtSTime.AddMinutes(1);
                
                if (!bCalc)
                {
                    continue;
                }

                #region 历史计算

                string strUnitFilter = "";
                string strKPIFilter = "";
                string[] arrUnit = strUnit.Split(new string[] { ";", "," }, StringSplitOptions.RemoveEmptyEntries);
                string[] arrKPI = strKPI.Split(new string[] { ";", "," }, StringSplitOptions.RemoveEmptyEntries);

                string strTime = dtTime.ToString("yyyy-MM-dd HH:mm:ss");

                if (arrUnit.Length == 0 && arrKPI.Length==0)
                {
                    //所有机组、所有指标的情况
                    //删除
                    WS_ExcelDBClient.DeleteData("", "", strTime);

                    //读写
                    ReadAndWrite(ltExcelTags, dtTime, nLength, nSecond);
                }
                else if (arrUnit.Length > 0 && arrKPI.Length == 0)
                {
                    //个别机组、所有指标的情况
                    for (int i = 0; i < arrUnit.Length; i++)
                    {
                        strUnitFilter = arrUnit[i].ToLower();

                        List<WS_ExcelTag> ltTags = ltExcelTags.Where(p => p.UnitCode.ToLower().Contains(strUnitFilter)).ToList();

                        //删除
                        WS_ExcelDBClient.DeleteData(strUnitFilter, "", strTime);

                        //读写
                        ReadAndWrite(ltTags, dtTime, nLength, nSecond);
                        
                    }
                }
                else
                {
                    //个别机组、个别指标的情况
                    for (int i = 0; i < arrUnit.Length; i++)
                    {
                        strUnitFilter = arrUnit[i].ToLower();

                        for (int j = 0; j < arrKPI.Length; j++)
                        {
                            strKPIFilter = arrKPI[j].ToLower();

                            List<WS_ExcelTag> ltTags = ltExcelTags.Where(p => (p.UnitCode.ToLower().Contains(strUnitFilter) && p.TagCode.ToLower().Contains(strKPIFilter))).ToList();

                            if (ltTags.Count <= 0)
                            {
                                continue;
                            }

                            //删除
                            WS_ExcelDBClient.DeleteData(strUnitFilter, strKPIFilter, strTime);

                            //读写
                            ReadAndWrite(ltTags, dtTime, nLength, nSecond);
                        }

                    }

                }

                #endregion
            }
            return true;
        }

        /// <summary>
        /// 从实时数据库抽取数据进行处理，处理结束后存入关系型数据库
        /// </summary>
        /// <param name="ltTags">各机组值点</param>
        /// <param name="dtTime"></param>
        /// <param name="nLength"></param>
        /// <param name="nSecond"></param>
        /// <returns></returns>
        private static bool ReadAndWrite(List<WS_ExcelTag> ltTags, DateTime dtTime, int nLength, int nSecond)
        {
            //如果nSecond==0时，没有变化
            DateTime dtValid = dtTime.AddSeconds(nSecond);            
            //////////////////////////////////////////////////////////////////////////
            //整理点表
            DataTable dt = WS_ExcelDBClient.GetTableSchema();

            //获得机组列表
            var unitresult = (from kpi in ltTags
                              orderby kpi.UnitCode
                              select kpi.UnitCode).Distinct().ToList();

            foreach (string unitcode in unitresult)
            {
                var kpiresult = from kpi in ltTags
                                where (kpi.UnitCode == unitcode && kpi.TagIsValid == "1")
                                select kpi;

                if (kpiresult.Count() <= 0)
                {
                    continue;
                }

                string strCondition = kpiresult.ElementAt(0).TagRunExp;
                Double dRunning = DBAccess.GetRealTime().TimedCalculate(strCondition, dtValid);

                //机组停运
                if (dRunning < 0.5)
                {
                    string strTime = dtValid.ToString("yyyy-MM-dd HH:mm:ss");
                    LogUtil.LogMessage("机组:" + unitcode + ",在" + strTime + "负荷太低或停运，本次不采集本机组数据!");
                    continue;
                }
                else
                {
                    LogUtil.LogMessage("开始采集机组数据:" + unitcode);
                }

                //机组相关标签点
                Dictionary<string, TagValue> dicTags = new Dictionary<string, TagValue>();
                foreach (WS_ExcelTag tagcode in kpiresult)
                {
                    String strCode = tagcode.TagShiftExp.Trim();
                    if (strCode != "")
                    {
                        dicTags[strCode] = new TagValue(strCode);
                    }

                    strCode = tagcode.TagPeriodExp.Trim();
                    if (strCode != "")
                    {
                        dicTags[strCode] = new TagValue(strCode);
                    }

                    strCode = tagcode.TagMWExp.Trim();
                    if (strCode != "")
                    {
                        dicTags[strCode] = new TagValue(strCode);
                    }

                    strCode = tagcode.TagSnapExp.Trim();
                    if (strCode != "")
                    {
                        dicTags[strCode] = new TagValue(strCode);
                    }

                    strCode = tagcode.TagTargetExp.Trim();
                    if (strCode != "")
                    {
                        dicTags[strCode] = new TagValue(strCode);
                    }

                    strCode = tagcode.TagScoreExp.Trim();
                    if (strCode != "")
                    {
                        dicTags[strCode] = new TagValue(strCode);
                    }

                }

                string strError = "";

                if (!DBAccess.GetRealTime().GetArchiveListData(ref dicTags, dtValid, out strError))
                {
                    LogUtil.LogMessage(strError);
                }

                foreach (WS_ExcelTag tagcode in kpiresult)
                {
                    DataRow dr = dt.NewRow();

                    dr["TagID"] = Guid.NewGuid().ToString();
                    dr["UnitCode"] = tagcode.UnitCode;
                    dr["TagCode"] = tagcode.TagCode;
                    dr["TagType"] = tagcode.TagType;
                    dr["TagEngunit"] = tagcode.TagEngunit;
                    dr["TagTime"] = dtTime.ToString("yyyy-MM-dd HH:mm:ss");

                    if (dicTags.ContainsKey(tagcode.TagShiftExp) 
                        && dicTags[tagcode.TagShiftExp].TagQulity == 0
                        && dicTags[tagcode.TagShiftExp].TagStringValue != null)
                    {
                        dr["TagShift"] = dicTags[tagcode.TagShiftExp].TagStringValue;
                    }
                    else
                    {
                        dr["TagShift"] = System.DBNull.Value;
                    }

                    if (dicTags.ContainsKey(tagcode.TagPeriodExp) 
                        && dicTags[tagcode.TagPeriodExp].TagQulity == 0
                        && dicTags[tagcode.TagPeriodExp].TagStringValue != null)
                    {
                        dr["TagPeriod"] = dicTags[tagcode.TagPeriodExp].TagStringValue;
                    }
                    else
                    {
                        dr["TagPeriod"] = System.DBNull.Value;
                    }

                    if (dicTags.ContainsKey(tagcode.TagMWExp) 
                        && dicTags[tagcode.TagMWExp].TagQulity == 0
                        && dicTags[tagcode.TagMWExp].TagStringValue !=null)
                    {
                        dr["TagMW"] = dicTags[tagcode.TagMWExp].TagStringValue;
                    }
                    else
                    {
                        dr["TagMW"] = System.DBNull.Value;
                    }

                    if (dicTags.ContainsKey(tagcode.TagSnapExp) 
                        && dicTags[tagcode.TagSnapExp].TagQulity == 0
                        && dicTags[tagcode.TagSnapExp].TagStringValue != null)
                    {
                        dr["TagSnap"] = dicTags[tagcode.TagSnapExp].TagStringValue;
                    }
                    else
                    {
                        dr["TagSnap"] = System.DBNull.Value;
                    }

                    if (dicTags.ContainsKey(tagcode.TagTargetExp) 
                        && dicTags[tagcode.TagTargetExp].TagQulity == 0
                        && dicTags[tagcode.TagTargetExp].TagStringValue != null)
                    {
                        dr["TagTarget"] = dicTags[tagcode.TagTargetExp].TagStringValue;
                    }
                    else
                    {
                        dr["TagTarget"] = System.DBNull.Value;
                    }

                    if (dicTags.ContainsKey(tagcode.TagScoreExp) 
                        && dicTags[tagcode.TagScoreExp].TagQulity == 0
                        && dicTags[tagcode.TagScoreExp].TagStringValue !=null)
                    {
                        dr["TagScore"] = dicTags[tagcode.TagScoreExp].TagStringValue;
                    }
                    else
                    {
                        dr["TagScore"] = System.DBNull.Value;
                    }

                    dr["TagLength"] = nLength.ToString();
                    dr["TagRemoved"] = "0";


                    dt.Rows.Add(dr);
                }

            }

            if (dt.Rows.Count > 0)
            {
                WS_ExcelDBClient.BulkToDB(dt);
            }

            dt.Dispose();

            return true;
        }

        /// <summary>
        /// 将Excel数据导入到List表链中
        /// </summary>
        /// <param name="xlstable">Excel文件名</param>
        /// <param name="xlssheet">工作簿</param>
        /// <returns></returns>
        protected static bool ImportExcelToList(string ExcelFileName, string xlssheet)
        {
            try
            {
                //打开Excel表,并写入DataSet中。
                //string xlsfile = ExcelFileName;
                //string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;" + "data source=" + xlsfile
                //                                                  + ";Extended Properties='Excel 8.0; HDR=YES; IMEX=1'";
                string strConn = "Provider=Microsoft.Jet.OleDb.4.0;" + "data source=" + ExcelFileName
                                                                  + ";Extended Properties='Excel 8.0; HDR=YES; IMEX=1'";

                OleDbConnection conn = new OleDbConnection(strConn);
                conn.Open();
                DataSet ds = new DataSet();
                OleDbDataAdapter odda = new OleDbDataAdapter("select * from [" + xlssheet + "$]", conn);

                odda.Fill(ds, "table1");
                conn.Close();
                //读取DataSet数据集
                ltExcelTags.Clear();

                ////////////////////////////////////////////////////////////////            
                System.Data.DataTable dt = ds.Tables[0];
                int nAll = dt.Rows.Count;
                int nValid = 0;
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    if (dr["TagID"].ToString() != "")
                    {
                        WS_ExcelTag exlstag = new WS_ExcelTag();
                        exlstag.TagID = dr["TagID"].ToString().Trim();
                        exlstag.UnitCode = dr["UnitCode"].ToString().Trim();
                        exlstag.TagCode = dr["TagCode"].ToString().Trim();
                        exlstag.TagDesc = dr["TagDesc"].ToString().Trim();
                        exlstag.TagType = dr["TagType"].ToString().Trim();
                        exlstag.TagEngunit = dr["TagEngunit"].ToString().Trim();
                        exlstag.TagIsValid = dr["TagIsValid"].ToString().Trim();
                        exlstag.TagRunExp = dr["TagRunExp"].ToString().ToUpper().Trim();
                        exlstag.TagShiftExp = dr["TagShiftExp"].ToString().ToUpper().Trim();
                        exlstag.TagPeriodExp = dr["TagPeriodExp"].ToString().ToUpper().Trim();
                        exlstag.TagMWExp = dr["TagMWExp"].ToString().ToUpper().Trim();
                        exlstag.TagSnapExp = dr["TagSnapExp"].ToString().ToUpper().Trim();
                        exlstag.TagTargetExp = dr["TagTargetExp"].ToString().ToUpper().Trim();
                        exlstag.TagScoreExp = dr["TagScoreExp"].ToString().ToUpper().Trim();
                        exlstag.TagPeriodExp = dr["TagPeriodExp"].ToString().ToUpper().Trim();

                        //sub tag
                        ltExcelTags.Add(exlstag);
                        nValid += 1;
                    }
                }
                string strInfor = "Excel表总数为：{0}个, 【ID不为空】有效点为:{1}个。";
                strInfor = string.Format(strInfor, nAll, nValid);                
                LogUtil.LogMessage(strInfor);    
                return true;
            }
            catch (Exception ex)
            {
                LogUtil.LogMessage("Excel表读取失败:"+ex.ToString());
                return false;
            }
        }
           
    }

    /// <summary>
    /// ExcelTag类
    /// </summary>
    public class WS_ExcelTag
    {
        public string TagID
        {
            get;
            set;
        }

        public string UnitCode
        {
            get;
            set;
        }

        public string TagCode
        {
            get;
            set;
        }

        public string 	TagDesc
        {
            get;
            set;
        }

        public string 	TagType
        {
            get;
            set;
        }

        public string 	TagEngunit
        {
            get;
            set;
        }

        public string 	TagIsValid
        {
            get;
            set;
        }

        public string 	TagRunExp
        {
            get;
            set;
        }

        public string 	TagShiftExp	
        {
            get;
            set;
        }

        public string TagPeriodExp
        {
            get;
            set;
        }

        public string 	TagMWExp
        {
            get;
            set;
        }

        public string 	TagSnapExp
        {
            get;
            set;
        }

        public string 	TagTargetExp
        {
            get;
            set;
        }

        public string 	TagScoreExp
        {
            get;
            set;
        }

    }
    
}
