 
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using System.Collections.Generic;
using System;

namespace SIS.Assistant
{
    public class ChartControl
    {
        System.Web.UI.DataVisualization.Charting.Chart Chart1;
        public ChartControl(System.Web.UI.DataVisualization.Charting.Chart chart)
        {
            this.Chart1 = chart;
        }
        /// <summary>
        /// 设置控件样式
        /// </summary>
        public void SetFinishStyle()
        {
            string fontname = "微软雅黑";
            FontStyle fontStyle = FontStyle.Regular;
            foreach (Legend leg in Chart1.Legends)
            {
                leg.Font = new Font(fontname, 10, fontStyle);
            }

            foreach (Series series in Chart1.Series)
            {
                series.Font = new Font(fontname, 8, FontStyle.Regular);
            }

            foreach (Title title in Chart1.Titles)
            {
                title.Font = new Font(fontname, 10, fontStyle);
            }

            Chart1.Titles[0].Font = new Font(fontname, 12, fontStyle);
            if (Chart1.Titles.Count > 1)
            {
                Chart1.Titles[1].Alignment = ContentAlignment.TopRight;
                Chart1.Titles[1].ForeColor = Color.Green;//ColorTranslator.FromHtml("#C9F76F");
            }
        }
        /// <summary>
        /// 画图表
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="RealData">实时值</param>
        public void DrawChart(ChartEntity entity, string RealData)
        {
            Chart1.Titles.Add(entity.title);
            if (RealData != "") Chart1.Titles.Add("实时值:" + RealData);
            AddSeries(ref Chart1, entity.Series.Count);

            for (int i = 0; i < entity.Series.Count; i++)
            {
                DrawChart(Chart1.Series[i], entity.Series[i]);
            }
        }
        /// <summary>
        /// 添加Series
        /// </summary>
        /// <param name="chart">图表控件</param>
        /// <param name="count">数量</param>
        void AddSeries(ref System.Web.UI.DataVisualization.Charting.Chart chart, int count)
        {
            for (int i = 0; i < count; i++)
            {
                Series item = new Series();
                item.Name = "Series" + i.ToString();
                item.MarkerSize = 10;
                item.BorderColor = System.Drawing.Color.FromArgb(180, 26, 59, 105);
                item.SmartLabelStyle.Enabled = true;
                chart.Series.Add(item);
            }
        }
        private double _high = double.MinValue;
        public double high
        {
            get { return _high; }
            set {  _high= value; }
        }

        private double _low = double.MaxValue;
        public double low
        {
            get { return _low; }
            set {  _low= value; }
        }
        /// <summary>
        /// 画图表
        /// </summary>
        /// <param name="series">系列对象</param>
        /// <param name="entity">系列实体</param>
        private void DrawChart(Series series, SeriesEntity entity)
        {
            series.Name = entity.name;
            series.ChartType = entity.ChartType;
            series.Color = entity.Color;
            if (entity.IsMarker)
            {
                series.MarkerSize = entity.MarkerSize;
                series.MarkerStyle = entity.MarkStyle;
            }
            series.BorderWidth = entity.BorderWidth;
            series.BorderDashStyle = entity.DashStyle;

            series.XValueType = ChartValueType.DateTime;

            double d = .00;
            //得到Y轴曲线上下限
            foreach (PointEntity point in entity.Points)
            {
                d = double.Parse(point.y.ToString());
                if (d > high)
                {
                    high = d;
                }
                if (d < low)
                {
                    low = d;
                }
            }

            series.Points.DataBind(entity.Points, "x", "y", "");

        }

    }
}