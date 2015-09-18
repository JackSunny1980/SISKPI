namespace SIS.DataEntity
{
    using System;
    using System.Xml;
    using System.Data;
    using System.Text;

    [System.Serializable()]
    [System.Runtime.InteropServices.Guid("b826402a-6e99-4f2b-b387-f25a882222ab")]
    public class InputValueEntity : EntityBase
    {
        protected String _RVID = null;
        protected String _InputCode = null;
        protected String _InputDesc = null;
        protected String _InputEngunit = null;
        protected String _InputTime = null;
        protected String _InputValue = null;
        protected int _InputSnap = int.MinValue;

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

        public virtual String InputCode
        {
            get
            {
                return this._InputCode;
            }
            set
            {
                this._InputCode = value;
            }
        }
        public virtual String InputDesc
        {
            get
            {
                return this._InputDesc;
            }
            set
            {
                this._InputDesc = value;
            }
        }
        public virtual String InputEngunit
        {
            get
            {
                return this._InputEngunit;
            }
            set
            {
                this._InputEngunit = value;
            }
        }
        public virtual String InputTime
        {
            get
            {
                return this._InputTime;
            }
            set
            {
                this._InputTime = value;
            }
        }
        public virtual String InputValue
        {
            get
            {
                return this._InputValue;
            }
            set
            {
                this._InputValue = value;
            }
        }

        public virtual int InputSnap
        {
            get
            {
                return this._InputSnap;
            }
            set
            {
                this._InputSnap = value;
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
                tmpBuild.Append("insert into KPI_InputValue(");
                if ((this.RVID == null))
                {
                }
                else
                {
                    tmpBuild.Append("RVID");
                    tmpBuild.Append(",");
                }
                
                if ((this.InputCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("InputCode");
                    tmpBuild.Append(",");
                }
                if ((this.InputDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("InputDesc");
                    tmpBuild.Append(",");
                }
                if ((this.InputEngunit == null))
                {
                }
                else
                {
                    tmpBuild.Append("InputEngunit");
                    tmpBuild.Append(",");
                }
                if ((this.InputTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("InputTime");
                    tmpBuild.Append(",");
                }
                if ((this.InputValue == null))
                {
                }
                else
                {
                    tmpBuild.Append("InputValue");
                    tmpBuild.Append(",");
                }

                if ((this.InputSnap == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("InputSnap");
                    tmpBuild.Append(",");
                }
                

                ////////////////////////////////////////////////////////////////
                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }

                tmpBuild.Append(") values(");

                if ((this.RVID == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + RVID + "'");
                    tmpBuild.Append(",");
                }

                if ((this.InputCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + InputCode + "'");
                    tmpBuild.Append(",");
                }
                if ((this.InputDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + InputDesc + "'");
                    tmpBuild.Append(",");
                }

                if ((this.InputEngunit == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + InputEngunit + "'");
                    tmpBuild.Append(",");
                }


                if ((this.InputTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + InputTime + "'");
                    tmpBuild.Append(",");
                }
                if ((this.InputValue == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + InputValue + "'");
                    tmpBuild.Append(",");
                }

                if ((this.InputSnap == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(InputSnap.ToString());
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
                tmpBuild.Append("update KPI_InputValue set ");

                if (this.InputCode == null)
                {
                }
                else
                {
                    tmpBuild.Append("InputCode='" + InputCode + "'");
                    tmpBuild.Append(",");
                }
                if (this.InputDesc == null)
                {
                }
                else
                {
                    tmpBuild.Append("InputDesc='" + InputDesc + "'");
                    tmpBuild.Append(",");
                }
                if (this.InputEngunit == null)
                {
                }
                else
                {
                    tmpBuild.Append("InputEngunit='" + InputEngunit + "'");
                    tmpBuild.Append(",");
                }

                if (this.InputTime == null)
                {
                }
                else
                {
                    tmpBuild.Append("InputTime='" + InputTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.InputValue == null))
                {
                }
                else
                {
                    tmpBuild.Append("InputValue='" + InputValue + "'");
                    tmpBuild.Append(",");
                }


                if (this.InputSnap == int.MinValue)
                {
                }
                else
                {
                    tmpBuild.Append("InputSnap=" + InputSnap.ToString());
                    tmpBuild.Append(",");
                }

                
                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }

                tmpBuild.Append(" where ");
                tmpBuild.Append("RVID='" + RVID + "'");
                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }

        public override string DeleteSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("delete KPI_InputValue");
                tmpBuild.Append(" where ");
                tmpBuild.Append("RVID='" + RVID + "'");
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
                tmpBuild.Append("RVID");
                tmpBuild.Append(",");
                tmpBuild.Append("InputCode");
                tmpBuild.Append(",");
                tmpBuild.Append("InputDesc");
                tmpBuild.Append(",");
                tmpBuild.Append("InputEngunit");
                tmpBuild.Append(",");
                tmpBuild.Append("InputTime");
                tmpBuild.Append(",");
                tmpBuild.Append("InputValue");
                tmpBuild.Append(",");
                tmpBuild.Append("InputSnap");

                tmpBuild.Append(" from KPI_InputValue");
                tmpBuild.Append(" where ");
                tmpBuild.Append("RVID='" + RVID + "'");
                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
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
                if ((dr["InputCode"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.InputCode = dr["InputCode"].ToString();
                }
                if ((dr["InputDesc"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.InputDesc = dr["InputDesc"].ToString();
                }
                if ((dr["InputEngunit"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.InputEngunit = dr["InputEngunit"].ToString();
                }
                if ((dr["InputTime"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.InputTime = dr["InputTime"].ToString();
                }
                if ((dr["InputValue"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.InputValue = dr["InputValue"].ToString();
                }

                if ((dr["InputSnap"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.InputSnap = int.Parse(dr["InputSnap"].ToString());
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
