using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

using SIS.DataAccess;
using SIS.DataEntity;
using SIS.Loger;
using SIS.Assistant;

using SIS.DBControl;

namespace SISKPI
{
    public partial class KPI_SubUserGroup : System.Web.UI.Page
    {
        private static DataTable dtNotUsers = null;        
        private static DataTable dtUsers = null;
        private static bool bSaved = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //btnClear.Attributes.Add("onclick", "setDivPos('Lay1');Lay1.style.visibility='';progress_update();");

                btnDel.Attributes.Add("onclick", "javascript:return confirm('请确认：从数据库中删除左侧选中的用户吗？');");
                              
                //权限集合
                //dtGroups For User
                DataTable dtGroups = GroupDal.GetGroupsForUser();
                
                foreach (DataRow dr in dtGroups.Rows)
                {
                    ddl_Groups.Items.Add(new ListItem(dr["GroupName"].ToString(), dr["GroupCode"].ToString()));
                }


                BindValues(true);
            }
        }


        void BindValues(bool bQuery)
        {
            string group = ddl_Groups.SelectedValue;

            if (bQuery)
            {
                dtNotUsers = KPI_UserDal.GetNotUsersForGroup(group);
                dtUsers = KPI_UserDal.GetUsersForGroup(group);
            }

            //左侧

            cbxNotUsers.Items.Clear();
            cbxNotUsers.DataSource = dtNotUsers;
            cbxNotUsers.DataValueField = "UserID";
            cbxNotUsers.DataTextField = "DisplayName";
            cbxNotUsers.DataBind();


            //右侧

            cbxUsers.Items.Clear();
            cbxUsers.DataSource = dtUsers;
            cbxUsers.DataValueField = "UserID";
            cbxUsers.DataTextField = "DisplayName";
            cbxUsers.DataBind();

        }

        protected void ddl_Groups_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bSaved)
            {
                MessageBox.popupClientMessage(this.Page, "前次操作没有保存，将放弃!", "call();");
            }
            
            BindValues(true);
        }              

        #region 按钮操作事件

        protected void btnRight_Click(object sender, ImageClickEventArgs e)
        {
            for (int i = 0; i < cbxNotUsers.Items.Count; i++)
            {
                string strSelected = cbxNotUsers.Items[i].Selected ? cbxNotUsers.Items[i].Value : "";
                if (strSelected != "")
                {
                    foreach (DataRow dr in dtNotUsers.Select("UserID='" + strSelected + "'"))
                    {
                        bSaved = false;

                        dtUsers.ImportRow(dr);

                        dtNotUsers.Rows.Remove(dr);
                    }

                }
            }
            
            BindValues(false);

        }

        protected void btnLeft_Click(object sender, ImageClickEventArgs e)
        {
            for (int i = 0; i < cbxUsers.Items.Count; i++)
            {
                string strSelected = cbxUsers.Items[i].Selected ? cbxUsers.Items[i].Value : "";
                if (strSelected != "")
                {
                    foreach (DataRow dr in dtUsers.Select("UserID='" + strSelected + "'"))
                    {
                        bSaved = false;

                        dtNotUsers.ImportRow(dr);

                        dtUsers.Rows.Remove(dr);
                    }

                }
            }

            BindValues(false);

        }

        protected void btnTransfer_Click(object sender, ImageClickEventArgs e)
        {
            //中间
            DataTable dtTemp = dtNotUsers.Copy();
            dtTemp.Rows.Clear();

            //不考虑是否选中的问题
            //Add From NotUser
            for (int i = 0; i < cbxNotUsers.Items.Count; i++)
            {
                string strSelected = cbxNotUsers.Items[i].Value;
                if (strSelected != "")
                {
                    foreach (DataRow dr in dtNotUsers.Select("UserID='" + strSelected + "'"))
                    {
                        bSaved = false;

                        dtTemp.ImportRow(dr);

                        dtNotUsers.Rows.Remove(dr);
                    }

                }
            }

            //Del From User and Add to NotUser
            for (int i = 0; i < cbxUsers.Items.Count; i++)
            {
                string strSelected = cbxUsers.Items[i].Value;
                if (strSelected != "")
                {
                    foreach (DataRow dr in dtUsers.Select("UserID='" + strSelected + "'"))
                    {
                        bSaved = false;

                        dtNotUsers.ImportRow(dr);
                        dtUsers.Rows.Remove(dr);
                    }

                }
            }

            //Add to User
            foreach (DataRow dr in dtTemp.Rows)
            {
                bSaved = false;

                dtUsers.ImportRow(dr);
                
                //执行结束后即失效,故不用删除，否则会无法使用 foreach
                //dtTemp.Rows.Remove(dr);
            }


            BindValues(false);

        }


        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            if (bSaved)
            {
                MessageBox.popupClientMessage(this.Page, "没有任何操作!", "call();");
                return;
            }

            string group = ddl_Groups.SelectedValue;

            //删除权限
            for (int i = 0; i < dtNotUsers.Rows.Count; i++)
            {
                string strSelected = dtNotUsers.Rows[i]["UserID"].ToString();
                string strGroup = dtNotUsers.Rows[i]["UserGroups"].ToString();

                if (strSelected != "")
                {
                    strGroup = strGroup.Replace(group + ",", "");
                    if (strGroup.Trim() == "")
                    {
                        strGroup = "AB,";
                    }

                    KPI_UserDal.UpdateUserGroups(strSelected, strGroup);

                }
                
                bSaved = true;
            }

            //添加权限
            for (int i = 0; i < dtUsers.Rows.Count; i++)
            {
                string strSelected = dtUsers.Rows[i]["UserID"].ToString();
                string strGroup = dtUsers.Rows[i]["UserGroups"].ToString();

                if (strSelected != "")
                {
                    if (!strGroup.Contains(group))
                    {
                        strGroup += group + ",";
                    }
                    
                    KPI_UserDal.UpdateUserGroups(strSelected, strGroup);
                }

                bSaved = true;
            }

           
            MessageBox.popupClientMessage(this.Page, "保存成功!", "call();");
              

            //可以不用
            //BindValues(true);      

        }

        protected void btnDel_Click(object sender, ImageClickEventArgs e)
        {
            for (int i = 0; i < cbxNotUsers.Items.Count; i++)
            {
                string strSelected = cbxNotUsers.Items[i].Selected ? cbxNotUsers.Items[i].Value : "";
                if (strSelected != "")
                {
                    KPI_UserDal.DeleteUserID(strSelected);
                }
            }

            BindValues(true);            
        }



        protected void btnNotCheckALL_Click(object sender, ImageClickEventArgs e)
        {
            for (int i = 0; i < cbxNotUsers.Items.Count; i++)
            {
                cbxNotUsers.Items[i].Selected = true;
            }

        }

        protected void btnNotCheckNot_Click(object sender, ImageClickEventArgs e)
        {
            for (int i = 0; i < cbxNotUsers.Items.Count; i++)
            {
                cbxNotUsers.Items[i].Selected = false;
            }

        }

        protected void btnNotCheck_Click(object sender, ImageClickEventArgs e)
        {
            for (int i = 0; i < cbxNotUsers.Items.Count; i++)
            {
                cbxNotUsers.Items[i].Selected = !cbxNotUsers.Items[i].Selected;
            }
        }

        protected void btnCheckALL_Click(object sender, ImageClickEventArgs e)
        {
            for (int i = 0; i < cbxUsers.Items.Count; i++)
            {
                cbxUsers.Items[i].Selected = true;
            }

        }

        protected void btnCheckNot_Click(object sender, ImageClickEventArgs e)
        {
            for (int i = 0; i < cbxUsers.Items.Count; i++)
            {
                cbxUsers.Items[i].Selected = false;
            }

        }

        protected void btnCheck_Click(object sender, ImageClickEventArgs e)
        {
            for (int i = 0; i < cbxUsers.Items.Count; i++)
            {
                cbxUsers.Items[i].Selected = !cbxUsers.Items[i].Selected;
            }

        }


        #endregion


    }
}
