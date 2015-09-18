using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RdlcReportBasic.KpiTagMonthReport
{
    public partial class KpiEcTagMonthMultiselect : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (RdlcReportBasic.KpiTagMonthReport.KpiEcTagMonthReport.dtKPI != null)
                {
                    cbxKPIName.Items.Clear();

                    cbxKPIName.DataSource = RdlcReportBasic.KpiTagMonthReport.KpiEcTagMonthReport.dtKPI;
                    cbxKPIName.DataValueField = "tagdefid";
                    cbxKPIName.DataTextField = "DESCRIPTION";

                    cbxKPIName.DataBind();

                    for (int b = 0; b < cbxKPIName.Items.Count; b++)
                    {
                        this.cbxKPIName.Items[b].Selected =
                            RdlcReportBasic.KpiTagMonthReport.KpiEcTagMonthReport.dtKPI.Rows[b]["tagselected"].ToString() == "1" ? true : false;
                    }
                }
            }

        }
  

        protected void btnApply_Click(object sender, EventArgs e)
        {           
            for (int b = 0; b < cbxKPIName.Items.Count; b++)
            {
                if (this.cbxKPIName.Items[b].Selected == true)
                {
                    RdlcReportBasic.KpiTagMonthReport.KpiEcTagMonthReport.dtKPI.Rows[b]["tagselected"] = 1;
                }
                else
                {
                    RdlcReportBasic.KpiTagMonthReport.KpiEcTagMonthReport.dtKPI.Rows[b]["tagselected"] = 0;                

                }
            }

            this.Response.Write("<script language=javascript>window.close();</script>");
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            //window.opener=null;
            this.Response.Write("<script language=javascript>window.close();</script>");
        }

        protected void btnAll_Click(object sender, EventArgs e)
        {
            for (int b = 0; b < cbxKPIName.Items.Count; b++)
            {
                this.cbxKPIName.Items[b].Selected = true;
            }

        }

        protected void btnNot_Click(object sender, EventArgs e)
        {
            for (int b = 0; b < cbxKPIName.Items.Count; b++)
            {
                this.cbxKPIName.Items[b].Selected = false;
            }
        }

        protected void btnFei_Click(object sender, EventArgs e)
        {
            for (int b = 0; b < cbxKPIName.Items.Count; b++)
            {
                this.cbxKPIName.Items[b].Selected = !this.cbxKPIName.Items[b].Selected;
            }
        }

       

    }
}