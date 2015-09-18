
namespace SIS.DataEntity
{
    using System;
    using System.Xml;
    using System.Data;
    using System.Text;
    
    [System.Serializable()]
    [System.Runtime.InteropServices.Guid("ECA1A83A-32AC-498C-B06D-C834DD8B917E")]

    public class KPI_SecurityEntity : EntityBase
    {
        //
        //将SATag的SAScoreExp分解为此实体，方便后续调用；创建、修改、删除不会使用该类。
        //
        protected String _SecurityID = null;
        protected String _SAID = null;

        protected String _SecurityCalcExp = null;
        protected String _SecurityGainExp = null;
        protected int _SecurityOptimal = int.MinValue;
        protected int _SecurityAlarm = int.MinValue;
        protected int _SecurityIsValid = int.MinValue;

        public virtual String SecurityID
        {
            get
            {
                return this._SecurityID;
            }
            set
            {
                this._SecurityID = value;
            }
        }

        public virtual String SAID
        {
            get
            {
                return this._SAID;
            }
            set
            {
                this._SAID = value;
            }
        }
                
        public virtual String SecurityCalcExp
        {
            get
            {
                return this._SecurityCalcExp;
            }
            set
            {
                this._SecurityCalcExp = value;
            }
        }

        public virtual String SecurityGainExp
        {
            get
            {
                return this._SecurityGainExp;
            }
            set
            {
                this._SecurityGainExp = value;
            }
        }

        public virtual int SecurityOptimal
        {
            get
            {
                return this._SecurityOptimal;
            }
            set
            {
                this._SecurityOptimal = value;
            }
        }

        public virtual int SecurityAlarm
        {
            get
            {
                return this._SecurityAlarm;
            }
            set
            {
                this._SecurityAlarm = value;
            }
        }

        public virtual int SecurityIsValid
        {
            get
            {
                return this._SecurityIsValid;
            }
            set
            {
                this._SecurityIsValid = value;
            }
        }

        #region NO DataBase 操作

        //public override string InsertSql
        //{
        //    get
        //    {
        //        System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
        //        tmpBuild.Append("insert into KPI_Security (");

        //        if ((this.SecurityID == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("SecurityID");
        //            tmpBuild.Append(",");
        //        }

        //        if ((this.SAID == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("SAID");
        //            tmpBuild.Append(",");
        //        }

        //        if ((this.SecurityCalcExp == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("SecurityCalcExp");
        //            tmpBuild.Append(",");
        //        }
        //        if ((this.SecurityGainExp == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("SecurityGainExp");
        //            tmpBuild.Append(",");
        //        }

        //        if ((this.SecurityOptimal == int.MinValue))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("SecurityOptimal");
        //            tmpBuild.Append(",");
        //        }

        //        if ((this.SecurityAlarm == int.MinValue))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("SecurityAlarm");
        //            tmpBuild.Append(",");
        //        }  
                
        //        if ((this.SecurityIsValid == int.MinValue))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("SecurityIsValid");
        //            tmpBuild.Append(",");
        //        }
                                
        //        ///////////////////////////////////////////////////////////////////////////////////////


        //        if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
        //        {
        //            tmpBuild.Remove((tmpBuild.Length - 1), 1);
        //        }

        //        tmpBuild.Append(") values(");

        //        if ((this.SecurityID == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("'" +SecurityID+"'");
        //            tmpBuild.Append(",");
        //        }

        //        if ((this.SAID == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("'"+SAID+"'");
        //            tmpBuild.Append(",");
        //        }
                
        //        if ((this.SecurityCalcExp == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("'"+SecurityCalcExp+"'");
        //            tmpBuild.Append(",");
        //        }
        //        if ((this.SecurityGainExp == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("'"+SecurityGainExp+"'");
        //            tmpBuild.Append(",");
        //        }

        //        if ((this.SecurityOptimal == int.MinValue))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append(SecurityOptimal.ToString());
        //            tmpBuild.Append(",");
        //        }

