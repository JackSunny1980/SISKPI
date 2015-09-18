
///////////////////////////////////////////////////////////////////////////////////////////////////
//wuguanhui
//
//
///////////////////////////////////////////////////////////////////////////////////////////////////

namespace SIS.DataEntity
{
    using System;
    using System.Xml;
    using System.Data;
    using System.Text;
    using System.Collections.Generic;

    [System.Serializable()]
    [System.Runtime.InteropServices.Guid("811708b6-a974-478d-aac9-3be6e2437b18")]
    public class KPI_ShiftEntity : EntityBase
    {
        protected String _ShiftID = null;
        protected String _ShiftCode = null;
        protected String _ShiftName = null;
        protected String _ShiftDesc = null;
        protected String _ShiftIsValid = null;
        protected String _ShiftNote = null;
        protected String _ShiftCreateTime = null;
        protected String _ShiftModifyTime = null;


        /// 该字段已经被作为Where的一部分
        public virtual String ShiftID
        {
            get
            {
                return this._ShiftID;
            }
            set
            {
                this._ShiftID = value;
            }
        }
        public virtual String ShiftCode
        {
            get
            {
                return this._ShiftCode;
            }
            set
            {
                this._ShiftCode = value;
            }
        }

        public virtual String ShiftName
        {
            get
            {
                return this._ShiftName;
            }
            set
            {
                this._ShiftName = value;
            }
        }

        public virtual String ShiftDesc
        {
            get
            {
                return this._ShiftDesc;
            }
            set
            {
                this._ShiftDesc = value;
            }
        }

        public virtual String ShiftIsValid
        {
            get
            {
                return this._ShiftIsValid;
            }
            set
            {
                this._ShiftIsValid = value;
            }
        }
        public virtual String ShiftNote
        {
            get
            {
                return this._ShiftNote;
            }
            set
            {
                this._ShiftNote = value;
            }
        }
        public virtual String ShiftCreateTime
        {
            get
            {
                return this._ShiftCreateTime;
            }
            set
            {
                this._ShiftCreateTime = value;
            }
        }
        public virtual String ShiftModifyTime
        {
            get
            {
                return this._ShiftModifyTime;
            }
            set
            {
                this._ShiftModifyTime = value;
            }
        }


        /// <summary>
        /// ////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        public override string InsertSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("insert into KPI_Shift(");
                if (this.ShiftID == null)
                {
                }
                else
                {
                    tmpBuild.Append("ShiftID");
                    tmpBuild.Append(",");
                }
                if (this.ShiftCode == null)
                {
                }
                else
                {
                    tmpBuild.Append("ShiftCode");
                    tmpBuild.Append(",");
                }

                if (this.ShiftName == null)
                {
                }
                else
                {
                    tmpBuild.Append("ShiftName");
                    tmpBuild.Append(",");
                }

                if (this.ShiftDesc == null)
                {
                }
                else
                {
                    tmpBuild.Append("ShiftDesc");
                    tmpBuild.Append(",");
                }

                if (this.ShiftIsValid == null)
                {
                }
                else
                {
                    tmpBuild.Append("ShiftIsValid");
                    tmpBuild.Append(",");
                }

                if ((this.ShiftNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("ShiftNote");
                    tmpBuild.Append(",");
                }

                if (this.ShiftCreateTime == null)
                {
                }
                else
                {
                    tmpBuild.Append("ShiftCreateTime");
                    tmpBuild.Append(",");
                }

                if (this.ShiftModifyTime == null)
                {
                }
                else
                {
                    tmpBuild.Append("ShiftModifyTime");
                    tmpBuild.Append(",");
                }


                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }
                tmpBuild.Append(") values(");

                if (this.ShiftID == null)
                {
                }
                else
                {
                    tmpBuild.Append("'" + ShiftID + "'");
                    tmpBuild.Append(",");
                }

                if (this.ShiftCode == null)
                {
                }
                else
                {
                    tmpBuild.Append("'" + ShiftCode + "'");
                    tmpBuild.Append(",");
                }
                
