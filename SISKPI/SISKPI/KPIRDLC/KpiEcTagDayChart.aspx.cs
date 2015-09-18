using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Reporting.WebForms;

namespace RdlcReportBasic.KpiTagDayReport
{
    public partial class KpiEcTagDayChart : System.Web.UI.Page
    {
        public static DataTable dtKPI = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //
                this.ddlStartHour.Items.Clear();
                this.ddlEndHour.Items.Clear();
                for (int i = 0; i <= 23; i++)
                {
                    string item = (i.ToString().Length == 1 ? "0" + i.ToString() : i.ToString());

                    this.ddlStartHour.Items.Add(new ListItem(item, item + ":00"));
                    this.ddlEndHour.Items.Add(new ListItem(item, item + ":00"));
                }

                //绑定机组
                this.ddlUnitEntity.Items.Clear();
                this.ddlUnitEntity.DataSource = this.odsUnitName;
                this.ddlUnitEntity.DataValueField = "ENTITY";
                this.ddlUnitEntity.DataTextField = "DESCRIBE";
                this.ddlUnitEntity.DataBind();

                //指标名称
                this.ddlKPIName.Items.Clear();
                this.ddlKPIName.DataSource = this.odsKPITag;
                this.ddlKPIName.DataValueField = "tagdefid";
                this.ddlKPIName.DataTextField = "DESCRIPTION";
                this.ddlKPIName.DataBind();

                tbStartDate.Text = DateTime.Now.AddDays(-1).Date.ToString("yyyy-MM-dd");
                tbStartDate.Attributes.Add("onfocus", "JavaScript:document.getElementById('Calendardiv').style.display = 'block'");

                tbEndDate.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
                tbEndDate.Attributes.Add("onfocus", "JavaScript:document.getElementById('Calendardiv2').style.display = 'block'");
                
                ///////////////////////////////////////////////////
                //默认全选
                LoadDate();
            }
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            tbStartDate.Text = Convert.ToDateTime(this.Calendar1.SelectedDate.ToShortDateString()).ToString("yyyy-MM-dd");
            Calendar1.Attributes.Add("onblur", "javascript:document.getElementById('Calendardiv').style.display=\"none\";");
        }

        protected void Calendar1_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "key", "document.getElementById('Calendardiv').style.display=\"block\";", true);
        }
        protected void Calendar2_SelectionChanged(object sender, EventArgs e)
        {
            tbEndDate.Text = Convert.ToDateTime(this.Calendar2.SelectedDate.ToShortDateString()).ToString("yyyy-MM-dd");
            Calendar2.Attributes.Add("onblur", "javascript:document.getElementById('Calendardiv').style.display=\"none\";");
        }

        protected void Calendar2_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            ScriptManager.RegisterStartupScript(UpdatePanel2, UpdatePanel2.GetType(), "key", "document.getElementById('Calendardiv2').style.display=\"block\";", true);
        }

        protected void LoadDate()
        {
            //判断是否都有值
            if (this.tbStartDate.Text.Trim() != "" && this.ddlUnitEntity.SelectedValue != "")
            {
                //清空数据源
                this.rvKPI.LocalReport.DataSources.Clear();

                //获得报表数据源
                //DataView dw = (DataView)ObjectDataSource1.Select();
                //ReportDataSource rds = new ReportDataSource("dsEcTagQuery", dw.Table);
                //this.rvKPI.LocalReport.DataSources.Add(rds);

                //dw = (DataView)ObjectDataSource2.Select();
                //rds = new ReportDataSource("dsTitle", dw.Table);
                //this.rvKPI.LocalReport.DataSources.Add(rds);
                //this.rvKPI.LocalReport.Refresh();
                //dw.Dispose();

                DataView dw = (DataView)odsDay.Select();
                ReportDataSource rds = new ReportDataSource("DataSet1", dw.Table);

                this.rvKPI.LocalReport.DataSources.Add(rds);

                dw = (DataView)odsTitle.Select();
                rds = new ReportDataSource("DataSet2", dw.Table);
                this.rvKPI.LocalReport.DataSources.Add(rds);
                
                //TimeSpan ts = DateTime.Parse(tbEndDate.Text) - DateTime.Parse(tbStartDate.Text);
                //double hInt = ts.TotalHours;

                //ReportParameter rp = new ReportParameter("HourIntervals", hInt.ToString());
                //this.rvKPI.LocalReport.SetParameters(new ReportParameter[] { rp });

                this.rvKPI.LocalReport.Refresh();

                dw.Dispose();
            }
        }

        protected void ReadBtton_Click(object sender, EventArgs e)
        {
            int iCheck = CheckSelectItem();
            if (iCheck == 1)
            {
                ClientScript.RegisterClientScriptBlock(GetType(),Guid.NewGuid().ToString(), 
                    "<script>alert('检索条件不能为空');</script>");
            }
            if (iCheck == 2) 
            {
                ClientScript.RegisterClientScriptBlock(GetType(), Guid.NewGuid().ToString(),
                    "<script>alert('开始日期不能大于结束日期');</script>");   
            }
            if (iCheck == 3)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), Guid.NewGuid().ToString(), 
                    "<script>alert('日期不能大于今天');</script>");
                //tbDate.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
            }

            if (iCheck == 0) 
            {
                LoadDate();
            }
            
        }

        private int CheckSelectItem() 
        {
            int iFlg = 0;
            //string[] strArr=new string[8];
            //strArr[0] = this.tbStartDate.Text.Trim();
            //strArr[1] = this.tbEndDate.Text.Trim();
            //strArr[2] = this.ddlStartHour.SelectedValue.ToString();
            //strArr[3] = this.ddlEndHour.SelectedValue.ToString();
            //strArr[4] = this.ddlShift.SelectedValue.ToString();
            //strArr[5] = this.ddlKPIName.SelectedValue.ToString();
            //strArr[6] = this.ddlTeam.SelectedValue.ToString();
            //strArr[7] = this.ddlUnitEntity.SelectedValue.ToString();
            //for (int i = 0; i < 8; i++) 
            //{
            //    if (strArr[i].Length <= 0) 
            //    {
            //        iFlg =  1;
            //    }
               
            //}


            if (iFlg == 0) 
            {
                if (Convert.ToDateTime(tbStartDate.Text) > Convert.ToDateTime(tbEndDate.Text)) 
                {
                    iFlg = 2;
                }
                else if (Convert.ToDateTime(tbStartDate.Text) == Convert.ToDateTime(tbEndDate.Text)) 
                {
                    if (Convert.ToDateTime(ddlStartHour.SelectedValue) > Convert.ToDateTime(ddlEndHour.SelectedValue))
                    {
                        iFlg = 2;
                    }
                }
                else if (Convert.ToDateTime(tbEndDate.Text) >= DateTime.Now.Date.AddDays(1))
                {
                    iFlg = 3;
                }
            }

            return iFlg;
        }


    }
}