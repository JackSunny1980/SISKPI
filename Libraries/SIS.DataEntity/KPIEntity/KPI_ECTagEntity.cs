
namespace SIS.DataEntity {
	using System;
	using System.Xml;
	using System.Data;
	using System.Text;
	using System.ComponentModel;

	[System.Serializable()]
	[System.Runtime.InteropServices.Guid("50e9801b-6409-4dc8-a554-9923a85743dc")]

	public class ECTagEntity : EntityBase {
		protected String _ECID = null;
		protected String _UnitID = null;
		protected String _SeqID = null;
		protected String _KpiID = null;
		protected String _EngunitID = null;
		protected String _CycleID = null;

		protected String _ECCode = null;
		protected String _ECName = null;
		protected String _ECDesc = null;
		protected int _ECIndex = int.MinValue;
		protected String _ECWeb = null;
		protected int _ECIsValid = int.MinValue;

		protected int _ECIsCalc = int.MinValue;
		protected int _ECIsAsses = int.MinValue;
		protected int _ECIsZero = int.MinValue;
		protected int _ECIsDisplay = int.MinValue;
		protected int _ECIsTotal = int.MinValue;
		protected String _ECDesign = null;

		protected String _ECOptimal = null;
		protected decimal _ECMaxValue = decimal.MinValue;
		protected decimal _ECMinValue = decimal.MinValue;
		protected decimal _ECWeight = decimal.MinValue;
		protected decimal _ECDenom = decimal.MinValue;
		protected int _ECCalcClass = int.MinValue;
		protected String _ECFilterExp = null;

		protected String _ECCalcExp = null;
		protected String _ECCalcDesc = null;
		protected int _ECIsSnapshot = int.MinValue;
		protected int _ECXLineType = int.MinValue;      //0,1,2,3
		protected int _ECXLineGetType = int.MinValue;   //-1,0,1
		protected String _ECXLineXRealTag = null;
		protected String _ECXLineYRealTag = null;
		protected String _ECXLineZRealTag = null;
		protected String _ECXLineXYZ = null;
		protected String _ECScoreExp = null;
		protected String _ECCurveGroup = null;
		protected int _ECIsSort = int.MinValue;
		protected int _ECType = int.MinValue;
		protected int _ECSort = int.MinValue;
		protected String _ECScore = null;
		protected String _ECExExp = null;
		protected String _ECExScore = null;
		protected String _ECNote = null;
		protected String _ECCreateTime = null;
		protected String _ECModifyTime = null;

