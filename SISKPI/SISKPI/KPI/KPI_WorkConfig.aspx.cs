using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web.UI.HtmlControls;

using SIS.DataAccess;
using SIS.Assistant;
using SIS.DataEntity;
using SIS.Loger;
using SIS.Assistant.WS;
using SIS.DBControl;

namespace SISKPI {
	public partial class KPI_WorkConfig : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {
			if (!IsPostBack) {
				btnAddWork.Attributes.Add("onclick", "javascript:return confirm('确认倒班配置录入数据库吗？');");

				//if (Request.QueryString["title"] != null)
				//{
				//    string title = OPM_TitleDal.GetTitle(Request.QueryString["title"].ToString());
				//    if (title != "")
				//    {
				//        lblTitle.Text = title;
				//    }
				//}



				//绑定参数信息
				//DataTable dtParam = Seek_ParamDal.GetParamsFromUnitID(UnitID);

				//foreach (DataRow dr in dtParam.Rows)
				//{
				//    ddl_ParamID.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
				//}

				//
				txt_WorkID.Value = "";

				//日期初始化
				txt_date.Value = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd");

				BindWork();

				BindValues();

			}
		}

		void BindWork() {
			DataTable dt = KPI_WorkDal.GetWorkList();
			gvWork.DataSource = dt;
			gvWork.DataBind();

		}

		void BindWorkID(string WorkID) {
			//初始化
			KPI_WorkEntity kwe = KPI_WorkDal.GetEntity(WorkID);

			txt_WorkID.Value = WorkID;

			int nShifts = kwe.WorkBaseShifts;
			int nDays = kwe.WorkBaseDays;


			ddl_BaseDays.SelectedValue = nDays.ToString();
			ddl_BaseShifts.SelectedValue = nShifts.ToString();

			txt_WorkName.Value = kwe.WorkName;
			txt_WorkDesc.Value = kwe.WorkDesc;

			ddl_WorkIsValid.SelectedValue = kwe.WorkIsValid.ToString();

			txt_WorkNote.Value = kwe.WorkNote;

			txt_date.Value = kwe.WorkStartTime;

			string sWorkSequence = kwe.WorkSequence;
			string sWorkShift = kwe.WorkShift;

			string[] ws = sWorkSequence.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
			string[] fs = sWorkShift.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);


			//值初始化
			for (int i = 0; i < nShifts; i++) {
				string id = string.Format("ddl_shift{0:d2}", i);
				DropDownList ddl = (DropDownList)this.FindControl(id);

				ddl.SelectedValue = fs[i];
			}

