
namespace SIS.DataEntity
{
    using System;
    using System.Xml;
    using System.Data;
    using System.Text;
	using System.ComponentModel;

    [System.Serializable()]
    [System.Runtime.InteropServices.Guid("50e9801b-6409-4dc8-a554-9923a85743dc")]

    public class Race_TagEntity : EntityBase
    {
        protected String _TagID = null;
        protected String _UnitID = null;
        protected String _TagName = null;
        protected String _TagDesc = null;
        protected String _TagType = null;
        protected String _TagEngunit = null;
        protected int _TagIsValid = int.MinValue;
        protected int _TagIndex = int.MinValue;
        protected String _TagFilterExp = null;
        protected String _TagCalcExp = null;
        protected int _TagCalcExpType = int.MinValue;
        protected int _TagCalcType = int.MinValue;
		protected decimal _TagFactor = decimal.MinValue;
		protected decimal _TagOffset = decimal.MinValue;
        //protected String _TagUnitName = null;
        //protected String _TagTableName = null;
        protected String _TagNote = null;
        protected String _TagCreateTime = null;
        protected String _TagModifyTime = null;

		[Description("TagID")]
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

		[Description("UnitID")]
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

		[Description("TagName")]
        public virtual String TagName
        {
            get
            {
                return this._TagName;
            }
            set
            {
                this._TagName = value;
            }
        }

		[Description("TagDesc")]
        public virtual String TagDesc
        {
            get
            {
                return this._TagDesc;
            }
            set
            {
                this._TagDesc = value;
            }
        }

		[Description("TagType")]
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

		[Description("TagEngunit")]
        public virtual String TagEngunit
        {
            get
            {
                return this._TagEngunit;
            }
            set
            {
                this._TagEngunit = value;
            }
        }

		[Description("TagIsValid")]
        public virtual int TagIsValid
        {
            get
            {
                return this._TagIsValid;
            }
            set
            {
                this._TagIsValid = value;
            }
        }

		[Description("TagIndex")]
        public virtual int TagIndex
        {
            get
            {
                return this._TagIndex;
            }
            set
            {
                this._TagIndex = value;
            }
        }

		[Description("TagFilterExp")]
        public virtual String TagFilterExp
        {
            get
            {
                return this._TagFilterExp;
            }
            set
            {
                this._TagFilterExp = value;
            }
        }

		[Description("TagCalcExp")]
        public virtual String TagCalcExp
        {
            get
            {
                return this._TagCalcExp;
            }
            set
            {
                this._TagCalcExp = value;
            }
        }

		[Description("TagCalcExpType")]
        public virtual int TagCalcExpType
        {
            get
            {
                return this._TagCalcExpType;
            }
            set
            {
                this._TagCalcExpType = value;
            }
        }

		[Description("TagCalcType")]
        public virtual int TagCalcType
        {
            get
            {
                return this._TagCalcType;
            }
            set
            {
                this._TagCalcType = value;
            }
        }

		[Description("TagFactor")]
		public virtual decimal TagFactor
        {
            get
            {
                return this._TagFactor;
            }
            set
            {
                this._TagFactor = value;
            }
        }

		[Description("TagOffset")]
		public virtual decimal TagOffset
        {
            get
            {
                return this._TagOffset;
            }
            set
            {
                this._TagOffset = value;
            }
        }

		[Description("TagNote")]
        public virtual String TagNote
        {
            get
            {
                return this._TagNote;
            }
            set
            {
                this._TagNote = value;
            }
        }

		[Description("TagCreateTime")]
        public virtual String TagCreateTime
        {
            get
            {
                return this._TagCreateTime;
            }
            set
            {
                this._TagCreateTime = value;
            }
        }

		[Description("TagModifyTime")]
        public virtual String TagModifyTime
        {
            get
            {
                return this._TagModifyTime;
            }
            set
            {
                this._TagModifyTime = value;
            }
        }

        public override string InsertSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("insert into Race_Tag(");

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

                if ((this.TagName == null))
                {
                }
                else
                {
                    tmpBuild.Append("TagName");
                    tmpBuild.Append(",");
                }

