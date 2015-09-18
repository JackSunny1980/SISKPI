using System;
using System.Collections;
using System.Collections.Generic;
//FieldInfo
using System.Reflection;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Reporting.WebForms;

using SIS.DataAccess;

namespace SISKPI
{
    public partial class KPI_ECDay : System.Web.UI.Page
    {
        public static DataTable dtKPI = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //
                if (Request.QueryString["webcode"] != null)
                {
                    string webcode = Request.QueryString["webcode"].ToString();
                    string webid = KPI_WebDal.GetWebID(webcode);

                    lblWebID.Text = webid;

                }
                else
                {
                    lblWebID.Text = "";
                }

                //
                txt_Day.Value = DateTime.Now.AddDays(-1).ToLocalTime().ToString("yyyy-MM-dd");
                
                lblQueryDay.Text = txt_Day.Value;

                //
                LoadReport();
            }
        }
        

        protected void LoadReport()
        {
            string selDay = txt_Day.Value;

            if (DateTime.Parse(selDay) > DateTime.Now)
            {
                return;
            }

            ////判断是否都有值
            //if (this.tbStartDate.Text.Trim() != "" && this.ddlUnitEntity.SelectedValue != "")
            //{
            //清空数据源
            this.rvKPI.LocalReport.DataSources.Clear();

            //获得报表数据源
            DataView dw = (DataView)odsWebDesc.Select();
            ReportDataSource rds = new ReportDataSource("dsTitle", dw.Table);
            this.rvKPI.LocalReport.DataSources.Add(rds);

            dw = (DataView)odsECDay.Select();
            rds = new ReportDataSource("dsData", dw.Table);
            this.rvKPI.LocalReport.DataSources.Add(rds);


            this.rvKPI.LocalReport.Refresh();
            dw.Dispose();
        }

        #region ReportViewer


        protected void rvKPI_PreRender(object sender, EventArgs e)
        {
            //rdlc在设计时,如果要将导出格式隐藏，使用下面的方法

            //ReportViewer rw = sender as ReportViewer; 
            //if (rw == null) 
            //{ 
            // return; 
            //} 

            //var renders = from r in rw.LocalReport.ListRenderingExtensions();
            // where string.Compare(r.Name, "Excel", true) != 0 
            // select r; 
            //foreach (var r in renders) 
            //{ 
            // 
            //HideRender(r); 
            //} 

            foreach (RenderingExtension extension in rvKPI.LocalReport.ListRenderingExtensions()) 
            { 
                if (extension.Name == "PDF") 
                { 
                    //extension.Visible = false; 
                    // Property is readonly... 
                    FieldInfo fi = extension.GetType().GetField("m_isVisible", BindingFlags.Instance|BindingFlags.NonPublic); 
                    
                    fi.SetValue(extension, false); 
                } 
            } 
        }

        #endregion

        protected void btnQuery_Click(object sender, EventArgs e)
        {

            lblQueryDay.Text = txt_Day.Value;

            LoadReport();

        }

    }
}