using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web.UI.HtmlControls;

using SIS.DBControl;
using SIS.Loger;
using SIS.Assistant;

using SIS.DataAccess;
using SIS.DataEntity;


namespace SISKPI
{
    public partial class KPI_SubWeb : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //指标信息
                string WebCode = "";
                if (Request.QueryString["webcode"] != null)
                {
                    WebCode = Request.QueryString["webcode"].ToString();
                }
                else
                {
                    WebCode = "";
                }
                lblInfor.Text = WebCode;

                //初始化指标集合
                DataTable dt = ECTagDal.GetKPIs("");
                this.ECCode.DataSource = dt;
                this.ECCode.DataBind();

                //绑定名称
                BindTextBox(ECCode.SelectedValue);

                SISKPI.KPIDataSetTableAdapters.GetCalcTypeTableAdapter a = new SISKPI.KPIDataSetTableAdapters.GetCalcTypeTableAdapter();
                this.KeyCalcType.DataSource = a.GetData();
                this.KeyCalcType.DataBind();


                //类型集合
                BindKeys(WebCode);
            }
            
        }


        public void BindKeys(string WebCode)
        {

            DataTable dt = KPI_WebKeyDal.GetKeyList(WebCode);
            gvData.DataSource = dt;
            gvData.DataBind();
        }

        public void BindTextBox(string ECCode)
        {
            string[] strnt = ECTagDal.GetNameEngunit(ECCode).Split(new string[] { ",", ";" }, StringSplitOptions.RemoveEmptyEntries);
            if (strnt.Length == 2)
            {
                this.ECName.Text = strnt[0];
                this.KeyEngunit.Text = strnt[1];
            }
            else if (strnt.Length == 1)
            {
                this.ECName.Text = strnt[0];
                this.KeyEngunit.Text = "";
            }
            else
            {
                this.ECName.Text = "";
                this.KeyEngunit.Text = "";
            }
        }
        
        #region GridView Key

        protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string sKeyID = gvData.DataKeys[e.Row.RowIndex]["KeyID"].ToString();
                string sWebCode = gvData.DataKeys[e.Row.RowIndex]["WebCode"].ToString();
                string sECCode = gvData.DataKeys[e.Row.RowIndex]["ECCode"].ToString();
                string sECName = gvData.DataKeys[e.Row.RowIndex]["ECName"].ToString();
                string sKeyEngunit = gvData.DataKeys[e.Row.RowIndex]["KeyEngunit"].ToString();
                string sKeyCalcType = gvData.DataKeys[e.Row.RowIndex]["KeyCalcType"].ToString();
                string sKeyIndex = gvData.DataKeys[e.Row.RowIndex]["KeyIndex"].ToString();
                e.Row.Attributes.Add("id", sKeyID + "," + sWebCode + "," + sECCode + "," + sECName + "," + sKeyEngunit + "," + sKeyCalcType + "," + sKeyIndex);


                foreach (ListItem i in KeyCalcType.Items)
                {
                    if (i.Value == e.Row.Cells[4].Text.ToString())
                    {
                        e.Row.Cells[4].Text = i.Text;
                        break;
                    }
                }
            }
        }

        #endregion

    }


}