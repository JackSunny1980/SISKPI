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
    public partial class KPI_SubUserPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {               
                btnConfirm.Attributes.Add("onclick", "javascript:return confirm('确认无误吗？');");

                if (Request.QueryString["usercode"] != null)
                {
                    string usercode = Request.QueryString["usercode"].ToString();

                    BindValues(usercode);
                }

            }
        }

        void BindValues(string usercode)
        {
            string userid = KPI_UserDal.GetUserID(usercode);

            ViewState["userid"] = userid;

            KPI_UserEntity usEntity = KPI_UserDal.GetEntity(userid);
                        
            tbxUserCode.Text = usEntity.UserCode;
            tbxUserName.Text = usEntity.UserName;
            //tbxUserPassword.Text = usEntity.UserPassword;
            //tbxUserP1.Text = "";
            //tbxUserP2.Text = "";
            tbxUserEMail.Text = usEntity.UserEMail;
            tbxUserPhone.Text = usEntity.UserPhone;
            tbxUserTitle.Text = usEntity.UserTitle;                     

        }
        
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            string userid = ViewState["userid"].ToString();
            string usercode = tbxUserCode.Text.Trim();

            //是否特殊用户sisdemo不能修改；sisadmin可以修改
            if (usercode.ToUpper() == "SISDEMO")
            {
                MessageBox.popupClientMessage(this.Page, "无权修改sisdemo用户！", "call();");
                return;
            }

            //不能为空
            if (tbxUserP1.Text.Trim()=="")
            {
                tbxUserPassword.Text = "";
                tbxUserP1.Text = "";
                tbxUserP2.Text = "";

                MessageBox.popupClientMessage(this.Page, "新密码不能为空！", "call();");
                return;

            }

            string username = tbxUserName.Text.Trim();

            string oldpassword = KPI_UserDal.GetUserPassword(userid);
            string userpassword = KPI_UserDal.GetDESString(tbxUserPassword.Text.Trim());
            
            string newpassword = KPI_UserDal.GetDESString(tbxUserP1.Text.Trim());

            string userp1 = tbxUserP1.Text.Trim();
            string userp2 = tbxUserP2.Text.Trim();
            string useremail = tbxUserEMail.Text.Trim();
            string userphone = tbxUserPhone.Text.Trim();
            string usertitle = tbxUserTitle.Text.Trim();

            //判断用户是否为空
            if (username == "")
            {
                tbxUserPassword.Text = "";
                tbxUserP1.Text = "";
                tbxUserP2.Text = "";

                MessageBox.popupClientMessage(this.Page, "用户姓名都不能空！", "call();");
                return;
            }

            //判断旧密码是否正确     
            if (oldpassword != userpassword)
            {
                tbxUserPassword.Text = "";
                tbxUserP1.Text = "";
                tbxUserP2.Text = "";

                MessageBox.popupClientMessage(this.Page, "旧密码不正确！", "call();");
                return;
            }

            //判断新密码是否一致
            if (userp1 != userp2)
            {
                tbxUserPassword.Text = "";
                tbxUserP1.Text = "";
                tbxUserP2.Text = "";

                MessageBox.popupClientMessage(this.Page, "新密码不一致！", "call();");
                return;
            }

            //判断新旧密码是否一致
            if (oldpassword == newpassword)
            {
                tbxUserPassword.Text = "";
                tbxUserP1.Text = "";
                tbxUserP2.Text = "";

                MessageBox.popupClientMessage(this.Page, "新密码不一致！", "call();");
                return;
            }

            KPI_UserEntity usEntity = new KPI_UserEntity();
            
            usEntity.UserID = userid;
            usEntity.UserName = username;
            usEntity.UserPassword = newpassword;
            usEntity.UserEMail = useremail;
            usEntity.UserPhone = userphone;
            usEntity.UserTitle = usertitle;


            if (KPI_UserDal.Update(usEntity))
            {
                MessageBox.popupClientMessage(this.Page, "修改成功！", "call();");
            }
            else
            {
                MessageBox.popupClientMessage(this.Page, "修改错误！", "call();");
            }

            return;

        }
               
    }
}
