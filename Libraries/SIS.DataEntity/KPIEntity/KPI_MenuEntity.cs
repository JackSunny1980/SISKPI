
namespace SIS.DataEntity
{
    using System;
    using System.Xml;
    using System.Data;
    using System.Text;
    
    [System.Serializable()]
    [System.Runtime.InteropServices.Guid("53e9841b-6409-4dc8-a554-9923a85743cc")]

    public class KPI_MenuEntity : EntityBase
    {
        protected String _MenuID = null;
        protected String _MenuParentID = null;
        protected String _MenuCode = null;
        protected String _MenuName = null;
        protected String _MenuDesc = null;
        protected int _MenuIsDisplay = int.MinValue;
        protected int _MenuIndex = int.MinValue;
        protected int _MenuType = int.MinValue;
        protected String _MenuURL = null;
        protected String _MenuGIF = null;
        protected int _MenuTarget = int.MinValue;
        protected String _MenuGroups = null;
        protected int _MenuIsValid = int.MinValue;
        protected String _MenuNote = null;
        protected String _MenuCreateTime = null;
        protected String _MenuModifyTime = null;

        public virtual String MenuID
        {
            get
            {
                return this._MenuID;
            }
            set
            {
                this._MenuID = value;
            }
        }

        public virtual String MenuParentID
        {
            get
            {
                return this._MenuParentID;
            }
            set
            {
                this._MenuParentID = value;
            }
        }   

        public virtual String MenuCode
        {
            get
            {
                return this._MenuCode;
            }
            set
            {
                this._MenuCode = value;
            }
        } 

        public virtual String MenuName
        {
            get
            {
                return this._MenuName;
            }
            set
            {
                this._MenuName = value;
            }
        }

        public virtual String MenuDesc
        {
            get
            {
                return this._MenuDesc;
            }
            set
            {
                this._MenuDesc = value;
            }
        }

        public virtual int MenuIsDisplay
        {
            get
            {
                return this._MenuIsDisplay;
            }
            set
            {
                this._MenuIsDisplay = value;
            }
        }

        public virtual int MenuIndex
        {
            get
            {
                return this._MenuIndex;
            }
            set
            {
                this._MenuIndex = value;
            }
        }

        public virtual int MenuType
        {
            get
            {
                return this._MenuType;
            }
            set
            {
                this._MenuType = value;
            }
        }

        public virtual String MenuURL
        {
            get
            {
                return this._MenuURL;
            }
            set
            {
                this._MenuURL = value;
            }
        }

        public virtual String MenuGIF
        {
            get
            {
                return this._MenuGIF;
            }
            set
            {
                this._MenuGIF = value;
            }
        }


        public virtual int MenuTarget
        {
            get
            {
                return this._MenuTarget;
            }
            set
            {
                this._MenuTarget = value;
            }
        }

        public virtual String MenuGroups
        {
            get
            {
                return this._MenuGroups;
            }
            set
            {
                this._MenuGroups = value;
            }
        }

        public virtual int MenuIsValid
        {
            get
            {
                return this._MenuIsValid;
            }
            set
            {
                this._MenuIsValid = value;
            }
        }

        public virtual String MenuNote
        {
            get
            {
                return this._MenuNote;
            }
            set
            {
                this._MenuNote = value;
            }
        }

        public virtual String MenuCreateTime
        {
            get
            {
                return this._MenuCreateTime;
            }
            set
            {
                this._MenuCreateTime = value;
            }
        }

        public virtual String MenuModifyTime
        {
            get
            {
                return this._MenuModifyTime;
            }
            set
            {
                this._MenuModifyTime = value;
            }
        }

        public override string InsertSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("insert into KPI_Menu(");

                if ((this.MenuID == null))
                {
                }
                else
                {
                    tmpBuild.Append("MenuID");
                    tmpBuild.Append(",");
                }

                if ((this.MenuParentID == null))
                {
                }
                else
                {
                    tmpBuild.Append("MenuParentID");
                    tmpBuild.Append(",");
                }

