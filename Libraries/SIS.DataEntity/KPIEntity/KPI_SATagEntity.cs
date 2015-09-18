
namespace SIS.DataEntity {
	using System;
	using System.Xml;
	using System.Data;
	using System.Text;
	using System.ComponentModel;

	[System.Serializable()]
	[System.Runtime.InteropServices.Guid("50e9801b-6409-4dc8-a554-9923a85743dc")]

	public class KPI_SATagEntity : EntityBase {
		protected String _SAID = null;
		protected String _UnitID = null;
		protected String _SeqID = null;
		protected String _KpiID = null;
		protected String _EngunitID = null;
		protected String _CycleID = null;
		protected String _CycleName = null;

		protected String _SACode = null;
		protected String _SAName = null;
		protected String _SADesc = null;
		protected int _SAIndex = int.MinValue;
		protected String _SAWeb = null;
		protected int _SAIsValid = int.MinValue;
		protected int _SAIsCalc = int.MinValue;
		protected int _SAIsDisplay = int.MinValue;
		protected int _SAIsTotal = int.MinValue;

        //protected int _SAType = int.MinValue;
		protected String _SAFilterExp = null;
		protected String _SACalcExp = null;

		protected String _SANote = null;
		protected String _SACreateTime = null;
		protected String _SAModifyTime = null;
		
		protected String _SACountExpression = null;
        protected String _SADurationExpression = null;

        

		[Description("SAID")]
		public virtual String SAID {
			get {
				return this._SAID;
			}
			set {
				this._SAID = value;
			}
		}

		[Description("UnitID")]
		public virtual String UnitID {
			get {
				return this._UnitID;
			}
			set {
				this._UnitID = value;
			}
		}

		[Description("SeqID")]
		public virtual String SeqID {
			get {
				return this._SeqID;
			}
			set {
				this._SeqID = value;
			}
		}

		[Description("KpiID")]
		public virtual String KpiID {
			get {
				return this._KpiID;
			}
			set {
				this._KpiID = value;
			}
		}

		[Description("EngunitID")]
		public virtual String EngunitID {
			get {
				return this._EngunitID;
			}
			set {
				this._EngunitID = value;
			}
		}


		[Description("CycleID")]
		public virtual String CycleID {
			get {
				return this._CycleID;
			}
			set {
				this._CycleID = value;
			}
		}

		[Description("CycleName")]
		public virtual String CycleName {
			get {
				return this._CycleName;
			}
			set {
				this._CycleName = value;
			}
		}

		[Description("CycleValue")]
		public virtual int CycleValue {
			get;
			set;
		}


		[Description("SACode")]
		public virtual String SACode {
			get {
				return this._SACode;
			}
			set {
				this._SACode = value;
			}
		}

		[Description("SAName")]
		public virtual String SAName {
			get {
				return this._SAName;
			}
			set {
				this._SAName = value;
			}
		}

		[Description("SADesc")]
		public virtual String SADesc {
			get {
				return this._SADesc;
			}
			set {
				this._SADesc = value;
			}
		}

		[Description("SAIndex")]
		public virtual int SAIndex {
			get {
				return this._SAIndex;
			}
			set {
				this._SAIndex = value;
			}
		}

		[Description("SAWeb")]
		public virtual String SAWeb {
			get {
				return this._SAWeb;
			}
			set {
				this._SAWeb = value;
			}
		}

		[Description("SAIsValid")]
		public virtual int SAIsValid {
			get {
				return this._SAIsValid;
			}
			set {
				this._SAIsValid = value;
			}
		}

		[Description("SAIsCalc")]
		public virtual int SAIsCalc {
			get {
				return this._SAIsCalc;
			}
			set {
				this._SAIsCalc = value;
			}
		}

		[Description("SAIsDisplay")]
		public virtual int SAIsDisplay {
			get {
				return this._SAIsDisplay;
			}
			set {
				this._SAIsDisplay = value;
			}
		}

