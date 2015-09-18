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

namespace SISKPI.Reliability {
	public partial class ReliabilityLedger : System.Web.UI.Page {


		#region 重写方法

		protected override void OnLoad(EventArgs e) {
			base.OnLoad(e);
			if (!IsPostBack) {
				txtYearMonth.Text = DateTime.Now.ToString("yyyy-MM");
				DataBind();
			}
		}

		public override void DataBind() {
			using (KPI_OverLimitRecordDal DataAccess = new KPI_OverLimitRecordDal()) {
				String YearMonth = txtYearMonth.Text;
				String SpecialField = Request.Params["SpecialField"];
				int Year, Month;
				Year = Convert.ToInt32(YearMonth.Substring(0, 4));
				Month = Convert.ToInt32(YearMonth.Substring(5, 2));
				DateTime StartDate = new DateTime(Year, Month, 1);
				DateTime EndDate = StartDate.AddMonths(1);
				//Repeater.DataSource = DataAccess.GetReliabilityTotals(StartDate, EndDate);
			}
			base.DataBind();
			
		}

		#endregion

		#region 私有方法

		

		#endregion

		#region 事件

		protected void btnSearch_Click(Object sender, EventArgs e) {
			DataBind();
		}

		protected void btnExport_Click(Object sender, EventArgs e) {
			Response.ContentEncoding = Encoding.UTF8;
			Response.ContentType = "application/vnd.ms-excel";
			Response.Charset = "utf-8";
			Response.ContentEncoding = Encoding.UTF8;
			Response.AppendHeader("Content-Disposition",
				"attachment;filename=" + HttpUtility.UrlEncode("设备可靠性台账.xls", Encoding.UTF8).ToString());
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