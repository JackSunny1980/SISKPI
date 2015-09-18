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
    public partial class KPI_ECTagConfig : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnDelete.Attributes.Add("onclick", "return confirm('确认删除吗?');");

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

                //实时指标信息
                dt = ECTagDal.GetECs();
                ddl_ECID.Items.Add(new ListItem("所有指标", "ALL"));
                foreach (DataRow dr in dt.Rows)
                {
                    ddl_ECID.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
                }

                BindEC();
            }
        }

        /// <summary>
        ///  //设备信息
        /// </summary>
        void BindEC()
        {
            //查询条件
            string UnitID="";
            string SeqID="";
            string KpiID="";
            string ECID="";

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

            //实时指标信息
            if (ddl_ECID.Value != "ALL")
            {
                ECID = ddl_ECID.Value;
            }
            

            //设备信息
            DataTable dtTags = ECTagDal.GetSearchList(UnitID, SeqID, KpiID, ECID);

            gvEC.DataSource = dtTags;
            gvEC.DataBind();
        }

        #region 实时指标信息

        protected void gvEC_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((LinkButton)e.Row.FindControl("delete") != null)
                {
                    LinkButton lb = (LinkButton)e.Row.FindControl("delete");
                    lb.Attributes.Add("onclick", "javascript:return confirm('确定删除吗？');");
                }

                DataRowView drv = (DataRowView)e.Row.DataItem;

                if ((DropDownList)e.Row.FindControl("ddlWeb") != null)
                {
                    DropDownList ddlCollege = (DropDownList)e.Row.FindControl("ddlWeb");

                    ddlCollege.SelectedValue = drv["ECWeb"].ToString();
                }


                if ((DropDownList)e.Row.FindControl("ddlValid") != null)
                {
                    DropDownList ddlCollege = (DropDownList)e.Row.FindControl("ddlValid");

                    ddlCollege.SelectedValue = drv["ECIsValid"].ToString();
                }

                if ((DropDownList)e.Row.FindControl("ddlCalc") != null)
                {
                    DropDownList ddlCollege = (DropDownList)e.Row.FindControl("ddlCalc");

                    ddlCollege.SelectedValue = drv["ECIsCalc"].ToString();
                }
                
                if ((DropDownList)e.Row.FindControl("ddlAsses") != null)
                {
                    DropDownList ddlCollege = (DropDownList)e.Row.FindControl("ddlAsses");

                    ddlCollege.SelectedValue = drv["ECIsAsses"].ToString();
                }
                
                if ((DropDownList)e.Row.FindControl("ddlZero") != null)
                {
                    DropDownList ddlCollege = (DropDownList)e.Row.FindControl("ddlZero");

                    ddlCollege.SelectedValue = drv["ECIsZero"].ToString();
                }


                if ((DropDownList)e.Row.FindControl("ddlDisplay") != null)
                {
                    DropDownList ddlCollege = (DropDownList)e.Row.FindControl("ddlDisplay");

                    ddlCollege.SelectedValue = drv["ECIsDisplay"].ToString();
                }


                if ((DropDownList)e.Row.FindControl("ddlTotal") != null)
                {
                    DropDownList ddlCollege = (DropDownList)e.Row.FindControl("ddlTotal");

                    ddlCollege.SelectedValue = drv["ECIsTotal"].ToString();
                }


                //鼠标移到效果
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");
            }

        }

        protected void gvEC_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string keyid = e.CommandArgument.ToString();

            if (e.CommandName == "dataDelete")
            {
                if (ECTagDal.DeleteTag(keyid))
                {
                    MessageBox.popupClientMessage(this.Page, "删除成功！", "call();");

                    BindEC();
                }
                else
                {
                    MessageBox.popupClientMessage(this.Page, "删除错误！", "call();");
                }
            }
            else if (e.CommandName == "dataView")
            {
                Response.Redirect("KPI_SubECTagConfig1.aspx?ecid=" + keyid);
            }
            else if (e.CommandName == "dataCopy")
            {
                //Response.Redirect("KPI_SubECTagConfig1.aspx?ecid=" + keyid);

                //string newid = PageControl.GetGuid();

                if (ECTagDal.CopyECTag(keyid))
                {
                    MessageBox.popupClientMessage(this.Page, "复制成功，请修改相关配置！", "call();");

                    BindEC();
                }
                else
                {
                    MessageBox.popupClientMessage(this.Page, "复制错误, Copy_xxx的名称已存在！", "call();");
                }

            }

        }

        protected void gvEC_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvEC.EditIndex = e.NewEditIndex;

            BindEC();
        }

        protected void gvEC_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvEC.EditIndex = -1;

            BindEC();
        }

        
        protected void gvEC_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEC.PageIndex = e.NewPageIndex;

            BindEC();

        }


        #endregion

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            BindEC();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {

            //查询条件
            string UnitID = "";
            string SeqID = "";
            string KpiID = "";
            string ECID = "";

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

            //实时指标信息
            if (ddl_ECID.Value != "ALL")
            {
                ECID = ddl_ECID.Value;
            }


            //设备信息
            if (ECTagDal.DeleteECTagForCondition(UnitID, SeqID, KpiID, ECID))
            {
                MessageBox.popupClientMessage(this.Page, "删除成功", "call();");

                BindEC();
             }

        }


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("KPI_SubECTagConfig1.aspx");
        }

        protected void btnSort_Click(object sender, EventArgs e)
        {

            string strjs = "<script language=javascript>window.open('KPI_SubECTagSort.aspx','newwindow')</script>";

            ClientScript.RegisterStartupScript(this.GetType(), "", strjs);

        }

        protected void btnBatch_Click(object sender, EventArgs e)
        {
            Response.Redirect("KPI_SubECTagBatch.aspx");
        }

        protected void btnCheck_Click(object sender, EventArgs e)
        {
            Response.Redirect("KPI_SubECTagCheck.aspx");

        }

    }
}