		[Description("SAIsTotal")]
		public virtual int SAIsTotal {
			get {
				return this._SAIsTotal;
			}
			set {
				this._SAIsTotal = value;
			}
		}


		[Description("SAFilterExp")]
		public virtual String SAFilterExp {
			get {
				return this._SAFilterExp;
			}
			set {
				this._SAFilterExp = value;
			}
		}

		[Description("SACalcExp")]
		public virtual String SACalcExp {
			get {
				return this._SACalcExp;
			}
			set {
				this._SACalcExp = value;
			}
		}

		[Description("SACountExpression")]
		public virtual String SACountExpression {
            get
            {
                return this._SACountExpression;
            }
            set
            {
                this._SACountExpression = value;
            }
		}

		[Description("SADurationExpression")]
		public virtual String SADurationExpression {
            get
            {
                return this._SADurationExpression;
            }
            set
            {
                this._SADurationExpression = value;
            }
		}

        //[Description("SAType")]
        //public virtual int SAType {
        //    get {
        //        return this._SAType;
        //    }
        //    set {
        //        this._SAType = value;
        //    }
        //}

		[Description("SANote")]
		public virtual String SANote {
			get {
				return this._SANote;
			}
			set {
				this._SANote = value;
			}
		}

		public virtual String SACreateTime {
			get {
				return this._SACreateTime;
			}
			set {
				this._SACreateTime = value;
			}
		}

		public virtual String SAModifyTime {
			get {
				return this._SAModifyTime;
			}
			set {
				this._SAModifyTime = value;
			}
		}

