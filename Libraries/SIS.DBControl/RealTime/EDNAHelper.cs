using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
//using SIS.DBControl.EDNAAPI;
///using SIS.DBControl;

namespace SIS.DBControl {
    public class EDNAHelper : RTInterface {
        private static EDNAHelper _mInstance = null;

        /// <summary>
        /// 读取数据的类型
        /// </summary>
        private enum DataStatus {
            Normal, //正常值
            Avg,    //平均值
            Max,    //最大值
            Min     //最小值
        };

        #region EDNA实例

        public static EDNAHelper Instance() {
            if (_mInstance == null) {
                _mInstance = new EDNAHelper();
            }
            return _mInstance;
        }

        #endregion

        #region 数据库操作

        public void Dispose() {
        }

        /// <summary>
        /// 判断实时库是否连接
        /// </summary>
        /// <returns></returns>
        public bool Connection() {
            int result = 0;
            string UnivIP = ConfigurationManager.AppSettings["UnivIP"].ToString();
            int UnivPort = int.Parse(ConfigurationManager.AppSettings["UnivPort"].ToString());
            result = UNIVSERV.eDnaLinkConnect(UnivIP, UnivPort);
            if (result != 0) {
                return false;
            }
            //关闭连接
            int t = UNIVSERV.IseDnaLinkConnected(1);
            if (t != 0) {
                UNIVSERV.eDnaLinkClear(1);
            }

            return true;
        }

        /// <summary>
        /// 获得服务器时间
        /// </summary>
        /// <returns></returns>
        public DateTime GetServerTime() {
            return DateTime.Now;
        }

        #endregion

        #region Point操作

        public bool AddPoint(string tagname, bool isdigital, object desc) {
            throw new Exception("The method or operation is not implemented.");
        }

        public bool UpdatePoint(string oKey, string nKey) {
            throw new Exception("The method or operation is not implemented.");
        }

        public bool DeletePoint(string tagname) {
            throw new Exception("The method or operation is not implemented.");
        }

        public bool ExistPoint(string tagname) {
            throw new Exception("The method or operation is not implemented.");
        }

        public bool PointIsDigital(string tagname) {
            return false;
        }

        public TagInfo GetPointInfo(string tagname) {
            return new TagInfo();
        }

        /// <summary>
        /// 根据条件得到测点集合  测点信息
        /// </summary>
        /// <returns></returns>
        public List<TagInfo> GetPointInfoList(string filterexp) {
            List<TagInfo> result = new List<TagInfo>();

            return result;

        }

        public List<string> GetPointList(string WhereClause, bool flag, DateTime time) {
            List<string> l = new List<string>();
            return l;
        }

        public List<string> GetPointList(string WhereClause) {
            List<string> l = new List<string>();
            return l;
        }

        public List<string> GetPointList() {
            List<string> l = new List<string>();
            return l;
        }


        public List<TagValue> GetPointListForSDLR(string condition) {
            List<TagValue> l = new List<TagValue>();


            return l;

        }

        #endregion

        #region Value读操作

        /// <summary>
        /// 判断标签值是否正常
        /// </summary>
        /// <param name="tagname"></param>
        /// <returns></returns>
        public bool PointValueIsGood(string tagname) {
            try {
                return true;
            }
            catch (Exception ex) {
                //
                return false;
            }
        }

        /// <summary>
        /// 获取某个点的Shapshot值
        /// </summary>
        /// <param name="tagname">点名</param>
        /// <returns></returns>
        public double GetSnapshotValue(string tagname) {
            double DValue = double.MinValue;
            try {
                DateTime DT = DateTime.Now;
                DValue = this.GetArchiveValue(tagname, DT);
            }
            catch {
                //
            }
            return DValue;
        }

        /// <summary>
        /// 得到字符串类型的的实时值
        /// </summary>
        /// <param name="tagname"></param>
        /// <returns></returns>
        public string GetSnapshotStringValue(string tagname) {
            return "";
        }

