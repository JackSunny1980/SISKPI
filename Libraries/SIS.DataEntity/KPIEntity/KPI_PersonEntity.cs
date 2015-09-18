
///////////////////////////////////////////////////////////////////////////////////////////////////
//wuguanhui
//
//
///////////////////////////////////////////////////////////////////////////////////////////////////

namespace SIS.DataEntity
{
    using System;
    using System.Xml;
    using System.Data;
    using System.Text;
    using System.Collections.Generic;
	using System.ComponentModel;

    [System.Serializable()]
    [System.Runtime.InteropServices.Guid("811708b6-a974-478d-aac9-3be6e2437b18")]
    public class KPI_PersonEntity : EntityBase
    {
        protected String _PersonID = null;
        protected String _PositionID = null;
        protected String _PersonCode = null;
        protected String _PersonName = null;
        protected String _PersonDesc = null;
        protected String _PersonIsValid = null;
        protected String _PersonNote = null;
		//Added by pyf 2013-11-01
		protected String _Shift = null;
		protected String _UnitID = null;
		protected String _UnitName = null;
		protected String _PositionName = null;
		protected String _SpecialField = null;
		//End of Added.
        protected String _PersonCreateTime = null;
        protected String _PersonModifyTime = null;

		[Description("PositionName")]
        public string PositionName { get; set; }

        public string BindingValue { get; set; }
        /// 该字段已经被作为Where的一部分
		[Description("PersonID")]
        public virtual String PersonID
        {
            get
            {
                return this._PersonID;
            }
            set
            {
                this._PersonID = value;
            }
        }

		[Description("PositionID")]
        public virtual String PositionID
        {
            get
            {
                return this._PositionID;
            }
            set
            {
                this._PositionID = value;
            }
        }

		[Description("PersonCode")]
        public virtual String PersonCode
        {
            get
            {
                return this._PersonCode;
            }
            set
            {
                this._PersonCode = value;
            }
        }

		[Description("PersonName")]
        public virtual String PersonName
        {
            get
            {
                return this._PersonName;
            }
            set
            {
                this._PersonName = value;
            }
        }

		[Description("SpecialField")]
		public virtual String SpecialField {
			get {
				return this._SpecialField;
			}
			set {
				this._SpecialField = value;
			}
		}

		[Description("PersonDesc")]
        public virtual String PersonDesc
        {
            get
            {
                return this._PersonDesc;
            }
            set
            {
                this._PersonDesc = value;
            }
        }

		[Description("PersonIsValid")]
        public virtual String PersonIsValid
        {
            get
            {
                return this._PersonIsValid;
            }
            set
            {
                this._PersonIsValid = value;
            }
        }

		[Description("PersonNote")]
        public virtual String PersonNote
        {
            get
            {
                return this._PersonNote;
            }
            set
            {
                this._PersonNote = value;
            }
        }

