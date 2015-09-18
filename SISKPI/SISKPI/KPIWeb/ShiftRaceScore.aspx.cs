using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;
using SIS.DataAccess;

namespace SISKPI.KPIWeb {

    /// <summary>
    /// 值际竞赛月报表
    /// </summary>
	public partial class ShiftRaceScore : System.Web.UI.Page {


		#region 重写方法

		protected override void OnLoad(EventArgs e) {
			base.OnLoad(e);
			if (!IsPostBack) {
				txtYearMonth.Text = DateTime.Now.ToString("yyyy-MM");
				DataBind();
			}
		}

		public override void DataBind() {
			using (ReportDal DataAccess = new ReportDal()) {
                String YearMonth = txtYearMonth.Text;
				String SpecialField = Request.Params["SpecialField"];
				if (!string.IsNullOrEmpty(YearMonth) && (YearMonth.Length > 6)) YearMonth = YearMonth.Substring(0, 4) + YearMonth.Substring(5, 2);
				ShiftBonusRepeater.DataSource = DataAccess.GetShiftRaces(YearMonth, SpecialField);
			}
			base.DataBind();
			ColSpan();
		}

		#endregion

		#region 私有方法

		private void ColSpan() {
			for (int i = ShiftBonusRepeater.Items.Count - 1; i > 0; i--) {
				HtmlTableCell oCell_previous = ShiftBonusRepeater.Items[i - 1].FindControl("shift") as HtmlTableCell;
				HtmlTableCell oCell = ShiftBonusRepeater.Items[i].FindControl("shift") as HtmlTableCell;
				oCell.RowSpan = (oCell.RowSpan == -1) ? 1 : oCell.RowSpan;
				oCell_previous.RowSpan = (oCell_previous.RowSpan == -1) ? 1 : oCell_previous.RowSpan;
				if (oCell.InnerText == oCell_previous.InnerText) {
					oCell.Visible = false;
					oCell_previous.RowSpan += oCell.RowSpan;
				}

				oCell_previous = ShiftBonusRepeater.Items[i - 1].FindControl("unitMonthScore") as HtmlTableCell;
				oCell = ShiftBonusRepeater.Items[i].FindControl("unitMonthScore") as HtmlTableCell;
				oCell.RowSpan = (oCell.RowSpan == -1) ? 1 : oCell.RowSpan;
				oCell_previous.RowSpan = (oCell_previous.RowSpan == -1) ? 1 : oCell_previous.RowSpan;
				if (oCell.InnerText == oCell_previous.InnerText) {
					oCell.Visible = false;
					oCell_previous.RowSpan += oCell.RowSpan;
				}


				oCell_previous = ShiftBonusRepeater.Items[i - 1].FindControl("unitMonthOrder") as HtmlTableCell;
				oCell = ShiftBonusRepeater.Items[i].FindControl("unitMonthOrder") as HtmlTableCell;
				oCell.RowSpan = (oCell.RowSpan == -1) ? 1 : oCell.RowSpan;
				oCell_previous.RowSpan = (oCell_previous.RowSpan == -1) ? 1 : oCell_previous.RowSpan;
				if (oCell.InnerText == oCell_previous.InnerText) {
					oCell.Visible = false;
					oCell_previous.RowSpan += oCell.RowSpan;
				}

				oCell_previous = ShiftBonusRepeater.Items[i - 1].FindControl("unitYearScore") as HtmlTableCell;
				oCell = ShiftBonusRepeater.Items[i].FindControl("unitYearScore") as HtmlTableCell;
				oCell.RowSpan = (oCell.RowSpan == -1) ? 1 : oCell.RowSpan;
				oCell_previous.RowSpan = (oCell_previous.RowSpan == -1) ? 1 : oCell_previous.RowSpan;
				if (oCell.InnerText == oCell_previous.InnerText) {
					oCell.Visible = false;
					oCell_previous.RowSpan += oCell.RowSpan;
				}

				oCell_previous = ShiftBonusRepeater.Items[i - 1].FindControl("unitYearOrder") as HtmlTableCell;
				oCell = ShiftBonusRepeater.Items[i].FindControl("unitYearOrder") as HtmlTableCell;
				oCell.RowSpan = (oCell.RowSpan == -1) ? 1 : oCell.RowSpan;
				oCell_previous.RowSpan = (oCell_previous.RowSpan == -1) ? 1 : oCell_previous.RowSpan;
				if (oCell.InnerText == oCell_previous.InnerText) {
					oCell.Visible = false;
					oCell_previous.RowSpan += oCell.RowSpan;
				}
			}
		}

		#endregion

        #region 重写方法

        protected String GetShiftName(Object Shift) {
            String[] Shifts = { "", "一值", "二值", "三值", "四值", "五值", "六值", "七值", "八值", "九值", "十值" };
            int Index = Convert.ToInt32(Shift);
            if (Index <= 0) return "";
            return Shifts[Index];
        }

        protected String GetRank(Object Rank) {
            String[] Ranks = { "", "第一名", "第二名", "第三名", "第四名", "第五名", 
                                   "第六名", "第七名", "第八名", "第九名", "第十名",
                                   "第十一名", "第十二名", "第十三名", "第十四名", "第十五名"};
            int Index = Convert.ToInt32(Rank);
            if (Index <= 0) return "";
            return Ranks[Index];
        }

        #endregion

		#region 事件

		protected void btnSearch_Click(Object sender, EventArgs e) {
			DataBind();
		}

		protected void btnExport_Click(Object sender, EventArgs e) {
			String Header = "<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset=utf-8\"/>";
			Response.Buffer = true;
			Response.ContentEncoding = Encoding.UTF8;
			Response.ContentType = "application/vnd.ms-excel";
			Response.Charset = "utf-8";
			//Response.ContentEncoding = Encoding.UTF8;
			Response.AppendHeader("Content-Disposition",
				"attachment;filename=" + HttpUtility.UrlEncode("值际竞赛月报表.xls", Encoding.UTF8).ToString());
			StringWriter sw = new StringWriter();
			HtmlTextWriter htw = new HtmlTextWriter(sw);
			ShiftBonusRepeater.RenderControl(htw);
			Response.Clear();
			Response.Write(Header + sw.ToString());
			Response.Flush();
			Response.End();
			//sw.Close();
			//sw.Dispose();
		}

		#endregion
	}
}