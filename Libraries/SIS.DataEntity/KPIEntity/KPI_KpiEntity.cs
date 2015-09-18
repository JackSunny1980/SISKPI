
namespace SIS.DataEntity
{
    using System;
    using System.Xml;
    using System.Data;
    using System.Text;
    
    [System.Serializable()]
    [System.Runtime.InteropServices.Guid("53e9841b-6309-4dc8-a554-9923a85743cc")]

    public class KpiEntity : EntityBase
    {
        protected String _KpiID = null;
        protected String _KpiCode = null;
        protected String _KpiName = null;
        protected String _KpiDesc = null;
        protected int _KpiIndex = int.MinValue;
        protected int _KpiIsValid = int.MinValue;
        protected String _KpiNote = null;
        protected String _KpiCreateTime = null;
        protected String _KpiModifyTime = null;

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

        public virtual String KpiCode
        {
            get
            {
                return this._KpiCode;
            }
            set
            {
                this._KpiCode = value;
            }
        }

        public virtual String KpiName
        {
            get
            {
                return this._KpiName;
            }
            set
            {
                this._KpiName = value;
            }
        }

        public virtual String KpiDesc
        {
            get
            {
                return this._KpiDesc;
            }
            set
            {
                this._KpiDesc = value;
            }
        }

        public virtual int KpiIndex
        {
            get
            {
                return this._KpiIndex;
            }
            set
            {
                this._KpiIndex = value;
            }
        }

        public virtual int KpiIsValid
        {
            get
            {
                return this._KpiIsValid;
            }
            set
            {
                this._KpiIsValid = value;
            }
        }
        
        public virtual String KpiNote
        {
            get
            {
                return this._KpiNote;
            }
            set
            {
                this._KpiNote = value;
            }
        }

        public virtual String KpiCreateTime
        {
            get
            {
                return this._KpiCreateTime;
            }
            set
            {
                this._KpiCreateTime = value;
            }
        }

        public virtual String KpiModifyTime
        {
            get
            {
                return this._KpiModifyTime;
            }
            set
            {
                this._KpiModifyTime = value;
            }
        }

        public override string InsertSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("insert into KPI_Kpi(");

                if ((this.KpiID == null))
                {
                }
                else
                {
                    tmpBuild.Append("KpiID");
                    tmpBuild.Append(",");
                }

                if ((this.KpiCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("KpiCode");
                    tmpBuild.Append(",");
                }

                if ((this.KpiName == null))
                {
                }
                else
                {
                    tmpBuild.Append("KpiName");
                    tmpBuild.Append(",");
                }

                if ((this.KpiDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("KpiDesc");
                    tmpBuild.Append(",");
                }

                if ((this.KpiIndex == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("KpiIndex");
                    tmpBuild.Append(",");
                }

                if ((this.KpiIsValid == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("KpiIsValid");
                    tmpBuild.Append(",");
                }
                                
                if ((this.KpiNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("KpiNote");
                    tmpBuild.Append(",");
                }

                if ((this.KpiCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("KpiCreateTime");
                    tmpBuild.Append(",");
                }

                if ((this.KpiModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("KpiModifyTime");
                    tmpBuild.Append(",");
                }


                ///////////////////////////////////////////////////////////////////////////////////////


                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }

                tmpBuild.Append(") values(");
                if ((this.KpiID == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + KpiID + "'");
                    tmpBuild.Append(",");
                }

                if ((this.KpiCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + KpiCode + "'");
                    tmpBuild.Append(",");
                }
                                
                if ((this.KpiName == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + KpiName + "'");
                    tmpBuild.Append(",");
                }

                if ((this.KpiDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + KpiDesc + "'");
                    tmpBuild.Append(",");
                }


                if ((this.KpiIndex == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(KpiIndex.ToString());
                    tmpBuild.Append(",");
                }
                if ((this.KpiIsValid == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(KpiIsValid.ToString());
                    tmpBuild.Append(",");
                }
             
                if ((this.KpiNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + KpiNote + "'");
                    tmpBuild.Append(",");
                }

                if ((this.KpiCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + KpiCreateTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.KpiModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + KpiModifyTime + "'");
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
                tmpBuild.Append("update KPI_Kpi set ");
                if ((this.KpiID == null))
                {
                }
                else
                {
                    tmpBuild.Append("KpiID='" + KpiID + "'");
                    tmpBuild.Append(",");
                }

                if ((this.KpiCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("KpiCode='" + KpiCode + "'");
                    tmpBuild.Append(",");
                }
                
                if ((this.KpiName == null))
                {
                }
                else
                {
                    tmpBuild.Append("KpiName='" + KpiName + "'");
                    tmpBuild.Append(",");
                }

                if ((this.KpiDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("KpiDesc='" + KpiDesc + "'");
                    tmpBuild.Append(",");
                }

                if ((this.KpiIndex == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("KpiIndex=" + KpiIndex.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.KpiIsValid == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("KpiIsValid=" + KpiIsValid.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.KpiNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("KpiNote='" + KpiNote + "'");
                    tmpBuild.Append(",");
                }

                if ((this.KpiCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("KpiCreateTime='" + KpiCreateTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.KpiModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("KpiModifyTime='" + KpiModifyTime + "'");
                    tmpBuild.Append(",");
                }

                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }

                tmpBuild.Append(" where ");
                tmpBuild.Append("KpiID='" + KpiID + "'");

                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }


        public override string DeleteSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("delete KPI_Kpi");
                tmpBuild.Append(" where ");
                tmpBuild.Append("KpiID='" + KpiID + "'");

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
                tmpBuild.Append("KpiID");
                tmpBuild.Append(",");
                tmpBuild.Append("KpiCode");
                tmpBuild.Append(",");
                tmpBuild.Append("KpiName");
                tmpBuild.Append(",");
                tmpBuild.Append("KpiDesc");
                tmpBuild.Append(",");
                tmpBuild.Append("KpiIndex");
                tmpBuild.Append(",");
                tmpBuild.Append("KpiIsValid");
                tmpBuild.Append(",");
                tmpBuild.Append("KpiNote");
                tmpBuild.Append(",");
                tmpBuild.Append("KpiCreateTime");
                tmpBuild.Append(",");
                tmpBuild.Append("KpiModifyTime");

                tmpBuild.Append(" from KPI_Kpi");
                tmpBuild.Append(" where ");
                tmpBuild.Append("KpiID='" + KpiID + "'");

                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }

        public override bool DrToMember(System.Data.DataRow dr)
        {
            try
            {
                if (dr["KpiID"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.KpiID = dr["KpiID"].ToString();
                }

                if (dr["KpiCode"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.KpiCode = dr["KpiCode"].ToString();
                }
              
                if (dr["KpiName"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.KpiName = dr["KpiName"].ToString();
                }

                if (dr["KpiDesc"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.KpiDesc = dr["KpiDesc"].ToString();
                }

                if (dr["KpiIndex"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.KpiIndex = int.Parse(dr["KpiIndex"].ToString());
                }

                if (dr["KpiIsValid"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.KpiIsValid = int.Parse(dr["KpiIsValid"].ToString());
                }

                if (dr["KpiNote"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.KpiNote = dr["KpiNote"].ToString();
                }


                if (dr["KpiCreateTime"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.KpiCreateTime = dr["KpiCreateTime"].ToString();
                }


                if (dr["KpiModifyTime"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.KpiModifyTime = dr["KpiModifyTime"].ToString();
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
