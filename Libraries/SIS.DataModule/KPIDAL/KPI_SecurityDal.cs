using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SIS.DBControl;
using SIS.DataAccess;
using SIS.DataEntity;
 

namespace SIS.DataAccess
{
    public class KPI_SecurityDal : DalBase<KPI_SecurityEntity>
    {
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <returns></returns>
        public static bool DeleteSecurity(string SecurityID)
        {
            //删除参数信息
            string sql = "delete from KPI_Security ";
            if (SecurityID != "")
            {
                sql += " where SecurityID = '" + SecurityID + "'";
            }

            return DBAccess.GetRelation().ExecuteNonQuery(sql) > 0;
        }

        /// <summary>
        /// 判断名称的唯一性
        /// </summary>
        /// <param name="SeqName"></param>
        /// <param name="SeqID"></param>
        /// <returns></returns>
        public static int SecurityIDCounts(string said)
        {
            string sql = "select SecurityID from KPI_Security where SAID='"+said+"'";

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0].Rows.Count;
        }

        /// <summary>
        /// 得到所有子表数据
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDataTable()
        {
            string sql = "select * from KPI_Security";
            DataTable dataTable = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
            return dataTable;
        }

        ///// <summary>
        ///// 判断名称的唯一性
        ///// </summary>
        ///// <param name="SecurityName"></param>
        ///// <param name="SecurityID"></param>
        ///// <returns></returns>
        //public static bool SecurityNameExists(string SecurityName, string SecurityID)
        //{
        //    string sql = "select count(1) from KPI_Security where SecurityName='{0}' ";
        //    sql = string.Format(sql, SecurityName);

        //    if (SecurityID != "")
        //    {
        //        sql = sql + " and SecurityID <> '" + SecurityID + "'";
        //    }

        //    return int.Parse(DBAccess.GetRelation().ExecuteScalar(sql).ToString()) > 0;
        //}

        ///// <summary>
        ///// 获得所有电厂ID、Name
        ///// </summary>
        ///// <param name="GroupID"></param>
        ///// <returns></returns>
        //public static DataTable GetSecuritys()
        //{
        //    string sql = "select SecurityID[ID], SecurityName[Name] from KPI_Security";

        //    return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        //}

        ///// <summary>
        ///// 获得所有电厂ID、Name
        ///// </summary>
        ///// <param name="GroupID"></param>
        ///// <returns></returns>
        //public static string GetSecurityID(string SecurityName)
        //{
        //    string sql = "select SecurityID[ID] from KPI_Security where SecurityIsValid=1 and SecurityName='" + SecurityName + "'";

        //    DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

        //    if (dt.Rows.Count != 1)
        //    {
        //        return "";
        //    }
        //    else
        //    {
        //        return dt.Rows[0]["ID"].ToString();
        //    }
        //}


        ///// <summary>
        ///// 获得所有电厂ID、Name
        ///// </summary>
        ///// <param name="GroupID"></param>
        ///// <returns></returns>
        //public static double GetSecurityValue(string SecurityName)
        //{
        //    string sql = "select SecurityValue from KPI_Security where SecurityName='" + SecurityName + "'";

        //    DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

        //    if (dt.Rows.Count != 1)
        //    {
        //        return 0;
        //    }
        //    else
        //    {
        //        return double.Parse(dt.Rows[0]["SecurityValue"].ToString());
        //    }
        //}     


        /// <summary>
        /// 通过主键得到实体对象
        /// </summary>
        /// <param name="SecurityID">主键</param>
        /// <returns></returns>
        public static KPI_SecurityEntity GetEntity(string SecurityID)
        {
            KPI_SecurityEntity entity = new KPI_SecurityEntity();

            string sql = "select * from KPI_Security where SecurityID='{0}'";
            sql = string.Format(sql, SecurityID);

            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                entity.DrToMember(dt.Rows[0]);
            }

            return entity;
        }


        /// <summary>
        /// 得到查询结果
        /// </summary>
        /// <param name="condition">条件字符串</param>
        /// <returns></returns>
        public static DataTable GetSecurityList(string said)
        {
            string sql = @"select SecurityID, SAID, b.SAName SecurityCalcExp, SecurityGainExp, SecurityOptimal, SecurityAlarm, SecurityIsValid, SecurityNote
                            from KPI_Security a
                            left outer join KPI_SATag b on a.SAID=b.SAID
                            where SAID='" + said + "'";

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }


        /// <summary>
        /// 得到查询结果
        /// </summary>
        /// <param name="condition">条件字符串</param>
        /// <returns></returns>
        public static DataTable GetSearchList()
        {
            string sql = @"select *  from KPI_Security";

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }

    }
}
