using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.DataEntity
{
    /// <summary>
    /// 实体类基类
    /// </summary>
    [System.Serializable()]
    public abstract class EntityBase
    {
        public virtual string InsertSql
        {
            get;
            set;
        }
        public virtual string UpdateSql
        {
            get;
            set;
        }
        public virtual string DeleteSql
        {
            get;
            set;
        }

        public virtual string SelectSql
        {
            get;
            set;
        }

        public abstract bool DrToMember(System.Data.DataRow dr);
    }
}
