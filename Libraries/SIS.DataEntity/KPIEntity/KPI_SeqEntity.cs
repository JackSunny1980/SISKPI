
namespace SIS.DataEntity
{
    using System;
    using System.Xml;
    using System.Data;
    using System.Text;
    
    [System.Serializable()]
    [System.Runtime.InteropServices.Guid("53e9841b-6309-4dc8-a554-9923a85743cc")]

    public class KPI_SeqEntity : EntityBase
    {
        protected String _SeqID = null;
        protected String _SeqCode = null;
        protected String _SeqName = null;
        protected String _SeqDesc = null;
        protected int _SeqIndex = int.MinValue;
        protected int _SeqIsValid = int.MinValue;
        protected String _SeqNote = null;
        protected String _SeqCreateTime = null;
        protected String _SeqModifyTime = null;

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

        public virtual String SeqCode
        {
            get
            {
                return this._SeqCode;
            }
            set
            {
                this._SeqCode = value;
            }
        }

        public virtual String SeqName
        {
            get
            {
                return this._SeqName;
            }
            set
            {
                this._SeqName = value;
            }
        }

        public virtual String SeqDesc
        {
            get
            {
                return this._SeqDesc;
            }
            set
            {
                this._SeqDesc = value;
            }
        }

        public virtual int SeqIndex
        {
            get
            {
                return this._SeqIndex;
            }
            set
            {
                this._SeqIndex = value;
            }
        }

        public virtual int SeqIsValid
        {
            get
            {
                return this._SeqIsValid;
            }
            set
            {
                this._SeqIsValid = value;
            }
        }
        
        public virtual String SeqNote
        {
            get
            {
                return this._SeqNote;
            }
            set
            {
                this._SeqNote = value;
            }
        }

        public virtual String SeqCreateTime
        {
            get
            {
                return this._SeqCreateTime;
            }
            set
            {
                this._SeqCreateTime = value;
            }
        }

        public virtual String SeqModifyTime
        {
            get
            {
                return this._SeqModifyTime;
            }
            set
            {
                this._SeqModifyTime = value;
            }
        }

        public override string InsertSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("insert into KPI_Seq(");

                if ((this.SeqID == null))
                {
                }
                else
                {
                    tmpBuild.Append("SeqID");
                    tmpBuild.Append(",");
                }

                if ((this.SeqCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("SeqCode");
                    tmpBuild.Append(",");
                }

                if ((this.SeqName == null))
                {
                }
                else
                {
                    tmpBuild.Append("SeqName");
                    tmpBuild.Append(",");
                }

                if ((this.SeqDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("SeqDesc");
                    tmpBuild.Append(",");
                }

                if ((this.SeqIndex == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("SeqIndex");
                    tmpBuild.Append(",");
                }

                if ((this.SeqIsValid == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("SeqIsValid");
                    tmpBuild.Append(",");
                }
                                
                if ((this.SeqNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("SeqNote");
                    tmpBuild.Append(",");
                }

                if ((this.SeqCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("SeqCreateTime");
                    tmpBuild.Append(",");
                }

                if ((this.SeqModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("SeqModifyTime");
                    tmpBuild.Append(",");
                }


                ///////////////////////////////////////////////////////////////////////////////////////


                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }

                tmpBuild.Append(") values(");
                if ((this.SeqID == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + SeqID + "'");
                    tmpBuild.Append(",");
                }

                if ((this.SeqCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + SeqCode + "'");
                    tmpBuild.Append(",");
                }
                                
                if ((this.SeqName == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + SeqName + "'");
                    tmpBuild.Append(",");
                }

                if ((this.SeqDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + SeqDesc + "'");
                    tmpBuild.Append(",");
                }


                if ((this.SeqIndex == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(SeqIndex.ToString());
                    tmpBuild.Append(",");
                }
                if ((this.SeqIsValid == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(SeqIsValid.ToString());
                    tmpBuild.Append(",");
                }
             
                if ((this.SeqNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + SeqNote + "'");
                    tmpBuild.Append(",");
                }

                if ((this.SeqCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + SeqCreateTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.SeqModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + SeqModifyTime + "'");
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
                tmpBuild.Append("update KPI_Seq set ");
                if ((this.SeqID == null))
                {
                }
                else
                {
                    tmpBuild.Append("SeqID='" + SeqID + "'");
                    tmpBuild.Append(",");
                }

                if ((this.SeqCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("SeqCode='" + SeqCode + "'");
                    tmpBuild.Append(",");
                }
                
                if ((this.SeqName == null))
                {
                }
                else
                {
                    tmpBuild.Append("SeqName='" + SeqName + "'");
                    tmpBuild.Append(",");
                }

                if ((this.SeqDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("SeqDesc='" + SeqDesc + "'");
                    tmpBuild.Append(",");
                }

                if ((this.SeqIndex == null))
                {
                }
                else
                {
                    tmpBuild.Append("SeqIndex=" + SeqIndex.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.SeqIsValid == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("SeqIsValid=" + SeqIsValid.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.SeqNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("SeqNote='" + SeqNote + "'");
                    tmpBuild.Append(",");
                }

                if ((this.SeqCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("SeqCreateTime='" + SeqCreateTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.SeqModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("SeqModifyTime='" + SeqModifyTime + "'");
                    tmpBuild.Append(",");
                }

                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }

                tmpBuild.Append(" where ");
                tmpBuild.Append("SeqID='" + SeqID + "'");

                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }


        public override string DeleteSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("delete KPI_Seq");
                tmpBuild.Append(" where ");
                tmpBuild.Append("SeqID='" + SeqID + "'");

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
                tmpBuild.Append("SeqID");
                tmpBuild.Append(",");
                tmpBuild.Append("SeqCode");
                tmpBuild.Append(",");
                tmpBuild.Append("SeqName");
                tmpBuild.Append(",");
                tmpBuild.Append("SeqDesc");
                tmpBuild.Append(",");
                tmpBuild.Append("SeqIndex");
                tmpBuild.Append(",");
                tmpBuild.Append("SeqIsValid");
                tmpBuild.Append(",");
                tmpBuild.Append("SeqNote");
                tmpBuild.Append(",");
                tmpBuild.Append("SeqCreateTime");
                tmpBuild.Append(",");
                tmpBuild.Append("SeqModifyTime");

                tmpBuild.Append(" from KPI_Seq");
                tmpBuild.Append(" where ");
                tmpBuild.Append("SeqID='" + SeqID + "'");

                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }

        public override bool DrToMember(System.Data.DataRow dr)
        {
            try
            {
                if (dr["SeqID"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.SeqID = dr["SeqID"].ToString();
                }

                if (dr["SeqCode"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.SeqCode = dr["SeqCode"].ToString();
                }
              
                if (dr["SeqName"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.SeqName = dr["SeqName"].ToString();
                }

                if (dr["SeqDesc"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.SeqDesc = dr["SeqDesc"].ToString();
                }

                if (dr["SeqIndex"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.SeqIndex = int.Parse(dr["SeqIndex"].ToString());
                }

                if (dr["SeqIsValid"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.SeqIsValid = int.Parse(dr["SeqIsValid"].ToString());
                }

                if (dr["SeqNote"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.SeqNote = dr["SeqNote"].ToString();
                }


                if (dr["SeqCreateTime"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.SeqCreateTime = dr["SeqCreateTime"].ToString();
                }


                if (dr["SeqModifyTime"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.SeqModifyTime = dr["SeqModifyTime"].ToString();
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
