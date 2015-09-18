using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Web.UI.HtmlControls;

using System.Text.RegularExpressions;

using SIS.Assistant;
using SIS.Loger;
using SIS.DBControl;
using SIS.DataAccess;
using SIS.DataEntity;

namespace SISKPI
{
    public partial class KPI_ECTrack : System.Web.UI.Page
    {
        //static DataTable gvTable;
        static Dictionary<string, double> dc = new Dictionary<string, double>();       

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //btnApply.Attributes.Add("onclick", "setDivPos('Lay1');Lay1.style.visibility=''; progress_update();");
                
                //获得参数集合
                if (Request.QueryString["ECID"] != null)
                {
                    ViewState["ECID"] = Request.QueryString["ECID"].ToString();
                }
                else
                {
                    ViewState["ECID"] = "";
                }

                tbx_ECCalcExp.Value = "";
                tbx_ECExpression.Value = "";


                BindValues();
            }
        }

        void BindGrid()
        {
            gvTag.DataSource = dc;

            gvTag.DataBind();
        }

        void BindValues()
        {
            string ECID = ViewState["ECID"].ToString();

            DataTable dt = ECSSSnapshotDal.GetOneRecord(ECID);

            if (dt.Rows.Count > 0)
            {
                //lblECCode.Text = "指标代码: " + 
                txtECCode.Value= dt.Rows[0]["ECCode"].ToString();
                txtECName.Value =dt.Rows[0]["ECName"].ToString();

                tbx_ECCalcExp.Value = dt.Rows[0]["ECCalcExp"].ToString();
                tbx_ECExpression.Value = dt.Rows[0]["ECExpression"].ToString();

            }

        }
               

        protected void gvTag_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //e.Row.Attributes.Add("onclick ", "if(window.oldtr!=null){window.oldtr.runtimeStyle.cssText='';}this.runtimeStyle.cssText= 'background-color:#e6c5fc';" +
                //     "window.oldtr=this; document.getElementById('txtIndex').value='" + e.Row.RowIndex.ToString() + "';");                
                //e.Row.Attributes["style"] = "cursor:hand";
                
                //鼠标移到效果
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");
            }
        }
        
        protected void gvTag_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvTag.EditIndex = -1;

            BindGrid();
        }

        protected void gvTag_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvTag.EditIndex = e.NewEditIndex;

            BindGrid();
        }

        protected void gvTag_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string sID = ((HtmlInputHidden)(gvTag.Rows[e.RowIndex].Cells[0].FindControl("key"))).Value.ToString().Trim();

            string svalue = ((TextBox)(gvTag.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim();
            double dv = double.MinValue;

            //"^[0-9]+$"
            if (!double.TryParse(svalue, out dv))
            {
                MessageBox.popupClientMessage(this.Page, "只能为数字！", "call();");
                return;
            }

            dc[sID] = dv;

            gvTag.EditIndex = -1;

            BindGrid();
        }
        
        protected void btnClose_Click(object sender, EventArgs e)
        {

            string strjs = "<script language=javascript>window.close();</script>";

            ClientScript.RegisterStartupScript(this.GetType(), "", strjs);
        }

    }
}
