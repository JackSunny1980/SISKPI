using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.Loger
{
    public class MessageBox
    {
        public static void PopupMsgAndRedirect(System.Web.UI.Page p, string msg, string pageurl)
        {
            msg = msg.Replace("\n", "\\n").Replace("\"", "��").Replace("'", "��");
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
        /// ����ȷ�ϴ��ڣ�����������ִ�в���
        /// </summary>
        /// <param name="btn">�󶨵İ�ť</param>
        /// <param name="Message">��ʾ��Ϣ</param>
        public static void popupConfirmMessage(System.Web.UI.WebControls.Button btn, string Message)
        {
            btn.Attributes.Add("OnClick", "javascript:window.event.returnValue=window.confirm('" + Message + "');");
        }
        /// <summary>
        /// ����ȷ�ϴ��ڣ�����������ִ�в���
        /// </summary>
        /// <param name="btn">�󶨵İ�ť</param>
        /// <param name="Message">��ʾ��Ϣ</param>
        public static void popupConfirmMessageHXH(System.Web.UI.WebControls.ImageButton btn, string Message)
        {
            btn.Attributes.Add("OnClick", "javascript:window.event.returnValue=window.confirm('" + Message + "');");
        }

        /// <summary>
        /// �����ɹ���ʾ,�������治Ϊ�հ�
        /// </summary>
        public static void popupClientMessage(System.Web.UI.Page p, string Message)
        {
            Message = Message.Replace("\r", "\\r").Replace("\n", "\\n").Replace("\"", "��").Replace("'", "��");
            string strScript = "<script language='javascript'>window.alert('" + Message + "');</script>";
            //p.RegisterStartupScript("successAlert", strScript);
            p.ClientScript.RegisterStartupScript(p.GetType(), "successAlert", strScript);
        }

        public static void popupClientMessage(System.Web.UI.Page p, string Message,string runScript)
        {
            Message = Message.Replace("\r", "\\r").Replace("\n", "\\n").Replace("\"", "��").Replace("'", "��");
            string strScript = "<script language='javascript'>"+runScript+"window.alert('" + Message + "');</script>";
            //p.RegisterStartupScript("successAlert", strScript);
            p.ClientScript.RegisterStartupScript(p.GetType(), "successAlert", strScript);
        }

        /// <summary>
        /// ��������
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
        /// �������ڲ��رյ�ǰ����
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
        /// ˢ�¸����ڲ��رյ�ǰ����
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

        //ASP.NET ʹ��alert�����Ի����CSS��ʽʧЧ��������Ľ������
        //������ʾ�����õ�Response.Write("<script>alert('ɾ���ɹ�');</script>")����֪��ʲôԭ�������ˣ�������

        public static void WebMessageBox(System.Web.UI.Page page, string values)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "", "<script language=javascript>alert('" + values + "')</script>");
        }
    }
}
