using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace SISKPI{
    public partial class ContentDetailMasterPage : System.Web.UI.MasterPage {
        #region 属性
        /// <summary>
        /// 返回虚拟目录路径
        /// </summary>
        public string ApplicationPath {
            get {
                string applicationPath = HttpContext.Current.Request.ApplicationPath;
                if (applicationPath == "/") {
                    return string.Empty;
                }
                else {
                    return applicationPath;
                }
            }
        }

        #endregion

        #region 重写方法

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            if (!IsPostBack) {
                //RegisterStyleInclude("css/Default.css");
                //RegisterScriptInclude("js/jquery-1.8.3.js");
                //RegisterScriptInclude("js/jquery-ui-1.9.2.custom.js");
                //RegisterScriptInclude("js/common.js");
                //RegisterScriptInclude("js/InitialInput.js");
                //RegisterScriptInclude("js/DatePicker/WdatePicker.js");
                ClientInitial();
            }
        }

        #endregion

        #region 受保护方法

        protected void RegisterScriptInclude(string filePath) {
            HtmlGenericControl jsRefControl = new HtmlGenericControl();
            jsRefControl.TagName = "script";
            jsRefControl.Attributes.Add("type", "text/javascript");
            jsRefControl.Attributes.Add("src", ApplicationPath + "/" + filePath);
            //Page.Header.Controls.Add(jsRefControl);
            head1.Controls.Add(jsRefControl);
        }

        protected void RegisterScriptBlock(string javascript) {
            HtmlGenericControl jsControl = new HtmlGenericControl();
            jsControl.TagName = "script";
            jsControl.Attributes.Add("type", "text/javascript");
            jsControl.InnerHtml = javascript;
            //Page.Header.Controls.Add(jsControl);
            head1.Controls.Add(jsControl);
        }


        protected void RegisterStyleInclude(string cssFile) {
            HtmlGenericControl cssControl = new HtmlGenericControl();
            cssControl.TagName = "link";
            cssControl.Attributes.Add("type", "text/css");
            cssControl.Attributes.Add("rel", "stylesheet");
            cssControl.Attributes.Add("href", ApplicationPath + "/" + cssFile);
            //Page.Header.Controls.Add(cssControl);
            head1.Controls.Add(cssControl);
        }

        #endregion

        #region 私有方法

        private void ClientInitial() {
            String scriptFmt = "<script type=\"text/javascript\" src=\"{0}\"></script>";
            //String StyleFmt = "<link href=\"{0}\" rel=\"stylesheet\" type=\"text/css\" />";

            String script = ApplicationPath + "/js/jquery-1.8.3.js";
            lblScript1.Text = String.Format(scriptFmt, script);

            script = ApplicationPath + "/js/jquery-ui-1.9.2.custom.js";
            lblScript2.Text = String.Format(scriptFmt, script);

            script = ApplicationPath + "/js/InitialInput.js";
            lblScript3.Text = String.Format(scriptFmt, script);

            script = ApplicationPath + "/js/common.js";
            lblScript4.Text = String.Format(scriptFmt, script);

            script = ApplicationPath + "/js/DatePicker/WdatePicker.js";
            lblScript5.Text = String.Format(scriptFmt, script);

            script = ApplicationPath + "/js/jquery.validationEngine-zh_CN.js";
            lblScript6.Text = String.Format(scriptFmt, script);

            script = ApplicationPath + "/js/jquery.validationEngine.js";
            lblScript7.Text = String.Format(scriptFmt, script);

        }

        #endregion
    }
}