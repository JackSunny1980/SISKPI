
namespace SIS.DataEntity
{
    using System;
    using System.Xml;
    using System.Data;
    using System.Text;
    
    [System.Serializable()]
    [System.Runtime.InteropServices.Guid("53e9841b-6409-4dc8-a554-9923a85743cc")]

    public class GroupEntity : EntityBase
    {
        protected String _GroupID = null;
        protected String _GroupCode = null;
        protected String _GroupName = null;
        protected String _GroupDesc = null;
        protected int _GroupIsValid = int.MinValue;
        protected String _GroupNote = null;
        protected String _GroupCreateTime = null;
        protected String _GroupModifyTime = null;

        public virtual String GroupID
        {
            get
            {
                return this._GroupID;
            }
            set
            {
                this._GroupID = value;
            }
        }

        public virtual String GroupCode
        {
            get
            {
                return this._GroupCode;
            }
            set
            {
                this._GroupCode = value;
            }
        } 

        public virtual String GroupName
        {
            get
            {
                return this._GroupName;
            }
            set
            {
                this._GroupName = value;
            }
        }

        public virtual String GroupDesc
        {
            get
            {
                return this._GroupDesc;
            }
            set
            {
                this._GroupDesc = value;
            }
        }

        public virtual int GroupIsValid
        {
            get
            {
                return this._GroupIsValid;
            }
            set
            {
                this._GroupIsValid = value;
            }
        }

        public virtual String GroupNote
        {
            get
            {
                return this._GroupNote;
            }
            set
            {
                this._GroupNote = value;
            }
        }

        public virtual String GroupCreateTime
        {
            get
            {
                return this._GroupCreateTime;
            }
            set
            {
                this._GroupCreateTime = value;
            }
        }

        public virtual String GroupModifyTime
        {
            get
            {
                return this._GroupModifyTime;
            }
            set
            {
                this._GroupModifyTime = value;
            }
        }

        public override string InsertSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("insert into KPI_Group(");

                if ((this.GroupID == null))
                {
                }
                else
                {
                    tmpBuild.Append("GroupID");
                    tmpBuild.Append(",");
                }

                if ((this.GroupCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("GroupCode");
                    tmpBuild.Append(",");
                }
                
                if ((this.GroupName == null))
                {
                }
                else
                {
                    tmpBuild.Append("GroupName");
                    tmpBuild.Append(",");
                }

                if ((this.GroupDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("GroupDesc");
                    tmpBuild.Append(",");
                }

                if ((this.GroupIsValid == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("GroupIsValid");
                    tmpBuild.Append(",");
                }
                
                if ((this.GroupNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("GroupNote");
                    tmpBuild.Append(",");
                }

                if ((this.GroupCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("GroupCreateTime");
                    tmpBuild.Append(",");
                }

                if ((this.GroupModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("GroupModifyTime");
                    tmpBuild.Append(",");
                }


                ///////////////////////////////////////////////////////////////////////////////////////


                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }

                tmpBuild.Append(") values(");
                if ((this.GroupID == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + GroupID + "'");
                    tmpBuild.Append(",");
                }
                
                if ((this.GroupCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + GroupCode + "'");
                    tmpBuild.Append(",");
                }
                
                if ((this.GroupName == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + GroupName + "'");
                    tmpBuild.Append(",");
                }
               
                if ((this.GroupDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + GroupDesc + "'");
                    tmpBuild.Append(",");
                }
             
                if ((this.GroupIsValid == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(GroupIsValid.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.GroupNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + GroupNote + "'");
                    tmpBuild.Append(",");
                }

                if ((this.GroupCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + GroupCreateTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.GroupModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + GroupModifyTime + "'");
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
                tmpBuild.Append("update KPI_Group set ");
                if ((this.GroupID == null))
                {
                }
                else
                {
                    tmpBuild.Append("GroupID='" + GroupID + "'");
                    tmpBuild.Append(",");
                }
                
                if ((this.GroupCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("GroupCode='" + GroupCode + "'");
                    tmpBuild.Append(",");
                }                

                if ((this.GroupName == null))
                {
                }
                else
                {
                    tmpBuild.Append("GroupName='" + GroupName + "'");
                    tmpBuild.Append(",");
                }
                if ((this.GroupDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("GroupDesc='" + GroupDesc + "'");
                    tmpBuild.Append(",");
                }

                if ((this.GroupIsValid == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("GroupIsValid=" + GroupIsValid.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.GroupNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("GroupNote='" + GroupNote + "'");
                    tmpBuild.Append(",");
                }

                if ((this.GroupCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("GroupCreateTime='" + GroupCreateTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.GroupModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("GroupModifyTime='" + GroupModifyTime + "'");
                    tmpBuild.Append(",");
                }

                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }

                tmpBuild.Append(" where ");
                tmpBuild.Append("GroupID='" + GroupID + "'");

                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }


        public override string DeleteSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("delete KPI_Group");
                tmpBuild.Append(" where ");
                tmpBuild.Append("GroupID='" + GroupID + "'");

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
                tmpBuild.Append("GroupID");
                tmpBuild.Append(",");
                tmpBuild.Append("GroupCode");
                tmpBuild.Append(",");
                tmpBuild.Append("GroupName");
                tmpBuild.Append(",");
                tmpBuild.Append("GroupDesc");
                tmpBuild.Append(",");
                tmpBuild.Append("GroupIsValid");
                tmpBuild.Append(",");
                tmpBuild.Append("GroupNote");
                tmpBuild.Append(",");
                tmpBuild.Append("GroupCreateTime");
                tmpBuild.Append(",");
                tmpBuild.Append("GroupModifyTime");

                tmpBuild.Append(" from KPI_Group");
                tmpBuild.Append(" where ");
                tmpBuild.Append("GroupID='" + GroupID + "'");

                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }

        public override bool DrToMember(System.Data.DataRow dr)
        {
            try
            {
                if (dr["GroupID"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.GroupID = dr["GroupID"].ToString();
                }

                if (dr["GroupCode"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.GroupCode = dr["GroupCode"].ToString();
                }     
                            
                if (dr["GroupName"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.GroupName = dr["GroupName"].ToString();
                }     

                if (dr["GroupDesc"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.GroupDesc = dr["GroupDesc"].ToString();
                }

                if (dr["GroupIsValid"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.GroupIsValid = int.Parse(dr["GroupIsValid"].ToString());
                }
                
                if (dr["GroupNote"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.GroupNote = dr["GroupNote"].ToString();
                }


                if (dr["GroupCreateTime"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.GroupCreateTime = dr["GroupCreateTime"].ToString();
                }


                if (dr["GroupModifyTime"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.GroupModifyTime = dr["GroupModifyTime"].ToString();
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
