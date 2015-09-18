
namespace SIS.DataEntity
{
    using System;
    using System.Xml;
    using System.Data;
    using System.Text;
    
    [System.Serializable()]
    [System.Runtime.InteropServices.Guid("53e9841b-6309-4dc8-a554-9923a85743cc")]

    public class KPI_PlantEntity : EntityBase
    {
        protected String _PlantID = null;
        protected String _PlantCode = null;
        protected String _PlantName = null;
        protected String _PlantDesc = null;
        protected int _PlantIndex = int.MinValue;
        protected int _PlantIsValid = int.MinValue;
        protected String _PlantAddress = null;
        protected String _PlantNote = null;
        protected String _PlantCreateTime = null;
        protected String _PlantModifyTime = null;

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

        public virtual String PlantCode
        {
            get
            {
                return this._PlantCode;
            }
            set
            {
                this._PlantCode = value;
            }
        }

        public virtual String PlantName
        {
            get
            {
                return this._PlantName;
            }
            set
            {
                this._PlantName = value;
            }
        }

        public virtual String PlantDesc
        {
            get
            {
                return this._PlantDesc;
            }
            set
            {
                this._PlantDesc = value;
            }
        }

        public virtual int PlantIndex
        {
            get
            {
                return this._PlantIndex;
            }
            set
            {
                this._PlantIndex = value;
            }
        }

        public virtual int PlantIsValid
        {
            get
            {
                return this._PlantIsValid;
            }
            set
            {
                this._PlantIsValid = value;
            }
        }

        public virtual String PlantAddress
        {
            get
            {
                return this._PlantAddress;
            }
            set
            {
                this._PlantAddress = value;
            }
        }

        public virtual String PlantNote
        {
            get
            {
                return this._PlantNote;
            }
            set
            {
                this._PlantNote = value;
            }
        }

        public virtual String PlantCreateTime
        {
            get
            {
                return this._PlantCreateTime;
            }
            set
            {
                this._PlantCreateTime = value;
            }
        }

        public virtual String PlantModifyTime
        {
            get
            {
                return this._PlantModifyTime;
            }
            set
            {
                this._PlantModifyTime = value;
            }
        }

        public override string InsertSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("insert into KPI_Plant(");

                if ((this.PlantID == null))
                {
                }
                else
                {
                    tmpBuild.Append("PlantID");
                    tmpBuild.Append(",");
                }

