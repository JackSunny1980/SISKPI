using System;
using System.Collections.Generic;
using System.Text;

using SIS.DataEntity;
using System.Data;
using SIS.DBControl;

namespace SIS.DataAccess
{
    public class EngunitDal:DalBase<EngunitEntity>
    {

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <returns></returns>
        public static bool DeleteEngunit(string EngunitID)
        {
            //删除参数信息
            string sql = "delete from KPI_Engunit ";
            if (EngunitID != "")
            {
                sql += " where EngunitID = '" + EngunitID + "'";
            }

            return DBAccess.GetRelation().ExecuteNonQuery(sql) > 0;
        }

        /// <summary>
        /// 根据描述得到对象
        /// </summary>
        /// <param name="UnitChangDesc">转换单位描述</param>
        /// <returns></returns>
        public static EngunitEntity GetEntity(string EngunitName)
        {
            EngunitEntity entity = new EngunitEntity();
            
            string sql="select * from KPI_Engunit where 1=1 and EngunitName='{0}'";

            sql = string.Format(sql, EngunitName);
            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

            if (dt.Rows.Count > 0)
            {
                entity.DrToMember(dt.Rows[0]);
            }

            return entity;
        }

        public static bool EngunitExist(string EngunitName, string EngunitID)
        {
            return int.Parse(DBAccess.GetRelation().ExecuteScalar("select count(1) from KPI_Engunit where EngunitName='" + EngunitName + "' and EngunitID<>'" + EngunitID + "'").ToString()) > 0;
        }

        /// <summary>
        /// 获得所有ID、Name
        /// </summary>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public static DataTable GetEngunits()
        {
            string sql = "select EngunitID[ID], EngunitName[Name] from KPI_Engunit";

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }

        /// <summary>
        /// 获得所有电厂ID、Name
        /// </summary>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public static string GetEngunitID(string EngunitName)
        {
            string sql = "select EngunitID[ID] from KPI_Engunit where EngunitName='" + EngunitName + "'";

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
        /// 获得所有ID、Name
        /// </summary>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public static DataTable GetSearchList(string name)
        {
            string sql = "select * from KPI_Engunit";

            if (name != "")
            {
                sql += "  where EngunitName like '%" + name + "%'";
            }

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

        }


        /// <summary>
        /// 判断名称的唯一性
        /// </summary>
        /// <param name="SeqName"></param>
        /// <param name="SeqID"></param>
        /// <returns></returns>
        public static int EngunitIDCounts()
        {
            string sql = "select EngunitID from KPI_Engunit";

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0].Rows.Count;
        }

      
    }
}
