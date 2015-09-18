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
    public partial class PlantBonusPage : System.Web.UI.Page {

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
                //if (!string.IsNullOrEmpty(YearMonth)&&(YearMonth.Length>6))	YearMonth = YearMonth.Substring(0, 4) + YearMonth.Substring(5, 2);
                PlantBonusRepeater.DataSource = DataAccess.GetPlantBonus(YearMonth);
            }
            base.DataBind();          
        }

        #endregion

        #region 重写方法

        protected String GetShiftName(Object Shift) {
            String[] Shifts = { "", "一值", "二值", "三值", "四值", "五值", "六值", "七值", "八值", "九值", "十值" };
            int Index = Convert.ToInt32(Shift);
            if (Index <= 0) return "";
            return Shifts[Index];
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
			Response.AppendHeader("Content-Disposition", "attachment;filename=" + 
                HttpUtility.UrlEncode("全厂奖金分配表.xls", System.Text.Encoding.UTF8).ToString());
			StringWriter sw = new StringWriter();
			HtmlTextWriter htw = new HtmlTextWriter(sw);
			PlantBonusRepeater.RenderControl(htw);
			Response.Write(sw.ToString());
			Response.Flush();
			Response.End();
			sw.Close();
			sw.Dispose();
		}

		#endregion
	}
   
}