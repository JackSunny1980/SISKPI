using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SIS.DBControl;
using SIS.DataAccess;
using SIS.DataEntity;
 

namespace SIS.DataAccess
{
    public class KPI_LinqDal : DalBase<LinqEntity>
    {

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <returns></returns>
        public static bool DeleteLinq(string LinqID)
        {
            //删除子表中的对应指标
            string sql = "delete from KPI_LinqTag where LinqID='" + LinqID + "'";

            DBAccess.GetRelation().ExecuteNonQuery(sql);

            //删除指标
            sql = "delete from KPI_Linq ";
            if (LinqID != "")
            {
                sql += " where LinqID = '" + LinqID + "'";
            }

            return DBAccess.GetRelation().ExecuteNonQuery(sql) > 0;
        }


        /// <summary>
        /// 得到所有数据
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDataTable()
        {
            string sql = "select * from KPI_Linq";
            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }

        /// <summary>
        /// 判断名称的唯一性
        /// </summary>
        /// <param name="GroupName"></param>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public static bool LinqNameExists(string LinqName, string LinqID)
        {
            string sql = "select count(LinqID) from KPI_Linq where LinqName='{0}' and LinqID<>'{1}' ";
            sql = string.Format(sql, LinqName, LinqID);

            return int.Parse(DBAccess.GetRelation().ExecuteScalar(sql).ToString()) > 0;
        }


        /// <summary>
        /// 得到有效的定义
        /// </summary>
        /// <param name="GroupID">主键</param>
        /// <returns></returns>
        public static DataTable GetLinqs()
        {
            string sql = "select LinqID[ID], LinqName[Name]  from KPI_Linq where LinqIsValid=1";

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }

        /// <summary>
        /// 获得记录个数
        /// </summary>
        /// <returns></returns>
        public static int LinqIDCounts()
        {
            string sql = "select LinqID from KPI_Linq";

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0].Rows.Count;
        }

        /// <summary>
        /// 得到有效的定义
        /// </summary>
        /// <param name="GroupID">主键</param>
        /// <returns></returns>
        public static string GetLinqName(string LinqID)
        {
            string sql = "select LinqID[ID], LinqName[Name]  from KPI_Linq where LinqID='" + LinqID + "'";
            
            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

            if (dt.Rows.Count != 1)
            {
                return "";
            }
            else
            {
                return dt.Rows[0]["Name"].ToString();
            }
        }      

        /// <summary>
        /// 通过主键得到实体对象
        /// </summary>
        /// <param name="LinqID">主键</param>
        /// <returns></returns>
        public static LinqEntity GetEntity(string LinqID)
        {
            LinqEntity entity = new LinqEntity();

            string sql = "select * from KPI_Linq where LinqID='{0}'";
            sql = string.Format(sql, LinqID);

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
        public static DataTable GetTagLists()
        {
            string sql = @"select UnitName,LinqName, a.LinqID, LinqEngunit, LinqIndex, ''LinqValue
                            from KPI_Linq a 
                            right outer join KPI_LinqTag b on a.LinqID = b.LinqID
                            left outer join KPI_ECSSSnapshot c on b.ECID = c.ECID
                            where LinqIsValid=1 order by LinqIndex ";

            //sql = string.Format(sql, condition);

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }


        /// <summary>
        /// 得到查询结果
        /// </summary>
        /// <param name="condition">条件字符串</param>
        /// <returns></returns>
        public static DataTable GetLinqSearch(string condition)
        {
            string sql = @"select LinqID, LinqName, LinqDesc, LinqEngunit, LinqIndex, LinqIsValid, LinqNote
                            from KPI_Linq order by LinqIndex ";

            sql = string.Format(sql, condition);

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }

     
    }
}
