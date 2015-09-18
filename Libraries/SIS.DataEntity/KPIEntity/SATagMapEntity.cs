using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SIS.DataEntity {


	/// <summary>
	/// 实体类:SATagMapEntity
	/// 文件名:SATagMapEntity.cs
	/// 说  明:安全指标与测点对应关系表
	/// </summary>
	public class SATagMapEntity {

		#region 构造方法

		public SATagMapEntity() {
		}

		#endregion

		#region 属性

		/// <summary>
		/// 安全指标编码
		/// </summary>
		[Description("SAID")]
		public string SAID {
			get;
			set;
		}

		[Description("SAName")]
		public String SAName {
			get;
			set;
		}

		/// <summary>
		/// 测点编码
		/// </summary>
		[Description("RealID")]
		public string RealID {
			get;
			set;
		}

		[Description("RealCode")]
		public String RealCode {
			get;
			set;
		}

		[Description("RealDesc")]
		public String RealDesc {
			get;
			set;
		}

		#endregion
	}
}
