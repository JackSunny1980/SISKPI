
namespace SIS.DataEntity
{
    using System;
    using System.Xml;
    using System.Data;
    using System.Text;
    
    [System.Serializable()]
    [System.Runtime.InteropServices.Guid("53e9801b-6009-4dc8-a554-9923a85743cc")]

    public class KPI_RemoveEntity : EntityBase
    {
        protected String _RMID = null;
        protected int _RMType = int.MinValue;
        protected String _RMKPIID = null;
        protected String _RMName = null;
        protected int _RMIsValid = int.MinValue;
        protected String _RMStartTime = null;
        protected String _RMEndTime = null;
        protected String _RMNote = null;
        protected String _RMCreateTime = null;
        protected String _RMModifyTime = null;

        public virtual String RMID
        {
            get
            {
                return this._RMID;
            }
            set
            {
                this._RMID = value;
            }
        }

        public virtual int RMType
        {
            get
            {
                return this._RMType;
            }
            set
            {
                this._RMType = value;
            }
        }

        public virtual String RMKPIID
        {
            get
            {
                return this._RMKPIID;
            }
            set
            {
                this._RMKPIID = value;
            }
        }

        public virtual String RMName
        {
            get
            {
                return this._RMName;
            }
            set
            {
                this._RMName = value;
            }
        }

        public virtual int RMIsValid
        {
            get
            {
                return this._RMIsValid;
            }
            set
            {
                this._RMIsValid = value;
            }
        }
        
        public virtual String RMStartTime
        {
            get
            {
                return this._RMStartTime;
            }
            set
            {
                this._RMStartTime = value;
            }
        }

        public virtual String RMEndTime
        {
            get
            {
                return this._RMEndTime;
            }
            set
            {
                this._RMEndTime = value;
            }
        }        

        public virtual String RMNote
        {
            get
            {
                return this._RMNote;
            }
            set
            {
                this._RMNote = value;
            }
        }

        public virtual String RMCreateTime
        {
            get
            {
                return this._RMCreateTime;
            }
            set
            {
                this._RMCreateTime = value;
            }
        }

        public virtual String RMModifyTime
        {
            get
            {
                return this._RMModifyTime;
            }
            set
            {
                this._RMModifyTime = value;
            }
        }

        public override string InsertSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("insert into KPI_Remove(");

                if ((this.RMID == null))
                {
                }
                else
                {
                    tmpBuild.Append("RMID");
                    tmpBuild.Append(",");
                }

