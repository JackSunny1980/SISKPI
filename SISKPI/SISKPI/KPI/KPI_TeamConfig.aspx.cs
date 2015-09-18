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
    public partial class KPI_TeamConfig : System.Web.UI.Page
    {
        //private static DataSet dsPosition = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                btnClear.Attributes.Add("OnClick", "javascript:return confirm('您确定清空吗？一旦清空将得分错乱！');");

                //dsPosition = KPI_PositionDal.GetPositionsDataSet();

                BindTeam();
            }
        }

        /// <summary>
        ///  //设备信息
        /// </summary>
        void BindTeam()
        {
            //设备信息
            DataTable dtTeam = KPI_TeamDal.GetTeamList();
            gvTeam.DataSource = dtTeam;
            gvTeam.DataBind();

        }


        /// <summary>
        ///  //设备信息
        /// </summary>
        void BindTeamByTime()
        {
            //设备信息
            DataTable dtTeam = KPI_TeamDal.GetTeamListByTime();
            gvTeam.DataSource = dtTeam;
            gvTeam.DataBind();

        }

        #region 岗位信息配置

        protected void gvTeam_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //HtmlInputHidden key = (HtmlInputHidden)e.Row.Cells[0].FindControl("personid");

                if ((LinkButton)e.Row.FindControl("delete") != null)
                {
                    LinkButton lb = (LinkButton)e.Row.FindControl("delete");
                    lb.Attributes.Add("onclick", "javascript:return confirm('确定删除吗？');");
                }

                DataRowView drv = (DataRowView)e.Row.DataItem;

                if ((DropDownList)e.Row.FindControl("ddlPlant") != null)
                {
                    DropDownList ddlCollege = (DropDownList)e.Row.FindControl("ddlPlant");

                    ddlCollege.SelectedValue = drv["PlantID"].ToString();
                }

                if ((DropDownList)e.Row.FindControl("ddlShift") != null)
                {
                    DropDownList ddlCollege = (DropDownList)e.Row.FindControl("ddlShift");

                    ddlCollege.SelectedValue = drv["ShiftID"].ToString();
                }
                
                if ((DropDownList)e.Row.FindControl("ddlPerson") != null)
                {
                    DropDownList ddlCollege = (DropDownList)e.Row.FindControl("ddlPerson");

                    ddlCollege.SelectedValue = drv["PersonID"].ToString();
                }


                if ((DropDownList)e.Row.FindControl("ddlPosition") != null)
                {
                    DropDownList ddlCollege = (DropDownList)e.Row.FindControl("ddlPosition");

                    ddlCollege.SelectedValue = drv["PositionID"].ToString();
                }

                if ((DropDownList)e.Row.FindControl("ddlTeamPerson") != null)
                {
                    DropDownList ddlCollege = (DropDownList)e.Row.FindControl("ddlTeamPerson");

                    ddlCollege.SelectedValue = drv["TeamPersonID"].ToString();
                }

                //鼠标移到效果
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");
            }

        }

        protected void gvTeam_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string keyid = e.CommandArgument.ToString();

            if (e.CommandName == "dataDelete")
            {
                if (KPI_TeamDal.DeleteTeam(keyid))
                {
                    MessageBox.popupClientMessage(this.Page, "删除成功！", "call();");

                    BindTeam();
                }
                else
                {
                    MessageBox.popupClientMessage(this.Page, "删除错误！", "call();");
                }

            }
        }

        protected void gvTeam_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvTeam.EditIndex = e.NewEditIndex;

            BindTeam();
        }

        protected void gvTeam_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvTeam.EditIndex = -1;

            BindTeam();
        }

        protected void gvTeam_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            HtmlInputHidden key = (HtmlInputHidden)gvTeam.Rows[e.RowIndex].Cells[0].FindControl("teamid");

            string sID = key.Value;
            string sShift = ((DropDownList)(gvTeam.Rows[e.RowIndex].Cells[1].FindControl("ddlShift"))).SelectedValue;
            string sPerson = ((DropDownList)(gvTeam.Rows[e.RowIndex].Cells[2].FindControl("ddlPerson"))).SelectedValue;
            //string sPosition = ((DropDownList)(gvTeam.Rows[e.RowIndex].Cells[3].FindControl("ddlPosition"))).SelectedValue;
            string sTeamPerson = ((DropDownList)(gvTeam.Rows[e.RowIndex].Cells[4].FindControl("ddlTeamPerson"))).SelectedValue;
            string sNote = ((TextBox)(gvTeam.Rows[e.RowIndex].Cells[5].Controls[0])).Text.ToString().Trim();

            //更新
            KPI_TeamEntity ote = new KPI_TeamEntity();
            ote.TeamID = sID;
            ote.ShiftID = sShift;
            ote.PersonID = sPerson;
            ote.PositionID = KPI_PersonDal.GetPositionID(sPerson); //防止乱选sPosition;
            ote.TeamPersonID = sTeamPerson;
            ote.TeamNote = sNote;
            ote.TeamModifyTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");

            if (KPI_TeamDal.Update(ote))
            {
                MessageBox.popupClientMessage(this.Page, "编辑成功！", "call();");

            }
            else
            {
                MessageBox.popupClientMessage(this.Page, "编辑错误！", "call();");

            }

            gvTeam.EditIndex = -1;

            BindTeam();


        }

        /// <summary>
        /// 编辑时,岗位随着人变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ddlPerson_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList DDPerson= (DropDownList)sender;//获取现在的事件触发者：人的DropDownList
            GridViewRow gvr = (GridViewRow)DDPerson.NamingContainer;//人与岗位同属于在一个NamingContainer下

            DropDownList DDPosition = (DropDownList)gvr.FindControl("ddlPosition"); //找到岗位的DropDownList

            string strperson = DDPerson.SelectedValue;
            string strpositionid = KPI_PersonDal.GetPositionID(strperson);
            DDPosition.SelectedValue = strpositionid;

            //DDCun.DataSource = getCommon_cun(DDXiang.SelectedValue).Tables[0].DefaultView;//这个函数是我自定义的，通过乡的编辑取得与之对应的村的数据集
            //DDCun.DataTextField = "村名";
            //DDCun.DataValueField = "编号";
            //DDCun.DataBind();
        }
        

        #endregion

        protected void btnAddTeam_Click(object sender, EventArgs e)
        {
            int index = KPI_TeamDal.TeamIDCounts();

            string sID = PageControl.GetGuid();

            KPI_TeamEntity ote = new KPI_TeamEntity();
            ote.TeamID = sID;
            ote.ShiftID = "";
            ote.PersonID = "";
            ote.PositionID = "";
            ote.TeamPersonID = "";
            ote.TeamNote = "";

            ote.TeamCreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            ote.TeamModifyTime = ote.TeamCreateTime;

            if (KPI_TeamDal.Insert(ote))
            {
                //MessageBox.popupClientMessage(this.Page, "添加成功！", "call();");

                gvTeam.EditIndex = index;

                BindTeamByTime();
            }
            else
            {
                MessageBox.popupClientMessage(this.Page, "添加错误！", "call();");

            }


        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            if (KPI_TeamDal.DeleteTeam(""))
            {
                BindTeam();

                MessageBox.popupClientMessage(this.Page, "删除成功！", "call();");
            }

        }

        protected void btnBatch_Click(object sender, EventArgs e)
        {

            Response.Redirect("KPI_SubTeamBatch.aspx");

        }


    }
}
