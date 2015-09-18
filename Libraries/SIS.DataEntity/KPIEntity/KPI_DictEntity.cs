
namespace SIS.DataEntity
{
    using System;
    using System.Xml;
    using System.Data;
    using System.Text;
    
    [System.Serializable()]
    [System.Runtime.InteropServices.Guid("53e9841b-6309-4dc8-a554-9923a85743cc")]

    public class DictEntity : EntityBase
    {
        protected String _DictID = null;
        protected String _DictCode = null;
        protected String _DictName = null;
        protected String _DictDesc = null;
        protected String _DictValue = null;
        protected String _DictNote = null;
        protected String _DictCreateTime = null;
        protected String _DictModifyTime = null;

        public virtual String DictID
        {
            get
            {
                return this._DictID;
            }
            set
            {
                this._DictID = value;
            }
        }

        public virtual String DictCode
        {
            get
            {
                return this._DictCode;
            }
            set
            {
                this._DictCode = value;
            }
        }

        public virtual String DictName
        {
            get
            {
                return this._DictName;
            }
            set
            {
                this._DictName = value;
            }
        }

        public virtual String DictDesc
        {
            get
            {
                return this._DictDesc;
            }
            set
            {
                this._DictDesc = value;
            }
        }
        public virtual String DictValue
        {
            get
            {
                return this._DictValue;
            }
            set
            {
                this._DictValue = value;
            }
        }
                
        public virtual String DictNote
        {
            get
            {
                return this._DictNote;
            }
            set
            {
                this._DictNote = value;
            }
        }

        public virtual String DictCreateTime
        {
            get
            {
                return this._DictCreateTime;
            }
            set
            {
                this._DictCreateTime = value;
            }
        }

        public virtual String DictModifyTime
        {
            get
            {
                return this._DictModifyTime;
            }
            set
            {
                this._DictModifyTime = value;
            }
        }

        public override string InsertSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("insert into KPI_Dict(");

                if ((this.DictID == null))
                {
                }
                else
                {
                    tmpBuild.Append("DictID");
                    tmpBuild.Append(",");
                }

                if ((this.DictCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("DictCode");
                    tmpBuild.Append(",");
                }

                if ((this.DictName == null))
                {
                }
                else
                {
                    tmpBuild.Append("DictName");
                    tmpBuild.Append(",");
                }

                if ((this.DictDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("DictDesc");
                    tmpBuild.Append(",");
                }

                if ((this.DictValue == null))
                {
                }
                else
                {
                    tmpBuild.Append("DictValue");
                    tmpBuild.Append(",");
                }

                                
                if ((this.DictNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("DictNote");
                    tmpBuild.Append(",");
                }

                if ((this.DictCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("DictCreateTime");
                    tmpBuild.Append(",");
                }

                if ((this.DictModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("DictModifyTime");
                    tmpBuild.Append(",");
                }


                ///////////////////////////////////////////////////////////////////////////////////////


                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }

                tmpBuild.Append(") values(");
                if ((this.DictID == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + DictID + "'");
                    tmpBuild.Append(",");
                }

                if ((this.DictCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + DictCode + "'");
                    tmpBuild.Append(",");
                }
                                
                if ((this.DictName == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + DictName + "'");
                    tmpBuild.Append(",");
                }

                if ((this.DictDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + DictDesc + "'");
                    tmpBuild.Append(",");
                }


                if ((this.DictValue == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + DictValue + "'");
                    tmpBuild.Append(",");
                }
             
                if ((this.DictNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + DictNote + "'");
                    tmpBuild.Append(",");
                }

                if ((this.DictCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + DictCreateTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.DictModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + DictModifyTime + "'");
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
                tmpBuild.Append("update KPI_Dict set ");
                if ((this.DictID == null))
                {
                }
                else
                {
                    tmpBuild.Append("DictID='" + DictID + "'");
                    tmpBuild.Append(",");
                }

                if ((this.DictCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("DictCode='" + DictCode + "'");
                    tmpBuild.Append(",");
                }
                
                if ((this.DictName == null))
                {
                }
                else
                {
                    tmpBuild.Append("DictName='" + DictName + "'");
                    tmpBuild.Append(",");
                }

                if ((this.DictDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("DictDesc='" + DictDesc + "'");
                    tmpBuild.Append(",");
                }

                if ((this.DictValue == null))
                {
                }
                else
                {
                    tmpBuild.Append("DictValue='" + DictValue + "'");
                    tmpBuild.Append(",");
                }


                if ((this.DictNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("DictNote='" + DictNote + "'");
                    tmpBuild.Append(",");
                }

                if ((this.DictCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("DictCreateTime='" + DictCreateTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.DictModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("DictModifyTime='" + DictModifyTime + "'");
                    tmpBuild.Append(",");
                }

                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }

                tmpBuild.Append(" where ");
                tmpBuild.Append("DictID='" + DictID + "'");

                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }


        public override string DeleteSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("delete KPI_Dict");
                tmpBuild.Append(" where ");
                tmpBuild.Append("DictID='" + DictID + "'");

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
                tmpBuild.Append("DictID");
                tmpBuild.Append(",");
                tmpBuild.Append("DictCode");
                tmpBuild.Append(",");
                tmpBuild.Append("DictName");
                tmpBuild.Append(",");
                tmpBuild.Append("DictDesc");
                tmpBuild.Append(",");
                tmpBuild.Append("DictValue");
                tmpBuild.Append(",");
                tmpBuild.Append("DictNote");
                tmpBuild.Append(",");
                tmpBuild.Append("DictCreateTime");
                tmpBuild.Append(",");
                tmpBuild.Append("DictModifyTime");

                tmpBuild.Append(" from KPI_Dict");
                tmpBuild.Append(" where ");
                tmpBuild.Append("DictID='" + DictID + "'");

                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }

        public override bool DrToMember(System.Data.DataRow dr)
        {
            try
            {
                if (dr["DictID"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.DictID = dr["DictID"].ToString();
                }

                if (dr["DictCode"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.DictCode = dr["DictCode"].ToString();
                }
              
                if (dr["DictName"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.DictName = dr["DictName"].ToString();
                }

                if (dr["DictDesc"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.DictDesc = dr["DictDesc"].ToString();
                }

                if (dr["DictValue"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.DictValue = dr["DictValue"].ToString();
                }

                if (dr["DictNote"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.DictNote = dr["DictNote"].ToString();
                }


                if (dr["DictCreateTime"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.DictCreateTime = dr["DictCreateTime"].ToString();
                }


                if (dr["DictModifyTime"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.DictModifyTime = dr["DictModifyTime"].ToString();
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
