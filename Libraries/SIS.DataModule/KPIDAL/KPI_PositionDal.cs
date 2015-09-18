using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SIS.DBControl;
using SIS.DataAccess;
using SIS.DataEntity;
 

namespace SIS.DataAccess
{
    public class KPI_PositionDal : DalBase<KPI_PositionEntity>
    {
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <returns></returns>
        public static bool DeletePosition(string PositionID)
        {
            //删除参数信息
            string sql = "delete from KPI_Position ";
            if (PositionID != "")
            {
                sql += " where PositionID = '" + PositionID + "'";
            }

            return DBAccess.GetRelation().ExecuteNonQuery(sql) > 0;
        }

        /// <summary>
        /// 判断名称的唯一性
        /// </summary>
        /// <param name="SeqName"></param>
        /// <param name="SeqID"></param>
        /// <returns></returns>
        public static int PositionIDCounts()
        {
            string sql = "select PositionID from KPI_Position";

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0].Rows.Count;
        }

        /// <summary>
        /// 得到所有子表数据
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDataTable()
        {
            string sql = "select * from KPI_Position";
            DataTable dataTable = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
            return dataTable;
        }

        /// <summary>
        /// 判断名称的唯一性
        /// </summary>
        /// <param name="SeqName"></param>
        /// <param name="SeqID"></param>
        /// <returns></returns>
        public static bool PositionNameExists(string PositionName, string PositionID)
        {
            string sql = "select count(1) from KPI_Position where 1=1 and PositionName='{0}' ";
            sql = string.Format(sql, PositionName);

            if (PositionID != "")
            {
                sql = sql + " and PositionID <> '" + PositionID + "'";
            }

            return int.Parse(DBAccess.GetRelation().ExecuteScalar(sql).ToString()) > 0;
        }


        /// <summary>
        /// 获得所有电厂ID、Name
        /// </summary>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public static DataTable GetPositions()
        {
            string sql = "select PositionID[ID], PositionName[Name] from KPI_Position where PositionIsValid=1 ";

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }

        /// <summary>
        /// 获得所有电厂ID、Name
        /// </summary>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public static DataSet GetPositionsDataSet()
        {
            string sql = "select PositionID[ID], PositionName[Name] from KPI_Position where PositionIsValid=1 ";

            return DBAccess.GetRelation().ExecuteDataset(sql);
        }
        /// <summary>
        /// 获得所有电厂ID、Name
        /// </summary>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public static string GetPositionID(string PositionName)
        {
            string sql = "select PositionID[ID] from KPI_Position where PositionIsValid=1 and PositionName='" + PositionName + "'";

            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

            if (dt.Rows.Count != 1)
            {
                return "";
            }
            else
            {
                return dt.Rows[0]["ID"].ToString();
            }


        }     


        /// <summary>
        /// 通过主键得到实体对象
        /// </summary>
        /// <param name="PositionID">主键</param>
        /// <returns></returns>
        public static KPI_PositionEntity GetEntity(string PositionID)
        {
            KPI_PositionEntity entity = new KPI_PositionEntity();

            string sql = "select * from KPI_Position where PositionID='{0}'";
            sql = string.Format(sql, PositionID);

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
        public static DataTable GetSearchList(string condition)
        {
            string sql = @"select PositionID, PositionName, PositionDesc, PositionWeight,
                                PositionIsHand, PositionIsShift, PositionIsValid, PositionNote 
                            from KPI_Position a 
                            where 1=1 {0}";

            sql = string.Format(sql, condition);

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }

    }
}
