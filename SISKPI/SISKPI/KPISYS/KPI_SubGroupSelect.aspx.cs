using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

using SIS.DataAccess;
using SIS.Assistant;
using SIS.DataEntity;
using SIS.Loger;
using System.Web.UI.HtmlControls;
using SIS.Assistant.WS;
using SIS.DBControl;

namespace SISKPI
{
    public partial class KPI_SubGroupSelect : System.Web.UI.Page
    {
        public static DataTable dtGroups = null;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                string mode = Request.QueryString["mode"].ToString();
                
                ViewState["mode"] = mode;

                //if (mode == "menu")
                //{
                //    dtGroups = SISKPI.KPI_Menu.dtGroups;
                //}
                //else
                if (mode == "user")
                {
                    dtGroups = SISKPI.KPI_SubUserConfig.dtGroups;
                }

                if (dtGroups != null)
                {
                    cbxGroupName.Items.Clear();

                    cbxGroupName.DataSource = dtGroups;
                    cbxGroupName.DataValueField = "GroupCode";
                    cbxGroupName.DataTextField = "GroupName";

                    cbxGroupName.DataBind();

                    for (int b = 0; b < cbxGroupName.Items.Count; b++)
                    {
                        this.cbxGroupName.Items[b].Selected = dtGroups.Rows[b]["GroupSelected"].ToString() == "1" ? true : false;
                    }
                }
            }

        }


        protected void btnApply_Click(object sender, EventArgs e)
        {
            string mode = ViewState["mode"].ToString();
            
            for (int b = 0; b < cbxGroupName.Items.Count; b++)
            {
                if (this.cbxGroupName.Items[b].Selected == true)
                {
                    dtGroups.Rows[b]["GroupSelected"] = "1";
                }
                else
                {
                    dtGroups.Rows[b]["GroupSelected"] = "0";
                }
            }

            //if (mode == "menu")
            //{
            //    SISKPI.KPI_Menu.dtGroups = dtGroups;
            //}
            //else 
            if (mode == "user")
            {
                SISKPI.KPI_SubUserConfig.dtGroups = dtGroups;
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
            for (int b = 0; b < cbxGroupName.Items.Count; b++)
            {
                this.cbxGroupName.Items[b].Selected = true;
            }

        }

        protected void btnNot_Click(object sender, EventArgs e)
        {
            for (int b = 0; b < cbxGroupName.Items.Count; b++)
            {
                this.cbxGroupName.Items[b].Selected = false;
            }
        }

        protected void btnFei_Click(object sender, EventArgs e)
        {
            for (int b = 0; b < cbxGroupName.Items.Count; b++)
            {
                this.cbxGroupName.Items[b].Selected = !this.cbxGroupName.Items[b].Selected;
            }
        }

    }
}
