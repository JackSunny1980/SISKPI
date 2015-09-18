
namespace SIS.DataEntity
{
    using System;
    using System.Xml;
    using System.Data;
    using System.Text;
    
    [System.Serializable()]
    [System.Runtime.InteropServices.Guid("53e9801b-6009-4dc8-a554-9923a85743cc")]

    public class AVGEntity : EntityBase
    {
        protected String _KeyID = null;
        protected String _ECID = null;
        protected String _ECCode = null;
        protected String _ECName = null;
        protected String _KeyEngunit = null;
        protected String _KeyTarget1 = null;
        protected String _KeyTarget2 = null;
        protected String _KeyDesign = null;
        protected double _KeyDIffMoney = double.MinValue;
        protected double _KeyOptMoney = double.MinValue;
        protected int _KeyIndex = int.MinValue;

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

        public virtual String KeyTarget1
        {
            get
            {
                return this._KeyTarget1;
            }
            set
            {
                this._KeyTarget1 = value;
            }
        }

        public virtual String KeyTarget2
        {
            get
            {
                return this._KeyTarget2;
            }
            set
            {
                this._KeyTarget2 = value;
            }
        }

        public virtual String  KeyDesign
        {
            get
            {
                return this._KeyDesign;
            }
            set
            {
                this._KeyDesign = value;
            }
        }
        public virtual double KeyDIffMoney
        {
            get
            {
                return this._KeyDIffMoney;
            }
            set
            {
                this._KeyDIffMoney = value;
            }
        }
        

        public virtual double KeyOptMoney
        {
            get
            {
                return this._KeyOptMoney;
            }
            set
            {
                this._KeyOptMoney = value;
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

       


        public override string InsertSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("insert into KPI_AVG(");

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
                
                if ((this.KeyEngunit == null))
                {
                }
                else
                {
                    tmpBuild.Append("KeyEngunit");
                    tmpBuild.Append(",");
                }

                if ((this.KeyTarget1 == null))
                {
                }
                else
                {
                    tmpBuild.Append("KeyTarget1");
                    tmpBuild.Append(",");
                }

                if ((this.KeyTarget2 == null))
                {
                }
                else
                {
                    tmpBuild.Append("KeyTarget2");
                    tmpBuild.Append(",");
                }

                if ((this.KeyDesign == null))
                {
                }
                else
                {
                    tmpBuild.Append("KeyDesign");
                    tmpBuild.Append(",");
                }

                if ((this.KeyDIffMoney == double.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("KeyDIffMoney");
                    tmpBuild.Append(",");
                }

                if ((this.KeyOptMoney == double.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("KeyOptMoney");
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
                
                if ((this.KeyEngunit == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + KeyEngunit + "'");
                    tmpBuild.Append(",");
                }


                if ((this.KeyTarget1 == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + KeyTarget1 + "'");
                    tmpBuild.Append(",");
                }

                if ((this.KeyTarget2 == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + KeyTarget2 + "'");
                    tmpBuild.Append(",");
                }

                if ((this.KeyDesign == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + KeyDesign + "'");
                    tmpBuild.Append(",");
                }
                
                if ((this.KeyDIffMoney == double.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(KeyDIffMoney.ToString());
                    tmpBuild.Append(",");
                }


                if ((this.KeyOptMoney == double.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(KeyOptMoney.ToString());
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
                tmpBuild.Append("update KPI_AVG set ");
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
                if ((this.KeyEngunit == null))
                {
                }
                else
                {
                    tmpBuild.Append("KeyEngunit='" + KeyEngunit + "'");
                    tmpBuild.Append(",");
                }
                if ((this.KeyTarget1 == null))
                {
                }
                else
                {
                    tmpBuild.Append("KeyTarget1='" + KeyTarget1 + "'");
                    tmpBuild.Append(",");
                }

                if ((this.KeyTarget2 == null))
                {
                }
                else
                {
                    tmpBuild.Append("KeyTarget2='" + KeyTarget2 + "'");
                    tmpBuild.Append(",");
                }

                if ((this.KeyDesign == null))
                {
                }
                else
                {
                    tmpBuild.Append("KeyDesign='" + KeyDesign + "'");
                    tmpBuild.Append(",");
                }
                
                if ((this.KeyDIffMoney == double.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("KeyDIffMoney=" + KeyDIffMoney.ToString());
                    tmpBuild.Append(",");
                }
                
                if ((this.KeyOptMoney == double.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("KeyOptMoney=" + KeyOptMoney.ToString());
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
                tmpBuild.Append("delete KPI_AVG ");
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
                tmpBuild.Append("KeyEngunit");
                tmpBuild.Append(",");
                tmpBuild.Append("KeyTarget1");
                tmpBuild.Append(",");
                tmpBuild.Append("KeyTarget2");
                tmpBuild.Append(",");
                tmpBuild.Append("KeyDesign");
                tmpBuild.Append(",");
                tmpBuild.Append("KeyDIffMoney");
                tmpBuild.Append(",");
                tmpBuild.Append("KeyOptMoney");
                tmpBuild.Append(",");
                tmpBuild.Append("KeyIndex");

                tmpBuild.Append(" from KPI_AVG");
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

                if (dr["KeyEngunit"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.KeyEngunit = dr["KeyEngunit"].ToString();
                }

                if (dr["KeyTarget1"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.KeyTarget1 = dr["KeyTarget1"].ToString();
                }

                if (dr["KeyTarget2"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.KeyTarget2 = dr["KeyTarget2"].ToString();
                }

                if (dr["KeyDesign"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.KeyDesign = dr["KeyDesign"].ToString();
                }

                if (dr["KeyDIffMoney"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.KeyDIffMoney = double.Parse(dr["KeyDIffMoney"].ToString());
                }
                
                if (dr["KeyOptMoney"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.KeyOptMoney = double.Parse(dr["KeyOptMoney"].ToString());
                }
                
                if (dr["KeyIndex"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.KeyIndex = int.Parse(dr["KeyIndex"].ToString());
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
