using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SIS.DataEntity
{
    public class KPI_UncartQuotaScorebookEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Description("UncartQuotaID")]
        public string UncartQuotaID { get; set; }

        /// <summary>
        /// 指标编码
        /// </summary>
        [Description("QuotaCode")]
        public string QuotaCode { get; set; }

        /// <summary>
        /// 指标名称
        /// </summary>
        [Description("QuotaName")]
        public string QuotaName { get; set; }

        /// <summary>
        /// 指标数量值
        /// </summary>
        [Description("QuotaValue")]
        public int QuotaValue { get; set; }

         /// <summary>
        /// 值别
        /// </summary>
        [Description("ShiftValue")]
        public string ShiftValue { get; set; }

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
        public decimal Score { get; set; }

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


        public decimal Cardinal { get; set; }

        public string TotalScore { get; set; }

        public int Ranking { get; set; }
    }

}
