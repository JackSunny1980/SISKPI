using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SIS.DataEntity {

    /// <summary>
    /// 超限设置实体类
    /// </summary>
    public class OverLimitConfigEntity {
        /// <summary>
        /// 主键
        /// </summary>
        [Description("OverLimitConfigID")]
        public string OverLimitConfigID { get; set; }

        /// <summary>
        /// 标签ID
        /// </summary>
        [Description("TagName")]
        public string TagName { get; set; }

        /// <summary>
        /// 标签名称
        /// </summary>
        public string RealDesc { get; set; }

        /// <summary>
        /// 高一限
        /// </summary>
        [Description("FirstLimitingValue")]
        public decimal? FirstLimitingValue { get; set; }

        /// <summary>
        /// 高二限
        /// </summary>
        [Description("SecondLimitingValue")]
        public decimal? SecondLimitingValue { get; set; }

        /// <summary>
        /// 高三限
        /// </summary>
        [Description("ThirdLimitingValue")]
        public decimal? ThirdLimitingValue { get; set; }

        /// <summary>
        /// 高四限
        /// </summary>
        [Description("FourthLimitingValue")]
        public decimal? FourthLimitingValue { get; set; }

        /// <summary>
        /// 低一限
        /// </summary>
        [Description("LowLimit1Value")]
        public decimal? LowLimit1Value { get; set; }

        /// <summary>
        /// 低二限
        /// </summary>
        [Description("LowLimit2Value")]
        public decimal? LowLimit2Value { get; set; }

        /// <summary>
        /// 低三限
        /// </summary>
        [Description("LowLimit3Value")]
        public decimal? LowLimit3Value { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Description("Comment")]
        public string Comment { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// 测点代码
        /// </summary>
        [Description("TagCode")]
        public String TagCode {
            get;
            set;
        }

        /// <summary>
        /// 测点描述
        /// </summary>
        [Description("TagDesc")]
        public String TagDesc {
            get;
            set;
        }

        /// <summary>
        /// 机组编号
        /// </summary>
        [Description("UnitID")]
        public String UnitID {
            get;
            set;
        }

        #region 为满足实时数据作为限值需求添加的字段

        /// <summary>
        /// 高一限测点配置
        /// </summary>
        [Description("FirstLimitingTag")]
        public string FirstLimitingTag { get; set; }

        /// <summary>
        /// 高二限测点配置
        /// </summary>
        [Description("SecondLimitingTag")]
        public string SecondLimitingTag { get; set; }

        /// <summary>
        /// 高三限测点配置
        /// </summary>
        [Description("ThirdLimitingTag")]
        public string ThirdLimitingTag { get; set; }

        /// <summary>
        /// 高四限测点配置
        /// </summary>
        [Description("FourthLimitingTag")]
        public string FourthLimitingTag { get; set; }

        /// <summary>
        /// 低一限测点配置
        /// </summary>
        [Description("LowLimit1Tag")]
        public string LowLimit1Tag { get; set; }

        /// <summary>
        /// 低二限测点配置
        /// </summary>
        [Description("LowLimit2Tag")]
        public string LowLimit2Tag { get; set; }

        /// <summary>
        /// 低三限测点配置
        /// </summary>
        [Description("LowLimit3Tag")]
        public string LowLimit3Tag { get; set; }

        /// <summary>
        /// 此字段只在DataReaderExtention中自动生成实体是使用
        /// 在报警服务中使用
        /// 临时方案！
        /// </summary>

        [Description("OverLimitComputeType")]
        public int OverLimitComputeType { get; set; }


        [Description("EnumOverLimitComputeType")]
        /// <summary>
        /// 超限计算方式
        /// </summary>
        public OverLimitComputeTypeEnum EnumOverLimitComputeType {
            get;
            set;
        }

        public string OverLimitComputeTypeText {
            get {
                if (EnumOverLimitComputeType == OverLimitComputeTypeEnum.FixedPara) {
                    return "固定值";
                }
                return "实时曲线";
            }
        }

        #endregion
    }

    public enum OverLimitComputeTypeEnum {
        /// <summary>
        /// 固定参数
        /// </summary>
        FixedPara = 0,

        /// <summary>
        /// 实时计算
        /// </summary>
        Realtime = 1
    }
}
