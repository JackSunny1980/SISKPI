using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using SIS.DBControl;
using System.Data;
using SIS.Loger;
using SIS.Assistant;
using SIS.DataAccess;
using SIS.DataEntity;

namespace SISKPI
{
    public partial class KPI_LinqConfig : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //绑定Gridview
                BindGrid();
            }
        }

        void BindGrid()
        {
 
            DataTable dt = KPI_LinqDal.GetLinqSearch("");

            gvLinq.DataSource = dt;
            gvLinq.DataBind();
        }

        #region Gridview

        protected void gvLinq_RowDataBound(object sender, GridViewRowEventArgs e)
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

                    ddlCollege.SelectedValue = drv["LinqIsValid"].ToString();
                }

                //鼠标移到效果
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");

            }

        }


        protected void gvLinq_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvLinq.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void gvLinq_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvLinq.EditIndex = -1;
            BindGrid();
        }

        protected void gvLinq_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //从GridView获得数据
            string sID = ((HtmlInputHidden)(gvLinq.Rows[e.RowIndex].Cells[0].FindControl("linqid"))).Value;

            string sName = ((TextBox)(gvLinq.Rows[e.RowIndex].Cells[1].Controls[0])).Text.ToString().Trim().ToUpper();
            string sDesc = ((TextBox)(gvLinq.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim().ToUpper();
            string sEngunit = ((TextBox)(gvLinq.Rows[e.RowIndex].Cells[3].Controls[0])).Text.ToString().Trim();

            string sValid = ((DropDownList)(gvLinq.Rows[e.RowIndex].Cells[4].FindControl("ddlValid"))).SelectedValue;
            string sNote = ((TextBox)(gvLinq.Rows[e.RowIndex].Cells[5].Controls[0])).Text.ToString().Trim();

            string msg = "";

            //检查名称
            if (sName == "")
            {
                msg += "名称不能为空！\r\n";
            }
            else if (KPI_LinqDal.LinqNameExists(sName, sID))
            {
                MessageBox.popupClientMessage(this.Page, "已存在相同的名称!");
                return;
            }

            if (msg != "")
            {
                MessageBox.popupClientMessage(this.Page, msg);
                return;
            }

            LinqEntity csE = new LinqEntity();

            csE.LinqID = sID;
            csE.LinqName = sName;
            csE.LinqDesc = sDesc;
            csE.LinqEngunit = sEngunit;
            csE.LinqIsValid = int.Parse(sValid);
            csE.LinqNote = sNote;
            csE.LinqModifyTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");

            if (KPI_LinqDal.Update(csE))
            {
                MessageBox.popupClientMessage(this.Page, "修改成功！", "call();");

                gvLinq.EditIndex = -1;
                BindGrid();
            }
            else
            {
                MessageBox.popupClientMessage(this.Page, "修改错误！", "call();");
            }            

        }       

        protected void gvLinq_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string keyid = e.CommandArgument.ToString();

            if (e.CommandName == "dataDelete")
            {
                if (KPI_LinqDal.DeleteLinq(keyid))
                {
                    MessageBox.popupClientMessage(this.Page, "删除成功！", "call();");

                    BindGrid();

                    return;
                }
                else
                {
                    MessageBox.popupClientMessage(this.Page, "删除错误！", "call();");
                    BindGrid();
                }

            }
            else if (e.CommandName == "dataConfig")
            {
                Response.Redirect("KPI_SubLinqTag.aspx?linqid=" + keyid);

            }

        }



        protected void btnAddLinq_Click(object sender, EventArgs e)
        {
            int index = KPI_LinqDal.LinqIDCounts();

            string sID = PageControl.GetGuid();

            LinqEntity ote = new LinqEntity();
            ote.LinqID = sID;
            ote.LinqName = "InputName";
            ote.LinqDesc = "";
            ote.LinqEngunit = "";
            ote.LinqIndex = index+1;
            ote.LinqIsValid = 1;
            ote.LinqNote = "";
            ote.LinqCreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            ote.LinqModifyTime = ote.LinqCreateTime;

            if (KPI_LinqDal.Insert(ote))
            {
                //MessageBox.popupClientMessage(this.Page, "添加成功！", "call();");

                gvLinq.EditIndex = index;

                BindGrid();
            }
            else
            {
                MessageBox.popupClientMessage(this.Page, "添加错误！", "call();");

            }

        }

        #endregion

        protected void btnSort_Click(object sender, EventArgs e)
        {
            string strjs = "<script language=javascript>window.open('KPI_SubLinqSort.aspx','newwindow','width=600,height=600')</script>";

            ClientScript.RegisterStartupScript(this.GetType(), "", strjs);

        }
    }
}
