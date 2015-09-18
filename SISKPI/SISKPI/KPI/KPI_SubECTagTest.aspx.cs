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
    public partial class KPI_SubECTagTest : System.Web.UI.Page
    {
        //static DataTable gvTable;
        static Dictionary<string, double> dc = new Dictionary<string, double>();
        static string expression;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //btnApply.Attributes.Add("onclick", "setDivPos('Lay1');Lay1.style.visibility=''; progress_update();");

                //判断是否新建或编辑
                expression = SISKPI.KPI_SubECTagConfig2.eccalcexp;
                tbx_Exp.Text = expression;

                //if (Request.QueryString["exp"] != null)
                //{
                //    expression = Request.QueryString["exp"].ToString();

                //    
                //}

                //dc 
                //dc.Add("key 1", 1);
                //dc.Add("key 2", 2);
                //dc.Add("key 3", 3);
                //dc.Add("key 4", 4);
                //dc.Add("key 5", 5);

                ExpVal();

                BindGrid();
            }
        }

        void BindGrid()
        {
            gvTag.DataSource = dc;

            gvTag.DataBind();
        }

        void ExpVal()
        {
            expression = tbx_Exp.Text;

            if (expression.Length > 0)
            {
                ///标签及指标解析
				ExpDone Parser = new ExpDone();
				if (Parser.ExpEvaluate(expression, ref dc) == 0)
                {
                    tbx_Result.BackColor = Color.Green;
                    tbx_Result.Text = "标签及指标解析正确";
                }
                else
                {
                    tbx_Result.BackColor = Color.Red;
                    tbx_Result.Text = "标签及指标解析不正确";
                }
            }
        }

        void ExpCalc()
        {
            //
            double result = double.MinValue;
            string strresult = "";
            string strexpression = "";
			ExpDone parser = new ExpDone();
			if (parser.ExpCalculate(expression, dc, out result, out strresult, out strexpression) == 0)
            {
                tbx_Result.BackColor = Color.Empty;
                tbx_Result.Text =expression+ ":  " + strexpression + " = "+strresult;
            }
            else
            {
                tbx_Result.BackColor = Color.Red;
                tbx_Result.Text = strresult;
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

        protected void btnEval_Click(object sender, EventArgs e)
        {
            ExpVal();

            BindGrid();
        }
 

        protected void btnCalc_Click(object sender, EventArgs e)
        {
            ExpCalc();
        }
 
        //protected void btnClose_Click(object sender, EventArgs e)
        //{

        //    string strjs = "<script language=javascript>window.close();</script>";

        //    ClientScript.RegisterStartupScript(this.GetType(), "", strjs);
        //}

    }
}
