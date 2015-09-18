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
    public partial class KPI_Average : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //指标信息
                //if (Request.QueryString["webcode"] != null)
                //{
                //    WebCode = Request.QueryString["webcode"].ToString();
                //}
                //else
                //{
                //    WebCode = "";
                //}

                //初始化指标集合
                DataTable dt = ECTagDal.GetKPIs("");
                ECCode.DataSource = dt;
                ECCode.DataBind();

                dt = CurveTagDal.GetCurvesByCode();
                KeyTarget1.Items.Add(new ListItem("", "NOTAG"));
                KeyTarget2.Items.Add(new ListItem("", "NOTAG"));
                foreach (DataRow dr in dt.Rows)
                {
                    KeyTarget1.Items.Add(new ListItem(dr["Name"].ToString(), dr["Code"].ToString()));
                    KeyTarget2.Items.Add(new ListItem(dr["Name"].ToString(), dr["Code"].ToString()));

                }

                //ddlECTag1.DataSource = dt;
                //ddlECTag1.DataTextField = "Name";
                //ddlECTag1.DataValueField = "Code";
                //ddlECTag1.DataBind();


                //ddlECTag2.DataSource = dt;
                //ddlECTag2.DataTextField = "Name";
                //ddlECTag2.DataValueField = "Code";
                //ddlECTag2.DataBind();

                //绑定名称
                BindTextBox(ECCode.SelectedValue);

                //类型集合
                BindKeys();
            }
            
        }
             

        public void BindKeys()
        {
            DataTable dt = AVGDal.GetKeyList();
            gvData.DataSource = dt;
            gvData.DataBind(); 
        }

        public void BindTextBox(string ECCode)
        {
            string[] strnt = ECTagDal.GetNameEngunit(ECCode).Split(new string[] { ",", ";" }, StringSplitOptions.RemoveEmptyEntries);
            if (strnt.Length == 2)
            {
                ECName.Text = strnt[0];
                KeyEngunit.Text = strnt[1];
            }
            else if (strnt.Length == 1)
            {
                ECName.Text = strnt[0];
                KeyEngunit.Text = "";
            }
            else
            {
                ECName.Text = "";
                KeyEngunit.Text = "";
            }
        }

        #region GridView Key
        protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string sKeyID = gvData.DataKeys[e.Row.RowIndex]["KeyID"].ToString();
                string sECCode = gvData.DataKeys[e.Row.RowIndex]["ECCode"].ToString();
                string sECName = gvData.DataKeys[e.Row.RowIndex]["ECName"].ToString();
                string sKeyEngunit = gvData.DataKeys[e.Row.RowIndex]["KeyEngunit"].ToString();
                string sKeyTarget1 = gvData.DataKeys[e.Row.RowIndex]["KeyTarget1"].ToString();
                string sKeyTarget2 = gvData.DataKeys[e.Row.RowIndex]["KeyTarget2"].ToString();
                string sKeyDesign = gvData.DataKeys[e.Row.RowIndex]["KeyDesign"].ToString();
                string sKeyDiffMoney = gvData.DataKeys[e.Row.RowIndex]["KeyDiffMoney"].ToString();
                string sKeyOptMoney = gvData.DataKeys[e.Row.RowIndex]["KeyOptMoney"].ToString();
                string sKeyIndex = gvData.DataKeys[e.Row.RowIndex]["KeyIndex"].ToString();
                e.Row.Attributes.Add("id", sKeyID + "," + sECCode + "," + sECName + "," + sKeyEngunit + "," + sKeyTarget1 + "," + sKeyTarget2 + "," + sKeyDesign + "," + sKeyDiffMoney + "," + sKeyOptMoney + "," + sKeyIndex);
                
                //DataRowView drv = (DataRowView)e.Row.DataItem;

                //if ((DropDownList)e.Row.FindControl("ddlType") != null)
                //{
                //    DropDownList ddlCollege = (DropDownList)e.Row.FindControl("ddlType");

                //    ddlCollege.SelectedValue = drv["KeyCalcType"].ToString();
                //}
            }
        }

        #endregion


        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            BindKeys();
        }
    }


}