		[Description("ECID")]
		public virtual String ECID {
			get {
				return this._ECID;
			}
			set {
				this._ECID = value;
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

		[Description("ECCode")]
		public virtual String ECCode {
			get {
				return this._ECCode;
			}
			set {
				this._ECCode = value;
			}
		}

		[Description("ECName")]
		public virtual String ECName {
			get {
				return this._ECName;
			}
			set {
				this._ECName = value;
			}
		}

		[Description("ECDesc")]
		public virtual String ECDesc {
			get {
				return this._ECDesc;
			}
			set {
				this._ECDesc = value;
			}
		}

		[Description("ECIndex")]
		public virtual int ECIndex {
			get {
				return this._ECIndex;
			}
			set {
				this._ECIndex = value;
			}
		}

		[Description("ECWeb")]
		public virtual String ECWeb {
			get {
				return this._ECWeb;
			}
			set {
				this._ECWeb = value;
			}
		}

		[Description("ECIsValid")]
		public virtual int ECIsValid {
			get {
				return this._ECIsValid;
			}
			set {
				this._ECIsValid = value;
			}
		}

		[Description("ECIsCalc")]
		public virtual int ECIsCalc {
			get {
				return this._ECIsCalc;
			}
			set {
				this._ECIsCalc = value;
			}
		}

		[Description("ECIsAsses")]
		public virtual int ECIsAsses {
			get {
				return this._ECIsAsses;
			}
			set {
				this._ECIsAsses = value;
			}
		}

		[Description("ECIsZero")]
		public virtual int ECIsZero {
			get {
				return this._ECIsZero;
			}
			set {
				this._ECIsZero = value;
			}
		}

		[Description("ECIsDisplay")]
		public virtual int ECIsDisplay {
			get {
				return this._ECIsDisplay;
			}
			set {
				this._ECIsDisplay = value;
			}
		}
		[Description("ECIsTotal")]
		public virtual int ECIsTotal {
			get {
				return this._ECIsTotal;
			}
			set {
				this._ECIsTotal = value;
			}
		}

		[Description("ECDesign")]
		public virtual String ECDesign {
			get {
				return this._ECDesign;
			}
			set {
				this._ECDesign = value;
			}
		}

		[Description("ECOptimal")]
		public virtual String ECOptimal {
			get {
				return this._ECOptimal;
			}
			set {
				this._ECOptimal = value;
			}
		}

		[Description("ECMaxValue")]
		public virtual decimal ECMaxValue {
			get {
				return this._ECMaxValue;
			}
			set {
				this._ECMaxValue = value;
			}
		}

		[Description("ECMinValue")]
		public virtual decimal ECMinValue {
			get {
				return this._ECMinValue;
			}
			set {
				this._ECMinValue = value;
			}
		}
		[Description("ECWeight")]
		public virtual decimal ECWeight {
			get {
				return this._ECWeight;
			}
			set {
				this._ECWeight = value;
			}
		}

		[Description("ECDenom")]
		public virtual decimal ECDenom {
			get {
				return this._ECDenom;
			}
			set {
				this._ECDenom = value;
			}
		}

		[Description("ECCalcClass")]
		public virtual int ECCalcClass {
			get {
				return this._ECCalcClass;
			}
			set {
				this._ECCalcClass = value;
			}
		}

		[Description("ECFilterExp")]
		public virtual String ECFilterExp {
			get {
				return this._ECFilterExp;
			}
			set {
				this._ECFilterExp = value;
			}
		}

		[Description("ECCalcExp")]
		public virtual String ECCalcExp {
			get {
				return this._ECCalcExp;
			}
			set {
				this._ECCalcExp = value;
			}
		}

		[Description("ECCalcDesc")]
		public virtual String ECCalcDesc {
			get {
				return this._ECCalcDesc;
			}
			set {
				this._ECCalcDesc = value;
			}
		}

		[Description("ECIsSnapshot")]
		public virtual int ECIsSnapshot {
			get {
				return this._ECIsSnapshot;
			}
			set {
				this._ECIsSnapshot = value;
			}
		}

		[Description("ECXLineType")]
		public virtual int ECXLineType {
			get {
				return this._ECXLineType;
			}
			set {
				this._ECXLineType = value;
			}
		}

		[Description("ECXLineGetType")]
		public virtual int ECXLineGetType {
			get {
				return this._ECXLineGetType;
			}
			set {
				this._ECXLineGetType = value;
			}
		}

		[Description("ECXLineXRealTag")]
		public virtual String ECXLineXRealTag {
			get {
				return this._ECXLineXRealTag;
			}
			set {
				this._ECXLineXRealTag = value;
			}
		}

		[Description("ECXLineYRealTag")]
		public virtual String ECXLineYRealTag {
			get {
				return this._ECXLineYRealTag;
			}
			set {
				this._ECXLineYRealTag = value;
			}
		}

		[Description("ECXLineZRealTag")]
		public virtual String ECXLineZRealTag {
			get {
				return this._ECXLineZRealTag;
			}
			set {
				this._ECXLineZRealTag = value;
			}
		}

		[Description("ECXLineXYZ")]
		public virtual String ECXLineXYZ {
			get {
				return this._ECXLineXYZ;
			}
			set {
				this._ECXLineXYZ = value;
			}
		}

		[Description("ECScoreExp")]
		public virtual String ECScoreExp {
			get {
				return this._ECScoreExp;
			}
			set {
				this._ECScoreExp = value;
			}
		}

		[Description("ECCurveGroup")]
		public virtual String ECCurveGroup {
			get {
				return this._ECCurveGroup;
			}
			set {
				this._ECCurveGroup = value;
			}
		}

		[Description("ECIsSort")]
		public virtual int ECIsSort {
			get {
				return this._ECIsSort;
			}
			set {
				this._ECIsSort = value;
			}
		}

		[Description("ECType")]
		public virtual int ECType {
			get {
				return this._ECType;
			}
			set {
				this._ECType = value;
			}
		}

		[Description("ECSort")]
		public virtual int ECSort {
			get {
				return this._ECSort;
			}
			set {
				this._ECSort = value;
			}
		}

		[Description("ECScore")]
		public virtual String ECScore {
			get {
				return this._ECScore;
			}
			set {
				this._ECScore = value;
			}
		}

		[Description("ECExExp")]
		public virtual String ECExExp {
			get {
				return this._ECExExp;
			}
			set {
				this._ECExExp = value;
			}
		}

		[Description("ECExScore")]
		public virtual String ECExScore {
			get {
				return this._ECExScore;
			}
			set {
				this._ECExScore = value;
			}
		}


		public virtual String ECNote {
			get {
				return this._ECNote;
			}
			set {
				this._ECNote = value;
			}
		}

	
		public virtual String ECCreateTime {
			get {
				return this._ECCreateTime;
			}
			set {
				this._ECCreateTime = value;
			}
		}

		
		public virtual String ECModifyTime {
			get {
				return this._ECModifyTime;
			}
			set {
				this._ECModifyTime = value;
			}
		}

		public override string InsertSql {
			get {
				System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
				tmpBuild.Append("insert into KPI_ECTag (");

				if ((this.ECID == null)) {
				}
				else {
					tmpBuild.Append("ECID");
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

				if ((this.ECCode == null)) {
				}
				else {
					tmpBuild.Append("ECCode");
					tmpBuild.Append(",");
				}

				if ((this.ECName == null)) {
				}
				else {
					tmpBuild.Append("ECName");
					tmpBuild.Append(",");
				}

				if ((this.ECDesc == null)) {
				}
				else {
					tmpBuild.Append("ECDesc");
					tmpBuild.Append(",");
				}
				if ((this.ECIndex == int.MinValue)) {
				}
				else {
					tmpBuild.Append("ECIndex");
					tmpBuild.Append(",");
				}

				if ((this.ECWeb == null)) {
				}
				else {
					tmpBuild.Append("ECWeb");
					tmpBuild.Append(",");
				}

				if ((this.ECIsValid == int.MinValue)) {
				}
				else {
					tmpBuild.Append("ECIsValid");
					tmpBuild.Append(",");
				}

				if ((this.ECIsCalc == int.MinValue)) {
				}
				else {
					tmpBuild.Append("ECIsCalc");
					tmpBuild.Append(",");
				}


				if ((this.ECIsAsses == int.MinValue)) {
				}
				else {
					tmpBuild.Append("ECIsAsses");
					tmpBuild.Append(",");
				}


				if ((this.ECIsZero == int.MinValue)) {
				}
				else {
					tmpBuild.Append("ECIsZero");
					tmpBuild.Append(",");
				}

				if ((this.ECIsDisplay == int.MinValue)) {
				}
				else {
					tmpBuild.Append("ECIsDisplay");
					tmpBuild.Append(",");
				}

				if ((this.ECIsTotal == int.MinValue)) {
				}
				else {
					tmpBuild.Append("ECIsTotal");
					tmpBuild.Append(",");
				}

				if ((this.ECDesign == null)) {
				}
				else {
					tmpBuild.Append("ECDesign");
					tmpBuild.Append(",");
				}
				if ((this.ECOptimal == null)) {
				}
				else {
					tmpBuild.Append("ECOptimal");
					tmpBuild.Append(",");
				}

				if ((this.ECMaxValue == decimal.MinValue)) {
				}
				else {
					tmpBuild.Append("ECMaxValue");
					tmpBuild.Append(",");
				}
				if ((this.ECMinValue == decimal.MinValue)) {
				}
				else {
					tmpBuild.Append("ECMinValue");
					tmpBuild.Append(",");
				}
				if ((this.ECWeight == decimal.MinValue)) {
				}
				else {
					tmpBuild.Append("ECWeight");
					tmpBuild.Append(",");
				}
				if ((this.ECDenom == decimal.MinValue)) {
				}
				else {
					tmpBuild.Append("ECDenom");
					tmpBuild.Append(",");
				}
				if ((this.ECCalcClass == int.MinValue)) {
				}
				else {
					tmpBuild.Append("ECCalcClass");
					tmpBuild.Append(",");
				}

				if ((this.ECFilterExp == null)) {
				}
				else {
					tmpBuild.Append("ECFilterExp");
					tmpBuild.Append(",");
				}

				if ((this.ECCalcExp == null)) {
				}
				else {
					tmpBuild.Append("ECCalcExp");
					tmpBuild.Append(",");
				}

				if ((this.ECCalcDesc == null)) {
				}
				else {
					tmpBuild.Append("ECCalcDesc");
					tmpBuild.Append(",");
				}

				if ((this.ECIsSnapshot == int.MinValue)) {
				}
				else {
					tmpBuild.Append("ECIsSnapshot");
					tmpBuild.Append(",");
				}


				if ((this.ECXLineType == int.MinValue)) {
				}
				else {
					tmpBuild.Append("ECXLineType");
					tmpBuild.Append(",");
				}
				if ((this.ECXLineGetType == int.MinValue)) {
				}
				else {
					tmpBuild.Append("ECXLineGetType");
					tmpBuild.Append(",");
				}

				if ((this.ECXLineXRealTag == null)) {
				}
				else {
					tmpBuild.Append("ECXLineXRealTag");
					tmpBuild.Append(",");
				}
				if ((this.ECXLineYRealTag == null)) {
				}
				else {
					tmpBuild.Append("ECXLineYRealTag");
					tmpBuild.Append(",");
				}
				if ((this.ECXLineZRealTag == null)) {
				}
				else {
					tmpBuild.Append("ECXLineZRealTag");
					tmpBuild.Append(",");
				}
				if ((this.ECXLineXYZ == null)) {
				}
				else {
					tmpBuild.Append("ECXLineXYZ");
					tmpBuild.Append(",");
				}

				if ((this.ECScoreExp == null)) {
				}
				else {
					tmpBuild.Append("ECScoreExp");
					tmpBuild.Append(",");
				}

				if ((this.ECCurveGroup == null)) {
				}
				else {
					tmpBuild.Append("ECCurveGroup");
					tmpBuild.Append(",");
				}

				if ((this.ECIsSort == int.MinValue)) {
				}
				else {
					tmpBuild.Append("ECIsSort");
					tmpBuild.Append(",");
				}

				if ((this.ECType == int.MinValue)) {
				}
				else {
					tmpBuild.Append("ECType");
					tmpBuild.Append(",");
				}

				if ((this.ECSort == int.MinValue)) {
				}
				else {
					tmpBuild.Append("ECSort");
					tmpBuild.Append(",");
				}

				if ((this.ECScore == null)) {
				}
				else {
					tmpBuild.Append("ECScore");
					tmpBuild.Append(",");
				}

				if ((this.ECExExp == null)) {
				}
				else {
					tmpBuild.Append("ECExExp");
					tmpBuild.Append(",");
				}

				if ((this.ECExScore == null)) {
				}
				else {
					tmpBuild.Append("ECExScore");
					tmpBuild.Append(",");
				}

				if ((this.ECNote == null)) {
				}
				else {
					tmpBuild.Append("ECNote");
					tmpBuild.Append(",");
				}

				if ((this.ECCreateTime == null)) {
				}
				else {
					tmpBuild.Append("ECCreateTime");
					tmpBuild.Append(",");
				}

				if ((this.ECModifyTime == null)) {
				}
				else {
					tmpBuild.Append("ECModifyTime");
					tmpBuild.Append(",");
				}

				///////////////////////////////////////////////////////////////////////////////////////


				if ((tmpBuild[(tmpBuild.Length - 1)] == ',')) {
					tmpBuild.Remove((tmpBuild.Length - 1), 1);
				}

				tmpBuild.Append(") values(");

				if ((this.ECID == null)) {
				}
				else {
					tmpBuild.Append("'" + ECID + "'");
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

				if ((this.ECCode == null)) {
				}
				else {
					tmpBuild.Append("'" + ECCode + "'");
					tmpBuild.Append(",");
				}

				if ((this.ECName == null)) {
				}
				else {
					tmpBuild.Append("'" + ECName + "'");
					tmpBuild.Append(",");
				}

				if ((this.ECDesc == null)) {
				}
				else {
					tmpBuild.Append("'" + ECDesc + "'");
					tmpBuild.Append(",");
				}

				if ((this.ECIndex == int.MinValue)) {
				}
				else {
					tmpBuild.Append(ECIndex.ToString());
					tmpBuild.Append(",");
				}

				if ((this.ECWeb == null)) {
				}
				else {
					tmpBuild.Append("'" + ECWeb + "'");
					tmpBuild.Append(",");
				}

				if ((this.ECIsValid == int.MinValue)) {
				}
				else {
					tmpBuild.Append(ECIsValid.ToString());
					tmpBuild.Append(",");
				}

				if ((this.ECIsCalc == int.MinValue)) {
				}
				else {
					tmpBuild.Append(ECIsCalc.ToString());
					tmpBuild.Append(",");
				}
				if ((this.ECIsAsses == int.MinValue)) {
				}
				else {
					tmpBuild.Append(ECIsAsses.ToString());
					tmpBuild.Append(",");
				}
				if ((this.ECIsZero == int.MinValue)) {
				}
				else {
					tmpBuild.Append(ECIsZero.ToString());
					tmpBuild.Append(",");
				}

				if ((this.ECIsDisplay == int.MinValue)) {
				}
				else {
					tmpBuild.Append(ECIsDisplay.ToString());
					tmpBuild.Append(",");
				}

				if ((this.ECIsTotal == int.MinValue)) {
				}
				else {
					tmpBuild.Append(ECIsTotal.ToString());
					tmpBuild.Append(",");
				}

				if ((this.ECDesign == null)) {
				}
				else {
					tmpBuild.Append("'" + ECDesign + "'");
					tmpBuild.Append(",");
				}
				if ((this.ECOptimal == null)) {
				}
				else {
					tmpBuild.Append("'" + ECOptimal + "'");
					tmpBuild.Append(",");
				}

				if ((this.ECMaxValue == decimal.MinValue)) {
				}
				else {
					tmpBuild.Append(ECMaxValue.ToString());
					tmpBuild.Append(",");
				}
				if ((this.ECMinValue == decimal.MinValue)) {
				}
				else {
					tmpBuild.Append(ECMinValue.ToString());
					tmpBuild.Append(",");
				}
				if ((this.ECWeight == decimal.MinValue)) {
				}
				else {
					tmpBuild.Append(ECWeight.ToString());
					tmpBuild.Append(",");
				}
				if ((this.ECDenom == decimal.MinValue)) {
				}
				else {
					tmpBuild.Append(ECDenom.ToString());
					tmpBuild.Append(",");
				}
				if ((this.ECCalcClass == int.MinValue)) {
				}
				else {
					tmpBuild.Append(ECCalcClass.ToString());
					tmpBuild.Append(",");
				}

				if ((this.ECFilterExp == null)) {
				}
				else {
					//函数引用了指标后，存在'，需要特殊处理
					string strtemp = this.ECFilterExp.Replace("'", "''");

					tmpBuild.Append("'" + strtemp + "'");
					tmpBuild.Append(",");
				}

				if ((this.ECCalcExp == null)) {
				}
				else {
					//函数引用了指标后，存在'，需要特殊处理
					string strtemp = this.ECCalcExp.Replace("'", "''");

					tmpBuild.Append("'" + strtemp + "'");
					tmpBuild.Append(",");
				}

				if ((this.ECCalcDesc == null)) {
				}
				else {
					//函数引用了指标后，存在'，需要特殊处理
					string strtemp = this.ECCalcDesc.Replace("'", "''");

					tmpBuild.Append("'" + strtemp + "'");
					tmpBuild.Append(",");
				}

				if ((this.ECIsSnapshot == int.MinValue)) {
				}
				else {
					tmpBuild.Append(ECIsSnapshot.ToString());
					tmpBuild.Append(",");
				}

				if ((this.ECXLineType == int.MinValue)) {
				}
				else {
					tmpBuild.Append(ECXLineType.ToString());
					tmpBuild.Append(",");
				}
				if ((this.ECXLineGetType == int.MinValue)) {
				}
				else {
					tmpBuild.Append(ECXLineGetType.ToString());
					tmpBuild.Append(",");
				}

				if ((this.ECXLineXRealTag == null)) {
				}
				else {
					tmpBuild.Append("'" + ECXLineXRealTag + "'");
					tmpBuild.Append(",");
				}


				if ((this.ECXLineYRealTag == null)) {
				}
				else {
					tmpBuild.Append("'" + ECXLineYRealTag + "'");
					tmpBuild.Append(",");
				}

				if ((this.ECXLineZRealTag == null)) {
				}
				else {
					tmpBuild.Append("'" + ECXLineZRealTag + "'");
					tmpBuild.Append(",");
				}

				if ((this.ECXLineXYZ == null)) {
				}
				else {
					//函数引用了指标后，存在'，需要特殊处理
					string strtemp = this.ECXLineXYZ.Replace("'", "''");

					tmpBuild.Append("'" + strtemp + "'");
					tmpBuild.Append(",");
				}

				if ((this.ECScoreExp == null)) {
				}
				else {
					//函数引用了指标后，存在'，需要特殊处理
					string strtemp = this.ECScoreExp.Replace("'", "''");

					tmpBuild.Append("'" + strtemp + "'");
					tmpBuild.Append(",");
				}
				if ((this.ECCurveGroup == null)) {
				}
				else {
					tmpBuild.Append("'" + ECCurveGroup + "'");
					tmpBuild.Append(",");
				}

				if ((this.ECIsSort == int.MinValue)) {
				}
				else {
					tmpBuild.Append(ECIsSort.ToString());
					tmpBuild.Append(",");
				}

				if ((this.ECType == int.MinValue)) {
				}
				else {
					tmpBuild.Append(ECType.ToString());
					tmpBuild.Append(",");
				}

				if ((this.ECSort == int.MinValue)) {
				}
				else {
					tmpBuild.Append(ECSort.ToString());
					tmpBuild.Append(",");
				}

				if ((this.ECScore == null)) {
				}
				else {
					tmpBuild.Append("'" + ECScore + "'");
					tmpBuild.Append(",");
				}

				if ((this.ECExExp == null)) {
				}
				else {
					//函数引用了指标后，存在'，需要特殊处理
					string strtemp = this.ECExExp.Replace("'", "''");

					tmpBuild.Append("'" + strtemp + "'");
					tmpBuild.Append(",");
				}

				if ((this.ECExScore == null)) {
				}
				else {
					tmpBuild.Append("'" + ECExScore + "'");
					tmpBuild.Append(",");
				}


				if ((this.ECNote == null)) {
				}
				else {
					tmpBuild.Append("'" + ECNote + "'");
					tmpBuild.Append(",");
				}

				if ((this.ECCreateTime == null)) {
				}
				else {
					tmpBuild.Append("'" + ECCreateTime + "'");
					tmpBuild.Append(",");
				}

				if ((this.ECModifyTime == null)) {
				}
				else {
					tmpBuild.Append("'" + ECModifyTime + "'");
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
				tmpBuild.Append("update KPI_ECTag set ");

				if ((this.ECID == null)) {
				}
				else {
					tmpBuild.Append("ECID='" + ECID + "'");
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

				if ((this.ECCode == null)) {
				}
				else {
					tmpBuild.Append("ECCode='" + ECCode + "'");
					tmpBuild.Append(",");
				}

				if ((this.ECName == null)) {
				}
				else {
					tmpBuild.Append("ECName='" + ECName + "'");
					tmpBuild.Append(",");
				}

				if ((this.ECDesc == null)) {
				}
				else {
					tmpBuild.Append("ECDesc='" + ECDesc + "'");
					tmpBuild.Append(",");
				}

				if ((this.ECIndex == int.MinValue)) {
				}
				else {
					tmpBuild.Append("ECIndex=" + ECIndex.ToString());
					tmpBuild.Append(",");
				}

				if ((this.ECWeb == null)) {
				}
				else {
					tmpBuild.Append("ECWeb='" + ECWeb + "'");
					tmpBuild.Append(",");
				}

				if ((this.ECIsValid == int.MinValue)) {
				}
				else {
					tmpBuild.Append("ECIsValid=" + ECIsValid.ToString());
					tmpBuild.Append(",");

				}

				if ((this.ECIsCalc == int.MinValue)) {
				}
				else {
					tmpBuild.Append("ECIsCalc=" + ECIsCalc.ToString());
					tmpBuild.Append(",");
				}
				if ((this.ECIsAsses == int.MinValue)) {
				}
				else {
					tmpBuild.Append("ECIsAsses=" + ECIsAsses.ToString());
					tmpBuild.Append(",");
				}
				if ((this.ECIsZero == int.MinValue)) {
				}
				else {
					tmpBuild.Append("ECIsZero=" + ECIsZero.ToString());
					tmpBuild.Append(",");
				}

				if ((this.ECIsDisplay == int.MinValue)) {
				}
				else {
					tmpBuild.Append("ECIsDisplay=" + ECIsDisplay.ToString());
					tmpBuild.Append(",");
				}

				if ((this.ECIsTotal == int.MinValue)) {
				}
				else {
					tmpBuild.Append("ECIsTotal=" + ECIsTotal.ToString());
					tmpBuild.Append(",");
				}

				if ((this.ECDesign == null)) {
				}
				else {
					tmpBuild.Append("ECDesign='" + ECDesign + "'");
					tmpBuild.Append(",");
				}
				if ((this.ECOptimal == null)) {
				}
				else {
					tmpBuild.Append("ECOptimal='" + ECOptimal + "'");
					tmpBuild.Append(",");
				}

				if ((this.ECMaxValue == decimal.MinValue)) {
				}
				else {
					tmpBuild.Append("ECMaxValue=" + ECMaxValue.ToString());
					tmpBuild.Append(",");
				}
				if ((this.ECMinValue == decimal.MinValue)) {
				}
				else {
					tmpBuild.Append("ECMinValue=" + ECMinValue.ToString());
					tmpBuild.Append(",");
				}
				if ((this.ECWeight == decimal.MinValue)) {
				}
				else {
					tmpBuild.Append("ECWeight=" + ECWeight.ToString());
					tmpBuild.Append(",");
				}
				if ((this.ECDenom == decimal.MinValue)) {
				}
				else {
					tmpBuild.Append("ECDenom=" + ECDenom.ToString());
					tmpBuild.Append(",");
				}
				if ((this.ECCalcClass == int.MinValue)) {
				}
				else {
					tmpBuild.Append("ECCalcClass=" + ECCalcClass.ToString());
					tmpBuild.Append(",");
				}

				if ((this.ECFilterExp == null)) {
				}
				else {
					//函数引用了指标后，存在'，需要特殊处理
					string strtemp = this.ECFilterExp.Replace("'", "''");

					tmpBuild.Append("ECFilterExp='" + strtemp + "'");
					tmpBuild.Append(",");
				}

				if ((this.ECCalcExp == null)) {
				}
				else {
					//函数引用了指标后，存在'，需要特殊处理
					string strtemp = this.ECCalcExp.Replace("'", "''");

					tmpBuild.Append("ECCalcExp='" + strtemp + "'");
					tmpBuild.Append(",");
				}

				if ((this.ECCalcDesc == null)) {
				}
				else {
					//函数引用了指标后，存在'，需要特殊处理
					string strtemp = this.ECCalcDesc.Replace("'", "''");


					tmpBuild.Append("ECCalcDesc='" + strtemp + "'");
					tmpBuild.Append(",");
				}

				if ((this.ECIsSnapshot == int.MinValue)) {
				}
				else {
					tmpBuild.Append("ECIsSnapshot=" + ECIsSnapshot.ToString());
					tmpBuild.Append(",");
				}


				if ((this.ECXLineType == int.MinValue)) {
				}
				else {
					tmpBuild.Append("ECXLineType=" + ECXLineType.ToString());
					tmpBuild.Append(",");
				}
				if ((this.ECXLineGetType == int.MinValue)) {
				}
				else {
					tmpBuild.Append("ECXLineGetType=" + ECXLineGetType.ToString());
					tmpBuild.Append(",");
				}

				if ((this.ECXLineXRealTag == null)) {
				}
				else {
					tmpBuild.Append("ECXLineXRealTag='" + ECXLineXRealTag + "'");
					tmpBuild.Append(",");
				}

				if ((this.ECXLineYRealTag == null)) {
				}
				else {
					tmpBuild.Append("ECXLineYRealTag='" + ECXLineYRealTag + "'");
					tmpBuild.Append(",");
				}

				if ((this.ECXLineZRealTag == null)) {
				}
				else {
					tmpBuild.Append("ECXLineZRealTag='" + ECXLineZRealTag + "'");
					tmpBuild.Append(",");
				}

				if ((this.ECXLineXYZ == null)) {
				}
				else {
					tmpBuild.Append("ECXLineXYZ='" + ECXLineXYZ + "'");
					tmpBuild.Append(",");
				}

				if ((this.ECScoreExp == null)) {
				}
				else {
					//
					string strtemp = this.ECScoreExp.Replace("'", "''");

					tmpBuild.Append("ECScoreExp='" + strtemp + "'");
					tmpBuild.Append(",");
				}
				if ((this.ECCurveGroup == null)) {
				}
				else {
					tmpBuild.Append("ECCurveGroup='" + ECCurveGroup + "'");
					tmpBuild.Append(",");
				}


				if ((this.ECIsSort == int.MinValue)) {
				}
				else {
					tmpBuild.Append("ECIsSort=" + ECIsSort.ToString());
					tmpBuild.Append(",");
				}

				if ((this.ECType == int.MinValue)) {
				}
				else {
					tmpBuild.Append("ECType=" + ECType.ToString());
					tmpBuild.Append(",");
				}

				if ((this.ECSort == int.MinValue)) {
				}
				else {
					tmpBuild.Append("ECSort=" + ECSort.ToString());
					tmpBuild.Append(",");
				}

				if ((this.ECScore == null)) {
				}
				else {
					tmpBuild.Append("ECScore='" + ECScore + "'");
					tmpBuild.Append(",");
				}

				if ((this.ECExExp == null)) {
				}
				else {
					//函数引用了指标后，存在'，需要特殊处理
					string strtemp = this.ECExExp.Replace("'", "''");

					tmpBuild.Append("ECExExp='" + strtemp + "'");
					tmpBuild.Append(",");
				}

				if ((this.ECExScore == null)) {
				}
				else {
					tmpBuild.Append("ECExScore='" + ECExScore + "'");
					tmpBuild.Append(",");
				}


				if ((this.ECNote == null)) {
				}
				else {
					tmpBuild.Append("ECNote='" + ECNote + "'");
					tmpBuild.Append(",");
				}

				if ((this.ECCreateTime == null)) {
				}
				else {
					tmpBuild.Append("ECCreateTime='" + ECCreateTime + "'");
					tmpBuild.Append(",");
				}

				if ((this.ECModifyTime == null)) {
				}
				else {
					tmpBuild.Append("ECModifyTime='" + ECModifyTime + "'");
					tmpBuild.Append(",");
				}

				if ((tmpBuild[(tmpBuild.Length - 1)] == ',')) {
					tmpBuild.Remove((tmpBuild.Length - 1), 1);
				}

				tmpBuild.Append(" where ");
				tmpBuild.Append("ECID='" + ECID + "'");

				string __tmpSql = tmpBuild.ToString();
				return __tmpSql;
			}
		}

		public override string DeleteSql {
			get {
				System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
				tmpBuild.Append("delete KPI_ECTag");
				tmpBuild.Append(" where ");
				tmpBuild.Append("ECID='" + ECID + "'");

				string __tmpSql = tmpBuild.ToString();
				return __tmpSql;
			}
		}

		public override string SelectSql {
			get {
				System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
				tmpBuild.Append("select ");
				tmpBuild.Append("ECID");
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
				tmpBuild.Append("ECCode");
				tmpBuild.Append(",");
				tmpBuild.Append("ECName");
				tmpBuild.Append(",");
				tmpBuild.Append("ECDesc");
				tmpBuild.Append(",");
				tmpBuild.Append("ECIndex");
				tmpBuild.Append(",");
				tmpBuild.Append("ECWeb");
				tmpBuild.Append(",");
				tmpBuild.Append("ECIsValid");
				tmpBuild.Append(",");
				tmpBuild.Append("ECIsCalc");
				tmpBuild.Append(",");
				tmpBuild.Append("ECIsAsses");
				tmpBuild.Append(",");
				tmpBuild.Append("ECIsZero");
				tmpBuild.Append(",");
				tmpBuild.Append("ECIsDisplay");
				tmpBuild.Append(",");
				tmpBuild.Append("ECIsTotal");
				tmpBuild.Append(",");
				tmpBuild.Append("ECDesign");
				tmpBuild.Append(",");
				tmpBuild.Append("ECOptimal");
				tmpBuild.Append(",");
				tmpBuild.Append("ECMaxValue");
				tmpBuild.Append(",");
				tmpBuild.Append("ECMinValue");
				tmpBuild.Append(",");
				tmpBuild.Append("ECWeight");
				tmpBuild.Append(",");
				tmpBuild.Append("ECDenom");
				tmpBuild.Append(",");
				tmpBuild.Append("ECCalcClass");
				tmpBuild.Append(",");
				tmpBuild.Append("ECFilterExp");
				tmpBuild.Append(",");
				tmpBuild.Append("ECCalcExp");
				tmpBuild.Append(",");
				tmpBuild.Append("ECCalcDesc");
				tmpBuild.Append(",");
				tmpBuild.Append("ECIsSnapshot");
				tmpBuild.Append(",");
				tmpBuild.Append("ECXLineType");
				tmpBuild.Append(",");
				tmpBuild.Append("ECXLineGetType");
				tmpBuild.Append(",");
				tmpBuild.Append("ECXLineXRealTag");
				tmpBuild.Append(",");
				tmpBuild.Append("ECXLineYRealTag");
				tmpBuild.Append(",");
				tmpBuild.Append("ECXLineZRealTag");
				tmpBuild.Append(",");
				tmpBuild.Append("ECXLineXYZ");
				tmpBuild.Append(",");
				tmpBuild.Append("ECIsSort");
				tmpBuild.Append(",");
				tmpBuild.Append("ECScoreExp");
				tmpBuild.Append(",");
				tmpBuild.Append("ECCurveGroup");
				tmpBuild.Append(",");
				tmpBuild.Append("ECType");
				tmpBuild.Append(",");
				tmpBuild.Append("ECSort");
				tmpBuild.Append(",");
				tmpBuild.Append("ECScore");
				tmpBuild.Append(",");
				tmpBuild.Append("ECExExp");
				tmpBuild.Append(",");
				tmpBuild.Append("ECExScore");
				tmpBuild.Append(",");
				tmpBuild.Append("ECNote");
				tmpBuild.Append(",");
				tmpBuild.Append("ECCreateTime");
				tmpBuild.Append(",");
				tmpBuild.Append("ECModifyTime");

				tmpBuild.Append(" from KPI_ECTag ");
				tmpBuild.Append(" where ");
				tmpBuild.Append("ECID='" + ECID + "'");

				string __tmpSql = tmpBuild.ToString();
				return __tmpSql;
			}
		}

		public override bool DrToMember(System.Data.DataRow dr) {
			try {
				if (dr["ECID"] == System.DBNull.Value) {
				}
				else {
					this.ECID = dr["ECID"].ToString();
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

				if (dr["ECCode"] == System.DBNull.Value) {
				}
				else {
					this.ECCode = dr["ECCode"].ToString();
				}

				if (dr["ECName"] == System.DBNull.Value) {
				}
				else {
					this.ECName = dr["ECName"].ToString();
				}

				if (dr["ECDesc"] == System.DBNull.Value) {
				}
				else {
					this.ECDesc = dr["ECDesc"].ToString();
				}

				if (dr["ECIndex"] == System.DBNull.Value) {
				}
				else {
					this.ECIndex = int.Parse(dr["ECIndex"].ToString());
				}

				if (dr["ECWeb"] == System.DBNull.Value) {
				}
				else {
					this.ECWeb = dr["ECWeb"].ToString();
				}

				if (dr["ECIsValid"] == System.DBNull.Value) {
				}
				else {
					this.ECIsValid = int.Parse(dr["ECIsValid"].ToString());
				}

				if (dr["ECIsCalc"] == System.DBNull.Value) {
				}
				else {
					this.ECIsCalc = int.Parse(dr["ECIsCalc"].ToString());
				}
				if (dr["ECIsAsses"] == System.DBNull.Value) {
				}
				else {
					this.ECIsAsses = int.Parse(dr["ECIsAsses"].ToString());
				}
				if (dr["ECIsZero"] == System.DBNull.Value) {
				}
				else {
					this.ECIsZero = int.Parse(dr["ECIsZero"].ToString());
				}
				if (dr["ECIsDisplay"] == System.DBNull.Value) {
				}
				else {
					this.ECIsDisplay = int.Parse(dr["ECIsDisplay"].ToString());
				}
				if (dr["ECIsTotal"] == System.DBNull.Value) {
				}
				else {
					this.ECIsTotal = int.Parse(dr["ECIsTotal"].ToString());
				}

				if (dr["ECDesign"] == System.DBNull.Value) {
				}
				else {
					this.ECDesign = dr["ECDesign"].ToString();
				}
				if (dr["ECOptimal"] == System.DBNull.Value) {
				}
				else {
					this.ECOptimal = dr["ECOptimal"].ToString();
				}

				if (dr["ECMaxValue"] == System.DBNull.Value) {
				}
				else {
					this.ECMaxValue = decimal.Parse(dr["ECMaxValue"].ToString());
				}

				if (dr["ECMinValue"] == System.DBNull.Value) {
				}
				else {
					this.ECMinValue = decimal.Parse(dr["ECMinValue"].ToString());
				}

				if (dr["ECWeight"] == System.DBNull.Value) {
				}
				else {
					this.ECWeight = decimal.Parse(dr["ECWeight"].ToString());
				}
				if (dr["ECDenom"] == System.DBNull.Value) {
				}
				else {
					this.ECDenom = decimal.Parse(dr["ECDenom"].ToString());
				}

				if (dr["ECCalcClass"] == System.DBNull.Value) {
				}
				else {
					this.ECCalcClass = int.Parse(dr["ECCalcClass"].ToString());
				}


				if (dr["ECFilterExp"] == System.DBNull.Value) {
				}
				else {
					this.ECFilterExp = dr["ECFilterExp"].ToString();
				}

				if (dr["ECCalcExp"] == System.DBNull.Value) {
				}
				else {
					this.ECCalcExp = dr["ECCalcExp"].ToString();
				}

				if (dr["ECCalcDesc"] == System.DBNull.Value) {
				}
				else {
					this.ECCalcDesc = dr["ECCalcDesc"].ToString();
				}

				if (dr["ECIsSnapshot"] == System.DBNull.Value) {
				}
				else {
					this.ECIsSnapshot = int.Parse(dr["ECIsSnapshot"].ToString());
				}


				if ((dr["ECXLineType"] == System.DBNull.Value)) {
				}
				else {
					this.ECXLineType = int.Parse(dr["ECXLineType"].ToString());
				}
				if ((dr["ECXLineGetType"] == System.DBNull.Value)) {
				}
				else {
					this.ECXLineGetType = int.Parse(dr["ECXLineGetType"].ToString());
				}

				if ((dr["ECXLineXRealTag"] == System.DBNull.Value)) {
				}
				else {
					this.ECXLineXRealTag = dr["ECXLineXRealTag"].ToString();
				}
				if ((dr["ECXLineYRealTag"] == System.DBNull.Value)) {
				}
				else {
					this.ECXLineYRealTag = dr["ECXLineYRealTag"].ToString();
				}
				if ((dr["ECXLineZRealTag"] == System.DBNull.Value)) {
				}
				else {
					this.ECXLineZRealTag = dr["ECXLineZRealTag"].ToString();
				}
				if ((dr["ECXLineXYZ"] == System.DBNull.Value)) {
				}
				else {
					this.ECXLineXYZ = dr["ECXLineXYZ"].ToString();
				}

				if (dr["ECScoreExp"] == System.DBNull.Value) {
				}
				else {
					this.ECScoreExp = dr["ECScoreExp"].ToString();
				}

				if (dr["ECCurveGroup"] == System.DBNull.Value) {
				}
				else {
					this.ECCurveGroup = dr["ECCurveGroup"].ToString();
				}

				if (dr["ECIsSort"] == System.DBNull.Value) {
				}
				else {
					this.ECIsSort = int.Parse(dr["ECIsSort"].ToString());
				}
				if (dr["ECType"] == System.DBNull.Value) {
				}
				else {
					this.ECType = int.Parse(dr["ECType"].ToString());
				}
				if (dr["ECSort"] == System.DBNull.Value) {
				}
				else {
					this.ECSort = int.Parse(dr["ECSort"].ToString());
				}
				if (dr["ECScore"] == System.DBNull.Value) {
				}
				else {
					this.ECScore = dr["ECScore"].ToString();
				}
				if (dr["ECExExp"] == System.DBNull.Value) {
				}
				else {
					this.ECExExp = dr["ECExExp"].ToString();
				}
				if (dr["ECExScore"] == System.DBNull.Value) {
				}
				else {
					this.ECExScore = dr["ECExScore"].ToString();
				}

				if (dr["ECNote"] == System.DBNull.Value) {
				}
				else {
					this.ECNote = dr["ECNote"].ToString();
				}

				if (dr["ECCreateTime"] == System.DBNull.Value) {
				}
				else {
					this.ECCreateTime = dr["ECCreateTime"].ToString();
				}


				if (dr["ECModifyTime"] == System.DBNull.Value) {
				}
				else {
					this.ECModifyTime = dr["ECModifyTime"].ToString();
				}


			}
			catch (System.Exception e) {
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
