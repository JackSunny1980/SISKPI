using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using System.IO;
using SIS.DataAccess;
using SIS.DataEntity;

namespace SISKPI.KPI {
	public partial class KPI_SATagValue : System.Web.UI.Page {


		#region 重写方法

		protected override void OnLoad(EventArgs e) {
			base.OnLoad(e);
			if (!IsPostBack) {
				ClientInitial();				
				DataBind();
			}
		}

		public override void DataBind() {
			using (KPI_SATagValueDal DataAccess = new KPI_SATagValueDal()) {				
				DateTime StartDate = Convert.ToDateTime(txtStartDate.Text);
                DateTime EndDate = Convert.ToDateTime(txtEndDate.Text);
				SATagValueRepeater.DataSource = DataAccess.GetKPI_SATagValues(StartDate, EndDate, drpUnits.SelectedValue);
			}
			base.DataBind();
			ColSpan();
		}

		#endregion

		#region 私有方法

		public void ClientInitial() {
			DateTime StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
			DateTime EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
			if (StartDate == EndDate) {
				StartDate = StartDate.AddMonths(-1);
			}
            txtStartDate.Text = StartDate.ToString("yyyy-MM-dd");
            txtEndDate.Text = EndDate.ToString("yyyy-MM-dd");
			using(KPI_UnitDal DataAccess = new  KPI_UnitDal()){
				List<KPI_UnitEntity> UnitList = DataAccess.GetUnitIDs("");
				foreach (KPI_UnitEntity Entity in UnitList) {
					if (Entity.UnitID !="0000")	drpUnits.Items.Add(new ListItem(Entity.UnitName,Entity.UnitID));
				}
			}
		}

		private void ColSpan() {
			for (int i = SATagValueRepeater.Items.Count - 1; i > 0; i--) {
				HtmlTableCell oCell_previous = SATagValueRepeater.Items[i - 1].FindControl("Shift") as HtmlTableCell;
				HtmlTableCell oCell = SATagValueRepeater.Items[i].FindControl("Shift") as HtmlTableCell;
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
			Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode("安全指标绩效统计.xls", System.Text.Encoding.UTF8).ToString());
			StringWriter sw = new StringWriter();
			HtmlTextWriter htw = new HtmlTextWriter(sw);
			SATagValueRepeater.RenderControl(htw);
			Response.Write(sw.ToString());
			Response.Flush();
			Response.End();
			sw.Close();
			sw.Dispose();
		}

		#endregion
	}
}