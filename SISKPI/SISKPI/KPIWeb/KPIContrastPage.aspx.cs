using SIS.DataAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SISKPI.KPIWeb {
    public partial class KPIContrastPage : System.Web.UI.Page {

        #region 重写方法

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            if (!IsPostBack) {
                ClientInitial();
                DataBind();
            }
        }
        public override void DataBind() {
            //Repeater1.DataSource = "";
            DateTime StartDate = Convert.ToDateTime(txtStartDate.Text);
            DateTime StartHDate = Convert.ToDateTime(txtLStartDate.Text);
            DateTime EndDate = Convert.ToDateTime(txtEndDate.Text);
            DateTime EndHDate = Convert.ToDateTime(txtLEndDate.Text);
            String UnitCode = Request.Params["UnitCode"];
            String UnitID = KPI_UnitDal.GetUnitIDByCode(UnitCode);
            using (ECHistoryDataAccess DataAccess = new ECHistoryDataAccess()) {
                KPIRepeater.DataSource = DataAccess.GetKPIContrast(UnitID, StartDate, EndDate, StartHDate, EndHDate);
            }
            base.DataBind();
        }

        #endregion

        #region 属性
        protected String StartDate {
            get {
                return txtLStartDate.Text;
            }
        }

        protected String  EndDate {
            get {
                return txtEndDate.Text;
            }
        }

        protected String LStartDate {
            get {
                return txtLStartDate.Text;
            }
        }

        protected String LEndDate {
            get {
                return txtLEndDate.Text;
            }
        }

        #endregion

        #region 事件

        protected String GetNumericString(Object Value) {
            if (Value == null) return "";
            decimal d = Convert.ToDecimal(Value);
            return d.ToString("0.00");
        }

        protected void btnSearch_Click(Object sender, EventArgs e) {
            DataBind();
        }


        protected void btnExport_Click(Object sender, EventArgs e) {
            Response.ContentEncoding = Encoding.UTF8;
            Response.ContentType = "application/ms-excel";
            Response.Charset = "utf-8";
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + 
                HttpUtility.UrlEncode("经济指标对比分析报表.xls", System.Text.Encoding.UTF8).ToString());
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            KPIRepeater.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.Flush();
            Response.End();
            sw.Close();
            sw.Dispose();
        }

        #endregion

        #region 私有方法

        private void ClientInitial() {
            DateTime Now = DateTime.Now;
            DateTime CurrentDate = new DateTime(Now.Year, Now.Month, 1);
            txtStartDate.Text = CurrentDate.ToString("yyyy-MM-dd");
            txtEndDate.Text = CurrentDate.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
            txtLStartDate.Text = CurrentDate.AddMonths(-1).ToString("yyyy-MM-dd");
            txtLEndDate.Text = CurrentDate.AddDays(-1).ToString("yyyy-MM-dd"); 
        }

        #endregion
    }
}