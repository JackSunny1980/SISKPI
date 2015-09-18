
namespace SIS.DataEntity
{
    using System;
    using System.Xml;
    using System.Data;
    using System.Text;
    
    [System.Serializable()]
    [System.Runtime.InteropServices.Guid("53e9841b-6409-4dc8-a554-9923a85743cc")]

    public class KPI_UserEntity : EntityBase
    {
        protected String _UserID = null;
        protected String _UserCode = null;
        protected String _UserName = null;
        protected String _UserDesc = null;
        protected String _UserPassword = null;
        protected String _UserEMail = null;
        protected String _UserPhone = null;
        protected String _UserTitle = null;
        protected String _UserURL = null;
        protected String _UserGIF = null;
        protected int _UserSEX = int.MinValue;
        protected String _UserGroups = null;
        protected int _UserIsValid = int.MinValue;
        protected String _UserNote = null;
        protected String _UserCreateTime = null;
        protected String _UserModifyTime = null;

        public virtual String UserID
        {
            get
            {
                return this._UserID;
            }
            set
            {
                this._UserID = value;
            }
        }   

        public virtual String UserCode
        {
            get
            {
                return this._UserCode;
            }
            set
            {
                this._UserCode = value;
            }
        } 

        public virtual String UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                this._UserName = value;
            }
        }

        public virtual String UserDesc
        {
            get
            {
                return this._UserDesc;
            }
            set
            {
                this._UserDesc = value;
            }
        }

        public virtual String UserPassword
        {
            get
            {
                return this._UserPassword;
            }
            set
            {
                this._UserPassword = value;
            }
        }

        public virtual String UserEMail
        {
            get
            {
                return this._UserEMail;
            }
            set
            {
                this._UserEMail = value;
            }
        }

        public virtual String UserPhone
        {
            get
            {
                return this._UserPhone;
            }
            set
            {
                this._UserPhone = value;
            }
        }

        public virtual String UserTitle
        {
            get
            {
                return this._UserTitle;
            }
            set
            {
                this._UserTitle = value;
            }
        }

        public virtual String UserURL
        {
            get
            {
                return this._UserURL;
            }
            set
            {
                this._UserURL = value;
            }
        }

        public virtual String UserGIF
        {
            get
            {
                return this._UserGIF;
            }
            set
            {
                this._UserGIF = value;
            }
        }


        public virtual int UserSEX
        {
            get
            {
                return this._UserSEX;
            }
            set
            {
                this._UserSEX = value;
            }
        }

        public virtual String UserGroups
        {
            get
            {
                return this._UserGroups;
            }
            set
            {
                this._UserGroups = value;
            }
        }

        public virtual int UserIsValid
        {
            get
            {
                return this._UserIsValid;
            }
            set
            {
                this._UserIsValid = value;
            }
        }

        public virtual String UserNote
        {
            get
            {
                return this._UserNote;
            }
            set
            {
                this._UserNote = value;
            }
        }

        public virtual String UserCreateTime
        {
            get
            {
                return this._UserCreateTime;
            }
            set
            {
                this._UserCreateTime = value;
            }
        }

        public virtual String UserModifyTime
        {
            get
            {
                return this._UserModifyTime;
            }
            set
            {
                this._UserModifyTime = value;
            }
        }

        public override string InsertSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("insert into KPI_User(");

                if ((this.UserID == null))
                {
                }
                else
                {
                    tmpBuild.Append("UserID");
                    tmpBuild.Append(",");
                }
                
                if ((this.UserCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("UserCode");
                    tmpBuild.Append(",");
                }
                
                if ((this.UserName == null))
                {
                }
                else
                {
                    tmpBuild.Append("UserName");
                    tmpBuild.Append(",");
                }

                if ((this.UserDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("UserDesc");
                    tmpBuild.Append(",");
                }

                if ((this.UserPassword == null))
                {
                }
                else
                {
                    tmpBuild.Append("UserPassword");
                    tmpBuild.Append(",");
                }

                if ((this.UserEMail == null))
                {
                }
                else
                {
                    tmpBuild.Append("UserEMail");
                    tmpBuild.Append(",");
                }

                if ((this.UserPhone == null))
                {
                }
                else
                {
                    tmpBuild.Append("UserPhone");
                    tmpBuild.Append(",");
                }

                if ((this.UserTitle == null))
                {
                }
                else
                {
                    tmpBuild.Append("UserTitle");
                    tmpBuild.Append(",");
                }

                if ((this.UserURL == null))
                {
                }
                else
                {
                    tmpBuild.Append("UserURL");
                    tmpBuild.Append(",");
                }

                if ((this.UserGIF == null))
                {
                }
                else
                {
                    tmpBuild.Append("UserGIF");
                    tmpBuild.Append(",");
                }

                if ((this.UserSEX == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("UserSEX");
                    tmpBuild.Append(",");
                }

                if ((this.UserGroups == null))
                {
                }
                else
                {
                    tmpBuild.Append("UserGroups");
                    tmpBuild.Append(",");
                }
                
                if ((this.UserIsValid == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("UserIsValid");
                    tmpBuild.Append(",");
                }
                
                if ((this.UserNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("UserNote");
                    tmpBuild.Append(",");
                }

                if ((this.UserCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("UserCreateTime");
                    tmpBuild.Append(",");
                }

                if ((this.UserModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("UserModifyTime");
                    tmpBuild.Append(",");
                }


                ///////////////////////////////////////////////////////////////////////////////////////


                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }

                tmpBuild.Append(") values(");
                if ((this.UserID == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + UserID + "'");
                    tmpBuild.Append(",");
                }

                if ((this.UserCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + UserCode + "'");
                    tmpBuild.Append(",");
                }
                
                if ((this.UserName == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + UserName + "'");
                    tmpBuild.Append(",");
                }
               
                if ((this.UserDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + UserDesc + "'");
                    tmpBuild.Append(",");
                }
               
                if ((this.UserPassword == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + UserPassword + "'");
                    tmpBuild.Append(",");
                }
               
                if ((this.UserEMail == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + UserEMail + "'");
                    tmpBuild.Append(",");
                }
               
                if ((this.UserPhone == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + UserPhone + "'");
                    tmpBuild.Append(",");
                }
               
                if ((this.UserTitle == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + UserTitle + "'");
                    tmpBuild.Append(",");
                }
             
                if ((this.UserURL == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + UserURL + "'");
                    tmpBuild.Append(",");
                }

                if ((this.UserGIF == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + UserGIF + "'");
                    tmpBuild.Append(",");
                }

                if ((this.UserSEX == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(UserSEX.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.UserGroups == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + UserGroups + "'");
                    tmpBuild.Append(",");
                }

                if ((this.UserIsValid == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(UserIsValid.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.UserNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + UserNote + "'");
                    tmpBuild.Append(",");
                }

                if ((this.UserCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + UserCreateTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.UserModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + UserModifyTime + "'");
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
                tmpBuild.Append("update KPI_User set ");
                if ((this.UserID == null))
                {
                }
                else
                {
                    tmpBuild.Append("UserID='" + UserID + "'");
                    tmpBuild.Append(",");
                }

                if ((this.UserCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("UserCode='" + UserCode + "'");
                    tmpBuild.Append(",");
                }                

                if ((this.UserName == null))
                {
                }
                else
                {
                    tmpBuild.Append("UserName='" + UserName + "'");
                    tmpBuild.Append(",");
                }

                if ((this.UserDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("UserDesc='" + UserDesc + "'");
                    tmpBuild.Append(",");
                }

                if ((this.UserPassword == null))
                {
                }
                else
                {
                    tmpBuild.Append("UserPassword='" + UserPassword + "'");
                    tmpBuild.Append(",");
                }

                if ((this.UserEMail == null))
                {
                }
                else
                {
                    tmpBuild.Append("UserEMail='" + UserEMail + "'");
                    tmpBuild.Append(",");
                }

                if ((this.UserPhone == null))
                {
                }
                else
                {
                    tmpBuild.Append("UserPhone='" + UserPhone + "'");
                    tmpBuild.Append(",");
                }

                if ((this.UserTitle == null))
                {
                }
                else
                {
                    tmpBuild.Append("UserTitle='" + UserTitle + "'");
                    tmpBuild.Append(",");
                }

                if ((this.UserURL == null))
                {
                }
                else
                {
                    tmpBuild.Append("UserURL='" + UserURL + "'");
                    tmpBuild.Append(",");
                }

                if ((this.UserGIF == null))
                {
                }
                else
                {
                    tmpBuild.Append("UserGIF='" + UserGIF + "'");
                    tmpBuild.Append(",");
                }

                if ((this.UserSEX == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("UserSEX=" + UserSEX.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.UserGroups == null))
                {
                }
                else
                {
                    tmpBuild.Append("UserGroups='" + UserGroups + "'");
                    tmpBuild.Append(",");
                }

                if ((this.UserIsValid == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("UserIsValid=" + UserIsValid.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.UserNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("UserNote='" + UserNote + "'");
                    tmpBuild.Append(",");
                }

                if ((this.UserCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("UserCreateTime='" + UserCreateTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.UserModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("UserModifyTime='" + UserModifyTime + "'");
                    tmpBuild.Append(",");
                }

                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }

                tmpBuild.Append(" where ");
                tmpBuild.Append("UserID='" + UserID + "'");

                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }


        public override string DeleteSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("delete KPI_User");
                tmpBuild.Append(" where ");
                tmpBuild.Append("UserID='" + UserID + "'");

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
                tmpBuild.Append("UserID");
                tmpBuild.Append(",");
                tmpBuild.Append("UserCode");
                tmpBuild.Append(",");
                tmpBuild.Append("UserName");
                tmpBuild.Append(",");
                tmpBuild.Append("UserDesc");
                tmpBuild.Append(",");
                tmpBuild.Append("UserPassword");
                tmpBuild.Append(",");
                tmpBuild.Append("UserEMail");
                tmpBuild.Append(",");
                tmpBuild.Append("UserPhone");
                tmpBuild.Append(",");
                tmpBuild.Append("UserTitle");
                tmpBuild.Append(",");
                tmpBuild.Append("UserURL");
                tmpBuild.Append(",");
                tmpBuild.Append("UserGIF");
                tmpBuild.Append(",");
                tmpBuild.Append("UserSEX");
                tmpBuild.Append(",");
                tmpBuild.Append("UserGroups");
                tmpBuild.Append(",");
                tmpBuild.Append("UserIsValid");
                tmpBuild.Append(",");
                tmpBuild.Append("UserNote");
                tmpBuild.Append(",");
                tmpBuild.Append("UserCreateTime");
                tmpBuild.Append(",");
                tmpBuild.Append("UserModifyTime");

                tmpBuild.Append(" from KPI_User");
                tmpBuild.Append(" where ");
                tmpBuild.Append("UserID='" + UserID + "'");

                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }

        public override bool DrToMember(System.Data.DataRow dr)
        {
            try
            {
                if (dr["UserID"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.UserID = dr["UserID"].ToString();
                }

                if (dr["UserCode"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.UserCode = dr["UserCode"].ToString();
                }     
                            
                if (dr["UserName"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.UserName = dr["UserName"].ToString();
                }     

                if (dr["UserDesc"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.UserDesc = dr["UserDesc"].ToString();
                }   

                if (dr["UserPassword"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.UserPassword = dr["UserPassword"].ToString();
                }   

                if (dr["UserEMail"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.UserEMail = dr["UserEMail"].ToString();
                }   

                if (dr["UserPhone"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.UserPhone = dr["UserPhone"].ToString();
                }

                if (dr["UserTitle"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.UserTitle = dr["UserTitle"].ToString();
                }

                if (dr["UserURL"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.UserURL = dr["UserURL"].ToString();
                }

                if (dr["UserGIF"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.UserGIF = dr["UserGIF"].ToString();
                }

                if (dr["UserSEX"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.UserSEX = int.Parse(dr["UserSEX"].ToString());
                }

                if (dr["UserGroups"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.UserGroups = dr["UserGroups"].ToString();
                }

                if (dr["UserIsValid"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.UserIsValid = int.Parse(dr["UserIsValid"].ToString());
                }
                
                if (dr["UserNote"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.UserNote = dr["UserNote"].ToString();
                }


                if (dr["UserCreateTime"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.UserCreateTime = dr["UserCreateTime"].ToString();
                }


                if (dr["UserModifyTime"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.UserModifyTime = dr["UserModifyTime"].ToString();
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
