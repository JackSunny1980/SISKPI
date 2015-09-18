using System;
using System.Collections.Generic;
using SIS.DBControl;
using System.Data;
using SIS.DataAccess;
using SIS.DataEntity;
using System.Threading;
using SIS.Arithmetic;


namespace SIS.Assistant.WS {

    static class Program {

        //private static WS_KPIMainMethod m_KPICalc = new WS_KPIMainMethod();
        //private static int m_Index = 1;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        public static void Main() {
            //ExpressionTest();
            //ExpressionTest();
            //Parser P = new Parser();
            //object result = Convert.ToBoolean("9>10");			
            //Console.WriteLine(result);
            //VsaEngine ve = VsaEngine.CreateEngine();
            //Console.WriteLine(Eval.JScriptEvaluate("9>=8", ve));            
            //String Exp = "IFF(AVG(\"1_AMMW_XQ01\")-AVG(\"1_AMMW_XQ01\"),19,20)";
            String Exp = "IFF(\"([1_AM18CCS0201_XQ01]- [1_AM18CCS0205_XQ01])>2\",0,-5)*"+
                "(IFF(\"[1_AM18BY03_XQ01]<178\",0,1)+IFF(\"[1_AMMW_XQ01]<178\",1,0))*" +
                "IFF(\"([1_AM18CCS0201_XQ01]-[1_AM18BY03_XQ01])>5\",1,0)";
            String Exp1 = "IFF(\"([1_AM18CCS0206_XQ01]-3.75)>=0\",0,-0.5)+" +
                        "IFF(\"([1_AM18CCS0206_XQ01]-3.00)>=0\",0,-1)+" +
                        "IFF(\"([1_AM18CCS0206_XQ01]-2.50)>=0\",0,-1.5)+" +
                        "IFF(\"([1_AM18CCS0206_XQ01]-2.00)>=0\",0,-2)+" +
                        "IFF(\"([1_AM18CCS0206_XQ01]-1.50)>=0\",0,-3)";
            //JurassicDemo(Exp);
            //AGC速率指标
            String AGC1="IFF(\"([1_AM18CCS0206_XQ01]-3.75)>=0\",0,-0.5) + "+
                       "IFF(\"([1_AM18CCS0206_XQ01]-3.00)>=0\",0,-1) + "+
                       "IFF(\"([1_AM18CCS0206_XQ01]-2.50)>=0\",0,-1.5) + "+
                       "IFF(\"([1_AM18CCS0206_XQ01]-2.00)>=0\",0,-2) + "+
                       "IFF(\"([1_AM18CCS0206_XQ01]-1.50)>=0\",0,-3)";
            //AGC限负荷指标
            String AGC2 = "(IFF(\"([1_AM18CCS0204_XQ01]-[1_AM18CCS0201_XQ01])>2\",0,-5) * IFF (\"[1_MAG01CPZ_XQ01]>31\",0,1)* IFF(\"([1_AM18BY03_XQ01]-[1_AM18CCS0201_XQ01])>10\",1,0)) + (IFF(\"([1_AM18CCS0201_XQ01]- [1_AM18CCS0205_XQ01])>2\",0,-5)*(IFF(\"[1_AM18BY03_XQ01]<178\",0,1)+IFF(\"[1_AMMW_XQ01]<178\",1,0))*IFF(\"([1_AM18CCS0201_XQ01]-[1_AM18BY03_XQ01])>5\",1,0))";
            //AGC调整补偿指标
            String AGC3 = "ABS([1_AM18BY03_XQ01]-[1_AMMW_XQ01])/2 * IFF (\"[1_AM18BY03_XQ01]<178\",0,1)+ IFF (\"[1_AMMW_XQ01]<178\",1,0) * IFF (\"AGC01==0\",1,0) * IFF(\"AGC02==0\",1,0)";
            Dictionary<String, double> dcexp = new Dictionary<string, double>();
            String strresult = "",  strexpression="";
            double result = 0;
            ExpDone P = new ExpDone();
            P.ExpCalculate(AGC2, dcexp, out result, out strresult, out strexpression);
            Console.WriteLine("{0}={1}", AGC2, result);
            //Console.WriteLine(IFCRefer.IFC67.TSK(ref p));
            //WS_KPIMainMethod KPICalc = new WS_KPIMainMethod();
            //KPICalc.KPIAppRunForPerformance(true);
            /*int i = 1;
            while (true) {
                KPICalc.KPIAppRunForPerformance(true);
                Console.WriteLine(string.Format("第{0}遍计算", i));
                i++;
                GC.Collect();
                Thread.Sleep(60000);
            }*/
            //KPIRecalculate();
            //GetUnitLoad();
            //KPICalc.KIsTest = 1;
            //KPICalc.KPIAppRunForRecalculate();            
            Console.ReadLine();
        }

