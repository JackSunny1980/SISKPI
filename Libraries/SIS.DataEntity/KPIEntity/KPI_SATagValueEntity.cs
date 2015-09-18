using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SIS.DataEntity {
	/// <summary>
	/// 实体类:KPI_SATagValueEntity
	/// 文件名:KPI_SATagValueEntity.cs
	/// 说  明:
	/// </summary>
	public class KPI_SATagValueEntity {

		#region 构造方法

		public KPI_SATagValueEntity() {
		}

		#endregion

		#region 属性

		/// <summary>
		/// 
		/// </summary>
		[Description("SAID")]
		public string SAID {
			get;
			set;
		}

		/// <summary>
		/// 
		/// </summary>
		[Description("SAName")]
		public string SAName {
			get;
			set;
		}

		/// <summary>
		/// 
		/// </summary>
		[Description("CalcDateTime")]
		public DateTime? CalcDateTime {
			get;
			set;
		}

		/// <summary>
		/// 
		/// </summary>
		[Description("Shift")]
		public string Shift {
			get;
			set;
		}

		/// <summary>
		/// 
		/// </summary>
		[Description("Period")]
		public string Period {
			get;
			set;
		}

		/// <summary>
		/// 
		/// </summary>
		[Description("SAScore")]
		public decimal? SAScore {
			get;
			set;
		}

		/// <summary>
		/// 
		/// </summary>
		[Description("TotalCount")]
		public int? TotalCount {
			get;
			set;
		}

		/// <summary>
		/// 
		/// </summary>
		[Description("TotalDuration")]
		public decimal? TotalDuration {
			get;
			set;
		}

		/// <summary>
		/// 年累计得分
		/// </summary>
		[Description("YearScore")]
		public decimal? YearScore {
			get;
			set;
		}

		/// <summary>
		/// 月累计得分
		/// </summary>
		[Description("MonthScore")]
		public decimal? MonthScore {
			get;
			set;
		}

		/// <summary>
		/// 值累计得分
		/// </summary>
		[Description("ShiftScore")]
		public decimal? ShiftScore {
			get;
			set;
		}

		#endregion
	}
}
