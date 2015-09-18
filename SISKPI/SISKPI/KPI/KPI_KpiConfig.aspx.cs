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
    public partial class KPI_KpiConfig : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindKpi();
            }
        }

        /// <summary>
        ///  //设备信息
        /// </summary>
        void BindKpi()
        {
            //设备信息
            DataTable dtKpi = KpiDal.GetSearchList("");
            gvKpi.DataSource = dtKpi;
            gvKpi.DataBind();

        }

        #region 设备信息配置

        protected void gvKpi_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //HtmlInputHidden key = (HtmlInputHidden)e.Row.Cells[0].FindControl("unitid");

                //if ((HtmlInputButton)e.Row.Cells[7].FindControl("stab") != null)
                //{
                //    HtmlInputButton btn = (HtmlInputButton)e.Row.Cells[7].FindControl("stab");
                //    btn.Attributes.Add("onclick", "javascript:UnitStab('" + key.Value + "');");
                //}

                //鼠标移到效果
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");
            }

        }

        protected void gvKpi_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string keyid = e.CommandArgument.ToString();

            if (e.CommandName == "dataDelete")
            {
                if (KpiDal.DeleteKpi(keyid))
                {
                    MessageBox.popupClientMessage(this.Page, "删除成功！", "call();");

                    BindKpi();
                }
                else
                {
                    MessageBox.popupClientMessage(this.Page, "删除错误！", "call();");
                }

            }
        }

        protected void gvKpi_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvKpi.PageIndex = e.NewPageIndex;

            BindKpi();
        }

        protected void gvKpi_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvKpi.EditIndex = e.NewEditIndex;

            BindKpi();
        }

        protected void gvKpi_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvKpi.EditIndex = -1;

            BindKpi();
        }

        protected void gvKpi_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            HtmlInputHidden key = (HtmlInputHidden)gvKpi.Rows[e.RowIndex].Cells[0].FindControl("Kpiid");

            string sID = key.Value; 
            string sCode = ((TextBox)(gvKpi.Rows[e.RowIndex].Cells[1].Controls[0])).Text.ToString().Trim();            
            string sName = ((TextBox)(gvKpi.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim();
            string sDesc = ((TextBox)(gvKpi.Rows[e.RowIndex].Cells[3].Controls[0])).Text.ToString().Trim();
            string sNote = ((TextBox)(gvKpi.Rows[e.RowIndex].Cells[4].Controls[0])).Text.ToString().Trim();

            string msg = "";
            if (sName == "" || sCode =="")
            {
                msg += "代码或名称不能为空！\r\n";
            }

            if(!Regex.IsMatch(sCode, "^[A-Za-z0-9]+$"))
            {                
                msg += "代码只能为字母和数字组成！\r\n";
            }

            if (msg != "")
            {
                MessageBox.popupClientMessage(this.Page, msg);
                return;
            }

            //代码是否重复
            if (KpiDal.KpiCodeExists(sCode, sID))
            {
                MessageBox.popupClientMessage(this.Page, "已存在相同的代码!");
                return;
            }

            //更新
            KpiEntity ote = new KpiEntity();
            ote.KpiID = sID;
            ote.KpiCode = sCode;
            ote.KpiName = sName;
            ote.KpiDesc = sDesc;
            ote.KpiNote = sNote;
            ote.KpiModifyTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");

            if (KpiDal.Update(ote))
            {
                MessageBox.popupClientMessage(this.Page, "编辑成功！", "call();");

            }
            else
            {
                MessageBox.popupClientMessage(this.Page, "编辑错误！", "call();");

            }

            gvKpi.EditIndex = -1;

            BindKpi();


        }

        protected void btnAddKpi_Click(object sender, EventArgs e)
        {
            int index = KpiDal.KpiIDCounts();

            string sID = PageControl.GetGuid();

            KpiEntity ote = new KpiEntity();
            ote.KpiID = sID;
            ote.KpiCode = "InputCode";
            ote.KpiName = "Input Name";
            ote.KpiDesc = "";
            ote.KpiIsValid = 1;
            ote.KpiNote = "";

            ote.KpiCreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            ote.KpiModifyTime = ote.KpiCreateTime;

            if (KpiDal.Insert(ote))
            {
                //MessageBox.popupClientMessage(this.Page, "添加成功！", "call();");
            }
            else
            {
                MessageBox.popupClientMessage(this.Page, "添加错误！", "call();");

            }

            gvKpi.EditIndex = index;

            BindKpi();

        }

        #endregion

    }
}
