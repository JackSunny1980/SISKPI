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
    public partial class sisleft : System.Web.UI.Page
    {
        protected string strSysMenu;
        protected int RowCount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //XML
                //DataSet ds = new DataSet("ds1");
                //ds.ReadXml(Server.MapPath("~/XMLMenu/RootMenu.xml"));

                //SQLServer
                string strUserCode = "sisdemo";

                //获得login页面的Cookie值 要用Request
                HttpCookie newCookie = Request.Cookies["SISUser"];
                if (newCookie != null)
                {
                    strUserCode = newCookie.Values["UserCode"].ToString();
                }
                else
                {
                    string strjs = "<script language = javascript>window.open('Index.aspx','_top')</script>";

                    ClientScript.RegisterStartupScript(this.GetType(), "", strjs);
                }

                DataSet ds = KPI_MenuDal.GetTopMenusForUser(strUserCode);
                
                //开始
                DataTable dtbl = ds.Tables[0];

                RowCount = dtbl.Rows.Count;
                strSysMenu = "<menu>";
                int index = 0;

                if (dtbl.Rows.Count<=0)
                {
                    MessageBox.popupClientMessage(this.Page, "菜单初始化错误，请联系开发人员!", "call();");

                    strSysMenu += "<menubar id=\"" + "0" + "\" levelid=\"" + "0000" + "\" icon=\"Default/Images/Ico/" + "100.gif" + "\" name=\"" + "空菜单" + "\"></menubar>";
                    
                    strSysMenu += "</menu>";

                    return;
                }

                foreach (DataRow drow in dtbl.Rows)
                {
                    strSysMenu += "<menubar id=\"" + index.ToString() + "\" levelid=\"" + drow["MenuID"].ToString() + "\" icon=\"Default/Images/Ico/" + drow["MenuGIF"].ToString().Trim() + "\" name=\"" + drow["MenuName"].ToString().Trim() + "\"></menubar>";
                    index++;
                }

                strSysMenu += "</menu>";
            }
        }
    }
}
