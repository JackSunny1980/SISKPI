using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SIS.DataEntity {

    /// <summary>
    /// 指标排名实体
    /// </summary>
    public class ECRankEntity {

        #region 构造器
        public ECRankEntity() {

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

        /// <summary>
        /// 1值累计均值
        /// </summary>
        [Description("Shift1Value")]
        public decimal? Shift1Value {
            get;
            set;
        }

        /// <summary>
        /// 1值指标排名
        /// </summary>
        [Description("Shift1Rank")]
        public Int64? Shift1Rank {
            get;
            set;
        }

        /// <summary>
        /// 2值累计均值
        /// </summary>
        [Description("Shift2Value")]
        public decimal? Shift2Value {
            get;
            set;
        }

        /// <summary>
        /// 2值指标排名
        /// </summary>
        [Description("Shift2Rank")]
        public long? Shift2Rank {
            get;
            set;
        }

        /// <summary>
        /// 3值累计均值
        /// </summary>
        [Description("Shift3Value")]
        public decimal? Shift3Value {
            get;
            set;
        }

        /// <summary>
        /// 3值指标排名
        /// </summary>
        [Description("Shift3Rank")]
        public long? Shift3Rank {
            get;
            set;
        }

        /// <summary>
        /// 4值累计均值
        /// </summary>
        [Description("Shift4Value")]
        public decimal? Shift4Value {
            get;
            set;
        }

        /// <summary>
        /// 4值指标排名
        /// </summary>
        [Description("Shift4Rank")]
        public long? Shift4Rank {
            get;
            set;
        }

        /// <summary>
        /// 5值累计均值
        /// </summary>
        [Description("Shift5Value")]
        public decimal? Shift5Value {
            get;
            set;
        }

        /// <summary>
        /// 5值指标排名
        /// </summary>
        [Description("Shift5Rank")]
        public long? Shift5Rank {
            get;
            set;
        }


        #endregion
    }
}
