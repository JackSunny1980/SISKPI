using System;
using System.Collections.Generic;
using System.Text;       
using System.Data;

using SIS.DataEntity;
using SIS.DataAccess;
using SIS.DBControl;
using SIS.Loger;


namespace SIS.DataAccess
{
    /// <summary>
    /// 逻辑方法基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DalBase<T> where T : EntityBase
    {
        static string  sql = "";
        public static bool Insert(T entity)
        {
            try
            {
                sql = ((EntityBase)entity).InsertSql;
                return DBAccess.GetRelation().ExecuteNonQuery(sql) > 0;
            }
            catch(Exception ex)
            {
                LogUtil.LogMessage(ex);
                return false;
            }
        }
        public static bool Update(T entity)
        {
            try
            {
                sql = ((EntityBase)entity).UpdateSql;
                return DBAccess.GetRelation().ExecuteNonQuery(sql) > 0;
            }
            catch (Exception ex)
            {
                LogUtil.LogMessage(ex);
                return false;
            }
        }
        public static bool Delete(T entity)
        {
            try
            {
                sql = ((EntityBase)entity).DeleteSql;
                return DBAccess.GetRelation().ExecuteNonQuery(sql) > 0;
            }
            catch (Exception ex)
            {
                LogUtil.LogMessage(ex);
                return false;
            }
        }

        public static DataTable GetList(string sqlString, Dictionary<string, string> condition)
        {          
            string cond = "";
            if (condition != null)
            {
                foreach (string key in condition.Keys)
                {
                    cond += " and " + key + condition[key];
                }
            }

            sqlString = string.Format(sqlString, cond);

            return DBAccess.GetRelation().ExecuteDataset(sqlString).Tables[0];
        }       
    }
}

