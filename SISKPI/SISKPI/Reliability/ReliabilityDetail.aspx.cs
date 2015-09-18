using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIS.DataAccess;
using System.Text;
using System.IO;

namespace SISKPI.Reliability {
	public partial class ReliabilityDetail : System.Web.UI.Page {
		#region 重写方法

		protected override void OnLoad(EventArgs e) {
			base.OnLoad(e);
			if (!IsPostBack) {
				DateTime StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
				DateTime EndDate = StartDate.AddMonths(1);
				txtStartDate.Text = StartDate.ToString("yyyy-MM-dd");
				txtEndDate.Text = EndDate.ToString("yyyy-MM-dd");
				DataBind();
			}
		}

		public override void DataBind() {
			using (KPI_OverLimitRecordDal DataAccess = new KPI_OverLimitRecordDal()) {
				DateTime StartDate = Convert.ToDateTime(txtStartDate.Text);
				DateTime EndDate = Convert.ToDateTime(txtEndDate.Text);
				int RecordCount = 0;
				String TageID = Request.Params["TagID"];
				Repeater.DataSource = DataAccess.GetReliabilitys(Pager.CurrentPageIndex,Pager.PageSize,
					StartDate, EndDate, TageID,"",out RecordCount);
				Pager.RecordCount = RecordCount;

			}
			base.DataBind();
			base.DataBind();			
		}

		#endregion

		#region 私有方法

		private void ColSpan() {
			//for (int i = ShiftBonusRepeater.Items.Count - 1; i > 0; i--) {
			//    HtmlTableCell oCell_previous = ShiftBonusRepeater.Items[i - 1].FindControl("UnitName") as HtmlTableCell;
			//    HtmlTableCell oCell = ShiftBonusRepeater.Items[i].FindControl("UnitName") as HtmlTableCell;
			//    oCell.RowSpan = (oCell.RowSpan == -1) ? 1 : oCell.RowSpan;
			//    oCell_previous.RowSpan = (oCell_previous.RowSpan == -1) ? 1 : oCell_previous.RowSpan;
			//    if (oCell.InnerText == oCell_previous.InnerText) {
			//        oCell.Visible = false;
			//        oCell_previous.RowSpan += oCell.RowSpan;
			//    }
			//}
		}

		#endregion

		#region 事件

		protected void Pager_PageChanged(Object sender, EventArgs e) {
			DataBind();
		}

		protected void btnSearch_Click(Object sender, EventArgs e) {
			DataBind();
		}

		protected void btnExport_Click(Object sender, EventArgs e) {
			Response.ContentEncoding = Encoding.UTF8;
			Response.ContentType = "application/ms-excel";
			Response.Charset = "utf-8";
			Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode("设备可靠性台账明细.xls", System.Text.Encoding.UTF8).ToString());
			StringWriter sw = new StringWriter();
			HtmlTextWriter htw = new HtmlTextWriter(sw);
			Repeater.RenderControl(htw);
			Response.Write(sw.ToString());
			Response.Flush();
			Response.End();
			sw.Close();
			sw.Dispose();
		}

		#endregion
	}
}