
namespace SIS.DataEntity
{
    using System;
    using System.Xml;
    using System.Data;
    using System.Text;
    
    [System.Serializable()]
    [System.Runtime.InteropServices.Guid("d3a5e7b9-bc2d-4a76-865c-f47d9367b286")]
    public class EngunitEntity : EntityBase
    {
        protected String _EngunitID = null;
        protected String _EngunitName = null;
        protected String _EngunitDesc = null;
        protected String _EngunitNote = null;
        protected String _EngunitCreateTime = null;
        protected String _EngunitModifyTime = null;

        /// 该字段已经被作为Where的一部分
        public String EngunitID
        {
            get
            {
                return this._EngunitID;
            }
            set
            {
                this._EngunitID = value;
            }
        }
        public virtual String EngunitName
        {
            get
            {
                return this._EngunitName;
            }
            set
            {
                this._EngunitName = value;
            }
        }
        public virtual String EngunitDesc
        {
            get
            {
                return this._EngunitDesc;
            }
            set
            {
                this._EngunitDesc = value;
            }
        }

        public virtual String EngunitNote
        {
            get
            {
                return this._EngunitNote;
            }
            set
            {
                this._EngunitNote = value;
            }
        }

        public virtual String EngunitCreateTime
        {
            get
            {
                return this._EngunitCreateTime;
            }
            set
            {
                this._EngunitCreateTime = value;
            }
        }

        public virtual String EngunitModifyTime
        {
            get
            {
                return this._EngunitModifyTime;
            }
            set
            {
                this._EngunitModifyTime = value;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public override string InsertSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("insert into KPI_Engunit(");
                if ((this.EngunitID == null))
                {
                }
                else
                {
                    tmpBuild.Append("EngunitID");
                    tmpBuild.Append(",");
                }
                if ((this.EngunitName == null))
                {
                }
                else
                {
                    tmpBuild.Append("EngunitName");
                    tmpBuild.Append(",");
                }
                if ((this.EngunitDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("EngunitDesc");
                    tmpBuild.Append(",");
                }

                if ((this.EngunitNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("EngunitNote");
                    tmpBuild.Append(",");
                }

                if ((this.EngunitCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("EngunitCreateTime");
                    tmpBuild.Append(",");
                }

                if ((this.EngunitModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("EngunitModifyTime");
                    tmpBuild.Append(",");
                }

                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }
                tmpBuild.Append(") values(");

                if ((this.EngunitID == null))
                {
                }
                else
                {
                    tmpBuild.Append("'"+EngunitID+"'");
                    tmpBuild.Append(",");
                }
                if ((this.EngunitName == null))
                {
                }
                else
                {
                    tmpBuild.Append("'"+EngunitName+"'");
                    tmpBuild.Append(",");
                }
                if ((this.EngunitDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + EngunitDesc + "'");
                    tmpBuild.Append(",");
                }

                if ((this.EngunitNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + EngunitNote + "'");
                    tmpBuild.Append(",");
                }

                if ((this.EngunitCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + EngunitCreateTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.EngunitModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + EngunitModifyTime + "'");
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


        /// <summary>
        /// 
        /// </summary>
        public override string UpdateSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("update KPI_Engunit set ");
                if ((this.EngunitName == null))
                {
                }
                else
                {
                    tmpBuild.Append("EngunitName='"+EngunitName+"'");
                    tmpBuild.Append(",");
                }
                if ((this.EngunitDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("EngunitDesc='" + EngunitDesc + "'");
                    tmpBuild.Append(",");
                }

                if ((this.EngunitNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("EngunitNote='" + EngunitNote + "'");
                    tmpBuild.Append(",");
                }

                if ((this.EngunitCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("EngunitCreateTime='" + EngunitCreateTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.EngunitModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("EngunitModifyTime='" + EngunitModifyTime + "'");
                    tmpBuild.Append(",");
                }
                
                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }
                tmpBuild.Append(" where ");
                tmpBuild.Append("EngunitID='"+EngunitID+"'");
                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }
        public override string DeleteSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("delete KPI_Engunit");
                tmpBuild.Append(" where ");
                tmpBuild.Append("EngunitID='"+EngunitID+"'");
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
                tmpBuild.Append("EngunitID");
                tmpBuild.Append(",");
                tmpBuild.Append("EngunitName");
                tmpBuild.Append(",");
                tmpBuild.Append("EngunitDesc");
                tmpBuild.Append(",");
                tmpBuild.Append("EngunitNote");
                tmpBuild.Append(",");
                tmpBuild.Append("EngunitCreateTime");
                tmpBuild.Append(",");
                tmpBuild.Append("EngunitModifyTime");

                tmpBuild.Append(" from KPI_Engunit");
                tmpBuild.Append(" where ");
                tmpBuild.Append("EngunitID='"+EngunitID+"'");
                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }
        public override bool DrToMember(System.Data.DataRow dr)
        {
            try
            {
                if ((dr["EngunitID"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.EngunitID = dr["EngunitID"].ToString();
                }
                if ((dr["EngunitName"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.EngunitName = dr["EngunitName"].ToString();
                }
                if ((dr["EngunitDesc"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.EngunitDesc = dr["EngunitDesc"].ToString();
                }

                if ((dr["EngunitNote"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.EngunitNote = dr["EngunitNote"].ToString();
                }

                if ((dr["EngunitCreateTime"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.EngunitCreateTime = dr["EngunitCreateTime"].ToString();
                }
                if ((dr["EngunitModifyTime"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.EngunitModifyTime = dr["EngunitModifyTime"].ToString();
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