                if ((this.TagDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("TagDesc");
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

                if ((this.TagEngunit == null))
                {
                }
                else
                {
                    tmpBuild.Append("TagEngunit");
                    tmpBuild.Append(",");
                }

                if ((this.TagIsValid == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("TagIsValid");
                    tmpBuild.Append(",");
                }

                if ((this.TagIndex == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("TagIndex");
                    tmpBuild.Append(",");
                }

                if ((this.TagFilterExp == null))
                {
                }
                else
                {
                    tmpBuild.Append("TagFilterExp");
                    tmpBuild.Append(",");
                }

                if ((this.TagCalcExp == null))
                {
                }
                else
                {
                    tmpBuild.Append("TagCalcExp");
                    tmpBuild.Append(",");
                }

                if ((this.TagCalcExpType == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("TagCalcExpType");
                    tmpBuild.Append(",");
                }

                if ((this.TagCalcType == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("TagCalcType");
                    tmpBuild.Append(",");
                }

				if ((this.TagFactor == decimal.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("TagFactor");
                    tmpBuild.Append(",");
                }

				if ((this.TagOffset == decimal.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("TagOffset");
                    tmpBuild.Append(",");
                }

                if ((this.TagNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("TagNote");
                    tmpBuild.Append(",");
                }

                if ((this.TagCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("TagCreateTime");
                    tmpBuild.Append(",");
                }

                if ((this.TagModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("TagModifyTime");
                    tmpBuild.Append(",");
                }

                ///////////////////////////////////////////////////////////////////////////////////////


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

                if ((this.TagName == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + TagName + "'");
                    tmpBuild.Append(",");
                }

                if ((this.TagDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + TagDesc + "'");
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

                if ((this.TagEngunit == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + TagEngunit + "'");
                    tmpBuild.Append(",");
                }

                if ((this.TagIsValid == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(TagIsValid.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.TagIndex == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(TagIndex.ToString());
                    tmpBuild.Append(",");
                }

                //Exp需要注意
                //'--->''

                if ((this.TagFilterExp == null))
                {
                }
                else
                {
                    string temp = TagFilterExp.Replace("'", "''");
                    tmpBuild.Append("'" + temp + "'");
                    tmpBuild.Append(",");
                }

                if ((this.TagCalcExp == null))
                {
                }
                else
                {
                    string temp = TagCalcExp.Replace("'", "''");
                    tmpBuild.Append("'" + temp + "'");
                    tmpBuild.Append(",");
                }

                if ((this.TagCalcExpType == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(TagCalcExpType.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.TagCalcType == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(TagCalcType.ToString());
                    tmpBuild.Append(",");
                }

				if ((this.TagFactor == decimal.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(TagFactor.ToString());
                    tmpBuild.Append(",");
                }

				if ((this.TagOffset == decimal.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(TagOffset.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.TagNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + TagNote + "'");
                    tmpBuild.Append(",");
                }

                if ((this.TagCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + TagCreateTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.TagModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + TagModifyTime + "'");
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
                tmpBuild.Append("update Race_Tag set ");
                if ((this.TagID == null))
                {
                }
                else
                {
                    tmpBuild.Append("TagID='" + TagID + "'");
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

                if ((this.TagName == null))
                {
                }
                else
                {
                    tmpBuild.Append("TagName='" + TagName + "'");
                    tmpBuild.Append(",");
                }

                if ((this.TagDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("TagDesc='" + TagDesc + "'");
                    tmpBuild.Append(",");
                }

                if ((this.TagType == null))
                {
                }
                else
                {
                    tmpBuild.Append("TagType='" + TagType + "'");
                    tmpBuild.Append(",");
                }

                if ((this.TagEngunit == null))
                {
                }
                else
                {
                    tmpBuild.Append("TagEngunit='" + TagEngunit + "'");
                    tmpBuild.Append(",");
                }

                if ((this.TagIsValid == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("TagIsValid=" + TagIsValid.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.TagIndex == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("TagIndex=" + TagIndex.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.TagFilterExp == null))
                {
                }
                else
                {
                    string temp = TagFilterExp.Replace("'", "''");
                    tmpBuild.Append("TagFilterExp='" + temp + "'");
                    tmpBuild.Append(",");
                }

                if ((this.TagCalcExp == null))
                {
                }
                else
                {
                    string temp = TagCalcExp.Replace("'", "''");
                    tmpBuild.Append("TagCalcExp='" + temp + "'");
                    tmpBuild.Append(",");
                }

                if ((this.TagCalcExpType == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("TagCalcExpType=" + TagCalcExpType.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.TagCalcType == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("TagCalcType=" + TagCalcType.ToString());
                    tmpBuild.Append(",");
                }

				if ((this.TagFactor == decimal.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("TagFactor=" + TagFactor.ToString());
                    tmpBuild.Append(",");
                }

				if ((this.TagOffset == decimal.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("TagOffset=" + TagOffset.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.TagNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("TagNote='" + TagNote + "'");
                    tmpBuild.Append(",");
                }

                if ((this.TagCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("TagCreateTime='" + TagCreateTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.TagModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("TagModifyTime='" + TagModifyTime + "'");
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
                tmpBuild.Append("delete Race_Tag");
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
                tmpBuild.Append("TagName");
                tmpBuild.Append(",");
                tmpBuild.Append("TagDesc");
                tmpBuild.Append(",");
                tmpBuild.Append("TagType");
                tmpBuild.Append(",");
                tmpBuild.Append("TagEngunit");
                tmpBuild.Append(",");
                tmpBuild.Append("TagIsValid");
                tmpBuild.Append(",");
                tmpBuild.Append("TagIndex");
                tmpBuild.Append(",");
                tmpBuild.Append("TagFilterExp");
                tmpBuild.Append(",");
                tmpBuild.Append("TagCalcExp");
                tmpBuild.Append(",");
                tmpBuild.Append("TagCalcExpType");
                tmpBuild.Append(",");
                tmpBuild.Append("TagCalcType");
                tmpBuild.Append(",");
                tmpBuild.Append("TagFactor");
                tmpBuild.Append(",");
                tmpBuild.Append("TagOffset");
                tmpBuild.Append(",");
                tmpBuild.Append("TagNote");
                tmpBuild.Append(",");
                tmpBuild.Append("TagCreateTime");
                tmpBuild.Append(",");
                tmpBuild.Append("TagModifyTime");

                tmpBuild.Append(" from Race_Tag");
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
                if (dr["TagID"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.TagID = dr["TagID"].ToString();
                }

                if (dr["UnitID"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.UnitID = dr["UnitID"].ToString();
                }

                if (dr["TagName"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.TagName = dr["TagName"].ToString();
                }

                if (dr["TagDesc"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.TagDesc = dr["TagDesc"].ToString();
                }

                if (dr["TagType"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.TagType = dr["TagType"].ToString();
                }

                if (dr["TagEngunit"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.TagEngunit = dr["TagEngunit"].ToString();
                }

                if (dr["TagIsValid"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.TagIsValid = int.Parse(dr["TagIsValid"].ToString());
                }
                if (dr["TagIndex"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.TagIndex = int.Parse(dr["TagIndex"].ToString());
                }

                if (dr["TagFilterExp"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.TagFilterExp = dr["TagFilterExp"].ToString();
                }

                if (dr["TagCalcExp"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.TagCalcExp = dr["TagCalcExp"].ToString();
                }

                if (dr["TagCalcExpType"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.TagCalcExpType = int.Parse(dr["TagCalcExpType"].ToString());
                }

                if (dr["TagCalcType"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.TagCalcType = int.Parse(dr["TagCalcType"].ToString());
                }

                if (dr["TagFactor"] == System.DBNull.Value)
                {
                }
                else
                {
					this.TagFactor = decimal.Parse(dr["TagFactor"].ToString());
                }

                if (dr["TagOffset"] == System.DBNull.Value)
                {
                }
                else
                {
					this.TagOffset = decimal.Parse(dr["TagOffset"].ToString());
                }

                if (dr["TagNote"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.TagNote = dr["TagNote"].ToString();
                }

                if (dr["TagCreateTime"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.TagCreateTime = dr["TagCreateTime"].ToString();
                }


                if (dr["TagModifyTime"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.TagModifyTime = dr["TagModifyTime"].ToString();
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
