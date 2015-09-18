using SIS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SISKPI.KPI {

    public partial class KPI_InputTagCategory : System.Web.UI.Page {

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            if (!IsPostBack) {
                DataBind();
            }
        }

        public override void DataBind() {
            TagCategorys.DataSource = KPI_ConstantDal.GetManagementTags();
            base.DataBind();
        }
    }
}