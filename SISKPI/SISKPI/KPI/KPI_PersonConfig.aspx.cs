using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

using SIS.DataAccess;
using SIS.DataEntity;
using SIS.DBControl;
using SIS.Loger;
using SIS.Assistant;

namespace SISKPI {
	/// <summary>
    /// 运行人员信息配置
	/// 查询、显示、添加、编辑
	/// 指标信息；
	/// 指标考核基本配置；
	/// </summary>
	public partial class KPI_PersonConfig : System.Web.UI.Page {

		#region 属性

		private string PersonID {
			get {
				if (ViewState["PersonID"] == null) return string.Empty;
				return (string)ViewState["PersonID"];
			}
			set {
				ViewState["PersonID"] = value;
			}
		}

		#endregion

		#region 重写方法

		protected override void OnLoad(EventArgs e) {
			base.OnLoad(e);
			if (!IsPostBack) {
				ClientInitial();
				DataBind();
			}
		}

		public override void DataBind() {
			using (KPI_PersonDal DataAccess = new KPI_PersonDal()) {
                int RecordCount = 0;
				PersonRepeater.DataSource = DataAccess.GetPersons(drpSSpecialFields.SelectedValue,
                    drpSUnits.SelectedValue, drpSShifts.SelectedValue, 
                    Pager.CurrentPageIndex, Pager.PageSize,out RecordCount);
                Pager.RecordCount = RecordCount;
			}
			base.DataBind();
		}

		protected String IsUsed(object sender) {
			string v = (string)sender;
			if (v == "1") return "启用";
			if (v == "0") return "未启用";
			return "";
		}

		#endregion

		#region 私有成员

		private void ClientInitial() {
			List<KPI_UnitEntity> UnitList = KPI_UnitDal.GetAllEntity();
			//drpUnits.Items.Add(new ListItem("全厂", "00"));
			drpSUnits.Items.Add(new ListItem("【所有机组】", ""));
			foreach (KPI_UnitEntity Unit in UnitList) {
				drpSUnits.Items.Add(new ListItem(Unit.UnitName, Unit.UnitID));
				drpUnits.Items.Add(new ListItem(Unit.UnitName, Unit.UnitID));
			}

			DataTable Shifts = KPI_ShiftDal.GetShifts();
			drpSShifts.Items.Add(new ListItem("【所有值次】", ""));
			foreach (DataRow dr in Shifts.Rows) {
				drpShifts.Items.Add(new ListItem(dr["Name"] + "值", dr["Name"] + ""));
				drpSShifts.Items.Add(new ListItem(dr["Name"] + "值", dr["Name"] + ""));
			}
			Shifts.Dispose();

			DataTable PositionTable = KPI_PositionDal.GetDataTable();
			foreach (DataRow dr in PositionTable.Rows) {
				drpPositions.Items.Add(new ListItem(dr["PositionName"] + "", dr["PositionID"] + ""));
			}
			PositionTable.Dispose();
		}

