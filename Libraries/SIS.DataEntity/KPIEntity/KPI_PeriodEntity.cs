
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
	using System.ComponentModel;
    using System.Collections.Generic;

    [System.Serializable()]
    [System.Runtime.InteropServices.Guid("811708b6-a974-478d-aac9-3be6e2437b18")]
    public class KPI_PeriodEntity : EntityBase
    {
        protected String _PeriodID = null;
        protected String _PeriodCode = null;
        protected String _PeriodName = null;
        protected String _PeriodDesc = null;
        protected decimal _PeriodStartHour = decimal.MinValue;
		protected decimal _PeriodEndHour = decimal.MinValue;
		protected decimal _PeriodHours = decimal.MinValue;
        protected String _PeriodIsIDL = null;
        protected String _PeriodIsValid = null;
        protected String _PeriodNote = null;
        protected String _PeriodCreateTime = null;
        protected String _PeriodModifyTime = null;


        /// 该字段已经被作为Where的一部分
		[Description("PeriodID")]
        public virtual String PeriodID
        {
            get
            {
                return this._PeriodID;
            }
            set
            {
                this._PeriodID = value;
            }
        }

		[Description("PeriodCode")]
        public virtual String PeriodCode
        {
            get
            {
                return this._PeriodCode;
            }
            set
            {
                this._PeriodCode = value;
            }
        }

		[Description("PeriodName")]
        public virtual String PeriodName
        {
            get
            {
                return this._PeriodName;
            }
            set
            {
                this._PeriodName = value;
            }
        }

		[Description("PeriodDesc")]
        public virtual String PeriodDesc
        {
            get
            {
                return this._PeriodDesc;
            }
            set
            {
                this._PeriodDesc = value;
            }
        }

		[Description("PeriodStartHour")]
        public virtual decimal PeriodStartHour
        {
            get
            {
                return this._PeriodStartHour;
            }
            set
            {
                this._PeriodStartHour = value;
            }
        }

		[Description("PeriodEndHour")]
        public virtual decimal PeriodEndHour
        {
            get
            {
                return this._PeriodEndHour;
            }
            set
            {
                this._PeriodEndHour = value;
            }
        }

		[Description("PeriodHours")]
        public virtual decimal PeriodHours
        {
            get
            {
                return this._PeriodHours;
            }
            set
            {
                this._PeriodHours = value;
            }
        }

		[Description("PeriodIsIDL")]
        public virtual String PeriodIsIDL
        {
            get
            {
                return this._PeriodIsIDL;
            }
            set
            {
                this._PeriodIsIDL = value;
            }
        }


		[Description("PeriodIsValid")]
        public virtual String PeriodIsValid
        {
            get
            {
                return this._PeriodIsValid;
            }
            set
            {
                this._PeriodIsValid = value;
            }
        }

		[Description("PeriodNote")]
        public virtual String PeriodNote
        {
            get
            {
                return this._PeriodNote;
            }
            set
            {
                this._PeriodNote = value;
            }
        }
        public virtual String PeriodCreateTime
        {
            get
            {
                return this._PeriodCreateTime;
            }
            set
            {
                this._PeriodCreateTime = value;
            }
        }
        public virtual String PeriodModifyTime
        {
            get
            {
                return this._PeriodModifyTime;
            }
            set
            {
                this._PeriodModifyTime = value;
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
                tmpBuild.Append("insert into KPI_Period(");
                if (this.PeriodID == null)
                {
                }
                else
                {
                    tmpBuild.Append("PeriodID");
                    tmpBuild.Append(",");
                }
                if (this.PeriodCode == null)
                {
                }
                else
                {
                    tmpBuild.Append("PeriodCode");
                    tmpBuild.Append(",");
                }

                if (this.PeriodName == null)
                {
                }
                else
                {
                    tmpBuild.Append("PeriodName");
                    tmpBuild.Append(",");
                }

                if (this.PeriodDesc == null)
                {
                }
                else
                {
                    tmpBuild.Append("PeriodDesc");
                    tmpBuild.Append(",");
                }
                if (this.PeriodStartHour == decimal.MinValue)
                {
                }
                else
                {
                    tmpBuild.Append("PeriodStartHour");
                    tmpBuild.Append(",");
                }

                if (this.PeriodEndHour == decimal.MinValue)
                {
                }
                else
                {
                    tmpBuild.Append("PeriodEndHour");
                    tmpBuild.Append(",");
                }

                if (this.PeriodHours == decimal.MinValue)
                {
                }
                else
                {
                    tmpBuild.Append("PeriodHours");
                    tmpBuild.Append(",");
                }
                if (this.PeriodIsIDL == null)
                {
                }
                else
                {
                    tmpBuild.Append("PeriodIsIDL");
                    tmpBuild.Append(",");
                }

                if (this.PeriodIsValid == null)
                {
                }
                else
                {
                    tmpBuild.Append("PeriodIsValid");
                    tmpBuild.Append(",");
                }

                if ((this.PeriodNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("PeriodNote");
                    tmpBuild.Append(",");
                }

                if (this.PeriodCreateTime == null)
                {
                }
                else
                {
                    tmpBuild.Append("PeriodCreateTime");
                    tmpBuild.Append(",");
                }

                if (this.PeriodModifyTime == null)
                {
                }
                else
                {
                    tmpBuild.Append("PeriodModifyTime");
                    tmpBuild.Append(",");
                }


                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }
                tmpBuild.Append(") values(");

                if (this.PeriodID == null)
                {
                }
                else
                {
                    tmpBuild.Append("'" + PeriodID + "'");
                    tmpBuild.Append(",");
                }
                if (this.PeriodCode == null)
                {
                }
                else
                {
                    tmpBuild.Append("'" + PeriodCode + "'");
                    tmpBuild.Append(",");
                }
                
                if (this.PeriodName == null)
                {
                }
                else
                {
                    tmpBuild.Append("'" + PeriodName + "'");
                    tmpBuild.Append(",");
                }

                if (this.PeriodDesc == null)
                {
                }
                else
                {
                    tmpBuild.Append("'" + PeriodDesc + "'");
                    tmpBuild.Append(",");
                }
                
                if ((this.PeriodStartHour == Int32.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(PeriodStartHour.ToString());
                    tmpBuild.Append(",");
                }
                if ((this.PeriodEndHour == Int32.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(PeriodEndHour.ToString());
                    tmpBuild.Append(",");
                }
                if ((this.PeriodHours == Int32.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(PeriodHours.ToString());
                    tmpBuild.Append(",");
                }
                if ((this.PeriodIsIDL == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" +PeriodIsIDL + "'");
                    tmpBuild.Append(",");
                }
                if ((this.PeriodIsValid == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + PeriodIsValid + "'");
                    tmpBuild.Append(",");
                }
                if ((this.PeriodNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + PeriodNote + "'");
                    tmpBuild.Append(",");
                }

                if ((this.PeriodCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + PeriodCreateTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.PeriodModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + PeriodModifyTime + "'");
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
                tmpBuild.Append("update KPI_Period set ");


                if ((this.PeriodCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("PeriodCode='" + PeriodCode + "'");
                    tmpBuild.Append(",");
                }
                if ((this.PeriodName == null))
                {
                }
                else
                {
                    tmpBuild.Append("PeriodName='" + PeriodName + "'");
                    tmpBuild.Append(",");
                }

                if ((this.PeriodDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("PeriodDesc='" + PeriodDesc + "'");
                    tmpBuild.Append(",");
                }


                if ((this.PeriodStartHour == decimal.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("PeriodStartHour=" + PeriodStartHour.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.PeriodEndHour == decimal.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("PeriodEndHour=" + PeriodEndHour.ToString());
                    tmpBuild.Append(",");
                }
                if ((this.PeriodHours == decimal.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("PeriodHours=" + PeriodHours.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.PeriodIsIDL == null))
                {
                }
                else
                {
                    tmpBuild.Append("PeriodIsIDL='" + PeriodIsIDL+ "'");
                    tmpBuild.Append(",");
                }
                
                if ((this.PeriodIsValid == null))
                {
                }
                else
                {
                    tmpBuild.Append("PeriodIsValid='" + PeriodIsValid + "'");
                    tmpBuild.Append(",");
                }

                if ((this.PeriodNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("PeriodNote='" + PeriodNote + "'");
                    tmpBuild.Append(",");
                }

                if ((this.PeriodCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("PeriodCreateTime='" + PeriodCreateTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.PeriodModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("PeriodModifyTime='" + PeriodModifyTime + "'");
                    tmpBuild.Append(",");
                }

                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }
                tmpBuild.Append(" where ");
                tmpBuild.Append("PeriodID='" + PeriodID + "'");

                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }

        public override string DeleteSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("delete KPI_Period");
                tmpBuild.Append(" where ");
                tmpBuild.Append("PeriodID='" + PeriodID + "'");
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
                tmpBuild.Append("PeriodID");
                tmpBuild.Append(",");
                tmpBuild.Append("PeriodCode");
                tmpBuild.Append(",");
                tmpBuild.Append("PeriodName");
                tmpBuild.Append(",");
                tmpBuild.Append("PeriodDesc");
                tmpBuild.Append(",");
                tmpBuild.Append("PeriodStartHour");
                tmpBuild.Append(",");
                tmpBuild.Append("PeriodEndHour");
                tmpBuild.Append(",");
                tmpBuild.Append("PeriodHours");
                tmpBuild.Append(",");
                tmpBuild.Append("PeriodIsIDL");
                tmpBuild.Append(",");
                tmpBuild.Append("PeriodIsValid");
                tmpBuild.Append(",");
                tmpBuild.Append("PeriodNote");
                tmpBuild.Append(",");
                tmpBuild.Append("PeriodCreateTime");
                tmpBuild.Append(",");
                tmpBuild.Append("PeriodModifyTime");

                /****/

                tmpBuild.Append(" from KPI_Period");
                tmpBuild.Append(" where ");
                tmpBuild.Append("PeriodID='" + PeriodID + "'");
                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }

        public override bool DrToMember(System.Data.DataRow dr)
        {
            try
            {
                if ((dr["PeriodID"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.PeriodID = dr["PeriodID"].ToString();
                }
                if ((dr["PeriodCode"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.PeriodCode = dr["PeriodCode"].ToString();
                }
                if ((dr["PeriodName"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.PeriodName = dr["PeriodName"].ToString();
                }
                if ((dr["PeriodDesc"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.PeriodDesc = dr["PeriodDesc"].ToString();
                }

                if ((dr["PeriodStartHour"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.PeriodStartHour = decimal.Parse(dr["PeriodStartHour"].ToString());
                }
                if ((dr["PeriodEndHour"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.PeriodEndHour = decimal.Parse(dr["PeriodEndHour"].ToString());
                }
                if ((dr["PeriodHours"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.PeriodHours = decimal.Parse(dr["PeriodHours"].ToString());
                }
                if ((dr["PeriodIsIDL"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.PeriodIsIDL = dr["PeriodIsIDL"].ToString();
                }

                if ((dr["PeriodIsValid"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.PeriodIsValid = dr["PeriodIsValid"].ToString();
                }

                if ((dr["PeriodNote"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.PeriodNote = dr["PeriodNote"].ToString();
                }
                if ((dr["PeriodCreateTime"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.PeriodCreateTime = dr["PeriodCreateTime"].ToString();
                }
                if ((dr["PeriodModifyTime"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.PeriodModifyTime = dr["PeriodModifyTime"].ToString();
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
