using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SIS.Arithmetic;
using SIS.Evaluator;
using SIS.DataAccess;
using SIS.IFCRefer;
using SIS.DBControl;
using SIS.Loger;

//处理解析布尔表达式
using Microsoft.JScript;
using Microsoft.JScript.Vsa;

namespace SIS.Assistant {

    /// <summary>
    /// 表达式解析器
    /// </summary>
    public class ExpDone {
        private Parser P = new Parser();
        RTInterface RTDataAccess = DBAccess.GetRealTime();
        private VsaEngine ve = VsaEngine.CreateEngine();
        private string Err = "";

        public DateTime BeginTime {
            get;
            set;
        }

        public DateTime EndTime {
            get;
            set;
        }

        #region 静态类，最新执行，并只执行一次

        public ExpDone() {
            /////////////////////////////////////////////////////
            //添加自定义函数
            P.CustomFunction += new SIS.Arithmetic.FunctionHandler(CustomFunction);

            //按此方式添加自定义函数名称，并在此类中实现。
            P.AddCustomFunction("CFSZBCLV", 2);
            P.AddCustomFunction("CFSZBCJJ", 4);
            P.AddCustomFunction("CFSZJJDD", 2);
            P.AddCustomFunction("CFSZQCLV", 4);

            //Added by pyf 2013-09-12
            P.AddCustomFunction("GETALARMCOUNT", 2);
            P.AddCustomFunction("TSK", 1);
            P.AddCustomFunction("PSK", 1);
            P.AddCustomFunction("POWERDIFF", 2);
            //End of Added.

            P.AddCustomFunction("AVG", 1);
            P.AddCustomFunction("IFF", 3);

            /////////////////////////////////////////////////////

            P.OnError += new ErrorHandler(OnError);
        }

        #endregion

        /// <summary>
        /// 解析表达式，并将其中的指标代码和系数a1\a2\a3\a4存储到Dictionary中
        /// //////////////////////////
        /// 返回值解释
        /// 0-正确；
        /// 1-错误，标签点引用不正确；
        /// 2-错误，空标签点；
        /// 3-错误，表达式不正确；
        /// 4-错误，
        /// </summary>
        /// <param name="strexp"></param>
        /// <param name="dcexp"></param>
        /// <returns></returns>
        public int ExpEvaluate(string strexp, ref Dictionary<string, double> dcexp) {
            int n = 0;
            string strsigle = "'";
            n = strexp.Length - strexp.Replace(strsigle, "").Length;
            if (n % 2 != 0) {
                //标签点引用不正确!
                return 1;
            }

            //处理标签、指标引用；
            int nstart = 0;
            int nend = 0;
            while (n > 0) {
                nstart = strexp.IndexOf(strsigle, nstart);
                nend = strexp.IndexOf(strsigle, nstart + 1);
                string strkpi = strexp.Substring(nstart, nend - nstart + 1).ToUpper().Trim();
                if (strkpi.Length <= 2) {
                    return 2;
                }
                dcexp[strkpi] = 0.0;
                nstart = nend + 1;
                n = n - 2;
            }


            //处理系数问题
            string[] af = new string[] { "@REF", "@A1", "@A2", "@A3", "@A4", "@A5", "@A6", "@A7", "@A8" };
            for (int i = 0; i < 9; i++) {
                n = strexp.Length - strexp.Replace(af[i], "").Length;
                if (n > 0) {
                    dcexp[af[i]] = 0.0;
                }
            }

            return 0;
        }

