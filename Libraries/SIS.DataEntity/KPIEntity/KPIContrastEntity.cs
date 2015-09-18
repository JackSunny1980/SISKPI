using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SIS.DataEntity {

    /// <summary>
    /// 指标对比分析
    /// </summary>
    public class KPIContrastEntity {

        #region 构造器
        public KPIContrastEntity() {

        }
        #endregion

        #region 属性

        /// <summary>
        /// 机组编码
        /// </summary>
        [Description("UnitID")]
        public String UnitID {
            get;
            set;
        }

        /// <summary>
        /// 指标编码
        /// </summary>
        [Description("ECID")]
        public String ECID {
            get;
            set;
        }

        /// <summary>
        /// 指标名称
        /// </summary>
        [Description("ECName")]
        public String ECName {
            get;
            set;
        }

        #region 本月排名情况

        /// <summary>
        /// 1值本月累计均值
        /// </summary>
        [Description("Shift1Value")]
        public decimal? Shift1Value {
            get;
            set;
        }

        /// <summary>
        /// 1值本月指标排名
        /// </summary>
        [Description("Shift1Rank")]
        public long? Shift1Rank {
            get;
            set;
        }

        /// <summary>
        /// 2值本月累计均值
        /// </summary>
        [Description("Shift2Value")]
        public decimal? Shift2Value {
            get;
            set;
        }

        /// <summary>
        /// 2值本月指标排名
        /// </summary>
        [Description("Shift2Rank")]
        public long? Shift2Rank {
            get;
            set;
        }

        /// <summary>
        /// 3值本月累计均值
        /// </summary>
        [Description("Shift3Value")]
        public decimal? Shift3Value {
            get;
            set;
        }

        /// <summary>
        /// 3值本月指标排名
        /// </summary>
        [Description("Shift3Rank")]
        public long? Shift3Rank {
            get;
            set;
        }

        /// <summary>
        /// 4值本月累计均值
        /// </summary>
        [Description("Shift4Value")]
        public decimal? Shift4Value {
            get;
            set;
        }

        /// <summary>
        /// 4值本月指标排名
        /// </summary>
        [Description("Shift4Rank")]
        public long? Shift4Rank {
            get;
            set;
        }

        /// <summary>
        /// 5值本月累计均值
        /// </summary>
        [Description("Shift5Value")]
        public decimal? Shift5Value {
            get;
            set;
        }

        /// <summary>
        /// 5值本月指标排名
        /// </summary>
        [Description("Shift5Rank")]
        public long? Shift5Rank {
            get;
            set;
        }

        #endregion

        #region 上月排名情况

        /// <summary>
        /// 1值上月累计均值
        /// </summary>
        [Description("Shift1HValue")]
        public decimal? Shift1HValue {
            get;
            set;
        }

        /// <summary>
        /// 1值上月指标排名
        /// </summary>
        [Description("Shift1HRank")]
        public long? Shift1HRank {
            get;
            set;
        }

        /// <summary>
        /// 2值上月累计均值
        /// </summary>
        [Description("Shift2HValue")]
        public decimal? Shift2HValue {
            get;
            set;
        }

        /// <summary>
        /// 2值上月指标排名
        /// </summary>
        [Description("Shift2HRank")]
        public long? Shift2HRank {
            get;
            set;
        }

        /// <summary>
        /// 3值上月累计均值
        /// </summary>
        [Description("Shift3HValue")]
        public decimal? Shift3HValue {
            get;
            set;
        }

        /// <summary>
        /// 3值上月指标排名
        /// </summary>
        [Description("Shift3HRank")]
        public long? Shift3HRank {
            get;
            set;
        }

        /// <summary>
        /// 4值上月累计均值
        /// </summary>
        [Description("Shift4HValue")]
        public decimal? Shift4HValue {
            get;
            set;
        }

        /// <summary>
        /// 4值上月指标排名
        /// </summary>
        [Description("Shift4HRank")]
        public long? Shift4HRank {
            get;
            set;
        }

        /// <summary>
        /// 5值上月累计均值
        /// </summary>
        [Description("Shift5HValue")]
        public decimal? Shift5HValue {
            get;
            set;
        }

        /// <summary>
        /// 5值上月指标排名
        /// </summary>
        [Description("Shift5HRank")]
        public long? Shift5HRank {
            get;
            set;
        }

        #endregion

        #endregion
    }
}
