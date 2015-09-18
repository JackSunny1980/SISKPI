using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SIS.DBControl;
using SIS.DataAccess;
using SIS.DataEntity;

using SIS.Loger;

namespace SIS.DataAccess
{
    public class ArchiveValueDal : DalBase<ValueEntity>
    {

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <returns></returns>
        public static bool DeleteTag(string RVID)
        {
            string sql = "delete from KPI_ArchiveValue ";
            if (RVID != "")
            {
                sql += " where RVID = '" + RVID + "'";
            }

            return DBAccess.GetRelation().ExecuteNonQuery(sql) > 0;
        } 
        
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <returns></returns>
        public static bool DeleteTagByTime(string UnitID, string RealTime)
        {
            string sql = "delete from KPI_ArchiveValue ";
            if (RealTime != "")
            {
                sql += " where UnitID='"+ UnitID+ "' and RealTime = '" + RealTime + "'";
            }

            return DBAccess.GetRelation().ExecuteNonQuery(sql) > 0;
        }


        /// <summary>
        /// 查询数据
        /// </summary>
        /// <returns></returns>
        public static List<ValueEntity> GetValuesByTime(string UnitID, string RealTime)
        {
            List<ValueEntity> lrv = new List<ValueEntity>();

            string sql = @"select * from KPI_ArchiveValue ";
            sql += " where UnitID = '" + UnitID + "' and RealTime = '" + RealTime + "'";


            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ValueEntity entity = new ValueEntity();
                    entity.DrToMember(dt.Rows[0]);

                    lrv.Add(entity);
                }
            }


            return lrv;
        }
        

        /// <summary>
        /// 得到所有子表数据
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDataTable()
        {
            string sql = "select * from KPI_ArchiveValue";
            DataTable dataTable = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
            return dataTable;
        }

        /// <summary>
        /// 判断名称的唯一性
        /// </summary>
        /// <param name="TagName"></param>
        /// <param name="TagID"></param>
        /// <returns></returns>
        public static bool RVExists(string RealCode, string RealTime)
        {
            string sql = "select count(1) from KPI_ArchiveValue where RealCode='{0}' and RealTime='{1}'";
            sql = string.Format(sql, RealCode, RealTime);


            return int.Parse(DBAccess.GetRelation().ExecuteScalar(sql).ToString()) > 0;
        }

        /// <summary>
        /// 得到相关时间段内的记录
        /// </summary>
        /// <returns></returns>
        public static DataTable GetRecords(string realid, string strStartTime, string strEndTime)
        {
            string sql = @"select * from KPI_ArchiveValue 
                        where RealID='{0}'  {1} 
                        order by RealTime ";

            string strfilter = "";
            if (strStartTime != "")
            {
                strfilter = " and RealTime>='" + strStartTime + "'";
            }

            if (strEndTime != "")
            {
                strfilter += " and RealTime<='" + strEndTime + "'";
            }

            sql = string.Format(sql, realid, strfilter);

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }

        /// <summary>
        /// 得到相关时间段内的记录
        /// </summary>
        /// <returns></returns>
        public static DataTable GetRecordsFromView(string realid, string strStartTime, string strEndTime)
        {
            string sql = @"select * from KPI_ArchiveValue_View_1day 
                        where RealID='{0}'  {1} 
                        order by RealTime ";

            string strfilter = "";
            if (strStartTime != "")
            {
                strfilter = " and RealTime>='" + strStartTime + "'";
            }

            if (strEndTime != "")
            {
                strfilter += " and RealTime<='" + strEndTime + "'";
            }

            sql = string.Format(sql, realid, strfilter);

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }



    }
}
