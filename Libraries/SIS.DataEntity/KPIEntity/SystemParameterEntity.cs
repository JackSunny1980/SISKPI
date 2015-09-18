using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SIS.DataEntity {


	/// <summary>
	/// 实体类:SystemParameterEntity
	/// 文件名:SystemParameterEntity.cs
	/// 说  明:
	/// </summary>
	public class SystemParameterEntity {

		#region 构造方法

		public SystemParameterEntity() {
		}

		#endregion

		#region 属性

		/// <summary>
		/// 
		/// </summary>
		[Description("SysID")]
		public string SysID {
			get;
			set;
		}

		/// <summary>
		/// 
		/// </summary>
		[Description("SysName")]
		public string SysName {
			get;
			set;
		}

		/// <summary>
		/// 
		/// </summary>
		[Description("SysDesc")]
		public string SysDesc {
			get;
			set;
		}

		/// <summary>
		/// 
		/// </summary>
		[Description("SysEngunit")]
		public string SysEngunit {
			get;
			set;
		}

		/// <summary>
		/// 
		/// </summary>
		[Description("SysIsValid")]
		public int? SysIsValid {
			get;
			set;
		}

		/// <summary>
		/// 
		/// </summary>
		[Description("SysValue")]
		public string SysValue {
			get;
			set;
		}

		/// <summary>
		/// 
		/// </summary>
		[Description("SysNote")]
		public string SysNote {
			get;
			set;
		}


		public string SysCode {
			get;
			set;
		}

		public string SysValue2 {
			get;
			set;
		}

		#endregion
	}
}
