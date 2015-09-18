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

namespace SISKPI
{
    public partial class Race_Real : System.Web.UI.Page
    {
        public string ShiftCurrentName = "";
        //public int ShiftCurrentIndex = -1;
        public int ViewModeIndex = 0;
        public bool BQuery = false;
        public static int nClickIndex = 1;
        public static bool bClick = false;        
       
        
        //private static Dictionary<int, string> dcUnits = new Dictionary<int, string>();

        public string SortExpression
        {
            get
            {
                if (ViewState["sortExpression"] == null)
                {
                    ViewState["sortExpression"] = "运行成本[元/KWh]";
                }

                return ViewState["sortExpression"].ToString();
            }
            set
            {
                ViewState["sortExpression"] = value;
            }
        }

        public string SortDire
        {
            get
            {
                if (ViewState["sortDire"] == null)
                {
                    ViewState["sortDire"] = "ASC";
                }
                return ViewState["sortDire"].ToString();
            }
            set
            {
                ViewState["sortDire"] = value;
            }
        }

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
               
                BQuery = false;
                ViewModeIndex = 0;

                divDay.Visible = true;
                divMonth.Visible = false;
                divYear.Visible = false;

                //string plantid = "";

                //if (Request.QueryString["plantcode"] != null)
                //{
                //    string plantcode = Request.QueryString["plantcode"].ToString();

                //    if (plantcode != "")
                //    {
                //        plantid = KPI_PlantDal.GetPlantIDByCode(plantcode);
                //    }
                //} 

                //绑定Button
                //dtUnits = KPI_UnitDal.GetUnitIDs(plantid);
                //if (dtUnits != null && dtUnits.Rows.Count > 0)
                //{
                //    for (int i = 0; i < dtUnits.Rows.Count; i++)
                //    {
                //        dcUnits[i] = dtUnits.Rows[i]["UnitID"].ToString();
                //    }
                //}
                
                //
                ViewState["UnitID"] = "";

                if (Request.QueryString["unitcode"] != null)
                {
                    string unitcode = Request.QueryString["unitcode"].ToString();

                    if (unitcode != "")
                    {
                        ViewState["UnitID"] = KPI_UnitDal.GetUnitIDByCode(unitcode);
                    }
                } 

                
                BindValues();
            
            }

            //绑定Button
            //if (dtUnits != null && dtUnits.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dtUnits.Rows.Count; i++)
            //    {           
            //        Button btd = new Button();
            //        btd.Width = 80;
            //        //btd.Height = 20;
            //        btd.Text = dtUnits.Rows[i]["UnitDesc"].ToString();
            //        btd.ID = i.ToString();
            //        //btd.CssClass = "lblstyle2";
            //        btd.Click += new System.EventHandler(BTN_Click);

