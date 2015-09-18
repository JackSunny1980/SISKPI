using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SIS.DBControl;
using SIS.DataAccess;
using SIS.DataEntity;
 

namespace SIS.DataAccess
{
    public class KPI_PlantDal : DalBase<KPI_PlantEntity>
    {
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <returns></returns>
        public static bool DeletePlant(string PlantID)
        {
            //删除参数信息
            string sql = "delete from KPI_Plant ";
            if (PlantID != "")
            {
                sql += " where PlantID = '" + PlantID + "'";
            }

            return DBAccess.GetRelation().ExecuteNonQuery(sql) > 0;
        }

        /// <summary>
        /// 得到所有子表数据
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDataTable()
        {
            string sql = "select * from KPI_Plant";
            DataTable dataTable = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
            return dataTable;
        }

        /// <summary>
        /// 判断名称的唯一性
        /// </summary>
        /// <param name="PlantName"></param>
        /// <param name="PlantID"></param>
        /// <returns></returns>
        public static bool PlantCodeExists(string PlantCode, string PlantID)
        {
            string sql = "select count(1) from KPI_Plant where 1=1 and PlantCode='{0}' ";
            sql = string.Format(sql, PlantCode);

            if (PlantID != "")
            {
                sql = sql + " and PlantID <> '" + PlantID + "'";
            }

            return int.Parse(DBAccess.GetRelation().ExecuteScalar(sql).ToString()) > 0;
        }

        /// <summary>
        /// 获得所有电厂ID、Name
        /// </summary>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public static DataTable GetPlants(string GroupID)
        {
            string sql = @"select PlantID[ID], PlantName[Name] from KPI_Plant
                        where PlantIsValid=1 {0} 
                        order by PlantCode";

            string condition = "";

            if (GroupID != "")
            {
                condition = " and GroupID='" + GroupID + "'";
            }

            sql = string.Format(sql, condition);


            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }

        /// <summary>
        /// 获得所有电厂ID、Name
        /// </summary>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public static string GetPlantID(string PlantName)
        {
            string sql = "select PlantID[ID] from KPI_Plant where PlantIsValid=1 and PlantName='" + PlantName + "'";

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
        /// 获得所有电厂ID、Name
        /// </summary>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public static string GetPlantIDByCode(string PlantCode)
        {
            string sql = "select PlantID[ID] from KPI_Plant where PlantIsValid=1 and PlantCode='" + PlantCode + "'";

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
        /// <param name="PlantID">主键</param>
        /// <returns></returns>
        public static KPI_PlantEntity GetEntity(string PlantID)
        {
            KPI_PlantEntity entity = new KPI_PlantEntity();

            string sql = "select * from KPI_Plant where PlantID='{0}'";
            sql = string.Format(sql, PlantID);

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
            string sql = @"select PlantID, PlantName, PlantCode, PlantIndex, PlantDesc, PlantIsValid, PlantAddress, PlantNote 
                            from KPI_Plant a 
                            where 1=1 {0}";

            sql = string.Format(sql, condition);

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }

    }
}