                if ((this.MenuCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("MenuCode");
                    tmpBuild.Append(",");
                }
                
                if ((this.MenuName == null))
                {
                }
                else
                {
                    tmpBuild.Append("MenuName");
                    tmpBuild.Append(",");
                }

                if ((this.MenuDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("MenuDesc");
                    tmpBuild.Append(",");
                }

                if ((this.MenuIsDisplay == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("MenuIsDisplay");
                    tmpBuild.Append(",");
                }

                if ((this.MenuIndex == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("MenuIndex");
                    tmpBuild.Append(",");
                }

                if ((this.MenuType == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("MenuType");
                    tmpBuild.Append(",");
                }

                if ((this.MenuURL == null))
                {
                }
                else
                {
                    tmpBuild.Append("MenuURL");
                    tmpBuild.Append(",");
                }

                if ((this.MenuGIF == null))
                {
                }
                else
                {
                    tmpBuild.Append("MenuGIF");
                    tmpBuild.Append(",");
                }

                if ((this.MenuTarget == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("MenuTarget");
                    tmpBuild.Append(",");
                }

                if ((this.MenuGroups == null))
                {
                }
                else
                {
                    tmpBuild.Append("MenuGroups");
                    tmpBuild.Append(",");
                }
                
                if ((this.MenuIsValid == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("MenuIsValid");
                    tmpBuild.Append(",");
                }
                
                if ((this.MenuNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("MenuNote");
                    tmpBuild.Append(",");
                }

                if ((this.MenuCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("MenuCreateTime");
                    tmpBuild.Append(",");
                }

                if ((this.MenuModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("MenuModifyTime");
                    tmpBuild.Append(",");
                }


                ///////////////////////////////////////////////////////////////////////////////////////


                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }

                tmpBuild.Append(") values(");
                if ((this.MenuID == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + MenuID + "'");
                    tmpBuild.Append(",");
                }

                if ((this.MenuParentID == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + MenuParentID + "'");
                    tmpBuild.Append(",");
                }

                if ((this.MenuCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + MenuCode + "'");
                    tmpBuild.Append(",");
                }
                
                if ((this.MenuName == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + MenuName + "'");
                    tmpBuild.Append(",");
                }
               
                if ((this.MenuDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + MenuDesc + "'");
                    tmpBuild.Append(",");
                }
             
                if ((this.MenuIsDisplay == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(MenuIsDisplay.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.MenuIndex == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(MenuIndex.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.MenuType == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(MenuType.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.MenuURL == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + MenuURL + "'");
                    tmpBuild.Append(",");
                }

                if ((this.MenuGIF == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + MenuGIF + "'");
                    tmpBuild.Append(",");
                }

                if ((this.MenuTarget == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(MenuTarget.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.MenuGroups == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + MenuGroups + "'");
                    tmpBuild.Append(",");
                }

                if ((this.MenuIsValid == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(MenuIsValid.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.MenuNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + MenuNote + "'");
                    tmpBuild.Append(",");
                }

                if ((this.MenuCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + MenuCreateTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.MenuModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + MenuModifyTime + "'");
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
                tmpBuild.Append("update KPI_Menu set ");
                if ((this.MenuID == null))
                {
                }
                else
                {
                    tmpBuild.Append("MenuID='" + MenuID + "'");
                    tmpBuild.Append(",");
                }

                if ((this.MenuParentID == null))
                {
                }
                else
                {
                    tmpBuild.Append("MenuParentID='" + MenuParentID + "'");
                    tmpBuild.Append(",");
                }

                if ((this.MenuCode == null))
                {
                }
                else
                {
                    tmpBuild.Append("MenuCode='" + MenuCode + "'");
                    tmpBuild.Append(",");
                }                

                if ((this.MenuName == null))
                {
                }
                else
                {
                    tmpBuild.Append("MenuName='" + MenuName + "'");
                    tmpBuild.Append(",");
                }
                if ((this.MenuDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("MenuDesc='" + MenuDesc + "'");
                    tmpBuild.Append(",");
                }

                if ((this.MenuIsDisplay == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("MenuIsDisplay=" + MenuIsDisplay.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.MenuIndex == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("MenuIndex=" + MenuIndex.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.MenuType == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("MenuType=" + MenuType.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.MenuURL == null))
                {
                }
                else
                {
                    tmpBuild.Append("MenuURL='" + MenuURL + "'");
                    tmpBuild.Append(",");
                }

                if ((this.MenuGIF == null))
                {
                }
                else
                {
                    tmpBuild.Append("MenuGIF='" + MenuGIF + "'");
                    tmpBuild.Append(",");
                }

                if ((this.MenuTarget == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("MenuTarget=" + MenuTarget.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.MenuGroups == null))
                {
                }
                else
                {
                    tmpBuild.Append("MenuGroups='" + MenuGroups + "'");
                    tmpBuild.Append(",");
                }

                if ((this.MenuIsValid == int.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("MenuIsValid=" + MenuIsValid.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.MenuNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("MenuNote='" + MenuNote + "'");
                    tmpBuild.Append(",");
                }

                if ((this.MenuCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("MenuCreateTime='" + MenuCreateTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.MenuModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("MenuModifyTime='" + MenuModifyTime + "'");
                    tmpBuild.Append(",");
                }

                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }

                tmpBuild.Append(" where ");
                tmpBuild.Append("MenuID='" + MenuID + "'");

                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }


        public override string DeleteSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("delete KPI_Menu");
                tmpBuild.Append(" where ");
                tmpBuild.Append("MenuID='" + MenuID + "'");

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
                tmpBuild.Append("MenuID");
                tmpBuild.Append(",");
                tmpBuild.Append("MenuParentID");
                tmpBuild.Append(",");
                tmpBuild.Append("MenuCode");
                tmpBuild.Append(",");
                tmpBuild.Append("MenuName");
                tmpBuild.Append(",");
                tmpBuild.Append("MenuDesc");
                tmpBuild.Append(",");
                tmpBuild.Append("MenuIsDisplay");
                tmpBuild.Append(",");
                tmpBuild.Append("MenuIndex");
                tmpBuild.Append(",");
                tmpBuild.Append("MenuType");
                tmpBuild.Append(",");
                tmpBuild.Append("MenuURL");
                tmpBuild.Append(",");
                tmpBuild.Append("MenuGIF");
                tmpBuild.Append(",");
                tmpBuild.Append("MenuTarget");
                tmpBuild.Append(",");
                tmpBuild.Append("MenuGroups");
                tmpBuild.Append(",");
                tmpBuild.Append("MenuIsValid");
                tmpBuild.Append(",");
                tmpBuild.Append("MenuNote");
                tmpBuild.Append(",");
                tmpBuild.Append("MenuCreateTime");
                tmpBuild.Append(",");
                tmpBuild.Append("MenuModifyTime");

                tmpBuild.Append(" from KPI_Menu");
                tmpBuild.Append(" where ");
                tmpBuild.Append("MenuID='" + MenuID + "'");

                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }

        public override bool DrToMember(System.Data.DataRow dr)
        {
            try
            {
                if (dr["MenuID"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.MenuID = dr["MenuID"].ToString();
                }

                if (dr["MenuParentID"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.MenuParentID = dr["MenuParentID"].ToString();
                }

                if (dr["MenuCode"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.MenuCode = dr["MenuCode"].ToString();
                }     
                            
                if (dr["MenuName"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.MenuName = dr["MenuName"].ToString();
                }     

                if (dr["MenuDesc"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.MenuDesc = dr["MenuDesc"].ToString();
                }

                if (dr["MenuIsDisplay"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.MenuIsDisplay = int.Parse(dr["MenuIsDisplay"].ToString());
                }

                if (dr["MenuIndex"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.MenuIndex = int.Parse(dr["MenuIndex"].ToString());
                }

                if (dr["MenuType"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.MenuType = int.Parse(dr["MenuType"].ToString());
                }

                if (dr["MenuURL"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.MenuURL = dr["MenuURL"].ToString();
                }

                if (dr["MenuGIF"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.MenuGIF = dr["MenuGIF"].ToString();
                }

                if (dr["MenuTarget"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.MenuTarget = int.Parse(dr["MenuTarget"].ToString());
                }

                if (dr["MenuGroups"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.MenuGroups = dr["MenuGroups"].ToString();
                }

                if (dr["MenuIsValid"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.MenuIsValid = int.Parse(dr["MenuIsValid"].ToString());
                }
                
                if (dr["MenuNote"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.MenuNote = dr["MenuNote"].ToString();
                }


                if (dr["MenuCreateTime"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.MenuCreateTime = dr["MenuCreateTime"].ToString();
                }


                if (dr["MenuModifyTime"] == System.DBNull.Value)
                {
                }
                else
                {
                    this.MenuModifyTime = dr["MenuModifyTime"].ToString();
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
