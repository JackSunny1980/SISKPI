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

using System.IO;
using System.Text;

using SIS.DataAccess;
using SIS.Loger;

namespace SISKPI
{
    public partial class sismenutree : System.Web.UI.Page
    {
        protected string SysMenu;
        private DataTable dtblMenu;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["ID"] != null && Request.Params["ID"].Trim() != "")
            {
                DataSet ds = new DataSet();
                ////////////////////////////////////////////////////////////////////////
                //1. readxml
                //ds.ReadXml(Server.MapPath("~/XMLMenu/SubMenu.xml"));

                //2. StreamReader
                //string strXmlPath = Server.MapPath("~/XMLMenu/SubMenu.xml");
                //StreamReader sr = new StreamReader(strXmlPath, Encoding.Default);
                //ds.ReadXml(sr);
                //sr.Close();

                //3.SQLServer

                string strUserCode = "";
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

                string strParentID = Request.Params["ID"].ToString();

                ds = KPI_MenuDal.GetLeafMenus(strUserCode);
                ////////////////////////////////////////////////////////////////////////

                dtblMenu = ds.Tables[0];
                //int ID = int.Parse(Request.Params["ID"]);
                //SysMenu = getMenu(ID);


                if (dtblMenu.Rows.Count <= 0)
                {
                    MessageBox.popupClientMessage(this.Page, "菜单初始化错误，请联系开发人员!", "call();");

                    SysMenu = getEmptyMenu(strParentID);

                    return;
                }

                SysMenu = getMenu(strParentID);

                //打开首页
                string strUrl = KPI_UserDal.GetUserURL(strUserCode);
                if (strUrl == "")
                {
                    return;
                }

                Response.Write("<script language=javascript>parent.parent.main.location.href='" + strUrl + "';</script>");


            }
        }

        //private string getMenu(int ParentID)
        private string getMenu(string ParentID)
        {
            string strSysMenu = "";
            foreach (DataRow drow in dtblMenu.Select("MenuParentID='" + ParentID + "'"))
            {
                strSysMenu += "['<img src=\"Default/Images/Ico/" + drow["MenuGIF"].ToString().Trim() + "\">','" + drow["MenuName"].ToString().Trim() + "','" + drow["MenuURL"].ToString().Trim() + "','main','" + drow["MenuName"].ToString().Trim() + "'";
                
                string strSub = GetSubMenu(drow["MenuID"].ToString());
                if (strSub == "")
                    strSysMenu += "],";
                else
                {
                    strSysMenu += "," + strSub + "],";
                }
            }
            if (strSysMenu.Length > 0)
                strSysMenu = strSysMenu.Substring(0, strSysMenu.Length - 1);
            else
            {
                strSysMenu += "['<img src=\"Default/Images/Ico/101.gif\">','暂无菜单','PageBar.aspx','main','暂无菜单']";
            }

            return strSysMenu;
        }

        private string GetSubMenu(string ParentID)
        {
            string strSub = "";
            foreach (DataRow drow in dtblMenu.Select("MenuParentID='" + ParentID + "'"))
            {
                strSub += "['<img src=\"Default/Images/Ico/" + drow["MenuGIF"].ToString().Trim() + "\">','" + drow["MenuName"].ToString().Trim() + "','" + drow["MenuURL"].ToString().Trim() + "','main','" + drow["MenuName"].ToString().Trim() + "'";
                string strSubSub = GetSubMenu(drow["MenuID"].ToString());
                if (strSubSub == "")
                    strSub += "],";
                else
                {
                    strSub += "," + strSubSub + "],";
                }
            }

            if (strSub == "")
                return strSub;
            else
            {
                strSub = strSub.Substring(0, strSub.Length - 1);
                return strSub;
            }
        }

        private string getEmptyMenu(string ParentID)
        {
            string strSysMenu = "['<img src=\"Default/Images/Ico/" + "101.gif" + "\">','" + "空菜单" + "','" + "sisintro.htm" + "','main','" + "空菜单" + "'";

            strSysMenu += "],";

            if (strSysMenu.Length > 0)
                strSysMenu = strSysMenu.Substring(0, strSysMenu.Length - 1);
            else
            {
                strSysMenu += "['<img src=\"Default/Images/Ico/101.gif\">','空菜单','sisintro.htm','main','空菜单']";
            }

            return strSysMenu;
        }
    }
}
