namespace SIS.DataEntity
{
    using System;
    using System.Xml;
    using System.Data;
    using System.Text;

    [System.Serializable()]
    [System.Runtime.InteropServices.Guid("b826402a-6e99-4f2b-b387-f25a882222ab")]
    public class KPI_RaceValueEntity : EntityBase
    {
        protected String _TagID = null;
        protected String _UnitCode = null;
        protected String _TagCode = null;
        protected String _TagType = null;
        protected String _TagTime = null;
        protected String _TagPeriod = null;
        protected String _TagShift = null;
        protected double _TagMW = double.MinValue;
        protected double _TagSnapshot = double.MinValue;
        protected double _TagTarget = double.MinValue;
        protected double _TagScore = double.MinValue;
        protected double _TagLength = double.MinValue;
        protected int _TagRemoved = int.MinValue;

        ////////////////////////////////////////////////////////////////////

        /// 该字段已经被作为Where的一部分
        public virtual String TagID
        {
            get
            {
                return this._TagID;
            }
            set
            {
                this._TagID = value;
            }
        }
        public virtual String UnitCode
        {
            get
            {
                return this._UnitCode;
            }
            set
            {
                this._UnitCode = value;
            }
        }
        public virtual String TagCode
        {
            get
            {
                return this._TagCode;
            }
            set
            {
                this._TagCode = value;
            }
        }
        public virtual String TagType
        {
            get
            {
                return this._TagType;
            }
            set
            {
                this._TagType = value;
            }
        }
        public virtual String TagTime
        {
            get
            {
                return this._TagTime;
            }
            set
            {
                this._TagTime = value;
            }
        }
        public virtual String TagShift
        {
            get
            {
                return this._TagShift;
            }
            set
            {
                this._TagShift = value;
            }
        }
        public virtual String TagPeriod
        {
            get
            {
                return this._TagPeriod;
            }
            set
            {
                this._TagPeriod = value;
            }
        }
        public virtual double TagMW
        {
            get
            {
                return this._TagMW;
            }
            set
            {
                this._TagMW = value;
            }
        }
        public virtual double TagSnapshot
        {
            get
            {
                return this._TagSnapshot;
            }
            set
            {
                this._TagSnapshot = value;
            }
        }
        public virtual double TagTarget
        {
            get
            {
                return this._TagTarget;
            }
            set
            {
                this._TagTarget = value;
            }
        }
        public virtual double TagScore
        {
            get
            {
                return this._TagScore;
            }
            set
            {
                this._TagScore = value;
            }
        }
        public virtual double TagLength
        {
            get
            {
                return this._TagLength;
            }
            set
            {
                this._TagLength = value;
            }
        }
        public virtual int TagRemoved
        {
            get
            {
                return this._TagRemoved;
            }
            set
            {
                this._TagRemoved = value;
            }
        }

