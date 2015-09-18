
namespace SIS.DataEntity
{
    using System;
    using System.Xml;
    using System.Data;
    using System.Text;
    
    [System.Serializable()]
    [System.Runtime.InteropServices.Guid("50e9801b-6409-4dc8-b554-9923a85743dc")]

    public class LinqEntity : EntityBase
    {
        protected String _LinqID = null;
        protected String _LinqName = null;
        protected String _LinqDesc = null;
        protected String _LinqEngunit = null;
        protected int _LinqIndex = int.MinValue;
        protected int _LinqIsValid = int.MinValue; 
        protected String _LinqNote = null;
        protected String _LinqCreateTime = null;
        protected String _LinqModifyTime = null;

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
        
        public virtual String LinqName
        {
            get
            {
                return this._LinqName;
            }
            set
            {
                this._LinqName = value;
            }
        }

        public virtual String LinqDesc
        {
            get
            {
                return this._LinqDesc;
            }
            set
            {
                this._LinqDesc = value;
            }
        }

        public virtual String LinqEngunit
        {
            get
            {
                return this._LinqEngunit;
            }
            set
            {
                this._LinqEngunit = value;
            }
        }

        public virtual int LinqIndex
        {
            get
            {
                return this._LinqIndex;
            }
            set
            {
                this._LinqIndex = value;
            }
        }


        public virtual int LinqIsValid
        {
            get
            {
                return this._LinqIsValid;
            }
            set
            {
                this._LinqIsValid = value;
            }
        }     

        public virtual String LinqNote
        {
            get
            {
                return this._LinqNote;
            }
            set
            {
                this._LinqNote = value;
            }
        }

        public virtual String LinqCreateTime
        {
            get
            {
                return this._LinqCreateTime;
            }
            set
            {
                this._LinqCreateTime = value;
            }
        }

        public virtual String LinqModifyTime
        {
            get
            {
                return this._LinqModifyTime;
            }
            set
            {
                this._LinqModifyTime = value;
            }
        }

        public override string InsertSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("insert into KPI_Linq(");

                if ((this.LinqID == null))
                {
                }
                else
                {
                    tmpBuild.Append("LinqID");
                    tmpBuild.Append(",");
                }
                                
                if ((this.LinqName == null))
                {
                }
                else
                {
                    tmpBuild.Append("LinqName");
                    tmpBuild.Append(",");
                }

                if ((this.LinqDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("LinqDesc");
                    tmpBuild.Append(",");
                }

                if ((this.LinqEngunit == null))
                {
                }
                else
                {
                    tmpBuild.Append("LinqEngunit");
                    tmpBuild.Append(",");
                }

                if ((this.LinqIndex == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("LinqIndex");
                    tmpBuild.Append(",");
                }

                if ((this.LinqIsValid == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("LinqIsValid");
                    tmpBuild.Append(",");
                }
                
                if ((this.LinqNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("LinqNote");
                    tmpBuild.Append(",");
                }

                if ((this.LinqCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("LinqCreateTime");
                    tmpBuild.Append(",");
                }

                if ((this.LinqModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("LinqModifyTime");
                    tmpBuild.Append(",");
                }


                ///////////////////////////////////////////////////////////////////////////////////////


                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }

                tmpBuild.Append(") values(");
                if ((this.LinqID == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + LinqID + "'");
                    tmpBuild.Append(",");
                }
                
                if ((this.LinqName == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + LinqName + "'");
                    tmpBuild.Append(",");
                }
               
                if ((this.LinqDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + LinqDesc + "'");
                    tmpBuild.Append(",");
                }

                if ((this.LinqEngunit == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + LinqEngunit + "'");
                    tmpBuild.Append(",");
                }
                                
                if ((this.LinqIndex == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(LinqIndex.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.LinqIsValid == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(LinqIsValid.ToString());
                    tmpBuild.Append(",");
                }
                
                if ((this.LinqNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + LinqNote + "'");
                    tmpBuild.Append(",");
                }

                if ((this.LinqCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + LinqCreateTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.LinqModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + LinqModifyTime + "'");
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
                tmpBuild.Append("update KPI_Linq set ");
                if ((this.LinqID == null))
                {
                }
                else
                {
                    tmpBuild.Append("LinqID='" + LinqID + "'");
                    tmpBuild.Append(",");
                }

                if ((this.LinqName == null))
                {
                }
                else
                {
                    tmpBuild.Append("LinqName='" + LinqName + "'");
                    tmpBuild.Append(",");
                }
              
                if ((this.LinqDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("LinqDesc='" + LinqDesc + "'");
                    tmpBuild.Append(",");
                }

                if ((this.LinqEngunit == null))
                {
                }
                else
                {
                    tmpBuild.Append("LinqEngunit='" + LinqEngunit + "'");
                    tmpBuild.Append(",");
                }
                
                if ((this.LinqIndex == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("LinqIndex=" + LinqIndex.ToString());
                    tmpBuild.Append(",");
                }
                
                if ((this.LinqIsValid == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("LinqIsValid=" + LinqIsValid.ToString());
                    tmpBuild.Append(",");
                }
                
                if ((this.LinqNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("LinqNote='" + LinqNote + "'");
                    tmpBuild.Append(",");
                }

                if ((this.LinqCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("LinqCreateTime='" + LinqCreateTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.LinqModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("LinqModifyTime='" + LinqModifyTime + "'");
                    tmpBuild.Append(",");
                }

                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }

                tmpBuild.Append(" where ");
                tmpBuild.Append("LinqID='" + LinqID + "'");

                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }


        public override string DeleteSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("delete KPI_Linq");
                tmpBuild.Append(" where ");
                tmpBuild.Append("LinqID='" + LinqID + "'");

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
                tmpBuild.Append("LinqID");
                tmpBuild.Append(",");
                tmpBuild.Append("LinqName");
                tmpBuild.Append(",");
                tmpBuild.Append("LinqDesc");
                tmpBuild.Append(",");
                tmpBuild.Append("LinqEngunit");
                tmpBuild.Append(",");
                tmpBuild.Append("LinqIndex");
                tmpBuild.Append(",");
                tmpBuild.Append("LinqIsValid");
                tmpBuild.Append(",");
                tmpBuild.Append("LinqNote");
                tmpBuild.Append(",");
                tmpBuild.Append("LinqCreateTime");
                tmpBuild.Append(",");
                tmpBuild.Append("LinqModifyTime");

                tmpBuild.Append(" from KPI_Linq");
                tmpBuild.Append(" where ");
                tmpBuild.Append("LinqID='" + LinqID + "'");

                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }

        public override bool DrToMember(System.Data.DataRow dr)
        {
            try
            {
                if (dr["LinqID"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.LinqID = dr["LinqID"].ToString();
                }

                if (dr["LinqName"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.LinqName = dr["LinqName"].ToString();
                }

                if (dr["LinqDesc"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.LinqDesc = dr["LinqDesc"].ToString();
                }

                if (dr["LinqEngunit"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.LinqEngunit = dr["LinqEngunit"].ToString();
                }

                
                if (dr["LinqIndex"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.LinqIndex = int.Parse(dr["LinqIndex"].ToString());
                }

                if (dr["LinqIsValid"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.LinqIsValid = int.Parse(dr["LinqIsValid"].ToString());
                }
                                
                if (dr["LinqNote"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.LinqNote = dr["LinqNote"].ToString();
                }


                if (dr["LinqCreateTime"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.LinqCreateTime = dr["LinqCreateTime"].ToString();
                }


                if (dr["LinqModifyTime"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.LinqModifyTime = dr["LinqModifyTime"].ToString();
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
