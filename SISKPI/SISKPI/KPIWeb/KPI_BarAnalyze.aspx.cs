using Newtonsoft.Json;
using SIS.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SISKPI.KPIWeb
{
    public partial class KPI_BarAnalyze : System.Web.UI.Page
    {
        private static string ecweb = "";
        private static string queryTime = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["queryTime"] != null)
                {
                    queryTime = Request.QueryString["queryTime"].ToString();
                }
                //页面集合
                if (Request.QueryString["ecweb"] != null)
                {
                    ecweb = Request.QueryString["ecweb"].ToString();
                }
                else
                {
                    ecweb = "";
                }
            }
        }

        [WebMethod]
        public static object GetAnalyzeData()
        {
            DataTable dt = ECSSSnapshotDal.GetSearchList(ecweb, queryTime, "", "");

            var totalQuery =from q in dt.AsEnumerable()
                       group q by new {uid=q.Field<string>("UnitID"),uname=q.Field<string>("UnitName")} into result
                       select new {
                           name=result.Key.uname,
                           value=result.Count()
                       };

            List<BarModel> barModelList = new List<BarModel>();
            foreach (var item in totalQuery)
            {
                    barModelList.Add(new BarModel {
                        name = item.name,
                        value=item.value.ToString(),
                       group="总数"
                });
            }

            var errorQuery =from q in dt.AsEnumerable().Where(d=>d.Field<int>("ECQulity")==1)
                            group q by new { uid = q.Field<string>("UnitID"), uname = q.Field<string>("UnitName") } into result
                            select new
                            {
                                name = result.Key.uname,
                                value = result.Count()
                            };
            foreach (var item in errorQuery)
            {
                barModelList.Add(new BarModel
                {
                    name = item.name,
                    value = item.value.ToString(),
                    group = "异常"
                });
            }
            return barModelList;
        }
    }

    public class BarModel
    {
        public string name { get; set; }
        public string value { get; set; }
        public string group { get; set; }

    }
}