        private static void JurassicDemo(String exp) {
            //var engine = new Jurassic.ScriptEngine();
            //engine.SetGlobalFunction("test", new Func<int, int, int>((a, b) => a + b));
            //Console.WriteLine(engine.Evaluate<int>("test(5, 6)"));
            int LastIndex = exp.LastIndexOf("'");
            if (LastIndex < 0) return;
            int StartIndex = 0;
            int EndIndex = 0;
            String Key = "";
            Random rand = new Random();
            Dictionary<String, double> Dict = new Dictionary<string, double>();
            while (EndIndex < LastIndex) {
                StartIndex = exp.IndexOf("'", EndIndex);
                EndIndex = exp.IndexOf("'", StartIndex + 1);
                if (StartIndex < EndIndex) {
                    Key = String.Format("'{0}'",
                        exp.Substring(StartIndex + 1, EndIndex - StartIndex - 1));
                    if (!Dict.ContainsKey(Key)) Dict.Add(Key, rand.NextDouble()*100);
                    //Console.WriteLine(Key);
                }
                EndIndex++;               
            }
            foreach(String K in Dict.Keys){
                exp = exp.Replace(K, Dict[K] + "");
                Console.WriteLine(K);
            }
            Console.WriteLine(exp);
        }

        private static void KPIRecalculate() {
            WS_KPIMainMethod KPICalc = new WS_KPIMainMethod();
            KPICalc.KPIAppRunForRecalculate();
        }

        private static void RTTest() {
            RTInterface m_RTDataAccess = DBAccess.GetRealTime();
            m_RTDataAccess.ExpCurrentValue("'\\mjdc\\DCS2\\HIC_32MW'>300");
        }

        private static void ExecuteCalc(Object sender) {
            //m_KPICalc.KPIAppRunForPerformance(true);
            //Console.WriteLine(string.Format("第{0}遍计算", m_Index));
            //m_Index++;
        }

        private static void GetUnitLoad() {
            RTInterface m_RTDataAccess = DBAccess.GetRealTime();
            DateTime StartTime = new DateTime(2014, 2, 14, 2, 45, 0), EndTime = new DateTime(2014, 2, 14, 3, 1, 0);
            double UnitLoad = 0;
            Console.WriteLine("");
            while (StartTime <= EndTime) {
                UnitLoad = m_RTDataAccess.GetArchiveValue(@"\mjdc\DCS1\HIC_31MW", StartTime);
                Console.WriteLine(StartTime.ToString("yyyy-MM-dd HH:mm:00") + "1#机组负荷：" + UnitLoad);
                StartTime = StartTime.AddMinutes(1);
            }
            Console.Read();
        }

        private static void GetShift() {
            String strCurrentShift = "";
            String strCurrentPeriod = "";
            String strStartTime = "";
            String strEndTime = "";
            //获取班次与值次
            //KPI_UnitDal.GetValidEntity()
            String strWorkID = "48883e53-0647-47f1-98d6-fa6b4f8464cb";
            String strCurrentMinute = DateTime.Now.ToString("yyyy-MM-dd HH:mm:00");
            KPI_WorkDal.GetShiftAndPeriod(strWorkID, strCurrentMinute,
                ref strCurrentShift, ref strCurrentPeriod, ref strStartTime, ref strEndTime);
            Console.WriteLine("当前值是:" + strCurrentShift);
            Console.WriteLine("当前班是:" + strCurrentPeriod);
        }