        /// <summary>
        /// 获取某个点的Shapshot值
        /// 
        /// </summary>
        /// <param name="tagname">点名</param>
        /// <returns></returns>
        public double GetSnapshotValue(string tagname, out object TimeStamp) {
            TimeStamp = null;
            return -1;
        }

        /// <summary>
        /// 获取某个点的History值
        /// </summary>
        /// <param name="tagname">点名</param>
        /// <returns></returns>
        public double GetArchiveValue(string tagname, DateTime dt) {
            double DValue = double.MinValue;
            try {
                System.UInt32 eDNAPoint = 0;
                int Index = 0;
                GetKey(tagname, dt, dt, 1, ref eDNAPoint, ref Index, DataStatus.Normal);
                StringBuilder szStatus = new StringBuilder(20);
                StringBuilder szTime = new StringBuilder(30);
                double pdValue = 0;
                int ptTime = 0;
                while (Index == 0) {
                    Index = History.DnaGetNextHistUTC(eDNAPoint, ref pdValue, ref ptTime, szStatus, 20);

                    if (Index == 0) {
                        //保留时间
                        //RealTime.UCTToStringTime(ptTime, szTime, 30);
                        //if (szStatus.ToString().StartsWith("OK"))
                        {
                            DValue = pdValue;
                            break;
                        }
                    }
                }
            }
            catch {
                //
            }
            return DValue;
        }

        /// <summary>
        /// 获取某个点的History值
        /// </summary>
        /// <param name="tagname">点名</param>
        /// <returns></returns>
        public double GetArchiveValue(string tagname, out object timeStamp) {
            double DValue = double.MinValue;


            timeStamp = null;

            try {
                timeStamp = null;

                //System.UInt32 eDNAPoint = 0;
                //int Index = 0;
                //GetKey(tagname, dt, dt, 1, ref eDNAPoint, ref Index, DataStatus.Normal);
                //StringBuilder szStatus = new StringBuilder(20);
                //StringBuilder szTime = new StringBuilder(30);
                //double pdValue = 0;
                //int ptTime = 0;
                //while (Index == 0)
                //{
                //    Index = History.DnaGetNextHistUTC(eDNAPoint, ref pdValue, ref ptTime, szStatus, 20);

                //    if (Index == 0)
                //    {
                //        //保留时间
                //        //RealTime.UCTToStringTime(ptTime, szTime, 30);
                //        //if (szStatus.ToString().StartsWith("OK"))
                //        {
                //            DValue = pdValue;
                //            break;
                //        }
                //    }
                //}
            }
            catch {
                //
            }

            return DValue;
        }

        /// <summary>
        /// 得到数字量的实时状态数值
        /// </summary>
        /// <param name="tagname">测点</param>
        /// <returns></returns>
        public string GetDigitalSnapshotValue(string tagname) {
            double DValue = double.MinValue;
            string result = null;
            try {
                DateTime DT = DateTime.Now;
                DValue = this.GetArchiveValue(tagname, DT);
                result = int.Parse(DValue.ToString()).ToString();
            }
            catch {
                //
            }
            return result;
        }

        /// <summary>
        /// 获取某个点的实时数据(数据量)
        /// </summary>
        /// <param name="tagname">点名</param>
        /// <returns></returns>
        public string GetDigitalSnapshotValueName(string tagname) {
            string ReData = null;
            try {
                //根据需要确定曾加是否数字量的判断.IsDigital
                ReData = this.GetDigitalSnapshotValue(tagname).ToString();
            }
            catch {
                ReData = null;
            }
            return ReData;
        }

        /// <summary>
        /// 得到数字量的历史状态数值
        /// </summary>
        /// <param name="tagname">测点</param>
        /// <param name="dt">时间标签</param>
        /// <returns></returns>
        public string GetDigitalArchiveValue(string tagname, DateTime dt) {
            double DValue = double.MinValue;
            string result = null;
            try {
                DValue = this.GetArchiveValue(tagname, dt);
                result = int.Parse(DValue.ToString()).ToString();
            }
            catch {
                //
            }
            return result;
        }

