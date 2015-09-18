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
    public partial class KPI_ConstantConfig : System.Web.UI.Page
    {
        //private static DataSet dsPosition = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //dsPosition = KPI_PositionDal.GetPositionsDataSet();

                BindConstant();
            }
        }

        /// <summary>
        ///  //设备信息
        /// </summary>
        void BindConstant()
        {
            //设备信息
            DataTable dtConstant = KPI_ConstantDal.GetConstantList();
            gvConstant.DataSource = dtConstant;
            gvConstant.DataBind();

        }

        #region 岗位信息配置

        protected void gvConstant_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //HtmlInputHidden key = (HtmlInputHidden)e.Row.Cells[0].FindControl("personid");

                if ((LinkButton)e.Row.FindControl("delete") != null)
                {
                    LinkButton lb = (LinkButton)e.Row.FindControl("delete");
                    lb.Attributes.Add("onclick", "javascript:return confirm('确定删除吗？');");
                }

                //鼠标移到效果
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");
            }

        }

        protected void gvConstant_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string keyid = e.CommandArgument.ToString();

            if (e.CommandName == "dataDelete")
            {
                if (KPI_ConstantDal.DeleteConstant(keyid))
                {
                    MessageBox.popupClientMessage(this.Page, "删除成功！", "call();");

                    BindConstant();
                }
                else
                {
                    MessageBox.popupClientMessage(this.Page, "删除错误！", "call();");
                }

            }
        }

        protected void gvConstant_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvConstant.PageIndex = e.NewPageIndex;

            BindConstant();
        }

        protected void gvConstant_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvConstant.EditIndex = e.NewEditIndex;

            BindConstant();
        }

        protected void gvConstant_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvConstant.EditIndex = -1;

            BindConstant();
        }

        protected void gvConstant_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            HtmlInputHidden key = (HtmlInputHidden)gvConstant.Rows[e.RowIndex].Cells[0].FindControl("constantid");

            string sID = key.Value; 
            string sCode = ((TextBox)(gvConstant.Rows[e.RowIndex].Cells[1].Controls[0])).Text.ToString().Trim();            
            string sName = ((TextBox)(gvConstant.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim();
            string sDesc = ((TextBox)(gvConstant.Rows[e.RowIndex].Cells[3].Controls[0])).Text.ToString().Trim();
            string sValue = ((TextBox)(gvConstant.Rows[e.RowIndex].Cells[4].Controls[0])).Text.ToString().Trim();          
            string sNote = ((TextBox)(gvConstant.Rows[e.RowIndex].Cells[5].Controls[0])).Text.ToString().Trim();

            string msg = "";
            if (sCode == "")
            {
                msg += "类型不能为空！\r\n";
            }

            if (sName == "")
            {
                msg += "名称不能为空！\r\n";
            }

            if (!Regex.IsMatch(sCode, "^[A-Za-z]+$"))
            {
                msg += "类型只能为字母！\r\n";
            }

            if (msg != "")
            {
                MessageBox.popupClientMessage(this.Page, msg);
                return;
            }

            //代码是否重复
            if (KPI_ConstantDal.ConstantNameExists(sName,sCode ))
            {
                MessageBox.popupClientMessage(this.Page, "相同的类型下已存在该名称!");
                return;
            }

            //更新
            ConstantEntity ote = new ConstantEntity();
            ote.ConstantID = sID;
            ote.ConstantCode = sCode;
            ote.ConstantName = sName;
            ote.ConstantDesc = sDesc;
            ote.ConstantValue = sValue;
            ote.ConstantNote = sNote;
            ote.ConstantModifyTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");

            if (KPI_ConstantDal.Update(ote))
            {
                MessageBox.popupClientMessage(this.Page, "编辑成功！", "call();");

            }
            else
            {
                MessageBox.popupClientMessage(this.Page, "编辑错误！", "call();");

            }

            gvConstant.EditIndex = -1;

            BindConstant();


        }


        #endregion

        protected void btnAddConstant_Click(object sender, EventArgs e)
        {
            int index = KPI_ConstantDal.ConstantIDCounts();

            string sID = PageControl.GetGuid();

            ConstantEntity ote = new ConstantEntity();
            ote.ConstantID = sID;
            ote.ConstantCode = "InputCode";
            ote.ConstantName = "Input Name";
            ote.ConstantDesc = "";
            ote.ConstantValue = "Input Value";
            ote.ConstantNote = "";

            ote.ConstantCreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            ote.ConstantModifyTime = ote.ConstantCreateTime;

            if (KPI_ConstantDal.Insert(ote))
            {
                //MessageBox.popupClientMessage(this.Page, "添加成功！", "call();");

                gvConstant.EditIndex = index;

                BindConstant();
            }
            else
            {
                MessageBox.popupClientMessage(this.Page, "添加错误！", "call();");

            }


        }

    }
}
