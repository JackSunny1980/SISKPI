using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SIS.Loger;
using SIS.DataAccess;

namespace SISKPI
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["user"] != null)
                {
                    string user = Request.QueryString["user"].ToString();

                    RedirectMain(false, user, "");
                }
            }

        }

        protected void RedirectMain(bool bCheck, string strUserCode, string strUserPwd)
        {
            if (bCheck)
            {
                //创建一个新的Cookie
                HttpCookie newCookie = new HttpCookie("SISUser");

                //往Cookie里面添加值，均为键/值对。Cookie可以根据关键字寻找到相应的值
                newCookie.Values.Add("UserCode", strUserCode);
                newCookie.Values.Add("UserPwd", strUserPwd);

                if (CookieDate.Value == "0")
                {
                    newCookie.Expires = DateTime.Now.AddHours(1);
                }
                else if (CookieDate.Value == "1")
                {
                    newCookie.Expires = DateTime.Now.AddDays(1);
                }
                else if (CookieDate.Value == "2")
                {
                    newCookie.Expires = DateTime.Now.AddMonths(1);
                }
                else if (CookieDate.Value == "3")
                {
                    newCookie.Expires = DateTime.Now.AddYears(1);
                }


                //设置Session的值
                Session["UserCode"] = strUserCode;
                Session["UserPwd"] = strUserPwd;


                //Cookie的设置页面要用Response
                Response.AppendCookie(newCookie);

            }

            Response.Redirect("siskpi.aspx");
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //判断用户名及密码
            string strUserCode = usercode.Value.Trim();
            string strUserPassword = userpassword.Value.Trim();

            if (KPI_UserDal.CheckUserPassword(strUserCode, strUserPassword))
            {
                RedirectMain(true, strUserCode, strUserPassword);
            }
            else
            {
                userpassword.Value = "";

                MessageBox.popupClientMessage(this.Page, "用户不存在或密码错误，请重新填写！", "call();");
            }
            
        }

        protected void btnDemo_Click(object sender, EventArgs e)
        {
            //创建Cookies信息
            HttpCookie newCookie = new HttpCookie("SISUser");

            //往Cookie里面添加值，均为键/值对。
            //Cookie可以根据关键字寻找到相应的值。
            newCookie.Values.Add("UserCode", "sisdemo");
            //newCookie.Values.Add("UserPwd", "");

            //newCookie.Expires = DateTime.Now.AddDays(0);
            newCookie.Expires = DateTime.Now.AddHours(1);
            Response.AppendCookie(newCookie);


            Response.Redirect("siskpi.aspx");
        }
    }
}