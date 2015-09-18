using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Drawing;

using SIS.DataAccess;
using SIS.Assistant;
using SIS.Loger;
using SIS.DBControl;

namespace SISKPI
{
    public partial class Race_Tag : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //
                btnSearch.Attributes.Add("onclick", "setDivPos('Lay1');Lay1.style.visibility='';progress_update();");

                //绑定机组序号
                DataTable dt = KPI_UnitDal.GetUnits("");
                ddlUnitInfor.Items.Add(new ListItem("所有机组", "ALL"));
                foreach (DataRow dr in dt.Rows)
                {
                    ddlUnitInfor.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
                }

                ////
                //ddlMainIsValid.Items.Add(new ListItem("全部", "ALL"));
                //ddlMainIsValid.Items.Add(new ListItem("无效", "0"));
                //ddlMainIsValid.Items.Add(new ListItem("有效", "1"));

                //判断是否新建或编辑
                if (Request.QueryString["unitid"] != null)
                {
                    ViewState["unitid"] = Request.QueryString["unitid"].ToString();

                    //ddlUnitInfor.Value = ViewState["unitid"].ToString();
                    
                    //ddlUnitInfor.Disabled = true;

                }
                else
                {
                    ViewState["unitid"] = "";

                    //btnReturn.Visible = false;
                }

                BindGrid();

                //gvTagAlarm.Visible = false;
            }
        }

        /// <summary>
        /// 根据条件绑定查询结果
        /// </summary>
        void BindGrid()
        {
            //txt_MainOutTagName.Text =txt_MainOutTagName.Text.Trim();
            //txt_InTagName.Text=txt_InTagName.Text.Trim();
            //txt_MainOutTagDesc.Text=txt_MainOutTagDesc.Text.Trim();
            //txt_InTagDesc.Text=txt_InTagDesc.Text.Trim();
            txt_TagName.Text = txt_TagName.Text.Trim();
            txt_TagDesc.Text = txt_TagDesc.Text.Trim();
            
            string condition = "";
            if (ddlUnitInfor.Value != "ALL")
            {
                condition += " and a.UnitID = '" + ddlUnitInfor.Value + "'";
            }

            //if (rblTagType.SelectedValue != "")
            //{
            //    condition += " and TagType = '" + rblTagType.SelectedValue + "'";
            //}

            if (txt_TagName.Text != "")
            {
                condition += " and TagName like '%" + txt_TagName.Text + "%'";
            }
            if (txt_TagDesc.Text != "")
            {
                condition += " and TagDesc like '%" + txt_TagDesc.Text + "%'";
            }

            //if (txt_MainOutTagName.Text != "")
            //{
            //    condition += " and MainOutTagName like '%" + txt_MainOutTagName.Text + "%'";
            //}
            //if (txt_MainOutTagDesc.Text != "")
            //{
            //    condition += " and MainOutTagDesc like '%" + txt_MainOutTagDesc.Text + "%'";
            //}

            //if (ddlUnitInfor.Value != "ALL")
            //{
            //    condition += " and a.UnitID = '" + ddlUnitInfor.Value + "'";
            //}

            //if (ddlMainIsValid.Value != "ALL")
            //{
            //    condition += " and MainIsValid = '" + ddlMainIsValid.Value + "'";
            //}

            //string temp = "";
            //if (txt_InTagName.Text != "")
            //{
            //    temp = " and SubTagName like '%" + txt_InTagName.Text + "%'";

            //}
            //if (txt_InTagDesc.Text != "")
            //{
            //    temp += " and SubTagDesc like '%" + txt_InTagDesc.Text + "%'";
            //}
            //if (temp != "")
            //{
            //    condition += " and MainID in (select MainID from Filter_TagSub where 1=1 " + temp + ")";
            //}

            gvTag.DataSource = Race_TagDal.GetSearchList3(condition);

            gvTag.DataBind();
        }

        /// <summary>
        /// 查询按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //gvTagAlarm.Visible = false;
            gvTag.Visible = true;
            gvTag.PageIndex = 0;

            BindGrid();           
        }


        /// <summary>
        /// 删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTag_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string keyid = e.CommandArgument.ToString();

            if (e.CommandName == "dataDelete")
            {
                if (Race_TagDal.Delete(keyid))
                {
                    MessageBox.popupClientMessage(this.Page, "删除成功！", "call();");

                    BindGrid();
                    return;
                }
                else
                {
                    MessageBox.popupClientMessage(this.Page, "删除错误！", "call();");
                    return;
                }
            }
        }


        /// <summary>
        /// 列表结果绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTag_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //=================================================================================
                //开关量不显示报警及数据分析
                //string MainOutTagName = e.Row.Cells[3].Text ;
                HtmlInputHidden key = (HtmlInputHidden)e.Row.Cells[0].FindControl("tagid");

                
                if ((LinkButton)e.Row.Cells[12].FindControl("lb_delete") != null)
                {
                    LinkButton lb = (LinkButton)e.Row.Cells[12].FindControl("lb_delete");
                    lb.Attributes.Add("onclick", "javascript:return confirm('确定删除吗？');");
                }

                //鼠标移到效果
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");

            }
        }

        void setColor(GridViewRowEventArgs e, int from, int to, Color c)
        {
            for (int i = from; i <= to; i++)
            {
                //e.Row.Cells[i].Font.Bold = true;
                //Color.YellowGreen
                //e.Row.Cells[i].ForeColor = c;//Color.Black;
                e.Row.BackColor = c;
            }
        } 

              
        /// <summary>
        /// 分页事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTag_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTag.PageIndex = e.NewPageIndex;

            BindGrid();
        }

        protected void btnBatchPro_Click(object sender, EventArgs e)
        {
            Response.Redirect("Race_SubTagBatch.aspx");
        }

        protected void btnSort_Click(object sender, EventArgs e)
        {

            string strjs = "<script language=javascript>window.open('Race_SubTagSort.aspx','newwindow','width=600,height=600')</script>";

            ClientScript.RegisterStartupScript(this.GetType(), "", strjs);
        }

                
    }
}
