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


namespace SISKPI
{
    public partial class KPI_Trend : System.Web.UI.Page
    {
        //private static int brcalc = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = ECTagDal.GetECs();
                foreach (DataRow dr in dt.Rows)
                {
                    ddl_EC.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
                }

                txt_ST.Value = "2012-12-15 08:00:00";
                txt_ET.Value = "2012-12-15 21:00:00";

                BindValues();      

            }
        }


        void BindValues()
        {
            string ecid = ddl_EC.SelectedValue;
            string ecname = ddl_EC.SelectedItem.Text;

            string stime = txt_ST.Value;
            string etime = txt_ET.Value;

            if (ecid == "")
            {
                return;
            }

            /////////////////////////////////////////////////////////////////////////////////////
            DataTable dt = ECSSSnapshotDal.GetSearchListTrend(ecid, stime, etime);

            if (dt.Rows.Count <= 0)
            {
                return;
            }
            
            //数据源
            //gvHC.DataSource = dt;

            //gvHC.DataBind();


            //画图
            DrawChart(ecname, dt);          

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
            seriespie.IsValueShownAsLabel = true;         // Set data points label style
            seriespie.LabelAngle = 0;
            //seriespie.CustomProperties = "LabelStyle=OutSide";
            seriespie.IsVisibleInLegend = true;
            //seriespie["BarLabelStyle"] = "Center";        // Show chart as 3D
            //seriespie["DrawingStyle"] = "Cylinder";
            seriespie.Label = "#VALY";

            List<double> lv = new List<double>();
            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                seriespie.Points.AddXY(dt.Rows[i]["ECTime"], dt.Rows[i]["ECValue"]);
                seriespie.Points[i].LegendText = dt.Rows[i]["ECTime"].ToString();

                lv.Add(double.Parse(dt.Rows[i]["ECValue"].ToString()));
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
            
            lv.Sort();
            double dp = lv[lv.Count-1] - lv[0];
            areapie.AxisY.Maximum = lv[lv.Count-1] + dp;
            areapie.AxisY.Minimum = lv[0] - dp;
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

        //protected void Timer1_Tick(object sender, EventArgs e)
        //{
        //    BindValues();

        //}

        protected void gvHC_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //鼠标移到效果
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");
              
            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            BindValues();
        }


    }


}
