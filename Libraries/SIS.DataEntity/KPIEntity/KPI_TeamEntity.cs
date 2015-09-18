
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
    public class KPI_TeamEntity : EntityBase
    {
        protected String _TeamID = null;
        protected String _PlantID = null;
        protected String _ShiftID = null;
        protected String _PersonID = null;
        protected String _PositionID = null;
        protected String _TeamPersonID = null;
        protected String _TeamNote = null;
        protected String _TeamCreateTime = null;
        protected String _TeamModifyTime = null;

        public string PlantName { get; set; }

        public string ShiftName { get; set; }

        public string PersonName { get; set; }

        public string PositionName { get; set; }

        public string TeamPersonName { get; set; }
        /// 该字段已经被作为Where的一部分
        public virtual String TeamID
        {
            get
            {
                return this._TeamID;
            }
            set
            {
                this._TeamID = value;
            }
        }
        public virtual String PlantID
        {
            get
            {
                return this._PlantID;
            }
            set
            {
                this._PlantID = value;
            }
        }
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
        public virtual String PersonID
        {
            get
            {
                return this._PersonID;
            }
            set
            {
                this._PersonID = value;
            }
        }
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

        public virtual String TeamPersonID
        {
            get
            {
                return this._TeamPersonID;
            }
            set
            {
                this._TeamPersonID = value;
            }
        }

        public virtual String TeamNote
        {
            get
            {
                return this._TeamNote;
            }
            set
            {
                this._TeamNote = value;
            }
        }
        public virtual String TeamCreateTime
        {
            get
            {
                return this._TeamCreateTime;
            }
            set
            {
                this._TeamCreateTime = value;
            }
        }
        public virtual String TeamModifyTime
        {
            get
            {
                return this._TeamModifyTime;
            }
            set
            {
                this._TeamModifyTime = value;
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
                tmpBuild.Append("insert into KPI_Team(");
                if (this.TeamID == null)
                {
                }
                else
                {
                    tmpBuild.Append("TeamID");
                    tmpBuild.Append(",");
                }
                if (this.PlantID == null)
                {
                }
                else
                {
                    tmpBuild.Append("PlantID");
                    tmpBuild.Append(",");
                }
                if (this.ShiftID == null)
                {
                }
                else
                {
                    tmpBuild.Append("ShiftID");
                    tmpBuild.Append(",");
                }
                if (this.PersonID == null)
                {
                }
                else
                {
                    tmpBuild.Append("PersonID");
                    tmpBuild.Append(",");
                }
                if (this.PositionID == null)
                {
                }
                else
                {
                    tmpBuild.Append("PositionID");
                    tmpBuild.Append(",");
                }
                if (this.TeamPersonID == null)
                {
                }
                else
                {
                    tmpBuild.Append("TeamPersonID");
                    tmpBuild.Append(",");
                }
                if ((this.TeamNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("TeamNote");
                    tmpBuild.Append(",");
                }

                if (this.TeamCreateTime == null)
                {
                }
                else
                {
                    tmpBuild.Append("TeamCreateTime");
                    tmpBuild.Append(",");
                }

                if (this.TeamModifyTime == null)
                {
                }
                else
                {
                    tmpBuild.Append("TeamModifyTime");
                    tmpBuild.Append(",");
                }


                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }
                tmpBuild.Append(") values(");

                if (this.TeamID == null)
                {
                }
                else
                {
                    tmpBuild.Append("'" + TeamID + "'");
                    tmpBuild.Append(",");
                }
                if (this.PlantID == null)
                {
                }
                else
                {
                    tmpBuild.Append("'" + PlantID + "'");
                    tmpBuild.Append(",");
                }
                if (this.ShiftID == null)
                {
                }
                else
                {
                    tmpBuild.Append("'" + ShiftID + "'");
                    tmpBuild.Append(",");
                }
                if (this.PersonID == null)
                {
                }
                else
                {
                    tmpBuild.Append("'" + PersonID + "'");
                    tmpBuild.Append(",");
                }


                if (this.PositionID == null)
                {
                }
                else
                {
                    tmpBuild.Append("'" + PositionID + "'");
                    tmpBuild.Append(",");
                }
                if (this.TeamPersonID == null)
                {
                }
                else
                {
                    tmpBuild.Append("'" + TeamPersonID + "'");
                    tmpBuild.Append(",");
                }
                if ((this.TeamNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + TeamNote + "'");
                    tmpBuild.Append(",");
                }

                if ((this.TeamCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + TeamCreateTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.TeamModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + TeamModifyTime + "'");
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
                tmpBuild.Append("update KPI_Team set ");


                if ((this.PlantID == null))
                {
                }
                else
                {
                    tmpBuild.Append("PlantID='" + PlantID + "'");
                    tmpBuild.Append(",");
                }
                if ((this.ShiftID == null))
                {
                }
                else
                {
                    tmpBuild.Append("ShiftID='" + ShiftID + "'");
                    tmpBuild.Append(",");
                }

                if ((this.PersonID == null))
                {
                }
                else
                {
                    tmpBuild.Append("PersonID='" + PersonID + "'");
                    tmpBuild.Append(",");
                }
                if ((this.PositionID == null))
                {
                }
                else
                {
                    tmpBuild.Append("PositionID='" + PositionID + "'");
                    tmpBuild.Append(",");
                }

                if ((this.TeamPersonID == null))
                {
                }
                else
                {
                    tmpBuild.Append("TeamPersonID='" + TeamPersonID + "'");
                    tmpBuild.Append(",");
                }   
                if ((this.TeamNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("TeamNote='" + TeamNote + "'");
                    tmpBuild.Append(",");
                }

                if ((this.TeamCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("TeamCreateTime='" + TeamCreateTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.TeamModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("TeamModifyTime='" + TeamModifyTime + "'");
                    tmpBuild.Append(",");
                }

                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }
                tmpBuild.Append(" where ");
                tmpBuild.Append("TeamID='" + TeamID + "'");

                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }

        public override string DeleteSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("delete KPI_Team");
                tmpBuild.Append(" where ");
                tmpBuild.Append("TeamID='" + TeamID + "'");
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
                tmpBuild.Append("TeamID");
                tmpBuild.Append(",");
                tmpBuild.Append("PlantID");
                tmpBuild.Append(",");
                tmpBuild.Append("ShiftID");
                tmpBuild.Append(",");
                tmpBuild.Append("PersonID");
                tmpBuild.Append(",");
                tmpBuild.Append("PositionID");
                tmpBuild.Append(",");
                tmpBuild.Append("TeamPersonID");
                tmpBuild.Append(",");
                tmpBuild.Append("TeamNote");
                tmpBuild.Append(",");
                tmpBuild.Append("TeamCreateTime");
                tmpBuild.Append(",");
                tmpBuild.Append("TeamModifyTime");

                /****/

                tmpBuild.Append(" from KPI_Team");
                tmpBuild.Append(" where ");
                tmpBuild.Append("TeamID='" + TeamID + "'");
                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }

        public override bool DrToMember(System.Data.DataRow dr)
        {
            try
            {
                if ((dr["TeamID"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.TeamID = dr["TeamID"].ToString();
                }
                if ((dr["PlantID"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.PlantID = dr["PlantID"].ToString();
                }
                if ((dr["ShiftID"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.ShiftID = dr["ShiftID"].ToString();
                }
                if ((dr["PersonID"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.PersonID = dr["PersonID"].ToString();
                }
                if ((dr["PositionID"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.PositionID = dr["PositionID"].ToString();
                }
                if ((dr["TeamPersonID"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.TeamPersonID = dr["TeamPersonID"].ToString();
                }
                if ((dr["TeamNote"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.TeamNote = dr["TeamNote"].ToString();
                }
                if ((dr["TeamCreateTime"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.TeamCreateTime = dr["TeamCreateTime"].ToString();
                }
                if ((dr["TeamModifyTime"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.TeamModifyTime = dr["TeamModifyTime"].ToString();
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