		public override string InsertSql {
			get {
				System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
				tmpBuild.Append("insert into KPI_SATag (");

				if ((this.SAID == null)) {
				}
				else {
					tmpBuild.Append("SAID");
					tmpBuild.Append(",");
				}

				if ((this.UnitID == null)) {
				}
				else {
					tmpBuild.Append("UnitID");
					tmpBuild.Append(",");
				}

				if ((this.SeqID == null)) {
				}
				else {
					tmpBuild.Append("SeqID");
					tmpBuild.Append(",");
				}

				if ((this.KpiID == null)) {
				}
				else {
					tmpBuild.Append("KpiID");
					tmpBuild.Append(",");
				}

				if ((this.EngunitID == null)) {
				}
				else {
					tmpBuild.Append("EngunitID");
					tmpBuild.Append(",");
				}

				if ((this.CycleID == null)) {
				}
				else {
					tmpBuild.Append("CycleID");
					tmpBuild.Append(",");
				}

				if ((this.SACode == null)) {
				}
				else {
					tmpBuild.Append("SACode");
					tmpBuild.Append(",");
				}

				if ((this.SAName == null)) {
				}
				else {
					tmpBuild.Append("SAName");
					tmpBuild.Append(",");
				}

				if ((this.SADesc == null)) {
				}
				else {
					tmpBuild.Append("SADesc");
					tmpBuild.Append(",");
				}

				if ((this.SAIndex == int.MinValue)) {
				}
				else {
					tmpBuild.Append("SAIndex");
					tmpBuild.Append(",");
				}

				if ((this.SAWeb == null)) {
				}
				else {
					tmpBuild.Append("SAWeb");
					tmpBuild.Append(",");
				}

				if ((this.SAIsValid == int.MinValue)) {
				}
				else {
					tmpBuild.Append("SAIsValid");
					tmpBuild.Append(",");
				}

				if ((this.SAIsDisplay == int.MinValue)) {
				}
				else {
					tmpBuild.Append("SAIsDisplay");
					tmpBuild.Append(",");
				}

				if ((this.SAIsTotal == int.MinValue)) {
				}
				else {
					tmpBuild.Append("SAIsTotal");
					tmpBuild.Append(",");
				}

				if ((this.SAFilterExp == null)) {
				}
				else {
					tmpBuild.Append("SAFilterExp");
					tmpBuild.Append(",");
				}

				if ((this.SACalcExp == null)) {
				}
				else {
					tmpBuild.Append("SACalcExp");
					tmpBuild.Append(",");
				}

                //if ((this.SAType == int.MinValue)) {
                //}
                //else {
                //    tmpBuild.Append("SAType");
                //    tmpBuild.Append(",");
                //}

				if ((this.SANote == null)) {
				}
				else {
					tmpBuild.Append("SANote");
					tmpBuild.Append(",");
				}

				if ((this.SACreateTime == null)) {
				}
				else {
					tmpBuild.Append("SACreateTime");
					tmpBuild.Append(",");
				}

				if ((this.SAModifyTime == null)) {
				}
				else {
					tmpBuild.Append("SAModifyTime");
					tmpBuild.Append(",");
				}

				if (!String.IsNullOrEmpty(SACountExpression)) {
					tmpBuild.Append("SACountExpression");
					tmpBuild.Append(",");
				}

				if (!String.IsNullOrEmpty(SADurationExpression)) {
					tmpBuild.Append("SADurationExpression");
					tmpBuild.Append(",");
				}

				///////////////////////////////////////////////////////////////////////////////////////


				if ((tmpBuild[(tmpBuild.Length - 1)] == ',')) {
					tmpBuild.Remove((tmpBuild.Length - 1), 1);
				}

				tmpBuild.Append(") values(");

				if ((this.SAID == null)) {
				}
				else {
					tmpBuild.Append("'" + SAID + "'");
					tmpBuild.Append(",");
				}

				if ((this.UnitID == null)) {
				}
				else {
					tmpBuild.Append("'" + UnitID + "'");
					tmpBuild.Append(",");
				}

				if ((this.SeqID == null)) {
				}
				else {
					tmpBuild.Append("'" + SeqID + "'");
					tmpBuild.Append(",");
				}

				if ((this.KpiID == null)) {
				}
				else {
					tmpBuild.Append("'" + KpiID + "'");
					tmpBuild.Append(",");
				}

				if ((this.EngunitID == null)) {
				}
				else {
					tmpBuild.Append("'" + EngunitID + "'");
					tmpBuild.Append(",");
				}

				if ((this.CycleID == null)) {
				}
				else {
					tmpBuild.Append("'" + CycleID + "'");
					tmpBuild.Append(",");
				}

				if ((this.SACode == null)) {
				}
				else {
					tmpBuild.Append("'" + SACode + "'");
					tmpBuild.Append(",");
				}

				if ((this.SAName == null)) {
				}
				else {
					tmpBuild.Append("'" + SAName + "'");
					tmpBuild.Append(",");
				}

				if ((this.SADesc == null)) {
				}
				else {
					tmpBuild.Append("'" + SADesc + "'");
					tmpBuild.Append(",");
				}

				if ((this.SAIndex == int.MinValue)) {
				}
				else {
					tmpBuild.Append(SAIndex.ToString());
					tmpBuild.Append(",");
				}

				if ((this.SAWeb == null)) {
				}
				else {
					tmpBuild.Append("'" + SAWeb + "'");
					tmpBuild.Append(",");
				}

				if ((this.SAIsValid == int.MinValue)) {
				}
				else {
					tmpBuild.Append(SAIsValid.ToString());
					tmpBuild.Append(",");
				}

				if ((this.SAIsDisplay == int.MinValue)) {
				}
				else {
					tmpBuild.Append(SAIsDisplay.ToString());
					tmpBuild.Append(",");
				}

				if ((this.SAIsTotal == int.MinValue)) {
				}
				else {
					tmpBuild.Append(SAIsTotal.ToString());
					tmpBuild.Append(",");
				}

				if ((this.SAFilterExp == null)) {
				}
				else {
					tmpBuild.Append("'" + SAFilterExp + "'");
					tmpBuild.Append(",");
				}

				if ((this.SACalcExp == null)) {
				}
				else {
					tmpBuild.Append("'" + SACalcExp + "'");
					tmpBuild.Append(",");
				}


                //if ((this.SAType == int.MinValue)) {
                //}
                //else {
                //    tmpBuild.Append(SAType.ToString());
                //    tmpBuild.Append(",");
                //}


				if ((this.SANote == null)) {
				}
				else {
					tmpBuild.Append("'" + SANote + "'");
					tmpBuild.Append(",");
				}

				if ((this.SACreateTime == null)) {
				}
				else {
					tmpBuild.Append("'" + SACreateTime + "'");
					tmpBuild.Append(",");
				}

				if ((this.SAModifyTime == null)) {
				}
				else {
					tmpBuild.Append("'" + SAModifyTime + "'");
					tmpBuild.Append(",");
				}

				if ((this.SACountExpression == null)) {
				}
				else {
					tmpBuild.Append("'" + SACountExpression + "'");
					tmpBuild.Append(",");
				}

				if ((this.SADurationExpression == null)) {
				}
				else {
					tmpBuild.Append("'" + SADurationExpression + "'");
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
				tmpBuild.Append("update KPI_SATag set ");

				if ((this.SAID == null)) {
				}
				else {
					tmpBuild.Append("SAID='" + SAID + "'");
					tmpBuild.Append(",");
				}

				if ((this.UnitID == null)) {
				}
				else {
					tmpBuild.Append("UnitID='" + UnitID + "'");
					tmpBuild.Append(",");
				}

				if ((this.SeqID == null)) {
				}
				else {
					tmpBuild.Append("SeqID='" + SeqID + "'");
					tmpBuild.Append(",");
				}

				if ((this.KpiID == null)) {
				}
				else {
					tmpBuild.Append("KpiID='" + KpiID + "'");
					tmpBuild.Append(",");
				}

				if ((this.EngunitID == null)) {
				}
				else {
					tmpBuild.Append("EngunitID='" + EngunitID + "'");
					tmpBuild.Append(",");
				}

				if ((this.CycleID == null)) {
				}
				else {
					tmpBuild.Append("CycleID='" + CycleID + "'");
					tmpBuild.Append(",");
				}

				if ((this.SACode == null)) {
				}
				else {
					tmpBuild.Append("SACode='" + SACode + "'");
					tmpBuild.Append(",");
				}

				if ((this.SAName == null)) {
				}
				else {
					tmpBuild.Append("SAName='" + SAName + "'");
					tmpBuild.Append(",");
				}

				if ((this.SADesc == null)) {
				}
				else {
					tmpBuild.Append("SADesc='" + SADesc + "'");
					tmpBuild.Append(",");
				}

				if ((this.SAIndex == int.MinValue)) {
				}
				else {
					tmpBuild.Append("SAIndex=" + SAIndex.ToString());
					tmpBuild.Append(",");
				}

				if ((this.SAWeb == null)) {
				}
				else {
					tmpBuild.Append("SAWeb='" + SAWeb + "'");
					tmpBuild.Append(",");
				}

				if ((this.SAIsValid == int.MinValue)) {
				}
				else {
					tmpBuild.Append("SAIsValid=" + SAIsValid.ToString());
					tmpBuild.Append(",");
				}

				if ((this.SAIsDisplay == int.MinValue)) {
				}
				else {
					tmpBuild.Append("SAIsDisplay=" + SAIsDisplay.ToString());
					tmpBuild.Append(",");
				}

				if ((this.SAIsTotal == int.MinValue)) {
				}
				else {
					tmpBuild.Append("SAIsTotal=" + SAIsTotal.ToString());
					tmpBuild.Append(",");
				}

				if ((this.SAFilterExp == null)) {
				}
				else {
					tmpBuild.Append("SAFilterExp='" + SAFilterExp + "'");
					tmpBuild.Append(",");
				}

				if ((this.SACalcExp == null)) {
				}
				else {
					tmpBuild.Append("SACalcExp='" + SACalcExp + "'");
					tmpBuild.Append(",");
				}

                //if ((this.SAType == int.MinValue)) {
                //}
                //else {
                //    tmpBuild.Append("SAType=" + SAType.ToString());
                //    tmpBuild.Append(",");
                //}

				if ((this.SANote == null)) {
				}
				else {
					tmpBuild.Append("SANote='" + SANote + "'");
					tmpBuild.Append(",");
				}

				if ((this.SACreateTime == null)) {
				}
				else {
					tmpBuild.Append("SACreateTime='" + SACreateTime + "'");
					tmpBuild.Append(",");
				}

				if ((this.SAModifyTime == null)) {
				}
				else {
					tmpBuild.Append("SAModifyTime='" + SAModifyTime + "'");
					tmpBuild.Append(",");
				}

				if ((this.SACountExpression == null)) {
				}
				else {
					tmpBuild.Append("SACountExpression='" + SACountExpression + "'");
					tmpBuild.Append(",");
				}

				if ((this.SADurationExpression == null)) {
				}
				else {
					tmpBuild.Append("SADurationExpression='" + SADurationExpression + "'");
					tmpBuild.Append(",");
				}

				if ((tmpBuild[(tmpBuild.Length - 1)] == ',')) {
					tmpBuild.Remove((tmpBuild.Length - 1), 1);
				}

				tmpBuild.Append(" where ");
				tmpBuild.Append("SAID='" + SAID + "'");

				string __tmpSql = tmpBuild.ToString();
				return __tmpSql;
			}
		}

		public override string DeleteSql {
			get {
				System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
				tmpBuild.Append("delete KPI_SATag");
				tmpBuild.Append(" where ");
				tmpBuild.Append("SAID='" + SAID + "'");

				string __tmpSql = tmpBuild.ToString();
				return __tmpSql;
			}
		}

		public override string SelectSql {
			get {
				System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
				tmpBuild.Append("select ");
				tmpBuild.Append("SAID");
				tmpBuild.Append(",");
				tmpBuild.Append("UnitID");
				tmpBuild.Append(",");
				tmpBuild.Append("SeqID");
				tmpBuild.Append(",");
				tmpBuild.Append("KpiID");
				tmpBuild.Append(",");
				tmpBuild.Append("EngunitID");
				tmpBuild.Append(",");
				tmpBuild.Append("CycleID");
				tmpBuild.Append(",");
				tmpBuild.Append("SACode");
				tmpBuild.Append(",");
				tmpBuild.Append("SAName");
				tmpBuild.Append(",");
				tmpBuild.Append("SADesc");
				tmpBuild.Append(",");
				tmpBuild.Append("SAIndex");
				tmpBuild.Append(",");
				tmpBuild.Append("SAWeb");
				tmpBuild.Append(",");
				tmpBuild.Append("SAIsValid");
				tmpBuild.Append(",");
				tmpBuild.Append("SAIsCalc");
				tmpBuild.Append(",");
				tmpBuild.Append("SAIsDisplay");
				tmpBuild.Append(",");
				tmpBuild.Append("SAIsTotal");
				tmpBuild.Append(",");
				tmpBuild.Append("SAFilterExp");
				tmpBuild.Append(",");
				tmpBuild.Append("SACalcExp");
				tmpBuild.Append(",");
                //tmpBuild.Append("SAType");
                //tmpBuild.Append(",");
				tmpBuild.Append("SANote");
				tmpBuild.Append(",");
				tmpBuild.Append("SACreateTime");
				tmpBuild.Append(",");
				tmpBuild.Append("SAModifyTime");
				tmpBuild.Append(",");
				tmpBuild.Append("SACountExpression");
				tmpBuild.Append(",");
				tmpBuild.Append("SADurationExpression");

				tmpBuild.Append(" from KPI_SATag ");
				tmpBuild.Append(" where ");
				tmpBuild.Append("SAID='" + SAID + "'");

				string __tmpSql = tmpBuild.ToString();
				return __tmpSql;
			}
		}

		public override bool DrToMember(System.Data.DataRow dr) {
			try {
				if (dr["SAID"] == System.DBNull.Value) {
				}
				else {
					this.SAID = dr["SAID"].ToString();
				}

				if (dr["UnitID"] == System.DBNull.Value) {
				}
				else {
					this.UnitID = dr["UnitID"].ToString();
				}

				if (dr["SeqID"] == System.DBNull.Value) {
				}
				else {
					this.SeqID = dr["SeqID"].ToString();
				}

				if (dr["KpiID"] == System.DBNull.Value) {
				}
				else {
					this.KpiID = dr["KpiID"].ToString();
				}

				if (dr["EngunitID"] == System.DBNull.Value) {
				}
				else {
					this.EngunitID = dr["EngunitID"].ToString();
				}

				if (dr["CycleID"] == System.DBNull.Value) {
				}
				else {
					this.CycleID = dr["CycleID"].ToString();
				}

				if (dr["SACode"] == System.DBNull.Value) {
				}
				else {
					this.SACode = dr["SACode"].ToString();
				}

				if (dr["SAName"] == System.DBNull.Value) {
				}
				else {
					this.SAName = dr["SAName"].ToString();
				}

				if (dr["SADesc"] == System.DBNull.Value) {
				}
				else {
					this.SADesc = dr["SADesc"].ToString();
				}

				if (dr["SAIndex"] == System.DBNull.Value) {
				}
				else {
					this.SAIndex = int.Parse(dr["SAIndex"].ToString());
				}

				if (dr["SAWeb"] == System.DBNull.Value) {
				}
				else {
					this.SAWeb = dr["SAWeb"].ToString();
				}

				if (dr["SAIsValid"] == System.DBNull.Value) {
				}
				else {
					this.SAIsValid = int.Parse(dr["SAIsValid"].ToString());
				}

				if (dr["SAIsCalc"] == System.DBNull.Value) {
				}
				else {
					this.SAIsCalc = int.Parse(dr["SAIsCalc"].ToString());
				}
				if (dr["SAIsDisplay"] == System.DBNull.Value) {
				}
				else {
					this.SAIsDisplay = int.Parse(dr["SAIsDisplay"].ToString());
				}
				if (dr["SAIsTotal"] == System.DBNull.Value) {
				}
				else {
					this.SAIsTotal = int.Parse(dr["SAIsTotal"].ToString());
				}

				if (dr["SAFilterExp"] == System.DBNull.Value) {
				}
				else {
					this.SAFilterExp = dr["SAFilterExp"].ToString();
				}

				if (dr["SACalcExp"] == System.DBNull.Value) {
				}
				else {
					this.SACalcExp = dr["SACalcExp"].ToString();
				}

                //if (dr["SAType"] == System.DBNull.Value) {
                //}
                //else {
                //    this.SAType = int.Parse(dr["SAType"].ToString());
                //}

				if (dr["SANote"] == System.DBNull.Value) {
				}
				else {
					this.SANote = dr["SANote"].ToString();
				}

				if (dr["SACreateTime"] == System.DBNull.Value) {
				}
				else {
					this.SACreateTime = dr["SACreateTime"].ToString();
				}


				if (dr["SAModifyTime"] == System.DBNull.Value) {
				}
				else {
					this.SAModifyTime = dr["SAModifyTime"].ToString();
				}
				if (dr["SACountExpression"] == System.DBNull.Value) {
				}
				else {
					this.SACountExpression = dr["SACountExpression"].ToString();
				}

				if (dr["SADurationExpression"] == System.DBNull.Value) {
				}
				else {
					this.SADurationExpression = dr["SADurationExpression"].ToString();
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
