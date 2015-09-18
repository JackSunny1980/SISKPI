using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

using SIS.Assistant;
using SIS.DBControl;
using SIS.Loger;
using SIS.DataEntity;
using SIS.DataAccess;

namespace SISKPI
{
    public partial class KPI_EngunitConfig : System.Web.UI.Page
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
            txt_EngunitName.Text = txt_EngunitName.Text.Trim();

            DataTable dt = EngunitDal.GetSearchList(txt_EngunitName.Text);

            gvEngunit.DataSource = dt;

            gvEngunit.DataBind();

        }

        #region Engunit   

        protected void gvEngunit_RowDataBound(object sender, GridViewRowEventArgs e)
        {
          if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((LinkButton)e.Row.Cells[5].FindControl("lb_delete") != null)
                {
                    LinkButton lb = (LinkButton)e.Row.Cells[5].FindControl("lb_delete");
                    lb.Attributes.Add("onclick", "javascript:return confirm('确定删除吗？');");
                }

                //鼠标移到效果
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");   

            }
 
        }

        protected void gvEngunit_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string engunitid = e.CommandArgument.ToString();

            if (e.CommandName == "dataDelete")
            {
                if (EngunitDal.DeleteEngunit(engunitid))
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



        protected void gvEngunit_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvEngunit.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void gvEngunit_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvEngunit.EditIndex = -1;
            BindGrid();
        }

        protected void gvEngunit_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {   
            string keyid = ((HtmlInputHidden)(gvEngunit.Rows[e.RowIndex].Cells[0].FindControl("engunitid"))).Value;
  
            string eName = ((TextBox)(gvEngunit.Rows[e.RowIndex].Cells[1].Controls[0])).Text.ToString().Trim();
            string eDesc = ((TextBox)(gvEngunit.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim();
            string eNote = ((TextBox)(gvEngunit.Rows[e.RowIndex].Cells[3].Controls[0])).Text.ToString().Trim();

           string msg = "";
           if (eName == "")
            {
                msg += "名称不能为空！\r\n";
            }

            if (msg != "")
            {
                MessageBox.popupClientMessage(this.Page, msg);
                return;
            }

            if (EngunitDal.EngunitExist(eName, keyid))
            {
                MessageBox.popupClientMessage(this.Page, "该单位已存在！", "call();");
                return;
            }


            EngunitEntity mEntity = new EngunitEntity();

            mEntity.EngunitID = keyid;
            mEntity.EngunitName = eName;
            mEntity.EngunitDesc = eDesc;
            mEntity.EngunitNote = eNote;

            //mEntity.EngunitCreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            mEntity.EngunitModifyTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");

            if (!EngunitDal.Update(mEntity))
            {
                MessageBox.popupClientMessage(this.Page, "编辑错误！", "call();");
            }
            else
            {
                MessageBox.popupClientMessage(this.Page, "编辑成功！", "call();");

                gvEngunit.EditIndex = -1;

                BindGrid();
            }
        }    


        protected void btnAddEngunit_Click(object sender, EventArgs e)
        {
            int index = EngunitDal.EngunitIDCounts();

            string sID = PageControl.GetGuid();

            EngunitEntity ote = new EngunitEntity();
            ote.EngunitID = sID;
            ote.EngunitName = "Input Name";
            ote.EngunitDesc = "";
            ote.EngunitNote = "";
            ote.EngunitCreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            ote.EngunitModifyTime = ote.EngunitCreateTime;

            if (EngunitDal.Insert(ote))
            {
                //MessageBox.popupClientMessage(this.Page, "添加成功！", "call();");
            }
            else
            {
                MessageBox.popupClientMessage(this.Page, "添加错误！", "call();");

            }

            gvEngunit.EditIndex = index;
            
            BindGrid();
        }


        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            BindGrid();
        }
    }
}
