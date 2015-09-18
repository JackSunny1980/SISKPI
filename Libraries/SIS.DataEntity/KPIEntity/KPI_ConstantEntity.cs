
namespace SIS.DataEntity
{
    using System;
    using System.Xml;
    using System.Data;
    using System.Text;
    using System.ComponentModel;
    
    [System.Serializable()]
    [System.Runtime.InteropServices.Guid("53e9841b-6309-4dc8-a554-9923a85743cc")]

    public class ConstantEntity : EntityBase
    {
        protected String _ConstantID = null;
        protected String _ConstantCode = null;
        protected String _ConstantName = null;
        protected String _ConstantDesc = null;
        protected String _ConstantValue = null;
        protected String _ConstantNote = null;
        protected String _ConstantCreateTime = null;
        protected String _ConstantModifyTime = null;

        [Description("ConstantID")]
        public virtual String ConstantID
        {
            get
            {
                return this._ConstantID;
            }
            set
            {
                this._ConstantID = value;
            }
        }

        [Description("ConstantCode")]
        public virtual String ConstantCode
        {
            get
            {
                return this._ConstantCode;
            }
            set
            {
                this._ConstantCode = value;
            }
        }

        [Description("ConstantName")]
        public virtual String ConstantName
        {
            get
            {
                return this._ConstantName;
            }
            set
            {
                this._ConstantName = value;
            }
        }

        [Description("ConstantDesc")]
        public virtual String ConstantDesc
        {
            get
            {
                return this._ConstantDesc;
            }
            set
            {
                this._ConstantDesc = value;
            }
        }
        [Description("ConstantValue")]
        public virtual String ConstantValue
        {
            get
            {
                return this._ConstantValue;
            }
            set
            {
                this._ConstantValue = value;
            }
        }

        [Description("ConstantNote")]
        public virtual String ConstantNote
        {
            get
            {
                return this._ConstantNote;
            }
            set
            {
                this._ConstantNote = value;
            }
        }

        [Description("ConstantCreateTime")]
        public virtual String ConstantCreateTime
        {
            get
            {
                return this._ConstantCreateTime;
            }
            set
            {
                this._ConstantCreateTime = value;
            }
        }

        [Description("ConstantModifyTime")]
        public virtual String ConstantModifyTime
        {
            get
            {
                return this._ConstantModifyTime;
            }
            set
            {
                this._ConstantModifyTime = value;
            }
        }

        public override string InsertSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("insert into KPI_Constant(");

                if ((this.ConstantID == null))
                {
                }
                else
                {
                    tmpBuild.Append("ConstantID");
                    tmpBuild.Append(",");
                }

                if ((this.ConstantCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("ConstantCode");
                    tmpBuild.Append(",");
                }

                if ((this.ConstantName == null))
                {
                }
                else
                {
                    tmpBuild.Append("ConstantName");
                    tmpBuild.Append(",");
                }

                if ((this.ConstantDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("ConstantDesc");
                    tmpBuild.Append(",");
                }

                if ((this.ConstantValue == null))
                {
                }
                else
                {
                    tmpBuild.Append("ConstantValue");
                    tmpBuild.Append(",");
                }

                                
                if ((this.ConstantNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("ConstantNote");
                    tmpBuild.Append(",");
                }

                if ((this.ConstantCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("ConstantCreateTime");
                    tmpBuild.Append(",");
                }

                if ((this.ConstantModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("ConstantModifyTime");
                    tmpBuild.Append(",");
                }


                ///////////////////////////////////////////////////////////////////////////////////////


                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }

                tmpBuild.Append(") values(");
                if ((this.ConstantID == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + ConstantID + "'");
                    tmpBuild.Append(",");
                }

                if ((this.ConstantCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + ConstantCode + "'");
                    tmpBuild.Append(",");
                }
                                
                if ((this.ConstantName == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + ConstantName + "'");
                    tmpBuild.Append(",");
                }

                if ((this.ConstantDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + ConstantDesc + "'");
                    tmpBuild.Append(",");
                }


                if ((this.ConstantValue == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + ConstantValue + "'");
                    tmpBuild.Append(",");
                }
             
                if ((this.ConstantNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + ConstantNote + "'");
                    tmpBuild.Append(",");
                }

                if ((this.ConstantCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + ConstantCreateTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.ConstantModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + ConstantModifyTime + "'");
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
                tmpBuild.Append("update KPI_Constant set ");
                if ((this.ConstantID == null))
                {
                }
                else
                {
                    tmpBuild.Append("ConstantID='" + ConstantID + "'");
                    tmpBuild.Append(",");
                }

                if ((this.ConstantCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("ConstantCode='" + ConstantCode + "'");
                    tmpBuild.Append(",");
                }
                
                if ((this.ConstantName == null))
                {
                }
                else
                {
                    tmpBuild.Append("ConstantName='" + ConstantName + "'");
                    tmpBuild.Append(",");
                }

                if ((this.ConstantDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("ConstantDesc='" + ConstantDesc + "'");
                    tmpBuild.Append(",");
                }

                if ((this.ConstantValue == null))
                {
                }
                else
                {
                    tmpBuild.Append("ConstantValue='" + ConstantValue + "'");
                    tmpBuild.Append(",");
                }


                if ((this.ConstantNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("ConstantNote='" + ConstantNote + "'");
                    tmpBuild.Append(",");
                }

                if ((this.ConstantCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("ConstantCreateTime='" + ConstantCreateTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.ConstantModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("ConstantModifyTime='" + ConstantModifyTime + "'");
                    tmpBuild.Append(",");
                }

                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }

                tmpBuild.Append(" where ");
                tmpBuild.Append("ConstantID='" + ConstantID + "'");

                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }


        public override string DeleteSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("delete KPI_Constant");
                tmpBuild.Append(" where ");
                tmpBuild.Append("ConstantID='" + ConstantID + "'");

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
                tmpBuild.Append("ConstantID");
                tmpBuild.Append(",");
                tmpBuild.Append("ConstantCode");
                tmpBuild.Append(",");
                tmpBuild.Append("ConstantName");
                tmpBuild.Append(",");
                tmpBuild.Append("ConstantDesc");
                tmpBuild.Append(",");
                tmpBuild.Append("ConstantValue");
                tmpBuild.Append(",");
                tmpBuild.Append("ConstantNote");
                tmpBuild.Append(",");
                tmpBuild.Append("ConstantCreateTime");
                tmpBuild.Append(",");
                tmpBuild.Append("ConstantModifyTime");

                tmpBuild.Append(" from KPI_Constant");
                tmpBuild.Append(" where ");
                tmpBuild.Append("ConstantID='" + ConstantID + "'");

                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }

        public override bool DrToMember(System.Data.DataRow dr)
        {
            try
            {
                if (dr["ConstantID"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.ConstantID = dr["ConstantID"].ToString();
                }

                if (dr["ConstantCode"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.ConstantCode = dr["ConstantCode"].ToString();
                }
              
                if (dr["ConstantName"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.ConstantName = dr["ConstantName"].ToString();
                }

                if (dr["ConstantDesc"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.ConstantDesc = dr["ConstantDesc"].ToString();
                }

                if (dr["ConstantValue"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.ConstantValue = dr["ConstantValue"].ToString();
                }

                if (dr["ConstantNote"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.ConstantNote = dr["ConstantNote"].ToString();
                }


                if (dr["ConstantCreateTime"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.ConstantCreateTime = dr["ConstantCreateTime"].ToString();
                }


                if (dr["ConstantModifyTime"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.ConstantModifyTime = dr["ConstantModifyTime"].ToString();
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
