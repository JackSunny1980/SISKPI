
namespace SIS.DataEntity
{
    using System;
    using System.Xml;
    using System.Data;
    using System.Text;
    
    [System.Serializable()]
    [System.Runtime.InteropServices.Guid("53e9801b-6009-4dc8-a554-9923a85743cc")]

    public class KPI_WebEntity : EntityBase
    {
        protected String _WebID = null;
        protected String _WebCode = null;  //英文
        protected String _WebDesc = null;
        protected int _WebType = int.MinValue;
        protected String _WebNote = null;
        protected String _WebCreateTime = null;
        protected String _WebModifyTime = null;

        public virtual String WebID
        {
            get
            {
                return this._WebID;
            }
            set
            {
                this._WebID = value;
            }
        }
        
        public virtual String WebCode
        {
            get
            {
                return this._WebCode;
            }
            set
            {
                this._WebCode = value;
            }
        }

        public virtual String WebDesc
        {
            get
            {
                return this._WebDesc;
            }
            set
            {
                this._WebDesc = value;
            }
        }

        public virtual int WebType
        {
            get
            {
                return this._WebType;
            }
            set
            {
                this._WebType = value;
            }
        }

        public virtual String WebNote
        {
            get
            {
                return this._WebNote;
            }
            set
            {
                this._WebNote = value;
            }
        }

        public virtual String WebCreateTime
        {
            get
            {
                return this._WebCreateTime;
            }
            set
            {
                this._WebCreateTime = value;
            }
        }

        public virtual String WebModifyTime
        {
            get
            {
                return this._WebModifyTime;
            }
            set
            {
                this._WebModifyTime = value;
            }
        }

        public override string InsertSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("insert into KPI_Web(");

                if ((this.WebID == null))
                {
                }
                else
                {
                    tmpBuild.Append("WebID");
                    tmpBuild.Append(",");
                }
                
                if ((this.WebCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("WebCode");
                    tmpBuild.Append(",");
                }

                if ((this.WebDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("WebDesc");
                    tmpBuild.Append(",");
                }

                if ((this.WebType == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("WebType");
                    tmpBuild.Append(",");
                }
                                                               
                if ((this.WebNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("WebNote");
                    tmpBuild.Append(",");
                }

                if ((this.WebCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("WebCreateTime");
                    tmpBuild.Append(",");
                }

                if ((this.WebModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("WebModifyTime");
                    tmpBuild.Append(",");
                }


                ///////////////////////////////////////////////////////////////////////////////////////


                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }

                tmpBuild.Append(") values(");

                if ((this.WebID == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + WebID + "'");
                    tmpBuild.Append(",");
                }
                
                if ((this.WebCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + WebCode + "'");
                    tmpBuild.Append(",");
                }

                if ((this.WebDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + WebDesc + "'");
                    tmpBuild.Append(",");
                }

                if ((this.WebType == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(WebType.ToString());
                    tmpBuild.Append(",");
                }
                
                if ((this.WebNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + WebNote + "'");
                    tmpBuild.Append(",");
                }

                if ((this.WebCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + WebCreateTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.WebModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + WebModifyTime + "'");
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
                tmpBuild.Append("update KPI_Web set ");
                if ((this.WebID == null))
                {
                }
                else
                {
                    tmpBuild.Append("WebID='" + WebID + "'");
                    tmpBuild.Append(",");
                }
                
                if ((this.WebCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("WebCode='" + WebCode + "'");
                    tmpBuild.Append(",");
                }

                if ((this.WebDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("WebDesc='" + WebDesc + "'");
                    tmpBuild.Append(",");
                }

   
                if ((this.WebType == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("WebType=" + WebType.ToString());
                    tmpBuild.Append(",");
                }
                
                if ((this.WebNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("WebNote='" + WebNote + "'");
                    tmpBuild.Append(",");
                }

                if ((this.WebCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("WebCreateTime='" + WebCreateTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.WebModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("WebModifyTime='" + WebModifyTime + "'");
                    tmpBuild.Append(",");
                }

                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }

                tmpBuild.Append(" where ");
                tmpBuild.Append("WebID='" + WebID + "'");

                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }


        public override string DeleteSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("delete KPI_Web");
                tmpBuild.Append(" where ");
                tmpBuild.Append("WebID='" + WebID + "'");

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
                tmpBuild.Append("WebID");
                tmpBuild.Append(",");
                tmpBuild.Append("WebCode");
                tmpBuild.Append(",");
                tmpBuild.Append("WebDesc");
                tmpBuild.Append(",");
                tmpBuild.Append("WebType");
                tmpBuild.Append(",");
                tmpBuild.Append("WebNote");
                tmpBuild.Append(",");
                tmpBuild.Append("WebCreateTime");
                tmpBuild.Append(",");
                tmpBuild.Append("WebModifyTime");

                tmpBuild.Append(" from KPI_Web");
                tmpBuild.Append(" where ");
                tmpBuild.Append("WebID='" + WebID + "'");

                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }

        public override bool DrToMember(System.Data.DataRow dr)
        {
            try
            {
                if (dr["WebID"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.WebID = dr["WebID"].ToString();
                }

                if (dr["WebCode"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.WebCode = dr["WebCode"].ToString();
                }

                if (dr["WebDesc"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.WebDesc = dr["WebDesc"].ToString();
                }
                if (dr["WebType"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.WebType = int.Parse(dr["WebType"].ToString());
                }
                
                if (dr["WebNote"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.WebNote = dr["WebNote"].ToString();
                }

                if (dr["WebCreateTime"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.WebCreateTime = dr["WebCreateTime"].ToString();
                }


                if (dr["WebModifyTime"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.WebModifyTime = dr["WebModifyTime"].ToString();
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
