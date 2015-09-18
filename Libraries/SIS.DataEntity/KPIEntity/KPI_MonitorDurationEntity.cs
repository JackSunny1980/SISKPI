using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SIS.DataEntity {

	/// <summary>
	/// 值监盘时间长实体
	/// </summary>
	public class MonitorDurationEntity {

        public MonitorDurationEntity() {
		}

		[Description("UnitID")]
		public String UnitID {
			get;
			set;
		}

		[Description("Shift")]
		public String Shift {
			get;
			set;
		}

		[Description("CheckDate")]
		public DateTime? CheckDate {
			get;
			set;
		}

		[Description("Duration")]
		public decimal? Duration {
			get;
			set;
		}


	}
}
