namespace SIS.DataEntity
{
    using System;
    using System.Xml;
    using System.Data;
    using System.Text;

    [System.Serializable()]
    [System.Runtime.InteropServices.Guid("b826402a-6e99-4f2b-b387-f25a882222ab")]
    public class Race_ArchiveEntity : EntityBase
    {
        protected String _TagID = null;
        protected String _UnitID = null;
        protected String _TagType = null;
        protected String _TagPeriod = null;
        protected String _TagShift = null;
        protected String _TagStartTime = null;
        protected String _TagEndTime = null;
        protected double _TagValue = double.MinValue;

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
        public virtual String UnitID
        {
            get
            {
                return this._UnitID;
            }
            set
            {
                this._UnitID = value;
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
        public virtual String TagStartTime
        {
            get
            {
                return this._TagStartTime;
            }
            set
            {
                this._TagStartTime = value;
            }
        }
        public virtual String TagEndTime
        {
            get
            {
                return this._TagEndTime;
            }
            set
            {
                this._TagEndTime = value;
            }
        }
        public virtual double TagValue
        {
            get
            {
                return this._TagValue;
            }
            set
            {
                this._TagValue = value;
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
                tmpBuild.Append("insert into Race_Archive(");
                if ((this.TagID == null))
                {
                }
                else
                {
                    tmpBuild.Append("TagID");
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
                
                if ((this.TagType == null))
                {
                }
                else
                {
                    tmpBuild.Append("TagType");
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
                
                if ((this.TagShift == null))
                {
                }
                else
                {
                    tmpBuild.Append("TagShift");
                    tmpBuild.Append(",");
                }
                
                if ((this.TagStartTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("TagStartTime");
                    tmpBuild.Append(",");
                }
                
                if ((this.TagEndTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("TagEndTime");
                    tmpBuild.Append(",");
                }

                if ((this.TagValue == double.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("TagValue");
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

                if ((this.UnitID == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + UnitID + "'");
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

                if ((this.TagPeriod == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + TagPeriod + "'");
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

                if ((this.TagStartTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + TagStartTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.TagEndTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + TagEndTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.TagValue == double.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("'" + TagValue.ToString() + "'");
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
                tmpBuild.Append("update Race_Archive set ");

                if (this.UnitID == null)
                {
                }
                else
                {
                    tmpBuild.Append("UnitID='" + UnitID + "'");
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

                if (this.TagPeriod == null)
                {
                }
                else
                {
                    tmpBuild.Append("TagPeriod='" + TagPeriod + "'");
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

                if (this.TagStartTime == null)
                {
                }
                else
                {
                    tmpBuild.Append("TagStartTime='" + TagStartTime + "'");
                    tmpBuild.Append(",");
                }

                if (this.TagEndTime == null)
                {
                }
                else
                {
                    tmpBuild.Append("TagEndTime='" + TagEndTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.TagValue == double.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("TagValue='" + TagValue.ToString() + "'");
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
                tmpBuild.Append("delete Race_Archive");
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
                tmpBuild.Append("UnitID");
                tmpBuild.Append(",");
                tmpBuild.Append("TagType");
                tmpBuild.Append(",");
                tmpBuild.Append("TagPeriod");
                tmpBuild.Append(",");
                tmpBuild.Append("TagShift");
                tmpBuild.Append(",");
                tmpBuild.Append("TagStartTime");
                tmpBuild.Append(",");
                tmpBuild.Append("TagEndTime");
                tmpBuild.Append(",");
                tmpBuild.Append("TagValue");

                tmpBuild.Append(" from Race_Archive");
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
                if ((dr["UnitID"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.UnitID = dr["UnitID"].ToString();
                }
                if ((dr["TagType"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.TagType = dr["TagType"].ToString();
                }
                if ((dr["TagPeriod"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.TagPeriod = dr["TagPeriod"].ToString();
                }
                if ((dr["TagShift"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.TagShift = dr["TagShift"].ToString();
                }
                if ((dr["TagStartTime"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.TagStartTime = dr["TagStartTime"].ToString();
                }
                if ((dr["TagEndTime"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.TagEndTime = dr["TagEndTime"].ToString();
                }
                if ((dr["TagValue"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.TagValue = double.Parse(dr["TagValue"].ToString());
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
