using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIS.DataAccess;
using SIS.DataEntity;
using System.Data;

namespace SISKPI.KPI {

	public partial class SARealTagConfigPage : System.Web.UI.Page {

		#region 重新方法

		protected override void OnLoad(EventArgs e) {
			base.OnLoad(e);
			if (!IsPostBack) {
				ClientInitial();
				DataBind();
			}
		}

		public override void DataBind() {
			using (KPI_SATagDal SATagDal = new KPI_SATagDal()) {
				SARepeater.DataSource = SATagDal.GetSATagList(drpUnits.SelectedValue);
			}					
			base.DataBind();		
		}

		#endregion

		#region 事件

		protected void btnSearch_Click(object sender, EventArgs e) {
			DataBind();
			lblMsg.Text = "安全指标对应的测点";
		}

		protected void ItemCommand(Object sender, RepeaterCommandEventArgs e) {
			Literal lblSAID,lblSAName;
			lblSAID  = (Literal)e.Item.FindControl("lblSAID");
			lblSAName = (Literal)e.Item.FindControl("lblSAName");
			lblMsg.Text = String.Format("安全指标【{0}】对应的测点如下：", lblSAName.Text);
			BindRealTag(lblSAID.Text);
		}

		#endregion

		#region 私有方法

		private void ClientInitial() {
            DataTable Plants = KPI_PlantDal.GetDataTable();
            drpPlants.DataSource = Plants;
            drpPlants.DataValueField = "PlantID";
            drpPlants.DataTextField = "PlantName";
            drpPlants.DataBind();
            if (Plants.Rows.Count > 0) {
                String PlantID = Plants.Rows[0]["PlantID"] + "";
                BindUnits(PlantID);
            }
		}

		private void BindRealTag(String SAID) {
			using (SATagMapDataAccess DataAccess = new SATagMapDataAccess()) {
				TagRepeater.DataSource = DataAccess.GetSATagMaps(SAID);
				TagRepeater.DataBind();
			}
		}

        private void BindUnits(String PlantID) {
            var UnitList = KPI_UnitDal.GetUnits(PlantID);
            drpUnits.DataSource = UnitList;
            drpUnits.DataTextField = "Name";
            drpUnits.DataValueField = "ID";
            drpUnits.DataBind();
            //drpUnits.DataSource = UnitList;
            //drpUnits.DataValueField = "ID";
            //drpUnits.DataTextField = "Name";
            //drpUnits.DataBind();
        }

		#endregion

        protected void drpPlants_SelectedIndexChanged(object sender, EventArgs e) {
            BindUnits(drpPlants.SelectedValue);
        }

	}
}