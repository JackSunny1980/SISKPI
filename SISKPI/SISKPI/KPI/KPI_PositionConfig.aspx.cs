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
    /// 指标信息；
    /// 指标考核基本配置；
    /// </summary>
    public partial class KPI_PositionConfig : System.Web.UI.Page
    {
        protected static DataSet dsYN = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //初始化
                dsYN = new DataSet();
                dsYN.Tables.Add(new DataTable());
                dsYN.Tables[0].Columns.Add("ID");
                dsYN.Tables[0].Columns.Add("Name");
                dsYN.Tables[0].Rows.Add(new string[] { "1", "是" });
                dsYN.Tables[0].Rows.Add(new string[] { "0", "否" });

                BindPosition();
            }
        }

        /// <summary>
        ///  //设备信息
        /// </summary>
        void BindPosition()
        {
            //设备信息
            DataTable dtPosition = KPI_PositionDal.GetSearchList("");
            gvPosition.DataSource = dtPosition;
            gvPosition.DataBind();

        }

        #region 岗位信息配置

        protected void gvPosition_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //HtmlInputHidden key = (HtmlInputHidden)e.Row.Cells[0].FindControl("positionid");

                if ((LinkButton)e.Row.FindControl("delete") != null)
                {
                    LinkButton lb = (LinkButton)e.Row.FindControl("delete");
                    lb.Attributes.Add("onclick", "javascript:return confirm('确定删除吗？');");
                }
                 
                DataRowView drv = (DataRowView)e.Row.DataItem;

                if ((DropDownList)e.Row.FindControl("ddlHand") != null)
                {
                    DropDownList ddlCollege = (DropDownList)e.Row.FindControl("ddlHand");
                    //ddlCollege.DataSource = dsYN;
                    //ddlCollege.DataTextField = "Name";
                    //ddlCollege.DataValueField = "ID";
                    //ddlCollege.DataBind();
                    
                    ddlCollege.SelectedValue = drv["PositionIsHand"].ToString();
                }

                if ((DropDownList)e.Row.FindControl("ddlShift") != null)
                {
                    DropDownList ddlCollege = (DropDownList)e.Row.FindControl("ddlShift");
                    //ddlCollege.DataSource = dsYN;
                    //ddlCollege.DataTextField = "Name";
                    //ddlCollege.DataValueField = "ID";
                    //ddlCollege.DataBind();

                    ddlCollege.SelectedValue = drv["PositionIsShift"].ToString();
                }


                if ((DropDownList)e.Row.FindControl("ddlValid") != null)
                {
                    DropDownList ddlCollege = (DropDownList)e.Row.FindControl("ddlValid");
                    //ddlCollege.DataSource = dsYN;
                    //ddlCollege.DataTextField = "Name";
                    //ddlCollege.DataValueField = "ID";
                    //ddlCollege.DataBind();

                    ddlCollege.SelectedValue = drv["PositionIsValid"].ToString();
                }

                //鼠标移到效果
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");
            }

        }

        protected void gvPosition_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string keyid = e.CommandArgument.ToString();

            if (e.CommandName == "dataDelete")
            {
                if (KPI_PositionDal.DeletePosition(keyid))
                {
                    MessageBox.popupClientMessage(this.Page, "删除成功！", "call();");

                    BindPosition();
                }
                else
                {
                    MessageBox.popupClientMessage(this.Page, "删除错误！", "call();");
                }

            }
        }

        protected void gvPosition_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPosition.PageIndex = e.NewPageIndex;

            BindPosition();
        }

        protected void gvPosition_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvPosition.EditIndex = e.NewEditIndex;

            BindPosition();
        }

        protected void gvPosition_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvPosition.EditIndex = -1;

            BindPosition();
        }

        protected void gvPosition_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            HtmlInputHidden key = (HtmlInputHidden)gvPosition.Rows[e.RowIndex].Cells[0].FindControl("Positionid");

            string sID = key.Value;
            string sName = ((TextBox)(gvPosition.Rows[e.RowIndex].Cells[1].Controls[0])).Text.ToString().Trim();
            string sDesc = ((TextBox)(gvPosition.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim();
            string sWeight = ((TextBox)(gvPosition.Rows[e.RowIndex].Cells[3].Controls[0])).Text.ToString().Trim();
            //DropDownList需要采用此类方法
            string sHand = ((DropDownList)(gvPosition.Rows[e.RowIndex].Cells[4].FindControl("ddlHand"))).SelectedValue;
            string sShift = ((DropDownList)(gvPosition.Rows[e.RowIndex].Cells[5].FindControl("ddlShift"))).SelectedValue;
            string sValid = ((DropDownList)(gvPosition.Rows[e.RowIndex].Cells[6].FindControl("ddlValid"))).SelectedValue;           
            string sNote = ((TextBox)(gvPosition.Rows[e.RowIndex].Cells[7].Controls[0])).Text.ToString().Trim();

            string msg = "";
            if (sName == "")
            {
                msg += "名称不能为空！\r\n";
            }

            //判断double格式
            if (!Regex.IsMatch(sWeight, @"^\d*[.]?\d*$"))
            {                
                msg += "权重只能为数字组成！\r\n";
            }

            if (msg != "")
            {
                MessageBox.popupClientMessage(this.Page, msg);
                return;
            }

            //代码是否重复
            if (KPI_PositionDal.PositionNameExists(sName, sID))
            {
                MessageBox.popupClientMessage(this.Page, "已存在相同的代码!");
                return;
            }

            //更新
            KPI_PositionEntity ote = new KPI_PositionEntity();
            ote.PositionID = sID;
            ote.PositionName = sName;
            ote.PositionDesc = sDesc;
            ote.PositionWeight = double.Parse(sWeight);
            ote.PositionIsHand = sHand;
            ote.PositionIsShift = sShift;
            ote.PositionIsValid = sValid;
            ote.PositionNote = sNote;
            ote.PositionModifyTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");

            if (KPI_PositionDal.Update(ote))
            {
                MessageBox.popupClientMessage(this.Page, "编辑成功！", "call();");

            }
            else
            {
                MessageBox.popupClientMessage(this.Page, "编辑错误！", "call();");

            }

            gvPosition.EditIndex = -1;

            BindPosition();


        }

        #endregion

        protected void btnAddPosition_Click(object sender, EventArgs e)
        {
            int index = KPI_PositionDal.PositionIDCounts();

            string sID = PageControl.GetGuid();

            KPI_PositionEntity ote = new KPI_PositionEntity();
            ote.PositionID = sID;
            ote.PositionName = "InputName";
            ote.PositionDesc = "";
            ote.PositionWeight = 10;
            ote.PositionIsHand = "1";
            ote.PositionIsShift = "1";
            ote.PositionIsValid = "1";
            ote.PositionNote = "";

            ote.PositionCreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            ote.PositionModifyTime = ote.PositionCreateTime;

            if (KPI_PositionDal.Insert(ote))
            {
                //MessageBox.popupClientMessage(this.Page, "添加成功！", "call();");
            }
            else
            {
                MessageBox.popupClientMessage(this.Page, "添加错误！", "call();");

            }

            gvPosition.EditIndex = index;

            BindPosition();

        }

    }
}
