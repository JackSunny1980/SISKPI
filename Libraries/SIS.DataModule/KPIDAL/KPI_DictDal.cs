using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SIS.DBControl;
using SIS.DataAccess;
using SIS.DataEntity;
 

namespace SIS.DataAccess
{
    public class DictDal : DalBase<DictEntity>
    {
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <returns></returns>
        public static bool DeleteDict(string DictID)
        {
            //删除参数信息
            string sql = "delete from KPI_Dict ";
            if (DictID != "")
            {
                sql += " where DictID = '" + DictID + "'";
            }

            return DBAccess.GetRelation().ExecuteNonQuery(sql) > 1;
        }

        /// <summary>
        /// 判断名称的唯一性
        /// </summary>
        /// <param name="SeqName"></param>
        /// <param name="SeqID"></param>
        /// <returns></returns>
        public static int DictIDCounts()
        {
            string sql = "select DictID from KPI_Dict";

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0].Rows.Count;
        }

        /// <summary>
        /// 得到所有子表数据
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDataTable()
        {
            string sql = "select * from KPI_Dict";
            DataTable dataTable = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
            return dataTable;
        }

        /// <summary>
        /// 判断名称的唯一性
        /// </summary>
        /// <param name="DictName"></param>
        /// <param name="DictID"></param>
        /// <returns></returns>
        public static bool DictNameExists(string DictName, string DictCode)
        {
            string sql = "select count(1) from KPI_Dict where 1=1 and DictName='{0}' ";
            sql = string.Format(sql, DictName);

            if (DictCode != "")
            {
                sql = sql + " and DictCode='" + DictCode + "'";
            }

            return int.Parse(DBAccess.GetRelation().ExecuteScalar(sql).ToString()) > 0;
        }

        /// <summary>
        /// 获得所有电厂ID、Name
        /// </summary>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public static DataTable GetDicts()
        {
            string sql = "select DictID[ID], DictName[Name] from KPI_Dict";

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }

        /// <summary>
        /// 获得所有电厂ID、Name
        /// </summary>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public static DataTable GetECWebs()
        {
            string sql = @"select DictName[Name], DictValue[Value]
                                  from KPI_Dict 
                                  where DictCode='kpiecweb'";

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }


        /// <summary>
        /// 获得所有电厂ID、Name
        /// </summary>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public static DataTable GetSAWebs()
        {
            string sql = @"select DictName[Name], DictValue[Value]
                                  from KPI_Dict 
                                  where DictCode='kpisaweb'";

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }

        /// <summary>
        /// 获得所有电厂ID、Name
        /// </summary>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public static string GetDictID(string DictName)
        {
            string sql = "select DictID[ID] from KPI_Dict where DictName='" + DictName + "'";

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
        /// <param name="DictID">主键</param>
        /// <returns></returns>
        public static DictEntity GetEntity(string DictID)
        {
            DictEntity entity = new DictEntity();

            string sql = "select * from KPI_Dict where DictID='{0}'";
            sql = string.Format(sql, DictID);

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
        public static DataTable GetDictList()
        {
            string sql = @"select DictID, DictCode, DictName, DictDesc, DictValue, DictNote 
                            from KPI_Dict ";

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }

    }
}
