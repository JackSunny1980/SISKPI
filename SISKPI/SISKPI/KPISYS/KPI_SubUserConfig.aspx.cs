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
    public partial class KPI_SubUserConfig : System.Web.UI.Page
    {
        public static DataTable dtGroups = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //btnClear.Attributes.Add("onclick", "setDivPos('Lay1');Lay1.style.visibility='';progress_update();");

                btnConfirm.Attributes.Add("onclick", "javascript:return confirm('确认无误吗？');");

                string opr = Request.QueryString["opr"].ToString();

                if (opr == "edit")
                {
                    btnConfirm.Text = "保 存";

                    tbxUserCode.Enabled = false;

                    trold.Style.Add("display", "none");

                    string userid = Request.QueryString["userid"].ToString();

                    ViewState["userid"] = userid;

                    BindValues(userid);
                }
                else if (opr == "add")
                {
                    btnConfirm.Text = "增 加";

                    ViewState["userid"] = "";
                }


                //权限集合
                //dtGroups For Menu
                dtGroups = GroupDal.GetGroupsForUser();

                ViewState["opr"] = opr;
            }
        }

        void BindValues(string userid)
        {
            KPI_UserEntity usEntity = KPI_UserDal.GetEntity(userid);
            
            tbxUserCode.Text = usEntity.UserCode;
            tbxUserName.Text = usEntity.UserName;
            //tbxUserPassword.Text = usEntity.UserPassword;
            //tbxUserP1.Text = "";
            //tbxUserP2.Text = "";
            tbxUserEMail.Text = "";
            tbxUserPhone.Text = "";
            tbxUserTitle.Text = "";


            lblUserGroups.Text = "";            

            //gvUser.DataSource = dt;

            //gvUser.DataBind();
        }
        
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            string opr = Request.QueryString["opr"].ToString();

            string userid = ViewState["userid"].ToString();

            string usercode = tbxUserCode.Text.Trim(); 
            string username = tbxUserName.Text.Trim();
            //string useremail = tbxUserEMail.Text.Trim();
            //string userphone = tbxUserPhone.Text.Trim();
            //string usertitle = tbxUserTitle.Text.Trim();

            //判断用户是否为空
            if (usercode == "" || username == "")
            {
                MessageBox.popupClientMessage(this.Page, "用户代码或用户姓名不能为空！", "call();");
                return;
            }    
            
            if (opr == "edit")
            {
                //edit
                UserUpdate();
            }
            else   
            {
                //add 
                UserInsert();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            tbxUserCode.Text = "";
            tbxUserName.Text = "";

            tbxUserEMail.Text = "";
            tbxUserPhone.Text = "";
            tbxUserTitle.Text = "";

            lblUserGroups.Text = "";

            //Response.Redirect("KPI_User.aspx");
        }

        protected bool UserUpdate()
        {
            string userid = ViewState["userid"].ToString();

            KPI_UserEntity usEntity = new KPI_UserEntity();

            usEntity.UserID = userid;

            usEntity.UserName = tbxUserCode.Text.Trim();
            usEntity.UserEMail = tbxUserEMail.Text.Trim();
            usEntity.UserPhone = tbxUserPhone.Text.Trim();
            usEntity.UserTitle = tbxUserTitle.Text.Trim();

            usEntity.UserGroups = lblUserGroups.Text.Trim();

            usEntity.UserIsValid = int.Parse(rblUserIsValid.SelectedValue);

            if (KPI_UserDal.Update(usEntity))
            {
                MessageBox.popupClientMessage(this.Page, "修改成功！", "call();");
                return true;
            }
            else
            {
                MessageBox.popupClientMessage(this.Page, "修改失败！", "call();");
                return false;

            }

        }

        protected bool UserInsert()
        {
            KPI_UserEntity usEntity = new KPI_UserEntity();

            usEntity.UserID = Guid.NewGuid().ToString();
            usEntity.UserCode = tbxUserCode.Text.Trim();
            usEntity.UserName = tbxUserName.Text.Trim();
            usEntity.UserPassword = KPI_UserDal.GetDESString("123456");
            usEntity.UserEMail = tbxUserEMail.Text.Trim();
            usEntity.UserPhone = tbxUserPhone.Text.Trim();
            usEntity.UserTitle = tbxUserTitle.Text.Trim();

            usEntity.UserGroups = lblUserGroups.Text.Trim();

            usEntity.UserIsValid = int.Parse(rblUserIsValid.SelectedValue);

            if (KPI_UserDal.Insert(usEntity))
            {
                MessageBox.popupClientMessage(this.Page, "添加成功！新用户的初始密码为:123456", "call();");
                return true;
            }
            else
            {
                MessageBox.popupClientMessage(this.Page, "添加失败！", "call();");
                return false;

            }
            
        }

        protected void btnUserGroups_Click(object sender, EventArgs e)
        {
            string strjs = "<script language=javascript>window.open('KPI_SubGroupSelect.aspx?mode=user','newwindow','width=400,height=500')</script>";

            ClientScript.RegisterStartupScript(this.GetType(), "", strjs);
        }

       
    }
}
