using SIS.DataAccess;
using SIS.DataEntity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using System.Data;

namespace SISKPI.KPIWeb {
    public partial class ECChartPage : System.Web.UI.Page {

        #region 重写方法

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            if (!IsPostBack) {
                ClientInitial();
                DataBind();
                ChartIntial();
            }
        }

        public override void DataBind() {
            int RecordCount = 0;
            DateTime StartDate = Convert.ToDateTime(txtStartDate.Text);
            DateTime EndDate = Convert.ToDateTime(txtEndDate.Text);
            String ECID = Request.Params["ECID"];
            using (ECHistoryDataAccess DataAccess = new ECHistoryDataAccess()) {
                ECDataRepeater.DataSource = DataAccess.GetECHourData(ECID, StartDate, EndDate,
                    Pager.CurrentPageIndex, Pager.PageSize, out RecordCount);
                Pager.RecordCount = RecordCount;
            }
            base.DataBind();
        }

        #endregion

        #region 事件

        protected void Pager_PageChanged(Object Sender, EventArgs e) {
            DataBind();
        }

        protected void btnSearch_Click(Object Sender, EventArgs e) {
            DataBind();
            ChartIntial();
        }

        protected void btnExport_Click(Object sender, EventArgs e) {
            Response.ContentEncoding = Encoding.UTF8;
            Response.ContentType = "application/ms-excel";
            Response.Charset = "utf-8";
            Response.AppendHeader("Content-Disposition",
                "attachment;filename=" + HttpUtility.UrlEncode("指标历史数据.xls",
                System.Text.Encoding.UTF8).ToString());
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            int RecordCount = 0;
            DateTime StartDate = Convert.ToDateTime(txtStartDate.Text);
            DateTime EndDate = Convert.ToDateTime(txtEndDate.Text);
            String ECID = Request.Params["ECID"];
            using (ECHistoryDataAccess DataAccess = new ECHistoryDataAccess()) {
                ECDataRepeater.DataSource = DataAccess.GetECHourData(ECID, StartDate, EndDate,
                   1, 20000, out RecordCount);
                ECDataRepeater.DataBind();
                ECDataRepeater.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.Flush();
                Response.End();
                sw.Close();
                sw.Dispose();
            }
            DataBind();
        }

        #endregion

        #region 私有方法

        private void ClientInitial() {
            txtStartDate.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            txtEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        protected String ECName {
            get {
                return HttpUtility.UrlDecode(Request.Params["ECName"]);
            }
        }

        protected String ReturnURL {
            get {
                String Kind = Request.Params["Kind"];
                String Result = "KPI_ECValue.aspx?plantcode=ghnd&unitcode=ghnd01&ecweb=JJZB";
                if (Kind == "2") {
                    Result = "CoalConsumptionPage.aspx?plantcode=ghnd&unitcode=ghnd01&ecweb=ZHMH";
                }
                return Result;
            }
        }

        private void ChartIntial() {
            Title m_Title = Chart1.Titles[0];
            m_Title.Font = new Font("宋体", 12f);
            m_Title.Font = new Font("宋体", 12f, System.Drawing.FontStyle.Bold);
            //String ECName = HttpUtility.UrlDecode(Request.Params["ECName"]);
            m_Title.Text = String.Format("{0}{1}至{2}历史曲线", ECName, txtStartDate.Text, txtEndDate.Text);
            ChartArea chartaera = Chart1.ChartAreas["ChartArea1"];
            chartaera.AxisX.LabelStyle.Font = new Font("宋体", 9.75f);
            chartaera.AxisY.LabelStyle.Font = new Font("宋体", 9.75f);
            Series MinValueSeries = Chart1.Series["MinValueSeries"];
            Series AvgValueSeries = Chart1.Series["AvgValueSeries"];
            Series MaxValueSeries = Chart1.Series["MaxValueSeries"];
            using (ECHistoryDataAccess DataAccess = new ECHistoryDataAccess()) {
                int RecordCount = 0;
                DateTime StartDate = Convert.ToDateTime(txtStartDate.Text);
                DateTime EndDate = Convert.ToDateTime(txtEndDate.Text);
                String ECID = Request.Params["ECID"];
                List<ECHistoryDataEntity> DataSource = DataAccess.GetECHourData(ECID, StartDate, EndDate,
                   1, 20000, out RecordCount);
                foreach (ECHistoryDataEntity ChartData in DataSource) {
                    MinValueSeries.Points.AddXY(ChartData.CheckDate,
                        Convert.ToDouble(ChartData.MinValue));
                    AvgValueSeries.Points.AddXY(ChartData.CheckDate,
                        Convert.ToDouble(ChartData.AvgValue));
                    MaxValueSeries.Points.AddXY(ChartData.CheckDate,
                        Convert.ToDouble(ChartData.MaxValue));
                }
            }
        }

        #endregion
    }
}