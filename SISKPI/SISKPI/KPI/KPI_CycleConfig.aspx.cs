using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

using SIS.Assistant;
using SIS.DBControl;
using SIS.Loger;
using SIS.DataEntity;
using SIS.DataAccess;

namespace SISKPI
{
    public partial class KPI_CycleConfig : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        void BindGrid()
        {
            DataTable dt = CycleDal.GetSearchList();

            gvCycle.DataSource = dt;

            gvCycle.DataBind();

        }

        #region Cycle   

        protected void gvCycle_RowDataBound(object sender, GridViewRowEventArgs e)
        {
          if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((LinkButton)e.Row.Cells[6].FindControl("lb_delete") != null)
                {
                    LinkButton lb = (LinkButton)e.Row.Cells[6].FindControl("lb_delete");
                    lb.Attributes.Add("onclick", "javascript:return confirm('确定删除吗？');");
                }

                //鼠标移到效果
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");   

            }
 
        }

        protected void gvCycle_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string Cycleid = e.CommandArgument.ToString();

            if (e.CommandName == "dataDelete")
            {
                if (CycleDal.DeleteCycle(Cycleid))
                {
                    MessageBox.popupClientMessage(this.Page, "删除成功！", "call();");
                    BindGrid();
                }
                else
                {
                    MessageBox.popupClientMessage(this.Page, "删除错误！", "call();");
                }
           
            }
        }



        protected void gvCycle_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvCycle.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void gvCycle_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvCycle.EditIndex = -1;
            BindGrid();
        }

        protected void gvCycle_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {   
            string keyid = ((HtmlInputHidden)(gvCycle.Rows[e.RowIndex].Cells[0].FindControl("Cycleid"))).Value;
  
            string eName = ((TextBox)(gvCycle.Rows[e.RowIndex].Cells[1].Controls[0])).Text.ToString().Trim().ToUpper();
            string eDesc = ((TextBox)(gvCycle.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim();
            string eValue = ((TextBox)(gvCycle.Rows[e.RowIndex].Cells[3].Controls[0])).Text.ToString().Trim();
            string eNote = ((TextBox)(gvCycle.Rows[e.RowIndex].Cells[4].Controls[0])).Text.ToString().Trim();

            string msg = "";
            if (eName == "")
            {
                msg += "名称不能为空！\r\n";
            }

            if (!Regex.IsMatch(eValue, "^[0-9]+$"))
            {
                msg += "周期只能为数字！\r\n";
            }

            if  ( (int.Parse(eValue)<=60 && 60 % int.Parse(eValue)!=0)
               || (int.Parse(eValue) > 60 && int.Parse(eValue) % 60 != 0))
            {
                msg += "周期只能为分钟的倍数！\r\n";
            }

            if ((eName != "TN" || eName != "TD") && double.Parse(eValue) == 0)
            {
                msg += "除TN，TD外，计算周期不能为0";
            }

            if (msg != "")
            {
                MessageBox.popupClientMessage(this.Page, msg);
                return;
            }

            if (CycleDal.CycleNameExists(eName, keyid))
            {
                MessageBox.popupClientMessage(this.Page, "该周期已存在！", "call();");
                return;
            }


            CycleEntity mEntity = new CycleEntity();

            mEntity.CycleID = keyid;
            mEntity.CycleName = eName;
            mEntity.CycleDesc = eDesc;
            mEntity.CycleValue = int.Parse(eValue);
            mEntity.CycleNote = eNote;

            //mEntity.CycleCreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            mEntity.CycleModifyTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");

            if (!CycleDal.Update(mEntity))
            {
                MessageBox.popupClientMessage(this.Page, "编辑错误！", "call();");
            }
            else
            {
                MessageBox.popupClientMessage(this.Page, "编辑成功！", "call();");

                gvCycle.EditIndex = -1;

                BindGrid();
            }
        }    

        #endregion

        protected void btnAddCycle_Click(object sender, EventArgs e)
        {
            int index = CycleDal.CycleIDCounts();

            string sID = PageControl.GetGuid();

            CycleEntity ote = new CycleEntity();
            ote.CycleID = sID;
            ote.CycleName = "TX";
            ote.CycleDesc = "10分钟";
            ote.CycleValue = 10;
            ote.CycleNote = "分钟计算周期";
            ote.CycleCreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            ote.CycleModifyTime = ote.CycleCreateTime;

            if (CycleDal.Insert(ote))
            {
                //MessageBox.popupClientMessage(this.Page, "添加成功！", "call();");
            }
            else
            {
                MessageBox.popupClientMessage(this.Page, "添加错误！", "call();");

            }

            gvCycle.EditIndex = index;
            
            BindGrid();
        }
    }
}
