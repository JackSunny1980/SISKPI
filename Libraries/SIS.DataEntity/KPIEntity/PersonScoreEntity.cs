using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SIS.DataEntity {

	public class PersonScoreEntity {

		public PersonScoreEntity() {
		}


		/// <summary>
		/// 人员编码
		/// </summary>
		[Description("PersonID")]
		public string PersonID {
			get;
			set;
		}

		/// <summary>
		/// 工号
		/// </summary>
		[Description("PersonCode")]
		public string PersonCode {
			get;
			set;
		}

		/// <summary>
		/// 人员姓名
		/// </summary>
		[Description("PersonName")]
		public string PersonName {
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
		/// 岗位
		/// </summary>
		[Description("PositionName")]
		public string PositionName {
			get;
			set;
		}

        /// <summary>
		/// 岗位ID
		/// </summary>
        [Description("PositionID")]
		public string PositionID {
			get;
			set;
		}

		/// <summary>
		/// 权重
		/// </summary>
		[Description("PositionWeight")]
		public decimal? PositionWeight {
			get;
			set;
		}

		/// <summary>
		/// 得分
		/// </summary>
		[Description("Score")]
		public decimal? Score {
			get;
			set;
		}

		/// <summary>
		/// 奖金数
		/// </summary>
		[Description("Bonus")]
		public decimal? Bonus {
			get;
			set;
		}

	}
}
