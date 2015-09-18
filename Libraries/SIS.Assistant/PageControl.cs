using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web.UI;

namespace SIS.Assistant
{
    public class PageControl
    {
        /// <summary>
        /// Ajax返回值到客户端
        /// </summary>
        /// <param name="val"></param>
        public void ResonseToClient(System.Web.UI.Page page,string val)
        {
            if (val.Equals("")) return;
            page.Response.Clear();

            page.Response.CacheControl = "no-cache";
            page.Response.Expires = -1;
            page.Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));

            page.Response.Write(val);
            page.Response.End();
        }

        /// <summary>
        /// 获得控件的HTML代码
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public string GetControlHTML(System.Web.UI.Control control)
        {
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            control.RenderControl(hw);

            return sw.ToString();
        }

        public static string  GetGuid()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
