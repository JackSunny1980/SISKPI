
namespace SIS.DataEntity
{
    using System;
    using System.Xml;
    using System.Data;
    using System.Text;

    [System.Serializable()]
    [System.Runtime.InteropServices.Guid("50e9801b-6409-4dc8-a554-9923a85743dc")]

    public class KPI_RaceTagEntity : EntityBase
    {
        protected String _TagID = null;
        protected String _UnitCode = null;
        protected String _TagCode = null;
        protected String _TagDesc = null;
        protected String _TagType = null;
        protected String _TagEngunit = null;
        protected int _TagIsValid = int.MinValue;
        //protected String _TagRunExp
        //protected String _TagShfitExp
        //protected String _TagPeriodExp
        //protected String _TagMWExp
        protected String _TagSnapExp = null;
        protected int _TagSnapExpType = int.MinValue;
        protected int _TagSnapType = int.MinValue;
        protected String _TagFilterExp = null;
        protected String _TagTargetExp = null;
        protected int _TagTargetExpType = int.MinValue;
        protected int _TagTargetType = int.MinValue;
        protected String _TagScoreExp = null;
        protected int _TagScoreExpType = int.MinValue;
        protected int _TagScoreType = int.MinValue;
        protected String _TagNote = null;
        protected String _TagCreateTime = null;
        protected String _TagModifyTime = null;

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


        public virtual String TagSnapExp
        {
            get
            {
                return this._TagSnapExp;
            }
            set
            {
                this._TagSnapExp = value;
            }
        }


        public virtual int TagSnapExpType
        {
            get
            {
                return this._TagSnapExpType;
            }
            set
            {
                this._TagSnapExpType = value;
            }
        }
        public virtual int TagSnapType
        {
            get
            {
                return this._TagSnapType;
            }
            set
            {
                this._TagSnapType = value;
            }
        }
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

        public virtual String TagTargetExp
        {
            get
            {
                return this._TagTargetExp;
            }
            set
            {
                this._TagTargetExp = value;
            }
        }


        public virtual int TagTargetExpType
        {
            get
            {
                return this._TagTargetExpType;
            }
            set
            {
                this._TagTargetExpType = value;
            }
        }
        public virtual int TagTargetType
        {
            get
            {
                return this._TagTargetType;
            }
            set
            {
                this._TagTargetType = value;
            }
        }


        public virtual String TagScoreExp
        {
            get
            {
                return this._TagScoreExp;
            }
            set
            {
                this._TagScoreExp = value;
            }
        }


        public virtual int TagScoreExpType
        {
            get
            {
                return this._TagScoreExpType;
            }
            set
            {
                this._TagScoreExpType = value;
            }
        }
        public virtual int TagScoreType
        {
            get
            {
                return this._TagScoreType;
            }
            set
            {
                this._TagScoreType = value;
            }
        }

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
                tmpBuild.Append("insert into KPI_RaceTag(");

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