		[Description("Shift")]
		public virtual String Shift {
			get {
				return this._Shift;
			}
			set {
				this._Shift = value;
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

		[Description("UnitName")]
		public virtual String UnitName {
			get {
				return this._UnitName;
			}
			set {
				this._UnitName = value;
			}
		}


        public virtual String PersonCreateTime
        {
            get
            {
                return this._PersonCreateTime;
            }
            set
            {
                this._PersonCreateTime = value;
            }
        }
        public virtual String PersonModifyTime
        {
            get
            {
                return this._PersonModifyTime;
            }
            set
            {
                this._PersonModifyTime = value;
            }
        }


        /// <summary>
        /// ////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        public override string InsertSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("insert into KPI_Person(");
                if (this.PersonID == null)
                {
                }
                else
                {
                    tmpBuild.Append("PersonID");
                    tmpBuild.Append(",");
                }
                if (this.PositionID == null)
                {
                }
                else
                {
                    tmpBuild.Append("PositionID");
                    tmpBuild.Append(",");
                }
                if (this.PersonCode == null)
                {
                }
                else
                {
                    tmpBuild.Append("PersonCode");
                    tmpBuild.Append(",");
                }


                if (this.PersonName == null)
                {
                }
                else
                {
                    tmpBuild.Append("PersonName");
                    tmpBuild.Append(",");
                }

                if (this.PersonDesc == null)
                {
                }
                else
                {
                    tmpBuild.Append("PersonDesc");
                    tmpBuild.Append(",");
                }

                if (this.PersonIsValid == null)
                {
                }
                else
                {
                    tmpBuild.Append("PersonIsValid");
                    tmpBuild.Append(",");
                }

                if ((this.PersonNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("PersonNote");
                    tmpBuild.Append(",");
                }

                if (this.PersonCreateTime == null)
                {
                }
                else
                {
                    tmpBuild.Append("PersonCreateTime");
                    tmpBuild.Append(",");
                }

                if (this.PersonModifyTime == null)
                {
                }
                else
                {
                    tmpBuild.Append("PersonModifyTime");
                    tmpBuild.Append(",");
                }

				//Added by pyf 2013-11-01
				if (!String.IsNullOrEmpty(Shift)) {
					tmpBuild.Append("Shift,");					
				}

				if (!String.IsNullOrEmpty(UnitID)) {
					tmpBuild.Append("UnitID,");
				}

				if (!String.IsNullOrEmpty(SpecialField)) {
					tmpBuild.Append("SpecialField,");
				}
				//End of Added.


                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }
                tmpBuild.Append(") values(");

                if (this.PersonID == null)
                {
                }
                else
                {
                    tmpBuild.Append("'" + PersonID + "'");
                    tmpBuild.Append(",");
                }


                if (this.PositionID == null)
                {
                }
                else
                {
                    tmpBuild.Append("'" + PositionID + "'");
                    tmpBuild.Append(",");
                }
                if (this.PersonCode == null)
                {
                }
                else
                {
                    tmpBuild.Append("'" + PersonCode + "'");
                    tmpBuild.Append(",");
                }
                
                if (this.PersonName == null)
                {
                }
                else
                {
                    tmpBuild.Append("'" + PersonName + "'");
                    tmpBuild.Append(",");
                }

                if (this.PersonDesc == null)
                {
                }
                else
                {
                    tmpBuild.Append("'" + PersonDesc + "'");
                    tmpBuild.Append(",");
                }
                if ((this.PersonIsValid == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + PersonIsValid + "'");
                    tmpBuild.Append(",");
                }
                
                if ((this.PersonNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + PersonNote + "'");
                    tmpBuild.Append(",");
                }

                if ((this.PersonCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + PersonCreateTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.PersonModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + PersonModifyTime + "'");
                    tmpBuild.Append(",");
                }

				//Added by pyf 2013-11-01
				if (!String.IsNullOrEmpty(Shift)) {
					tmpBuild.Append(string.Format("'{0}',",Shift));
				}

				if (!String.IsNullOrEmpty(UnitID)) {
					tmpBuild.Append(string.Format("'{0}',", UnitID));
				}
				if (!String.IsNullOrEmpty(SpecialField)) {
					tmpBuild.Append(string.Format("'{0}',", SpecialField));
				}
				
				//End of Added.


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
                tmpBuild.Append("update KPI_Person set ");
                               
                if ((this.PositionID == null))
                {
                }
                else
                {
                    tmpBuild.Append("PositionID='" + PositionID + "'");
                    tmpBuild.Append(",");
                }

                if ((this.PersonCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("PersonCode='" + PersonCode + "'");
                    tmpBuild.Append(",");
                }    
                if ((this.PersonName == null))
                {
                }
                else
                {
                    tmpBuild.Append("PersonName='" + PersonName + "'");
                    tmpBuild.Append(",");
                }

                if ((this.PersonDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("PersonDesc='" + PersonDesc + "'");
                    tmpBuild.Append(",");
                }

                if ((this.PersonIsValid == null))
                {
                }
                else
                {
                    tmpBuild.Append("PersonIsValid='" + PersonIsValid + "'");
                    tmpBuild.Append(",");
                }
                
                if ((this.PersonNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("PersonNote='" + PersonNote + "'");
                    tmpBuild.Append(",");
                }

                if ((this.PersonCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("PersonCreateTime='" + PersonCreateTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.PersonModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("PersonModifyTime='" + PersonModifyTime + "'");
                    tmpBuild.Append(",");
                }

				//Added by pyf 2013-11-01
				if (!String.IsNullOrEmpty(Shift)) {
					tmpBuild.Append(string.Format("Shift = '{0}',", Shift));
				}

				if (!String.IsNullOrEmpty(UnitID)) {
					tmpBuild.Append(string.Format("UnitID='{0}',", UnitID));
				}
				if (!String.IsNullOrEmpty(SpecialField)) {
					tmpBuild.Append(string.Format("SpecialField='{0}',", SpecialField));
				}
				
				//End of Added.

                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }
                tmpBuild.Append(" where ");
                tmpBuild.Append("PersonID='" + PersonID + "'");

                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }

        public override string DeleteSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("delete KPI_Person");
                tmpBuild.Append(" where ");
                tmpBuild.Append("PersonID='" + PersonID + "'");
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
                tmpBuild.Append("PersonID");
                tmpBuild.Append(",");
                tmpBuild.Append("PositionID");
                tmpBuild.Append(",");
                tmpBuild.Append("PersonCode");
                tmpBuild.Append(",");
                tmpBuild.Append("PersonName");
                tmpBuild.Append(",");
                tmpBuild.Append("PersonDesc");
                tmpBuild.Append(",");
                tmpBuild.Append("PersonIsValid");
                tmpBuild.Append(",");
                tmpBuild.Append("PersonNote");
                tmpBuild.Append(",");
				tmpBuild.Append("Shift,UnitID");
                tmpBuild.Append("PersonCreateTime");
                tmpBuild.Append(",");
                tmpBuild.Append("PersonModifyTime");

                /****/

                tmpBuild.Append(" from KPI_Person");
                tmpBuild.Append(" where ");
                tmpBuild.Append("PersonID='" + PersonID + "'");
                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }

        public override bool DrToMember(System.Data.DataRow dr)
        {
            try
            {
                if ((dr["PersonID"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.PersonID = dr["PersonID"].ToString();
                }
                if ((dr["PositionID"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.PositionID = dr["PositionID"].ToString();
                }
                if ((dr["PersonCode"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.PersonCode = dr["PersonCode"].ToString();
                }
                if ((dr["PersonName"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.PersonName = dr["PersonName"].ToString();
                }
                if ((dr["PersonDesc"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.PersonDesc = dr["PersonDesc"].ToString();
                }

                if ((dr["PersonIsValid"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.PersonIsValid = dr["PersonIsValid"].ToString();
                }
                if ((dr["PersonNote"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.PersonNote = dr["PersonNote"].ToString();
                }
                if ((dr["PersonCreateTime"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.PersonCreateTime = dr["PersonCreateTime"].ToString();
                }
                if ((dr["PersonModifyTime"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.PersonModifyTime = dr["PersonModifyTime"].ToString();
                }


            }
            catch (System.Exception)
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
