
namespace SIS.DataEntity {
	using System;
	using System.Xml;
	using System.Data;
	using System.Text;
using System.ComponentModel;

	[System.Serializable()]
	[System.Runtime.InteropServices.Guid("53e9801b-6009-4dc8-a554-9923a85743cc")]

	public class KPI_RedoEntity : EntityBase {
		protected String _RDID = null;
		protected int _RDType = int.MinValue;
		protected String _RDKPIID = null;
		protected String _RDName = null;
		protected int _RDIsValid = int.MinValue;
		protected int _RDIsCollect = int.MinValue;
		protected int _RDIsCalced = int.MinValue;
		protected String _RDCalcedTime = null;
		protected String _RDStartTime = null;
		protected String _RDEndTime = null;
		protected String _RDNote = null;
		protected String _RDCreateTime = null;
		protected String _RDModifyTime = null;

		[Description("RDID")]
		public virtual String RDID {
			get {
				return this._RDID;
			}
			set {
				this._RDID = value;
			}
		}

		[Description("RDType")]
		public virtual int RDType {
			get {
				return this._RDType;
			}
			set {
				this._RDType = value;
			}
		}

		[Description("RDKPIID")]
		public virtual String RDKPIID {
			get {
				return this._RDKPIID;
			}
			set {
				this._RDKPIID = value;
			}
		}

		[Description("RDName")]
		public virtual String RDName {
			get {
				return this._RDName;
			}
			set {
				this._RDName = value;
			}
		}

		[Description("RDIsValid")]
		public virtual int RDIsValid {
			get {
				return this._RDIsValid;
			}
			set {
				this._RDIsValid = value;
			}
		}

		[Description("RDIsCollect")]
		public virtual int RDIsCollect {
			get {
				return this._RDIsCollect;
			}
			set {
				this._RDIsCollect = value;
			}
		}

		[Description("RDIsCalced")]
		public virtual int RDIsCalced {
			get {
				return this._RDIsCalced;
			}
			set {
				this._RDIsCalced = value;
			}
		}

		[Description("RDCalcedTime")]
		public virtual String RDCalcedTime {
			get {
				return this._RDCalcedTime;
			}
			set {
				this._RDCalcedTime = value;
			}
		}

		[Description("RDStartTime")]
		public virtual String RDStartTime {
			get {
				return this._RDStartTime;
			}
			set {
				this._RDStartTime = value;
			}
		}

		[Description("RDEndTime")]
		public virtual String RDEndTime {
			get {
				return this._RDEndTime;
			}
			set {
				this._RDEndTime = value;
			}
		}
		[Description("RDNote")]
		public virtual String RDNote {
			get {
				return this._RDNote;
			}
			set {
				this._RDNote = value;
			}
		}

		public virtual String RDCreateTime {
			get {
				return this._RDCreateTime;
			}
			set {
				this._RDCreateTime = value;
			}
		}

		public virtual String RDModifyTime {
			get {
				return this._RDModifyTime;
			}
			set {
				this._RDModifyTime = value;
			}
		}

