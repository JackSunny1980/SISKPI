
namespace SIS.DataEntity
{
    using System;
    using System.Xml;
    using System.Data;
    using System.Text;
	using System.ComponentModel;
    
    [System.Serializable()]
    [System.Runtime.InteropServices.Guid("50e9801b-6409-4dc8-a554-9923a85743dc")]

    public class KPI_RealTagEntity : EntityBase
    {
        protected String _RealID = null;
        protected String _UnitID = null;
        protected String _RealCode = null;
        protected String _RealDesc = null;
        protected String _RealEngunit = null;
		protected decimal _RealMaxValue = decimal.MinValue;
		protected decimal _RealMinValue = decimal.MinValue;
        protected String _RealSnapshot = null;
        protected String _RealSort = null;
        protected String _RealDisplay = null;
        protected String _RealXYZ = null;
        protected String _RealNote = null;
        protected String _RealCreateTime = null;
        protected String _RealModifyTime = null;

		[Description("RealID")]
		public virtual String RealID {
			get {
				return this._RealID;
			}
			set {
				this._RealID = value;
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

		[Description("RealCode")]
		public virtual String RealCode {
			get {
				return this._RealCode;
			}
			set {
				this._RealCode = value;
			}
		}

		[Description("RealDesc")]
		public virtual String RealDesc {
			get {
				return this._RealDesc;
			}
			set {
				this._RealDesc = value;
			}
		}

		[Description("RealEngunit")]
		public virtual String RealEngunit {
			get {
				return this._RealEngunit;
			}
			set {
				this._RealEngunit = value;
			}
		}

		[Description("RealMaxValue")]
		public virtual decimal RealMaxValue {
			get {
				return this._RealMaxValue;
			}
			set {
				this._RealMaxValue = value;
			}
		}

		[Description("RealMinValue")]
		public virtual decimal RealMinValue {
			get {
				return this._RealMinValue;
			}
			set {
				this._RealMinValue = value;
			}
		}

		[Description("RealSnapshot")]
		public virtual String RealSnapshot {
			get {
				return this._RealSnapshot;
			}
			set {
				this._RealSnapshot = value;
			}
		}

		[Description("RealSort")]
		public virtual String RealSort {
			get {
				return this._RealSort;
			}
			set {
				this._RealSort = value;
			}
		}

		[Description("RealDisplay")]
		public virtual String RealDisplay {
			get {
				return this._RealDisplay;
			}
			set {
				this._RealDisplay = value;
			}
		}

		[Description("RealXYZ")]
		public virtual String RealXYZ {
			get {
				return this._RealXYZ;
			}
			set {
				this._RealXYZ = value;
			}
		}

                
        public virtual String RealNote
        {
            get
            {
                return this._RealNote;
            }
            set
            {
                this._RealNote = value;
            }
        }

        public virtual String RealCreateTime
        {
            get
            {
                return this._RealCreateTime;
            }
            set
            {
                this._RealCreateTime = value;
            }
        }

        public virtual String RealModifyTime
        {
            get
            {
                return this._RealModifyTime;
            }
            set
            {
                this._RealModifyTime = value;
            }
        }

        public override string InsertSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("insert into KPI_RealTag (");

                if ((this.RealID == null))
                {
                }
                else
                {
                    tmpBuild.Append("RealID");
                    tmpBuild.Append(",");
                }
                if ((this.UnitID == null))
                {
                }
                else
                {
                    tmpBuild.Append("UnitID");
                    tmpBuild.Append(",");
                }

                if ((this.RealCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("RealCode");
                    tmpBuild.Append(",");
                }

                if ((this.RealDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("RealDesc");
                    tmpBuild.Append(",");
                }

                if ((this.RealEngunit == null))
                {
                }
                else
                {
                    tmpBuild.Append("RealEngunit");
                    tmpBuild.Append(",");
                }

                if ((this.RealMaxValue == decimal.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("RealMaxValue");
                    tmpBuild.Append(",");
                }
				if ((this.RealMinValue == decimal.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("RealMinValue");
                    tmpBuild.Append(",");
                }

                if ((this.RealSnapshot == null))
                {
                }
                else
                {
                    tmpBuild.Append("RealSnapshot");
                    tmpBuild.Append(",");
                }
                if ((this.RealSort == null))
                {
                }
                else
                {
                    tmpBuild.Append("RealSort");
                    tmpBuild.Append(",");
                }


                if ((this.RealDisplay == null))
                {
                }
                else
                {
                    tmpBuild.Append("RealDisplay");
                    tmpBuild.Append(",");
                }
                if ((this.RealXYZ == null))
                {
                }
                else
                {
                    tmpBuild.Append("RealXYZ");
                    tmpBuild.Append(",");
                }
                                
                if ((this.RealNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("RealNote");
                    tmpBuild.Append(",");
                }

                if ((this.RealCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("RealCreateTime");
                    tmpBuild.Append(",");
                }

                if ((this.RealModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("RealModifyTime");
                    tmpBuild.Append(",");
                }
                
                ///////////////////////////////////////////////////////////////////////////////////////


                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }

                tmpBuild.Append(") values(");

                if ((this.RealID == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" +RealID+"'");
                    tmpBuild.Append(",");
                }

                if ((this.UnitID == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + UnitID + "'");
                    tmpBuild.Append(",");
                }

                if ((this.RealCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + RealCode + "'");
                    tmpBuild.Append(",");
                }


                if ((this.RealDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + RealDesc + "'");
                    tmpBuild.Append(",");
                }


                if ((this.RealEngunit == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + RealEngunit + "'");
                    tmpBuild.Append(",");
                }


				if ((this.RealMaxValue == decimal.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(RealMaxValue.ToString());
                    tmpBuild.Append(",");
                }
				if ((this.RealMinValue == decimal.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(RealMinValue.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.RealSnapshot == null))
                {
                }
                else
                {
                    tmpBuild.Append("'"+RealSnapshot+"'");
                    tmpBuild.Append(",");
                }
                if ((this.RealSort == null))
                {
                }
                else
                {
                    tmpBuild.Append("'"+RealSort+"'");
                    tmpBuild.Append(",");
                }

                if ((this.RealDisplay == null))
                {
                }
                else
                {
                    tmpBuild.Append("'"+RealDisplay+"'");
                    tmpBuild.Append(",");
                }

                if ((this.RealXYZ == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + RealXYZ + "'");
                    tmpBuild.Append(",");
                }             
                if ((this.RealNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + RealNote + "'");
                    tmpBuild.Append(",");
                }

                if ((this.RealCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + RealCreateTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.RealModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + RealModifyTime + "'");
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
                tmpBuild.Append("update KPI_RealTag set ");

                if ((this.RealID == null))
                {
                }
                else
                {
                    tmpBuild.Append("RealID='" + RealID + "'");
                    tmpBuild.Append(",");
                }
                if ((this.UnitID == null))
                {
                }
                else
                {
                    tmpBuild.Append("UnitID='" + UnitID + "'");
                    tmpBuild.Append(",");
                }

                if ((this.RealCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("RealCode='" + RealCode + "'");
                    tmpBuild.Append(",");
                }
                if ((this.RealDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("RealDesc='" + RealDesc + "'");
                    tmpBuild.Append(",");
                }

                if ((this.RealEngunit == null))
                {
                }
                else
                {
                    tmpBuild.Append("RealEngunit='" + RealEngunit + "'");
                    tmpBuild.Append(",");
                }

				if ((this.RealMaxValue == decimal.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("RealMaxValue=" + RealMaxValue.ToString());
                    tmpBuild.Append(",");
                }
				if ((this.RealMinValue == decimal.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("RealMinValue=" + RealMinValue.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.RealSnapshot == null))
                {
                }
                else
                {
                    tmpBuild.Append("RealSnapshot='" + RealSnapshot+"'");
                    tmpBuild.Append(",");
                }
                if ((this.RealSort == null))
                {
                }
                else
                {
                    tmpBuild.Append("RealSort='" + RealSort+"'");
                    tmpBuild.Append(",");
                }

                if ((this.RealDisplay == null))
                {
                }
                else
                {
                    tmpBuild.Append("RealDisplay='"+RealDisplay+"'");
                    tmpBuild.Append(",");
                }

                if ((this.RealXYZ == null))
                {
                }
                else
                {
                    tmpBuild.Append("RealXYZ='" + RealXYZ + "'");
                    tmpBuild.Append(",");
                }


                if ((this.RealNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("RealNote='" + RealNote + "'");
                    tmpBuild.Append(",");
                }

                if ((this.RealCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("RealCreateTime='" + RealCreateTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.RealModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("RealModifyTime='" + RealModifyTime + "'");
                    tmpBuild.Append(",");
                }

                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }

                tmpBuild.Append(" where ");
                tmpBuild.Append("RealID='" + RealID + "'");

                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }

        public override string DeleteSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("delete KPI_RealTag");
                tmpBuild.Append(" where ");
                tmpBuild.Append("RealID='" + RealID + "'");

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
                tmpBuild.Append("RealID");
                tmpBuild.Append(",");
                tmpBuild.Append("UnitID");
                tmpBuild.Append(",");
                tmpBuild.Append("RealCode");
                tmpBuild.Append(",");
                tmpBuild.Append("RealDesc");
                tmpBuild.Append(",");
                tmpBuild.Append("RealEngunit");
                tmpBuild.Append(",");
                tmpBuild.Append("RealMaxValue");
                tmpBuild.Append(",");
                tmpBuild.Append("RealMinValue");
                tmpBuild.Append(",");
                tmpBuild.Append("RealSnapshot");
                tmpBuild.Append(",");
                tmpBuild.Append("RealSort");
                tmpBuild.Append(",");
                tmpBuild.Append("RealDisplay");
                tmpBuild.Append(",");
                tmpBuild.Append("RealXYZ");
                tmpBuild.Append(",");
                tmpBuild.Append("RealNote");
                tmpBuild.Append(",");
                tmpBuild.Append("RealCreateTime");
                tmpBuild.Append(",");
                tmpBuild.Append("RealModifyTime");

                tmpBuild.Append(" from KPI_RealTag ");
                tmpBuild.Append(" where ");
                tmpBuild.Append("RealID='" + RealID + "'");

                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }

        public override bool DrToMember(System.Data.DataRow dr)
        {
            try
            {
                if (dr["RealID"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.RealID = dr["RealID"].ToString();
                }
                if (dr["UnitID"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.UnitID = dr["UnitID"].ToString();
                }

                if (dr["RealCode"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.RealCode = dr["RealCode"].ToString();
                }
                if (dr["RealDesc"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.RealDesc = dr["RealDesc"].ToString();
                }

                if (dr["RealEngunit"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.RealEngunit = dr["RealEngunit"].ToString();
                }
                if (dr["RealMaxValue"] == System.DBNull.Value)
                {
                }
                else
                {
					this.RealMaxValue = decimal.Parse(dr["RealMaxValue"].ToString());
                }

                if (dr["RealMinValue"] == System.DBNull.Value)
                {
                }
                else
                {
					this.RealMinValue = decimal.Parse(dr["RealMinValue"].ToString());
                }

                if (dr["RealSnapshot"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.RealSnapshot = dr["RealSnapshot"].ToString();
                }
                if (dr["RealSort"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.RealSort = dr["RealSort"].ToString();
                }
                if (dr["RealDisplay"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.RealDisplay = dr["RealDisplay"].ToString();
                }
                if (dr["RealXYZ"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.RealXYZ = dr["RealXYZ"].ToString();
                }
              
               
                if (dr["RealNote"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.RealNote = dr["RealNote"].ToString();
                }

                if (dr["RealCreateTime"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.RealCreateTime = dr["RealCreateTime"].ToString();
                }


                if (dr["RealModifyTime"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.RealModifyTime = dr["RealModifyTime"].ToString();
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
