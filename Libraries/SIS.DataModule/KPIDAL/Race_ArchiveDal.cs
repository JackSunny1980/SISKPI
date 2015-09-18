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
    public class Race_ArchiveDal : DalBase<Race_ArchiveEntity>
    {

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <returns></returns>
        public static bool DeleteTag(string TagID)
        {
            string sql = "delete from Race_Archive ";
            if (TagID != "")
            {
                sql += " where TagID = '" + TagID + "'";
            }

            return DBAccess.GetRelation().ExecuteNonQuery(sql)>1;
        }


        /// <summary>
        /// 得到所有子表数据
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDataTable()
        {
            string sql = "select * from Race_Archive";
            DataTable dataTable = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
            return dataTable;
        }

        /// <summary>
        /// 判断名称的唯一性
        /// </summary>
        /// <param name="TagName"></param>
        /// <param name="TagID"></param>
        /// <returns></returns>
        public static bool TagNameExists(string TagName, string TagID)
        {
            string sql = "select count(1) from Race_Archive where TagName='{0}' ";
            sql = string.Format(sql, TagName);

            if (TagID != "")
                sql = sql + " and TagID <> '" + TagID + "'";

            return int.Parse(DBAccess.GetRelation().ExecuteScalar(sql).ToString()) > 0;
        }

        /// <summary>
        /// 通过主键得到实体对象
        /// </summary>
        /// <param name="TagID">主键</param>
        /// <returns></returns>
        public static Race_ArchiveEntity GetEntity(string TagID)
        {
            Race_ArchiveEntity entity = new Race_ArchiveEntity();

            string sql = "select * from Race_Archive where TagID='{0}'";
            sql = string.Format(sql, TagID);

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
            string sql = @"select * from Race_Archive  
                            where 1=1 {0}";

            sql = string.Format(sql, condition);

            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

            return dt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UnitID"></param>
        /// <returns></returns>
        public static DataTable GetSearchListC(string UnitID, string StartTime, string EndTime)
        {
            DataTable dt = null;

            string WorkID = KPI_UnitDal.GetWorkIDByID(UnitID);

            DataTable dtItems = Race_TagDal.GetTags(UnitID);

            if (dtItems == null && dtItems.Rows.Count <= 0)
            {
                return dt;
            }

            string condition = "";
            for (int i = 0; i < dtItems.Rows.Count; i++)
            {
                string ItemID = dtItems.Rows[i]["TagID"].ToString();
                string ItemDesc = dtItems.Rows[i]["TagDesc"].ToString();
                string ItemEngunit = dtItems.Rows[i]["TagEngunit"].ToString();

                condition += "cast(avg(case TagID when '" + ItemID + "' then TagValue else null  end) as numeric(18,4)) as '" + ItemDesc + "[" + ItemEngunit + "]', ";

            }

            condition = condition.Remove(condition.LastIndexOf(','));

            string where = "  where UnitID= '" +UnitID + "' and TagStartTime >='" + StartTime + "' and TagStartTime <'" + EndTime + "'";

            string sql = @"select TagShift As '运行值', {0}
                                from Race_Archive
                                {1}
                                group by TagShift";

            sql = string.Format(sql, condition, where);

            dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

            return dt;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UnitID"></param>
        /// <returns></returns>
        public static DataTable GetSearchListS(string UnitID, string StartTime, string EndTime)
        {
            DataTable dt  = null;

            string WorkID = KPI_UnitDal.GetWorkIDByID(UnitID);

            int nBaseShifts = KPI_WorkDal.GetBaseShifts(WorkID);
            string[] AllShifts = KPI_WorkDal.GetShift(WorkID);

            if(nBaseShifts != AllShifts.Length)
            {
                return dt;
            }

            string condition = "";
            for (int i = 0; i < nBaseShifts; i++)
            {
                string ShiftID = AllShifts[i];
                string ShiftName = KPI_ShiftDal.GetShiftName(ShiftID);

                condition += "cast(avg(case TagShift when '" + ShiftID + "' then TagValue else null  end) as numeric(18,4)) as '" + ShiftName + "', ";

            }

            condition = condition.Remove(condition.LastIndexOf(','));

            string where = " where TagStartTime>='" + StartTime + "' and TagStartTime<'" + EndTime + "'";

            string sql = @"select TagDesc As '运行参数', {0}
                                from Race_Archive
                                a left outer join Race_Tag b on a.TagID=b.TagID
                                {1}
                                group by TagDesc, TagIndex 
                                order by TagIndex";

            sql = string.Format(sql, condition, where);

            dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

            return dt;
        }

    }
}
