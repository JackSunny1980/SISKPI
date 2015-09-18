
namespace SIS.DataEntity
{
    using System;
    using System.Xml;
    using System.Data;
    using System.Text;
	using System.ComponentModel;
    
    [System.Serializable()]
    [System.Runtime.InteropServices.Guid("50e9801b-6409-4dc8-a554-9923a85743dc")]

    public class CurveTagEntity : EntityBase
    {
        protected String _CurveID = null;
        protected String _CurveCode = null;
        protected String _CurveName = null;
        protected String _CurveDesc = null;
        protected String _CurveGroup = null;
        protected String _CurveMonth = null;
        protected int _CurveIndex = int.MinValue;
        protected int _CurveIsValid = int.MinValue;

        protected int _CurveType = int.MinValue;      //0,1,2,3
        protected int _CurveGetType = int.MinValue;   //-1,0,1
        protected String _CurveXRealTag = null;

        protected String _CurveYRealTag = null;
        protected String _CurveZRealTag = null;
        protected String _CurveXYZ = null;
        protected String _CurveNote = null;
        protected String _CurveCreateTime = null;
        protected String _CurveModifyTime = null;

		[Description("CurveID")]
		public virtual String CurveID {
			get {
				return this._CurveID;
			}
			set {
				this._CurveID = value;
			}
		}

		[Description("CurveCode")]
		public virtual String CurveCode {
			get {
				return this._CurveCode;
			}
			set {
				this._CurveCode = value;
			}
		}

		[Description("CurveName")]
		public virtual String CurveName {
			get {
				return this._CurveName;
			}
			set {
				this._CurveName = value;
			}
		}

		[Description("CurveDesc")]
		public virtual String CurveDesc {
			get {
				return this._CurveDesc;
			}
			set {
				this._CurveDesc = value;
			}
		}

		[Description("CurveGroup")]
		public virtual String CurveGroup {
			get {
				return this._CurveGroup;
			}
			set {
				this._CurveGroup = value;
			}
		}

		[Description("CurveMonth")]
		public virtual String CurveMonth {
			get {
				return this._CurveMonth;
			}
			set {
				this._CurveMonth = value;
			}
		}

		[Description("CurveIndex")]
		public virtual int CurveIndex {
			get {
				return this._CurveIndex;
			}
			set {
				this._CurveIndex = value;
			}
		}

		[Description("CurveIsValid")]
		public virtual int CurveIsValid {
			get {
				return this._CurveIsValid;
			}
			set {
				this._CurveIsValid = value;
			}
		}

		[Description("CurveType")]
		public virtual int CurveType {
			get {
				return this._CurveType;
			}
			set {
				this._CurveType = value;
			}
		}

		[Description("CurveGetType")]
		public virtual int CurveGetType {
			get {
				return this._CurveGetType;
			}
			set {
				this._CurveGetType = value;
			}
		}

		[Description("CurveXRealTag")]
		public virtual String CurveXRealTag {
			get {
				return this._CurveXRealTag;
			}
			set {
				this._CurveXRealTag = value;
			}
		}

		[Description("CurveYRealTag")]
		public virtual String CurveYRealTag {
			get {
				return this._CurveYRealTag;
			}
			set {
				this._CurveYRealTag = value;
			}
		}

		[Description("CurveZRealTag")]
		public virtual String CurveZRealTag {
			get {
				return this._CurveZRealTag;
			}
			set {
				this._CurveZRealTag = value;
			}
		}

		[Description("CurveXYZ")]
		public virtual String CurveXYZ {
			get {
				return this._CurveXYZ;
			}
			set {
				this._CurveXYZ = value;
			}
		}

        public virtual String CurveNote
        {
            get
            {
                return this._CurveNote;
            }
            set
            {
                this._CurveNote = value;
            }
        }

        public virtual String CurveCreateTime
        {
            get
            {
                return this._CurveCreateTime;
            }
            set
            {
                this._CurveCreateTime = value;
            }
        }

        public virtual String CurveModifyTime
        {
            get
            {
                return this._CurveModifyTime;
            }
            set
            {
                this._CurveModifyTime = value;
            }
        }

        public override string InsertSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("insert into KPI_CurveTag (");

