using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SIS.DBControl;
using System.IO;
using System.Web.UI.HtmlControls;

using SIS.Loger;

using SIS.DataAccess;
using SIS.DataEntity;
using SIS.Exceler;


namespace SISKPI {
    public partial class KPI_RealValue : System.Web.UI.Page {
        private static DateTime dtCurrentTime = DateTime.Now;


        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            if (!IsPostBack) {
                DataBind();
            }
        }

        public override void DataBind() {   
            string SpecialField = Request.Params["ecweb"];
            string queryTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:00");
            String PlantCode = Request.Params["plantcode"];
            String UnitCode = Request.Params["unitcode"];
            String PlantID = KPI_PlantDal.GetPlantIDByCode(PlantCode);
            String UnitID = KPI_UnitDal.GetUnitIDByCode(UnitCode);
            gvReal.DataSource = KPI_RealValueDal.GetRealValue(PlantID, "");
            base.DataBind();

            nowtime.Text = "计算时间：" + queryTime;
            nowshift.Text = "轮班值次：";
            if (!String.IsNullOrWhiteSpace(UnitID)) {
                string workid = KPI_UnitDal.GetWorkIDByID(UnitID);
                string ShiftName = "";
                string PeriodName = "";
                string StartTime = "";
                string EndTime = "";
                bool bGood = KPI_WorkDal.GetShiftAndPeriod(workid, queryTime, ref ShiftName,
                    ref PeriodName, ref StartTime, ref EndTime);
                nowshift.Text = "轮班值次：" + ShiftName;
            }
        }
 

        protected void Timer1_Tick(object sender, EventArgs e) {
            DataBind();
        }

    }


}
