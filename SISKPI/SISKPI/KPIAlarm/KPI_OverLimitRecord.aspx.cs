using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIS.Exceler;
using SISKPI.KPIAlarm.Helper;
using System.IO;
using System.Text;
using System.Data;
using SIS.DataAccess;
using SIS.DataEntity;

namespace SISKPI.KPIAlarm {
	public partial class KPI_OverLimitRecord : System.Web.UI.Page {

		private readonly IKPI_OverLimitRecordDal overLimitRecordDal = DataModuleFactory.CreateKPI_OverLimitRecordDal();
		private Dictionary<int,String> AlarmTypeList;

		#region 重写方法

		protected override void OnLoad(EventArgs e) {
			base.OnLoad(e);
			if (!IsPostBack) {
				ClientInitial();
				DataBind();
			}
		}


		/// <summary>
		/// 数据绑定
		/// </summary>
		public override void DataBind() {
			int totalCount = 0;
			string kpiName = drpKPI.SelectedValue;
			string unityValue = drpUnits.SelectedValue;
			DateTime beginTime = Convert.ToDateTime(txtBeginTime.Text);
            //beginTime = beginTime.AddHours(1);
			DateTime endTime = Convert.ToDateTime(txtEndTime.Text);
			endTime = endTime.AddHours(24);
			OverLimitRecordRepeater.DataSource = overLimitRecordDal.SearchOverLimitRecord(Pager.CurrentPageIndex, 
				Pager.PageSize, kpiName, unityValue,drpShifts.SelectedValue, beginTime, endTime, out totalCount);
			Pager.RecordCount = totalCount;
			base.DataBind();
		}

		#endregion


		#region 私有方法

		private void ClientInitial() {
			DateTime Current = DateTime.Now;
			//txtBeginTime.Attributes.Add("onclick", "'new WdatePicker(this,'%Y年%M月%D日',false,'whyGreen')'");
			//txtEndTime.Attributes.Add("onclick", "new WdatePicker(this,'%Y年%M月%D日',false,'whyGreen')");
			txtBeginTime.Text = Current.ToString("yyyy-MM-dd");
			txtEndTime.Text = Current.ToString("yyyy-MM-dd");

            //List<KPI_UnitEntity> UnitList = KPI_UnitDal.GetAllEntity();
            //UnitList.Insert(0, new KPI_UnitEntity { UnitID = "", UnitName = "【所有机组】" });
            //drpUnits.DataSource = UnitList;
            //drpUnits.DataTextField = "UnitName";
            //drpUnits.DataValueField = "UnitID";
            //drpUnits.DataBind();
            //DataTable Plants = KPI_PlantDal.GetDataTable();
            //drpPlants.DataSource = Plants;
            //drpPlants.DataValueField = "PlantID";
            //drpPlants.DataTextField = "PlantName";
            //drpPlants.DataBind();
            //if (Plants.Rows.Count > 0) {
            //    String PlantID = Plants.Rows[0]["PlantID"] + "";
               
            //}
            string plantCode = Request.Params["Plantcode"];
            drpUnits.DataSource = KPI_UnitDal.GetUnitListByPlantCode(plantCode);
            drpUnits.DataTextField = "UnitName";
            drpUnits.DataValueField = "UnitID";
            drpUnits.DataBind();
            BindSAKPI();
            //string plantid = KPI_PlantDal.GetPlantIDByCode(plantCode);
            //BindUnits(plantid);
		}
		private void BindSAKPI() {
			List<KPI_SATagEntity> DataSource;
			using (KPI_SATagDal SATagDal = new KPI_SATagDal()) {
				if (String.IsNullOrEmpty(drpUnits.SelectedValue)) {
					DataSource = SATagDal.GetSATags();
				}
				else {
					DataSource = SATagDal.GetSATagList(drpUnits.SelectedValue);
				}
				KPI_SATagEntity item = new KPI_SATagEntity {
					SAID = "",
					SAName = "【所有指标】"
				};
				DataSource.Insert(0, item);
				drpKPI.DataSource = DataSource;
				drpKPI.DataTextField = "SAName";
				drpKPI.DataValueField = "SAID";
				drpKPI.DataBind();
			}
		}

 
		#endregion

		#region 事件

		protected String GetAlarmType(Object AlarmType) {
			int Key = Convert.ToInt32(AlarmType);			
			if (Key == -3) return  "低三限";
			if (Key == -2) return "低二限";
			if (Key == -1) return "低一限";
			if (Key == 1) return "高一限";
			if (Key == 2) return "高二限";
			if (Key == 3) return "高三限";
			if (Key == 4) return "高四限";
			return "";			
		}

		protected void Pager_PageChanged(object src, EventArgs e) {
			DataBind();
		}

        //protected void drpPlants_SelectedIndexChanged(object sender, EventArgs e) {
        //    BindUnits(drpPlants.SelectedValue);
        //}

		protected void drpUnits_SelectedIndexChanged(object sender, EventArgs e) {
			BindSAKPI();
		}

		protected void btnSearch_Click(object sender, EventArgs e) {
			DataBind();
		}

		protected void btnExport_Click(object sender, EventArgs e) {
			int totalCount = 0;
			string kpiName = drpKPI.SelectedValue;
			string unityValue = drpUnits.SelectedValue;
			DateTime beginTime = Convert.ToDateTime(txtBeginTime.Text);
			beginTime = beginTime.AddHours(1);
			DateTime endTime = Convert.ToDateTime(txtEndTime.Text);
			endTime = endTime.AddHours(25);
			OverLimitRecordRepeater.DataSource = overLimitRecordDal.SearchOverLimitRecord(1,
				int.MaxValue, kpiName, unityValue, drpShifts.SelectedValue, beginTime, endTime, out totalCount);
			OverLimitRecordRepeater.DataBind();
			Response.ContentEncoding = Encoding.UTF8;
			Response.ContentType = "application/ms-excel";
			Response.Charset = "utf-8";
			Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode("超限明细报表.xls", System.Text.Encoding.UTF8).ToString());
			StringWriter sw = new StringWriter();
			HtmlTextWriter htw = new HtmlTextWriter(sw);
			OverLimitRecordRepeater.RenderControl(htw);
			Response.Write(sw.ToString());
			Response.Flush();
			Response.End();
			sw.Close();
			sw.Dispose();		
			DataBind();
		}

		#endregion
	}

}