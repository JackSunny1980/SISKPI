using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Linq;
using System.Collections;

using PISDK;

using SIS.Loger;
using SIS.Evaluator;
using System.Configuration;

namespace SIS.DBControl {
    public class PIHelper : RTInterface {
        protected PISDK.PISDK pSdk;
        protected PISDK.Server piServer;       // PI���ݿ����������
        protected PISDK.PIPoints piPoints;     // PI���ݿ��������Ķ���
        protected PISDK.PIPoint piSnapPoint;   // PI���ݿ����������յĶ���
        protected PISDK.PIValue piValue;       // PI���ݿ��������ֵ�Ķ���

        protected static PISDK.PointList piPointList = new PointList();  //ȡ�������ݼ���

        //protected PITimeServer.PITime piTime;  // PI���ݿ��ʱ�����
        private static List<String> TagList = new List<string>();

        protected static string SIP = "";
        protected static string ConnString = "";

        private string _ErrorInfo = "";

        public string ErrorInfo() {
            return _ErrorInfo;
        }

        #region PIʵ��

        public PIHelper() {
            //this.ConnectToPI();
        }
        private static PIHelper _mInstance = null;

        public static PIHelper Instance() {
            return Instance(false);
        }

        /// <summary>
        /// Ϊ�˻������⣬��Ҫִ�йر������ٴ�����
        /// </summary>
        /// <param name="needclose"></param>
        /// <returns></returns>
        public static PIHelper Instance(bool needclose) {
            if (_mInstance == null) {
                _mInstance = new PIHelper();
            }
            if (needclose) {
                _mInstance.Close();
                _mInstance.ConnectToPI();
            }
            else
                _mInstance.ConnectToPI();

            if (!_mInstance.Connection())
                return null;

            return _mInstance;
        }

        #endregion

        #region PI˽�к���

        /// <summary>
        /// ��ȡ���еĵ�����
        /// </summary>
        /// <returns></returns>
        private PISDK.PIPoints GetPoints() {
            return piServer.PIPoints;
        }

        /// <summary>
        /// ��ȡĳ����
        /// </summary>
        /// <param name="tagname">����</param>
        /// <returns></returns>
        private PISDK.PIPoint GetPoint(string tagname) {
            try {
                return piServer.PIPoints[tagname];
            }
            catch {
                return null;
            }
        }


        /// <summary>
        /// �õ���������б�
        /// </summary>
        /// <param name="tagname"></param>
        /// <returns></returns>
        private PointAttributes GetPointAttributes(string tagname) {
            PIPoint point = piServer.PIPoints[tagname];
            return point.PointAttributes;
        }


        /// <summary>
        /// ��ȡĳ���������
        /// </summary>
        /// <param name="tagname">����</param>
        /// <returns></returns>
        private PISDK.DigitalState GetPointPIValue(string tagname) {
            PISDK.DigitalState ReData = null;
            try {
                ReData = piServer.PIPoints[tagname].Data.Snapshot.Value as PISDK.DigitalState;
            }
            catch (Exception ex) {
                _ErrorInfo = ex.Message;
                ReData = null;
            }
            return ReData;
        }

        private object[] getHistorys(string tagname, DateTime[] dts, int count) {
            object[] doubles = new object[count];
            int j = 0;

            double temp = double.MinValue;
            for (int i = 0; i < count; i++) {
                temp = GetArchiveValue(tagname, dts[i]);
                if (temp != double.MinValue) {
                    doubles[j] = (object)temp;

                }
                else {
                    //û�еõ���ֵ��ǰһ����ǩֵ
                    //if (j > 0)
                    doubles[j] = .00;//doubles[j - 1];
                }
                j++;

            }

            return doubles;
        }

        private object[] getDigitalHistorys(string tagname, DateTime[] dts, int count) {
            object[] doubles = new object[count];
            int j = 0;

            string temp = string.Empty;
            for (int i = 0; i < count; i++) {
                temp = GetDigitalArchiveValue(tagname, dts[i]);
                if (temp != string.Empty) {
                    doubles[j] = (object)temp;
                    j++;
                }

            }

            return doubles;
        }

        private DateTime[] GetDateList(DateTime s, DateTime e, ref int count) {
            return GetDateList(s, e, ref count, null);
        }

        private DateTime[] GetDateList(DateTime s, DateTime e, ref int count, object span) {
            if ((int)span == 0) span = 1;//{return GetDateList(s, e, ref count, 1);  }
            int TimeSpan = (int)span;
            TimeSpan ts = e - s;
            int minus = ts.Days * 1440 + ts.Hours * 60 + ts.Minutes;

            count = (minus / TimeSpan); //Ĭ��1����

            DateTime d = DateTime.MinValue;


            DateTime[] time = new DateTime[count];

            for (int j = 0; j < count; j++) {
                d = s.AddMinutes(j * TimeSpan);
                if (d > e) d = e;
                time[j] = d;
            }

            return time;
        }

        #endregion

        #region ���ݿ����

        public void Dispose() {
            Close();
        }

        public bool Connection() {
            try {
                string connectstr = ConfigurationManager.AppSettings["PIConnectionString"].ToString();

                //���Ӵ���� ��ִ�йرղ���
                if (ConnString != "" && ConnString != connectstr)
                    piServer.Close();

                if (piServer.Connected) return true;

                piServer.Open(connectstr);
                ConnString = connectstr;
            }
            catch (Exception ex) {
                _ErrorInfo = ex.Message;
                pSdk = null;
                return false;
            }
            return true;
        }

        public bool Connection(string ServerIP, string PIConnectionString) {
            try {
                //���Ӵ���� ��ִ�йرղ���
                if (ConnString != "" && ConnString != PIConnectionString)
                    piServer.Close();
                piServer.Open(PIConnectionString);

                ConnString = PIConnectionString;
            }
            catch (Exception ex) {
                _ErrorInfo = ex.Message;
                pSdk = null;
                return false;
            }
            return true;
        }

        public bool ConnectToPI() {
            try {
                if (pSdk == null || !piServer.Connected) {
                    pSdk = new PISDK.PISDK();
                    string piserver_name = ConfigurationManager.AppSettings["PIServer"].ToString();
                    string connectstr = ConfigurationManager.AppSettings["PIConnectionString"].ToString();
                    piServer = pSdk.Servers[piserver_name];

                    if (piServer.Connected) return true;
                    piServer.ConnectTimeout = 120;
                    piServer.Open(connectstr);
                }
            }
            catch (Exception ex) {
                pSdk = null;
                _ErrorInfo = ex.Message;
                return false;
            }
            return true;
        }

        public bool Close() {
            try {
                if (piServer != null && piServer.Connected)
                    piServer.Close();
            }
            catch (Exception ex) {
                pSdk = null;
                _ErrorInfo = ex.Message;
                return false;
            }
            return true;
        }

        /// <summary>
        /// ��÷�����ʱ��
        /// </summary>
        /// <returns></returns>
        public DateTime GetServerTime() {
            return piServer.ServerTime().LocalDate;
        }

        #endregion