		public override string InsertSql {
			get {
				System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
				tmpBuild.Append("insert into KPI_Redo(");

				if ((this.RDID == null)) {
				}
				else {
					tmpBuild.Append("RDID");
					tmpBuild.Append(",");
				}

				if ((this.RDType == int.MinValue)) {
				}
				else {
					tmpBuild.Append("RDType");
					tmpBuild.Append(",");
				}

				if ((this.RDKPIID == null)) {
				}
				else {
					tmpBuild.Append("RDKPIID");
					tmpBuild.Append(",");
				}

				if ((this.RDName == null)) {
				}
				else {
					tmpBuild.Append("RDName");
					tmpBuild.Append(",");
				}

				if ((this.RDIsValid == int.MinValue)) {
				}
				else {
					tmpBuild.Append("RDIsValid");
					tmpBuild.Append(",");
				}

				if ((this.RDIsCollect == int.MinValue)) {
				}
				else {
					tmpBuild.Append("RDIsCollect");
					tmpBuild.Append(",");
				}

				if ((this.RDIsCalced == int.MinValue)) {
				}
				else {
					tmpBuild.Append("RDIsCalced");
					tmpBuild.Append(",");
				}

				if ((this.RDCalcedTime == null)) {
				}
				else {
					tmpBuild.Append("RDCalcedTime");
					tmpBuild.Append(",");
				}

				if ((this.RDStartTime == null)) {
				}
				else {
					tmpBuild.Append("RDStartTime");
					tmpBuild.Append(",");
				}

				if ((this.RDEndTime == null)) {
				}
				else {
					tmpBuild.Append("RDEndTime");
					tmpBuild.Append(",");
				}

				if ((this.RDNote == null)) {
				}
				else {
					tmpBuild.Append("RDNote");
					tmpBuild.Append(",");
				}

				if ((this.RDCreateTime == null)) {
				}
				else {
					tmpBuild.Append("RDCreateTime");
					tmpBuild.Append(",");
				}

				if ((this.RDModifyTime == null)) {
				}
				else {
					tmpBuild.Append("RDModifyTime");
					tmpBuild.Append(",");
				}


				///////////////////////////////////////////////////////////////////////////////////////


				if ((tmpBuild[(tmpBuild.Length - 1)] == ',')) {
					tmpBuild.Remove((tmpBuild.Length - 1), 1);
				}

				tmpBuild.Append(") values(");

				if ((this.RDID == null)) {
				}
				else {
					tmpBuild.Append("'" + RDID + "'");
					tmpBuild.Append(",");
				}

				if ((this.RDType == int.MinValue)) {
				}
				else {
					tmpBuild.Append(RDType.ToString());
					tmpBuild.Append(",");
				}

				if ((this.RDKPIID == null)) {
				}
				else {
					tmpBuild.Append("'" + RDKPIID + "'");
					tmpBuild.Append(",");
				}

				if ((this.RDName == null)) {
				}
				else {
					tmpBuild.Append("'" + RDName + "'");
					tmpBuild.Append(",");
				}

				if ((this.RDIsValid == int.MinValue)) {
				}
				else {
					tmpBuild.Append(RDIsValid.ToString());
					tmpBuild.Append(",");
				}
				if ((this.RDIsCollect == int.MinValue)) {
				}
				else {
					tmpBuild.Append(RDIsCollect.ToString());
					tmpBuild.Append(",");
				}

				if ((this.RDIsCalced == int.MinValue)) {
				}
				else {
					tmpBuild.Append(RDIsCalced.ToString());
					tmpBuild.Append(",");
				}

				if ((this.RDCalcedTime == null)) {
				}
				else {
					tmpBuild.Append("'" + RDCalcedTime + "'");
					tmpBuild.Append(",");
				}

				if ((this.RDStartTime == null)) {
				}
				else {
					tmpBuild.Append("'" + RDStartTime + "'");
					tmpBuild.Append(",");
				}

				if ((this.RDEndTime == null)) {
				}
				else {
					tmpBuild.Append("'" + RDEndTime + "'");
					tmpBuild.Append(",");
				}

				if ((this.RDNote == null)) {
				}
				else {
					tmpBuild.Append("'" + RDNote + "'");
					tmpBuild.Append(",");
				}

				if ((this.RDCreateTime == null)) {
				}
				else {
					tmpBuild.Append("'" + RDCreateTime + "'");
					tmpBuild.Append(",");
				}

				if ((this.RDModifyTime == null)) {
				}
				else {
					tmpBuild.Append("'" + RDModifyTime + "'");
					tmpBuild.Append(",");
				}

				if ((tmpBuild[(tmpBuild.Length - 1)] == ',')) {
					tmpBuild.Remove((tmpBuild.Length - 1), 1);
				}

				tmpBuild.Append(")");

				string __tmpSql = tmpBuild.ToString();

				return __tmpSql;
			}
		}


		public override string UpdateSql {
			get {
				System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
				tmpBuild.Append("update KPI_Redo set ");
				if ((this.RDID == null)) {
				}
				else {
					tmpBuild.Append("RDID='" + RDID + "'");
					tmpBuild.Append(",");
				}

				if ((this.RDType == int.MinValue)) {
				}
				else {
					tmpBuild.Append("RDType=" + RDType.ToString());
					tmpBuild.Append(",");
				}


				if ((this.RDKPIID == null)) {
				}
				else {
					tmpBuild.Append("RDKPIID='" + RDKPIID + "'");
					tmpBuild.Append(",");
				}

				if ((this.RDName == null)) {
				}
				else {
					tmpBuild.Append("RDName='" + RDName + "'");
					tmpBuild.Append(",");
				}

				if ((this.RDIsValid == int.MinValue)) {
				}
				else {
					tmpBuild.Append("RDIsValid=" + RDIsValid.ToString());
					tmpBuild.Append(",");
				}

				if ((this.RDIsCollect == int.MinValue)) {
				}
				else {
					tmpBuild.Append("RDIsCollect=" + RDIsCollect.ToString());
					tmpBuild.Append(",");
				}

				if ((this.RDIsCalced == int.MinValue)) {
				}
				else {
					tmpBuild.Append("RDIsCalced=" + RDIsCalced.ToString());
					tmpBuild.Append(",");
				}

				if ((this.RDCalcedTime == null)) {
				}
				else {
					tmpBuild.Append("RDCalcedTime='" + RDCalcedTime + "'");
					tmpBuild.Append(",");
				}

				if ((this.RDStartTime == null)) {
				}
				else {
					tmpBuild.Append("RDStartTime='" + RDStartTime + "'");
					tmpBuild.Append(",");
				}

				if ((this.RDEndTime == null)) {
				}
				else {
					tmpBuild.Append("RDEndTime='" + RDEndTime + "'");
					tmpBuild.Append(",");
				}

				if ((this.RDNote == null)) {
				}
				else {
					tmpBuild.Append("RDNote='" + RDNote + "'");
					tmpBuild.Append(",");
				}

				if ((this.RDCreateTime == null)) {
				}
				else {
					tmpBuild.Append("RDCreateTime='" + RDCreateTime + "'");
					tmpBuild.Append(",");
				}

				if ((this.RDModifyTime == null)) {
				}
				else {
					tmpBuild.Append("RDModifyTime='" + RDModifyTime + "'");
					tmpBuild.Append(",");
				}

				if ((tmpBuild[(tmpBuild.Length - 1)] == ',')) {
					tmpBuild.Remove((tmpBuild.Length - 1), 1);
				}

				tmpBuild.Append(" where ");
				tmpBuild.Append("RDID='" + RDID + "'");

				string __tmpSql = tmpBuild.ToString();
				return __tmpSql;
			}
		}


