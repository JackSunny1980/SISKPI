using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SIS.DataEntity {

		/// <summary>
		/// 实体类:ShiftScoreAndBonusEntity
		/// 文件名:ShiftScoreAndBonusEntity.cs
		/// 说  明:值得分与奖金实体
		/// </summary>		
		public class ShiftScoreAndBonusEntity {

			#region 构造方法

			public ShiftScoreAndBonusEntity() {

			}

			#endregion

			#region 属性

			/// <summary>
			/// 年月
			/// </summary>
			[Description("YearMonth")]
			public string YearMonth {
				get;
				set;
			}

			/// <summary>
			/// 机组编码
			/// </summary>
			[Description("UnitID")]
			public string UnitID {
				get;
				set;
			}
		

			/// <summary>
			/// 机组名称
			/// </summary>
			[Description("UnitName")]
			public string UnitName {
				get;
				set;
			}

			/// <summary>
			/// 值次
			/// </summary>
			[Description("Shift")]
			public string Shift {
				get;
				set;
			}

			

			/// <summary>
			/// 经济得分
			/// </summary>
			[Description("ECScore")]
			public decimal? ECScore {
				get;
				set;
			}

			/// <summary>
			/// 安全得分
			/// </summary>
			[Description("SASCore")]
			public decimal? SASCore {
				get;
				set;
			}

			/// <summary>
			/// 总得分
			/// </summary>
			[Description("TotalScore")]
			public decimal? TotalScore {
				get;
				set;
			}

			/// <summary>
			/// 平均得分
			/// </summary>
			[Description("AvgScore")]
			public decimal? AvgScore {
				get;
				set;
			}

			/// <summary>
			/// 奖金
			/// </summary>
			[Description("Bonus")]
			public decimal? Bonus {
				get;
				set;
			}

			/// <summary>
			/// 名次
			/// </summary>
			[Description("OrderNo")]
			public Int64? OrderNo {
				get;
				set;
			}

			#endregion
		}
}
