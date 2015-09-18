using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SIS.DataEntity {

    
    [Serializable]
    public class PlantBonusEntity {

        #region 构造器
        public PlantBonusEntity() {

        }

        #endregion

        #region 属性

        /// <summary>
        /// 值别
        /// </summary>
        [Description("Shift")]
        public String Shift {
            get;
            set;
        }

        /// <summary>
        /// 年月
        /// </summary>
        [Description("YearMonth")]
        public String YearMonth {
            get;
            set;
        }

        /// <summary>
        /// 1#得奖
        /// </summary>
        [Description("Unit1Bonus")]
        public decimal? Unit1Bonus {
            get;
            set;
        }

        /// <summary>
        /// 2#机得奖
        /// </summary>
        [Description("Unit2Bonus")]
        public decimal? Unit2Bonus {
            get;
            set;
        }

        /// <summary>
        /// 全厂奖金
        /// </summary>
        [Description("PlantBonus")]
        public decimal? PlantBonus {
            get;
            set;
        }

        /// <summary>
        /// 出力系数得奖
        /// </summary>
        [Description("PowerCapacity")]
        public decimal? PowerCapacity {
            get;
            set;
        }

        /// <summary>
        /// 奖金合计
        /// </summary>
        [Description("TotalBonus")]
        public decimal? TotalBonus {
            get;
            set;
        }

        /// <summary>
        /// 绩效
        /// </summary>
        [Description("Achievement")]
        public decimal? Achievement {
            get;
            set;
        }
        #endregion
    }
}
