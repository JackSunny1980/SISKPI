using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIS.DBControl;
using System.Data;
using SIS.Loger;
using System.Web.UI.HtmlControls;

using SIS.DataEntity;
using SIS.DataAccess;

namespace SISKPI
{
    public partial class KPI_Group : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //btnSISAdmin.Attributes.Add("onclick", "javascript:return confirm('此操作将删除所有角色！确认初始化吗？');");

                BindGrid();
            }
        }

        void BindGrid()
        {
            string strName = txt_GroupName.Text.Trim();

            DataTable dt = GroupDal.GetGroups(strName);

            gvGroup.DataSource = dt;

            gvGroup.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvGroup.PageIndex = 0;
            BindGrid();
        }

        protected void gvGroup_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvGroup.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void gvGroup_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvGroup.EditIndex = -1;
            BindGrid();
        }

        protected void gvGroup_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string groupname = ((TextBox)(gvGroup.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim();
            string groupdesc = ((TextBox)(gvGroup.Rows[e.RowIndex].Cells[3].Controls[0])).Text.ToString().Trim();
            string groupisvalid = ((TextBox)(gvGroup.Rows[e.RowIndex].Cells[4].Controls[0])).Text.ToString().Trim();
            string groupnote = ((TextBox)(gvGroup.Rows[e.RowIndex].Cells[5].Controls[0])).Text.ToString().Trim();

            string groupid = ((HtmlInputHidden)(gvGroup.Rows[e.RowIndex].Cells[0].FindControl("groupid"))).Value;

            string msg = "";
            if (groupname == "")
            {
                msg += "名称不能为空！\r\n";
            }
            
            if (groupisvalid == "")
            {
                msg += "有效性不能为空！\r\n";
            }
            else
            {
                try
                {
                    int.Parse(groupisvalid);
                }
                catch
                {
                    msg += "有效性格式错误！\r\n";
                }
            }
            
            if (GroupDal.GroupExist(groupname, groupid))
            {
                MessageBox.popupClientMessage(this.Page, "该组名称已存在！", "call();");
                return;
            }


            GroupEntity mEntity = new GroupEntity();
            mEntity.GroupID = groupid;
            mEntity.GroupName = groupname;
            mEntity.GroupDesc = groupdesc;
            mEntity.GroupIsValid = int.Parse(groupisvalid);
            mEntity.GroupNote = groupnote;

            //mEntity.GroupCreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            mEntity.GroupModifyTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");

            if (!GroupDal.Update(mEntity))
            {
                MessageBox.popupClientMessage(this.Page, "修改错误！", "call();");
            }
            else
            {
                MessageBox.popupClientMessage(this.Page, "修改成功！", "call();");

                gvGroup.EditIndex = -1;
                BindGrid();
            }
        }       

        protected void gvGroup_RowDataBound(object sender, GridViewRowEventArgs e)
        {
          if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((LinkButton)e.Row.Cells[7].FindControl("lb_delete") != null)
                {
                    LinkButton lb = (LinkButton)e.Row.Cells[7].FindControl("lb_delete");
                    lb.Attributes.Add("onclick", "javascript:return confirm('确定删除吗？');");
                }

                //鼠标移到效果
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");   

            }
 
        }

        protected void gvGroup_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string groupid = e.CommandArgument.ToString();

            if (e.CommandName == "dataDelete")
            {
                if (GroupDal.DeleteGroupID(groupid))
                {
                    MessageBox.popupClientMessage(this.Page, "删除成功！", "call();");
                    BindGrid();
                }
                else
                {
                    MessageBox.popupClientMessage(this.Page, "删除错误！", "call();");
                    return;
                }
           
            }
        }

        protected void gvGroup_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGroup.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string groupname = tbx_GroupName.Text.Trim();
            string groupdesc = tbx_GroupDesc.Text.Trim();
            string groupisvalid = ddl_GroupIsValid.Value;
            string groupnote = tbx_GroupNote.Text.Trim();

            //
            string groupid = Guid.NewGuid().ToString();
            string groupcode = GroupDal.GetNextCode();

            string msg = "";
            if (groupname == "")
            {
                msg += "名称不能为空！\r\n";
            }

            if (groupisvalid == "")
            {
                msg += "有效性不能为空！\r\n";
            }
            else
            {
                try
                {
                    int.Parse(groupisvalid);
                }
                catch
                {
                    msg += "有效性格式错误！\r\n";
                }
            }

            if (msg != "")
            {
                MessageBox.popupClientMessage(this.Page, msg, "call();");
                return;
            }

            if (GroupDal.GroupExist(groupname, groupid))
            {
                MessageBox.popupClientMessage(this.Page, "该组名称已存在！", "call();");
                return;
            }

            GroupEntity mEntity = new GroupEntity();

            mEntity.GroupID = groupid;
            mEntity.GroupCode = groupcode;
            mEntity.GroupName = groupname;
            mEntity.GroupDesc = groupdesc;
            mEntity.GroupIsValid = int.Parse(groupisvalid);
            mEntity.GroupNote = groupnote;

            mEntity.GroupCreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            mEntity.GroupModifyTime = mEntity.GroupCreateTime;

            if (!GroupDal.Insert(mEntity))
            {
                MessageBox.popupClientMessage(this.Page, "修改错误！", "call();");
            }
            else
            {
                MessageBox.popupClientMessage(this.Page, "修改成功！", "call();");

                tbx_GroupName.Text = "";
                tbx_GroupDesc.Text = "";
                tbx_GroupNote.Text = "";

                gvGroup.EditIndex = -1;
                BindGrid();
            }

            
        }

        protected void btnMenu_Click(object sender, EventArgs e)
        {
            Response.Redirect("KPI_Menu.aspx");
        }

        protected void btnUser_Click(object sender, EventArgs e)
        {
            Response.Redirect("KPI_User.aspx");

        }

        protected void btnGroup_Click(object sender, EventArgs e)
        {
            //Response.Redirect("KPI_Group.aspx");

        }

        //protected void btnSISAdmin_Click(object sender, EventArgs e)
        //{
        //    KPI_GroupDal.SetSISAdmin();

        //    MessageBox.popupClientMessage(this.Page, "初始化成功！", "call();");

        //}
    }
}
