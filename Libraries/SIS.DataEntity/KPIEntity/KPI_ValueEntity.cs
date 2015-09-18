namespace SIS.DataEntity
{
    using System;
    using System.Xml;
    using System.Data;
    using System.Text;

    [System.Serializable()]
    [System.Runtime.InteropServices.Guid("b826402a-6e99-4f2b-b387-f25a882222ab")]
    public class ValueEntity : EntityBase
    {
        protected String _RVID = null;
        protected String _UnitID = null;
        protected String _RealID = null;
        protected String _RealCode = null;
        protected String _RealDesc = null;
        protected String _RealEngunit = null;
        protected String _RealTime = null;
        protected double _RealValue = double.MinValue;
        protected int _RealQulity = int.MinValue;

        ////////////////////////////////////////////////////////////////////

        /// 该字段已经被作为Where的一部分
        public virtual String RVID
        {
            get
            {
                return this._RVID;
            }
            set
            {
                this._RVID = value;
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
        public virtual String RealID
        {
            get
            {
                return this._RealID;
            }
            set
            {
                this._RealID = value;
            }
        }
        public virtual String RealCode
        {
            get
            {
                return this._RealCode;
            }
            set
            {
                this._RealCode = value;
            }
        }
        public virtual String RealDesc
        {
            get
            {
                return this._RealDesc;
            }
            set
            {
                this._RealDesc = value;
            }
        }
        public virtual String RealEngunit
        {
            get
            {
                return this._RealEngunit;
            }
            set
            {
                this._RealEngunit = value;
            }
        }
        public virtual String RealTime
        {
            get
            {
                return this._RealTime;
            }
            set
            {
                this._RealTime = value;
            }
        }
        public virtual double RealValue
        {
            get
            {
                return this._RealValue;
            }
            set
            {
                this._RealValue = value;
            }
        }

        public virtual int RealQulity
        {
            get
            {
                return this._RealQulity;
            }
            set
            {
                this._RealQulity = value;
            }
        }

        public override bool DrToMember(System.Data.DataRow dr)
        {
            try
            {
                if ((dr["RVID"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.RVID = dr["RVID"].ToString();
                }
                if ((dr["UnitID"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.UnitID = dr["UnitID"].ToString();
                }
                if ((dr["RealID"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.RealID = dr["RealID"].ToString();
                }
                if ((dr["RealCode"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.RealCode = dr["RealCode"].ToString();
                }
                if ((dr["RealDesc"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.RealDesc = dr["RealDesc"].ToString();
                }
                if ((dr["RealEngunit"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.RealEngunit = dr["RealEngunit"].ToString();
                }
                if ((dr["RealTime"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.RealTime = dr["RealTime"].ToString();
                }
                if ((dr["RealValue"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.RealValue = double.Parse(dr["RealValue"].ToString());
                }

                if ((dr["RealQulity"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.RealQulity = int.Parse(dr["RealQulity"].ToString());
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

        #region No Use Code


        ///// <summary>
        ///// /
        ///// </summary>
        //public override string InsertSql
        //{
        //    get
        //    {
        //        System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
        //        tmpBuild.Append("insert into KPI_RealValue(");
        //        if ((this.RVID == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("RVID");
        //            tmpBuild.Append(",");
        //        }

        //        if ((this.UnitID == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("UnitID");
        //            tmpBuild.Append(",");
        //        }

        //        if ((this.RealID == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("RealID");
        //            tmpBuild.Append(",");
        //        }

        //        if ((this.RealCode == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("RealCode");
        //            tmpBuild.Append(",");
        //        }
        //        if ((this.RealDesc == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("RealDesc");
        //            tmpBuild.Append(",");
        //        }
        //        if ((this.RealEngunit == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("RealEngunit");
        //            tmpBuild.Append(",");
        //        }
        //        if ((this.RealTime == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("RealTime");
        //            tmpBuild.Append(",");
        //        }
        //        if ((this.RealValue == double.MinValue))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("RealValue");
        //            tmpBuild.Append(",");
        //        }

        //        if ((this.RealQulity == int.MinValue))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("RealQulity");
        //            tmpBuild.Append(",");
        //        }


        //        ////////////////////////////////////////////////////////////////
        //        if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
        //        {
        //            tmpBuild.Remove((tmpBuild.Length - 1), 1);
        //        }

        //        tmpBuild.Append(") values(");

        //        if ((this.RVID == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("'" + RVID + "'");
        //            tmpBuild.Append(",");
        //        }

        //        if ((this.UnitID == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("'" + UnitID + "'");
        //            tmpBuild.Append(",");
        //        }
        //        if ((this.RealID == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("'" + RealID + "'");
        //            tmpBuild.Append(",");
        //        }

        //        if ((this.RealCode == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("'" + RealCode + "'");
        //            tmpBuild.Append(",");
        //        }
        //        if ((this.RealDesc == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("'" + RealDesc + "'");
        //            tmpBuild.Append(",");
        //        }

        //        if ((this.RealEngunit == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("'" + RealEngunit + "'");
        //            tmpBuild.Append(",");
        //        }


        //        if ((this.RealTime == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("'" + RealTime + "'");
        //            tmpBuild.Append(",");
        //        }
        //        if ((this.RealValue == double.MinValue))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append(RealValue.ToString());
        //            tmpBuild.Append(",");
        //        }

        //        if ((this.RealQulity == int.MinValue))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append(RealQulity.ToString());
        //            tmpBuild.Append(",");
        //        }

        //        if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
        //        {
        //            tmpBuild.Remove((tmpBuild.Length - 1), 1);
        //        }

        //        tmpBuild.Append(")");

        //        string __tmpSql = tmpBuild.ToString();
        //        return __tmpSql;
        //    }
        //}

        //public override string UpdateSql
        //{
        //    get
        //    {
        //        System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
        //        tmpBuild.Append("update KPI_RealValue set ");

        //        if (this.UnitID == null)
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("UnitID='" + UnitID + "'");
        //            tmpBuild.Append(",");
        //        }
        //        if (this.RealID == null)
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("RealID='" + RealID + "'");
        //            tmpBuild.Append(",");
        //        }
        //        if (this.RealCode == null)
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("RealCode='" + RealCode + "'");
        //            tmpBuild.Append(",");
        //        }
        //        if (this.RealDesc == null)
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("RealDesc='" + RealDesc + "'");
        //            tmpBuild.Append(",");
        //        }
        //        if (this.RealEngunit == null)
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("RealEngunit='" + RealEngunit + "'");
        //            tmpBuild.Append(",");
        //        }

        //        if (this.RealTime == null)
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("RealTime='" + RealTime + "'");
        //            tmpBuild.Append(",");
        //        }

        //        if ((this.RealValue == double.MinValue))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("RealValue=" + RealValue.ToString());
        //            tmpBuild.Append(",");
        //        }


        //        if (this.RealQulity == int.MinValue)
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("RealQulity=" + RealQulity.ToString());
        //            tmpBuild.Append(",");
        //        }


        //        if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
        //        {
        //            tmpBuild.Remove((tmpBuild.Length - 1), 1);
        //        }

        //        tmpBuild.Append(" where ");
        //        tmpBuild.Append("RVID='" + RVID + "'");
        //        string __tmpSql = tmpBuild.ToString();
        //        return __tmpSql;
        //    }
        //}

        //public override string DeleteSql
        //{
        //    get
        //    {
        //        System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
        //        tmpBuild.Append("delete KPI_RealValue");
        //        tmpBuild.Append(" where ");
        //        tmpBuild.Append("RVID='" + RVID + "'");
        //        string __tmpSql = tmpBuild.ToString();
        //        return __tmpSql;
        //    }
        //}

        //public override string SelectSql
        //{
        //    get
        //    {
        //        System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
        //        tmpBuild.Append("select ");
        //        tmpBuild.Append("RVID");
        //        tmpBuild.Append(",");
        //        tmpBuild.Append("UnitID");
        //        tmpBuild.Append(",");
        //        tmpBuild.Append("RealID");
        //        tmpBuild.Append(",");
        //        tmpBuild.Append("RealCode");
        //        tmpBuild.Append(",");
        //        tmpBuild.Append("RealDesc");
        //        tmpBuild.Append(",");
        //        tmpBuild.Append("RealEngunit");
        //        tmpBuild.Append(",");
        //        tmpBuild.Append("RealTime");
        //        tmpBuild.Append(",");
        //        tmpBuild.Append("RealValue");
        //        tmpBuild.Append(",");
        //        tmpBuild.Append("RealQulity");

        //        tmpBuild.Append(" from KPI_RealValue");
        //        tmpBuild.Append(" where ");
        //        tmpBuild.Append("RVID='" + RVID + "'");
        //        string __tmpSql = tmpBuild.ToString();
        //        return __tmpSql;
        //    }
        //}

        #endregion
    }
}
