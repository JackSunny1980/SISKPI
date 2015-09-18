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
using SIS.Exceler;
using System.Configuration;


namespace SISPI
{
    public partial class Default : System.Web.UI.Page
    {
        public static Dictionary<string, string> dictags;
        public static Dictionary<string, string> dicunits;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dictags = new Dictionary<string, string>();
                dicunits = new Dictionary<string, string>();

                //获得标签列表
                string strtag = ConfigurationManager.AppSettings["PITag"].ToString();

                string [] strtags = strtag.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                if (strtags.Length > 0)
                {
                    foreach (string tagnamedesc in strtags)
                    {
                        string[] strattrs = tagnamedesc.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                        if (strattrs.Length >= 3)
                        {
                            string tagname = strattrs[2];
                            dicunits.Add(tagname, strattrs[1]);
                            dictags.Add(tagname, tagnamedesc);
                        }
                    }

                }

                //初始化下拉列表
                foreach (KeyValuePair<string, string> tag in dictags)
                {
                    ddlTags.Items.Add(new ListItem(tag.Value, tag.Key ));
                }
                
                //初始化时间
                txt_ST.Value = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                txt_ET.Value = DateTime.Now.ToString("yyyy-MM-dd");
                
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        void BindValues()
        {
            //判断标签点
            if(ddlTags.SelectedValue=="")
            {
                MessageBox.popupClientMessage(this.Page, "标签点配置不正确!", "call();");
                return;
            }

            //判断时间
            DateTime dtST = DateTime.Parse(txt_ST.Value);
            DateTime dtET = DateTime.Parse(txt_ET.Value);

            if (dtST > dtET)
            {
                MessageBox.popupClientMessage(this.Page, "开始日期不能大于结束日期", "call();");
                return;
            }

            if (dtET > DateTime.Now)
            {
                MessageBox.popupClientMessage(this.Page, "结束日期不能大于当前时间", "call();");
                return;
            }

            //get value
            int nDays= (dtET-dtST).Days;

            string tagname = ddlTags.SelectedValue;
            //string tag

            //生成DataTable
            DataTable dt = new DataTable();

            dt.Columns.Add("TagID", typeof(String));
            dt.Columns.Add("TagName", typeof(String));
            dt.Columns.Add("TagUnit", typeof(String));
            dt.Columns.Add("TagTime", typeof(String));
            dt.Columns.Add("TagValue", typeof(String));
            dt.Columns.Add("TagStatus", typeof(String));

            double [] dValues = new double [nDays+1];

            for(int i=0; i<=nDays; i++)
            {
                dValues[i] = DBAccess.GetRealTime().GetArchiveValue(tagname, dtST.AddDays(i));


                DataRow dr = dt.NewRow();
                dr[0] = i.ToString();
                dr[1] = tagname;
                dr[2] = dicunits[tagname].ToString();
                dr[3] = dtST.AddDays(i).ToString("yyyy-MM-dd HH:mm:ss");
                dr[4] = dValues[i]!=double.MinValue? dValues[i].ToString("0.00"): null;
                dr[5] = dValues[i]!=double.MinValue? "Good": "BAD";

                dt.Rows.Add(dr);
            }
            
            /////////////////////////////////////////////////////////////////////////
            //差值
            DataRow drr = dt.NewRow();
            drr[0] = (nDays + 1).ToString();
            drr[1] = "计算结果";
            drr[2] = dicunits[tagname].ToString();
            drr[3] = "结束时间值减开始时间值";
            double dDiff = double.MinValue;
            if(dValues[0]!=double.MinValue && dValues[nDays]!=double.MinValue)
            {
                dDiff = dValues[nDays]- dValues[0];
            }

            drr[4] = dDiff != double.MinValue ? dDiff.ToString("0.00") : null;
            drr[5] = dDiff != double.MinValue ? "Good" : "BAD";

            dt.Rows.Add(drr);
            /////////////////////////////////////////////////////////////////////////
           

            //绑定参数
            gvEC.DataSource = dt;
            gvEC.DataBind();
        }

        protected void gvEC_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //鼠标移到效果
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");

                //报警              
                string color = ((HtmlInputHidden)e.Row.Cells[0].FindControl("qulity")).Value;

                if (color != "" && color == "BAD")
                {
                    setColor(e, 0, 10, System.Drawing.Color.FromName("Red"));
                }

            }

        }

        void setColor(GridViewRowEventArgs e, int from, int to, System.Drawing.Color c)
        {
            for (int i = from; i <= to; i++)
            {
                //e.Row.Cells[i].Font.Bold = true;
                //e.Row.Cells[i].ForeColor = c;
                e.Row.BackColor = c;
            }
        }       

        protected void gvEC_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "runTrend")
            {               

            }

        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            //更新Gridview
            BindValues();
        }


        // <summary>
        /// 导出到EXCEL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (gvEC.Rows.Count > 0)
            {
                DocExport docEexport = new DocExport();

                docEexport.Dt = GridViewHelper.GridView2DataTable(gvEC);

                docEexport.Export("SIS报表");
            }

            return;
        }


    }


}
