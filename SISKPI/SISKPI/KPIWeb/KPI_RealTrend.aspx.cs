using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;
using System.Data;


using SIS.DBControl;
using SIS.DataAccess;
using SIS.DataEntity;
using SIS.Loger;

using System.Linq;


namespace SISKPI
{
    public partial class KPI_RealTrend: System.Web.UI.Page
    {

        private static DataTable dt = null;
        private static String KPIName = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                txt_ST.Value = DateTime.Now.AddHours(-2).ToString("yyyy-MM-dd HH:mm:00");
                txt_ET.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:00");
                
                //获得参数集合
                if (Request.QueryString["RealID"] != null)
                {
                    ViewState["RealID"] = Request.QueryString["RealID"].ToString();
                }
                else
                {
                    ViewState["RealID"] = "";
                }

                BindValues(true);      

            }
        }


        void BindValues(bool bdrawchart)
        {
            if (bdrawchart)
            {
                //需要显示的集合
                string RealID = ViewState["RealID"].ToString();

                if (RealID.Equals(""))
                {
                    return;
                }

                string strStartTime = txt_ST.Value;
                string strEndTime = txt_ET.Value;
                DateTime dtST = DateTime.Now;
                DateTime dtET = DateTime.Now;
                if (!DateTime.TryParse(strStartTime, out dtST)
                    || !DateTime.TryParse(strEndTime, out dtET))
                {
                    MessageBox.popupClientMessage(this.Page, "时间格式不正确  且 只能查询1天之内的数据!", "call();");
                    return;
                }


                dt = ArchiveValueDal.GetRecordsFromView(RealID, strStartTime, strEndTime);

                if (dt == null && dt.Rows.Count <= 0)
                {
                    return;
                }                

                //绑定参数
                gvReal.DataSource = dt;
                gvReal.DataBind();

                var kpiresult = (from kpi in dt.AsEnumerable()
                                  select kpi.Field<string>("RealDesc")).Distinct().ToList();
                if (kpiresult.Count > 0)
                {
                    KPIName = kpiresult.ElementAt(0).ToString();
                }

                //画图
                DrawChart(KPIName, dt);
            }
            else
            {
                if (dt==null && dt.Rows.Count <= 0)
                {
                    return;
                }

                //绑定参数
                gvReal.DataSource = dt;
                gvReal.DataBind();

                //画图
                DrawChart(KPIName, dt);
            }

        }

        /// <summary>
        /// 生成图表
        /// </summary>
        void DrawChart(string title, DataTable dt)
        {
            if (dt.Rows.Count <= 0)
            {
                return;
            }

            //
            Font font8 = new Font("Microsoft Sans Serif", 8);


            //MS Chart
            ///////////////////////////////////////////////////////////////////////////////////////////////
            //Lengend
            Legend legendpub = new Legend("Default");

            legendpub.Docking = Docking.Bottom;
            legendpub.Alignment = StringAlignment.Center;
            legendpub.IsDockedInsideChartArea = true;
            legendpub.Font = font8;
            legendpub.BackColor = Color.Transparent;

            ///////////////////////////////////////////////////////////////////////////////////////////////
            //Pie
            //Series
            Series seriespie = new Series(title);           
            seriespie.ChartType = SeriesChartType.Line;     // Set the bar width
            seriespie["PointWidth"] = "0.5";              // Show data points labels
            seriespie.MarkerStyle = MarkerStyle.Triangle;
            seriespie.IsValueShownAsLabel = false;         // Set data points label style
            seriespie.LabelAngle = 0;
            //seriespie.CustomProperties = "LabelStyle=OutSide";
            seriespie.IsVisibleInLegend = true;
            //seriespie["BarLabelStyle"] = "Center";        // Show chart as 3D
            //seriespie["DrawingStyle"] = "Cylinder";
            //seriespie.Label = "#VALY";

            List<double> lv = new List<double>();
            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                seriespie.Points.AddXY(dt.Rows[i]["RealTime"], dt.Rows[i]["RealValue"]);
                seriespie.Points[i].LegendText = dt.Rows[i]["RealTime"].ToString();

                lv.Add(double.Parse(dt.Rows[i]["RealValue"].ToString()));
            }
                  
            pieHC.Series.Add(seriespie);            

            pieHC.Legends.Add(legendpub);

            //ChartArea
            ChartArea areapie = new ChartArea("xx");

            areapie.AxisX.LineColor = Color.ForestGreen;
            areapie.AxisX.MajorGrid.LineColor = Color.Transparent;
            //areapie.AxisX.Interval = 1;
            areapie.AxisX.LabelStyle.Font = font8;
            areapie.AxisX.IsLabelAutoFit = false;
            areapie.AxisX.LabelStyle.Angle = 0;
            areapie.AxisX.Title = "时间";

            areapie.AxisY.LineColor = Color.ForestGreen;
            areapie.AxisY.LabelStyle.Font = font8;
            areapie.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            areapie.AxisY.MajorGrid.LineColor = Color.LightSlateGray;
            
            //已经排序
            lv.Sort();

            //double dp = lv[lv.Count-1] - lv[0];
            //double dMax = lv[lv.Count - 1] +dp;
            //double dMin = lv[0] - dp;
            //if (dMin < 10e-3)
            //{
            //    dMin = 1;
            //}

            //areapie.AxisY.Maximum = dMax;
            //areapie.AxisY.Minimum = dMin;
            //areapie.AxisY.Interval = 2;
            areapie.AxisY.Title = title;

            areapie.ShadowColor = Color.Transparent;
            areapie.BackColor = Color.Azure;
            areapie.BackGradientStyle = GradientStyle.TopBottom;
            areapie.BackSecondaryColor = Color.White;
            //areapie.Area3DStyle.Enable3D = true;     // Draw chart as 3D Cylinder             
            //areapie
            pieHC.ChartAreas.Add(areapie);


        }


        protected void gvReal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //鼠标移到效果
                //e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                //e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");
              
            }
        }


        protected void gvReal_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvReal.PageIndex = e.NewPageIndex;

            BindValues(false);
        }


        protected void btnQuery_Click(object sender, EventArgs e)
        {
            BindValues(true);
        }

    }


}