		public override string DeleteSql {
			get {
				System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
				tmpBuild.Append("delete KPI_Redo ");
				tmpBuild.Append(" where ");
				tmpBuild.Append("RDID='" + RDID + "'");

				string __tmpSql = tmpBuild.ToString();
				return __tmpSql;
			}
		}

		public override string SelectSql {
			get {
				System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
				tmpBuild.Append("select ");
				tmpBuild.Append("RDID");
				tmpBuild.Append(",");
				tmpBuild.Append("RDType");
				tmpBuild.Append(",");
				tmpBuild.Append("RDKPIID");
				tmpBuild.Append(",");
				tmpBuild.Append("RDName");
				tmpBuild.Append(",");
				tmpBuild.Append("RDIsValid");
				tmpBuild.Append(","); ;
				tmpBuild.Append("RDIsCollect");
				tmpBuild.Append(",");
				tmpBuild.Append("RDIsCalced");
				tmpBuild.Append(",");
				tmpBuild.Append("RDCalcedTime");
				tmpBuild.Append(",");
				tmpBuild.Append("RDStartTime");
				tmpBuild.Append(",");
				tmpBuild.Append("RDEndTime");
				tmpBuild.Append(",");
				tmpBuild.Append("RDNote");
				tmpBuild.Append(",");
				tmpBuild.Append("RDCreateTime");
				tmpBuild.Append(",");
				tmpBuild.Append("RDModifyTime");

				tmpBuild.Append(" from KPI_Redo");
				tmpBuild.Append(" where ");
				tmpBuild.Append("RDID='" + RDID + "'");

				string __tmpSql = tmpBuild.ToString();
				return __tmpSql;
			}
		}

		public override bool DrToMember(System.Data.DataRow dr) {
			try {
				if (dr["RDID"] == System.DBNull.Value) {
				}
				else {
					this.RDID = dr["RDID"].ToString();
				}

				if (dr["RDType"] == System.DBNull.Value) {
				}
				else {
					this.RDType = int.Parse(dr["RDType"].ToString());
				}

				if (dr["RDKPIID"] == System.DBNull.Value) {
				}
				else {
					this.RDKPIID = dr["RDKPIID"].ToString();
				}

				if (dr["RDName"] == System.DBNull.Value) {
				}
				else {
					this.RDName = dr["RDName"].ToString();
				}

				if (dr["RDIsValid"] == System.DBNull.Value) {
				}
				else {
					this.RDIsValid = int.Parse(dr["RDIsValid"].ToString());
				}

				if (dr["RDIsCollect"] == System.DBNull.Value) {
				}
				else {
					this.RDIsCollect = int.Parse(dr["RDIsCollect"].ToString());
				}

				if (dr["RDIsCalced"] == System.DBNull.Value) {
				}
				else {
					this.RDIsCalced = int.Parse(dr["RDIsCalced"].ToString());
				}

				if (dr["RDCalcedTime"] == System.DBNull.Value) {
				}
				else {
					this.RDCalcedTime = dr["RDCalcedTime"].ToString();
				}

				if (dr["RDStartTime"] == System.DBNull.Value) {
				}
				else {
					this.RDStartTime = dr["RDStartTime"].ToString();
				}
				if (dr["RDEndTime"] == System.DBNull.Value) {
				}
				else {
					this.RDEndTime = dr["RDEndTime"].ToString();
				}

				if (dr["RDNote"] == System.DBNull.Value) {
				}
				else {
					this.RDNote = dr["RDNote"].ToString();
				}

				if (dr["RDCreateTime"] == System.DBNull.Value) {
				}
				else {
					this.RDCreateTime = dr["RDCreateTime"].ToString();
				}


				if (dr["RDModifyTime"] == System.DBNull.Value) {
				}
				else {
					this.RDModifyTime = dr["RDModifyTime"].ToString();
				}


			}
			catch (System.Exception) {
				// 如果有必要,请处理你的异常代码
				return false;
			}
			finally {
				// 异常的finally代码
			}
			return true;
		}
	}
}