                if (this.ShiftName == null)
                {
                }
                else
                {
                    tmpBuild.Append("'" + ShiftName + "'");
                    tmpBuild.Append(",");
                }

                if (this.ShiftDesc == null)
                {
                }
                else
                {
                    tmpBuild.Append("'" + ShiftDesc + "'");
                    tmpBuild.Append(",");
                }
                if ((this.ShiftIsValid == null))
                {
                }
                else
                {
                    tmpBuild.Append("'"+ShiftIsValid+"'");
                    tmpBuild.Append(",");
                }
                
                if ((this.ShiftNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + ShiftNote + "'");
                    tmpBuild.Append(",");
                }

                if ((this.ShiftCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + ShiftCreateTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.ShiftModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + ShiftModifyTime + "'");
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
                tmpBuild.Append("update KPI_Shift set ");

                if ((this.ShiftCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("ShiftCode='" + ShiftCode + "'");
                    tmpBuild.Append(",");
                }                             

                if ((this.ShiftName == null))
                {
                }
                else
                {
                    tmpBuild.Append("ShiftName='" + ShiftName + "'");
                    tmpBuild.Append(",");
                }

                if ((this.ShiftDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("ShiftDesc='" + ShiftDesc + "'");
                    tmpBuild.Append(",");
                }

                if ((this.ShiftIsValid == null))
                {
                }
                else
                {
                    tmpBuild.Append("ShiftIsValid='" + ShiftIsValid+"'");
                    tmpBuild.Append(",");
                }
                
                if ((this.ShiftNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("ShiftNote='" + ShiftNote + "'");
                    tmpBuild.Append(",");
                }

                if ((this.ShiftCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("ShiftCreateTime='" + ShiftCreateTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.ShiftModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("ShiftModifyTime='" + ShiftModifyTime + "'");
                    tmpBuild.Append(",");
                }

                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }
                tmpBuild.Append(" where ");
                tmpBuild.Append("ShiftID='" + ShiftID + "'");

                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }

        public override string DeleteSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("delete KPI_Shift");
                tmpBuild.Append(" where ");
                tmpBuild.Append("ShiftID='" + ShiftID + "'");
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
                tmpBuild.Append("ShiftID");
                tmpBuild.Append(",");
                tmpBuild.Append("ShiftCode");
                tmpBuild.Append(",");
                tmpBuild.Append("ShiftName");
                tmpBuild.Append(",");
                tmpBuild.Append("ShiftDesc");
                tmpBuild.Append(",");
                tmpBuild.Append("ShiftIsValid");
                tmpBuild.Append(",");
                tmpBuild.Append("ShiftNote");
                tmpBuild.Append(",");
                tmpBuild.Append("ShiftCreateTime");
                tmpBuild.Append(",");
                tmpBuild.Append("ShiftModifyTime");

                /****/

                tmpBuild.Append(" from KPI_Shift");
                tmpBuild.Append(" where ");
                tmpBuild.Append("ShiftID='" + ShiftID + "'");
                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }

        public override bool DrToMember(System.Data.DataRow dr)
        {
            try
            {
                if ((dr["ShiftID"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.ShiftID = dr["ShiftID"].ToString();
                }
                if ((dr["ShiftCode"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.ShiftCode = dr["ShiftCode"].ToString();
                }
                if ((dr["ShiftName"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.ShiftName = dr["ShiftName"].ToString();
                }
                if ((dr["ShiftDesc"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.ShiftDesc = dr["ShiftDesc"].ToString();
                }

                if ((dr["ShiftIsValid"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.ShiftIsValid = dr["ShiftIsValid"].ToString();
                }
                if ((dr["ShiftNote"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.ShiftNote = dr["ShiftNote"].ToString();
                }
                if ((dr["ShiftCreateTime"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.ShiftCreateTime = dr["ShiftCreateTime"].ToString();
                }
                if ((dr["ShiftModifyTime"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.ShiftModifyTime = dr["ShiftModifyTime"].ToString();
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