            //        pButton.Controls.Add(btd);
            //    }
            //}
        }
        
        protected void BTN_Click(object sender, EventArgs e)
        {
            //Button btnTemp = (Button)sender;
            ////btnTemp.BackColor = System.Drawing.Color.Gold;

            //int i = int.Parse(btnTemp.ID);

            //UnitID = dcUnits[i];

            //BindValues();
        }

        void BindValues()
        {
            string UnitID = ViewState["UnitID"].ToString();
            //if (dcUnits.Count <= 0)
            //{
            //    return;
            //}

            if (UnitID == "")
            {
                //UnitID = dcUnits[0];
                return;
            }

            string WorkID = KPI_UnitDal.GetWorkIDByID(UnitID);

            if (WorkID == "")
            {
                return;
            }

            double IDLHour = KPI_WorkDal.GetIDLHour(WorkID);
            //班开始结束时间的小时、分钟转换，考虑有半点交班的情况。
            int ih = (int)Math.Floor(IDLHour);
            int im = (int)((IDLHour - ih) * 60);

            DateTime dt = DateTime.Now;
            DateTime dc = DateTime.Now;            

            DateTime QueryStartTime = DateTime.Now;
            DateTime QueryEndTime = QueryStartTime;

            try
            {
                //判断日期
                if (ViewModeIndex == 0)
                {
                    if (BQuery)
                    {
                        dt = DateTime.Parse(txt_Day.Value);

                        if ((dt > DateTime.Now) || (dt.Year==dc.Year && dt.Month==dc.Month && dt.Day== dc.Day))
                        {
                            MessageBox.popupClientMessage(this.Page, "不能大于等于本日,请重新选择！", "call();");
                            //ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('不能大于等于本日,请重新选择！')", true);
                            return;
                         }
                        
                        QueryStartTime = new DateTime(dt.Year, dt.Month, dt.Day, ih, im, 0);
                        QueryEndTime = QueryStartTime.AddDays(1);
                    }

                    //snapshot

                }
                else if (ViewModeIndex == 1)
                {
                    if (BQuery)
                    {
                        dt = DateTime.Parse(txt_Month.Value);

                        if ((dt > DateTime.Now) || (dt.Year == dc.Year && dt.Month == dc.Month))
                        {

                            MessageBox.popupClientMessage(this.Page, "不能大于等于本月,请重新选择！", "call();");
                            //ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('不能大于等于本月,请重新选择！')", true);
                            return;
                        }

                        QueryStartTime = new DateTime(dt.Year, dt.Month, 1, ih, im, 0);
                        QueryEndTime = QueryStartTime.AddMonths(1);
                    }
                    else
                    {
                        QueryStartTime = new DateTime(dc.Year, dc.Month, 1, ih, im, 0);
                        QueryEndTime = QueryStartTime.AddMonths(1);
                    }

                }
                else if (ViewModeIndex == 2)
                {
                    if (BQuery)
                    {
                        dt =new DateTime(int.Parse(txt_Year.Value), 1, 1);

                        if ((dt > DateTime.Now) || (dt.Year == dc.Year))
                        {
                            MessageBox.popupClientMessage(this.Page, "不能大于等于本年,请重新选择！", "call();");
                            //ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('不能大于等于本年,请重新选择！')", true);
                            return;
                        }

                        QueryStartTime = new DateTime(dt.Year, 1, 1, ih, im, 0);
                        QueryEndTime = QueryStartTime.AddYears(1);
                    }
                    else
                    {
                        QueryStartTime = new DateTime(dc.Year, 1, 1, ih, im, 0);
                        QueryEndTime = QueryStartTime.AddYears(1);
                    }
                }

            }catch(Exception)
            {

                MessageBox.popupClientMessage(this.Page, "时间输入错误！", "call();");
                //ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('时间输入错误！')", true);
                return;
            }


            string QueryTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:00");

            ShiftCurrentName = KPI_WorkDal.GetCurrentShift(WorkID, QueryTime);

            DataTable dtValue = null;

            if (QueryStartTime == QueryEndTime)
            {
                //if (rblGVType.SelectedIndex == 0)
                //{
                    dtValue = Race_SnapshotDal.GetSearchListC(UnitID);
                //}
                //else
                //{
                //    dtValue = Race_SnapshotDal.GetSearchListS(UnitID);
                //}

                lblInfor.Text = "当前查询为各值最近运行班统计！";
            }
            else
            {
                //if (rblGVType.SelectedIndex == 0)
                //{
                    dtValue = Race_ArchiveDal.GetSearchListC(UnitID, QueryStartTime.ToString("yyyy-MM-dd HH:mm:ss"), QueryEndTime.ToString("yyyy-MM-dd HH:mm:ss"));
                //}
                //else
                //{
                //    dtValue = Race_ArchiveDal.GetSearchListS(UnitID, QueryStartTime.ToString("yyyy-MM-dd HH:mm:ss"), QueryEndTime.ToString("yyyy-MM-dd HH:mm:ss"));
                //}

                lblInfor.Text = "当前查询开始时间为：" + QueryStartTime.ToString("yyyy-MM-dd HH:mm:ss") + "; 结束时间为：" + QueryEndTime.ToString("yyyy-MM-dd HH:mm:ss");

            }

            //gvValue.DataSource = dtValue;

            //gvValue.DataBind();
            if (dtValue==null ||dtValue.Rows.Count <= 0)
            {
                gvValue.DataSource = null;
                gvValue.DataBind();

                return;
            }

            //手动绑定
            gvValue.Columns.Clear();

            for(int i=0; i<dtValue.Columns.Count; i++)
            {
                BoundField nameColumn = new BoundField();
                nameColumn.HeaderText = dtValue.Columns[i].Caption;
                nameColumn.DataField = dtValue.Columns[i].Caption;

                nameColumn.SortExpression = dtValue.Columns[i].Caption;

                gvValue.Columns.Add(nameColumn);

                if (!bClick)
                {
                    SortExpression = nameColumn.SortExpression;
                    SortDire = "ASC";

                    nClickIndex = i;
                }
            }

            //数据源
            string sort = this.SortExpression + " " + this.SortDire;

            dtValue.DefaultView.Sort = sort;

            gvValue.DataSource = dtValue;

            gvValue.DataBind();

            //nIndex = 
            DrawChart(dtValue, nClickIndex);

        }

        /// <summary>
        /// 生成图表
        /// </summary>
        void DrawChart(DataTable dt, int nIndex)
        {
            if (dt.Rows.Count <= 0)
            {
                return;
            }

            if (nIndex > (dt.Columns.Count - 1) || nIndex < 0)
            {
                return;
            }

            //获得标题头
            string strXTitle = "运行值";
            string strYTitle = dt.Columns[nIndex].Caption;

            //获得值、数据
            int nRows = dt.Rows.Count;

            List<string> sall = new List<string>();
            List<double> dall = new List<double>();

            //dt.Rows[nRows][0].ToString();
            //dt.Rows[nRows][nIndex].ToString();
            for (int i = 0; i < nRows; i++)
            {
                sall.Add(dt.Rows[i][0].ToString());

                dall.Add(double.Parse(dt.Rows[i][nIndex].ToString())); 
            }


            //////////////////////////////////////////////////////////////////////////////////////////////
            //What other values are available elsewhere for formating text like #VALX, trying to format dates
            //Keyword	Replaced By	Supports Multiple Y Values 	Supports Formatting String
            //#VALX	X value of the data point.	No	Yes
            //#VALY	Y value of the data point	Yes	Yes
            //#SERIESNAME	Series name	No	No
            //#LABEL	Data point label	No	No
            //#AXISLABEL	Data point axis label	No	No
            //#INDEX	Data point index in the series	No	Yes
            //#PERCENT	Percent of the data point Y value	Yes	Yes
            //#LEGENDTEXT	Series or data point legend text	No	No
            //#CUSTOMPROPERTY(XXX)	Series or data point XXX custom property value, where XXX is the name of the custom property.	No	No
            //#TOTAL	Total of all Y values in the series	Yes	Yes
            //#AVG	Average of all Y values in the series	Yes	Yes
            //#MIN	Minimum of all Y values in the series	Yes	Yes
            //#MAX	Maximum of all Y values in the series	Yes	Yes
            //#FIRST	Y value of the first point in the series	Yes	Yes
            //#LAST	Y value of the last point in the series	Yes	Yes

            //MS Chart
            ///////////////////////////////////////////////////////////////////////////////////////////////
            //Lengend
            //Legend legendpub = new Legend("Default");
            //legendpub.HeaderSeparator = LegendSeparatorStyle.Line;
            //legendpub.HeaderSeparatorColor = Color.Gray;
            //// Add Color column      
            //LegendCellColumn firstColumn = new LegendCellColumn();
            //firstColumn.ColumnType = LegendCellColumnType.SeriesSymbol;
            //firstColumn.HeaderText = "颜色";
            //firstColumn.HeaderBackColor = Color.WhiteSmoke;
            //legendpub.CellColumns.Add(firstColumn);

            //// Add Legend Text column      
            //LegendCellColumn secondColumn = new LegendCellColumn();
            //secondColumn.ColumnType = LegendCellColumnType.Text;
            //secondColumn.HeaderText = "参数";
            //secondColumn.Text = "#VALX";
            //secondColumn.HeaderBackColor = Color.WhiteSmoke;
            //legendpub.CellColumns.Add(secondColumn);

            // Add Value cell column      
            //LegendCellColumn avgColumn = new LegendCellColumn();
            //avgColumn.Text = "#VALY";
            //avgColumn.HeaderText = "耗差";
            //avgColumn.Name = "Value";
            //avgColumn.HeaderBackColor = Color.WhiteSmoke;
            //legendpub.CellColumns.Add(avgColumn);

            //legendpub.Docking = Docking.Left;
            //legendpub.Alignment = StringAlignment.Center;
            //legendpub.IsDockedInsideChartArea = true;
            //legendpub.BackColor = Color.FromArgb(206, 219, 214);


            Font fts = new Font("楷书", 8);

            ///////////////////////////////////////////////////////////////////////////////////////////////
            //Column
            Series seriescol = new Series("Default");
            seriescol.ChartType = SeriesChartType.Column;
            //seriescol.Color = Color.LightBlue;
            seriescol["PointWidth"] = "0.3";              // Set the bar width
            seriescol.IsValueShownAsLabel = true;         // Show data points labels
            //seriescol.LabelAngle = 20;                  // Set data points label style
            //seriescol.CustomProperties = "LabelStyle=OutSide";
            seriescol.IsVisibleInLegend = true;
            seriescol["BarLabelStyle"] = "Center";        // Show chart as 3D
            seriescol["DrawingStyle"] = "Cylinder";
            seriescol.ToolTip = "#VALX";          // #VALX 是用来获取 饼图的实际数字的 #PERCENT{P1}是用来获取百分比的
            //seriescol.LegendText = strYTitle;
            seriescol.Font = fts;

            for (int i = 0; i < nRows; i++)
            {
                seriescol.Points.AddXY(sall[i], dall[i]);
                seriescol.Points[i].Label = dall[i].ToString();
            }

            colValue.Series.Add(seriescol);

            //添加图例
            //colValue.Legends.Add(legendpub);

            Font ft = new Font("楷书", 10);

            //
            ChartArea areacol = new ChartArea("Default");

            areacol.AxisX.LineColor = Color.Transparent;
            areacol.AxisX.MajorGrid.LineColor = Color.Transparent;
            //areacol.AxisX.Interval = 1;
            areacol.AxisX.Title = strXTitle;
            areacol.AxisX.TitleFont = ft;
            //areacol.AxisX.l

            areacol.AxisY.LineColor = Color.LightGray;
            areacol.AxisY.MajorGrid.LineColor = Color.Transparent;
            //areacol.AxisY.Maximum = 8;
            //areacol.AxisY.Minimum = -8;
            //areacol.AxisY.Interval = 2;
            areacol.AxisY.Title = strYTitle;
            areacol.AxisY.TitleFont = ft;

            areacol.ShadowColor = Color.Transparent;
            areacol.BackColor = Color.White;
            areacol.BackGradientStyle = GradientStyle.TopBottom;
            areacol.BackSecondaryColor = Color.Azure;
            areacol.Area3DStyle.Enable3D = true;  // Draw chart as 3D Cylinder

            colValue.ChartAreas.Add(areacol);

        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            BindValues();
        }
        
        protected void gvValue_RowDataBound(object sender, GridViewRowEventArgs e)
        {   
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //设置标题栏对齐格式
                e.Row.HorizontalAlign = HorizontalAlign.Center;

                foreach (DataControlField dataControlField in gvValue.Columns)
                {
                    if (dataControlField.SortExpression.Equals(this.SortExpression))
                    {
                        Label label = new Label();
                        label.Text = this.SortDire.Equals("ASC") ? "▲" : "▼";
                        e.Row.Cells[gvValue.Columns.IndexOf(dataControlField)].Controls.Add(label);
                    }
                }                                
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // 设置行的背景颜色  
                if (ShiftCurrentName!= "" && e.Row.Cells[0].Text == ShiftCurrentName)
                {
                    e.Row.BackColor = System.Drawing.Color.Tomato;
                }
                
                //设置数据行的对齐格式；
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Center;
                }
                
                //鼠标移到效果
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");
            }

        }
        
        protected void gvValue_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sPage = e.SortExpression;

            if (this.SortExpression == sPage)
            {
                if (this.SortDire == "DESC")
                    this.SortDire = "ASC";
                else
                    this.SortDire = "DESC";
            }
            else
            {
                this.SortExpression = sPage;

                this.SortDire = "ASC";
            }

            if (String.Empty != this.SortExpression)
            {
                // Based on the sort expression, find the index of the sorted column
                //获得顺序
                bClick = true;

                nClickIndex = GetSortColumnIndex(this.gvValue, this.SortExpression);
            }

            BindValues();
        }

        private int GetSortColumnIndex(GridView gridView, String sortExpression)
        {
            if (gridView == null)
                return 1;

            foreach (DataControlField field in gridView.Columns)
            {
                if (field.SortExpression == sortExpression)
                {
                    return gridView.Columns.IndexOf(field);
                }
            }

            return 1;
        }


        protected void btnCurrent_Click(object sender, EventArgs e)
        {
            BQuery = false;
            txt_Day.Value = "";

            ViewModeIndex = 0;
            divDay.Visible = true;
            divMonth.Visible = false;
            divYear.Visible = false;


            BindValues();
        }

        protected void btnCurrentMonth_Click(object sender, EventArgs e)
        {
            BQuery = false;
            txt_Month.Value = "";

            ViewModeIndex = 1;
            divDay.Visible = false;
            divMonth.Visible = true;
            divYear.Visible = false;

            BindValues();
        }

        protected void btnCurrentYear_Click(object sender, EventArgs e)
        {
            BQuery = false;
            txt_Year.Value = "";

            ViewModeIndex = 2;
            divDay.Visible = false;
            divMonth.Visible = false;
            divYear.Visible = true;


            BindValues();
        }


        protected void btnDay_Click(object sender, EventArgs e)
        {

            BQuery = true;
            BindValues();
        }

        protected void btnMonth_Click(object sender, EventArgs e)
        {
            BQuery = true;
            BindValues();
        }

        protected void btnYear_Click(object sender, EventArgs e)
        {
            BQuery = true;
            BindValues();
        }



    }


}
