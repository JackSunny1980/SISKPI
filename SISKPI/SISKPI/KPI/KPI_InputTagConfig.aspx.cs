using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using SIS.Assistant;
using SIS.Loger;
using System.Web.UI.HtmlControls;
using SIS.DBControl;
using SIS.DataAccess;
using SIS.DataEntity;

namespace SISKPI {
    public partial class KPI_InputTagConfig : System.Web.UI.Page {

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            if (!IsPostBack) {
                ClientInitial();
                DataBind();
            }
        }

        public override void DataBind() {
            KPI_InputTagDal DataAccess = new KPI_InputTagDal();
            int RecordCount = 0;
            String TagCategory = drpTagCategorys.SelectedValue;
            Tags.DataSource = DataAccess.GetInputTags(Pager.CurrentPageIndex,Pager.PageSize,
                TagCategory,out RecordCount);
            Pager.RecordCount = RecordCount;
            base.DataBind();
        }

        protected void Pager_PageChanged(Object sender, EventArgs e) {
            DataBind();
        }

        protected void btnSearch_Click(Object sender, EventArgs e) {
            DataBind();
        }

        private void ClientInitial() {           
            drpInputType.DataSource = KPI_ConstantDal.GetManagementTags();
            drpInputType.DataValueField = "ConstantValue";
            drpInputType.DataTextField = "ConstantName";
            drpInputType.DataBind();

            drpTagCategorys.DataSource = drpInputType.DataSource;
            drpTagCategorys.DataValueField = "ConstantValue";
            drpTagCategorys.DataTextField = "ConstantName";
            drpTagCategorys.DataBind();
        }
    }


}
