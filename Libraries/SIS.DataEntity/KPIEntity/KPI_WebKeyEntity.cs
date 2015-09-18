
namespace SIS.DataEntity
{
    using System;
    using System.Xml;
    using System.Data;
    using System.Text;
    
    [System.Serializable()]
    [System.Runtime.InteropServices.Guid("53e9801b-6009-4dc8-a554-9923a85743cc")]

    public class KPI_WebKeyEntity : EntityBase
    {
        protected String _KeyID = null;
        protected String _ECID = null;
        protected String _ECCode = null;
        protected String _ECName = null;
        protected String _WebCode = null;
        protected String _KeyEngunit = null;
        protected int _KeyCalcType = int.MinValue;
        protected int _KeyIndex = int.MinValue;
        protected int _KeyIsValid = int.MinValue;
        protected String _KeyNote = null;
        protected String _KeyCreateTime = null;
        protected String _KeyModifyTime = null;

        public virtual String KeyID
        {
            get
            {
                return this._KeyID;
            }
            set
            {
                this._KeyID = value;
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

        public virtual String ECCode
        {
            get
            {
                return this._ECCode;
            }
            set
            {
                this._ECCode = value;
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

        public virtual String KeyEngunit
        {
            get
            {
                return this._KeyEngunit;
            }
            set
            {
                this._KeyEngunit = value;
            }
        }

        public virtual int KeyCalcType
        {
            get
            {
                return this._KeyCalcType;
            }
            set
            {
                this._KeyCalcType = value;
            }
        }
        
        public virtual int KeyIndex
        {
            get
            {
                return this._KeyIndex;
            }
            set
            {
                this._KeyIndex = value;
            }
        }

        public virtual int KeyIsValid
        {
            get
            {
                return this._KeyIsValid;
            }
            set
            {
                this._KeyIsValid = value;
            }
        }

        public virtual String KeyNote
        {
            get
            {
                return this._KeyNote;
            }
            set
            {
                this._KeyNote = value;
            }
        }

        public virtual String KeyCreateTime
        {
            get
            {
                return this._KeyCreateTime;
            }
            set
            {
                this._KeyCreateTime = value;
            }
        }

        public virtual String KeyModifyTime
        {
            get
            {
                return this._KeyModifyTime;
            }
            set
            {
                this._KeyModifyTime = value;
            }
        }

        public override string InsertSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("insert into KPI_WebKey(");

                if ((this.KeyID == null))
                {
                }
                else
                {
                    tmpBuild.Append("KeyID");
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

                if ((this.ECCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("ECCode");
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

                if ((this.WebCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("WebCode");
                    tmpBuild.Append(",");
                }

                if ((this.KeyEngunit == null))
                {
                }
                else
                {
                    tmpBuild.Append("KeyEngunit");
                    tmpBuild.Append(",");
                }


                if ((this.KeyCalcType == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("KeyCalcType");
                    tmpBuild.Append(",");
                }
                
                if ((this.KeyIndex == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("KeyIndex");
                    tmpBuild.Append(",");
                }
                
                if ((this.KeyIsValid == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("KeyIsValid");
                    tmpBuild.Append(",");
                }
                                                               
                if ((this.KeyNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("KeyNote");
                    tmpBuild.Append(",");
                }

                if ((this.KeyCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("KeyCreateTime");
                    tmpBuild.Append(",");
                }

                if ((this.KeyModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("KeyModifyTime");
                    tmpBuild.Append(",");
                }


                ///////////////////////////////////////////////////////////////////////////////////////


                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }

                tmpBuild.Append(") values(");

                if ((this.KeyID == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + KeyID + "'");
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

                if ((this.ECCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + ECCode + "'");
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
                
                if ((this.WebCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + WebCode + "'");
                    tmpBuild.Append(",");
                }

                if ((this.KeyEngunit == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + KeyEngunit + "'");
                    tmpBuild.Append(",");
                }

                if ((this.KeyCalcType == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(KeyCalcType.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.KeyIndex == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(KeyIndex.ToString());
                    tmpBuild.Append(",");
                }


                if ((this.KeyIsValid == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(KeyIsValid.ToString());
                    tmpBuild.Append(",");
                }
                
                if ((this.KeyNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + KeyNote + "'");
                    tmpBuild.Append(",");
                }

                if ((this.KeyCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + KeyCreateTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.KeyModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + KeyModifyTime + "'");
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
                tmpBuild.Append("update KPI_WebKey set ");
                if ((this.KeyID == null))
                {
                }
                else
                {
                    tmpBuild.Append("KeyID='" + KeyID + "'");
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


                if ((this.ECCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("ECCode='" + ECCode + "'");
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

                if ((this.WebCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("WebCode='" + WebCode + "'");
                    tmpBuild.Append(",");
                }

                if ((this.KeyEngunit == null))
                {
                }
                else
                {
                    tmpBuild.Append("KeyEngunit='" + KeyEngunit + "'");
                    tmpBuild.Append(",");
                }

                if ((this.KeyCalcType == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("KeyCalcType=" + KeyCalcType.ToString());
                    tmpBuild.Append(",");
                }
                
                if ((this.KeyIndex== int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("KeyIndex=" + KeyIndex.ToString());
                    tmpBuild.Append(",");
                }             

                if ((this.KeyIsValid == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("KeyIsValid=" + KeyIsValid.ToString());
                    tmpBuild.Append(",");
                }
                
                if ((this.KeyNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("KeyNote='" + KeyNote + "'");
                    tmpBuild.Append(",");
                }

                if ((this.KeyCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("KeyCreateTime='" + KeyCreateTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.KeyModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("KeyModifyTime='" + KeyModifyTime + "'");
                    tmpBuild.Append(",");
                }

                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }

                tmpBuild.Append(" where ");
                tmpBuild.Append("KeyID='" + KeyID + "'");

                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }


        public override string DeleteSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("delete KPI_WebKey ");
                tmpBuild.Append(" where ");
                tmpBuild.Append("KeyID='" + KeyID + "'");

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
                tmpBuild.Append("KeyID");
                tmpBuild.Append(",");
                tmpBuild.Append("ECID");
                tmpBuild.Append(",");
                tmpBuild.Append("ECCode");
                tmpBuild.Append(",");
                tmpBuild.Append("ECName");
                tmpBuild.Append(",");
                tmpBuild.Append("WebCode");
                tmpBuild.Append(",");
                tmpBuild.Append("KeyEngunit");
                tmpBuild.Append(",");
                tmpBuild.Append("KeyCalcType");
                tmpBuild.Append(",");
                tmpBuild.Append("KeyIndex");
                tmpBuild.Append(",");
                tmpBuild.Append("KeyIsValid");
                tmpBuild.Append(",");
                tmpBuild.Append("KeyNote");
                tmpBuild.Append(",");
                tmpBuild.Append("KeyCreateTime");
                tmpBuild.Append(",");
                tmpBuild.Append("KeyModifyTime");

                tmpBuild.Append(" from KPI_WebKey");
                tmpBuild.Append(" where ");
                tmpBuild.Append("KeyID='" + KeyID + "'");

                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }

        public override bool DrToMember(System.Data.DataRow dr)
        {
            try
            {
                if (dr["KeyID"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.KeyID = dr["KeyID"].ToString();
                }

                if (dr["ECID"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.ECID = dr["ECID"].ToString();
                }
                if (dr["ECCode"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.ECCode = dr["ECCode"].ToString();
                }

                if (dr["ECName"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.ECName = dr["ECName"].ToString();
                }

                if (dr["WebCode"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.WebCode = dr["WebCode"].ToString();
                }
                if (dr["KeyEngunit"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.KeyEngunit = dr["KeyEngunit"].ToString();
                }
                if (dr["KeyCalcType"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.KeyCalcType = int.Parse(dr["KeyCalcType"].ToString());
                }
                
                if (dr["KeyIndex"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.KeyIndex = int.Parse(dr["KeyIndex"].ToString());
                }
                
                if (dr["KeyIsValid"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.KeyIsValid = int.Parse(dr["KeyIsValid"].ToString());
                }
                
                if (dr["KeyNote"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.KeyNote = dr["KeyNote"].ToString();
                }

                if (dr["KeyCreateTime"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.KeyCreateTime = dr["KeyCreateTime"].ToString();
                }


                if (dr["KeyModifyTime"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.KeyModifyTime = dr["KeyModifyTime"].ToString();
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
