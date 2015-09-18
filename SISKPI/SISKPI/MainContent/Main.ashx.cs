using SIS.DataModule.KPIDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace SISKPI.MainContent
{
    /// <summary>
    /// Main 的摘要说明
    /// </summary>
    public class Main : IHttpHandler
    {
        JavaScriptSerializer jsS = new JavaScriptSerializer();
        public void ProcessRequest(HttpContext context)
        {
            string command = context.Request["cmd"];
            string flag = context.Request["flag"];
            switch (command)
            {
                case "MTS":
                    GetMonthTotalScore(context);
                    break;
                case "MDT":
                    GetMonthDurationTotal(context);
                    break;
                case "YTS":
                    GetYearTotalScore(context);
                    break;
                case "YDT":
                    GetYearDurationTotal(context);
                    break; 
            };
        }
        /// <summary>
        /// 各值当月得分统计
        /// </summary>
        /// <param name="context"></param>
        public void GetMonthTotalScore(HttpContext context)
        {
            var dt = Charts.CreateCharts.CreateInstance.GetMonthTotalScore();
            IList<ChartModel> resultLists = new List<ChartModel>();
            foreach (DataRow dr in dt.AsEnumerable())
            {
                resultLists.Add(new ChartModel
                {
                    name = dr["Shift"].ToString()+"值",
                    value = dr["TotalScore"].ToString(),
                    group = "各值当月得分统计"
                });
            }
            jsS = new JavaScriptSerializer();
            context.Response.Write(jsS.Serialize(resultLists));
        }
        /// <summary>
        /// 各值当月监盘时间统计
        /// </summary>
        /// <param name="context"></param>
        public void GetMonthDurationTotal(HttpContext context)
        {
            var dt = Charts.CreateCharts.CreateInstance.GetMonthDurationTotal();
            IList<ChartModel> resultLists = new List<ChartModel>();
            foreach (DataRow dr in dt.AsEnumerable())
            {
                resultLists.Add(new ChartModel
                {
                    name = dr["Shift"].ToString()+"值",
                    value = dr["Duration"].ToString(),
                    group = "各值当月监盘时间统计"
                });
            }
            jsS = new JavaScriptSerializer();
            context.Response.Write(jsS.Serialize(resultLists));
        }
        /// <summary>
        /// 各值当年得分统计
        /// </summary>
        /// <param name="context"></param>
        public void GetYearTotalScore(HttpContext context)
        {
            var dt = Charts.CreateCharts.CreateInstance.GetYearTotalScore();
            IList<ChartModel> resultLists = new List<ChartModel>();
            foreach (DataRow dr in dt.AsEnumerable())
            {
                resultLists.Add(new ChartModel
                {
                    name = dr["Shift"].ToString() + "值",
                    value = dr["TotalScore"].ToString(),
                    group = "各值当年得分统计"
                });
            }
            jsS = new JavaScriptSerializer();
            context.Response.Write(jsS.Serialize(resultLists));
        }
         /// <summary>
        /// 各值当年监盘时间统计
        /// </summary>
        /// <param name="context"></param>
        public void GetYearDurationTotal(HttpContext context)
        {
            var dt = Charts.CreateCharts.CreateInstance.GetYearDurationTotal();
            IList<ChartModel> resultLists = new List<ChartModel>();
            foreach (DataRow dr in dt.AsEnumerable())
            {
                resultLists.Add(new ChartModel
                {
                    name = dr["Shift"].ToString() + "值",
                    value = dr["Duration"].ToString(),
                    group = "当月监盘时间统计"
                });
            }
            jsS = new JavaScriptSerializer();
            context.Response.Write(jsS.Serialize(resultLists));
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
    public class Charts
    {
        public static Charts CreateCharts 
        {
            get
            {
                return new Charts();
            }
        }
        private KPI_MainChartsDal _instance;
        public KPI_MainChartsDal CreateInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new KPI_MainChartsDal();
                    return _instance;
                }
                else
                    return _instance;
            }
        }
    }
    public class ChartModel
    {
        public string name { get; set; }
        public string value { get; set; }
        public string group { get; set; }

    }
}