using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIS.DataEntity;
using System.Data;
using SIS.DataAccess;


namespace SISKPI.KPI {

	public partial class RealTagDialog : System.Web.UI.Page {

		#region 属性

		private String UnitID {
			get {
				return Request.Params["UnitID"];
			}
		}

		private String SAID {
			get {
				return Request.Params["SAID"];
			}
		}

		#endregion


		#region 重新方法

		protected override void OnLoad(EventArgs e) {
			base.OnLoad(e);
			if (!IsPostBack) {
                //btnSave.Enabled = true;
				DataBind();
			}
		}

		public override void DataBind() {
			TagRepeater.DataSource = KPI_RealTagDal.GetRealTags(UnitID);
			base.DataBind();
			Literal lblRealID;
			CheckBox chkSelected;
			using (SATagMapDataAccess DataAccess = new SATagMapDataAccess()) {
				List<String> TagIDList = DataAccess.GetSATagMaps(SAID).Select(p => p.RealID).ToList<String>();
				RepeaterItemCollection Items = TagRepeater.Items;
				foreach (RepeaterItem Item in Items) {
					lblRealID = (Literal)Item.FindControl("lblRealID");
					chkSelected = (CheckBox)Item.FindControl("chkSelected");
					chkSelected.Checked = TagIDList.Contains(lblRealID.Text);
				}
			}
		}

		#endregion

		#region 事件
		protected void btnSave_Click(object sender, EventArgs e) {
			CheckBox chkSelected;
			Literal lblRealID;
			SATagMapEntity SATagMap;

			using (SATagMapDataAccess DataAccess = new SATagMapDataAccess()) {
				DataAccess.DeleteSATagMap(SAID);
				RepeaterItemCollection Items = TagRepeater.Items;
				foreach (RepeaterItem Item in Items) {
					lblRealID = (Literal)Item.FindControl("lblRealID");
					chkSelected = (CheckBox)Item.FindControl("chkSelected");
					if (chkSelected.Checked) {
						SATagMap = new SATagMapEntity {
							SAID = SAID,
							RealID = lblRealID.Text
						};
						DataAccess.SaveSATagMap(SATagMap);
					}
				}
			}
			ScriptManager.RegisterClientScriptBlock(this, GetType(), "Msg", "alert('数据保存成功！')", true);
		}

		#endregion
	}
}