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
    /// 设备信息；
    /// 指标考核基本配置；
    /// </summary>
    public partial class KPI_RealTagConfig : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //机组信息
                DataTable dt = KPI_UnitDal.GetUnits("");
                ddlUnitID.Items.Add(new ListItem("所有机组集", "ALL"));
                foreach (DataRow dr in dt.Rows)
                {
                    ddlUnitID.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
                }
                
                BindReal();
            }
        }

        /// <summary>
        ///  //设备信息
        /// </summary>
        void BindReal()
        {
            string UnitID = ddlUnitID.SelectedValue.Trim();
            if (UnitID == "ALL")
            {
                UnitID = "";
            }

            //信息
            DataTable dtReal = KPI_RealTagDal.GetTagLists(UnitID);
            gvReal.DataSource = dtReal;
            gvReal.DataBind();
        }

        #region 实时标签配置

        protected void gvReal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //HtmlInputHidden key = (HtmlInputHidden)e.Row.Cells[0].FindControl("unitid");

                if ((LinkButton)e.Row.FindControl("delete") != null)
                {
                    LinkButton lb = (LinkButton)e.Row.FindControl("delete");
                    lb.Attributes.Add("onclick", "javascript:return confirm('确定删除吗？');");
                }

                DataRowView drv = (DataRowView)e.Row.DataItem;

                if ((DropDownList)e.Row.FindControl("ddlSnapshot") != null)
                {
                    DropDownList ddlCollege = (DropDownList)e.Row.FindControl("ddlSnapshot");

                    ddlCollege.SelectedValue = drv["RealSnapshot"].ToString();
                }

                if ((DropDownList)e.Row.FindControl("ddlSort") != null)
                {
                    DropDownList ddlCollege = (DropDownList)e.Row.FindControl("ddlSort");

                    ddlCollege.SelectedValue = drv["RealSort"].ToString();
                }

                if ((DropDownList)e.Row.FindControl("ddlDisplay") != null)
                {
                    DropDownList ddlCollege = (DropDownList)e.Row.FindControl("ddlDisplay");

                    if (int.Parse(drv["RealDisplay"].ToString()) > 0)
                    {
                        ddlCollege.SelectedValue = "1";
                    }
                    else
                    {
                        ddlCollege.SelectedValue = "0";
                    }
                }

                if ((DropDownList)e.Row.FindControl("ddlXYZ") != null)
                {
                    DropDownList ddlCollege = (DropDownList)e.Row.FindControl("ddlXYZ");

                    if (int.Parse(drv["RealXYZ"].ToString()) > 0)
                    {
                        ddlCollege.SelectedValue = "1";
                    }
                    else
                    {
                        ddlCollege.SelectedValue = "0";
                    }
                }

                //鼠标移到效果
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");
            }

        }

        protected void gvReal_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string keyid = e.CommandArgument.ToString();

            if (e.CommandName == "dataDelete")
            {
                if (KPI_RealTagDal.DeleteTag(keyid))
                {
                    MessageBox.popupClientMessage(this.Page, "删除成功！", "call();");

                    BindReal();
                }
                else
                {
                    MessageBox.popupClientMessage(this.Page, "删除错误！", "call();");
                }
            }

        }

        protected void gvReal_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvReal.EditIndex = e.NewEditIndex;

            BindReal();
        }

        protected void gvReal_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvReal.EditIndex = -1;

            BindReal();
        }

        protected void gvReal_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            HtmlInputHidden key = (HtmlInputHidden)gvReal.Rows[e.RowIndex].Cells[0].FindControl("realid");

            string sID = key.Value; 
            string sCode = ((TextBox)(gvReal.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim();
            string sDesc = ((TextBox)(gvReal.Rows[e.RowIndex].Cells[3].Controls[0])).Text.ToString().Trim();
            string sEngunit = ((TextBox)(gvReal.Rows[e.RowIndex].Cells[4].Controls[0])).Text.ToString().Trim();
            string sMax = ((TextBox)(gvReal.Rows[e.RowIndex].Cells[5].Controls[0])).Text.ToString().Trim();
            string sMin = ((TextBox)(gvReal.Rows[e.RowIndex].Cells[6].Controls[0])).Text.ToString().Trim();
            string sSnapshot = ((DropDownList)(gvReal.Rows[e.RowIndex].Cells[7].FindControl("ddlSnapshot"))).SelectedValue;
            string sSort = ((DropDownList)(gvReal.Rows[e.RowIndex].Cells[8].FindControl("ddlSort"))).SelectedValue;
            string sDisplay = ((DropDownList)(gvReal.Rows[e.RowIndex].Cells[9].FindControl("ddlDisplay"))).SelectedValue;
            string sXYZ= ((DropDownList)(gvReal.Rows[e.RowIndex].Cells[10].FindControl("ddlXYZ"))).SelectedValue;
            string sNote = ((TextBox)(gvReal.Rows[e.RowIndex].Cells[11].Controls[0])).Text.ToString().Trim();

            string msg = "";
            if (sCode =="")
            {
                msg += "名称不能为空！\r\n";
            }
            
            //判断double格式
            if (sMax!="" && !Regex.IsMatch(sMax, @"^\d*[.]?\d*$"))
            {
                msg += "最值只能为空或数字组成！\r\n";
            }

            //判断double格式
            if (sMin!="" && !Regex.IsMatch(sMin, @"^\d*[.]?\d*$"))
            {
                msg += "最值只能为空或数字组成！\r\n";
            }

            if (msg != "")
            {
                MessageBox.popupClientMessage(this.Page, msg);
                return;
            }

            //代码是否重复
            if (KPI_RealTagDal.CodeExist(sCode, sID) || ALLDal.CodeExist(sCode, sID))
            {
                MessageBox.popupClientMessage(this.Page, "已存在相同的标签!");
                return;
            }

            //更新
            KPI_RealTagEntity ote = new KPI_RealTagEntity();
            ote.RealID = sID;
            ote.RealCode = sCode;
            ote.RealDesc = sDesc;
            ote.RealEngunit = sEngunit;
            if (sMax != "") ote.RealMaxValue = decimal.Parse(sMax);
			if (sMin != "") ote.RealMinValue = decimal.Parse(sMin);
            ote.RealSnapshot = sSnapshot;
            ote.RealSort = sSort;
            //if (sDisplay == "0") 
            ote.RealDisplay = sDisplay;
            ote.RealXYZ = sXYZ;
            ote.RealNote = sNote;
            ote.RealModifyTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");

            if (KPI_RealTagDal.Update(ote))
            {
                MessageBox.popupClientMessage(this.Page, "编辑成功！", "call();");

            }
            else
            {
                MessageBox.popupClientMessage(this.Page, "编辑错误！", "call();");

            }

            gvReal.EditIndex = -1;

            BindReal();


        }

        #endregion


        protected void ddlUnitID_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindReal();
        }

        protected void btnAddReal_Click(object sender, EventArgs e)
        {
            if (ddlUnitID.SelectedValue == "ALL")
            {
                MessageBox.popupClientMessage(this.Page, "请选择具体机组！", "call();");
                return;
            }

            string UnitID = ddlUnitID.SelectedValue.Trim();

            int index = KPI_RealTagDal.IDCounts(UnitID);

            string sID = PageControl.GetGuid();

            KPI_RealTagEntity ote = new KPI_RealTagEntity();
            ote.RealID = sID;
            ote.UnitID = UnitID;
            ote.RealCode = "InputCode";
            ote.RealDesc = "Input DEsc";
            ote.RealEngunit = "Input Engunit";
            ote.RealSnapshot = "0";
            ote.RealSort = "1";
            ote.RealDisplay = "0";
            ote.RealXYZ = "0";
            ote.RealNote = "";

            ote.RealCreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            ote.RealModifyTime = ote.RealCreateTime;

            if (KPI_RealTagDal.Insert(ote))
            {
                //MessageBox.popupClientMessage(this.Page, "添加成功！", "call();");
            }
            else
            {
                MessageBox.popupClientMessage(this.Page, "添加错误！", "call();");

            }

            gvReal.EditIndex = index;

            BindReal();
        }

        protected void btnAddBatch_Click(object sender, EventArgs e)
        {
            Response.Redirect("KPI_SubRealTagBatch.aspx");
        }

        protected void btnDisplaySort_Click(object sender, EventArgs e)
        {
            Response.Redirect("KPI_SubRealTagSort.aspx");

        }


    }
}