                if ((this.CurveID == null))
                {
                }
                else
                {
                    tmpBuild.Append("CurveID");
                    tmpBuild.Append(",");
                }
                if ((this.CurveCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("CurveCode");
                    tmpBuild.Append(",");
                }   

                if ((this.CurveName == null))
                {
                }
                else
                {
                    tmpBuild.Append("CurveName");
                    tmpBuild.Append(",");
                }

                if ((this.CurveDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("CurveDesc");
                    tmpBuild.Append(",");
                }

                if ((this.CurveGroup == null))
                {
                }
                else
                {
                    tmpBuild.Append("CurveGroup");
                    tmpBuild.Append(",");
                }

                if ((this.CurveMonth == null))
                {
                }
                else
                {
                    tmpBuild.Append("CurveMonth");
                    tmpBuild.Append(",");
                }
                if ((this.CurveIndex == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("CurveIndex");
                    tmpBuild.Append(",");
                } 

                if ((this.CurveIsValid == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("CurveIsValid");
                    tmpBuild.Append(",");
                }
                
                if ((this.CurveType == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("CurveType");
                    tmpBuild.Append(",");
                }
                if ((this.CurveGetType == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("CurveGetType");
                    tmpBuild.Append(",");
                }

                if ((this.CurveXRealTag == null))
                {
                }
                else
                {
                    tmpBuild.Append("CurveXRealTag");
                    tmpBuild.Append(",");
                }
                if ((this.CurveYRealTag == null))
                {
                }
                else
                {
                    tmpBuild.Append("CurveYRealTag");
                    tmpBuild.Append(",");
                }
                if ((this.CurveZRealTag == null))
                {
                }
                else
                {
                    tmpBuild.Append("CurveZRealTag");
                    tmpBuild.Append(",");
                }
                if ((this.CurveXYZ == null))
                {
                }
                else
                {
                    tmpBuild.Append("CurveXYZ");
                    tmpBuild.Append(",");
                }

                if ((this.CurveNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("CurveNote");
                    tmpBuild.Append(",");
                }

                if ((this.CurveCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("CurveCreateTime");
                    tmpBuild.Append(",");
                }

                if ((this.CurveModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("CurveModifyTime");
                    tmpBuild.Append(",");
                }
                
                ///////////////////////////////////////////////////////////////////////////////////////


                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }

                tmpBuild.Append(") values(");

                if ((this.CurveID == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" +CurveID+"'");
                    tmpBuild.Append(",");
                }
                
                if ((this.CurveCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("'"+CurveCode+"'");
                    tmpBuild.Append(",");
                }

                if ((this.CurveName == null))
                {
                }
                else
                {
                    tmpBuild.Append("'"+CurveName+"'");
                    tmpBuild.Append(",");
                }

                if ((this.CurveDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("'"+CurveDesc+"'");
                    tmpBuild.Append(",");
                }

                if ((this.CurveGroup == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + CurveGroup + "'");
                    tmpBuild.Append(",");
                }

                if ((this.CurveMonth == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + CurveMonth + "'");
                    tmpBuild.Append(",");
                }

                if ((this.CurveIndex == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(CurveIndex.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.CurveIsValid == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(CurveIsValid.ToString());
                    tmpBuild.Append(",");
                }
                
                if ((this.CurveType == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(CurveType.ToString());
                    tmpBuild.Append(",");
                }
                if ((this.CurveGetType == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(CurveGetType.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.CurveXRealTag == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + CurveXRealTag + "'");
                    tmpBuild.Append(",");
                }


                if ((this.CurveYRealTag == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + CurveYRealTag + "'");
                    tmpBuild.Append(",");
                }

                if ((this.CurveZRealTag == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + CurveZRealTag + "'");
                    tmpBuild.Append(",");
                }

                if ((this.CurveXYZ == null))
                {
                }
                else
                {
                    //函数引用了指标后，存在'，需要特殊处理
                    string strtemp = this.CurveXYZ.Replace("'", "''");

                    tmpBuild.Append("'" + strtemp + "'");
                    tmpBuild.Append(",");
                }

                if ((this.CurveNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + CurveNote + "'");
                    tmpBuild.Append(",");
                }

                if ((this.CurveCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + CurveCreateTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.CurveModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + CurveModifyTime + "'");
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
                tmpBuild.Append("update KPI_CurveTag set ");

                if ((this.CurveID == null))
                {
                }
                else
                {
                    tmpBuild.Append("CurveID='" + CurveID + "'");
                    tmpBuild.Append(",");
                }

                if ((this.CurveCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("CurveCode='" + CurveCode + "'");
                    tmpBuild.Append(",");
                }

                if ((this.CurveName == null))
                {
                }
                else
                {
                    tmpBuild.Append("CurveName='" + CurveName + "'");
                    tmpBuild.Append(",");
                }

                if ((this.CurveDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("CurveDesc='" + CurveDesc + "'");
                    tmpBuild.Append(",");
                }

                if ((this.CurveGroup == null))
                {
                }
                else
                {
                    tmpBuild.Append("CurveGroup='" + CurveGroup + "'");
                    tmpBuild.Append(",");
                }

                if ((this.CurveMonth == null))
                {
                }
                else
                {
                    tmpBuild.Append("CurveMonth='" + CurveMonth + "'");
                    tmpBuild.Append(",");
                }

                if ((this.CurveIndex == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("CurveIndex=" + CurveIndex.ToString());
                    tmpBuild.Append(",");
                }
                
                if ((this.CurveIsValid == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("CurveIsValid="+CurveIsValid.ToString());
                    tmpBuild.Append(",");

                }

                if ((this.CurveType == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("CurveType=" + CurveType.ToString());
                    tmpBuild.Append(",");
                }
                if ((this.CurveGetType == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("CurveGetType=" + CurveGetType.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.CurveXRealTag == null))
                {
                }
                else
                {
                    tmpBuild.Append("CurveXRealTag='" + CurveXRealTag + "'");
                    tmpBuild.Append(",");
                }

                if ((this.CurveYRealTag == null))
                {
                }
                else
                {
                    tmpBuild.Append("CurveYRealTag='" + CurveYRealTag + "'");
                    tmpBuild.Append(",");
                }

                if ((this.CurveZRealTag == null))
                {
                }
                else
                {
                    tmpBuild.Append("CurveZRealTag='" + CurveZRealTag + "'");
                    tmpBuild.Append(",");
                }

                if ((this.CurveXYZ == null))
                {
                }
                else
                {
                    tmpBuild.Append("CurveXYZ='" + CurveXYZ + "'");
                    tmpBuild.Append(",");
                }
                
                if ((this.CurveNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("CurveNote='" + CurveNote + "'");
                    tmpBuild.Append(",");
                }

                if ((this.CurveCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("CurveCreateTime='" + CurveCreateTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.CurveModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("CurveModifyTime='" + CurveModifyTime + "'");
                    tmpBuild.Append(",");
                }

                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }

                tmpBuild.Append(" where ");
                tmpBuild.Append("CurveID='" + CurveID + "'");

                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }

        public override string DeleteSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("delete KPI_CurveTag");
                tmpBuild.Append(" where ");
                tmpBuild.Append("CurveID='" + CurveID + "'");

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
                tmpBuild.Append("CurveID");
                tmpBuild.Append(",");
                tmpBuild.Append("CurveCode");
                tmpBuild.Append(",");
                tmpBuild.Append("CurveName");
                tmpBuild.Append(",");
                tmpBuild.Append("CurveDesc");
                tmpBuild.Append(",");
                tmpBuild.Append("CurveGroup");
                tmpBuild.Append(",");
                tmpBuild.Append("CurveMonth");
                tmpBuild.Append(",");
                tmpBuild.Append("CurveIndex");
                tmpBuild.Append(",");
                tmpBuild.Append("CurveIsValid");
                tmpBuild.Append(",");
                tmpBuild.Append("CurveType");
                tmpBuild.Append(",");
                tmpBuild.Append("CurveGetType");
                tmpBuild.Append(",");
                tmpBuild.Append("CurveXRealTag");
                tmpBuild.Append(",");
                tmpBuild.Append("CurveYRealTag");
                tmpBuild.Append(",");
                tmpBuild.Append("CurveZRealTag");
                tmpBuild.Append(",");
                tmpBuild.Append("CurveXYZ");
                tmpBuild.Append(",");
                tmpBuild.Append("CurveNote");
                tmpBuild.Append(",");
                tmpBuild.Append("CurveCreateTime");
                tmpBuild.Append(",");
                tmpBuild.Append("CurveModifyTime");

                tmpBuild.Append(" from KPI_CurveTag ");
                tmpBuild.Append(" where ");
                tmpBuild.Append("CurveID='" + CurveID + "'");

                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }

        public override bool DrToMember(System.Data.DataRow dr)
        {
            try
            {
                if (dr["CurveID"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.CurveID = dr["CurveID"].ToString();
                }
              
                if (dr["CurveCode"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.CurveCode = dr["CurveCode"].ToString();
                }

                if (dr["CurveName"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.CurveName = dr["CurveName"].ToString();
                }

                if (dr["CurveDesc"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.CurveDesc = dr["CurveDesc"].ToString();
                }

                if (dr["CurveGroup"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.CurveGroup = dr["CurveGroup"].ToString();
                }

                if (dr["CurveMonth"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.CurveMonth = dr["CurveMonth"].ToString();
                }

                if (dr["CurveIndex"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.CurveIndex = int.Parse(dr["CurveIndex"].ToString());
                }

                
                if (dr["CurveIsValid"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.CurveIsValid = int.Parse(dr["CurveIsValid"].ToString());
                }


                if ((dr["CurveType"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.CurveType = int.Parse(dr["CurveType"].ToString());
                }
                if ((dr["CurveGetType"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.CurveGetType = int.Parse(dr["CurveGetType"].ToString());
                }

                if ((dr["CurveXRealTag"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.CurveXRealTag = dr["CurveXRealTag"].ToString();
                }
                if ((dr["CurveYRealTag"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.CurveYRealTag = dr["CurveYRealTag"].ToString();
                }
                if ((dr["CurveZRealTag"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.CurveZRealTag = dr["CurveZRealTag"].ToString();
                }
                if ((dr["CurveXYZ"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.CurveXYZ = dr["CurveXYZ"].ToString();
                }
                
                if (dr["CurveNote"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.CurveNote = dr["CurveNote"].ToString();
                }

                if (dr["CurveCreateTime"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.CurveCreateTime = dr["CurveCreateTime"].ToString();
                }


                if (dr["CurveModifyTime"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.CurveModifyTime = dr["CurveModifyTime"].ToString();
                }


            }
            catch (System.Exception e)
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