			//班初始化   
			for (int j = 0; j < nDays; j++) {
				for (int i = 0; i < nShifts; i++) {
					string id = string.Format("ddl_period{0}{1}", i, j);
					DropDownList ddl = (DropDownList)this.FindControl(id);
					ddl.SelectedValue = ws[j * nShifts + i];
				}
			}
			SetDisplay();

		}

		void BindValues() {
			int nShifts = 5;
			int nDays = 90;

			//值初始化
			for (int i = 0; i < nShifts; i++) {
				string id = string.Format("ddl_shift{0:d2}", i);
				DropDownList ddl = (DropDownList)this.FindControl(id);

				ddl.DataSourceID = "GetShiftCodes";
				ddl.DataTextField = "Name";
				ddl.DataValueField = "Code";
				ddl.DataBind();
			}

			//班初始化   
			for (int j = 0; j < nDays; j++) {
				for (int i = 0; i < nShifts; i++) {
					string id = string.Format("ddl_period{0}{1}", i, j);
					DropDownList ddl = (DropDownList)this.FindControl(id);
					ddl.DataSourceID = "GetPeriodCodes";
					ddl.DataTextField = "Name";
					ddl.DataValueField = "Code";
					ddl.DataBind();
				}
			}




			SetDisplay();
		}


		/// <summary>
		/// 绑定数据
		/// </summary>
		void SetDisplay() {
			int nShifts = int.Parse(ddl_BaseShifts.SelectedValue);
			int nDays = int.Parse(ddl_BaseDays.SelectedValue);

			//所有的显示
			/////////////////////////////////////////////////////////
			tr_00.Style.Add("display", "block");
			tr_01.Style.Add("display", "block");
			tr_02.Style.Add("display", "block");
			tr_03.Style.Add("display", "block");
			tr_04.Style.Add("display", "block");

			for (int j = 0; j < 10; j++) {
				string id = string.Format("lbl_day{0:d2}", j);
				Label lbl = (Label)this.FindControl(id);
				lbl.Visible = true;

				for (int i = 0; i < 5; i++) {
					id = string.Format("ddl_period{0}{1}", i, j);
					DropDownList ddl = (DropDownList)this.FindControl(id);
					ddl.Visible = true;
				}
			}

			////////////////////////////////////////////////////////
			//按行隐藏
			for (int i = nShifts; i < 5; i++) {
				SetRowHide(i);
			}

			//按列隐藏
			for (int j = nDays; j < 10; j++) {
				//SetRowDisplay(j);
				string id = string.Format("lbl_day{0:d2}", j);
				Label lbl = (Label)this.FindControl(id);
				lbl.Visible = false;
				for (int n = 0; n < 5; n++) {
					id = "ddl_period" + n.ToString() + j.ToString();
					DropDownList ddl = (DropDownList)this.FindControl(id);
					ddl.Visible = false;
				}
			}
			if (nDays < 11) {
				tbody.Style.Add("display","none");
			}
			if (nDays > 10) {
				tbody.Style.Add("display", "block");
			}
			

		}

		void SetRowHide(int nIndex) {
			switch (nIndex) {
				case 0:
					tr_00.Style.Add("display", "none");
					break;
				case 1:
					tr_01.Style.Add("display", "none");
					break;
				case 2:
					tr_02.Style.Add("display", "none");
					break;
				case 3:
					tr_03.Style.Add("display", "none");
					break;
				case 4:
					tr_04.Style.Add("display", "none");
					break;

				default:
					break;
			}
			int nShifts = int.Parse(ddl_BaseShifts.SelectedValue);
			HtmlTableRow tr;
			String id;
			int rowNo;
			for (int i = nShifts; i < 5; i++) {
				for (int k = 1; k < 9; k++) {
					rowNo = k * 5 + i;
					id = "tr_" + rowNo;
					if (rowNo < 10) id = "tr_0" + rowNo;
					tr = (HtmlTableRow)FindControl(id);
					tr.Style.Add("display", "none");
				}
			}
			//TableRow tr = (TableRow)FindControl("");
			//tr.Style("", "");

		}

		#region GridView

		protected void gvWork_RowDataBound(object sender, GridViewRowEventArgs e) {
			if (e.Row.RowType == DataControlRowType.DataRow) {
				if ((LinkButton)e.Row.FindControl("delete") != null) {
					LinkButton lb = (LinkButton)e.Row.FindControl("delete");
					lb.Attributes.Add("onclick", "javascript:return confirm('确定删除吗？');");
				}

				DataRowView drv = (DataRowView)e.Row.DataItem;

				if ((DropDownList)e.Row.FindControl("ddlValid") != null) {
					DropDownList ddlCollege = (DropDownList)e.Row.FindControl("ddlValid");

					ddlCollege.SelectedValue = drv["WorkIsValid"].ToString();
				}

				//鼠标移到效果
				e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
				e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");
			}
		}

		protected void gvWork_RowCommand(object sender, GridViewCommandEventArgs e) {
			string keyid = e.CommandArgument.ToString();

			if (e.CommandName == "dataDelete") {
				if (KPI_WorkDal.DeleteWork(keyid)) {
					MessageBox.popupClientMessage(this.Page, "删除成功！", "call();");

					BindWork();
				}
				else {
					MessageBox.popupClientMessage(this.Page, "删除错误！", "call();");
				}
			}
			else if (e.CommandName == "dataView") {
				BindWorkID(keyid);
			}
		}

		#endregion


		#region 【按钮事件、调用方法】

		protected void ddl_BaseDays_SelectedIndexChanged(object sender, EventArgs e) {
			txt_WorkID.Value = "";
			SetDisplay();
		}

		protected void ddl_BaseShifts_SelectedIndexChanged(object sender, EventArgs e) {
			txt_WorkID.Value = "";
			SetDisplay();
		}



		protected void btnEditWork_Click(object sender, EventArgs e) {
			if (txt_WorkID.Value == "") {
				MessageBox.popupClientMessage(this.Page, "未选定倒班计划，请使用另存按钮！", "call();");
			}
			else {
				int nShifts = int.Parse(ddl_BaseShifts.SelectedValue);
				int nDays = int.Parse(ddl_BaseDays.SelectedValue);

				string sWorkSequence = "";
				//WorkSequence
				for (int j = 0; j < nDays; j++) {
					for (int i = 0; i < nShifts; i++) {
						string id = string.Format("ddl_period{0}{1}", i, j);
						DropDownList ddl = (DropDownList)this.FindControl(id);
						sWorkSequence += ddl.SelectedValue + "-";
					}
				}

				string sWorkShift = "";
				//WorkShift
				for (int i = 0; i < nShifts; i++) {
					string id = string.Format("ddl_shift0{0}", i);
					DropDownList ddl = (DropDownList)this.FindControl(id);

					sWorkShift += ddl.SelectedValue + "-";
				}

				string keyid = txt_WorkID.Value;

				//////////////////////////////////////////////////////////////////////////////
				KPI_WorkEntity kwe = new KPI_WorkEntity();
				kwe.WorkID = keyid;
				kwe.WorkName = txt_WorkName.Value;
				kwe.WorkDesc = txt_WorkDesc.Value;
				kwe.WorkStartTime = txt_date.Value;
				kwe.WorkEndTime = DateTime.Now.AddYears(10).ToLocalTime().ToString("yyyy-MM-dd");
				kwe.WorkBaseShifts = nShifts;
				kwe.WorkBaseDays = nDays;
				kwe.WorkSequence = sWorkSequence;
				kwe.WorkShift = sWorkShift;
				kwe.WorkIsValid = int.Parse(ddl_WorkIsValid.SelectedValue);
				kwe.WorkNote = txt_WorkNote.Value;
				kwe.WorkCreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");

				kwe.WorkModifyTime = kwe.WorkCreateTime;

				///////////////////////////////////////////////////////////////////////////////

				if (KPI_WorkDal.Update(kwe)) {
					MessageBox.popupClientMessage(this.Page, "修改成功！", "call();");

					BindWork();
				}
				else {
					MessageBox.popupClientMessage(this.Page, "修改错误！", "call();");

				}
			}

		}

		protected void btnAddWork_Click(object sender, EventArgs e) {
			//检查名称是否重复
			string sName = txt_WorkName.Value.Trim();

			if (KPI_WorkDal.WorkNameExists(sName, "")) {
				MessageBox.popupClientMessage(this.Page, "名称已存在，请修改倒班名称!", "call();");

				return;
			}


			int nShifts = int.Parse(ddl_BaseShifts.SelectedValue);
			int nDays = int.Parse(ddl_BaseDays.SelectedValue);

			string sWorkSequence = "";
			//WorkSequence
			for (int j = 0; j < nDays; j++) {
				for (int i = 0; i < nShifts; i++) {
					string id = string.Format("ddl_period{0}{1}", i, j);
					DropDownList ddl = (DropDownList)this.FindControl(id);
					sWorkSequence += ddl.SelectedValue + "-";
				}
			}

			string sWorkShift = "";
			//WorkShift
			for (int i = 0; i < nShifts; i++) {
				string id = string.Format("ddl_shift0{0}", i);
				DropDownList ddl = (DropDownList)this.FindControl(id);

				sWorkShift += ddl.SelectedValue + "-";
			}

			string keyid = PageControl.GetGuid();

			//////////////////////////////////////////////////////////////////////////////
			KPI_WorkEntity kwe = new KPI_WorkEntity();
			kwe.WorkID = keyid;
			kwe.WorkName = txt_WorkName.Value;
			kwe.WorkDesc = txt_WorkDesc.Value;
			kwe.WorkStartTime = txt_date.Value;
			kwe.WorkEndTime = DateTime.Now.AddYears(10).ToLocalTime().ToString("yyyy-MM-dd");
			kwe.WorkBaseShifts = nShifts;
			kwe.WorkBaseDays = nDays;
			kwe.WorkSequence = sWorkSequence;
			kwe.WorkShift = sWorkShift;
			kwe.WorkIsValid = int.Parse(ddl_WorkIsValid.SelectedValue);
			kwe.WorkNote = txt_WorkNote.Value;
			kwe.WorkCreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");

			kwe.WorkModifyTime = kwe.WorkCreateTime;

			///////////////////////////////////////////////////////////////////////////////
			if (KPI_WorkDal.Insert(kwe)) {
				MessageBox.popupClientMessage(this.Page, "添加成功！", "call();");

				BindWork();
			}
			else {
				MessageBox.popupClientMessage(this.Page, "添加错误！", "call();");

			}

			if (KPI_WorkDal.Update(kwe)) {
				MessageBox.popupClientMessage(this.Page, "修改成功！", "call();");

				BindWork();
			}
			else {
				MessageBox.popupClientMessage(this.Page, "修改错误！", "call();");

			}

		}


		///// <summary>
		///// 取消按钮
		///// </summary>
		///// <param name="sender"></param>
		///// <param name="e"></param>
		//protected void btnSearch_Click(object sender, EventArgs e)
		//{
		//    string ParamID = ddl_ParamID.Value.ToString();

		//    string UnitID = ViewState["keyid"].ToString();

		//    if (UnitID != "" && ParamID !="")
		//    {
		//        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "",
		//                "window.open('Seek_SubLine.aspx?unitid=" + UnitID + "&paramid=" + ParamID + "','newwindow','width=700,height=500')", true);

		//                    //Response.Redirect("Seek_UnitSeqTagConfig.aspx?type=1&keyid=" + Request.QueryString["keyid"].ToString());
		//    }
		//    else
		//    {

		//        //Response.Redirect("Seek_UnitSeqTagConfig.aspx?type=2");
		//    }        


		//}


		#endregion



	}
}
