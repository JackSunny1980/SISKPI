using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;

namespace SISKPI {
    /// <summary>
    /// 所用用户控件的基类
    /// </summary>
    public class BaseUserControl : UserControl {

        #region 属性

        protected bool CanEditData {
            get {
                return true;
            }
        }

        #endregion

        #region 虚方法

        public void ShowMessage(string message) {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Information", "alert('" + message + "')", true);
        }

        #endregion
    }
}
