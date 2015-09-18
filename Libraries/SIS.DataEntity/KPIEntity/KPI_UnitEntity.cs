
namespace SIS.DataEntity
{
    using System;
    using System.Xml;
    using System.Data;
    using System.Text;
	using System.ComponentModel;
    
    [System.Serializable()]
    [System.Runtime.InteropServices.Guid("53e9841b-6499-4dc8-a554-9923a85743cc")]

    public class KPI_UnitEntity : EntityBase
    {
        protected String _UnitID = null;
        protected String _PlantID = null;
        protected String _UnitCode = null;
        protected String _UnitName = null;
        protected String _UnitDesc = null;
        protected int _UnitIndex = int.MinValue;
        protected int _UnitIsValid = int.MinValue;
        protected String _UnitPrefix = null;
		protected decimal _UnitMW = decimal.MinValue;
        protected String _UnitMWTag = null;
        protected String _UnitCondition = null;
        protected int _UnitIsKPI = int.MinValue;
        protected int _UnitIsSnapshot = int.MinValue;
        protected int _UnitIsSort = int.MinValue;
        protected int _UnitIsSecurity = int.MinValue;
        protected int _UnitIsPower = int.MinValue;
        protected String _WorkID = null;   //倒班表ID
        protected String _UnitNote = null;
        protected String _UnitCreateTime = null;
        protected String _UnitModifyTime = null;

		[Description("UnitID")]
		public virtual String UnitID {
			get {
				return this._UnitID;
			}
			set {
				this._UnitID = value;
			}
		}

		[Description("PlantID")]
		public virtual String PlantID {
			get {
				return this._PlantID;
			}
			set {
				this._PlantID = value;
			}
		}

		[Description("UnitCode")]
		public virtual String UnitCode {
			get {
				return this._UnitCode;
			}
			set {
				this._UnitCode = value;
			}
		}

		[Description("UnitName")]
		public virtual String UnitName {
			get {
				return this._UnitName;
			}
			set {
				this._UnitName = value;
			}
		}

		[Description("UnitDesc")]
		public virtual String UnitDesc {
			get {
				return this._UnitDesc;
			}
			set {
				this._UnitDesc = value;
			}
		}

		[Description("UnitIndex")]
		public virtual int UnitIndex {
			get {
				return this._UnitIndex;
			}
			set {
				this._UnitIndex = value;
			}
		}

		[Description("UnitIsValid")]
		public virtual int UnitIsValid {
			get {
				return this._UnitIsValid;
			}
			set {
				this._UnitIsValid = value;
			}
		}

		[Description("UnitPrefix")]
		public virtual String UnitPrefix {
			get {
				return this._UnitPrefix;
			}
			set {
				this._UnitPrefix = value;
			}
		}

		[Description("UnitMW")]
		public virtual decimal UnitMW {
			get {
				return this._UnitMW;
			}
			set {
				this._UnitMW = value;
			}
		}

		[Description("UnitMWTag")]
		public virtual String UnitMWTag {
			get {
				return this._UnitMWTag;
			}
			set {
				this._UnitMWTag = value;
			}
		}

		[Description("UnitCondition")]
		public virtual String UnitCondition {
			get {
				return this._UnitCondition;
			}
			set {
				this._UnitCondition = value;
			}
		}

		[Description("UnitIsKPI")]
		public virtual int UnitIsKPI {
			get {
				return this._UnitIsKPI;
			}
			set {
				this._UnitIsKPI = value;
			}
		}

		[Description("UnitIsSnapshot")]
		public virtual int UnitIsSnapshot {
			get {
				return this._UnitIsSnapshot;
			}
			set {
				this._UnitIsSnapshot = value;
			}
		}

		[Description("UnitIsSort")]
		public virtual int UnitIsSort {
			get {
				return this._UnitIsSort;
			}
			set {
				this._UnitIsSort = value;
			}
		}

		[Description("UnitIsSecurity")]
		public virtual int UnitIsSecurity {
			get {
				return this._UnitIsSecurity;
			}
			set {
				this._UnitIsSecurity = value;
			}
		}

		[Description("UnitIsPower")]
		public virtual int UnitIsPower {
			get {
				return this._UnitIsPower;
			}
			set {
				this._UnitIsPower = value;
			}
		}

