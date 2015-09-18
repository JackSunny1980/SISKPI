using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Data;
using SIS.DataAccess;
using SIS.DataEntity;

namespace SISKPI.KPIWeb {

    public partial class PersonScore : System.Web.UI.Page {

        #region 重写方法

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            if (!IsPostBack) {

                txtYearMonth.Text = DateTime.Now.ToString("yyyy-MM");
                BindPosition();
                DataBind();
            }
        }

        public override void DataBind() {
            using (ReportDal DataAccess = new ReportDal()) {
                String YearMonth = txtYearMonth.Text;
                String SpecialField = Request.Params["SpecialField"];
                string position = ddlPosition.SelectedValue;
                if (!string.IsNullOrEmpty(YearMonth) && (YearMonth.Length > 6)) YearMonth = YearMonth.Substring(0, 4) + YearMonth.Substring(5, 2);
                List<PersonScoreEntity> resultDataList = DataAccess.GetPersonScore(YearMonth, SpecialField);

                if (position != "ALL") {
                    resultDataList = resultDataList.Where(r => r.PositionID == position).ToList();
                }
                PersonScoreRepeater.DataSource = resultDataList;
            }
            base.DataBind();
            ColSpan();
        }
        #endregion


        #region 私有方法

        private void ColSpan() {
            for (int i = PersonScoreRepeater.Items.Count - 1; i > 0; i--) {
                HtmlTableCell oCell_previous = PersonScoreRepeater.Items[i - 1].FindControl("Shift") as HtmlTableCell;
                HtmlTableCell oCell = PersonScoreRepeater.Items[i].FindControl("Shift") as HtmlTableCell;
                oCell.RowSpan = (oCell.RowSpan == -1) ? 1 : oCell.RowSpan;
                oCell_previous.RowSpan = (oCell_previous.RowSpan == -1) ? 1 : oCell_previous.RowSpan;
                if (oCell.InnerText == oCell_previous.InnerText) {
                    oCell.Visible = false;
                    oCell_previous.RowSpan += oCell.RowSpan;
                }
            }
        }

        #endregion



        #region 事件

        protected void btnSearch_Click(Object sender, EventArgs e) {

            DataBind();
        }

        protected void btnExport_Click(Object sender, EventArgs e) {
            Response.ContentEncoding = Encoding.UTF8;
            Response.ContentType = "application/ms-excel";
            Response.Charset = "utf-8";
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode("个人得分报表.xls", System.Text.Encoding.UTF8).ToString());
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            PersonScoreRepeater.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.Flush();
            Response.End();
            sw.Close();
            sw.Dispose();
        }

        #endregion

        private void BindPosition() {
            DataTable dt = KPI_PositionDal.GetDataTable();
            ddlPosition.Items.Add(new ListItem("全部", "ALL"));
            foreach (DataRow dr in dt.Rows) {
                ddlPosition.Items.Add(new ListItem(dr["PositionName"].ToString(), dr["PositionID"].ToString()));
            }
            ddlPosition.SelectedValue = "ALL";
        }

    }
}