using SIS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SISKPI.KPI {
    public partial class KPI_SubManagementScore : System.Web.UI.Page {
        #region 重写方法

        protected override void OnLoad(EventArgs e) {
            if (!IsPostBack) {
                ClientInitial();
            }
        }
        #endregion

        #region  私有方法

        private void ClientInitial() {
            drpTagCategorys.DataSource = KPI_ConstantDal.GetManagementTags();
            drpTagCategorys.DataValueField = "ConstantValue";
            drpTagCategorys.DataTextField = "ConstantName";
            drpTagCategorys.DataBind();
            txtCheckDate.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
            txtTagScore.Text = "0";
        }

        #endregion

        protected void btnSave_Click(object sender, EventArgs e) {

        }

        protected void drpTagCategorys_SelectedIndexChanged(object sender, EventArgs e) {

        }

        protected void drpTags_SelectedIndexChanged(object sender, EventArgs e) {

        }
    }
}