                if ((this.TagCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("TagCode");
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

                if ((this.TagSnapExp == null))
                {
                }
                else
                {
                    tmpBuild.Append("TagSnapExp");
                    tmpBuild.Append(",");
                }

                if ((this.TagSnapExpType == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("TagSnapExpType");
                    tmpBuild.Append(",");
                }
                if ((this.TagSnapType == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("TagSnapType");
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

                if ((this.TagTargetExp == null))
                {
                }
                else
                {
                    tmpBuild.Append("TagTargetExp");
                    tmpBuild.Append(",");
                }

                if ((this.TagTargetExpType == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("TagTargetExpType");
                    tmpBuild.Append(",");
                }
                if ((this.TagTargetType == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("TagTargetType");
                    tmpBuild.Append(",");
                }

                if ((this.TagScoreExp == null))
                {
                }
                else
                {
                    tmpBuild.Append("TagScoreExp");
                    tmpBuild.Append(",");
                }

                if ((this.TagScoreExpType == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("TagScoreExpType");
                    tmpBuild.Append(",");
                }
                if ((this.TagScoreType == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("TagScoreType");
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

                if ((this.UnitCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + UnitCode + "'");
                    tmpBuild.Append(",");
                }

                if ((this.TagCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + TagCode + "'");
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

                //Exp需要注意
                //'--->''
                if ((this.TagSnapExp == null))
                {
                }
                else
                {
                    string temp = TagSnapExp.Replace("'", "''");
                    tmpBuild.Append("'" + temp + "'");
                    tmpBuild.Append(",");
                }

                if ((this.TagSnapExpType == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(TagSnapExpType.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.TagSnapType == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(TagSnapType.ToString());
                    tmpBuild.Append(",");
                }                

                if ((this.TagFilterExp == null))
                {
                }
                else
                {
                    string temp = TagFilterExp.Replace("'", "''");
                    tmpBuild.Append("'" + temp + "'");
                    tmpBuild.Append(",");
                }

                if ((this.TagTargetExp == null))
                {
                }
                else
                {
                    string temp = TagTargetExp.Replace("'", "''");
                    tmpBuild.Append("'" + temp + "'");
                    tmpBuild.Append(",");
                }

                if ((this.TagTargetExpType == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(TagTargetExpType.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.TagTargetType == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(TagTargetType.ToString());
                    tmpBuild.Append(",");
                }


                if ((this.TagScoreExp == null))
                {
                }
                else
                {
                    string temp = TagScoreExp.Replace("'", "''");
                    tmpBuild.Append("'" + temp + "'");
                    tmpBuild.Append(",");
                }

                if ((this.TagScoreExpType == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(TagScoreExpType.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.TagScoreType == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(TagScoreType.ToString());
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
                tmpBuild.Append("update KPI_RaceTag set ");
                if ((this.TagID == null))
                {
                }
                else
                {
                    tmpBuild.Append("TagID='" + TagID + "'");
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

                if ((this.TagCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("TagCode='" + TagCode + "'");
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

                if ((this.TagSnapExp == null))
                {
                }
                else
                {
                    string temp = TagSnapExp.Replace("'", "''");
                    tmpBuild.Append("TagSnapExp='" + temp + "'");
                    tmpBuild.Append(",");
                }

                if ((this.TagSnapExpType == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("TagSnapExpType=" + TagSnapExpType.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.TagSnapType == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("TagSnapType=" + TagSnapType.ToString());
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

                if ((this.TagTargetExp == null))
                {
                }
                else
                {
                    string temp = TagTargetExp.Replace("'", "''");
                    tmpBuild.Append("TagTargetExp='" + temp + "'");
                    tmpBuild.Append(",");
                }

                if ((this.TagTargetExpType == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("TagTargetExpType=" + TagTargetExpType.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.TagTargetType == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("TagTargetType=" + TagTargetType.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.TagScoreExp == null))
                {
                }
                else
                {
                    string temp = TagScoreExp.Replace("'", "''");
                    tmpBuild.Append("TagScoreExp='" + temp + "'");
                    tmpBuild.Append(",");
                }

                if ((this.TagScoreExpType == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("TagScoreExpType=" + TagScoreExpType.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.TagScoreType == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("TagScoreType=" + TagScoreType.ToString());
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
                tmpBuild.Append("delete KPI_RaceTag");
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
                tmpBuild.Append("TagCode");
                tmpBuild.Append(",");
                tmpBuild.Append("TagDesc");
                tmpBuild.Append(",");
                tmpBuild.Append("TagType");
                tmpBuild.Append(",");
                tmpBuild.Append("TagEngunit");
                tmpBuild.Append(",");
                tmpBuild.Append("TagIsValid");
                tmpBuild.Append(",");
                tmpBuild.Append("TagSnapExp");
                tmpBuild.Append(",");
                tmpBuild.Append("TagSnapExpType");
                tmpBuild.Append(",");
                tmpBuild.Append("TagSnapType");
                tmpBuild.Append(",");
                tmpBuild.Append("TagFilterExp");
                tmpBuild.Append(",");
                tmpBuild.Append("TagTargetExp");
                tmpBuild.Append(",");
                tmpBuild.Append("TagTargetExpType");
                tmpBuild.Append(",");
                tmpBuild.Append("TagTargetType");
                tmpBuild.Append(",");
                tmpBuild.Append("TagScoreExp");
                tmpBuild.Append(",");
                tmpBuild.Append("TagScoreExpType");
                tmpBuild.Append(",");
                tmpBuild.Append("TagScoreType");
                tmpBuild.Append(",");
                tmpBuild.Append("TagNote");
                tmpBuild.Append(",");
                tmpBuild.Append("TagCreateTime");
                tmpBuild.Append(",");
                tmpBuild.Append("TagModifyTime");

                tmpBuild.Append(" from Race_LinqTag");
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

                if (dr["UnitCode"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.UnitCode = dr["UnitCode"].ToString();
                }

                if (dr["TagCode"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.TagCode = dr["TagCode"].ToString();
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

                if (dr["TagSnapExp"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.TagSnapExp = dr["TagSnapExp"].ToString();
                }

                if (dr["TagSnapExpType"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.TagSnapExpType = int.Parse(dr["TagSnapExpType"].ToString());
                }

                if (dr["TagSnapType"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.TagSnapType = int.Parse(dr["TagSnapType"].ToString());
                }

                if (dr["TagFilterExp"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.TagFilterExp = dr["TagFilterExp"].ToString();
                }
                
                if (dr["TagTargetExp"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.TagTargetExp = dr["TagTargetExp"].ToString();
                }

                if (dr["TagTargetExpType"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.TagTargetExpType = int.Parse(dr["TagTargetExpType"].ToString());
                }

                if (dr["TagTargetType"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.TagTargetType = int.Parse(dr["TagTargetType"].ToString());
                }

                if (dr["TagScoreExp"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.TagScoreExp = dr["TagScoreExp"].ToString();
                }

                if (dr["TagScoreExpType"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.TagScoreExpType = int.Parse(dr["TagScoreExpType"].ToString());
                }

                if (dr["TagScoreType"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.TagScoreType = int.Parse(dr["TagScoreType"].ToString());
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
