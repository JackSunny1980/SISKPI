using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SIS.DataEntity {

	/// <summary>
	/// 值际竞赛月报表实体
	/// </summary>
	public class ShiftRaceEntity {

		/// <summary>
		/// 值次
		/// </summary>
		[Description("Shift")]
		public string Shift {
			get;
			set;
		}

		/// <summary>
		/// 值次
		/// </summary>
		[Description("UnitID")]
		public string UnitID {
			get;
			set;
		}



		/// <summary>
		/// 值次
		/// </summary>
		[Description("UnitName")]
		public string UnitName {
			get;
			set;
		}

		/// <summary>
		/// 值月得分
		/// </summary>
		[Description("ShiftMonthScore")]
		public decimal? ShiftMonthScore {
			get;
			set;
		}

		/// <summary>
		/// 值月排名
		/// </summary>
		[Description("ShiftMonthOrderNo")]
		public long? ShiftMonthOrderNo {
			get;
			set;
		}

		/// <summary>
		/// 值年得分
		/// </summary>
		[Description("ShiftYearScore")]
		public decimal? ShiftYearScore {
			get;
			set;
		}
		/// <summary>
		/// 值年排名
		/// </summary>
		[Description("ShiftYearOrderNo")]
		public long? ShiftYearOrderNo {
			get;
			set;
		}

		/// <summary>
		/// 机组月得分
		/// </summary>
		[Description("UnitMonthScore")]
		public decimal? UnitMonthScore {
			get;
			set;
		}

		/// <summary>
		/// 机组月排名
		/// </summary>
		[Description("UnitMonthOrderNo")]
		public long? UnitMonthOrderNo {
			get;
			set;
		}

		/// <summary>
		/// 机组年得分
		/// </summary>
		[Description("UnitYearScore")]
		public decimal? UnitYearScore {
			get;
			set;
		}
		/// <summary>
		/// 机组年排名
		/// </summary>
		[Description("UnitYearOrderNo")]
		public long? UnitYearOrderNo {
			get;
			set;
		}
	}
}
