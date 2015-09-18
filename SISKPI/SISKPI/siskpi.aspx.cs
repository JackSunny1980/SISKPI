using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
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
    public partial class siskpi : System.Web.UI.Page
    {
        private static DataTable dtMenus = null;
        private static Dictionary<int, string> dcMenus = new Dictionary<int, string>();

        private static List<Button> ltButton = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //btnRealKPI.Attributes.Add("onclick", "setURL('sispitrend.htm');");
                //btnKPI.Attributes.Add("onclick", "setURL('sisintro.htm');");

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

                //
                dtMenus = KPI_MenuDal.GetMenus("").Tables[0];

                if (dtMenus != null && dtMenus.Rows.Count > 0)
                {

                    for (int i = 0; i < dtMenus.Rows.Count; i++)
                    {
                        dcMenus[i] = dtMenus.Rows[i]["MenuURL"].ToString();
                    }
                }

            }

            if (dtMenus != null && dtMenus.Rows.Count > 0)
            {
                ltButton = new List<Button>();

                for (int i = 0; i < dtMenus.Rows.Count; i++)
                {
                    Button btd = new Button();
                    btd.Width = 110;
                    btd.Height = 40;
                    //btd.BorderColor=System.Drawing.Color
                    btd.Text = dtMenus.Rows[i]["MenuName"].ToString();
                    btd.ID = i.ToString();
                    if (i == 0)
                    {
                        btd.CssClass = "lblstyle1";
                    }
                    else
                    {
                        btd.CssClass = "lblstyle2";
                    }


                    btd.Click += new System.EventHandler(BTN_Click);

                    ltButton.Add(btd);

                    pButton.Controls.Add(btd);
                }
            }

        }

        protected void BTN_Click(object sender, EventArgs e)
        {            
            Button btnTemp = (Button)sender;

            for (int i = 0; i < ltButton.Count; i++)
            {
                if (i == int.Parse(btnTemp.ID))
                {
                    btnTemp.CssClass = "lblstyle1";
                    kpiInfo.Attributes.Add("src", dcMenus[i]);
                }
                else
                {
                    ltButton[i].CssClass = "lblstyle2";

                }
            }

            ////btnTemp.BackColor = System.Drawing.Color.Gold;

            //int i = int.Parse(btnTemp.ID);

            //kpiInfo.Attributes.Add("src", dcMenus[i]);
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
        
        //protected void btnSetFirst_Click(object sender, EventArgs e)
        //{        
        //    string strUserCode = "sisdemo";
    
        //    HttpCookie newCookie = Request.Cookies["SISUser"];
        //    if (newCookie != null)
        //    {
        //        strUserCode = newCookie.Values["UserCode"].ToString();
        //    }
            

        //    string strUserURL = txtMainURL.Value;

        //    if (strUserURL == "")
        //    {
        //        return;
        //    }

        //    if (KPI_UserDal.SetUserURL(strUserCode, strUserURL))
        //    {
        //        MessageBox.popupClientMessage(this.Page, "首页设置成功!", "call();");
        //    }


        //}

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

        //protected void btnRealKPI_Click(object sender, EventArgs e)
        //{
        //    kpiInfo.Attributes.Add("src", "KPIWeb\\KPI_RealList.aspx"); 

        //}

        //protected void btnXZBKPI_Click(object sender, EventArgs e)
        //{
        //    kpiInfo.Attributes.Add("src", "KPIWeb\\KPI_XZB.aspx?ecweb=xzb"); 

        //}

        //protected void btnScore_Click(object sender, EventArgs e)
        //{
        //    kpiInfo.Attributes.Add("src", "KPIWeb\\KPI_Score.aspx");
        //}

        //protected void btnMoney_Click(object sender, EventArgs e)
        //{
        //    kpiInfo.Attributes.Add("src", "KPIWeb\\KPI_Money.aspx");

        //}

        //protected void btnReport_Click(object sender, EventArgs e)
        //{

        //    kpiInfo.Attributes.Add("src", "KPI_Report.aspx");

        //    //string strjs = "<script language=javascript>window.open('ReportNavigator.aspx')</script>";

        //    //ClientScript.RegisterStartupScript(this.GetType(), "", strjs);

        //}

        //protected void btnKPISystem_Click(object sender, EventArgs e)
        //{

        //    kpiInfo.Attributes.Add("src", "KPI_Management.aspx");

        //    //string strjs = "<script language=javascript>window.open('KPINavigator.aspx')</script>";

        //    //ClientScript.RegisterStartupScript(this.GetType(), "", strjs);

        //}



    }
}
