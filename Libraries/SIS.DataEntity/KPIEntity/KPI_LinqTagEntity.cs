
namespace SIS.DataEntity
{
    using System;
    using System.Xml;
    using System.Data;
    using System.Text;
    
    [System.Serializable()]
    [System.Runtime.InteropServices.Guid("50e9801b-6409-4dc8-a554-9923a85743dc")]

    public class LinqTagEntity : EntityBase
    {
        protected String _TagID = null;
        protected String _LinqID = null;
        protected String _UnitID = null;
        protected String _UnitName = null;
        protected String _ECID = null;
        protected String _ECName = null;

        public virtual String TagID
        {
            get
            {
                return this._TagID;
            }
            set
            {
                this._TagID = value;
            }
        }

        public virtual String LinqID
        {
            get
            {
                return this._LinqID;
            }
            set
            {
                this._LinqID = value;
            }
        }

        public virtual String UnitID
        {
            get
            {
                return this._UnitID;
            }
            set
            {
                this._UnitID = value;
            }
        }

        public virtual String UnitName
        {
            get
            {
                return this._UnitName;
            }
            set
            {
                this._UnitName = value;
            }
        }

        public virtual String ECID
        {
            get
            {
                return this._ECID;
            }
            set
            {
                this._ECID = value;
            }
        }

        public virtual String ECName
        {
            get
            {
                return this._ECName;
            }
            set
            {
                this._ECName = value;
            }
        }

        public override string InsertSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("insert into KPI_LinqTag(");

                if ((this.TagID == null))
                {
                }
                else
                {
                    tmpBuild.Append("TagID");
                    tmpBuild.Append(",");
                }

                if ((this.LinqID == null))
                {
                }
                else
                {
                    tmpBuild.Append("LinqID");
                    tmpBuild.Append(",");
                }     
       
                if ((this.UnitID == null))
                {
                }
                else
                {
                    tmpBuild.Append("UnitID");
                    tmpBuild.Append(",");
                }

                if ((this.UnitName == null))
                {
                }
                else
                {
                    tmpBuild.Append("UnitName");
                    tmpBuild.Append(",");
                }

                if ((this.ECID == null))
                {
                }
                else
                {
                    tmpBuild.Append("ECID");
                    tmpBuild.Append(",");
                }    

                if ((this.ECName == null))
                {
                }
                else
                {
                    tmpBuild.Append("ECName");
                    tmpBuild.Append(",");
                }

                ///////////////////////////////////////////////////////////////////////////////////////


                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }

                tmpBuild.Append(") values(");
                if ((this.TagID == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + TagID + "'");
                    tmpBuild.Append(",");
                }
                
                if ((this.LinqID == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + LinqID + "'");
                    tmpBuild.Append(",");
                }

                if ((this.UnitID == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + UnitID + "'");
                    tmpBuild.Append(",");
                }
               
                if ((this.UnitName == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + UnitName + "'");
                    tmpBuild.Append(",");
                }

                if ((this.ECID == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + ECID + "'");
                    tmpBuild.Append(",");
                }

                if ((this.ECName == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + ECName + "'");
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
                tmpBuild.Append("update KPI_LinqTag set ");
                if ((this.TagID == null))
                {
                }
                else
                {
                    tmpBuild.Append("TagID='" + TagID + "'");
                    tmpBuild.Append(",");
                }

                if ((this.LinqID == null))
                {
                }
                else
                {
                    tmpBuild.Append("LinqID='" + LinqID + "'");
                    tmpBuild.Append(",");
                }

                if ((this.UnitID == null))
                {
                }
                else
                {
                    tmpBuild.Append("UnitID='" + UnitID + "'");
                    tmpBuild.Append(",");
                }
              
                if ((this.UnitName == null))
                {
                }
                else
                {
                    tmpBuild.Append("UnitName='" + UnitName + "'");
                    tmpBuild.Append(",");
                }

                if ((this.ECID == null))
                {
                }
                else
                {
                    tmpBuild.Append("ECID='" + ECID + "'");
                    tmpBuild.Append(",");
                }

                if ((this.ECName == null))
                {
                }
                else
                {
                    tmpBuild.Append("ECName='" + ECName + "'");
                    tmpBuild.Append(",");
                }
                
                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }

                tmpBuild.Append(" where ");
                tmpBuild.Append("TagID='" + TagID + "'");

                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }

        public override string DeleteSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("delete KPI_LinqTag");
                tmpBuild.Append(" where ");
                tmpBuild.Append("TagID='" + TagID + "'");

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
                tmpBuild.Append("TagID");
                tmpBuild.Append(",");
                tmpBuild.Append("LinqID");
                tmpBuild.Append(",");
                tmpBuild.Append("UnitID");
                tmpBuild.Append(",");
                tmpBuild.Append("UnitName");
                tmpBuild.Append(",");
                tmpBuild.Append("ECID");
                tmpBuild.Append(",");
                tmpBuild.Append("ECName");

                tmpBuild.Append(" from KPI_LinqTag");
                tmpBuild.Append(" where ");
                tmpBuild.Append("TagID='" + TagID + "'");

                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }

        public override bool DrToMember(System.Data.DataRow dr)
        {
            try
            {
                if (dr["TagID"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.TagID = dr["TagID"].ToString();
                }
              
                if (dr["LinqID"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.LinqID = dr["LinqID"].ToString();
                }

                if (dr["UnitID"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.UnitID = dr["UnitID"].ToString();
                }

                if (dr["UnitName"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.UnitName = dr["UnitName"].ToString();
                }

                if (dr["ECID"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.ECID = dr["ECID"].ToString();
                }

                if (dr["ECName"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.ECName = dr["ECName"].ToString();
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
