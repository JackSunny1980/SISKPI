using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

using SIS.Assistant;
using SIS.DBControl;
using SIS.Loger;
using SIS.DataAccess;
using SIS.DataEntity;

namespace SISKPI
{
    /// <summary>
    /// 查询、显示、添加、编辑
    /// 班、值、倒班信息；
    /// </summary>
    public partial class KPI_ShiftConfig : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                BindShift();

            }
        }


        void BindShift()
        {
            //值信息
            DataTable dtShift = KPI_ShiftDal.GetSearchList("");
            gvShift.DataSource = dtShift;
            gvShift.DataBind();

        }

    
        #region 值信息配置
        
        protected void gvShift_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((LinkButton)e.Row.FindControl("delete") != null)
                {
                    LinkButton lb = (LinkButton)e.Row.FindControl("delete");
                    lb.Attributes.Add("onclick", "javascript:return confirm('确定删除吗？');");
                }

                DataRowView drv = (DataRowView)e.Row.DataItem;

                if ((DropDownList)e.Row.FindControl("ddlValid") != null)
                {
                    DropDownList ddlCollege = (DropDownList)e.Row.FindControl("ddlValid");

                    ddlCollege.SelectedValue = drv["ShiftIsValid"].ToString();
                }

                //鼠标移到效果
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");
            }

        }

        protected void gvShift_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string keyid = e.CommandArgument.ToString();

            if (e.CommandName == "dataDelete")
            {
                if (KPI_ShiftDal.DeleteShift(keyid))
                {
                    MessageBox.popupClientMessage(this.Page, "删除成功！", "call();");

                    BindShift();
                }
                else
                {
                    MessageBox.popupClientMessage(this.Page, "删除错误！", "call();");
                }     
            }
        }

        protected void gvShift_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvShift.EditIndex = e.NewEditIndex;

            BindShift();

        }

        protected void gvShift_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvShift.EditIndex = -1;

            BindShift();

        }

        protected void gvShift_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string sID = ((HtmlInputHidden)(gvShift.Rows[e.RowIndex].Cells[0].FindControl("shiftid"))).Value.ToString().Trim();

            string sName = ((TextBox)(gvShift.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim();
            string sDesc = ((TextBox)(gvShift.Rows[e.RowIndex].Cells[3].Controls[0])).Text.ToString().Trim();
            string sValid = ((DropDownList)(gvShift.Rows[e.RowIndex].Cells[4].FindControl("ddlValid"))).SelectedValue;
            string sNote = ((TextBox)(gvShift.Rows[e.RowIndex].Cells[5].Controls[0])).Text.ToString().Trim();

            if (sName == "")
            {
                MessageBox.popupClientMessage(this.Page, "名称不能为空！", "call();");
                return;
            }

            //名称是否重复
            if (KPI_ShiftDal.ShiftNameExists(sName, sID))
            {
                MessageBox.popupClientMessage(this.Page, "已存在相同的名称!");
                return;
            }

            //更新
            KPI_ShiftEntity ote = new KPI_ShiftEntity();
            ote.ShiftID = sID;
            //
            ote.ShiftName = sName;
            ote.ShiftDesc = sDesc;
            ote.ShiftIsValid = sValid;
            ote.ShiftNote = sNote;

            ote.ShiftModifyTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");

            if (KPI_ShiftDal.Update(ote))
            {
                MessageBox.popupClientMessage(this.Page, "编辑成功！", "call();");

            }
            else
            {
                MessageBox.popupClientMessage(this.Page, "编辑错误！", "call();");
            }

            gvShift.EditIndex = -1;
            
            BindShift();
        }


        protected void btnAddShift_Click(object sender, EventArgs e)
        {
            ////添加新电厂信息

            //string strjs = "<script language=javascript>window.open('KPI_SubRunShift.aspx','newwindow','width=500,height=400')</script>";

            //ClientScript.RegisterStartupScript(this.GetType(), "", strjs);

            int index = KPI_ShiftDal.ShiftIDCounts();

            string sID = PageControl.GetGuid();

            KPI_ShiftEntity ote = new KPI_ShiftEntity();
            ote.ShiftID = sID;
            ote.ShiftCode = KPI_ShiftDal.GetNextCode();
            ote.ShiftName = "InputName";
            ote.ShiftDesc = "";
            ote.ShiftIsValid = "1";
            ote.ShiftNote = "";

            ote.ShiftCreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            ote.ShiftModifyTime = ote.ShiftCreateTime;

            if (KPI_ShiftDal.Insert(ote))
            {
                //MessageBox.popupClientMessage(this.Page, "添加成功！", "call();");

                gvShift.EditIndex = index;

                BindShift();
            }
            else
            {
                MessageBox.popupClientMessage(this.Page, "添加错误！", "call();");

            }
        }

        #endregion

    }
}
