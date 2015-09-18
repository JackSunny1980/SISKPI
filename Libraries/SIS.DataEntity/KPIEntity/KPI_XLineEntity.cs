
namespace SIS.DataEntity
{
    using System;
    using System.Xml;
    using System.Data;
    using System.Text;
    
    [System.Serializable()]
    [System.Runtime.InteropServices.Guid("54de38a7-f113-4b2e-abb2-6d4c6ecfcf03")]
    public class KPI_XLineEntity :EntityBase
    {
        //
        //将ECTag的ECXlineXYZ分解为此实体，方便后续调用；创建、修改、删除不会使用该类。
        //
        protected String _XLineID = null;
        protected String _ECID = null;
        protected String _ECCode = null;
        protected String _ECCurveGroup = null;
        protected String _XLineMonth = null;
        protected String _XLineCoef = null;
        protected int _XLineGet = int.MinValue;

        protected String _XLineXBase = null;
        protected String _XLineYBase = null;
        //protected String _XLineZBase = null;

        protected double _XLineX = double.MinValue;
        protected double _XLineY = double.MinValue;
        //protected double _XLineZ = double.MinValue;

        protected double _XLineValue = double.MinValue;

        /// 该字段已经被作为Where的一部分
        public virtual String XLineID
        {
            get
            {
                return this._XLineID;
            }
            set
            {
                this._XLineID = value;
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

        public virtual String ECCode
        {
            get
            {
                return this._ECCode;
            }
            set
            {
                this._ECCode = value;
            }
        }

        public virtual String ECCurveGroup

        {
            get
            {
                return this._ECCurveGroup;
            }
            set
            {
                this._ECCurveGroup = value;
            }
        }

        public virtual String XLineMonth
        {
            get
            {
                return this._XLineMonth;
            }
            set
            {
                this._XLineMonth = value;
            }
        }
        /// 
        public virtual String XLineCoef
        {
            get
            {
                return this._XLineCoef;
            }
            set
            {
                this._XLineCoef = value;
            }
        }

        ///
        public virtual int XLineGet
        {
            get
            {
                return this._XLineGet;
            }
            set
            {
                this._XLineGet = value;
            }
        }


        /// 
        public virtual String XLineXBase
        {
            get
            {
                return this._XLineXBase;
            }
            set
            {
                this._XLineXBase = value;
            }
        }


        /// 
        public virtual String XLineYBase
        {
            get
            {
                return this._XLineYBase;
            }
            set
            {
                this._XLineYBase = value;
            }
        }


        /// 
        //public virtual String XLineZBase
        //{
        //    get
        //    {
        //        return this._XLineZBase;
        //    }
        //    set
        //    {
        //        this._XLineZBase = value;
        //    }
        //}

        ///
        public virtual double XLineX
        {
            get
            {
                return this._XLineX;
            }
            set
            {
                this._XLineX = value;
            }
        }


        ///
        public virtual double XLineY
        {
            get
            {
                return this._XLineY;
            }
            set
            {
                this._XLineY = value;
            }
        }


        ///
        //public virtual double XLineZ
        //{
        //    get
        //    {
        //        return this._XLineZ;
        //    }
        //    set
        //    {
        //        this._XLineZ = value;
        //    }
        //}

        public virtual double XLineValue
        {
            get
            {
                return this._XLineValue;
            }
            set
            {
                this._XLineValue = value;
            }
        }

        #region No DataBase 操作

        ///// <summary>
        ///// 
        ///// </summary>
        //public override string InsertSql
        //{
        //    get
        //    {
        //        System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
        //        tmpBuild.Append("insert into KPI_XLine(");
        //        if ((this.XLineID == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("XLineID");
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

        //        if ((this.XLineCoef == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("XLineCoef");
        //            tmpBuild.Append(",");
        //        }

        //        if ((this.XLineX == double.MinValue))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("XLineX");
        //            tmpBuild.Append(",");
        //        }
                
        //        if ((this.XLineValue == double.MinValue))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("XLineValue");
        //            tmpBuild.Append(",");
        //        }

        //        if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
        //        {
        //            tmpBuild.Remove((tmpBuild.Length - 1), 1);
        //        }
        //        tmpBuild.Append(") values(");


        //        if ((this.XLineID == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("'" + XLineID + "'");
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
        //        if ((this.XLineCoef == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("'" + XLineCoef + "'");
        //            tmpBuild.Append(",");
        //        }

        //        if ((this.XLineX == double.MinValue))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append(XLineX.ToString());
        //            tmpBuild.Append(",");
        //        }

        //        if ((this.XLineValue == double.MinValue))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append(XLineValue.ToString());
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

        ///// <summary>
        ///// 
        ///// </summary>
        //public override string UpdateSql
        //{
        //    get
        //    {
        //        System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
        //        tmpBuild.Append("update KPI_XLine set ");

        //        if ((this.XLineValue == double.MinValue))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("XLineX=" + XLineX.ToString());
        //            tmpBuild.Append(",");
        //        }

        //        if ((this.XLineValue == double.MinValue))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("XLineValue=" + XLineValue.ToString());
        //            tmpBuild.Append(",");
        //        }
        //        if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
        //        {
        //            tmpBuild.Remove((tmpBuild.Length - 1), 1);
        //        }

        //        tmpBuild.Append(" where ");
        //        tmpBuild.Append("XLineID='" + XLineID + "'");

        //        string __tmpSql = tmpBuild.ToString();
        //        return __tmpSql;
        //    }
        //}


        ///// <summary>
        ///// 
        ///// </summary>
        //public override string DeleteSql
        //{
        //    get
        //    {
        //        System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
        //        tmpBuild.Append("delete KPI_XLine");
        //        tmpBuild.Append(" where ");
        //        tmpBuild.Append("XLineID='" + XLineID + "'");

        //        string __tmpSql = tmpBuild.ToString();
        //        return __tmpSql;
        //    }
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //public override string SelectSql
        //{
        //    get
        //    {
        //        System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
        //        tmpBuild.Append("select ");
        //        tmpBuild.Append("XLineID");
        //        tmpBuild.Append(",");
        //        tmpBuild.Append("ECID");
        //        tmpBuild.Append(",");
        //        tmpBuild.Append("XLineCoef");
        //        tmpBuild.Append(",");
        //        tmpBuild.Append("XLineX");
        //        tmpBuild.Append(",");
        //        tmpBuild.Append("XLineValue");

        //        tmpBuild.Append(" from KPI_XLine");

        //        tmpBuild.Append(" where ");
        //        tmpBuild.Append("XLineID='" + XLineID + "'");

        //        string __tmpSql = tmpBuild.ToString();
        //        return __tmpSql;
        //    }
        //}

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public override bool DrToMember(System.Data.DataRow dr)
        {
            try
            {
                if ((dr["XLineID"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.XLineID = dr["XLineID"].ToString();
                }
                if ((dr["ECID"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.ECID = dr["ECID"].ToString();
                }

                if ((dr["ECCode"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.ECCode = dr["ECCode"].ToString();
                }

                if ((dr["XLineMonth"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.XLineMonth = dr["XLineMonth"].ToString();
                }

                if ((dr["XLineCoef"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.XLineCoef = dr["XLineCoef"].ToString();
                }

                if ((dr["XLineGet"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.XLineGet = int.Parse(dr["XLineGet"].ToString());
                }

                if ((dr["XLineXBase"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.XLineXBase = dr["XLineXBase"].ToString();
                }

                if ((dr["XLineYBase"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.XLineYBase = dr["XLineYBase"].ToString();
                }

                //if ((dr["XLineZBase"] == System.DBNull.Value))
                //{
                //}
                //else
                //{
                //    this.XLineZBase = dr["XLineZBase"].ToString();
                //}

                if ((dr["XLineX"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.XLineX = double.Parse(dr["XLineX"].ToString());
                }

                if ((dr["XLineY"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.XLineY = double.Parse(dr["XLineY"].ToString());
                }

                //if ((dr["XLineZ"] == System.DBNull.Value))
                //{
                //}
                //else
                //{
                //    this.XLineZ = double.Parse(dr["XLineZ"].ToString());
                //}

                if ((dr["XLineValue"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.XLineValue = double.Parse(dr["XLineValue"].ToString());
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