        #region Point����

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="tagname"></param>
        /// <param name="isdigital"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public bool AddPoint(string tagname, bool isdigital, object desc) {
            try {
                PISDKCommon.NamedValues nv = new PISDKCommon.NamedValues();
                nv.Add("descriptor", ref desc);

                if (isdigital) {
                    piServer.PIPoints.Add(tagname, "classic", PISDK.PointTypeConstants.pttypDigital, nv);
                }
                else
                    piServer.PIPoints.Add(tagname, "classic", PISDK.PointTypeConstants.pttypFloat32, nv);
                return true;
            }
            catch (Exception ex) {
                _ErrorInfo = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="tagname"></param>
        /// <returns></returns>
        public bool DeletePoint(string tagname) {
            try {
                piServer.PIPoints.Remove(tagname);
                return true;
            }
            catch (Exception ex) {
                _ErrorInfo = ex.Message;
                return false;
            }

        }

        /// <summary>
        /// �ĵ�
        /// </summary>
        /// <param name="oKey"></param>
        /// <param name="nKey"></param>
        /// <returns></returns>
        public bool UpdatePoint(string oKey, string nKey) {
            try {
                piServer.PIPoints.Rename(oKey, nKey);
                return true;
            }
            catch (Exception ex) {
                _ErrorInfo = ex.Message;
                return false;
            }
        }


        public bool UpdateAttr(string tagname, string attr, string SetVal) {
            PISDK.PIPoint piSnapPoint = GetPoint(tagname);
            PISDKCommon.PIAsynchStatus pias = new PISDKCommon.PIAsynchStatus();
            PointAttribute ptatr = piSnapPoint.PointAttributes[attr];
            piSnapPoint.PointAttributes.ReadOnly = false;
            ptatr.Value = SetVal;
            piSnapPoint.PointAttributes.ReadOnly = true;

            return true;
        }

        /// <summary>
        /// ĳ�����Ƿ����
        /// </summary>
        /// <returns></returns>
        public bool ExistPoint(string tagname) {
            try {
                return (this.GetPoint(tagname) != null);
            }
            catch (Exception ex) {
                _ErrorInfo = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// �жϲ���Ƿ�Ϊ������
        /// </summary>
        /// <param name="tagname">���</param>
        /// <returns></returns>
        public bool PointIsDigital(string tagname) {
            try {
                bool flag = false;
                string type = GetPoint(tagname).PointType.ToString();

                if (type.ToLower().Equals("pttypdigital")) flag = true;
                return flag;
            }
            catch (Exception ex) {
                _ErrorInfo = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// �õ����ʵ�����
        /// </summary>
        /// <param name="tagname"></param>
        /// <returns></returns>
        public TagInfo GetPointInfo(string tagname) {
            TagInfo taginfo = new TagInfo();
            try {
                PIPoint point = piServer.PIPoints[tagname];
                taginfo.TagName = point.PointAttributes["tag"].Value.ToString().ToUpper();
                taginfo.TagDesc = point.PointAttributes["descriptor"].Value.ToString();
                taginfo.TagEngunit = point.PointAttributes["engunits"].Value.ToString().ToUpper();
                taginfo.TagIsDigital = PointIsDigital(tagname);
            }
            catch (Exception ex) {
                _ErrorInfo = ex.Message;
            }

            return taginfo;
        }

        /// <summary>
        /// ���������õ���㼯��  �����Ϣ
        /// </summary>
        /// <returns></returns>
        public List<TagInfo> GetPointInfoList(string filterexp) {
            List<string> taglist = GetPointList(filterexp);

            List<TagInfo> result = new List<TagInfo>();
            string[] attrs = new string[3];
            foreach (string pt in taglist) {
                attrs = pt.Split(',');
                TagInfo entity = new TagInfo();
                entity.TagName = attrs[0];
                entity.TagDesc = attrs[1];
                entity.TagEngunit = attrs[2];

                result.Add(entity);
            }

            return result;

        }


        public List<TagValue> GetPointListForSDLR(string condition) {
            List<TagValue> result = new List<TagValue>();

            try {
                if (condition == "") {
                    condition = "userint1=0";
                }

                PointList pl = piServer.GetPoints(condition);

                foreach (PIPoint pt in pl) {
                    if (pt.Data.Snapshot.IsGood()) {
                        result.Add(new TagValue(long.Parse(pt.PointAttributes["pointid"].Value.ToString()),
                                                pt.PointAttributes["tag"].Value.ToString(),
                                                pt.PointAttributes["descriptor"].Value.ToString(), pt.PointAttributes["engunits"].Value.ToString(),
                                                double.Parse(pt.Data.Snapshot.Value.ToString()),
                                                pt.Data.Snapshot.TimeStamp.LocalDate.ToString(),
                                                double.Parse(pt.Data.Snapshot.Value.ToString()),
                                                "")
                                   );
                    }
                    else {

                        result.Add(new TagValue(long.Parse(pt.PointAttributes["pointid"].Value.ToString()),
                                                pt.PointAttributes["tag"].Value.ToString(),
                                                pt.PointAttributes["descriptor"].Value.ToString(),
                                                pt.PointAttributes["engunits"].Value.ToString(),
                                                0,
                                                pt.Data.Snapshot.TimeStamp.LocalDate.ToString(),
                                                0,
                                                "")
                                   );
                    }
                }
            }
            catch (Exception ex) {
                _ErrorInfo = ex.Message;
            }

            return result;

        }

        /// <summary>
        /// ���������õ���㼯��
        /// </summary>
        /// <param name="WhereClause"></param>
        /// <param name="flag">�Ƿ�ʵʱֵ</param>
        /// <param name="time"></param>
        /// <returns></returns>
        public List<string> GetPointList(string WhereClause, bool flag, DateTime time) {
            List<string> l = new List<string>();
            try {
                //GetPointsSQL tagname=tagname* ����ģ����ϲ�ѯ and����           
                PointList pl = piServer.GetPointsSQL(WhereClause, null, null);

                if (flag) {
                    foreach (PIPoint pt in pl) {
                        l.Add(pt.PointAttributes["pointid"].Value.ToString() + "," + pt.PointAttributes["tag"].Value.ToString().ToUpper() + "," + pt.PointAttributes["descriptor"].Value.ToString().ToUpper() + "," + pt.PointAttributes["engunits"].Value.ToString().ToUpper() + "," + pt.Data.Snapshot.Value.ToString());
                    }
                }
                else {

                    PISDK.RetrievalTypeConstants rtc = PISDK.RetrievalTypeConstants.rtInterpolated;
                    PISDKCommon.PIAsynchStatus pias = new PISDKCommon.PIAsynchStatus();

                    foreach (PIPoint pt in pl) {
                        l.Add(pt.PointAttributes["pointid"].Value.ToString() + ","
                            + pt.PointAttributes["tag"].Value.ToString().ToUpper() + ","
                            + pt.PointAttributes["descriptor"].Value.ToString().ToUpper() + ","
                            + pt.PointAttributes["engunits"].Value.ToString().ToUpper() + ","
                            + pt.Data.ArcValue(time, rtc, pias).Value.ToString());
                    }

                }
            }
            catch (Exception ex) {
                _ErrorInfo = ex.Message;
            }
            return l;
        }

        /// <summary>
        /// ���������õ���㼯��
        /// </summary>
        /// <param name="WhereClause"></param>
        /// <returns></returns>
        public List<string> GetPointList(string WhereClause) {
            List<string> l = new List<string>();
            try {
                //GetPointsSQL tagname=tagname* ����ģ����ϲ�ѯ and����           
                PointList pl = piServer.GetPointsSQL(WhereClause, null, null);
                foreach (PIPoint pt in pl) {
                    l.Add(pt.PointAttributes["tag"].Value.ToString().ToUpper() + "," + pt.PointAttributes["descriptor"].Value.ToString().ToUpper() + "," + pt.PointAttributes["engunits"].Value.ToString().ToUpper());
                }
            }
            catch (Exception ex) {
                _ErrorInfo = ex.Message;
            }
            return l;
        }


        /// <summary>
        /// ������ֻ����CSģʽ��ʹ��
        /// </summary>
        /// <returns></returns>
        public List<string> GetPointList() {
            List<string> l = new List<string>();
            try {
                PISDKDlg.ApplicationObject AppObj = new PISDKDlg.ApplicationObject();
                PISDKDlg.TagSearch tagsearch = AppObj.TagSearch;

                PISDK.PointList PtList = new PISDK.PointList();
                PISDKCommon.NamedValues nv = new PISDKCommon.NamedValues();
                string piserver_name = ConfigurationManager.AppSettings["PIServer"].ToString();
                nv.LoadFromString(piserver_name);

                PtList = tagsearch.Show(nv, PISDKDlg.TagSearchOptions.tsoptSingleSelect);  //tsoptDisableServerPickList

                foreach (PIPoint pt in PtList) {
                    l.Add(pt.PointAttributes["tag"].Value.ToString().ToUpper() + "," + pt.PointAttributes["descriptor"].Value.ToString().ToUpper() + "," + pt.PointAttributes["engunits"].Value.ToString().ToUpper());
                }
            }
            catch (Exception ex) {
                _ErrorInfo = ex.Message;
            }
            return l;
        }

        #endregion

        #region Value����

        /// <summary>
        /// �жϱ�ǩֵ�Ƿ�����
        /// </summary>
        /// <param name="tagname"></param>
        /// <returns></returns>
        public bool PointValueIsGood(string tagname) {
            try {
                PISDK.PIPoint piSnapPoint = GetPoint(tagname);
                return piServer.PIPoints[tagname].Data.Snapshot.IsGood();
            }
            catch (Exception ex) {
                _ErrorInfo = ex.Message;
                return false;
            }
        }


        /// <summary>
        /// ��ȡĳ�����Shapshotֵ
        /// </summary>
        /// <param name="tagname">����</param>
        /// <returns></returns>
        public double GetSnapshotValue(string tagname) {
            double DValue = double.MinValue;
            try {
                DValue = Convert.ToDouble(piServer.PIPoints[tagname].Data.Snapshot.Value.ToString());
            }
            catch (Exception ex) {
                _ErrorInfo = ex.Message;
            }
            return DValue;
        }

        /// <summary>
        /// ��ȡĳ�����Shapshotֵ
        /// 
        /// </summary>
        /// <param name="tagname">����</param>
        /// <returns></returns>
        public double GetSnapshotValue(string tagname, out object timeStamp) {
            double DValue = double.MinValue;
            try {
                DValue = Convert.ToDouble(piServer.PIPoints[tagname].Data.Snapshot.Value.ToString());

                timeStamp = piServer.PIPoints[tagname].Data.Snapshot.TimeStamp;
            }
            catch (Exception ex) {
                _ErrorInfo = ex.Message;
                timeStamp = null;
            }
            return DValue;
        }

        /// <summary>
        /// ��ȡĳ�����Archiveֵ
        /// </summary>
        /// <param name="tagname">����</param>
        /// <returns></returns>
        public double GetArchiveValue(string tagname, DateTime dt) {
            double DValue = double.MinValue;
            try {
                PISDK.RetrievalTypeConstants rtc = PISDK.RetrievalTypeConstants.rtInterpolated;
                PISDKCommon.PIAsynchStatus pias = new PISDKCommon.PIAsynchStatus();
                PISDK.PIPoint piPoint = piServer.PIPoints[tagname];
                PISDK.PIValue _PIValue = piPoint.Data.ArcValue(dt, rtc, pias);
                DValue = Convert.ToDouble(_PIValue.Value.ToString());
            }
            catch (Exception ex) {
                _ErrorInfo = ex.Message;
            }
            return DValue;
        }


        /// <summary>
        /// �õ��鵵ֵ�����д���ʱ���ǩ
        /// </summary>
        /// <param name="tagname"></param>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public double GetArchiveValue(string tagname, out object timeStamp) {
            double DValue = double.MinValue;
            //double DValue = dou;

            DateTime dt = DateTime.Now;
            try {
                DValue = Convert.ToDouble(piServer.PIPoints[tagname].Data.ArcValue(dt, RetrievalTypeConstants.rtBefore, null).Value.ToString());
                timeStamp = piServer.PIPoints[tagname].Data.ArcValue(dt, RetrievalTypeConstants.rtBefore, null).TimeStamp.LocalDate;
            }
            catch (Exception ex) {
                _ErrorInfo = ex.Message;
                timeStamp = null;
            }
            return DValue;
        }


        /// <summary>
        /// �õ��ַ������͵ĵ�ʵʱֵ
        /// </summary>
        /// <param name="tagname"></param>
        /// <returns></returns>
        public string GetSnapshotStringValue(string tagname) {
            string strValue = "";
            try {
                strValue = piServer.PIPoints[tagname].Data.Snapshot.Value.ToString();
            }
            catch (Exception ex) {
                _ErrorInfo = ex.Message;
            }

            return strValue;
        }


        /// <summary>
        /// �õ���������ʵʱ״̬��ֵ
        /// </summary>
        /// <param name="tagname">���</param>
        /// <returns></returns>
        public string GetDigitalSnapshotValue(string tagname) {
            string state = null;
            try {
                state = GetPointPIValue(tagname).Code.ToString();
                if (state.Equals("253")) //pt created
                    state = null;
            }
            catch (Exception ex) {
                _ErrorInfo = ex.Message;
            }

            return state;
        }

        /// <summary>
        /// ��ȡĳ���������
        /// </summary>
        /// <param name="tagname">����</param>
        /// <returns></returns>
        public string GetDigitalSnapshotValueName(string tagname) {
            string ReData = null;
            try {
                PISDKCommon.PIAsynchStatus pias = new PISDKCommon.PIAsynchStatus();
                PISDK.PIPoint piPoint = piServer.PIPoints[tagname];
                PISDK.PIValue _PIValue = piPoint.Data.Snapshot;
                PISDK.DigitalState myDigState = null;
                //if (_PIValue.Value.GetType().IsCOMObject)
                {
                    myDigState = (PISDK.DigitalState)_PIValue.Value;
                    ReData = myDigState.Name.ToString();
                }
            }
            catch (Exception ex) {
                _ErrorInfo = ex.Message;
                ReData = null;
            }
            return ReData;
        }

        /// <summary>
        /// �õ�����������ʷ״̬��ֵ
        /// </summary>
        /// <param name="tagname">���</param>
        /// <param name="dt">ʱ���ǩ</param>
        /// <returns></returns>
        public string GetDigitalArchiveValue(string tagname, DateTime dt) {
            string ReData = null;
            try {
                PISDK.RetrievalTypeConstants rtc = PISDK.RetrievalTypeConstants.rtInterpolated;
                PISDKCommon.PIAsynchStatus pias = new PISDKCommon.PIAsynchStatus();
                PISDK.PIPoint piPoint = piServer.PIPoints[tagname];
                PISDK.PIValue _PIValue = piPoint.Data.ArcValue(dt, rtc, pias);
                PISDK.DigitalState myDigState = null;
                myDigState = (PISDK.DigitalState)_PIValue.Value;
                ReData = myDigState.Code.ToString();
            }
            catch (Exception ex) {
                _ErrorInfo = ex.Message;
            }
            return ReData;
        }

        /// <summary>
        /// ��ȡĳ���������
        /// </summary>
        /// <param name="tagname">����</param>
        /// <returns></returns>
        public string GetDigitalArchiveValueName(string tagname, DateTime dt) {
            string ReData = null;
            try {
                PISDK.RetrievalTypeConstants rtc = PISDK.RetrievalTypeConstants.rtInterpolated;
                PISDKCommon.PIAsynchStatus pias = new PISDKCommon.PIAsynchStatus();
                PISDK.PIPoint piPoint = piServer.PIPoints[tagname];
                PISDK.PIValue _PIValue = piPoint.Data.ArcValue(dt, rtc, pias);
                PISDK.DigitalState myDigState = null;
                //if (_PIValue.Value.GetType().IsCOMObject)
                {
                    myDigState = (PISDK.DigitalState)_PIValue.Value;
                    ReData = myDigState.Name.ToString();
                }
            }
            catch (Exception ex) {
                _ErrorInfo = ex.Message;
            }
            return ReData;
        }

        //Added by pyf 2013-09-16
        public List<TagValue> GetHistoryDataList(string tagname, DateTime stime, DateTime etime) {
            throw new NotImplementedException();
        }

        //End of Added.

        /// <summary>
        /// 
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
            int seconds = Convert.ToInt32(d);//ts.Days * 1440 + ts.Hours * 60 + ts.Minutes;
            int count = (seconds / interval) + 1;

            //object[] doubles = new object[count];
            List<double> Results = new List<double>();
            try {
                PISDK.IPIData2 pidata2 = (PISDK.IPIData2)this.GetPoint(tagname).Data;
                PISDK.PIValues vals = pidata2.InterpolatedValues2(stime, etime, interval.ToString() + "s", "", PISDK.FilteredViewConstants.fvRemoveFiltered, null);
                if (!isdigital) {
                    for (int i = 1; i <= count; i++) {
                        if (!vals[i].Value.GetType().IsCOMObject)//û�����ݵõ���һ������ֵ                        
                        {
                            Results.Add(Convert.ToDouble(vals[i].Value));
                        }
                    }
                }
                else {                    
                        for (int i = 1; i <= count; i++) {
                            Results.Add(((PISDK.DigitalState)vals[i].Value).Code == 248 
                                ? 0.0f :Convert.ToDouble(((PISDK.DigitalState)vals[i].Value).Code));
                        }                    
                }


            }
            catch (Exception ex) {
                _ErrorInfo = ex.Message;  
            }

            return Results;
        }

        /// <summary>
        /// �õ����ʱ�������ڰ�ʱ����ȡֵ
        /// </summary>
        /// <param name="tagname">���</param>
        /// <param name="isdigital">�Ƿ�Ϊ������</param>
        /// <param name="sdate">��ʼʱ��</param>
        /// <param name="edate">����ʱ��</param>
        /// <param name="span">ʱ����</param>
        /// <returns></returns>
        public List<double> GetHistoryDataListByMinuteSpan(string tagname, bool isdigital, DateTime stime, DateTime etime, int interval) {
            return GetHistoryDataListBySecondSpan(tagname, isdigital, stime, etime, interval * 60);
        }

        /// <summary>
        /// �õ�ʱ�������ڵĲ�����ݼ�(������ȡ��)
        /// </summary>
        /// <param name="tagname">���</param>
        /// <param name="isdigital">�Ƿ�Ϊģ����</param>
        /// <param name="stime">��ʼ����</param>
        /// <param name="etime">��������</param>
        /// <param name="interval">ȡ������</param>
        /// <returns>�����</returns>
        public object[] GetHistoryDataListByCount(string tagname, bool isdigital, DateTime stime, DateTime etime, int count) {
            TimeSpan ts = etime - stime;
            double d = Math.Ceiling(ts.TotalSeconds);
            int interval = int.Parse(d.ToString()) / count;//ts.Days * 1440 + ts.Hours * 60 + ts.Minutes;

            object[] doubles = new object[count];
            try {
                PISDK.IPIData2 pidata2 = (PISDK.IPIData2)this.GetPoint(tagname).Data;

                PISDK.PIValues vals = pidata2.InterpolatedValues2(stime, etime, interval.ToString() + "s", "", PISDK.FilteredViewConstants.fvRemoveFiltered, null);


                if (!isdigital) {
                    if (vals == null) {
                        for (int i = 0; i < count; i++) {
                            doubles[i] = ".00";
                        }
                    }
                    else {
                        for (int i = 1; i <= count; i++) {
                            if (vals[i].Value.GetType().IsCOMObject)
                            //û�����ݵõ���һ������ֵ
                            {
                                if (i == 1)
                                    doubles[i - 1] = ".00";
                                else
                                    doubles[i - 1] = doubles[i - 2];
                            }
                            else
                                doubles[i - 1] = vals[i].Value.ToString();
                        }
                    }
                }
                else {
                    if (vals == null) {
                        for (int i = 0; i < count; i++) {
                            doubles[i] = "0";
                        }
                    }
                    else {
                        for (int i = 1; i <= count; i++) {
                            doubles[i - 1] = ((PISDK.DigitalState)vals[i].Value).Code == 248 ? "0" : ((PISDK.DigitalState)vals[i].Value).Code.ToString();
                        }
                    }
                }


            }
            catch (Exception ex) {
                _ErrorInfo = ex.Message;
                if (!isdigital) {
                    for (int i = 0; i < count; i++) {
                        doubles[i] = ".00";
                    }
                }
                else {
                    for (int i = 0; i < count; i++) {
                        doubles[i] = "0";
                    }
                }

            }

            return doubles;
        }

        #endregion


        #region Valueд����

        /// <summary>
        /// ��д����
        /// </summary>
        /// <param name="tagname"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool WriteSnapshotValue(string tagname, string value) {
            try {
                PISDK.PIPoint piSnapPoint = GetPoint(tagname);
                PISDKCommon.PIAsynchStatus pias = new PISDKCommon.PIAsynchStatus();
                piSnapPoint.PointAttributes.ReadOnly = false;
                piSnapPoint.Data.UpdateValue(value, 0, PISDK.DataMergeConstants.dmReplaceDuplicates, pias);
                piSnapPoint.PointAttributes.ReadOnly = true;
                return true;
            }
            catch (Exception ex) {
                _ErrorInfo = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// ��д����
        /// </summary>
        /// <param name="tagname"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool WriteArchiveValue(string tagname, object time, string value) {
            try {
                PISDK.PIPoint piSnapPoint = GetPoint(tagname);
                PISDKCommon.PIAsynchStatus pias = new PISDKCommon.PIAsynchStatus();
                piSnapPoint.PointAttributes.ReadOnly = false;
                piSnapPoint.Data.UpdateValue(value, time, PISDK.DataMergeConstants.dmReplaceDuplicates, pias);
                piSnapPoint.PointAttributes.ReadOnly = true;
                return true;
            }
            catch (Exception ex) {
                _ErrorInfo = ex.Message;
                return false;
            }
        }

        #endregion

        #region Valueɾ������

        /// <summary>
        /// ��д����
        /// </summary>
        /// <param name="tagname"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool DeleteValue(string tagname, DateTime stime, DateTime etime) {
            try {
                return true;
            }
            catch (Exception ex) {
                _ErrorInfo = ex.Message;
                return false;
            }
        }


        #endregion

        #region ListData����

        /// <summary>
        /// ��ʵʱ���ݿ��PointList��ֵ
        /// </summary>
        /// <param name="lttvs"></param>
        /// <returns></returns>
        public bool SetPointList(Dictionary<string, TagValue> lttvs, out string strError) {
            if (piPointList == null || piPointList.Count == 0) {
                piPointList = new PointList();
            }

            if (piPointList == null || piPointList.Count == 0) {
                piPointList = new PointList();
            }

            strError = "";
            //List<String> PointNameList = new List<String>();
            List<string> PointList = new List<string>(lttvs.Keys);
            PISDK.PIPoint pt;
            for (int i = 0; i < PointList.Count; i++) {
                try {
                    pt = piServer.PIPoints[PointList[i]];
                    if ((!TagList.Contains(PointList[i])) && (pt != null)) {
                        piPointList.Add(pt);
                        TagList.Add(PointList[i]);
                    }
                }
                catch (System.Exception ex) {
                    strError += PointList[i] + ",";
                    PointList.RemoveAt(i);
                    i = i - 1;
                    continue;
                }
            }

            foreach (PIPoint Point in piPointList) {
                Console.WriteLine(Point.Name);
            }

            if (strError == "") {
                return true;
            }
            else {
                strError = "ʵʱ���ݿ���δ�ҵ��ĵ�: " + strError;
                return false;
            }
        }

        /// <summary>
        /// �õ�List�����б�ǩ���ʵʱ����
        /// </summary>
        /// <param name="tagname"></param>
        /// <returns></returns>
        public bool GetSnapshotListData(ref Dictionary<string, TagValue> lttvs, out string strError) {
            try {
                //���
                //SetPointList
                if (piPointList == null || piPointList.Count == 0) {
                    strError = "PointListδ��ֵ�����ȵ��ú���SetPointList......";

                    return false;
                }

                PISDK.ListData ltData;
                PISDK.PointValues pvsSnaps;
                PISDK.PointValue pvValue;
                PISDKCommon.NamedValues nvsErr;

                ltData = piPointList.Data;

                //Must range from 1 to the value of the collection's Count property
                pvsSnaps = ltData.get_Snapshot(out nvsErr);

                for (int i = 0; i < pvsSnaps.Count; i++) {
                    pvValue = pvsSnaps[i + 1];

                    if (pvValue.PIPoint.PointType == PointTypeConstants.pttypDigital) {
                        //Digital��
                        string tagname = pvValue.PIPoint.Name.ToUpper();

                        lttvs[tagname].TagQulity = pvValue.PIValue.IsGood() == true ? 0 : 1;

                        PISDK.DigitalState ReData = pvValue.PIValue.Value as PISDK.DigitalState;

                        lttvs[tagname].TagStringValue = ReData.Code.ToString();

                        lttvs[tagname].TagTime = pvValue.PIValue.TimeStamp.LocalDate.ToString("yyyy-MM-dd HH:mm:ss");

                    }
                    else if (pvValue.PIPoint.PointType == PointTypeConstants.pttypFloat16
                        || pvValue.PIPoint.PointType == PointTypeConstants.pttypFloat32
                        || pvValue.PIPoint.PointType == PointTypeConstants.pttypFloat64
                        || pvValue.PIPoint.PointType == PointTypeConstants.pttypInt16
                        || pvValue.PIPoint.PointType == PointTypeConstants.pttypInt32) {
                        //any float , any intergrator 
                        string tagname = pvValue.PIPoint.Name.ToUpper();

                        lttvs[tagname].TagQulity = pvValue.PIValue.IsGood() == true ? 0 : 1;
                        lttvs[tagname].TagStringValue = Convert.ToString(pvValue.PIValue.Value);//.ToString();
                        lttvs[tagname].TagTime = pvValue.PIValue.TimeStamp.LocalDate.ToString("yyyy-MM-dd HH:mm:ss");

                    }
                    else {
                        //other
                        string tagname = pvValue.PIPoint.Name.ToUpper();

                        lttvs[tagname].TagQulity = pvValue.PIValue.IsGood() == true ? 0 : 1;
                        lttvs[tagname].TagStringValue = Convert.ToString(pvValue.PIValue.Value);//.ToString();
                        lttvs[tagname].TagTime = pvValue.PIValue.TimeStamp.LocalDate.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }

            }
            catch (System.Exception ex) {
                strError = ex.ToString();

                return false;
            }

            strError = "";
            return true;
        }


        /// <summary>
        /// �õ�List�����б�ǩ�����ʷ����,����ʱlttvs��ת��Ϊ��д
        /// </summary>
        /// <param name="tagname"></param>
        /// <returns></returns>
        public bool GetArchiveListData(ref Dictionary<string, TagValue> lttvs, DateTime stime, out string strError) {
            //��ӵ�ListData
            try {
                //���
                //SetPointList
                if (piPointList == null || piPointList.Count == 0) {
                    strError = "PointListδ��ֵ�����ȵ��ú���SetPointList......";
                    return false;
                }

                PISDK.ListData ltData;
                PISDK.PointValues pvsArchs;
                PISDK.PointValue pvValue;
                PISDKCommon.NamedValues nvsErr;

                ltData = piPointList.Data;

                //Must range from 1 to the value of the collection's Count property
                pvsArchs = ltData.ArcValue(stime, RetrievalTypeConstants.rtInterpolated, out nvsErr);
                //(out nvsErr);

                for (int i = 0; i < pvsArchs.Count; i++) {
                    pvValue = pvsArchs[i + 1];
                    string tagname = pvValue.PIPoint.Name.ToUpper();
                    if (!lttvs.ContainsKey(tagname)) continue;
                    if (pvValue.PIPoint.PointType == PointTypeConstants.pttypDigital) {
                        //Digital��                       
                        lttvs[tagname].TagQulity = pvValue.PIValue.IsGood() == true ? 0 : 1;
                        PISDK.DigitalState ReData = pvValue.PIValue.Value as PISDK.DigitalState;
                        lttvs[tagname].TagStringValue = ReData.Code.ToString();
                        lttvs[tagname].TagTime = pvValue.PIValue.TimeStamp.LocalDate.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else {
                        //��Digital��
                        //string tagname = pvValue.PIPoint.Name.ToUpper();
                        lttvs[tagname].TagQulity = pvValue.PIValue.IsGood() == true ? 0 : 1;
                        lttvs[tagname].TagStringValue = pvValue.PIValue.Value.ToString();
                        lttvs[tagname].TagTime = pvValue.PIValue.TimeStamp.LocalDate.ToString("yyyy-MM-dd HH:mm:ss");

                    }
                }

            }
            catch (System.Exception ex) {
                strError = ex.ToString();
                return false;
            }

            strError = "";
            return true;
        }

        #endregion

        #region  ͳ�Ʋ���(Expression\Tag)

        /// <summary>
        /// ĳ���ʽ��ʵʱֵ
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="dttime"></param>
        /// <returns></returns>
        public double ExpCurrentValue(string expression) {
            double ReturnValue = double.MinValue;

            try {
                PISDK.IPICalculation ipicalc = (IPICalculation)piServer;
                PIValues valsum;

                DateTime dtTime = GetServerTime();

                valsum = ipicalc.TimedCalculate(dtTime, expression);

                if (valsum.Count == 1) {
                    ReturnValue = (double)valsum[1].Value;
                }


            }
            catch (Exception ex) {
                _ErrorInfo = ex.Message;
            }

            return ReturnValue;
        }

        public double ExpCalculatedData(string expression, DateTime stime, DateTime etime, string filter, SummaryType type) {
            //IPICalculation 
            //Dim srv As Server
            //Dim ipiCalc As IPICalculation
            //Dim vals1 As PIValues, vals2 As PIValues, vals3 As PIValues
            //Dim expr1 As String, expr2 As String, startTime As String, endTime As String
            //Dim testTimes As Variant, cnt As Long, I As Long

            //Dim bCheck As Boolean, bValCheck As Boolean

            //    startTime = "y" ' yesterday midnight
            //    endTime = "y+1h"
            //    expr1 = "if 'cdm158' = ""auto"" then 1 else 0"
            //    expr2 = "'sinusoid' + 'cdt158'"

            //    Set srv = Servers("localhost")
            //    Set ipiCalc = srv                        ' Get pointer to IPICalculation Interface


            //' note that the SampleInterval argument is ignored if stRecordedValues is the SampleType
            //    Set vals1 = ipiCalc.Calculate(startTime, endTime, expr1, stRecordedValues, "")
            //    cnt = vals1.Count

            //ExpressionSummaries
            //This method creates a data stream from an arbitrary expression and calculate one or more summaries on the expression result. The method returns a NamedValues collection with the requested summaries as described below under Remarks. This method is currently not supported for PI2 servers.
            //Syntax
            //object.ExpressionSummaries StartTime, EndTime, SummaryDuration, Expression, SummaryType, CalculationBasis, stSampleType, SampleInterval, asynchStatus
            //Expression
            // A string containing the expression to be evaluated.  The syntax for the expression follows the Performance Equation syntax as described in the server documentation.
            //Syntax
            //object.PercentTrue StartTime, EndTime, CalculationDuration, Expression, SampleType, SampleInterval, asynchStatus

            double ReturnValue = double.MinValue;


            try {
                PISDK.IPICalculation ipicalc = (IPICalculation)piServer;

                PISDKCommon.NamedValues nvsSum;
                PIValues valsum;

                double dpercent = 0.0;

                if (filter != "") {
                    valsum = ipicalc.PercentTrue(stime, etime, "", filter);

                    if (valsum.Count == 1) {
                        dpercent = (double)valsum[1].Value;
                    }

                    //wugh,add at 2013.4.8
                    if (dpercent < 0.00001) {
                        return ReturnValue;
                    }
                }
                else {
                    dpercent = 100;
                }

                /////////////////////////////////////////////////////////////////////////////////////
                //
                if (dpercent < 100) {
                    int count = 0;
                    int startIndex = 0;
                    List<int> positionIndex = new List<int>();

                    while (true) {
                        int y = expression.IndexOf("'", startIndex);
                        if (y != -1) {
                            //��¼�ַ�("'")λ��
                            positionIndex.Add(y);

                            count++;
                            startIndex = y + 1;
                        }
                        else {
                            break;
                        }
                    }

                    if (count % 2 != 0) {
                        //error
                        return ReturnValue;
                    }

                    //Dictionary<string, double> tagdic = new Dictionary<string, double>();
                    string newexpression = expression;

                    for (int i = 0; i < count; i = i + 2) {
                        string tagname = expression.Substring(positionIndex[i] + 1, positionIndex[i + 1] - positionIndex[i] - 1);

                        double tagvalue = TagCalculatedData(tagname, stime, etime, "", type);

                        //tagdic.Add(tagname, tagvalue);

                        tagname = "'" + tagname + "'";

                        newexpression = newexpression.Replace(tagname, tagvalue.ToString());
                    }

                    //ReturnValue = Evaluator.Evaluator.EvaluateToDouble(newexpression);
                    ReturnValue = Evaluator.Evaluator.EvaluateToDouble(newexpression);

                    return ReturnValue;
                }

                ////////////////////////////////////////////////////////////////////////////
                valsum = null;

                switch (type) {
                    case SummaryType.asTotal:
                        nvsSum = ipicalc.ExpressionSummaries(stime, etime, "", expression, ArchiveSummariesTypeConstants.asTotal);
                        valsum = (PIValues)nvsSum["Total"].Value;
                        break;

                    case SummaryType.asMinimum:
                        nvsSum = ipicalc.ExpressionSummaries(stime, etime, "", expression, ArchiveSummariesTypeConstants.asMinimum);
                        valsum = (PIValues)nvsSum["Minimum"].Value;
                        break;

                    case SummaryType.asMaximum:
                        nvsSum = ipicalc.ExpressionSummaries(stime, etime, "", expression, ArchiveSummariesTypeConstants.asMaximum);
                        valsum = (PIValues)nvsSum["Maximum"].Value;
                        break;

                    case SummaryType.asStdDev:
                        nvsSum = ipicalc.ExpressionSummaries(stime, etime, "", expression, ArchiveSummariesTypeConstants.asStdDev);
                        valsum = (PIValues)nvsSum["StdDev"].Value;
                        break;

                    case SummaryType.asRange:
                        nvsSum = ipicalc.ExpressionSummaries(stime, etime, "", expression, ArchiveSummariesTypeConstants.asRange);
                        valsum = (PIValues)nvsSum["Range"].Value;
                        break;

                    case SummaryType.asAverage:
                        nvsSum = ipicalc.ExpressionSummaries(stime, etime, "", expression, ArchiveSummariesTypeConstants.asAverage);
                        valsum = (PIValues)nvsSum["Average"].Value;
                        break;

                    case SummaryType.asPStdDev:
                        nvsSum = ipicalc.ExpressionSummaries(stime, etime, "", expression, ArchiveSummariesTypeConstants.asPStdDev);
                        valsum = (PIValues)nvsSum["PStdDev"].Value;
                        break;

                    //case SummaryType.asTotal:
                    //nvsSum = ipicalc.ExpressionSummaries(stime, etime, "", expression, ArchiveSummariesTypeConstants.asAverage);
                    //valsum = (PIValues)nvsSum["Average"].Value;
                    //break;

                    default:
                        nvsSum = ipicalc.ExpressionSummaries(stime, etime, "", expression, ArchiveSummariesTypeConstants.asAverage);
                        valsum = (PIValues)nvsSum["Average"].Value;
                        break;
                }

                if (valsum.Count == 1) {
                    ReturnValue = (double)valsum[1].Value;
                }

            }
            catch (Exception ex) {
                LogUtil.LogMessage(ex.Message);
                //_ErrorInfo = ex.Message;
            }

            return ReturnValue;
        }

        public bool ExpCalculatedData(string expression, DateTime stime, DateTime etime, string duration, string filter, SummaryType type, out double[] pdata) {
            //����ÿһ���㣬Ȼ���滻���ʽ,ʹ��.NET���ʽ���㺯�����㡣
            //Ȼ�����ExpressionSummaries���㣬����

            //PITimeServer.ITimeInterval ti;
            //PITimeServer.PITime pitime= new PITimeServer.PITimeClass();
            //pitime.LocalDate = stime;
            //DateTime dtime = ti.AddIntervals(pitime, 100); //PITimeServer.ITimeInterval

            //ti.AddIntervals = stime;

            //ti = new PITimeServer.ITimeInterval("2h");
            //ti = PITimeServer.ITimeIntervals("2h");


            //������������������������������������������������������������������������
            //��Ҫ���ƣ�����������������������������

            pdata = null;

            return true;
        }

        public double TagCalculatedData(string tagname, DateTime stime, DateTime etime, string filter, SummaryType type) {
            double ReturnValue = double.MinValue;

            try {
                PISDK.PIPoint piPoint = piServer.PIPoints[tagname];


                if (filter.Trim() != "") {
                    /////////////////////////////////////////////////////////////////////////////////
                    PISDK.IPIData2 ipda = (PISDK.IPIData2)piPoint.Data;

                    PISDKCommon.NamedValues nvsSum;
                    PIValues valsum;
                    //ReturnValue = Convert.ToDouble(ipda.Snapshot.Value);

                    ///////////////////////////////////////////////////////////////////////
                    PISDK.IPICalculation ipicalc = (IPICalculation)piServer;

                    if (filter != "") {
                        double dpercent = 0.0;
                        valsum = ipicalc.PercentTrue(stime, etime, "", filter);

                        if (valsum.Count == 1) {
                            dpercent = (double)valsum[1].Value;
                        }

                        //wugh,add at 2013.4.8
                        if (dpercent < 0.00001) {
                            return ReturnValue;
                        }

                    }
                    /////////////////////////////////////////////////////////////


                    switch (type) {
                        case SummaryType.asTotal:
                            nvsSum = ipda.FilteredSummaries(stime, etime, "", filter, ArchiveSummariesTypeConstants.asTotal);
                            valsum = (PIValues)nvsSum["Total"].Value;
                            break;

                        case SummaryType.asMinimum:
                            nvsSum = ipda.FilteredSummaries(stime, etime, "", filter, ArchiveSummariesTypeConstants.asMinimum);
                            valsum = (PIValues)nvsSum["Minimum"].Value;
                            break;

                        case SummaryType.asMaximum:
                            nvsSum = ipda.FilteredSummaries(stime, etime, "", filter, ArchiveSummariesTypeConstants.asMaximum);
                            valsum = (PIValues)nvsSum["Maximum"].Value;
                            break;

                        case SummaryType.asStdDev:
                            nvsSum = ipda.FilteredSummaries(stime, etime, "", filter, ArchiveSummariesTypeConstants.asStdDev);
                            valsum = (PIValues)nvsSum["StdDev"].Value;
                            break;

                        case SummaryType.asRange:
                            nvsSum = ipda.FilteredSummaries(stime, etime, "", filter, ArchiveSummariesTypeConstants.asRange);
                            valsum = (PIValues)nvsSum["Range"].Value;
                            break;

                        case SummaryType.asAverage:
                            nvsSum = ipda.FilteredSummaries(stime, etime, "", filter, ArchiveSummariesTypeConstants.asAverage);
                            valsum = (PIValues)nvsSum["Average"].Value;
                            break;

                        case SummaryType.asPStdDev:
                            nvsSum = ipda.FilteredSummaries(stime, etime, "", filter, ArchiveSummariesTypeConstants.asPStdDev);
                            valsum = (PIValues)nvsSum["PStdDev"].Value;
                            break;

                        //case SummaryType.asTotal:
                        //    nvsSum = ipda.FilteredSummaries(stime, etime, "", filter, ArchiveSummariesTypeConstants.asAverage);
                        //    valsum = (PIValues)nvsSum["Average"].Value;
                        //    break;

                        default:
                            nvsSum = ipda.FilteredSummaries(stime, etime, "", filter, ArchiveSummariesTypeConstants.asAverage);
                            valsum = (PIValues)nvsSum["Average"].Value;
                            break;
                    }

                    if (valsum.Count == 1) {
                        //LogUtil.LogMessage(valsum[1].Value.ToString());
                        ReturnValue = (double)valsum[1].Value;
                    }

                }
                else {
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //No Filter
                    PISDK.PIValue rv = null;

                    switch (type) {
                        case SummaryType.asTotal:
                            rv = piPoint.Data.Summary(stime, etime, PISDK.ArchiveSummaryTypeConstants.astTotal);
                            break;

                        case SummaryType.asMinimum:
                            rv = piPoint.Data.Summary(stime, etime, PISDK.ArchiveSummaryTypeConstants.astMaximum);
                            break;

                        case SummaryType.asMaximum:
                            rv = piPoint.Data.Summary(stime, etime, PISDK.ArchiveSummaryTypeConstants.astMaximum);
                            break;

                        case SummaryType.asStdDev:
                            rv = piPoint.Data.Summary(stime, etime, PISDK.ArchiveSummaryTypeConstants.astStdDev);
                            break;

                        case SummaryType.asRange:
                            rv = piPoint.Data.Summary(stime, etime, PISDK.ArchiveSummaryTypeConstants.astRange);
                            break;

                        case SummaryType.asAverage:
                            rv = piPoint.Data.Summary(stime, etime, PISDK.ArchiveSummaryTypeConstants.astAverage);
                            break;

                        case SummaryType.asPStdDev:
                            rv = piPoint.Data.Summary(stime, etime, PISDK.ArchiveSummaryTypeConstants.astPStdDev);
                            break;

                        //case SummaryType.asTotal:
                        //    rv = piPoint.Data.Summary(stime, etime, PISDK.ArchiveSummaryTypeConstants.astMaximum);
                        //    break;

                        default:
                            rv = piPoint.Data.Summary(stime, etime, PISDK.ArchiveSummaryTypeConstants.astAverage);
                            break;

                    }

                    ReturnValue = Convert.ToDouble(rv.Value);
                }

            }
            catch (Exception ex) {
                LogUtil.LogMessage(ex.Message);
                //_ErrorInfo = ex.Message;
            }

            return ReturnValue;
        }

        public bool TagCalculatedData(string tagname, DateTime stime, DateTime etime, string duration, string filter, SummaryType type, out double[] pdata) {
            pdata = null;

            try {
                PISDK.PIPoint piPoint = piServer.PIPoints[tagname];

                /////////////////////////////////////////////////////////////////////////////////
                PISDK.IPIData2 ipda = (PISDK.IPIData2)piPoint.Data;
                PISDKCommon.NamedValues nvsSum;
                PIValues valsum;

                //////////////////////////////////////////////////////////////
                PISDK.IPICalculation ipicalc = (IPICalculation)piServer;

                if (filter != "") {
                    double dpercent = 0.0;
                    valsum = ipicalc.PercentTrue(stime, etime, "", filter);

                    if (valsum.Count == 1) {
                        dpercent = (double)valsum[1].Value;
                    }

                    //wugh,add at 2013.4.8
                    if (dpercent < 0.00001) {
                        return false;
                    }

                }

                switch (type) {
                    case SummaryType.asTotal:
                        nvsSum = ipda.FilteredSummaries(stime, etime, duration, filter, ArchiveSummariesTypeConstants.asTotal);
                        valsum = (PIValues)nvsSum["Total"].Value;
                        break;

                    case SummaryType.asMinimum:
                        nvsSum = ipda.FilteredSummaries(stime, etime, duration, filter, ArchiveSummariesTypeConstants.asMinimum);
                        valsum = (PIValues)nvsSum["Minimum"].Value;
                        break;

                    case SummaryType.asMaximum:
                        nvsSum = ipda.FilteredSummaries(stime, etime, duration, filter, ArchiveSummariesTypeConstants.asMaximum);
                        valsum = (PIValues)nvsSum["Maximum"].Value;
                        break;

                    case SummaryType.asStdDev:
                        nvsSum = ipda.FilteredSummaries(stime, etime, duration, filter, ArchiveSummariesTypeConstants.asStdDev);
                        valsum = (PIValues)nvsSum["StdDev"].Value;
                        break;

                    case SummaryType.asRange:
                        nvsSum = ipda.FilteredSummaries(stime, etime, duration, filter, ArchiveSummariesTypeConstants.asRange);
                        valsum = (PIValues)nvsSum["Range"].Value;
                        break;

                    case SummaryType.asAverage:
                        nvsSum = ipda.FilteredSummaries(stime, etime, duration, filter, ArchiveSummariesTypeConstants.asAverage);
                        valsum = (PIValues)nvsSum["Average"].Value;
                        break;

                    case SummaryType.asPStdDev:
                        nvsSum = ipda.FilteredSummaries(stime, etime, duration, filter, ArchiveSummariesTypeConstants.asPStdDev);
                        valsum = (PIValues)nvsSum["PStdDev"].Value;
                        break;

                    //case SummaryType.asTotal:
                    //    nvsSum = ipda.FilteredSummaries(stime, etime, duration, filter, ArchiveSummariesTypeConstants.asAverage);
                    //    valsum = (PIValues)nvsSum["Average"].Value;
                    //    break;

                    default:
                        nvsSum = ipda.FilteredSummaries(stime, etime, duration, filter, ArchiveSummariesTypeConstants.asAverage);
                        valsum = (PIValues)nvsSum["Average"].Value;
                        break;
                }

                if (valsum.Count > 0) {
                    pdata = new double[valsum.Count];

                    for (int i = 1; i <= valsum.Count; i++) {
                        pdata[i - 1] = (double)valsum[i].Value;
                    }

                    return true;
                }
                else {

                    return false;
                }



            }
            catch (Exception ex) {
                _ErrorInfo = ex.Message;

                return false;
            }

        }

        public bool TagSummaryData(string tagname, DateTime stime, DateTime etime, string filter, out TagAllValue pdata) {
            //double ReturnValue = double.MinValue;
            pdata = new TagAllValue();

            try {
                PISDK.PIPoint piPoint = piServer.PIPoints[tagname];

                pdata.TagName = tagname;
                pdata.TagSnapshot = Convert.ToDouble(piPoint.Data.Snapshot.Value.ToString());
                pdata.TagDesc = piPoint.PointAttributes["Descriptor"].Value.ToString();
                pdata.TagEngunit = piPoint.PointAttributes["Engunits"].Value.ToString();

                //if (filter.Trim() != "")
                //{
                /////////////////////////////////////////////////////////////////////////////////
                PISDK.IPIData2 ipda = (PISDK.IPIData2)piPoint.Data;

                //ReturnValue = Convert.ToDouble(ipda.Snapshot.Value);

                PISDKCommon.NamedValues nvsSum;
                PIValues valsum;

                //////////////////////////////////////////////////////////////////////
                PISDK.IPICalculation ipicalc = (IPICalculation)piServer;

                if (filter != "") {
                    double dpercent = 0.0;
                    valsum = ipicalc.PercentTrue(stime, etime, "", filter);

                    if (valsum.Count == 1) {
                        dpercent = (double)valsum[1].Value;
                    }

                    //wugh,add at 2013.4.8
                    if (dpercent < 0.00001) {
                        return false;
                    }

                }
                /////////////////////////////////////////////////

                nvsSum = ipda.FilteredSummaries(stime, etime, "", filter, ArchiveSummariesTypeConstants.asAll);

                //
                valsum = (PIValues)nvsSum["Total"].Value;

                if (valsum.Count == 1) {
                    pdata.TagTotal = (double)valsum[1].Value;
                }

                valsum = (PIValues)nvsSum["Average"].Value;

                if (valsum.Count == 1) {
                    pdata.TagAverage = (double)valsum[1].Value;
                }


                valsum = (PIValues)nvsSum["Maximum"].Value;

                if (valsum.Count == 1) {
                    pdata.TagMaximum = (double)valsum[1].Value;
                }

                valsum = (PIValues)nvsSum["Minimum"].Value;

                if (valsum.Count == 1) {
                    pdata.TagMinimum = (double)valsum[1].Value;
                }


                valsum = (PIValues)nvsSum["Range"].Value;

                if (valsum.Count == 1) {
                    pdata.TagRange = (double)valsum[1].Value;
                }

                valsum = (PIValues)nvsSum["StdDev"].Value;

                if (valsum.Count == 1) {
                    pdata.TagStdDev = (double)valsum[1].Value;
                }

                valsum = (PIValues)nvsSum["PStdDev"].Value;

                if (valsum.Count == 1) {
                    pdata.TagPStdDev = (double)valsum[1].Value;
                }

                //}
                //else
                //{
                //    ////////////////////////////////////////////////////////////////////////////////////////////////////////
                //    //No Filter
                //    PISDK.PIValue rv = null;
                //    rv = piPoint.Data.Summary(stime, etime, PISDK.ArchiveSummaryTypeConstants.);



                //    ReturnValue = Convert.ToDouble(rv.Value);
                //}

            }
            catch (Exception ex) {
                LogUtil.LogMessage(ex.Message);
                //_ErrorInfo = ex.Message;
            }

            return true;
        }

        #endregion

        #region Time����

        /// <summary>
        /// ͳ�Ʊ��ʽΪ�������ʱ�䣬���ص�λ�롣
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="stime"></param>
        /// <param name="etime"></param>
        /// <returns></returns>
        public double GetExpressionTrueSecondTime(string expression, DateTime stime, DateTime etime) {
            double ReturnValue = double.MinValue;

            try {
                PISDK.IPICalculation ipicalc = (IPICalculation)piServer;
                PIValues valsum;

                double dpercent = 0.0;

                valsum = ipicalc.PercentTrue(stime, etime, "", expression);

                if (valsum.Count == 1) {
                    dpercent = (double)valsum[1].Value;
                }

                TimeSpan tspan = etime - stime;

                ReturnValue = dpercent / 100 * tspan.TotalSeconds;

            }
            catch (Exception ex) {
                _ErrorInfo = ex.Message;
            }

            return ReturnValue;
        }

        /// <summary>
        /// ����ĳʱ��ĳ���ʽ��ֵ
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="dttime"></param>
        /// <returns></returns>
        public double TimedCalculate(string expression, DateTime dttime) {
            double ReturnValue = double.MinValue;

            try {
                PISDK.IPICalculation ipicalc = (IPICalculation)piServer;
                PIValues valsum;

                valsum = ipicalc.TimedCalculate(dttime, expression);

                if (valsum.Count == 1) {
                    ReturnValue = (double)valsum[1].Value;
                }


            }
            catch (Exception ex) {
                _ErrorInfo = ex.Message;
            }

            return ReturnValue;
        }

        #endregion
    }
}
