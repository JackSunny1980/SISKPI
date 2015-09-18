using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

using SIS.DataAccess;
using SIS.DataEntity;
using SIS.DBControl;
using SIS.Loger;
using SIS.Assistant;

namespace SISKPI
{
    /// <summary>
    /// 查询、显示、添加、编辑
    /// 电厂、机组信息；
    /// 指标考核基本配置；
    /// </summary>
    public partial class KPI_Config : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindPlant();
                BindUnit();
            }
        }

        /// <summary>
        /// 显示电厂信息
        /// </summary>
        void BindPlant()
        {
            //电厂信息
            DataTable dtPlant = KPI_PlantDal.GetSearchList("");
            gvPlant.DataSource = dtPlant;
            gvPlant.DataBind();

        }

        /// <summary>
        /// 显示机组信息
        /// </summary>
        void BindUnit()
        {
            //机组信息
            txt_UnitName.Text = txt_UnitName.Text.Trim();
            txt_UnitDesc.Text = txt_UnitDesc.Text.Trim();

            string sql = "";

            if (txt_UnitName.Text != "")
            {
                sql += " and UnitName like '%" + txt_UnitName.Text + "%'";
            }

            if (txt_UnitDesc.Text != "")
            {
                sql += " and UnitDesc like '%" + txt_UnitDesc.Text + "%'";
            }

            DataTable dtUnit = KPI_UnitDal.GetSearchList(sql);
            gvUnit.DataSource = dtUnit;
            gvUnit.DataBind();


        }
           
        
        #region 电厂信息配置
        
        protected void gvPlant_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HtmlInputHidden key = (HtmlInputHidden)e.Row.Cells[0].FindControl("plantid");

                if ((HtmlInputButton)e.Row.Cells[7].FindControl("plantupdate") != null)
                {
                    HtmlInputButton btn = (HtmlInputButton)e.Row.Cells[7].FindControl("plantupdate");
                    btn.Attributes.Add("onclick", "javascript:PlantUpdate('" + key.Value + "');");
                }

                if ((LinkButton)e.Row.Cells[8].FindControl("delete") != null)
                {
                    LinkButton lb = (LinkButton)e.Row.Cells[8].FindControl("delete");
                    lb.Attributes.Add("onclick", "javascript:return confirm('确定删除吗？');");
                }
            }

        }

        protected void gvPlant_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string keyid = e.CommandArgument.ToString();

            if (e.CommandName == "dataDelete")
            {
                if (KPI_PlantDal.DeletePlant(keyid))
                {
                    MessageBox.popupClientMessage(this.Page, "删除成功！", "call();");
                }
                else
                {
                    MessageBox.popupClientMessage(this.Page, "删除错误！", "call();");
                }     
            }
        }

        protected void gvPlant_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPlant.PageIndex = e.NewPageIndex;

            BindPlant();
        }

        protected void btnAddPlant_Click(object sender, EventArgs e)
        {
            //添加新电厂信息

            string strjs = "<script language=javascript>window.open('KPI_SubPlantConfig.aspx','newwindow','width=500,height=400')</script>";

            ClientScript.RegisterStartupScript(this.GetType(), "", strjs);

        }

        #endregion

        #region 机组信息配置

        protected void btnSearchUnit_Click(object sender, EventArgs e)
        {
            gvUnit.PageIndex = 0;

            BindUnit();
        }

        protected void gvUnit_RowDataBound(object sender, GridViewRowEventArgs e)
        {
          if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HtmlInputHidden key = (HtmlInputHidden)e.Row.Cells[0].FindControl("unitid");

                if ((HtmlInputButton)e.Row.Cells[7].FindControl("stab") != null)
                {
                    HtmlInputButton btn = (HtmlInputButton)e.Row.Cells[7].FindControl("stab");
                    btn.Attributes.Add("onclick", "javascript:UnitStab('" + key.Value + "');");
                }

                if ((HtmlInputButton)e.Row.Cells[8].FindControl("filter") != null)
                {
                    HtmlInputButton btn = (HtmlInputButton)e.Row.Cells[8].FindControl("filter");
                    btn.Attributes.Add("onclick", "javascript:UnitFilter('" + key.Value + "');");
                }

                if ((HtmlInputButton)e.Row.Cells[9].FindControl("seek") != null)
                {
                    HtmlInputButton btn = (HtmlInputButton)e.Row.Cells[9].FindControl("seek");
                    btn.Attributes.Add("onclick", "javascript:UnitSeek('" + key.Value + "');");
                }

                if ((HtmlInputButton)e.Row.Cells[10].FindControl("unitupdate") != null)
                {
                    HtmlInputButton btn = (HtmlInputButton)e.Row.Cells[10].FindControl("unitupdate");
                    btn.Attributes.Add("onclick", "javascript:UnitUpdate('" + key.Value + "');");
                }

                if ((LinkButton)e.Row.Cells[11].FindControl("delete") != null)
                {
                    LinkButton lb = (LinkButton)e.Row.Cells[11].FindControl("delete");
                    lb.Attributes.Add("onclick", "javascript:return confirm('确定删除吗？');");
                }

                //鼠标移到效果
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");
            }
 
        }

        protected void gvUnit_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string keyid = e.CommandArgument.ToString();

            if (e.CommandName == "dataDelete")
            {
                if (KPI_UnitDal.DeleteUnit(keyid))
                {
                    MessageBox.popupClientMessage(this.Page, "删除成功！", "call();");
                }
                else
                {
                    MessageBox.popupClientMessage(this.Page, "删除错误！", "call();");
                }     
           
            }
        }

        protected void gvUnit_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUnit.PageIndex = e.NewPageIndex;

            BindUnit();
        }

        protected void btnAddUnit_Click(object sender, EventArgs e)
        {
            //添加新机组，keyid="", type=3
            string strjs = "<script language=javascript>window.open('KPI_SubUnitConfig.aspx','newwindow','width=600,height=500')</script>";

            ClientScript.RegisterStartupScript(this.GetType(), "", strjs);

        }

        #endregion

    }
}
