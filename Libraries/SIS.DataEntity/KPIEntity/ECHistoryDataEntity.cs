using System;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SIS.DataEntity {

    /// <summary>
    /// 经济指标历史数据
    /// </summary>
    public class ECHistoryDataEntity {

        #region  构造器

        public ECHistoryDataEntity() {

        }

        #endregion

        #region 属性

        /// <summary>
        /// 机组编码
        /// </summary>
        [Description("UnitID")]
        public virtual String UnitID {
            get;
            set;
        }

        /// <summary>
        /// 经济指标编码
        /// </summary>
        [Description("ECID")]
        public virtual String ECID {
            get;
            set;
        }

        /// <summary>
        /// 经济指标名称
        /// </summary>
        [Description("ECName")]
        public virtual String ECName {
            get;
            set;
        }

        /// <summary>
        /// 经济指标单位
        /// </summary>
        [Description("EngunitName")]
        public virtual String EngunitName {
            get;
            set;
        }

        /// <summary>
        /// 值次
        /// </summary>
        [Description("Shift")]
        public virtual String Shift {
            get;
            set;
        }

        /// <summary>
        /// 考核日期
        /// </summary>
        [Description("CheckDate")]
        public virtual DateTime CheckDate {
            get;
            set;
        }

        /// <summary>
        /// 最小值
        /// </summary>
        [Description("MinValue")]
        public virtual Decimal MinValue {
            get;
            set;
        }

        /// <summary>
        /// 平均值
        /// </summary>
        [Description("AvgValue")]
        public virtual Decimal AvgValue {
            get;
            set;
        }

        /// <summary>
        /// 最大值
        /// </summary>
        [Description("MaxValue")]
        public virtual Decimal MaxValue {
            get;
            set;
        }

        /// <summary>
        /// 累计得分
        /// </summary>
        [Description("Score")]
        public virtual Decimal Score {
            get;
            set;
        }

        #endregion
    }
}
