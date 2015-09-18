using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.Loger
{
    public class MessageBox
    {
        public static void PopupMsgAndRedirect(System.Web.UI.Page p, string msg, string pageurl)
        {
            msg = msg.Replace("\n", "\\n").Replace("\"", "“").Replace("'", "‘");
            string s = "<script language=\"javascript\">";
            s += "alert('" + msg + "');";
            s += "window.open('" + pageurl;
            s += "','_self');</script>";

            p.Response.Write(s);
        }

        public static void SetFocus(System.Web.UI.Page p, System.Web.UI.Control c)
        {
            SetFocus(p, c, null);
        }
        public static void SetFocus(System.Web.UI.Page p, System.Web.UI.Control c, string formid)
        {
            string script = "<script language=\"javascript\">document." + ((formid == null || formid.Trim() == "") ? "form1" : formid) + "." + c.ClientID + ".value='';document." + ((formid == null || formid.Trim() == "") ? "form1" : formid) + "." + c.ClientID + ".focus();</script>";
            //p.RegisterStartupScript("SetFocus", script);
            p.ClientScript.RegisterStartupScript(p.GetType(), "SetFocus", script);
        }

        /// <summary>
        /// 弹出确认窗口，如果点击否则不执行操作
        /// </summary>
        /// <param name="btn">绑定的按钮</param>
        /// <param name="Message">提示信息</param>
        public static void popupConfirmMessage(System.Web.UI.WebControls.Button btn, string Message)
        {
            btn.Attributes.Add("OnClick", "javascript:window.event.returnValue=window.confirm('" + Message + "');");
        }
        /// <summary>
        /// 弹出确认窗口，如果点击否则不执行操作
        /// </summary>
        /// <param name="btn">绑定的按钮</param>
        /// <param name="Message">提示信息</param>
        public static void popupConfirmMessageHXH(System.Web.UI.WebControls.ImageButton btn, string Message)
        {
            btn.Attributes.Add("OnClick", "javascript:window.event.returnValue=window.confirm('" + Message + "');");
        }

        /// <summary>
        /// 弹出成功提示,背景界面不为空白
        /// </summary>
        public static void popupClientMessage(System.Web.UI.Page p, string Message)
        {
            Message = Message.Replace("\r", "\\r").Replace("\n", "\\n").Replace("\"", "“").Replace("'", "‘");
            string strScript = "<script language='javascript'>window.alert('" + Message + "');</script>";
            //p.RegisterStartupScript("successAlert", strScript);
            p.ClientScript.RegisterStartupScript(p.GetType(), "successAlert", strScript);
        }

        public static void popupClientMessage(System.Web.UI.Page p, string Message,string runScript)
        {
            Message = Message.Replace("\r", "\\r").Replace("\n", "\\n").Replace("\"", "“").Replace("'", "‘");
            string strScript = "<script language='javascript'>"+runScript+"window.alert('" + Message + "');</script>";
            //p.RegisterStartupScript("successAlert", strScript);
            p.ClientScript.RegisterStartupScript(p.GetType(), "successAlert", strScript);
        }

        /// <summary>
        /// 弹出窗口
        /// </summary>
        /// <param name="url"></param>
        public static void popupWindow(System.Web.UI.Page p, string url)
        {
            string strScript = "<script language='javascript'>window.open('" + url + "','_blank','location=0,directories=0,menubar=0,scrollbars=1,resizable =1,toolbar=0');</script>";
            //if (p.IsStartupScriptRegistered("openwindow") == false)
            if (p.ClientScript.IsStartupScriptRegistered("openwindow") == false) 
            {
                //p.RegisterStartupScript("openwindow", strScript);
                p.ClientScript.RegisterStartupScript(p.GetType(), "successAlert", strScript);
            }
        }

        /// <summary>
        /// 弹出窗口并关闭当前窗口
        /// </summary>
        /// <param name="url"></param>
        public static void popupNewWindow(System.Web.UI.Page p, string url)
        {
            string strScript = "<script language='javascript'>window.open('" + url + "','_blank','location=0,directories=0,menubar=0,scrollbars=1,resizable =1,toolbar=0');window.close();</script>";
            //if (p.IsStartupScriptRegistered("openwindow") == false)
            if (p.ClientScript.IsStartupScriptRegistered("openwindow") == false)
            {
                //p.RegisterStartupScript("openwindow", strScript);
                p.ClientScript.RegisterStartupScript(p.GetType(), "successAlert", strScript);
            }
        }

        /// <summary>
        /// 刷新父窗口并关闭当前窗口
        /// </summary>
        /// <returns></returns>
        /// add by yueweihang
        public static void RefreshOpenerAndClose(System.Web.UI.Page p)
        {
            StringBuilder script = new StringBuilder();
            script.Append("<SCRIPT type=\"text/javascript\">\r\n");
            script.Append("window.returnValue='refresh';window.close();");
            script.Append("</SCRIPT>");
            //p.RegisterStartupScript("refclose", script.ToString());
            p.ClientScript.RegisterStartupScript(p.GetType(), "refclose", script.ToString());
        }

        //ASP.NET 使用alert弹出对话框后，CSS样式失效，字体变大的解决方法
        //弹出提示窗口用的Response.Write("<script>alert('删除成功');</script>")，不知道什么原因不能用了，字体变大。

        public static void WebMessageBox(System.Web.UI.Page page, string values)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "", "<script language=javascript>alert('" + values + "')</script>");
        }
    }
}
