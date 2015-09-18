using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SIS.DataEntity {
    public class KPIRankEntity {

        #region 构造器

        public KPIRankEntity() {

        }

        #endregion

        #region 属性

        [Description("ECID")]
        public String ECID {
            get;
            set;
        }


        [Description("ECName")]
        public String ECName {
            get;
            set;
        }

        [Description("UnitID")]
        public String UnitID {
            get;
            set;
        }

        #region 一值

        /// <summary>
        /// 总得分
        /// </summary>
        [Description("Shift1Score")]
        public decimal? Shift1Score {
            get;
            set;
        }

        /// <summary>
        /// 监盘时间
        /// </summary>
        [Description("Shift1Hours")]
        public int? Shift1Hours {
            get;
            set;
        }

        /// <summary>
        /// 平均得分
        /// </summary>
        [Description("Shift1AvgScore")]
        public decimal? Shift1AvgScore {
            get;
            set;
        }

        /// <summary>
        /// 排名
        /// </summary>
        [Description("Shift1Rank")]
        public long? Shift1Rank {
            get;
            set;
        }

        #endregion

        #region 二值

        [Description("Shift2Score")]
        public decimal? Shift2Score {
            get;
            set;
        }

        [Description("Shift2Hours")]
        public int? Shift2Hours {
            get;
            set;
        }

        [Description("Shift2AvgScore")]
        public decimal? Shift2AvgScore {
            get;
            set;
        }

        [Description("Shift2Rank")]
        public long? Shift2Rank {
            get;
            set;
        }

        #endregion

        #region 三值

        [Description("Shift3Score")]
        public decimal? Shift3Score {
            get;
            set;
        }

        [Description("Shift3Hours")]
        public int? Shift3Hours {
            get;
            set;
        }

        [Description("Shift3AvgScore")]
        public decimal? Shift3AvgScore {
            get;
            set;
        }

        [Description("Shift3Rank")]
        public long? Shift3Rank {
            get;
            set;
        }

        #endregion

        #region 四值

        [Description("Shift4Score")]
        public decimal? Shift4Score {
            get;
            set;
        }

        [Description("Shift4Hours")]
        public int? Shift4Hours {
            get;
            set;
        }

        [Description("Shift4AvgScore")]
        public decimal? Shift4AvgScore {
            get;
            set;
        }

        [Description("Shift4Rank")]
        public long? Shift4Rank {
            get;
            set;
        }

        #endregion

        #region 五值

        [Description("Shift5Score")]
        public decimal? Shift5Score {
            get;
            set;
        }

        [Description("Shift5Hours")]
        public int? Shift5Hours {
            get;
            set;
        }

        [Description("Shift5AvgScore")]
        public decimal? Shift5AvgScore {
            get;
            set;
        }

        [Description("Shift5Rank")]
        public long? Shift5Rank {
            get;
            set;
        }

        #endregion


        #endregion
    }
}
