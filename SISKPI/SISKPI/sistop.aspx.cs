using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using SIS.DataAccess;
using SIS.Loger;

namespace SISKPI
{
    public partial class sistop : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnSetFirst.Attributes.Add("onclick", "setMainURL();");

                //获得login页面的Cookie值 要用Request
                HttpCookie newCookie = Request.Cookies["SISUser"];
                if (newCookie != null)
                {
                    btnPassword.Text = "欢迎您：" + newCookie.Values["UserCode"].ToString();
                    btnLog.Text = "注销";
                }else
                {
                    string strjs = "<script language = javascript>window.open('Index.aspx','_top')</script>";

                    ClientScript.RegisterStartupScript(this.GetType(), "", strjs);

                }
            }

        }

        protected void btnFirst_Click(object sender, EventArgs e)
        { 
            string strUserCode = "sisdemo";

            //获得login页面的Cookie值 要用Request
            HttpCookie newCookie = Request.Cookies["SISUser"];
            if (newCookie != null)
            {
                strUserCode = newCookie.Values["UserCode"].ToString();
            }
            
            string strUrl = KPI_UserDal.GetUserURL(strUserCode);
            if (strUrl == "")
            {
                return;
            }

            Response.Write("<script language=javascript>parent.parent.main.location.href='"+ strUrl +"';</script>");
        }
        
        protected void btnSetFirst_Click(object sender, EventArgs e)
        {        
            string strUserCode = "sisdemo";
    
            HttpCookie newCookie = Request.Cookies["SISUser"];
            if (newCookie != null)
            {
                strUserCode = newCookie.Values["UserCode"].ToString();
            }
            

            string strUserURL = txtMainURL.Value;

            if (strUserURL == "")
            {
                return;
            }

            if (KPI_UserDal.SetUserURL(strUserCode, strUserURL))
            {
                MessageBox.popupClientMessage(this.Page, "首页设置成功!", "call();");
            }


        }

        protected void btnLog_Click(object sender, EventArgs e)
        {
            HttpCookie newCookie = Request.Cookies["SISUser"];
            if (newCookie != null)
            {
                newCookie.Expires = DateTime.Now.AddHours(-1);

                Response.AppendCookie(newCookie);

            }

            string strjs = "<script language = javascript>window.open('Index.aspx','_top')</script>";

            ClientScript.RegisterStartupScript(this.GetType(), "", strjs);
        }

        protected void btnPassword_Click(object sender, EventArgs e)
        {
            //弹出编辑密码页面
            HttpCookie newCookie = Request.Cookies["SISUser"];
            string strUser = "";
            if (newCookie != null)
            {
                strUser =  newCookie.Values["UserCode"].ToString();
            }

            if (strUser != "" && strUser != "sisdemo")
            {
                string strjs = "<script language=javascript>window.open('../SISKPI/KPI_SubUserPassword.aspx?usercode=" + strUser + "','newwindow','width=600,height=500')</script>";

                ClientScript.RegisterStartupScript(this.GetType(), "", strjs);
            }
            

        }


    }
}