        /// <summary>
        /// 得到数字量的历史状态数值
        /// </summary>
        /// <param name="tagname">测点</param>
        /// <param name="dt">时间标签</param>
        /// <returns></returns>
        public string GetDigitalArchiveValueName(string tagname, DateTime dt) {
            double DValue = double.MinValue;
            string result = null;
            try {
                DValue = this.GetArchiveValue(tagname, dt);
                result = int.Parse(DValue.ToString()).ToString();
            }
            catch {
                //
            }
            return result;
        }


        /// <summary>
        /// 秒级取数
        /// </summary>
        /// <param name="tagname"></param>
        /// <param name="isdigital"></param>
        /// <param name="stime"></param>
        /// <param name="etime"></param>
        /// <param name="interval"></param>
        /// <returns></returns>
        public List<double> GetHistoryDataListBySecondSpan(string tagname, bool isdigital, DateTime stime, DateTime etime, int interval) {
            TimeSpan ts = etime - stime;
            double d = Math.Ceiling(ts.TotalSeconds);
            int minus = int.Parse(d.ToString()); //ts.Days * 1440 + ts.Hours * 60 + ts.Minutes;
            int count = (minus / interval);

            object[] doubles = new object[count];
            try {
                System.UInt32 eDNAPoint = 0;
                int Index = 0;
                GetKey(tagname, stime, etime, interval, ref eDNAPoint, ref Index, DataStatus.Normal);
                StringBuilder szStatus = new StringBuilder(20);
                StringBuilder szTime = new StringBuilder(30);
                double pdValue = 0;
                int ptTime = 0;
                int i = 0;
                while (Index == 0) {
                    Index = History.DnaGetNextHistUTC(eDNAPoint, ref pdValue, ref ptTime, szStatus, 20);

                    if (Index == 0) {
                        doubles[i] = pdValue;
                    }
                    else
                        continue;
                    i++;
                }
            }
            catch {
                //
            }
            return new List<double>();
        }


        /// <summary>
        /// 得到测点时间区间内按时间间隔取值(分钟取数)
        /// </summary>
        /// <param name="tagname">测点</param>
        /// <param name="isdigital">是否为数字量</param>
        /// <param name="sdate">开始时间</param>
        /// <param name="edate">结束时间</param>
        /// <param name="span">时间间隔</param>
        /// <returns></returns>
        public List<double> GetHistoryDataListByMinuteSpan(string tagname, bool isdigital, DateTime stime, DateTime etime, int interval) {

            return GetHistoryDataListBySecondSpan(tagname, isdigital, stime, etime, interval * 60);
            /*TimeSpan ts = etime - stime;
            int minus = ts.Days * 1440 + ts.Hours * 60 + ts.Minutes;
            int count = (minus / interval);

            object[] doubles = new object[count];
            try
            {
                System.UInt32 eDNAPoint = 0;
                int Index = 0;
                GetKey(tagname, stime, etime, interval * 60, ref eDNAPoint, ref Index, DataStatus.Normal);
                StringBuilder szStatus = new StringBuilder(20);
                StringBuilder szTime = new StringBuilder(30);
                double pdValue = 0;
                int ptTime = 0;
                int i = 0;
                while (Index == 0)
                {
                    Index = History.DnaGetNextHistUTC(eDNAPoint, ref pdValue, ref ptTime, szStatus, 20);

                    if (Index == 0)
                    {
                        doubles[i] = pdValue;
                    }
                    else
                        continue;
                    i++;
                }
            }
            catch
            {
                //
            }
            return doubles;*/
        }

        /// <summary>
        /// 得到时间周期内的测点数据集(按个数取数)
        /// </summary>
        /// <param name="tagname">测点</param>
        /// <param name="isdigital">是否为模拟量</param>
        /// <param name="stime">开始日期</param>
        /// <param name="etime">结束日期</param>
        /// <param name="interval">取数个数</param>
        /// <returns>结果集</returns>
        public object[] GetHistoryDataListByCount(string tagname, bool isdigital, DateTime stime, DateTime etime, int interval) {
            //TimeSpan ts = etime - stime;
            //double d = Math.Ceiling(ts.TotalSeconds) / interval;
            //int seconds = int.Parse(d.ToString());//ts.Days * 1440 + ts.Hours * 60 + ts.Minutes;

