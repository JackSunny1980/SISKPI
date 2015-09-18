using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SIS.DataEntity {

    /// <summary>
    /// 实体类:ManagementScoreEntity
    /// 文件名:KPI_ManagementScoreEntity.cs
    /// 说  明:日常管理考核得分
    /// </summary>
    public class ManagementScoreEntity {

        #region 构造方法

        public ManagementScoreEntity() {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 个人编码
        /// </summary>
        [Description("PersonID")]
        public string PersonID {
            get;
            set;
        }

        public String PersonName {
            get;
            set;
        }

        public String PositionName {
            get;
            set;
        }

        /// <summary>
        /// 考核日期
        /// </summary>
        [Description("CheckDate")]
        public DateTime CheckDate {
            get;
            set;
        }

        /// <summary>
        /// 值别
        /// </summary>
        [Description("Shift")]
        public int Shift {
            get;
            set;
        }

        /// <summary>
        /// 指标编码
        /// </summary>
        [Description("TagID")]
        public string TagID {
            get;
            set;
        }

        /// <summary>
        /// 得分
        /// </summary>
        [Description("Score")]
        public decimal Score {
            get;
            set;
        }

        /// <summary>
        /// 是否有效
        /// </summary>
        [Description("IsValid")]
        public bool IsValid {
            get;
            set;
        }

        /// <summary>
        /// 指标得分
        /// </summary>
        [Description("TagScore")]
        public decimal TagScore {
            get;
            set;
        }

        /// <summary>
        /// 考核系数
        /// </summary>
        [Description("Rate")]
        public decimal Rate {
            get;
            set;
        }

        #endregion
    }
}
