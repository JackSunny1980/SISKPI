using System;
using System.ComponentModel;

namespace SIS.DataEntity {
    /// <summary>
    /// 实体类:KPI_OverLimitRecordEntity
    /// 文件名:KPI_OverLimitRecordEntity.cs
    /// 说  明:超限报警记录
    /// </summary>
    public class KPI_OverLimitRecordEntity {
        #region 构造方法

        public KPI_OverLimitRecordEntity() {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 报警编号
        /// </summary>
        [Description("AlarmID")]
        public string AlarmID {
            get;
            set;
        }

        /// <summary>
        /// 指标编号
        /// </summary>
        [Description("TagID")]
        public string TagID {
            get;
            set;
        }

        /// <summary>
        /// 指标编号名称
        /// </summary>
        [Description("KpiName")]
        public string KpiName {
            get;
            set;
        }



        /// <summary>
        /// 开始时间
        /// </summary>
        [Description("AlarmStartTime")]
        public DateTime? AlarmStartTime {
            get;
            set;
        }

        /// <summary>
        /// 结束时间
        /// </summary>
        [Description("AlarmEndTime")]
        public DateTime? AlarmEndTime {
            get;
            set;
        }

        /// <summary>
        /// 时长
        /// </summary>
        [Description("Duration")]
        public int? Duration {
            get;
            set;
        }

        /// <summary>
        /// 时长分钟
        /// </summary>		
        public decimal? DurationMinute {
            get {
                return Duration / 60.0m;
            }

        }

        /// <summary>
        /// 超限极值
        /// </summary>
        [Description("AlarmValue")]
        public decimal? AlarmValue {
            get;
            set;
        }

        /// <summary>
        /// 超限标准
        /// </summary>
        [Description("StandardValue")]
        public decimal? StandardValue {
            get;
            set;
        }

        /// <summary>
        /// 偏移量
        /// </summary>
        [Description("Offset")]
        public decimal? Offset {
            get;
            set;
        }

        /// <summary>
        /// 超限类型
        /// </summary>
        [Description("AlarmType")]
        public int? AlarmType {
            get;
            set;
        }

        /// <summary>
        /// 超限类型名称
        /// </summary>
        [Description("AlarmTypeName")]
        public string AlarmTypeName {
            get;
            set;
        }

        /// <summary>
        /// 超限次数
        /// </summary>
        public int AlarmCount {
            get;
            set;
        }

        /// <summary>
        /// 班次
        /// </summary>
        [Description("Period")]
        public String Period {
            get;
            set;
        }

        /// <summary>
        /// 值次
        /// </summary>
        [Description("Shift")]
        public string Shift {
            get;
            set;
        }

        /// <summary>
        /// 班次名称
        /// </summary>
        [Description("ShiftName")]
        public string ShiftName {
            get;
            set;
        }

        /// <summary>
        /// 机组
        /// </summary>
        [Description("UnitID")]
        public string UnitID {
            get;
            set;
        }

        /// <summary>
        /// 机组名称
        /// </summary>
        [Description("UnitName")]
        public string UnitName {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [Description("SAName")]
        public string SAName {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [Description("RealDesc")]
        public string RealDesc {
            get;
            set;
        }

        [Description("RealCode")]
        public string RealCode {
            get;
            set;
        }

        #endregion
    }


}