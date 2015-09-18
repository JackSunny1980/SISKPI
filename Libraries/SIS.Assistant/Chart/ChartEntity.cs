using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;

namespace SIS.Assistant
{
    /// <summary>
    /// 图表单点实体类
    /// </summary>
    public class PointEntity
    {
        public PointEntity()
        {
        }
        /// <summary>
        /// x值
        /// </summary>
        public object x
        {
            get;
            set;
        }
        /// <summary>
        /// y值
        /// </summary>
        public object y
        {
            get;
            set;
        }
        /// <summary>
        /// x格式
        /// </summary>
        public string xFormat
        {
            get;
            set;
        }
        /// <summary>
        /// 点描述
        /// </summary>
        public string PointDesc
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 系列实体类
    /// </summary>
    public class SeriesEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        private string _name = "";
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }
        /// <summary>
        /// 点列表
        /// </summary>
        public List<PointEntity> Points
        {
            get;
            set;
        }
        /// <summary>
        /// 图表类型
        /// </summary>
        public SeriesChartType ChartType
        {
            get;
            set;
        }
        /// <summary>
        /// 系列颜色
        /// </summary>
        public Color Color
        {
            get;
            set;
        }
        /// <summary>
        /// 是否显示标记点
        /// </summary>
        private bool _IsMarker = false;
        public bool IsMarker
        {
            get { return _IsMarker; }
            set { _IsMarker = value; }
        }
        /// <summary>
        /// 标记点样式
        /// </summary>
        public MarkerStyle MarkStyle
        {
            get;
            set;
        }
        /// <summary>
        /// 标记点大小
        /// </summary>
        public int MarkerSize
        {
            get;
            set;
        }
        /// <summary>
        /// 线宽
        /// </summary>
        private int _BorderWidth = 1;
        public int BorderWidth
        {
            get { return _BorderWidth; }
            set { _BorderWidth = value; }
        }
        /// <summary>
        /// 线样式
        /// </summary>
        private ChartDashStyle _DashStyle = ChartDashStyle.Solid;
        public ChartDashStyle DashStyle
        {
            get { return _DashStyle; }
            set { _DashStyle = value; }
        }
    }
    /// <summary>
    /// 图表实体类
    /// </summary>
    public class ChartEntity
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string title
        {
            get;
            set;
        }
        /// <summary>
        /// 系列集合
        /// </summary>
        public List<SeriesEntity> Series
        {
            get;
            set;
        }
        /// <summary>
        /// y最大值
        /// </summary>
        public double MaxY
        {
            get;
            set;
        }
        /// <summary>
        /// y最小值
        /// </summary>
        public double MinY
        {
            get;
            set;
        }
        /// <summary>
        /// x最大值
        /// </summary>
        public double MaxX
        {
            get;
            set;
        }
        /// <summary>
        /// x最小值
        /// </summary>
        public double MinX
        {
            get;
            set;
        }       
        /// <summary>
        /// 图表区域
        /// </summary>
        public ChartArea chartArea
        {
            get;
            set;
        }
    }
}