		/// <summary>
		/// 设置界面控件显示状态
		/// </summary>
		/// <param name="Enabled"></param>
		private void SetUIStatus(bool Enabled) {
			ControlCollection Controls = UP2.ContentTemplateContainer.Controls;
			foreach (Control ctrl in Controls) {
				if (ctrl is WebControl) ((WebControl)ctrl).Enabled = Enabled;
			}
		}
		/// <summary>
		/// 设置界面按钮显示状态
		/// </summary>
		/// <param name="State"></param>
		private void SetUIState(string State) {
			if (State == "Default") {
				SetUIStatus(false);
				btnNew.Enabled = true;
				btnEdit.Enabled = true;
				btnDelete.Enabled = true;
				btnSave.Enabled = false;
			}
			if (State == "New") {
				SetUIStatus(true);
				btnNew.Enabled = false;
				btnEdit.Enabled = false;
				btnDelete.Enabled = false;
				btnSave.Enabled = true;
			}

			if (State == "Edit") {
				SetUIStatus(true);
				btnNew.Enabled = false;
				btnEdit.Enabled = false;
				btnDelete.Enabled = false;
				btnSave.Enabled = true;
			}
		}
		/// <summary>
		/// 重置界面
		/// </summary>
		private void ClearPersonUI() {
			PersonID = "";
			txtPersonCode.Text = "";
			txtPersonName.Text ="";
			txtPersonDesc.Text = "";
			chkPersonIsValid.Checked =true;
			txtPersonNote.Text = "";	
		}
		/// <summary>
		/// 填充界面
		/// </summary>
		private void SetPersonUI() {
			using (KPI_PersonDal DataAccess = new KPI_PersonDal()) {
				KPI_PersonEntity Result = DataAccess.GetKPI_Person(PersonID);
				if (Result == null) return;
				drpPositions.SelectedValue = Result.PositionID;				
				txtPersonCode.Text = Result.PersonCode;
				txtPersonName.Text = Result.PersonName;
				txtPersonDesc.Text = Result.PersonDesc;
				chkPersonIsValid.Checked = Result.PersonIsValid=="1";
				txtPersonNote.Text = Result.PersonNote;				
				drpUnits.SelectedValue = Result.UnitID;
				drpShifts.SelectedValue = Result.Shift;
				drpSpecialFields.SelectedValue = Result.SpecialField;
			}			
		}
		
		/// <summary>
		/// 从界面获取对象
		/// </summary>
		/// <returns></returns>
		private KPI_PersonEntity GetPersonUI() {
			KPI_PersonEntity Result = new KPI_PersonEntity();
			Result.PersonID = PersonID;
			Result.PositionID = drpPositions.SelectedValue;
			Result.PersonCode = txtPersonCode.Text;
			Result.PersonName = txtPersonName.Text;
			Result.PersonDesc = txtPersonDesc.Text;
			Result.PersonIsValid = chkPersonIsValid.Checked?"1":"0";
			Result.PersonNote = txtPersonNote.Text;			
			Result.UnitID = drpUnits.SelectedValue;
			Result.Shift = drpShifts.SelectedValue;
			Result.SpecialField = drpSpecialFields.SelectedValue;
			return Result;
		}

		private void ShowMessage(string message) {
			ScriptManager.RegisterClientScriptBlock(this, GetType(), "Information", "alert('" + message + "')", true);
		}

		#endregion

		#region 事件

        protected void Pager_PageChanged(object sender, EventArgs e) {
            DataBind();
        }

		protected void btnSavePerson_Click(object sender, EventArgs e) {
			KPI_PersonEntity Result = GetPersonUI();
			using (KPI_PersonDal DataAccess = new KPI_PersonDal()) {
				int Succeed = DataAccess.SavePerson(Result);
				if (Succeed > 0) ShowMessage("数据保存成功!");
				if (Succeed < 0) ShowMessage("数据保存失败!");
			}
			DataBind();
			SetUIState("Default");
		}

		protected void btnNewPerson_Click(object sender, EventArgs e) {
			ClearPersonUI();
			btnSave.Enabled = true;
			SetUIState("New");
		}

		protected void btnDeletePerson_Click(object sender, EventArgs e) {
			using (KPI_PersonDal DataAccess = new KPI_PersonDal()) {
				int Succeed = DataAccess.DeletePerson(GetPersonUI());
				if (Succeed > 0) ShowMessage("数据删除成功!");
				if (Succeed < 0) ShowMessage("数据删除失败!");
			}
			DataBind();
			SetUIState("Default");
		}
		protected void btnEditPerson_Click(object sender, EventArgs e) {
			SetUIState("Edit");
		}

		protected void btnCancelPerson_Click(object sender, EventArgs e) {
			SetUIState("Default");
		}

		protected void btnSearch_Click(object sender, EventArgs e) {
			DataBind();
		}

		protected void PersonItemCommand(object source, RepeaterCommandEventArgs e) {
			if (e.CommandName.ToLower() == "select") {
				Literal lblPersonID = (Literal)e.Item.FindControl("lblPersonID");
				PersonID = lblPersonID.Text;
				SetPersonUI();
				SetUIState("Default");
			}
		}


		#endregion
	}
}