                if ((this.PlantCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("PlantCode");
                    tmpBuild.Append(",");
                }

                if ((this.PlantName == null))
                {
                }
                else
                {
                    tmpBuild.Append("PlantName");
                    tmpBuild.Append(",");
                }

                if ((this.PlantDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("PlantDesc");
                    tmpBuild.Append(",");
                }

                if ((this.PlantIndex == null))
                {
                }
                else
                {
                    tmpBuild.Append("PlantIndex");
                    tmpBuild.Append(",");
                }

                if ((this.PlantIsValid == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("PlantIsValid");
                    tmpBuild.Append(",");
                }

                if ((this.PlantAddress == null))
                {
                }
                else
                {
                    tmpBuild.Append("PlantAddress");
                    tmpBuild.Append(",");
                }
                
                if ((this.PlantNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("PlantNote");
                    tmpBuild.Append(",");
                }

                if ((this.PlantCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("PlantCreateTime");
                    tmpBuild.Append(",");
                }

                if ((this.PlantModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("PlantModifyTime");
                    tmpBuild.Append(",");
                }


                ///////////////////////////////////////////////////////////////////////////////////////


                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }

                tmpBuild.Append(") values(");
                if ((this.PlantID == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + PlantID + "'");
                    tmpBuild.Append(",");
                }

                if ((this.PlantCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + PlantCode + "'");
                    tmpBuild.Append(",");
                }
                                
                if ((this.PlantName == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + PlantName + "'");
                    tmpBuild.Append(",");
                }

                if ((this.PlantDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + PlantDesc + "'");
                    tmpBuild.Append(",");
                }


                if ((this.PlantIndex == null))
                {
                }
                else
                {
                    tmpBuild.Append(PlantIndex.ToString());
                    tmpBuild.Append(",");
                }
                if ((this.PlantIsValid == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(PlantIsValid.ToString());
                    tmpBuild.Append(",");
                }
             
                if ((this.PlantAddress == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + PlantAddress + "'");
                    tmpBuild.Append(",");
                }

                if ((this.PlantNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + PlantNote + "'");
                    tmpBuild.Append(",");
                }

                if ((this.PlantCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + PlantCreateTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.PlantModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + PlantModifyTime + "'");
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
                tmpBuild.Append("update KPI_Plant set ");
                if ((this.PlantID == null))
                {
                }
                else
                {
                    tmpBuild.Append("PlantID='" + PlantID + "'");
                    tmpBuild.Append(",");
                }

                if ((this.PlantCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("PlantCode='" + PlantCode + "'");
                    tmpBuild.Append(",");
                }
                
                if ((this.PlantName == null))
                {
                }
                else
                {
                    tmpBuild.Append("PlantName='" + PlantName + "'");
                    tmpBuild.Append(",");
                }

                if ((this.PlantDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("PlantDesc='" + PlantDesc + "'");
                    tmpBuild.Append(",");
                }

                if ((this.PlantIndex == null))
                {
                }
                else
                {
                    tmpBuild.Append("PlantIndex=" + PlantIndex.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.PlantIsValid == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("PlantIsValid=" + PlantIsValid.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.PlantAddress == null))
                {
                }
                else
                {
                    tmpBuild.Append("PlantAddress='" + PlantAddress + "'");
                    tmpBuild.Append(",");
                }

                if ((this.PlantNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("PlantNote='" + PlantNote + "'");
                    tmpBuild.Append(",");
                }

                if ((this.PlantCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("PlantCreateTime='" + PlantCreateTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.PlantModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("PlantModifyTime='" + PlantModifyTime + "'");
                    tmpBuild.Append(",");
                }

                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }

                tmpBuild.Append(" where ");
                tmpBuild.Append("PlantID='" + PlantID + "'");

                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }


        public override string DeleteSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("delete KPI_Plant");
                tmpBuild.Append(" where ");
                tmpBuild.Append("PlantID='" + PlantID + "'");

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
                tmpBuild.Append("PlantID");
                tmpBuild.Append(",");
                tmpBuild.Append("PlantCode");
                tmpBuild.Append(",");
                tmpBuild.Append("PlantName");
                tmpBuild.Append(",");
                tmpBuild.Append("PlantDesc");
                tmpBuild.Append(",");
                tmpBuild.Append("PlantIndex");
                tmpBuild.Append(",");
                tmpBuild.Append("PlantIsValid");
                tmpBuild.Append(",");
                tmpBuild.Append("PlantAddress");
                tmpBuild.Append(",");
                tmpBuild.Append("PlantNote");
                tmpBuild.Append(",");
                tmpBuild.Append("PlantCreateTime");
                tmpBuild.Append(",");
                tmpBuild.Append("PlantModifyTime");

                tmpBuild.Append(" from KPI_Plant");
                tmpBuild.Append(" where ");
                tmpBuild.Append("PlantID='" + PlantID + "'");

                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }

        public override bool DrToMember(System.Data.DataRow dr)
        {
            try
            {
                if (dr["PlantID"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.PlantID = dr["PlantID"].ToString();
                }

                if (dr["PlantCode"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.PlantCode = dr["PlantCode"].ToString();
                }
              
                if (dr["PlantName"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.PlantName = dr["PlantName"].ToString();
                }

                if (dr["PlantDesc"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.PlantDesc = dr["PlantDesc"].ToString();
                }

                if (dr["PlantIndex"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.PlantIndex = int.Parse(dr["PlantIndex"].ToString());
                }

                if (dr["PlantIsValid"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.PlantIsValid = int.Parse(dr["PlantIsValid"].ToString());
                }

                if (dr["PlantAddress"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.PlantAddress = dr["PlantAddress"].ToString();
                }
                
                if (dr["PlantNote"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.PlantNote = dr["PlantNote"].ToString();
                }


                if (dr["PlantCreateTime"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.PlantCreateTime = dr["PlantCreateTime"].ToString();
                }


                if (dr["PlantModifyTime"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.PlantModifyTime = dr["PlantModifyTime"].ToString();
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