		[Description("WorkID")]
		public virtual String WorkID {
			get {
				return this._WorkID;
			}
			set {
				this._WorkID = value;
			}
		}

		[Description("UnitNote")]
		public virtual String UnitNote {
			get {
				return this._UnitNote;
			}
			set {
				this._UnitNote = value;
			}
		}


        public virtual String UnitCreateTime
        {
            get
            {
                return this._UnitCreateTime;
            }
            set
            {
                this._UnitCreateTime = value;
            }
        }

        public virtual String UnitModifyTime
        {
            get
            {
                return this._UnitModifyTime;
            }
            set
            {
                this._UnitModifyTime = value;
            }
        }

        public override string InsertSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("insert into KPI_Unit(");

                if ((this.UnitID == null))
                {
                }
                else
                {
                    tmpBuild.Append("UnitID");
                    tmpBuild.Append(",");
                }

                if ((this.PlantID == null))
                {
                }
                else
                {
                    tmpBuild.Append("PlantID");
                    tmpBuild.Append(",");
                }

                if ((this.UnitCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("UnitCode");
                    tmpBuild.Append(",");
                }

                if ((this.UnitName == null))
                {
                }
                else
                {
                    tmpBuild.Append("UnitName");
                    tmpBuild.Append(",");
                }

                if ((this.UnitDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("UnitDesc");
                    tmpBuild.Append(",");
                }

                if ((this.UnitIndex == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("UnitIndex");
                    tmpBuild.Append(",");
                }

                if ((this.UnitIsValid == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("UnitIsValid");
                    tmpBuild.Append(",");
                }

                if ((this.UnitPrefix == null))
                {
                }
                else
                {
                    tmpBuild.Append("UnitPrefix");
                    tmpBuild.Append(",");
                }

				if ((this.UnitMW == decimal.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("UnitMW");
                    tmpBuild.Append(",");
                }

                if ((this.UnitMWTag == null))
                {
                }
                else
                {
                    tmpBuild.Append("UnitMWTag");
                    tmpBuild.Append(",");
                }

                if ((this.UnitCondition == null))
                {
                }
                else
                {
                    tmpBuild.Append("UnitCondition");
                    tmpBuild.Append(",");
                }

                if ((this.UnitIsKPI == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("UnitIsKPI");
                    tmpBuild.Append(",");
                }

                if ((this.UnitIsSnapshot == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("UnitIsSnapshot");
                    tmpBuild.Append(",");
                }

                if ((this.UnitIsSort == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("UnitIsSort");
                    tmpBuild.Append(",");
                }

                if ((this.UnitIsSecurity == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("UnitIsSecurity");
                    tmpBuild.Append(",");
                }

                if ((this.UnitIsPower == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("UnitIsPower");
                    tmpBuild.Append(",");
                }

                if ((this.WorkID == null))
                {
                }
                else
                {
                    tmpBuild.Append("WorkID");
                    tmpBuild.Append(",");
                }
                
                if ((this.UnitNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("UnitNote");
                    tmpBuild.Append(",");
                }

                if ((this.UnitCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("UnitCreateTime");
                    tmpBuild.Append(",");
                }

                if ((this.UnitModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("UnitModifyTime");
                    tmpBuild.Append(",");
                }


                ///////////////////////////////////////////////////////////////////////////////////////


                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }

                tmpBuild.Append(") values(");
                if ((this.UnitID == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + UnitID + "'");
                    tmpBuild.Append(",");
                }

                if ((this.PlantID == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + PlantID + "'");
                    tmpBuild.Append(",");
                }

                if ((this.UnitCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + UnitCode + "'");
                    tmpBuild.Append(",");
                }

                if ((this.UnitName == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + UnitName + "'");
                    tmpBuild.Append(",");
                }

                if ((this.UnitDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + UnitDesc + "'");
                    tmpBuild.Append(",");
                }

                if ((this.UnitIndex == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(UnitIndex.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.UnitIsValid == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(UnitIsValid.ToString());
                    tmpBuild.Append(",");
                }
                if ((this.UnitPrefix == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + UnitPrefix + "'");
                    tmpBuild.Append(",");
                }

				if ((this.UnitMW == decimal.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(UnitMW.ToString() );
                    tmpBuild.Append(",");
                }

                if ((this.UnitMWTag == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + UnitMWTag + "'");
                    tmpBuild.Append(",");
                }

                if ((this.UnitCondition == null))
                {
                }
                else
                {
                    string strtemp = UnitCondition.Replace("'", "''");
                    tmpBuild.Append("'" + strtemp + "'");
                    tmpBuild.Append(",");
                }

                if ((this.UnitIsKPI == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(UnitIsKPI.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.UnitIsSnapshot == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(UnitIsSnapshot.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.UnitIsSort == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(UnitIsSort.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.UnitIsSecurity == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(UnitIsSecurity.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.UnitIsPower == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(UnitIsPower.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.WorkID == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + WorkID + "'");
                    tmpBuild.Append(",");
                }
                
                if ((this.UnitNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + UnitNote + "'");
                    tmpBuild.Append(",");
                }

                if ((this.UnitCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + UnitCreateTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.UnitModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + UnitModifyTime + "'");
                    tmpBuild.Append(",");
                }

                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }

                tmpBuild.Append(")");

                string __tmpSql = tmpBuild.ToString();

                return __tmpSql;
            }
        }


        public override string UpdateSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("update KPI_Unit set ");
                if ((this.UnitID == null))
                {
                }
                else
                {
                    tmpBuild.Append("UnitID='" + UnitID + "'");
                    tmpBuild.Append(",");
                }

                if ((this.PlantID == null))
                {
                }
                else
                {
                    tmpBuild.Append("PlantID='" + PlantID + "'");
                    tmpBuild.Append(",");
                }

                if ((this.UnitCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("UnitCode='" + UnitCode + "'");
                    tmpBuild.Append(",");
                }

                if ((this.UnitName == null))
                {
                }
                else
                {
                    tmpBuild.Append("UnitName='" + UnitName + "'");
                    tmpBuild.Append(",");
                }

                if ((this.UnitDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("UnitDesc='" + UnitDesc + "'");
                    tmpBuild.Append(",");
                }

                if ((this.UnitIndex == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("UnitIndex=" + UnitIndex.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.UnitIsValid == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("UnitIsValid=" + UnitIsValid.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.UnitPrefix == null))
                {
                }
                else
                {
                    tmpBuild.Append("UnitPrefix='" + UnitPrefix + "'");
                    tmpBuild.Append(",");
                }

				if ((this.UnitMW == decimal.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("UnitMW="+UnitMW.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.UnitMWTag == null))
                {
                }
                else
                {
                    tmpBuild.Append("UnitMWTag='" + UnitMWTag + "'");
                    tmpBuild.Append(",");
                }

                if ((this.UnitCondition == null))
                {
                }
                else
                {
                    string strtemp = UnitCondition.Replace("'", "''");
                    tmpBuild.Append("UnitCondition='" + strtemp + "'");
                    tmpBuild.Append(",");
                }

                if ((this.UnitIsKPI == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("UnitIsKPI=" + UnitIsKPI.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.UnitIsSnapshot == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("UnitIsSnapshot=" + UnitIsSnapshot.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.UnitIsSort == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("UnitIsSort=" + UnitIsSort.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.UnitIsSecurity == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("UnitIsSecurity=" + UnitIsSecurity.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.UnitIsPower == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("UnitIsPower=" + UnitIsPower.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.WorkID == null))
                {
                }
                else
                {
                    tmpBuild.Append("WorkID='" + WorkID + "'");
                    tmpBuild.Append(",");
                }
                
                if ((this.UnitNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("UnitNote='" + UnitNote + "'");
                    tmpBuild.Append(",");
                }

                if ((this.UnitCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("UnitCreateTime='" + UnitCreateTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.UnitModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("UnitModifyTime='" + UnitModifyTime + "'");
                    tmpBuild.Append(",");
                }

                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }

                tmpBuild.Append(" where ");
                tmpBuild.Append("UnitID='" + UnitID + "'");

                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }


        public override string DeleteSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("delete KPI_Unit");
                tmpBuild.Append(" where ");
                tmpBuild.Append("UnitID='" + UnitID + "'");

                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }

        public override string SelectSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("select ");
                tmpBuild.Append("UnitID");
                tmpBuild.Append(",");
                tmpBuild.Append("PlantID");
                tmpBuild.Append(",");
                tmpBuild.Append("UnitCode");
                tmpBuild.Append(",");
                tmpBuild.Append("UnitName");
                tmpBuild.Append(",");
                tmpBuild.Append("UnitDesc");
                tmpBuild.Append(",");
                tmpBuild.Append("UnitIndex");
                tmpBuild.Append(",");
                tmpBuild.Append("UnitIsValid");
                tmpBuild.Append(",");
                tmpBuild.Append("UnitPrefix");
                tmpBuild.Append(",");
                tmpBuild.Append("UnitMW");
                tmpBuild.Append(",");
                tmpBuild.Append("UnitMWTag");
                tmpBuild.Append(",");

                tmpBuild.Append("UnitCondition");
                tmpBuild.Append(",");
                tmpBuild.Append("UnitIsKPI");
                tmpBuild.Append(",");
                tmpBuild.Append("UnitIsSnapshot");
                tmpBuild.Append(",");
                tmpBuild.Append("UnitIsSort");
                tmpBuild.Append(",");
                tmpBuild.Append("UnitIsSecurity");
                tmpBuild.Append(",");
                tmpBuild.Append("UnitIsPower");
                tmpBuild.Append(",");
                tmpBuild.Append("WorkID");
                tmpBuild.Append(",");

                tmpBuild.Append("UnitNote");
                tmpBuild.Append(",");
                tmpBuild.Append("UnitCreateTime");
                tmpBuild.Append(",");
                tmpBuild.Append("UnitModifyTime");

                tmpBuild.Append(" from KPI_Unit");
                tmpBuild.Append(" where ");
                tmpBuild.Append("UnitID='" + UnitID + "'");

                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }

        public override bool DrToMember(System.Data.DataRow dr)
        {
            try
            {
                if (dr["UnitID"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.UnitID = dr["UnitID"].ToString();
                }

                if (dr["PlantID"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.PlantID = dr["PlantID"].ToString();
                }

                if (dr["UnitCode"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.UnitCode = dr["UnitCode"].ToString();
                }

                if (dr["UnitName"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.UnitName = dr["UnitName"].ToString();
                }

                if (dr["UnitDesc"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.UnitDesc = dr["UnitDesc"].ToString();
                }

                if (dr["UnitIndex"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.UnitIndex = int.Parse(dr["UnitIndex"].ToString());
                }

                if (dr["UnitIsValid"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.UnitIsValid = int.Parse(dr["UnitIsValid"].ToString());
                }


                if (dr["UnitPrefix"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.UnitPrefix = dr["UnitPrefix"].ToString();
                }


                if (dr["UnitMW"] == System.DBNull.Value)
                {
                }
                else
                {
					this.UnitMW = decimal.Parse(dr["UnitMW"].ToString());
                } 

                if (dr["UnitMWTag"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.UnitMWTag = dr["UnitMWTag"].ToString();
                }

                if (dr["UnitCondition"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.UnitCondition = dr["UnitCondition"].ToString();
                }

                if (dr["UnitIsKPI"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.UnitIsKPI = int.Parse(dr["UnitIsKPI"].ToString());
                }

                if (dr["UnitIsSnapshot"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.UnitIsSnapshot = int.Parse(dr["UnitIsSnapshot"].ToString());
                }

                if (dr["UnitIsSort"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.UnitIsSort = int.Parse(dr["UnitIsSort"].ToString());
                }

                if (dr["UnitIsSecurity"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.UnitIsSecurity = int.Parse(dr["UnitIsSecurity"].ToString());
                }

                if (dr["UnitIsPower"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.UnitIsPower = int.Parse(dr["UnitIsPower"].ToString());
                }

                if (dr["WorkID"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.WorkID = dr["WorkID"].ToString();
                }

                if (dr["UnitNote"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.UnitNote = dr["UnitNote"].ToString();
                }


                if (dr["UnitCreateTime"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.UnitCreateTime = dr["UnitCreateTime"].ToString();
                }


                if (dr["UnitModifyTime"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.UnitModifyTime = dr["UnitModifyTime"].ToString();
                }


            }
            catch (System.Exception )
            {
                // 如果有必要,请处理你的异常代码
                return false;
            }
            finally
            {
                // 异常的finally代码
            }
            return true;
        }
    }
}
