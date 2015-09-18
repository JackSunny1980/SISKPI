
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
    public class KPI_PositionEntity : EntityBase
    {
        protected String _PositionID = null;
        protected String _PositionName = null;
        protected String _PositionDesc = null;
        protected double _PositionWeight = double.MinValue;
        protected String _PositionIsHand = null;
        protected String _PositionIsShift = null;
        protected String _PositionIsValid = null;
        protected String _PositionNote = null;
        protected String _PositionCreateTime = null;
        protected String _PositionModifyTime = null;


        /// 该字段已经被作为Where的一部分
        public virtual String PositionID
        {
            get
            {
                return this._PositionID;
            }
            set
            {
                this._PositionID = value;
            }
        }

        public virtual String PositionName
        {
            get
            {
                return this._PositionName;
            }
            set
            {
                this._PositionName = value;
            }
        }

        public virtual String PositionDesc
        {
            get
            {
                return this._PositionDesc;
            }
            set
            {
                this._PositionDesc = value;
            }
        }
        
        public virtual double PositionWeight
        {
            get
            {
                return this._PositionWeight;
            }
            set
            {
                this._PositionWeight = value;
            }
        }

        public virtual String PositionIsHand
        {
            get
            {
                return this._PositionIsHand;
            }
            set
            {
                this._PositionIsHand = value;
            }
        }

        public virtual String PositionIsShift
        {
            get
            {
                return this._PositionIsShift;
            }
            set
            {
                this._PositionIsShift = value;
            }
        }

        public virtual String PositionIsValid
        {
            get
            {
                return this._PositionIsValid;
            }
            set
            {
                this._PositionIsValid = value;
            }
        }
        public virtual String PositionNote
        {
            get
            {
                return this._PositionNote;
            }
            set
            {
                this._PositionNote = value;
            }
        }
        public virtual String PositionCreateTime
        {
            get
            {
                return this._PositionCreateTime;
            }
            set
            {
                this._PositionCreateTime = value;
            }
        }
        public virtual String PositionModifyTime
        {
            get
            {
                return this._PositionModifyTime;
            }
            set
            {
                this._PositionModifyTime = value;
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
                tmpBuild.Append("insert into KPI_Position(");
                if (this.PositionID == null)
                {
                }
                else
                {
                    tmpBuild.Append("PositionID");
                    tmpBuild.Append(",");
                }
                
                if (this.PositionName == null)
                {
                }
                else
                {
                    tmpBuild.Append("PositionName");
                    tmpBuild.Append(",");
                }

                if (this.PositionDesc == null)
                {
                }
                else
                {
                    tmpBuild.Append("PositionDesc");
                    tmpBuild.Append(",");
                }

                if (this.PositionWeight == double.MinValue)
                {
                }
                else
                {
                    tmpBuild.Append("PositionWeight");
                    tmpBuild.Append(",");
                }


                if (this.PositionIsHand == null)
                {
                }
                else
                {
                    tmpBuild.Append("PositionIsHand");
                    tmpBuild.Append(",");
                }

                if (this.PositionIsShift == null)
                {
                }
                else
                {
                    tmpBuild.Append("PositionIsShift");
                    tmpBuild.Append(",");
                }

                if (this.PositionIsValid == null)
                {
                }
                else
                {
                    tmpBuild.Append("PositionIsValid");
                    tmpBuild.Append(",");
                }

                if ((this.PositionNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("PositionNote");
                    tmpBuild.Append(",");
                }

                if (this.PositionCreateTime == null)
                {
                }
                else
                {
                    tmpBuild.Append("PositionCreateTime");
                    tmpBuild.Append(",");
                }

                if (this.PositionModifyTime == null)
                {
                }
                else
                {
                    tmpBuild.Append("PositionModifyTime");
                    tmpBuild.Append(",");
                }


                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }
                tmpBuild.Append(") values(");

                if (this.PositionID == null)
                {
                }
                else
                {
                    tmpBuild.Append("'" + PositionID + "'");
                    tmpBuild.Append(",");
                }
                if (this.PositionName == null)
                {
                }
                else
                {
                    tmpBuild.Append("'" + PositionName + "'");
                    tmpBuild.Append(",");
                }

                if (this.PositionDesc == null)
                {
                }
                else
                {
                    tmpBuild.Append("'" + PositionDesc + "'");
                    tmpBuild.Append(",");
                }
                if (this.PositionWeight == double.MinValue)
                {
                }
                else
                {
                    tmpBuild.Append("'" + PositionWeight + "'");
                    tmpBuild.Append(",");
                }

                if ((this.PositionIsHand == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + PositionIsHand + "'");
                    tmpBuild.Append(",");
                }

                if ((this.PositionIsShift == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + PositionIsShift + "'");
                    tmpBuild.Append(",");
                }

                if ((this.PositionIsValid == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + PositionIsValid + "'");
                    tmpBuild.Append(",");
                }
                
                if ((this.PositionNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + PositionNote + "'");
                    tmpBuild.Append(",");
                }

                if ((this.PositionCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + PositionCreateTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.PositionModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + PositionModifyTime + "'");
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
                tmpBuild.Append("update KPI_Position set ");
                               
                if ((this.PositionID == null))
                {
                }
                else
                {
                    tmpBuild.Append("PositionID='" + PositionID + "'");
                    tmpBuild.Append(",");
                }

                if ((this.PositionName == null))
                {
                }
                else
                {
                    tmpBuild.Append("PositionName='" + PositionName + "'");
                    tmpBuild.Append(",");
                }

                if ((this.PositionDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("PositionDesc='" + PositionDesc + "'");
                    tmpBuild.Append(",");
                }
                if ((this.PositionWeight == double.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("PositionWeight=" + PositionWeight.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.PositionIsHand == null))
                {
                }
                else
                {
                    tmpBuild.Append("PositionIsHand='" + PositionIsHand + "'");
                    tmpBuild.Append(",");
                }

                if ((this.PositionIsShift == null))
                {
                }
                else
                {
                    tmpBuild.Append("PositionIsShift='" + PositionIsShift + "'");
                    tmpBuild.Append(",");
                }

                if ((this.PositionIsValid == null))
                {
                }
                else
                {
                    tmpBuild.Append("PositionIsValid='" + PositionIsValid + "'");
                    tmpBuild.Append(",");
                }
                
                if ((this.PositionNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("PositionNote='" + PositionNote + "'");
                    tmpBuild.Append(",");
                }

                if ((this.PositionCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("PositionCreateTime='" + PositionCreateTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.PositionModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("PositionModifyTime='" + PositionModifyTime + "'");
                    tmpBuild.Append(",");
                }

                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }
                tmpBuild.Append(" where ");
                tmpBuild.Append("PositionID='" + PositionID + "'");

                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }

        public override string DeleteSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("delete KPI_Position");
                tmpBuild.Append(" where ");
                tmpBuild.Append("PositionID='" + PositionID + "'");
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
                tmpBuild.Append("PositionID");
                tmpBuild.Append(",");
                tmpBuild.Append("PositionName");
                tmpBuild.Append(",");
                tmpBuild.Append("PositionDesc");
                tmpBuild.Append(",");
                tmpBuild.Append("PositionWeight");
                tmpBuild.Append(",");
                tmpBuild.Append("PositionIsHand");
                tmpBuild.Append(",");
                tmpBuild.Append("PositionIsShift");
                tmpBuild.Append(",");
                tmpBuild.Append("PositionIsValid");
                tmpBuild.Append(",");
                tmpBuild.Append("PositionNote");
                tmpBuild.Append(",");
                tmpBuild.Append("PositionCreateTime");
                tmpBuild.Append(",");
                tmpBuild.Append("PositionModifyTime");

                /****/

                tmpBuild.Append(" from KPI_Position");
                tmpBuild.Append(" where ");
                tmpBuild.Append("PositionID='" + PositionID + "'");
                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }

        public override bool DrToMember(System.Data.DataRow dr)
        {
            try
            {
                if ((dr["PositionID"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.PositionID = dr["PositionID"].ToString();
                }
                if ((dr["PositionName"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.PositionName = dr["PositionName"].ToString();
                }
                if ((dr["PositionDesc"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.PositionDesc = dr["PositionDesc"].ToString();
                }

                if ((dr["PositionWeight"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.PositionWeight = double.Parse(dr["PositionWeight"].ToString());
                }
                if ((dr["PositionIsHand"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.PositionIsHand = dr["PositionIsHand"].ToString();
                }
                if ((dr["PositionIsShift"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.PositionIsShift = dr["PositionIsShift"].ToString();
                }
                if ((dr["PositionIsValid"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.PositionIsValid = dr["PositionIsValid"].ToString();
                }
                if ((dr["PositionNote"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.PositionNote = dr["PositionNote"].ToString();
                }
                if ((dr["PositionCreateTime"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.PositionCreateTime = dr["PositionCreateTime"].ToString();
                }
                if ((dr["PositionModifyTime"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.PositionModifyTime = dr["PositionModifyTime"].ToString();
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