        //        if ((this.SecurityAlarm == int.MinValue))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append(SecurityAlarm.ToString());
        //            tmpBuild.Append(",");
        //        }

        //        if ((this.SecurityIsValid == int.MinValue))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append(SecurityIsValid.ToString());
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
        //        tmpBuild.Append("update KPI_Security set ");

        //        if ((this.SecurityID == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("SecurityID='" + SecurityID + "'");
        //            tmpBuild.Append(",");
        //        }

        //        if ((this.SAID == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("SAID='" + SAID + "'");
        //            tmpBuild.Append(",");
        //        }
                
        //        if ((this.SecurityCalcExp == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("SecurityCalcExp='" + SecurityCalcExp + "'");
        //            tmpBuild.Append(",");
        //        }
        //        if ((this.SecurityGainExp == null))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("SecurityGainExp='" + SecurityGainExp + "'");
        //            tmpBuild.Append(",");
        //        }
                
        //        if ((this.SecurityOptimal == int.MinValue))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("SecurityOptimal="+SecurityOptimal.ToString());
        //            tmpBuild.Append(",");
        //        }

        //        if ((this.SecurityAlarm == int.MinValue))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("SecurityAlarm="+SecurityAlarm.ToString());
        //            tmpBuild.Append(",");
        //        }

        //        if ((this.SecurityIsValid == int.MinValue))
        //        {
        //        }
        //        else
        //        {
        //            tmpBuild.Append("SecurityIsValid="+SecurityIsValid.ToString());
        //            tmpBuild.Append(",");
        //        }
                
        //        if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
        //        {
        //            tmpBuild.Remove((tmpBuild.Length - 1), 1);
        //        }

        //        tmpBuild.Append(" where ");
        //        tmpBuild.Append("SecurityID='" + SecurityID + "'");

        //        string __tmpSql = tmpBuild.ToString();
        //        return __tmpSql;
        //    }
        //}

        //public override string DeleteSql
        //{
        //    get
        //    {
        //        System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
        //        tmpBuild.Append("delete KPI_Security");
        //        tmpBuild.Append(" where ");
        //        tmpBuild.Append("SecurityID='" + SecurityID + "'");

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
        //        tmpBuild.Append("SecurityID");
        //        tmpBuild.Append(",");
        //        tmpBuild.Append("SAID");
        //        tmpBuild.Append(",");
        //        tmpBuild.Append("SecurityCalcExp");
        //        tmpBuild.Append(",");
        //        tmpBuild.Append("SecurityGainExp");
        //        tmpBuild.Append(",");
        //        tmpBuild.Append("SecurityOptimal");
        //        tmpBuild.Append(",");
        //        tmpBuild.Append("SecurityAlarm");

        //        tmpBuild.Append(" from KPI_Security ");
        //        tmpBuild.Append(" where ");
        //        tmpBuild.Append("SecurityID='" + SecurityID + "'");

        //        string __tmpSql = tmpBuild.ToString();
        //        return __tmpSql;
        //    }
        //}

        #endregion

        public override bool DrToMember(System.Data.DataRow dr)
        {
            try
            {
                if (dr["SecurityID"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.SecurityID = dr["SecurityID"].ToString();
                }
              
                if (dr["SAID"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.SAID = dr["SAID"].ToString();
                }
                                
                if (dr["SecurityCalcExp"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.SecurityCalcExp = dr["SecurityCalcExp"].ToString();
                }
                if (dr["SecurityGainExp"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.SecurityGainExp = dr["SecurityGainExp"].ToString();
                }
                
                if (dr["SecurityOptimal"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.SecurityOptimal = int.Parse(dr["SecurityOptimal"].ToString());
                }
                if (dr["SecurityAlarm"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.SecurityAlarm = int.Parse(dr["SecurityAlarm"].ToString());
                }
                if (dr["SecurityIsValid"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.SecurityIsValid = int.Parse(dr["SecurityIsValid"].ToString());
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
