
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
	using System.ComponentModel;
    using System.Collections.Generic;

    [System.Serializable()]
    [System.Runtime.InteropServices.Guid("811708b6-a974-478d-aac9-3be6e2437b18")]
    public class KPI_WorkEntity : EntityBase
    {
        protected String _WorkID = null;
        protected String _WorkName = null;
        protected String _WorkDesc = null;
        protected String _WorkStartTime = null;
        protected String _WorkEndTime = null;
        protected Int32 _WorkBaseShifts = Int32.MinValue;
        protected Int32 _WorkBaseDays = Int32.MinValue;
        protected String _WorkSequence = null;
        protected String _WorkShift = null;
        protected Int32 _WorkIsValid = Int32.MinValue;
        //protected String _WorkPeriod = null;
        protected String _WorkNote = null;
        protected String _WorkCreateTime = null;
        protected String _WorkModifyTime = null;


        /// 该字段已经被作为Where的一部分
		[Description("WorkID")]
        public virtual String WorkID
        {
            get
            {
                return this._WorkID;
            }
            set
            {
                this._WorkID = value;
            }
        }

		[Description("WorkName")]
        public virtual String WorkName
        {
            get
            {
                return this._WorkName;
            }
            set
            {
                this._WorkName = value;
            }
        }

		[Description("WorkDesc")]
        public virtual String WorkDesc
        {
            get
            {
                return this._WorkDesc;
            }
            set
            {
                this._WorkDesc = value;
            }
        }

		[Description("WorkStartTime")]
        public virtual String WorkStartTime
        {
            get
            {
                return this._WorkStartTime;
            }
            set
            {
                this._WorkStartTime = value;
            }
        }

		[Description("WorkEndTime")]
        public virtual String WorkEndTime
        {
            get
            {
                return this._WorkEndTime;
            }
            set
            {
                this._WorkEndTime = value;
            }
        }

		[Description("WorkBaseShifts")]
        public virtual Int32 WorkBaseShifts
        {
            get
            {
                return this._WorkBaseShifts;
            }
            set
            {
                this._WorkBaseShifts = value;
            }
        }

		[Description("WorkBaseDays")]
        public virtual Int32 WorkBaseDays
        {
            get
            {
                return this._WorkBaseDays;
            }
            set
            {
                this._WorkBaseDays = value;
            }
        }

		[Description("WorkSequence")]
        public virtual String WorkSequence
        {
            get
            {
                return this._WorkSequence;
            }
            set
            {
                this._WorkSequence = value;
            }
        }

		[Description("WorkShift")]
        public virtual String WorkShift
        {
            get
            {
                return this._WorkShift;
            }
            set
            {
                this._WorkShift = value;
            }
        }

		[Description("WorkIsValid")]
        public virtual Int32 WorkIsValid
        {
            get
            {
                return this._WorkIsValid;
            }
            set
            {
                this._WorkIsValid = value;
            }
        }

        //public virtual String WorkPeriod
        //{
        //    get
        //    {
        //        return this._WorkPeriod;
        //    }
        //    set
        //    {
        //        this._WorkPeriod = value;
        //    }
        //}

		[Description("WorkNote")]
        public virtual String WorkNote
        {
            get
            {
                return this._WorkNote;
            }
            set
            {
                this._WorkNote = value;
            }
        }
	
        public virtual String WorkCreateTime
        {
            get
            {
                return this._WorkCreateTime;
            }
            set
            {
                this._WorkCreateTime = value;
            }
        }
        public virtual String WorkModifyTime
        {
            get
            {
                return this._WorkModifyTime;
            }
            set
            {
                this._WorkModifyTime = value;
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
                tmpBuild.Append("insert into KPI_Work(");
                if (this.WorkID == null)
                {
                }
                else
                {
                    tmpBuild.Append("WorkID");
                    tmpBuild.Append(",");
                }

                if (this.WorkName == null)
                {
                }
                else
                {
                    tmpBuild.Append("WorkName");
                    tmpBuild.Append(",");
                }

                if (this.WorkDesc == null)
                {
                }
                else
                {
                    tmpBuild.Append("WorkDesc");
                    tmpBuild.Append(",");
                }

                if (this.WorkStartTime == null)
                {
                }
                else
                {
                    tmpBuild.Append("WorkStartTime");
                    tmpBuild.Append(",");
                }
                if (this.WorkEndTime == null)
                {
                }
                else
                {
                    tmpBuild.Append("WorkEndTime");
                    tmpBuild.Append(",");
                }

                if (this.WorkBaseShifts == Int32.MinValue)
                {
                }
                else
                {
                    tmpBuild.Append("WorkBaseShifts");
                    tmpBuild.Append(",");
                }

                if (this.WorkBaseDays == Int32.MinValue)
                {
                }
                else
                {
                    tmpBuild.Append("WorkBaseDays");
                    tmpBuild.Append(",");
                }
                if (this.WorkSequence == null)
                {
                }
                else
                {
                    tmpBuild.Append("WorkSequence");
                    tmpBuild.Append(",");
                }
                if (this.WorkShift == null)
                {
                }
                else
                {
                    tmpBuild.Append("WorkShift");
                    tmpBuild.Append(",");
                }


                if (this.WorkIsValid == Int32.MinValue)
                {
                }
                else
                {
                    tmpBuild.Append("WorkIsValid");
                    tmpBuild.Append(",");
                }
                //if (this.WorkPeriod == null)
                //{
                //}
                //else
                //{
                //    tmpBuild.Append("WorkPeriod");
                //    tmpBuild.Append(",");
                //}

                if ((this.WorkNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("WorkNote");
                    tmpBuild.Append(",");
                }

                if (this.WorkCreateTime == null)
                {
                }
                else
                {
                    tmpBuild.Append("WorkCreateTime");
                    tmpBuild.Append(",");
                }

                if (this.WorkModifyTime == null)
                {
                }
                else
                {
                    tmpBuild.Append("WorkModifyTime");
                    tmpBuild.Append(",");
                }


                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }
                tmpBuild.Append(") values(");

                if (this.WorkID == null)
                {
                }
                else
                {
                    tmpBuild.Append("'" + WorkID + "'");
                    tmpBuild.Append(",");
                }
                
                if (this.WorkName == null)
                {
                }
                else
                {
                    tmpBuild.Append("'" + WorkName + "'");
                    tmpBuild.Append(",");
                }

                if (this.WorkDesc == null)
                {
                }
                else
                {
                    tmpBuild.Append("'" + WorkDesc + "'");
                    tmpBuild.Append(",");
                }
                
                if (this.WorkStartTime == null)
                {
                }
                else
                {
                    tmpBuild.Append("'" + WorkStartTime + "'");
                    tmpBuild.Append(",");
                }
                if (this.WorkEndTime == null)
                {
                }
                else
                {
                    tmpBuild.Append("'" + WorkEndTime + "'");
                    tmpBuild.Append(",");
                }
                if ((this.WorkBaseShifts == Int32.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(WorkBaseShifts.ToString());
                    tmpBuild.Append(",");
                }
                if ((this.WorkBaseDays == Int32.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(WorkBaseDays.ToString());
                    tmpBuild.Append(",");
                }
                if (this.WorkSequence == null)
                {
                }
                else
                {
                    tmpBuild.Append("'" + WorkSequence + "'");
                    tmpBuild.Append(",");
                }

                if ((this.WorkShift == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + WorkShift + "'");
                    tmpBuild.Append(",");
                }

                if ((this.WorkIsValid == Int32.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append(WorkIsValid.ToString());
                    tmpBuild.Append(",");
                }
                //if ((this.WorkPeriod == null))
                //{
                //}
                //else
                //{
                //    tmpBuild.Append("'" + WorkPeriod + "'");
                //    tmpBuild.Append(",");
                //}

                if ((this.WorkNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + WorkNote + "'");
                    tmpBuild.Append(",");
                }

                if ((this.WorkCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + WorkCreateTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.WorkModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("'" + WorkModifyTime + "'");
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
                tmpBuild.Append("update KPI_Work set ");
                               

                if ((this.WorkName == null))
                {
                }
                else
                {
                    tmpBuild.Append("WorkName='" + WorkName + "'");
                    tmpBuild.Append(",");
                }

                if ((this.WorkDesc == null))
                {
                }
                else
                {
                    tmpBuild.Append("WorkDesc='" + WorkDesc + "'");
                    tmpBuild.Append(",");
                }
                
                if ((this.WorkStartTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("WorkStartTime='" + WorkStartTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.WorkEndTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("WorkEndTime='" + WorkEndTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.WorkBaseShifts == Int32.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("WorkBaseShifts=" + WorkBaseShifts.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.WorkBaseDays == Int32.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("WorkBaseDays=" + WorkBaseDays.ToString());
                    tmpBuild.Append(",");
                }

                if ((this.WorkSequence == null))
                {
                }
                else
                {
                    tmpBuild.Append("WorkSequence='" + WorkSequence + "'");
                    tmpBuild.Append(",");
                }


                if ((this.WorkShift == null))
                {
                }
                else
                {
                    tmpBuild.Append("WorkShift='" + WorkShift + "'");
                    tmpBuild.Append(",");
                }


                if ((this.WorkIsValid == Int32.MinValue))
                {
                }
                else
                {
                    tmpBuild.Append("WorkIsValid=" + WorkIsValid.ToString());
                    tmpBuild.Append(",");
                }
                //if ((this.WorkPeriod == null))
                //{
                //}
                //else
                //{
                //    tmpBuild.Append("WorkPeriod='" + WorkPeriod + "'");
                //    tmpBuild.Append(",");
                //}

                if ((this.WorkNote == null))
                {
                }
                else
                {
                    tmpBuild.Append("WorkNote='" + WorkNote + "'");
                    tmpBuild.Append(",");
                }

                if ((this.WorkCreateTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("WorkCreateTime='" + WorkCreateTime + "'");
                    tmpBuild.Append(",");
                }

                if ((this.WorkModifyTime == null))
                {
                }
                else
                {
                    tmpBuild.Append("WorkModifyTime='" + WorkModifyTime + "'");
                    tmpBuild.Append(",");
                }

                if ((tmpBuild[(tmpBuild.Length - 1)] == ','))
                {
                    tmpBuild.Remove((tmpBuild.Length - 1), 1);
                }
                tmpBuild.Append(" where ");
                tmpBuild.Append("WorkID='" + WorkID + "'");

                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }

        public override string DeleteSql
        {
            get
            {
                System.Text.StringBuilder tmpBuild = new System.Text.StringBuilder();
                tmpBuild.Append("delete KPI_Work");
                tmpBuild.Append(" where ");
                tmpBuild.Append("WorkID='" + WorkID + "'");
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
                tmpBuild.Append("WorkID");
                tmpBuild.Append(",");
                tmpBuild.Append("WorkName");
                tmpBuild.Append(",");
                tmpBuild.Append("WorkDesc");
                tmpBuild.Append(",");
                tmpBuild.Append("WorkStartTime");
                tmpBuild.Append(",");
                tmpBuild.Append("WorkEndTime");
                tmpBuild.Append(",");
                tmpBuild.Append("WorkBaseShifts");
                tmpBuild.Append(",");
                tmpBuild.Append("WorkBaseDays");
                tmpBuild.Append(",");
                tmpBuild.Append("WorkSequence");
                tmpBuild.Append(",");
                tmpBuild.Append("WorkShift");
                tmpBuild.Append(",");
                tmpBuild.Append("WorkIsValid");
                tmpBuild.Append(",");
                //tmpBuild.Append("WorkPeriod");
                //tmpBuild.Append(",");
                tmpBuild.Append("WorkNote");
                tmpBuild.Append(",");
                tmpBuild.Append("WorkCreateTime");
                tmpBuild.Append(",");
                tmpBuild.Append("WorkModifyTime");

                /****/

                tmpBuild.Append(" from KPI_Work");
                tmpBuild.Append(" where ");
                tmpBuild.Append("WorkID='" + WorkID + "'");
                string __tmpSql = tmpBuild.ToString();
                return __tmpSql;
            }
        }

        public override bool DrToMember(System.Data.DataRow dr)
        {
            try
            {
                if ((dr["WorkID"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.WorkID = dr["WorkID"].ToString();
                }
                if ((dr["WorkName"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.WorkName = dr["WorkName"].ToString();
                }
                if ((dr["WorkDesc"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.WorkDesc = dr["WorkDesc"].ToString();
                }

                if ((dr["WorkStartTime"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.WorkStartTime = dr["WorkStartTime"].ToString();
                }
                if ((dr["WorkEndTime"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.WorkEndTime = dr["WorkEndTime"].ToString();
                }
                if ((dr["WorkBaseShifts"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.WorkBaseShifts = Int32.Parse(dr["WorkBaseShifts"].ToString());
                }
                if ((dr["WorkBaseDays"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.WorkBaseDays = Int32.Parse(dr["WorkBaseDays"].ToString());
                }
                if ((dr["WorkSequence"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.WorkSequence = dr["WorkSequence"].ToString();
                }
                
                if ((dr["WorkShift"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.WorkShift = dr["WorkShift"].ToString();
                }


                if ((dr["WorkIsValid"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.WorkIsValid = Int32.Parse(dr["WorkIsValid"].ToString());
                }
                //if ((dr["WorkPeriod"] == System.DBNull.Value))
                //{
                //}
                //else
                //{
                //    this.WorkPeriod = dr["WorkPeriod"].ToString();
                //}
                if ((dr["WorkNote"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.WorkNote = dr["WorkNote"].ToString();
                }
                if ((dr["WorkCreateTime"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.WorkCreateTime = dr["WorkCreateTime"].ToString();
                }
                if ((dr["WorkModifyTime"] == System.DBNull.Value))
                {
                }
                else
                {
                    this.WorkModifyTime = dr["WorkModifyTime"].ToString();
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
