namespace SIS.DataEntity
{
    using System;
    using System.Xml;
    using System.Data;
    using System.Text;
    
    //
    //KPI_ECSSValueEntity, Replace KPI_ECSSArchiveEntity \ KPI_ECSSSnapshotEntity
    //
    //不能有删除、更新、插入操作。

    [System.Serializable()]
    [System.Runtime.InteropServices.Guid("b826402a-6e99-4f2b-b387-f25c882222ab")]
    public class ECSSValueEntity : EntityBase
    {
        protected String _SSID = null;
        protected String _UnitID = null;
        protected String _SeqID = null;
        protected String _KpiID = null;
        protected String _ECID = null;
        protected String _ECName = null;
        protected String _ECTime = null;
        protected double _ECValue = double.MinValue;
        protected double _ECOpt = double.MinValue;
        protected String _ECOptExp = null;
        protected String _ECExpression = null;
        protected double _ECScore = double.MinValue;
        protected int _ECQulity = int.MinValue;
        protected String _ECPeriod = null;
        protected String _ECShift = null;
        protected int _ECIsRemove = int.MinValue;

        ////////////////////////////////////////////////////////////////////

        /// 该字段已经被作为Where的一部分
        public virtual String SSID
        {
            get
            {
                return this._SSID;
            }
            set
            {
                this._SSID = value;
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
        public virtual String SeqID
        {
            get
            {
                return this._SeqID;
            }
            set
            {
                this._SeqID = value;
            }
        }
        public virtual String KpiID
        {
            get
            {
                return this._KpiID;
            }
            set
            {
                this._KpiID = value;
            }
        }
        public virtual String ECID
        {
            get
            {
                return this._ECID;
            }
            set
            {
                this._ECID = value;
            }
        }
        public virtual String ECName
        {
            get
            {
                return this._ECName;
            }
            set
            {
                this._ECName = value;
            }
        }
        public virtual String ECTime
        {
            get
            {
                return this._ECTime;
            }
            set
            {
                this._ECTime = value;
            }
        }
        public virtual double ECValue
        {
            get
            {
                return this._ECValue;
            }
            set
            {
                this._ECValue = value;
            }
        }
        public virtual double ECOpt
        {
            get
            {
                return this._ECOpt;
            }
            set
            {
                this._ECOpt = value;
            }
        }
        public virtual String ECOptExp
        {
            get
            {
                return this._ECOptExp;
            }
            set
            {
                this._ECOptExp = value;
            }
        }
        public virtual String ECExpression
        {
            get
            {
                return this._ECExpression;
            }
            set
            {
                this._ECExpression = value;
            }
        }
        public virtual double ECScore
        {
            get
            {
                return this._ECScore;
            }
            set
            {
                this._ECScore = value;
            }
        }
        public virtual int ECQulity
        {
            get
            {
                return this._ECQulity;
            }
            set
            {
                this._ECQulity = value;
            }
        }


        public virtual String ECPeriod
        {
            get
            {
                return this._ECPeriod;
            }
            set
            {
                this._ECPeriod = value;
            }
        }
        public virtual String ECShift
        {
            get
            {
                return this._ECShift;
            }
            set
            {
                this._ECShift = value;
            }
        }
        public virtual int ECIsRemove
        {
            get
            {
                return this._ECIsRemove;
            }
            set
            {
                this._ECIsRemove = value;
            }
        }      

        public override bool DrToMember(System.Data.DataRow dr)
        {
            try
            {
                if ((dr["SSID"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.SSID = dr["SSID"].ToString();
                }
                if ((dr["UnitID"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.UnitID = dr["UnitID"].ToString();
                }
                if ((dr["SeqID"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.SeqID = dr["SeqID"].ToString();
                }
                if ((dr["KpiID"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.KpiID = dr["KpiID"].ToString();
                }
                if ((dr["ECID"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.ECID = dr["ECID"].ToString();
                }
                if ((dr["ECName"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.ECName = dr["ECName"].ToString();
                }
                if ((dr["ECTime"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.ECTime = dr["ECTime"].ToString();
                }
                if ((dr["ECValue"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.ECValue = double.Parse(dr["ECValue"].ToString());
                }
                if ((dr["ECOpt"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.ECOpt = double.Parse(dr["ECOpt"].ToString());
                }
                if ((dr["ECOptExp"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.ECOptExp = dr["ECOptExp"].ToString();
                }
                if ((dr["ECExpression"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.ECExpression = dr["ECExpression"].ToString();
                }
                if ((dr["ECScore"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.ECScore = double.Parse(dr["ECScore"].ToString());
                }
                if ((dr["ECQulity"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.ECQulity = int.Parse(dr["ECQulity"].ToString());
                }
                if ((dr["ECPeriod"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.ECPeriod = dr["ECPeriod"].ToString();
                }
                if ((dr["ECShift"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.ECShift = dr["ECShift"].ToString();
                }
                if ((dr["ECIsRemove"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.ECIsRemove = int.Parse(dr["ECIsRemove"].ToString());
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
        //        tmpBuild.Append("insert into KPI_ECSSArchive(");
        //        if ((this.SSID == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("SSID");
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
        //        if ((this.SeqID == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("SeqID");
        //            tmpBuild.Append(",");
        //        }
        //        if ((this.KpiID == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("KpiID");
        //            tmpBuild.Append(",");
        //        }
        //        if ((this.ECID == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("ECID");
        //            tmpBuild.Append(",");
        //        }

        //        if ((this.ECName == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("ECName");
        //            tmpBuild.Append(",");
        //        }
        //        if ((this.ECTime == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("ECTime");
        //            tmpBuild.Append(",");
        //        }
        //        if ((this.ECValue == double.MinValue))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("ECValue");
        //            tmpBuild.Append(",");
        //        }
        //        if ((this.ECScore == double.MinValue))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("ECScore");
        //            tmpBuild.Append(",");
        //        }
        //        if ((this.ECOpt == double.MinValue))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("ECOpt");
        //            tmpBuild.Append(",");
        //        }
        //        if ((this.ECOptExp == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("ECOptExp");
        //            tmpBuild.Append(",");
        //        }
        //        if ((this.ECExpression == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("ECExpression");
        //            tmpBuild.Append(",");
        //        }

        //        if ((this.ECQulity == int.MinValue))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("ECQulity");
        //            tmpBuild.Append(",");
        //        }

        //        if ((this.ECPeriod == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("ECPeriod");
        //            tmpBuild.Append(",");
        //        }

        //        if ((this.ECShift == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("ECShift");
        //            tmpBuild.Append(",");
        //        }

        //        if ((this.ECIsRemove == int.MinValue))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("ECIsRemove");
        //            tmpBuild.Append(",");
        //        }


        //        ////////////////////////////////////////////////////////////////
        //        if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
        //        {
        //            tmpBuild.Remove((tmpBuild.Length - 1), 1);
        //        }

        //        tmpBuild.Append(") values(");

        //        if ((this.SSID == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("'" + SSID + "'");
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
        //        if ((this.SeqID == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("'" + SeqID + "'");
        //            tmpBuild.Append(",");
        //        }
        //        if ((this.KpiID == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("'" + KpiID + "'");
        //            tmpBuild.Append(",");
        //        }

        //        if ((this.ECID == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("'" + ECID + "'");
        //            tmpBuild.Append(",");
        //        }

        //        if ((this.ECName == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("'" + ECName + "'");
        //            tmpBuild.Append(",");
        //        }

        //        if ((this.ECTime == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("'" + ECTime + "'");
        //            tmpBuild.Append(",");
        //        }
        //        if ((this.ECValue == double.MinValue))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append(ECValue.ToString());
        //            tmpBuild.Append(",");
        //        }
        //        if ((this.ECScore == double.MinValue))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append(ECScore.ToString());
        //            tmpBuild.Append(",");
        //        }
        //        if ((this.ECOpt == double.MinValue))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append(ECOpt.ToString());
        //            tmpBuild.Append(",");
        //        }
        //        if ((this.ECOptExp == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("'" + ECOptExp + "'");
        //            tmpBuild.Append(",");
        //        }
        //        if ((this.ECExpression == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("'" + ECExpression + "'");
        //            tmpBuild.Append(",");
        //        }

        //        if ((this.ECQulity == int.MinValue))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append(ECQulity.ToString());
        //            tmpBuild.Append(",");
        //        }

        //        if ((this.ECPeriod == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("'" + ECPeriod + "'");
        //            tmpBuild.Append(",");
        //        }

        //        if ((this.ECShift == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("'" + ECShift + "'");
        //            tmpBuild.Append(",");
        //        }
        //        if ((this.ECIsRemove == int.MinValue))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append(ECIsRemove.ToString());
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
        //        tmpBuild.Append("update KPI_ECSSArchive set ");

        //        if (this.UnitID == null)
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("UnitID='" + UnitID + "'");
        //            tmpBuild.Append(",");
        //        }

        //        if (this.SeqID == null)
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("SeqID='" + SeqID + "'");
        //            tmpBuild.Append(",");
        //        }

        //        if (this.KpiID == null)
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("KpiID='" + KpiID + "'");
        //            tmpBuild.Append(",");
        //        }

        //        if (this.ECID == null)
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("ECID='" + ECID + "'");
        //            tmpBuild.Append(",");
        //        }
        //        if (this.ECName == null)
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("ECName='" + ECName + "'");
        //            tmpBuild.Append(",");
        //        }

        //        if (this.ECTime == null)
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("ECTime='" + ECTime + "'");
        //            tmpBuild.Append(",");
        //        }

        //        if ((this.ECValue == double.MinValue))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("ECValue=" + ECValue.ToString());
        //            tmpBuild.Append(",");
        //        }
        //        if ((this.ECOpt == double.MinValue))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("ECOpt=" + ECOpt.ToString());
        //            tmpBuild.Append(",");
        //        }
        //        if ((this.ECOptExp == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("ECOptExp='" + ECOptExp + "'");
        //            tmpBuild.Append(",");
        //        }
        //        if ((this.ECExpression == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("ECExpression='" + ECExpression + "'");
        //            tmpBuild.Append(",");
        //        }

        //        if ((this.ECScore == double.MinValue))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("ECScore=" + ECScore.ToString());
        //            tmpBuild.Append(",");
        //        }

        //        if (this.ECQulity == int.MinValue)
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("ECQulity=" + ECQulity.ToString());
        //            tmpBuild.Append(",");
        //        }

        //        if (this.ECPeriod == null)
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("ECPeriod='" + ECPeriod + "'");
        //            tmpBuild.Append(",");
        //        }

        //        if (this.ECShift == null)
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("ECShift='" + ECShift + "'");
        //            tmpBuild.Append(",");
        //        }

        //        if (this.ECIsRemove == int.MinValue)
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("ECIsRemove=" + ECIsRemove.ToString());
        //            tmpBuild.Append(",");
        //        }

        //        if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
        //        {
        //            tmpBuild.Remove((tmpBuild.Length - 1), 1);
        //        }

        //        tmpBuild.Append(" where ");
        //        tmpBuild.Append("SSID='" + SSID + "'");
        //        string __tmpSql = tmpBuild.ToString();
        //        return __tmpSql;
        //    }
        //}

        //public override string DeleteSql
        //{
        //    get
        //    {
        //        System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
        //        tmpBuild.Append("delete KPI_ECSSArchive");
        //        tmpBuild.Append(" where ");
        //        tmpBuild.Append("SSID='" + SSID + "'");
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
        //        tmpBuild.Append("SSID");
        //        tmpBuild.Append(",");
        //        tmpBuild.Append("UnitID");
        //        tmpBuild.Append(",");
        //        tmpBuild.Append("SeqID");
        //        tmpBuild.Append(",");
        //        tmpBuild.Append("KpiID");
        //        tmpBuild.Append(",");
        //        tmpBuild.Append("ECID");
        //        tmpBuild.Append(",");
        //        tmpBuild.Append("ECName");
        //        tmpBuild.Append(",");
        //        tmpBuild.Append("ECTime");
        //        tmpBuild.Append(",");
        //        tmpBuild.Append("ECValue");
        //        tmpBuild.Append(",");
        //        tmpBuild.Append("ECOpt");
        //        tmpBuild.Append(",");
        //        tmpBuild.Append("ECOptExp");
        //        tmpBuild.Append(",");
        //        tmpBuild.Append("ECExpression");
        //        tmpBuild.Append(",");
        //        tmpBuild.Append("ECScore");
        //        tmpBuild.Append(",");
        //        tmpBuild.Append("ECQulity");
        //        tmpBuild.Append(",");
        //        tmpBuild.Append("ECPeriod");
        //        tmpBuild.Append(",");
        //        tmpBuild.Append("ECShift");
        //        tmpBuild.Append(",");
        //        tmpBuild.Append("ECIsRemove");

        //        tmpBuild.Append(" from KPI_ECSSArchive");
        //        tmpBuild.Append(" where ");
        //        tmpBuild.Append("SSID='" + SSID + "'");
        //        string __tmpSql = tmpBuild.ToString();
        //        return __tmpSql;
        //    }
        //}


        #endregion
    }
}