        /// <summary>
        /// 计算表达式
        /// //////////////////////////
        /// 返回值解释
        /// 0-正确；
        /// 1-错误，标签点引用不正确；
        /// 2-错误，空标签点；
        /// 3-错误，表达式不正确；
        /// 4-错误，
        /// </summary>
        /// <param name="strexp"></param>
        /// <param name="dcexp"></param>
        /// <param name="result"></param>
        /// <param name="strinfor"></param>
        /// <returns></returns>
        public int ExpCalculate(string strexp, Dictionary<string, double> dcexp, out double result, out string strresult, out string strexpression) {
            result = double.MinValue;
            strresult = "";
            strexpression = strexp;
            foreach (KeyValuePair<string, double> kvp in dcexp) {
                strexpression = strexpression.Replace(kvp.Key, kvp.Value.ToString("0.00000"));
            }

            try {
                strexp = strexp.ToUpper();
                if ((strexp.IndexOf("IFF") >= 0) && (strexp.IndexOf("POWERDIFF") < 0)) {
                    strexpression = GetIFFExpression(strexpression);
                }
                strresult = P.Evaluate(strexpression).ToString();
                if (double.TryParse(strresult, out result)) {
                    return 0;
                }
                else {
                    return 1;
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.StackTrace);
                strresult = Err;
                return 2;
            }
        }

        /// <summary>
        /// 返回值解释：
        /// 0-正确；
        /// 1-错误，标签点引用不正确；
        /// 2-错误，空标签点；
        /// 3-错误，表达式不正确；
        /// 4-错误，
        /// </summary>
        /// <param name="strexp"></param>
        /// <param name="dcexp"></param>
        /// <param name="result"></param>
        /// <param name="strinfor"></param>
        /// <returns></returns>
        public int ExpBool(string strexp, Dictionary<string, double> dcexp, out bool result, out string strresult) {
            result = false;
            strresult = "";
            string strexpression = strexp;
            foreach (KeyValuePair<string, double> kvp in dcexp) {
                strexpression = strexpression.Replace(kvp.Key, kvp.Value.ToString("0.00000"));
            }
            try {
                //result = Evaluator.Evaluator.EvaluateToBool(strexpression);
                result = System.Convert.ToBoolean(Eval.JScriptEvaluate(strexpression, ve));
                //result = true;
                return 0;
            }
            catch (Exception e) {
                LogUtil.LogMessage("错误信息：" + e.Message + "堆栈信息：" + e.StackTrace);
                LogUtil.LogMessage("原始表达式：" + strexp + "解析后表达式:" + strexpression);
                strresult = e.Message;
                return 1;
            }
        }

        #region  自定义函数

        private void OnError(object s, ErrorEventArgs e) {
            Err = e.ErrorMessage;
        }