            //return GetHistoryDataListBySecondSpan(tagname, isdigital, stime, etime, seconds);
            return null;
        }

        //Added by pyf 2013-09-16
        public List<TagValue> GetHistoryDataList(string tagname, DateTime stime, DateTime etime) {
            throw new NotImplementedException();
        }

        //End of Added.

        #endregion

        #region Value写操作

        /// <summary>
        /// 回写数据
        /// </summary>
        /// <param name="tagname">
        /// tagname是一个字符串组合 用逗号分割
        /// 分别为：ServerIP,ServerPort,ShortID
        /// </param>
        /// <param name="value">AI 数值， DI传入的值为TRUE FALSE</param>
        /// <returns></returns>
        public bool WriteSnapshotValue(string shortid, string value) {
            try {
                string[] strs = shortid.Split(',');
                string ServerIP = strs[0];
                string Port = strs[1];
                string ShortID = strs[2];
                int tagname = 0;
                int result = 0;
                result = UNIVSERV.eDnaMxUniversalInitialize(out tagname, true, true, true, 10240, "", "");
                if (result != 0) return false;
                result = UNIVSERV.eDnaMxUniversalDataConnect(tagname, ServerIP, int.Parse(Port), "", 0);
                if (result != 0) return false;
                UNIVSERV.eDnaMxAddRec(tagname, ShortID, -1, 0, -1, double.Parse(value));
                UNIVSERV.eDnaMxFlushUniversalRecord(tagname, 1);
                UNIVSERV.eDnaMxUniversalCloseSocket(tagname);

                return true;
            }
            catch (Exception ex) {
                string s = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 回写数据
        /// 
        /// </summary>
        /// <param name="tagname"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool WriteArchiveValue(string tagname, object time, string value) {
            return false;
        }


        #endregion

        #region  Value删除操作

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="tagname">测点</param>
        /// <param name="value">数值</param>
        /// <returns></returns>
        public bool DeleteValue(string tagname, DateTime stime, DateTime etime) {
            return true;
        }

        #endregion

        #region 无用代码

        ///// <summary>
        ///// 返回时间段内某点的标准差，stdev
        ///// EDNA需要修改
        ///// </summary>
        ///// <param name="tagname"></param>
        ///// <param name="stime"></param>
        ///// <param name="etime"></param>
        ///// <returns></returns>
        //public double GetStdevAtPeriod(string tagname, DateTime stime, DateTime etime)
        //{
        //    return 0;
        //}


        /// tagname是一个字符串组合 用逗号分割
        /// 前面的部分是历史服务 由站点名+服务名组成
        /// 后面为测点名(由站点名+服务名+点名组成)
        //public bool WriteSnapValue(string tagname, string value)
        //{
        //    string[] strs = tagname.Split(',');
        //    DateTime s = DateTime.Now;
        //    DateTime atime = DateTime.Parse("1970-1-1 08:00:00");
        //    int Start = (int)s.Subtract(atime).TotalSeconds;
        //    StringBuilder szError = new StringBuilder(20);

        //    //DI点 0x1003 false 0x1043 true  传入的值为TRUE FALSE
        //    //AI点 0x0003
        //    ushort state = 0x0003;
        //    if (value == "TRUE") state = 0x1043;
        //    else if (value == "FALSE") state = 0x1003;

        //    int j = History.DnaHistQueueAppendValue(strs[0], strs[1], Start, state, value, szError, 20); 
        //    if (j == 0)
        //    {
        //        int k = History.DnaHistFlushAppendValues(strs[0], strs[1], szError, 20);
        //        if (k == 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }

        //    return false;
        //}    

        /// <summary>
        /// 获取Key标识
        /// </summary>
        /// <param name="dm"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="tPeriod"></param>
        /// <returns></returns>
        private void GetKey(string dm, DateTime start, DateTime end, int tPeriod, ref System.UInt32 eDNAKey, ref int isOk, DataStatus dataStatus) {
            DateTime atime = DateTime.Parse("1970-1-1 08:00:00");
            int Start = (int)start.Subtract(atime).TotalSeconds;
            int End = (int)end.Subtract(atime).TotalSeconds;
            StringBuilder szStatus = new StringBuilder(20);
            StringBuilder szTime = new StringBuilder(30);
            if (dataStatus == DataStatus.Normal) {
                isOk = History.DnaGetHistSnapUTC(dm, Start, End, tPeriod, ref eDNAKey);
            }
            else if (dataStatus == DataStatus.Avg) {
                isOk = History.DnaGetHistAvgUTC(dm, Start, End, tPeriod, ref eDNAKey);
            }
            else if (dataStatus == DataStatus.Min) {
                isOk = History.DnaGetHistMinUTC(dm, Start, End, tPeriod, ref eDNAKey);
            }
            else if (dataStatus == DataStatus.Max) {
                isOk = History.DnaGetHistMaxUTC(dm, Start, End, tPeriod, ref eDNAKey);
            }
        }

        /// <summary>
        /// 获取最大值，最小值，平均值
        /// </summary>
        /// <param name="dm"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="dataStatus"></param>
        /// <returns></returns>
        private double GetValueOnStatus(string dm, DateTime start, DateTime end, DataStatus dataStatus) {
            double ReturnValue = double.MinValue;
            try {
                DateTime DT = DateTime.Now;
                int tPeriod = (int)end.Subtract(start).TotalSeconds;
                System.UInt32 eDNAPoint = 0;
                int Index = 0;
                GetKey(dm, start, end, tPeriod, ref eDNAPoint, ref Index, dataStatus);
                StringBuilder szStatus = new StringBuilder(20);
                StringBuilder szTime = new StringBuilder(30);
                double pdValue = 0;
                int ptTime = 0;
                while (Index == 0) {
                    Index = History.DnaGetNextHistUTC(eDNAPoint, ref pdValue, ref ptTime, szStatus, 20);

                    if (Index == 0) {
                        //保留时间
                        //RealTime.UCTToStringTime(ptTime, szTime, 30);
                        if (szStatus.ToString().StartsWith("OK")) {
                            ReturnValue = pdValue;
                            break;
                        }
                    }
                }
            }
            catch {
                //
            }
            return ReturnValue;
        }

        ///// <summary>
        ///// 获取日平均数据
        ///// </summary>
        ///// <param name="tagname">点名</param>
        ///// <param name="date">日期</param>
        ///// <returns></returns>
        //public string GetDayAvereage(string tagname, string date)
        //{
        //    return this.GetDayAvereageAsDouble(tagname, date).ToString("0.000");
        //}

        ///// <summary>
        ///// 获取日平均数据
        ///// </summary>
        ///// <param name="tagname">点名</param>
        ///// <param name="date">日期</param>
        ///// <returns></returns>
        //public double GetDayAvereageAsDouble(string tagname, string date)
        //{
        //    double ReturnValue = double.MinValue; ;

        //    if (string.IsNullOrEmpty(date)) return ReturnValue;
        //    DateTime stime = Convert.ToDateTime(date + " 00:00:00");
        //    DateTime etime = Convert.ToDateTime(date + " 23:59:59");

        //    try
        //    {
        //        ReturnValue = GetValueOnStatus(tagname, stime, etime, DataStatus.Avg);
        //    }
        //    catch
        //    {
        //        //
        //    }
        //    return ReturnValue;
        //}

        ///// <summary>
        ///// 获取每10分钟的平均数据
        ///// </summary>
        ///// <param name="tagname">点名</param>
        ///// <param name="date">日期</param>
        ///// <returns></returns>
        //public double GetTenMinuteAvereage(string tagname, DateTime dt)
        //{
        //    DateTime stime = dt.AddMinutes(-10);
        //    DateTime etime = dt;
        //    double ReturnValue = double.MinValue;
        //    try
        //    {
        //        ReturnValue = this.GetValueOnStatus(tagname, stime, etime, DataStatus.Avg);
        //    }
        //    catch
        //    {
        //        //
        //    }
        //    return ReturnValue;
        //}

        ///// <summary>
        ///// 获取指定时间段的平均数据
        ///// </summary>
        ///// <param name="tagname">点名</param>
        ///// <param name="date">日期</param>
        ///// <returns></returns>
        //public string GetSignAvereage(string tagname, DateTime stime, DateTime etime)
        //{
        //    return this.GetValueOnStatus(tagname, stime, etime, DataStatus.Avg).ToString("0.000");
        //}

        ///// <summary>
        ///// 获取指定时间段的平均数据
        ///// </summary>
        ///// <param name="tagname">点名</param>
        ///// <param name="date">日期</param>
        ///// <returns></returns>
        //public double GetSignAvereageAsDouble(string tagname, DateTime stime, DateTime etime)
        //{
        //    return this.GetValueOnStatus(tagname, stime, etime, DataStatus.Avg);
        //}

        ///// <summary>
        ///// 获取指定时间段的最大值
        ///// </summary>
        ///// <param name="tagname">点名</param>
        ///// <param name="date">日期</param>
        ///// <returns></returns>
        //public string GetSignMax(string tagname, DateTime stime, DateTime etime)
        //{
        //    return this.GetValueOnStatus(tagname, stime, etime, DataStatus.Max).ToString("0.000");
        //}

        ///// <summary>
        ///// 获取指定时间段的最小值
        ///// </summary>
        ///// <param name="tagname">点名</param>
        ///// <param name="date">日期</param>
        ///// <returns></returns>
        //public string GetSignMin(string tagname, DateTime stime, DateTime etime)
        //{
        //    return this.GetValueOnStatus(tagname, stime, etime, DataStatus.Min).ToString("0.000");
        //}


        #endregion


        #region ListData操作

        /// <summary>
        /// 将实时数据库的PointList赋值
        /// </summary>
        /// <param name="lttvs"></param>
        /// <returns></returns>
        public bool SetPointList(Dictionary<string, TagValue> lttvs, out string strError) {
            strError = "";

            return true;
        }

        /// <summary>
        /// 得到List表所有标签点的实时数据
        /// </summary>
        /// <param name="tagname"></param>
        /// <returns></returns>
        public bool GetSnapshotListData(ref Dictionary<string, TagValue> lttvs, out string strError) {
            strError = "";
            return true;
        }


        /// <summary>
        /// 得到List表所有标签点的历史数据
        /// </summary>
        /// <param name="tagname"></param>
        /// <returns></returns>
        public bool GetArchiveListData(ref Dictionary<string, TagValue> lttvs, DateTime stime, out string strError) {

            strError = "";
            return true;
        }

        #endregion

        #region 统计操作(Expression\Tag)

        public double ExpCurrentValue(string expression) {
            return 0.0;
        }

        public double ExpCalculatedData(string expression, DateTime stime, DateTime etime, string filter, SummaryType type) {
            return 0.0;
        }

        public bool ExpCalculatedData(string expression, DateTime stime, DateTime etime, string duration, string filter, SummaryType type, out double[] pdata) {
            pdata = null;

            return true;
        }

        public double TagCalculatedData(string tagname, DateTime stime, DateTime etime, string filter, SummaryType type) {
            return 0.0;
        }

        public bool TagCalculatedData(string tagname, DateTime stime, DateTime etime, string duration, string filter, SummaryType type, out double[] pdata) {
            pdata = null;

            return true;
        }

        public bool TagSummaryData(string tagname, DateTime stime, DateTime etime, string filter, out TagAllValue pdata) {
            pdata = null;

            return true;
        }

        #endregion

        #region Time操作

        public double GetExpressionTrueSecondTime(string expression, DateTime stime, DateTime etime) {
            return double.MinValue;
        }

        public double TimedCalculate(string expression, DateTime dttime) {
            return 0.0;
        }

        #endregion

    }
}