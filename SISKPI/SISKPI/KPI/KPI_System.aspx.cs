using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

using SIS.DBControl;
using SIS.DataAccess;
using SIS.Loger;
using SIS.Assistant;

namespace SISKPI
{
    public partial class KPI_System : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                rbnKPIAuto.SelectedValue = KPI_SystemDal.GetKPIAuto().ToString();
                rbnKPITimeMode.SelectedValue = KPI_SystemDal.GetKPITimeMode().ToString();
                tbxKPIOffset.Text = KPI_SystemDal.GetKPIOffset().ToString();

                rbnKPIReload.SelectedValue = KPI_SystemDal.GetKPIReload()?"1" :"0";

                BindGrid();
            }
        }

        void BindGrid()
        {
            DataTable dt = KPI_SystemDal.GetParamList();

            gvSys.DataSource = dt;

            gvSys.DataBind();
        }

        
        protected void gvSys_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //鼠标移到效果
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");
            }
        }    

        protected void gvSys_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvSys.EditIndex = e.NewEditIndex;

            BindGrid();

        }

        protected void gvSys_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvSys.EditIndex = -1;

            BindGrid();

        }

        protected void gvSys_RowUpdating(object sender, GridViewUpdateEventArgs e)
        { 
            string sysid = ((Label)(gvSys.Rows[e.RowIndex].Cells[0].FindControl("sysid"))).Text.ToString().Trim();

            string valid = ((TextBox)(gvSys.Rows[e.RowIndex].Cells[3].Controls[0])).Text.ToString().Trim();
            string value = ((TextBox)(gvSys.Rows[e.RowIndex].Cells[4].Controls[0])).Text.ToString().Trim();
            string note = ((TextBox)(gvSys.Rows[e.RowIndex].Cells[5].Controls[0])).Text.ToString().Trim();

            if (KPI_SystemDal.UpdateByID(sysid, valid, value, note))
            {
                gvSys.EditIndex = -1;

                BindGrid();

                //MessageBox.popupClientMessage(this.Page, "编辑成功！", "call();");
                return;
            }
            else
            {
                MessageBox.popupClientMessage(this.Page, "编辑错误！", "call();");
            }
        }

        protected void rbnKPIAuto_SelectedIndexChanged(object sender, EventArgs e)
        {
            //判断
            if (rbnKPIAuto.SelectedValue == "0" || rbnKPIAuto.SelectedValue == "1")
            {
                if (KPI_SystemDal.UpdateByName("KPIAuto", "1", rbnKPIAuto.SelectedValue, ""))
                {
                    //MessageBox.popupClientMessage(this.Page, "编辑成功！", "call();");

                    BindGrid();
                }
                else
                {
                    MessageBox.popupClientMessage(this.Page, "请添加KPIAuto！", "call();");

                }

            }

        }


        protected void rbnKPITimeMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            //判断
            if (rbnKPITimeMode.SelectedValue == "0" || rbnKPITimeMode.SelectedValue == "1")
            {
                if (KPI_SystemDal.UpdateByName("KPITimeMode", "1", rbnKPITimeMode.SelectedValue, ""))
                {
                    //MessageBox.popupClientMessage(this.Page, "编辑成功！", "call();");

                    BindGrid();
                }
                else
                {
                    MessageBox.popupClientMessage(this.Page, "请添加KPITimeMode！", "call();");

                }

            }

        }

        protected void btnUpdateOffset_Click(object sender, EventArgs e)
        {
            //判断
            int a = 0;
            if(!int.TryParse(tbxKPIOffset.Text.Trim(), out a))
            {
                MessageBox.popupClientMessage(this.Page, "数值不正确！", "call();");

                return;
            }

            if (a < 0)
            {
                MessageBox.popupClientMessage(this.Page, "数值不能为负值，请调整计算站时间！", "call();");

                return;
            }

            //
            if (KPI_SystemDal.UpdateByName("KPIOffset", "1", a.ToString(), ""))
            {
                //MessageBox.popupClientMessage(this.Page, "编辑成功！", "call();");

                BindGrid();
            }

            
        }

        protected void rbnKPIReload_SelectedIndexChanged(object sender, EventArgs e)
        {
            //判断
            if (rbnKPIReload.SelectedValue == "0" || rbnKPIReload.SelectedValue == "1")
            {
                if (KPI_SystemDal.UpdateByName("KPIReload", "1", rbnKPIReload.SelectedValue, ""))
                {
                    //MessageBox.popupClientMessage(this.Page, "编辑成功！", "call();");

                    BindGrid();
                }

            }

        }

        protected void btnUpdateKPIFirstURL_Click(object sender, EventArgs e)
        {
            string url = tbxKPIFirtURL.Text;

            //
            if (KPI_SystemDal.UpdateByName("KPIFirstURL", "1", url, ""))
            {
                //MessageBox.popupClientMessage(this.Page, "编辑成功！", "call();");

                BindGrid();
            }


        }

    }
}
