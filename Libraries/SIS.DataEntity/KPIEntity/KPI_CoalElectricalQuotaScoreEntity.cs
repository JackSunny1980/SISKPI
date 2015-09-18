using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SIS.DataEntity
{
    public class CoalElectricalQuotaScoreEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Description("CoalElectricalQuotaID")]
        public string CoalElectricalQuotaID { get; set; }

        /// <summary>
        /// 值别
        /// </summary>
        [Description("ShiftValue")]
        public string ShiftValue { get; set; }

        /// <summary>
        /// 电量
        /// </summary>
        [Description("ElectricalQuantity")]
        public decimal ElectricalQuantity { get; set; }

        /// <summary>
        /// 上煤量
        /// </summary>
        [Description("CoalQuantity")]
        public decimal CoalQuantity { get; set; }

        /// <summary>
        /// 卸煤量
        /// </summary>
        [Description("UnloadingQuantity")]
        public decimal? UnloadingQuantity { get; set; }


        /// <summary>
        /// 当班筒仓料位
        /// </summary>
        [Description("SiloMaterial")]
        public decimal SiloMaterial { get; set; }

        /// <summary>
        /// 年
        /// </summary>
        [Description("Year")]
        public int Year { get; set; }

        /// <summary>
        /// 月
        /// </summary>
        [Description("Month")]
        public int Month { get; set; }

        /// <summary>
        /// 日
        /// </summary>
        [Description("Day")]
        public int Day { get; set; }

        /// <summary>
        /// 得分值
        /// </summary>
        [Description("Score")]
        public decimal? Score { get; set; }


        /// <summary>
        /// 是否配置
        /// </summary>
        [Description("IsSetting")]
        public bool IsSetting { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Description("CreatedDate")]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [Description("ModifiedDate")]
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Description("Remark")]
        public string Remark { get; set; }

        public string TotalCocal { get; set; }

        public string TotalScore { get; set; }

        public int Ranking { get; set; }

    }
}
