
namespace SIS.DataEntity
{
    using System;
    using System.Xml;
    using System.Data;
    using System.Text;
	using System.ComponentModel;
    
    [System.Serializable()]
    [System.Runtime.InteropServices.Guid("d3a5e7b9-bc2d-4a76-865c-f47d9367b286")]
    public class CycleEntity : EntityBase
    {
        protected String _CycleID = null;
        protected String _CycleName = null;
        protected String _CycleDesc = null;
        protected Int32 _CycleValue = Int32.MinValue;
        protected String _CycleNote = null;
        protected String _CycleCreateTime = null;
        protected String _CycleModifyTime = null;

        /// 该字段已经被作为Where的一部分
		[Description("CycleID")]
		public String CycleID {
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

		[Description("CycleDesc")]
		public virtual String CycleDesc {
			get {
				return this._CycleDesc;
			}
			set {
				this._CycleDesc = value;
			}
		}

		[Description("CycleValue")]
		public virtual Int32 CycleValue {
			get {
				return this._CycleValue;
			}
			set {
				this._CycleValue = value;
			}
		}

        public virtual String CycleNote
        {
            get
            {
                return this._CycleNote;
            }
            set
            {
                this._CycleNote = value;
            }
        }

        public virtual String CycleCreateTime
        {
            get
            {
                return this._CycleCreateTime;
            }
            set
            {
                this._CycleCreateTime = value;
            }
        }

        public virtual String CycleModifyTime
        {
            get
            {
                return this._CycleModifyTime;
            }
            set
            {
                this._CycleModifyTime = value;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public override string InsertSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("insert into KPI_Cycle(");
                if ((this.CycleID == null))
                {
                }
                else
                {
                    tmpBuild.Append("CycleID");
                    tmpBuild.Append(",");
                }
                if ((this.CycleName == null))
                {
                }
                else
                {
                    tmpBuild.Append("CycleName");
                    tmpBuild.Append(",");
                }
                if ((this.CycleDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("CycleDesc");
                    tmpBuild.Append(",");
                }
                if ((this.CycleValue == Int32.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("CycleValue");
                    tmpBuild.Append(",");
                }

                if ((this.CycleNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("CycleNote");
                    tmpBuild.Append(",");
                }

                if ((this.CycleCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("CycleCreateTime");
                    tmpBuild.Append(",");
                }

                if ((this.CycleModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("CycleModifyTime");
                    tmpBuild.Append(",");
                }

                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }
                tmpBuild.Append(") values(");

                if ((this.CycleID == null))
                {
                }
                else
                {
                    tmpBuild.Append("'"+CycleID+"'");
                    tmpBuild.Append(",");
                }
                if ((this.CycleName == null))
                {
                }
                else
                {
                    tmpBuild.Append("'"+CycleName+"'");
                    tmpBuild.Append(",");
                }
                if ((this.CycleDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + CycleDesc + "'");
                    tmpBuild.Append(",");
                }
                if ((this.CycleValue == Int32.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(CycleValue.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.CycleNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + CycleNote + "'");
                    tmpBuild.Append(",");
                }

                if ((this.CycleCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + CycleCreateTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.CycleModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + CycleModifyTime + "'");
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


        /// <summary>
        /// 
        /// </summary>
        public override string UpdateSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("update KPI_Cycle set ");
                if ((this.CycleName == null))
                {
                }
                else
                {
                    tmpBuild.Append("CycleName='"+CycleName+"'");
                    tmpBuild.Append(",");
                }
                if ((this.CycleDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("CycleDesc='" + CycleDesc + "'");
                    tmpBuild.Append(",");
                }
                if ((this.CycleValue == Int32.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("CycleValue=" + CycleValue.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.CycleNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("CycleNote='" + CycleNote + "'");
                    tmpBuild.Append(",");
                }

                if ((this.CycleCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("CycleCreateTime='" + CycleCreateTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.CycleModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("CycleModifyTime='" + CycleModifyTime + "'");
                    tmpBuild.Append(",");
                }
                
                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }
                tmpBuild.Append(" where ");
                tmpBuild.Append("CycleID='"+CycleID+"'");
                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }
        public override string DeleteSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("delete KPI_Cycle");
                tmpBuild.Append(" where ");
                tmpBuild.Append("CycleID='"+CycleID+"'");
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
                tmpBuild.Append("CycleID");
                tmpBuild.Append(",");
                tmpBuild.Append("CycleName");
                tmpBuild.Append(",");
                tmpBuild.Append("CycleDesc");
                tmpBuild.Append(",");
                tmpBuild.Append("CycleValue");
                tmpBuild.Append(",");
                tmpBuild.Append("CycleNote");
                tmpBuild.Append(",");
                tmpBuild.Append("CycleCreateTime");
                tmpBuild.Append(",");
                tmpBuild.Append("CycleModifyTime");

                tmpBuild.Append(" from KPI_Cycle");
                tmpBuild.Append(" where ");
                tmpBuild.Append("CycleID='"+CycleID+"'");
                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }
        public override bool DrToMember(System.Data.DataRow dr)
        {
            try
            {
                if ((dr["CycleID"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.CycleID = dr["CycleID"].ToString();
                }
                if ((dr["CycleName"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.CycleName = dr["CycleName"].ToString();
                }
                if ((dr["CycleDesc"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.CycleDesc = dr["CycleDesc"].ToString();
                }
                if ((dr["CycleValue"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.CycleValue = int.Parse(dr["CycleValue"].ToString());
                }

                if ((dr["CycleNote"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.CycleNote = dr["CycleNote"].ToString();
                }

                if ((dr["CycleCreateTime"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.CycleCreateTime = dr["CycleCreateTime"].ToString();
                }
                if ((dr["CycleModifyTime"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.CycleModifyTime = dr["CycleModifyTime"].ToString();
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
