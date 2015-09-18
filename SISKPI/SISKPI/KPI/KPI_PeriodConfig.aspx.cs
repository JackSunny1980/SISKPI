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
    public partial class KPI_PeriodConfig : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindPeriod();
            }
        }

        void BindPeriod()
        {
            //班信息
            DataTable dtPeriod = KPI_PeriodDal.GetPeriodList();
            gvPeriod.DataSource = dtPeriod;
            gvPeriod.DataBind();
        }


        #region 班信息配置

        protected void gvPeriod_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //HtmlInputHidden key = (HtmlInputHidden)e.Row.Cells[0].FindControl("periodid");
               
                if ((LinkButton)e.Row.FindControl("delete") != null)
                {
                    LinkButton lb = (LinkButton)e.Row.FindControl("delete");
                    lb.Attributes.Add("onclick", "javascript:return confirm('确定删除吗？');");
                }

                DataRowView drv = (DataRowView)e.Row.DataItem;

                if ((DropDownList)e.Row.FindControl("ddlIDL") != null)
                {
                    DropDownList ddlCollege = (DropDownList)e.Row.FindControl("ddlIDL");

                    ddlCollege.SelectedValue = drv["PeriodIsIDL"].ToString();
                }

                if ((DropDownList)e.Row.FindControl("ddlValid") != null)
                {
                    DropDownList ddlCollege = (DropDownList)e.Row.FindControl("ddlValid");

                    ddlCollege.SelectedValue = drv["PeriodIsValid"].ToString();
                }

                //鼠标移到效果
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");
            
            }

        }

        protected void gvPeriod_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string keyid = e.CommandArgument.ToString();

            if (e.CommandName == "dataDelete")
            {
                if (KPI_PeriodDal.DeletePeriod(keyid))
                {
                    MessageBox.popupClientMessage(this.Page, "删除成功！", "call();");


                    BindPeriod();
                }
                else
                {
                    MessageBox.popupClientMessage(this.Page, "删除错误！", "call();");
                }                

            }
        }

        protected void gvPeriod_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvPeriod.EditIndex = e.NewEditIndex;

            BindPeriod();

        }

        protected void gvPeriod_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvPeriod.EditIndex = -1;

            BindPeriod();

        }

        protected void gvPeriod_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string sID = ((HtmlInputHidden)(gvPeriod.Rows[e.RowIndex].Cells[0].FindControl("periodid"))).Value.ToString().Trim();

            string sName = ((TextBox)(gvPeriod.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim();
            string sDesc = ((TextBox)(gvPeriod.Rows[e.RowIndex].Cells[3].Controls[0])).Text.ToString().Trim();
            string sStart = ((TextBox)(gvPeriod.Rows[e.RowIndex].Cells[4].Controls[0])).Text.ToString().Trim();
            string sEnd = ((TextBox)(gvPeriod.Rows[e.RowIndex].Cells[5].Controls[0])).Text.ToString().Trim();
            //string sLong = ((TextBox)(gvPeriod.Rows[e.RowIndex].Cells[6].Controls[0])).Text.ToString().Trim();
            string sIDL = ((DropDownList)(gvPeriod.Rows[e.RowIndex].Cells[7].FindControl("ddlIDL"))).SelectedValue;
            string sValid = ((DropDownList)(gvPeriod.Rows[e.RowIndex].Cells[8].FindControl("ddlValid"))).SelectedValue;
            string sNote = ((TextBox)(gvPeriod.Rows[e.RowIndex].Cells[9].Controls[0])).Text.ToString().Trim();

            if (sName =="")
            {
                MessageBox.popupClientMessage(this.Page, "名称不能为空！", "call();");
                return;
            }

            //名称是否重复
            if (KPI_PeriodDal.PeriodNameExists(sName, sID))
            {
                MessageBox.popupClientMessage(this.Page, "已存在相同的名称!");
                return;
            }

            //更新
            KPI_PeriodEntity ote = new KPI_PeriodEntity();
            ote.PeriodID = sID;
            ote.PeriodName = sName;
            ote.PeriodDesc = sDesc;
            ote.PeriodStartHour = decimal.Parse(sStart);
			ote.PeriodEndHour = decimal.Parse(sEnd);
			ote.PeriodHours = sIDL == "1" ? (decimal.Parse(sEnd) + 24 - decimal.Parse(sStart)) : (decimal.Parse(sEnd) - decimal.Parse(sStart));
            ote.PeriodIsIDL = sIDL;
            ote.PeriodIsValid = sValid;
            ote.PeriodNote = sNote;
            ote.PeriodModifyTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");

            if (KPI_PeriodDal.Update(ote))
            {
                MessageBox.popupClientMessage(this.Page, "编辑成功！", "call();");

            }
            else
            {
                MessageBox.popupClientMessage(this.Page, "编辑错误！", "call();");
            }

            gvPeriod.EditIndex = -1;

            BindPeriod();

        }


        protected void btnAddPeriod_Click(object sender, EventArgs e)
        {
            //集团信息，新增页面
            //string strjs = "<script language=javascript>window.open('KPI_SubRunPeriod.aspx','newwindow','width=500,height=400')</script>";

            //ClientScript.RegisterStartupScript(this.GetType(), "", strjs);

            int index = KPI_PeriodDal.PeriodIDCounts();

            string sID = PageControl.GetGuid();

            KPI_PeriodEntity ote = new KPI_PeriodEntity();
            ote.PeriodID = sID;
            ote.PeriodCode = KPI_PeriodDal.GetNextCode();
            ote.PeriodName = "InputName";
            ote.PeriodDesc = "";
            ote.PeriodStartHour = 8;
            ote.PeriodEndHour = 17;
            ote.PeriodHours = 9;
            ote.PeriodIsIDL = "0";
            ote.PeriodIsValid = "1";
            ote.PeriodNote = "";

            ote.PeriodCreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            ote.PeriodModifyTime = ote.PeriodCreateTime;

            if (KPI_PeriodDal.Insert(ote))
            {
                //MessageBox.popupClientMessage(this.Page, "添加成功！", "call();");

                gvPeriod.EditIndex = index;

                BindPeriod();
            }
            else
            {
                MessageBox.popupClientMessage(this.Page, "添加错误！", "call();");

            }



        }
        

        #endregion

    }
}
