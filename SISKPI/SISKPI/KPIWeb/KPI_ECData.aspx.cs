using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SIS.DBControl;
using System.IO;
using System.Web.UI.HtmlControls;

using SIS.Loger;

using SIS.DataAccess;
using SIS.DataEntity;
using SIS.Exceler;


namespace SISKPI
{
    public partial class KPI_ECData : System.Web.UI.Page
    {
        public static int nTest = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (Request.QueryString["title"] != null)
                //{
                //    string title = OPM_TitleDal.GetTitle(Request.QueryString["title"].ToString());
                //    if (title != "")
                //    {
                //        lblTitle.Text = title;
                //    }
                //}
                
                //初始化时间
                txt_ST.Value = DateTime.Now.AddHours(-2).ToString("yyyy-MM-dd HH:mm:00");
                txt_ET.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:00");


                //获得参数集合
                if (Request.QueryString["ECID"] != null)
                {
                    ViewState["ECID"] = Request.QueryString["ECID"].ToString();
                }
                else
                {
                    ViewState["ECID"] = "";
                }

                BindValues();

            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        void BindValues()
        {
            //需要显示的集合
            string ECID = ViewState["ECID"].ToString();

            if (ECID.Equals(""))
            {
                return;
            }

            string strStartTime = txt_ST.Value;
            string strEndTime = txt_ET.Value;
            DateTime dtST = DateTime.Now;
            DateTime dtET = DateTime.Now;
            if(!DateTime.TryParse(strStartTime, out dtST)
                ||!DateTime.TryParse(strEndTime, out dtET)
                || dtST<dtET.AddHours(-48))
            {
                MessageBox.popupClientMessage(this.Page, "时间格式不正确，并且间隔不能大于 48小时!");
               
                return;
            }
            

            DataTable dt = ECSSArchiveDal.GetAllRecords(ECID, strStartTime, strEndTime);
            
            //绑定参数
            gvEC.DataSource = dt;
            gvEC.DataBind();
        }

        protected void gvEC_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ////鼠标移到效果
                //e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                //e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");

                //报警              
                string color = ((HtmlInputHidden)e.Row.Cells[0].FindControl("qulity")).Value;

                if (color != "" && color == "1")
                {
                    setColor(e, 2, 2, System.Drawing.Color.Yellow);
                }

            }

        }

        void setColor(GridViewRowEventArgs e, int from, int to, System.Drawing.Color c)
        {
            for (int i = from; i <= to; i++)
            {
                //单元格字体及颜色
                //e.Row.Cells[i].Font.Bold = true;
                e.Row.Cells[i].ForeColor = System.Drawing.Color.Red;

                //单元格背景色
                e.Row.Cells[i].BackColor = c;

                //整行的颜色
                //e.Row.BackColor = c;
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

                docEexport.Export("SISKPI报表");
            }

            return;
        }

    }


}
