using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SISKPI
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void IBnt_Login_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("sismain.aspx");
        }
    }
}