                if ((this.RMType == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("RMType");
                    tmpBuild.Append(",");
                }

                if ((this.RMKPIID == null))
                {
                }
                else
                {
                    tmpBuild.Append("RMKPIID");
                    tmpBuild.Append(",");
                }

                if ((this.RMName == null))
                {
                }
                else
                {
                    tmpBuild.Append("RMName");
                    tmpBuild.Append(",");
                }

                if ((this.RMIsValid == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("RMIsValid");
                    tmpBuild.Append(",");
                }

                if ((this.RMStartTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("RMStartTime");
                    tmpBuild.Append(",");
                }

                if ((this.RMEndTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("RMEndTime");
                    tmpBuild.Append(",");
                }
                                                      
                if ((this.RMNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("RMNote");
                    tmpBuild.Append(",");
                }

                if ((this.RMCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("RMCreateTime");
                    tmpBuild.Append(",");
                }

                if ((this.RMModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("RMModifyTime");
                    tmpBuild.Append(",");
                }


                ///////////////////////////////////////////////////////////////////////////////////////


                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }

                tmpBuild.Append(") values(");

                if ((this.RMID == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + RMID + "'");
                    tmpBuild.Append(",");
                }

                if ((this.RMType == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(RMType.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.RMKPIID == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + RMKPIID + "'");
                    tmpBuild.Append(",");
                }

                if ((this.RMName == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + RMName + "'");
                    tmpBuild.Append(",");
                }

                if ((this.RMIsValid == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(RMIsValid.ToString());
                    tmpBuild.Append(",");
                }
                
                if ((this.RMStartTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + RMStartTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.RMEndTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + RMEndTime + "'");
                    tmpBuild.Append(",");
                }
                                
                if ((this.RMNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + RMNote + "'");
                    tmpBuild.Append(",");
                }

                if ((this.RMCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + RMCreateTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.RMModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + RMModifyTime + "'");
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
                tmpBuild.Append("update KPI_Remove set ");
                if ((this.RMID == null))
                {
                }
                else
                {
                    tmpBuild.Append("RMID='" + RMID + "'");
                    tmpBuild.Append(",");
                }

                if ((this.RMType == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("RMType=" + RMType.ToString());
                    tmpBuild.Append(",");
                }
                

                if ((this.RMKPIID == null))
                {
                }
                else
                {
                    tmpBuild.Append("RMKPIID='" + RMKPIID + "'");
                    tmpBuild.Append(",");
                }

                if ((this.RMName == null))
                {
                }
                else
                {
                    tmpBuild.Append("RMName='" + RMName + "'");
                    tmpBuild.Append(",");
                }

                if ((this.RMIsValid == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("RMIsValid=" + RMIsValid.ToString());
                    tmpBuild.Append(",");
                }  

                if ((this.RMStartTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("RMStartTime='" + RMStartTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.RMEndTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("RMEndTime='" + RMEndTime + "'");
                    tmpBuild.Append(",");
                }
                
                if ((this.RMNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("RMNote='" + RMNote + "'");
                    tmpBuild.Append(",");
                }

                if ((this.RMCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("RMCreateTime='" + RMCreateTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.RMModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("RMModifyTime='" + RMModifyTime + "'");
                    tmpBuild.Append(",");
                }

                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }

                tmpBuild.Append(" where ");
                tmpBuild.Append("RMID='" + RMID + "'");

                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }


        public override string DeleteSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("delete KPI_Remove ");
                tmpBuild.Append(" where ");
                tmpBuild.Append("RMID='" + RMID + "'");

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
                tmpBuild.Append("RMID");
                tmpBuild.Append(",");
                tmpBuild.Append("RMType");
                tmpBuild.Append(",");
                tmpBuild.Append("RMKPIID");
                tmpBuild.Append(",");
                tmpBuild.Append("RMName");
                tmpBuild.Append(",");
                tmpBuild.Append("RMIsValid");
                tmpBuild.Append(",");
                tmpBuild.Append("RMStartTime");
                tmpBuild.Append(",");
                tmpBuild.Append("RMEndTime");
                tmpBuild.Append(",");
                tmpBuild.Append("RMNote");
                tmpBuild.Append(",");
                tmpBuild.Append("RMCreateTime");
                tmpBuild.Append(",");
                tmpBuild.Append("RMModifyTime");

                tmpBuild.Append(" from KPI_Remove");
                tmpBuild.Append(" where ");
                tmpBuild.Append("RMID='" + RMID + "'");

                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }

        public override bool DrToMember(System.Data.DataRow dr)
        {
            try
            {
                if (dr["RMID"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.RMID = dr["RMID"].ToString();
                }

                if (dr["RMType"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.RMType = int.Parse(dr["RMType"].ToString());
                }

                if (dr["RMKPIID"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.RMKPIID = dr["RMKPIID"].ToString();
                }

                if (dr["RMName"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.RMName = dr["RMName"].ToString();
                }

                if (dr["RMIsValid"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.RMIsValid = int.Parse(dr["RMIsValid"].ToString());
                }

                if (dr["RMStartTime"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.RMStartTime = dr["RMStartTime"].ToString();
                }
                if (dr["RMEndTime"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.RMEndTime = dr["RMEndTime"].ToString();
                }                
                
                if (dr["RMNote"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.RMNote = dr["RMNote"].ToString();
                }

                if (dr["RMCreateTime"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.RMCreateTime = dr["RMCreateTime"].ToString();
                }


                if (dr["RMModifyTime"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.RMModifyTime = dr["RMModifyTime"].ToString();
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
