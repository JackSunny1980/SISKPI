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

namespace SISKPI
{
    /// <summary>
    /// 查询、显示、添加、编辑
    /// 设备信息；
    /// 指标考核基本配置；
    /// </summary>
    public partial class KPI_SATagConfig : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //机组信息
                DataTable dt = KPI_UnitDal.GetUnits("");
                ddl_UnitID.Items.Add(new ListItem("所有机组集", "ALL"));
                foreach (DataRow dr in dt.Rows)
                {
                    ddl_UnitID.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
                }

                //设备信息
                dt = KPI_SeqDal.GetSeqs();
                ddl_SeqID.Items.Add(new ListItem("所有设备集", "ALL"));
                foreach (DataRow dr in dt.Rows)
                {
                    ddl_SeqID.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
                }

                //指标信息
                dt = KpiDal.GetKpis();
                ddl_KpiID.Items.Add(new ListItem("所有指标集", "ALL"));
                foreach (DataRow dr in dt.Rows)
                {
                    ddl_KpiID.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
                }

                //周期集信息
                //dt = KPI_CycleDal.GetCycles();
                //ddl_CycleID.Items.Add(new ListItem("所有周期", "ALL"));
                //foreach (DataRow dr in dt.Rows)
                //{
                //    ddl_CycleID.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
                //}

                //安全指标信息
                dt = KPI_SATagDal.GetSAs();
                ddl_SAID.Items.Add(new ListItem("所有安全指标", "ALL"));
                foreach (DataRow dr in dt.Rows)
                {
                    ddl_SAID.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
                }

                BindSA();
            }
        }

        /// <summary>
        ///  //设备信息
        /// </summary>
        void BindSA()
        {
            //查询条件
            string UnitID="";
            string SeqID="";
            string KpiID="";
            string SAID="";

            //机组信息
            if (ddl_UnitID.Value != "ALL")
            {
                UnitID = ddl_UnitID.Value;
            }
            
            //设备信息
            if (ddl_SeqID.Value != "ALL")
            {
                SeqID = ddl_SeqID.Value;
            }

            //指标信息
            if (ddl_KpiID.Value != "ALL")
            {
                KpiID = ddl_KpiID.Value;
            }

            //周期集信息
            //if (ddl_CycleID.Value != "ALL")
            //{
            //    CycleID = ddl_CycleID.Value;
            //}

            //安全指标信息
            if (ddl_SAID.Value != "ALL")
            {
                SAID = ddl_SAID.Value;
            }
            

            //设备信息
            DataTable dtTags = KPI_SATagDal.GetSearchList(UnitID, SeqID, KpiID, SAID);

            gvSA.DataSource = dtTags;
            gvSA.DataBind();
        }

        #region 安全指标信息

        protected void gvSA_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((LinkButton)e.Row.FindControl("delete") != null)
                {
                    LinkButton lb = (LinkButton)e.Row.FindControl("delete");
                    lb.Attributes.Add("onclick", "javascript:return confirm('确定删除吗？');");
                }

                DataRowView drv = (DataRowView)e.Row.DataItem;

                if ((DropDownList)e.Row.FindControl("ddlCycle") != null)
                {
                    DropDownList ddlCollege = (DropDownList)e.Row.FindControl("ddlCycle");

                    ddlCollege.SelectedValue = drv["CycleName"].ToString();
                }

                if ((DropDownList)e.Row.FindControl("ddlWeb") != null)
                {
                    DropDownList ddlCollege = (DropDownList)e.Row.FindControl("ddlWeb");

                    ddlCollege.SelectedValue = drv["SAWeb"].ToString();
                }


                if ((DropDownList)e.Row.FindControl("ddlValid") != null)
                {
                    DropDownList ddlCollege = (DropDownList)e.Row.FindControl("ddlValid");

                    ddlCollege.SelectedValue = drv["SAIsValid"].ToString();
                }

                if ((DropDownList)e.Row.FindControl("ddlCalc") != null)
                {
                    DropDownList ddlCollege = (DropDownList)e.Row.FindControl("ddlCalc");

                    ddlCollege.SelectedValue = drv["SAIsCalc"].ToString();
                }

                if ((DropDownList)e.Row.FindControl("ddlDisplay") != null)
                {
                    DropDownList ddlCollege = (DropDownList)e.Row.FindControl("ddlDisplay");

                    ddlCollege.SelectedValue = drv["SAIsDisplay"].ToString();
                }


                if ((DropDownList)e.Row.FindControl("ddlTotal") != null)
                {
                    DropDownList ddlCollege = (DropDownList)e.Row.FindControl("ddlTotal");

                    ddlCollege.SelectedValue = drv["SAIsTotal"].ToString();
                }


                //鼠标移到效果
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");
            }

        }

        protected void gvSA_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string keyid = e.CommandArgument.ToString();

            if (e.CommandName == "dataDelete")
            {
                if (KPI_SATagDal.DeleteTag(keyid))
                {
                    MessageBox.popupClientMessage(this.Page, "删除成功！", "call();");

                    BindSA();
                }
                else
                {
                    MessageBox.popupClientMessage(this.Page, "删除错误！", "call();");
                }
            }
            else if (e.CommandName == "dataView")
            {
                Response.Redirect("KPI_SubSATagConfig.aspx?said=" + keyid);
            }
            else if (e.CommandName == "dataCopy")
            {
                //Response.Redirect("KPI_SubSATagConfig1.aspx?ecid=" + keyid);

                //string newid = PageControl.GetGuid();

                if (KPI_SATagDal.CopySATag(keyid))
                {
                    MessageBox.popupClientMessage(this.Page, "复制成功，请修改相关配置！", "call();");

                    BindSA();
                }
                else
                {
                    MessageBox.popupClientMessage(this.Page, "复制错误！", "call();");
                }

            }

        }

        protected void gvSA_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvSA.EditIndex = e.NewEditIndex;

            BindSA();
        }

        protected void gvSA_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvSA.EditIndex = -1;

            BindSA();
        }

        #endregion

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            BindSA();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("KPI_SubSATagConfig.aspx");
        }

        protected void btnSort_Click(object sender, EventArgs e)
        {

            //string strjs = "<script language=javascript>window.open('KPI_SubSATagSort.aspx','newwindow')</script>";

            //ClientScript.RegisterStartupScript(this.GetType(), "", strjs);
            Response.Redirect("KPI_SubSATagSort.aspx");
        }

    }
}
