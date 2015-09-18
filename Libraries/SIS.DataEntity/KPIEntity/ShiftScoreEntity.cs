using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SIS.DataEntity {
	/// <summary>
	/// 实体类:ShiftScoreEntity
	/// 文件名:ShiftScoreEntity.cs
	/// 说  明:值次得分实体
	/// </summary>
	public class ShiftScoreEntity{ 

		#region 构造方法

		public ShiftScoreEntity() {

		}

		#endregion

		#region 属性


		/// <summary>
		/// 考核年月
		/// </summary>
		[Description("CheckYearMonth")]
		public string CheckYearMonth {
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
		/// 监盘总时间
		/// </summary>
		[Description("TotalHours")]
		public decimal? TotalHours {
			get;
			set;
		}

		/// <summary>
		/// 平均得分/小时
		/// </summary>
		[Description("AvgScore")]
		public decimal? AvgScore {
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
