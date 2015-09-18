using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIS.DataEntity;
using SIS.DataAccess;


namespace SISKPI.KPISYS {

    /// <summary>
    /// 机组运行奖金设置
    /// </summary>
	public partial class SysParameterPage : System.Web.UI.Page {

		#region 重写方法

		protected override void OnLoad(EventArgs e) {
			base.OnLoad(e);
			if (!IsPostBack) {
				DataBind();
			}
		}

		public override void DataBind() {
			Repeater1.DataSource = GetDataSource();
			base.DataBind();
		}

		#endregion

		#region 私有方法

		private void ShowMessage(string message) {
			ScriptManager.RegisterClientScriptBlock(this, GetType(), "Msg", "alert('" + message + "')", true);
		}

		private List<SystemParameterEntity> GetDataSource() {
			KPI_SystemDal DataAccess = new KPI_SystemDal();
			List<SystemParameterEntity> List = new List<SystemParameterEntity>();
			SystemParameterEntity SystemParameter,Param;
			List<KPI_UnitEntity> UnitList = KPI_UnitDal.GetAllEntity();
			foreach (KPI_UnitEntity Unit in UnitList) {
				SystemParameter = DataAccess.GetSystemParameter(Unit.UnitID);				
				if (SystemParameter == null) {
					SystemParameter = new SystemParameterEntity();					
					SystemParameter.SysName = Unit.UnitID;
					SystemParameter.SysDesc = Unit.UnitName + "奖金金额";					
				}
				SystemParameter.SysCode = Unit.UnitCode;
				Param = DataAccess.GetSystemParameter(Unit.UnitCode);
				if (Param != null) SystemParameter.SysValue2 = Param.SysValue;
				//SystemParameter.SysValue2 = DataAccess.GetSystemParameter(Unit.UnitCode).SysValue;	
				List.Add(SystemParameter);
			}
			return List;
		}



		#endregion

		#region 事件

		protected void btnSave_Click(Object sender, EventArgs e) {
			Literal lblSysName, lblSysDesc, lblSysCode;
			TextBox txtSysValue, txtSysNote, txtSysValue2;
			KPI_SystemDal DataAccess = new KPI_SystemDal();
			SystemParameterEntity SystemParameter;
			RepeaterItemCollection Items = Repeater1.Items;
			foreach (RepeaterItem Item in Items) {
				lblSysName = (Literal)Item.FindControl("lblSysName");
				lblSysDesc = (Literal)Item.FindControl("lblSysDesc");
				lblSysCode = (Literal)Item.FindControl("lblSysCode");
				txtSysValue = (TextBox)Item.FindControl("txtSysValue");
				txtSysValue2 = (TextBox)Item.FindControl("txtSysValue2");
				txtSysNote = (TextBox)Item.FindControl("txtSysNote");
				SystemParameter = new SystemParameterEntity();
				SystemParameter.SysEngunit = "元";
				SystemParameter.SysIsValid = 1;
				SystemParameter.SysID = DateTime.Now.Ticks + "";
				SystemParameter.SysName = lblSysName.Text;
				SystemParameter.SysDesc = lblSysDesc.Text;
				SystemParameter.SysValue = txtSysValue.Text;
				SystemParameter.SysNote = txtSysNote.Text;
				DataAccess.SaveSystemParameter(SystemParameter);


				SystemParameter = new SystemParameterEntity();
				SystemParameter.SysEngunit = "";
				SystemParameter.SysIsValid = 1;
				SystemParameter.SysID = lblSysName.Text;
				SystemParameter.SysName = lblSysCode.Text;
				SystemParameter.SysDesc = "奖励名次";
				SystemParameter.SysValue = txtSysValue2.Text;
				SystemParameter.SysNote = "奖励名次";
				DataAccess.SaveSystemParameter(SystemParameter);
			}
			ShowMessage("数据保存成功！");
		}

		#endregion
	}
		
}