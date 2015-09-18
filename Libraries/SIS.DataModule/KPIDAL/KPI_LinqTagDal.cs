using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SIS.DBControl;
using SIS.DataAccess;
using SIS.DataEntity;


namespace SIS.DataAccess {
    public class LinqTagDal : DalBase<LinqTagEntity> {

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <returns></returns>
        public static bool DeleteTag(string TagID) {
            //删除信息
            string sql = "delete from KPI_LinqTag ";
            if (TagID != "") {
                sql += " where TagID = '" + TagID + "'";
            }

            return DBAccess.GetRelation().ExecuteNonQuery(sql) > 0;
        }


        /// <summary>
        /// 得到所有子表数据
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDataTable() {
            string sql = "select * from KPI_LinqTag";
            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }

        /// <summary>
        /// 判断机组及指标下Tag的唯一性
        /// </summary>
        /// <param name="GroupName"></param>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public static bool TagExists(string LinqID, string UnitID) {
            string sql = "select count(TagID) from KPI_LinqTag where LinqID='{0}' and UnitID='{1}'";

            sql = string.Format(sql, LinqID, UnitID);

            return int.Parse(DBAccess.GetRelation().ExecuteScalar(sql).ToString()) > 0;
        }

        /// <summary>
        /// 判断机组及指标下Tag的唯一性
        /// </summary>
        /// <param name="GroupName"></param>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public static int GetTagNum(string ZBID) {
            string sql = "select count(TagID) from KPI_LinqTag where TagIsValid=1 and ZBID='{0}' ";
            sql = string.Format(sql, ZBID);

            return int.Parse(DBAccess.GetRelation().ExecuteScalar(sql).ToString());
        }


        /// <summary>
        /// 得到有效的定义
        /// </summary>
        /// <param name="GroupID">主键</param>
        /// <returns></returns>
        public static DataTable GetTags(string LinqID) {
            string sql = @"select TagID, UnitName, ECName 
                           from KPI_LinqTag 
                           where LinqID='{0}'";

            sql = string.Format(sql, LinqID);

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

        }

        /// <summary>
        /// 通过主键得到实体对象
        /// </summary>
        /// <param name="TagID">主键</param>
        /// <returns></returns>
        public static LinqTagEntity GetEntity(string TagID) {
            LinqTagEntity entity = new LinqTagEntity();
            string sql = "select * from KPI_LinqTag where TagID='{0}'";
            sql = string.Format(sql, TagID);

            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
            if (dt.Rows.Count > 0) {
                entity.DrToMember(dt.Rows[0]);
            }

            return entity;
        }


        /// <summary>
        /// 得到查询结果
        /// </summary>
        /// <param name="condition">条件字符串</param>
        /// <returns></returns>
        public static DataTable GetSearchList(string condition) {
            string sql = @"select TagID, UnitName, ZBName, ZBEngunit, TagTag, TagIsValid,TagNote
                            from KPI_LinqTag a 
                            left outer join RDLC_ZB b on a.ZBID=b.ZBID  
                            left outer join OPM_Unit c on a.UnitID=c.UnitID  
                            where 1=1 {0}  order by a.UnitID ";

            sql = string.Format(sql, condition);

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }

    }
}