        public static void TagTest() {
            String[] tags = { @"\mjdc\DCS2\TE_32254D_PV", @"\mjdc\DCS2\TE_32254E_PV", @"\mjdc\DCS2\TE_32254F_PV" };
            Dictionary<String, TagValue> lttvs = new Dictionary<String, TagValue>();
            foreach (String tag in tags) {
                lttvs.Add(tag.ToUpper(), new TagValue(tag.ToUpper()));
            }
            String strError = "";
            RTInterface RTDataAccess = DBAccess.GetRealTime();
            RTDataAccess.SetPointList(lttvs, out strError);
            if (!String.IsNullOrEmpty(strError)) {
                Console.WriteLine(strError);
                Console.ReadLine();
            }
            RTDataAccess.GetSnapshotListData(ref lttvs, out strError);
            if (!String.IsNullOrEmpty(strError)) {
                Console.WriteLine(strError);
                return;
            }
            foreach (String tag in lttvs.Keys) {
                Console.WriteLine(tag + "=" + lttvs[tag].TagSnapshot);
            }
            Console.ReadLine();
        }

        private static void BulkTable() {
            string BulkTable = "KPI_ECSSSnapshot";
            WS_KPIDBClient DBClient = new WS_KPIDBClient();
            DataTable dt = DBClient.GetTableSchema("kpivalue");
            for (int i = 0; i < 10; i++) {
                DataRow dr = dt.NewRow();
                dr["SSID"] = i + "";
                dr["UnitID"] = i + "";
                dr["SeqID"] = i + "";
                dr["KpiID"] = i + "";
                dr["ECID"] = i + "";
                dr["ECName"] = i + "";
                dr["ECTime"] = i + "";
                dr["ECValue"] = i;
                dr["ECOpt"] = i;// System.DBNull.Value;
                dr["ECOptExp"] = "ha";
                dr["ECExpression"] = i;// kpiECV.ECExpression;	
                dr["ECScore"] = i;
                dr["ECQulity"] = 0;
                dr["ECPeriod"] = i + "";
                dr["ECShift"] = 1 + "";
                dr["ECIsRemove"] = 1;
                dt.Rows.Add(dr);
                ECSSValueEntity kpiECV = new ECSSValueEntity();

            }
            DBClient.Connection();
            DBClient.DeleteData("", "", "", BulkTable);
            //insert
            DBClient.BulkToDB(dt, BulkTable);
            DBClient.DisConnection();
        }

        private static void ExpressionTest() {
            ExpDone m_ExpressionParse = new ExpDone();
            Dictionary<string, double> dcexp = new Dictionary<string, double>();
            double result = 0;
            String strExpression = "";
            string strresult = "";
            String tagid = "\"\\MJDC\\CYD\\PI00021\"";
            String exp = "POWERDIFF(" + tagid + ",10)";
            Console.WriteLine(exp);
            m_ExpressionParse.ExpCalculate(exp, dcexp, out result, out strresult, out strExpression);
            Console.WriteLine(String.Format("表达式{0}计算结果是{1}", exp, result));

            Dictionary<String, double> dic1 = new Dictionary<String, double>();
            m_ExpressionParse.ExpEvaluate(exp, ref dic1);
        }

        private static void AlarmProductTest() {
            AlarmProductor AlarmProductor1 = new AlarmProductor();
            AlarmProductor1.PorcessExceedLimit();
        }

        private static void GetArchiveData() {
            RTInterface RTDataAccess = DBAccess.GetRealTime();
            String TagID = @"\mjdc\DCS2\TE_32254E_PV";
            DateTime endTime = DateTime.Now;
            DateTime beginTime = endTime.AddMinutes(-10);
            List<TagValue> TagValues = RTDataAccess.GetHistoryDataList(TagID, beginTime, endTime);
            Console.WriteLine("");
            foreach (TagValue tagvalue in TagValues) {
                Console.WriteLine(String.Format("测点{0}监测时间:{1}—值是:{2}", TagID, tagvalue.TimeStamp, tagvalue.TagStringValue));
            }
        }
    }

}
