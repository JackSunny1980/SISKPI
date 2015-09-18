using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SIS.DataEntity {
    /// <summary>
    /// 实体类:KPI_InputTagEntity
    /// 文件名:KPI_InputTagEntity.cs
    /// 说  明:手动录入指标管理
    /// </summary>
    public class InputTagEntity:EntityBase {

        #region 构造方法

        public InputTagEntity() {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 指标编码
        /// </summary>
        [Description("InputID")]
        public string InputID {
            get;
            set;
        }

        /// <summary>
        /// 指标代码
        /// </summary>
        [Description("InputCode")]
        public string InputCode {
            get;
            set;
        }

        /// <summary>
        /// 指标名称
        /// </summary>
        [Description("InputDesc")]
        public string InputDesc {
            get;
            set;
        }

        /// <summary>
        /// 指标单位
        /// </summary>
        [Description("InputEngunit")]
        public string InputEngunit {
            get;
            set;
        }

        /// <summary>
        /// 指标序号
        /// </summary>
        [Description("InputIndex")]
        public int? InputIndex {
            get;
            set;
        }

        /// <summary>
        /// 指标类别代码
        /// </summary>
        [Description("InputType")]
        public int? InputType {
            get;
            set;
        }

        /// <summary>
        /// 指标类别
        /// </summary>
        [Description("TagCategory")]
        public String TagCategory {
            get;
            set;
        }

        #endregion

        public override bool DrToMember(System.Data.DataRow dr) {
            return false;
        }
    }
}