        /// <summary>
        /// /
        /// </summary>
        public override string InsertSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("insert into KPI_RaceValue(");
                if ((this.TagID == null))
                {
                }
                else
                {
                    tmpBuild.Append("TagID");
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
                
                if ((this.TagType == null))
                {
                }
                else
                {
                    tmpBuild.Append("TagType");
                    tmpBuild.Append(",");
                }

                if ((this.TagTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("TagTime");
                    tmpBuild.Append(",");
                }

                if ((this.TagShift == null))
                {
                }
                else
                {
                    tmpBuild.Append("TagShift");
                    tmpBuild.Append(",");
                }                
                
                if ((this.TagPeriod == null))
                {
                }
                else
                {
                    tmpBuild.Append("TagPeriod");
                    tmpBuild.Append(",");
                }

                if ((this.TagMW == double.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("TagMW");
                    tmpBuild.Append(",");
                }

                if ((this.TagSnapshot == double.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("TagSnapshot");
                    tmpBuild.Append(",");
                }

                if ((this.TagTarget == double.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("TagTarget");
                    tmpBuild.Append(",");
                }

                if ((this.TagScore == double.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("TagScore");
                    tmpBuild.Append(",");
                }

                if ((this.TagLength == double.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("TagLength");
                    tmpBuild.Append(",");
                }

                if ((this.TagRemoved == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("TagRemoved");
                    tmpBuild.Append(",");
                }

                ////////////////////////////////////////////////////////////////
                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }

                tmpBuild.Append(") values(");

                if ((this.TagID == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + TagID + "'");
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

                if ((this.TagType == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + TagType + "'");
                    tmpBuild.Append(",");
                }

                if ((this.TagTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + TagTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.TagShift == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + TagShift + "'");
                    tmpBuild.Append(",");
                }

                if ((this.TagPeriod == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + TagPeriod + "'");
                    tmpBuild.Append(",");
                }

                if ((this.TagMW == double.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(TagMW.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.TagSnapshot == double.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(TagSnapshot.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.TagTarget == double.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(TagTarget.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.TagScore == double.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(TagScore.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.TagLength == double.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(TagLength.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.TagRemoved == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(TagRemoved.ToString());
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
                tmpBuild.Append("update KPI_RaceValue set ");

                if (this.UnitCode == null)
                {
                }
                else
                {
                    tmpBuild.Append("UnitCode='" + UnitCode + "'");
                    tmpBuild.Append(",");
                }

                if (this.TagType == null)
                {
                }
                else
                {
                    tmpBuild.Append("TagType='" + TagType + "'");
                    tmpBuild.Append(",");
                }

                if (this.TagTime == null)
                {
                }
                else
                {
                    tmpBuild.Append("TagTime='" + TagTime + "'");
                    tmpBuild.Append(",");
                }

                if (this.TagShift == null)
                {
                }
                else
                {
                    tmpBuild.Append("TagShift='" + TagShift + "'");
                    tmpBuild.Append(",");
                }

                if (this.TagPeriod == null)
                {
                }
                else
                {
                    tmpBuild.Append("TagPeriod='" + TagPeriod + "'");
                    tmpBuild.Append(",");
                }

                if (this.TagMW == double.MinValue)
                {
                }
                else
                {
                    tmpBuild.Append("TagMW=" + TagMW.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.TagSnapshot == double.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("TagSnapshot=" + TagSnapshot.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.TagTarget == double.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("TagTarget=" + TagTarget.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.TagScore == double.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("TagScore=" + TagScore.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.TagLength == double.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("TagLength=" + TagLength.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.TagRemoved == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("TagRemoved=" + TagRemoved.ToString());
                    tmpBuild.Append(",");
                }

                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }

                tmpBuild.Append(" where ");
                tmpBuild.Append("TagID='" + TagID + "'");
                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }

        public override string DeleteSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("delete KPI_RaceValue");
                tmpBuild.Append(" where ");
                tmpBuild.Append("TagID='" + TagID + "'");
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
                tmpBuild.Append("TagID");
                tmpBuild.Append(",");
                tmpBuild.Append("UnitCode");
                tmpBuild.Append(",");
                tmpBuild.Append("TagType");
                tmpBuild.Append(",");
                tmpBuild.Append("TagTime");
                tmpBuild.Append(",");
                tmpBuild.Append("TagShift");
                tmpBuild.Append(",");
                tmpBuild.Append("TagPeriod");
                tmpBuild.Append(",");
                tmpBuild.Append("TagMW");
                tmpBuild.Append(",");
                tmpBuild.Append("TagSnapshot");
                tmpBuild.Append(",");
                tmpBuild.Append("TagTarget");
                tmpBuild.Append(",");
                tmpBuild.Append("TagScore");
                tmpBuild.Append(",");
                tmpBuild.Append("TagLength");
                tmpBuild.Append(",");
                tmpBuild.Append("TagRemoved");

                tmpBuild.Append(" from KPI_RaceValue");
                tmpBuild.Append(" where ");
                tmpBuild.Append("TagID='" + TagID + "'");
                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }

        public override bool DrToMember(System.Data.DataRow dr)
        {
            try
            {
                if ((dr["TagID"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.TagID = dr["TagID"].ToString();
                }
                if ((dr["UnitCode"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.UnitCode = dr["UnitCode"].ToString();
                }
                if ((dr["TagType"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.TagType = dr["TagType"].ToString();
                }
                if ((dr["TagTime"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.TagTime = dr["TagTime"].ToString();
                }
                if ((dr["TagShift"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.TagShift = dr["TagShift"].ToString();
                }
                if ((dr["TagPeriod"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.TagPeriod = dr["TagPeriod"].ToString();
                }
                if ((dr["TagMW"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.TagMW = double.Parse(dr["TagMW"].ToString());
                }
                if ((dr["TagSnapshot"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.TagSnapshot = double.Parse(dr["TagSnapshot"].ToString());
                }
                if ((dr["TagTarget"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.TagTarget = double.Parse(dr["TagTarget"].ToString());
                }
                if ((dr["TagScore"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.TagScore = double.Parse(dr["TagScore"].ToString());
                }
                if ((dr["TagLength"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.TagLength = double.Parse(dr["TagLength"].ToString());
                }
                if ((dr["TagRemoved"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.TagRemoved = int.Parse(dr["TagRemoved"].ToString());
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
