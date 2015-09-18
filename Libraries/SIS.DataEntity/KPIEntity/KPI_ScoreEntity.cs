
namespace SIS.DataEntity
{
    using System;
    using System.Xml;
    using System.Data;
    using System.Text;
    
    [System.Serializable()]
    [System.Runtime.InteropServices.Guid("50e9801b-6409-4dc8-a554-9923a85743dc")]

    public class KPI_ScoreEntity : EntityBase
    {
        //
        //将ECTag的ECScoreExp分解为此实体，方便后续调用；创建、修改、删除不会使用该类。
        //
        protected String _ScoreID = null;
        protected String _ECID = null;
        protected String _ScoreCalcExp = null;
        protected String _ScoreGainExp = null;
        protected int _ScoreOptimal = int.MinValue;
        protected int _ScoreAlarm = int.MinValue;
        protected int _ScoreIsValid = int.MinValue;

        public virtual String ScoreID
        {
            get
            {
                return this._ScoreID;
            }
            set
            {
                this._ScoreID = value;
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
                
        public virtual String ScoreCalcExp
        {
            get
            {
                return this._ScoreCalcExp;
            }
            set
            {
                this._ScoreCalcExp = value;
            }
        }

        public virtual String ScoreGainExp
        {
            get
            {
                return this._ScoreGainExp;
            }
            set
            {
                this._ScoreGainExp = value;
            }
        }

        public virtual int ScoreOptimal
        {
            get
            {
                return this._ScoreOptimal;
            }
            set
            {
                this._ScoreOptimal = value;
            }
        }

        public virtual int ScoreAlarm
        {
            get
            {
                return this._ScoreAlarm;
            }
            set
            {
                this._ScoreAlarm = value;
            }
        }

        public virtual int ScoreIsValid
        {
            get
            {
                return this._ScoreIsValid;
            }
            set
            {
                this._ScoreIsValid = value;
            }
        }

        #region No DataBase 操作

        //public override string InsertSql
        //{
        //    get
        //    {
        //        System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
        //        tmpBuild.Append("insert into KPI_Score (");

        //        if ((this.ScoreID == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("ScoreID");
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
                
        //        if ((this.ScoreCalcExp == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("ScoreCalcExp");
        //            tmpBuild.Append(",");
        //        }
        //        if ((this.ScoreGainExp == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("ScoreGainExp");
        //            tmpBuild.Append(",");
        //        }

        //        if ((this.ScoreOptimal == int.MinValue))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("ScoreOptimal");
        //            tmpBuild.Append(",");
        //        }

        //        if ((this.ScoreAlarm == int.MinValue))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("ScoreAlarm");
        //            tmpBuild.Append(",");
        //        }  
                
        //        if ((this.ScoreIsValid == int.MinValue))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("ScoreIsValid");
        //            tmpBuild.Append(",");
        //        }

        //        ///////////////////////////////////////////////////////////////////////////////////////


        //        if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
        //        {
        //            tmpBuild.Remove((tmpBuild.Length - 1), 1);
        //        }

        //        tmpBuild.Append(") values(");

        //        if ((this.ScoreID == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("'" +ScoreID+"'");
        //            tmpBuild.Append(",");
        //        }

        //        if ((this.ECID == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("'"+ECID+"'");
        //            tmpBuild.Append(",");
        //        }


        //        if ((this.ScoreCalcExp == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("'"+ScoreCalcExp+"'");
        //            tmpBuild.Append(",");
        //        }
        //        if ((this.ScoreGainExp == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("'"+ScoreGainExp+"'");
        //            tmpBuild.Append(",");
        //        }

        //        if ((this.ScoreOptimal == int.MinValue))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append(ScoreOptimal.ToString());
        //            tmpBuild.Append(",");
        //        }

        //        if ((this.ScoreAlarm == int.MinValue))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append(ScoreAlarm.ToString());
        //            tmpBuild.Append(",");
        //        }

        //        if ((this.ScoreIsValid == int.MinValue))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append(ScoreIsValid.ToString());
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
        //        tmpBuild.Append("update KPI_Score set ");

        //        if ((this.ScoreID == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("ScoreID='" + ScoreID + "'");
        //            tmpBuild.Append(",");
        //        }

        //        if ((this.ECID == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("ECID='" + ECID + "'");
        //            tmpBuild.Append(",");
        //        }
                
        //        if ((this.ScoreCalcExp == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("ScoreCalcExp='" + ScoreCalcExp + "'");
        //            tmpBuild.Append(",");
        //        }
        //        if ((this.ScoreGainExp == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("ScoreGainExp='" + ScoreGainExp + "'");
        //            tmpBuild.Append(",");
        //        }
                
        //        if ((this.ScoreOptimal == int.MinValue))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("ScoreOptimal="+ScoreOptimal.ToString());
        //            tmpBuild.Append(",");
        //        }

        //        if ((this.ScoreAlarm == int.MinValue))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("ScoreAlarm="+ScoreAlarm.ToString());
        //            tmpBuild.Append(",");
        //        }

        //        if ((this.ScoreIsValid == int.MinValue))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("ScoreIsValid="+ScoreIsValid.ToString());
        //            tmpBuild.Append(",");
        //        }

        //        if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
        //        {
        //            tmpBuild.Remove((tmpBuild.Length - 1), 1);
        //        }

        //        tmpBuild.Append(" where ");
        //        tmpBuild.Append("ScoreID='" + ScoreID + "'");

        //        string __tmpSql = tmpBuild.ToString();
        //        return __tmpSql;
        //    }
        //}

        //public override string DeleteSql
        //{
        //    get
        //    {
        //        System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
        //        tmpBuild.Append("delete KPI_Score");
        //        tmpBuild.Append(" where ");
        //        tmpBuild.Append("ScoreID='" + ScoreID + "'");

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
        //        tmpBuild.Append("ScoreID");
        //        tmpBuild.Append(",");
        //        tmpBuild.Append("ECID");
        //        tmpBuild.Append(",");
        //        tmpBuild.Append("ScoreCalcExp");
        //        tmpBuild.Append(",");
        //        tmpBuild.Append("ScoreGainExp");
        //        tmpBuild.Append(",");
        //        tmpBuild.Append("ScoreOptimal");
        //        tmpBuild.Append(",");
        //        tmpBuild.Append("ScoreAlarm");
        //        tmpBuild.Append(",");
        //        tmpBuild.Append("ScoreIsValid");

        //        tmpBuild.Append(" from KPI_Score ");
        //        tmpBuild.Append(" where ");
        //        tmpBuild.Append("ScoreID='" + ScoreID + "'");

        //        string __tmpSql = tmpBuild.ToString();
        //        return __tmpSql;
        //    }
        //}

        #endregion

        public override bool DrToMember(System.Data.DataRow dr)
        {
            try
            {
                if (dr["ScoreID"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.ScoreID = dr["ScoreID"].ToString();
                }
              
                if (dr["ECID"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.ECID = dr["ECID"].ToString();
                }
                
                if (dr["ScoreCalcExp"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.ScoreCalcExp = dr["ScoreCalcExp"].ToString();
                }
                if (dr["ScoreGainExp"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.ScoreGainExp = dr["ScoreGainExp"].ToString();
                }
                
                if (dr["ScoreOptimal"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.ScoreOptimal = int.Parse(dr["ScoreOptimal"].ToString());
                }
                if (dr["ScoreAlarm"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.ScoreAlarm = int.Parse(dr["ScoreAlarm"].ToString());
                }
                if (dr["ScoreIsValid"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.ScoreIsValid = int.Parse(dr["ScoreIsValid"].ToString());
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