        //自定义函数添加
        public Dictionary<String, String> CustomFunctionsListing() {
            Dictionary<String, String> diccf = new Dictionary<String, String>();

            //在经济指标配置页面中提供自定义函数选项
            //按此模式添加

            diccf.Add("CFSZBCLV()", "CFSZBCLV(P3, P4)------绥中B厂实际负荷率函数");
            diccf.Add("CFSZBCJJ()", "CFSZBCJJ(P1, P2, P3, P4)------绥中B厂经济负荷率函数");
            diccf.Add("CFSZJJDD()", "CFSZJJDD(breal, bopt)------绥中经济负荷调度得分函数");

            diccf.Add("CFSZQCLV()", "CFSZSJLV(P1, P2, P3, P4)------绥中全厂实际负荷率函数");

            return diccf;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void CustomFunction(object s, SIS.Arithmetic.FunctionEventArgs e) {
            object ret;
            if (e.FunctionName == "CFSZQCLV") {
                //绥中全厂实际负荷率
                double[] dloads = new double[4];

                dloads[0] = P.GetDoubleParameter(0);
                dloads[1] = P.GetDoubleParameter(1);
                dloads[2] = P.GetDoubleParameter(2);
                dloads[3] = P.GetDoubleParameter(3);

                ret = CFSZQCLV(dloads);

                P.CFSetResult(ret);
            }
            else if (e.FunctionName == "CFSZBCLV") {
                //绥中B厂实际负荷率
                double dBReal = P.GetDoubleParameter(0);
                double dBOpt = P.GetDoubleParameter(1);

                ret = CFSZBCLV(dBReal, dBOpt);

                P.CFSetResult(ret);
            }
            else if (e.FunctionName == "CFSZBCJJ") {
                //绥中B厂经济负荷率
                double[] dloads = new double[4];

                dloads[0] = P.GetDoubleParameter(0);
                dloads[1] = P.GetDoubleParameter(1);
                dloads[2] = P.GetDoubleParameter(2);
                dloads[3] = P.GetDoubleParameter(3);

                ret = CFSZBCJJ(dloads);

                P.CFSetResult(ret);
            }
            else if (e.FunctionName == "CFSZJJDD") {
                //绥中经济负荷调度得分
                double dBReal = P.GetDoubleParameter(0);
                double dBOpt = P.GetDoubleParameter(1);

                ret = CFSZJJDD(dBReal, dBOpt);

                P.CFSetResult(ret);
            }

            if (e.FunctionName == "GETALARMCOUNT") {
                String TagID = P.GetStringParameter(0);
                double AlarmTotals = P.GetDoubleParameter(1);
                ret = GetAlarmCount(TagID, AlarmTotals);
                P.CFSetResult(ret);
            }
            if (e.FunctionName == "POWERDIFF") {
                String tagName = P.GetStringParameter(0);
                double span = P.GetDoubleParameter(1);
                double Result = PowerDiff(tagName, span);
                P.CFSetResult(Result);
            }
            if (e.FunctionName == "TSK") {
                P.CFSetResult(0);
                double p = P.GetDoubleParameter(0);
                if (p > 0) {
                    double Result = IFC67.TSK(ref p);
                    P.CFSetResult(Result);
                }

            }
            if (e.FunctionName == "PSK") {
                P.CFSetResult(0);
                double t = P.GetDoubleParameter(0);
                if (t > 0) {
                    double Result = IFC67.PSK(ref t);
                    P.CFSetResult(Result);
                }

            }

            if (e.FunctionName == "IFF") {
                String Exp = P.GetStringParameter(0);
                double V1 = P.GetDoubleParameter(1);
                double V2 = P.GetDoubleParameter(2);
                P.CFSetResult(IFF(Exp, V1, V2));
            }
        }

        /// <summary>
        /// 绥中B厂实际负荷率
        /// </summary>
        /// <param name="dloads"></param>
        /// <returns></returns>
        public double CFSZBCLV(double dB3, double dB4) {
            int nruns = (dB3 > 400 ? 1 : 0) + (dB4 > 400 ? 1 : 0);

            //B厂停机
            if (nruns == 0) {
                return 0;
            }

            //至少一台运行
            return (dB3 + dB4) / (1000 * nruns);
        }

        /// <summary>
        /// 绥中B厂经济负荷率
        /// </summary>
        /// <param name="dloads"></param>
        /// <returns></returns>
        public double CFSZBCJJ(double[] dloads) {
            double dresult = 0.0;

            int[] nruns = new int[4];

            nruns[0] = dloads[0] > 400 ? 1 : 0;
            nruns[1] = dloads[1] > 400 ? 1 : 0;
            nruns[2] = dloads[2] > 400 ? 1 : 0;
            nruns[3] = dloads[3] > 400 ? 1 : 0;

            //判断是不是只有A厂\B厂单独运行或全厂停机
            //此条件下都不用计算最优负荷率了。
            //全厂停机
            if (nruns[0] == 0 && nruns[1] == 0 && nruns[2] == 0 && nruns[3] == 0) {
                return 0.0;
            }

            //A厂停机
            if (nruns[0] == 0 && nruns[1] == 0) {
                return 1.0;
            }

            //B厂停机
            if (nruns[2] == 0 && nruns[3] == 0) {
                return 0.0;
            }

            //根据机组运行状态，计算A厂、B厂、全厂额定负荷
            double dAe = nruns[0] * 800 + nruns[1] * 800;
            double dBe = nruns[2] * 1000 + nruns[3] * 1000;
            double dalle = dAe + dBe;

            //计算A厂、B厂、全厂实际负荷
            double dA = dloads[0] + dloads[1];
            double dB = dloads[2] + dloads[3];
            double dall = dA + dB;

            //实际负荷率
            double dallrate = dall / dalle;

            //A厂基本负荷与剩余负荷
            double dAbase = (dAe >= 1600) ? 1000 : (dAe >= 800 ? 500 : 0);
            double dAover = dall - dAbase;

            //B厂基本负荷与剩余负荷
            //B厂带A厂基本负荷后的所有负荷至满负荷，
            //B厂剩余负荷将给A厂，
            //但对于最优负荷率的计算不重要了，所以不用管A厂的负荷再分配.
            double dBbase = (dAover < dBe) ? dAover : dBe;
            double dBover = dAover - dBe;

            //B厂最优负荷率
            //double doptrate = dBbase / dBe;

            //考核负荷率
            //dresult = (doptrate + dallrate) / 2.0;
            dresult = dBbase / dBe;

            return dresult;
        }

        /// <summary>
        /// 绥中经济负荷调度得分专用函数
        /// </summary>
        /// <param name="dBReal"></param>
        /// <param name="dBOpt"></param>
        /// <returns></returns>
        public double CFSZJJDD(double dBReal, double dBOpt) {
            double dresult = dBReal - dBOpt;

            //全厂停机、A厂、B厂停机的情况
            if (dBOpt <= 0) {
                return 0.0;
            }

            if (dresult >= 0) {
                return 100.0;
            }
            else {
                return 100.0 * (1 - Math.Abs(dresult));
            }
        }


        /// <summary>
        /// 绥中实际负荷率
        /// </summary>
        /// <param name="dloads"></param>
        /// <returns></returns>
        public double CFSZQCLV(double[] dloads) {
            double dresult = 0.0;

            int[] nruns = new int[4];

            nruns[0] = dloads[0] > 400 ? 1 : 0;
            nruns[1] = dloads[1] > 400 ? 1 : 0;
            nruns[2] = dloads[2] > 400 ? 1 : 0;
            nruns[3] = dloads[3] > 400 ? 1 : 0;

            //判断是不是只有A厂\B厂单独运行或全厂停机
            //此条件下都不用计算最优负荷率了。
            //全厂停机
            if (nruns[0] == 0 && nruns[1] == 0 && nruns[2] == 0 && nruns[3] == 0) {
                return dresult;
            }

            //
            dresult = (dloads[0] + dloads[1] + dloads[2] + dloads[3]) / (800 * (nruns[0] + nruns[1]) + 1000 * (nruns[2] + nruns[3]));

            return dresult;
        }

        private object GetAlarmCount(String tags, double alarmDuration) {
            String[] Tags = tags.Split(',');
            List<int?> Durations = new List<int?>();
            double Duration = 0.0f;//指标超限时长(单位：秒）
            using (KPI_OverLimitRecordDal DataAccess = new KPI_OverLimitRecordDal()) {
                foreach (String TagID in Tags) {
                    Durations.Add(DataAccess.GetOverLimitRecords(BeginTime, EndTime, TagID).Sum(p => p.Duration));
                }
            }
            if (Durations.Count > 0) Duration = Durations.Max().Value;
            return Math.Ceiling(Duration / alarmDuration);
        }

        /// <summary>
        /// 根据当前电量和上一时刻电量计算电量差值
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="span"></param>
        /// <returns></returns>
        private double PowerDiff(String tagName, double span) {
            double Result = 0.0f;
            DateTime LastTime = DateTime.Now.AddMinutes(-1 * span);//上一时刻
            //RTInterface RTDataAccess = DBAccess.GetRealTime();
            double CurrentValue = RTDataAccess.GetSnapshotValue(tagName);//当前值
            double LastValue = RTDataAccess.GetArchiveValue(tagName, LastTime);//上一时刻值
            Result = CurrentValue - LastValue;
            return Result;
        }

        private double IFF(String Expression, double Value1, double Value2) {
            if (String.IsNullOrWhiteSpace(Expression)) return 0.0f;
            if (Expression.IndexOf("AGC01") >= 0) {
                String AGC01 = "(IFF(\"([1_AM18CCS0204_XQ01]-[1_AM18CCS0201_XQ01])>2\",0,-5) * IFF (\"[1_MAG01CPZ_XQ01]>31\",0,1)* IFF(\"([1_AM18BY03_XQ01]-[1_AM18CCS0201_XQ01])>10\",1,0))";
                String exp = Expression.Replace("AGC01", AGC01);
                exp = GetIFFExpression(exp);
                double AGCValue = (double)P.Evaluate(exp);
                Expression = Expression.Replace("AGC01", AGCValue + "");
            }
            if (Expression.IndexOf("AGC02") >= 0) {
                String AGC02 = "(IFF(\"([1_AM18CCS0201_XQ01]- [1_AM18CCS0205_XQ01])>2\",0,-5)*(IF(\"[1_AM18BY03_XQ01]<178\",0,1)+IF(\"[1_AMMW_XQ01]<178\",1,0))*IFF(\"([1_AM18CCS0201_XQ01]-[1_AM18BY03_XQ01])>5\",1,0))";
                String exp = Expression.Replace("AGC02", AGC02);
                exp = GetIFFExpression(exp);
                double AGCValue = (double)P.Evaluate(exp);
                Expression = Expression.Replace("AGC02", AGCValue + "");
            }
            if (Expression.IndexOf("AGC03") >= 0) {
                String AGC03 = "(IFF(\"([2_AM18CCS0204_XQ01]-[2_AM18CCS0201_XQ01])>2\",0,-5) * IFF (\"[2_MAG01CPZ_XQ01]>31\",0,1)* IFF(\"([2_AM18BY03_XQ01]-[2_AM18CCS0201_XQ01])>10\",1,0))";
                String exp = Expression.Replace("AGC03", AGC03);
                exp = GetIFFExpression(exp);
                double AGCValue = (double)P.Evaluate(exp);
                Expression = Expression.Replace("AGC03", AGCValue + "");
            }

            if (Expression.IndexOf("AGC04") >= 0) {
                String AGC04 = "(IFF(\"([2_AM18CCS0204_XQ01]-[2_AM18CCS0201_XQ01])>2\",0,-5) * IFF (\"[2_MAG01CPZ_XQ01]>31\",0,1)* IFF(\"([2_AM18BY03_XQ01]-[2_AM18CCS0201_XQ01])>10\",1,0))";
                String exp = Expression.Replace("AGC04", AGC04);
                exp = GetIFFExpression(exp);
                double AGCValue = (double)P.Evaluate(exp);
                Expression = Expression.Replace("AGC04", AGCValue + "");
            }
            bool result = System.Convert.ToBoolean(Eval.JScriptEvaluate(Expression, ve));
            return result ? Value1 : Value2;
        }

        /// <summary>
        /// 解析IFF表达式，将IFF表达式中的测点代码替换为测点1分钟内数据的均值
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        private String GetIFFExpression(String exp) {
            String Result = "";
            int LastIndex = exp.LastIndexOf("[");
            if (LastIndex < 0) return Result;
            int StartIndex = 0;
            int EndIndex = 0;
            String Key = "";
            Random rand = new Random();
            Dictionary<String, double> Dict = new Dictionary<string, double>();
            while (EndIndex < LastIndex) {
                StartIndex = exp.IndexOf("[", EndIndex);
                EndIndex = exp.IndexOf("]", StartIndex + 1);
                if (StartIndex < EndIndex) {
                    Key = exp.Substring(StartIndex + 1, EndIndex - StartIndex - 1);
                    if (!Dict.ContainsKey(Key)) Dict.Add(Key, GetTagValue(Key, 1));
                }
                EndIndex++;
            }
            Result = exp;
            foreach (String K in Dict.Keys) {
                Result = Result.Replace("[" + K + "]", Dict[K] + "");
            }
            return Result;
        }

        /// <summary>
        /// 返回测点指定分钟内的均值
        /// </summary>
        /// <param name="TagName">测点代码</param>
        /// <param name="Durution">时长（分钟）</param>
        /// <returns></returns>
        private double GetTagValue(String TagName, int Durution) {

            DateTime EndDateTime = DateTime.Now;
            EndDateTime = EndDateTime.AddSeconds(-1 * EndDateTime.Second);
            DateTime StartDateTime = EndDateTime.AddMinutes(-1 * Durution);
            List<double> TagValues = RTDataAccess.GetHistoryDataListBySecondSpan(TagName, false,
                StartDateTime, EndDateTime, 5);
            return TagValues.Average();
            //Random random = new Random();
            //return random.NextDouble() * 1000;
        }


        #endregion

    }
}
