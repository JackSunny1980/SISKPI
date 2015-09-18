using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SIS.DBControl;
using SIS.DataAccess;
using SIS.DataEntity;
 

namespace SIS.DataAccess
{
    public class KPI_ScoreDal : DalBase<KPI_ScoreEntity>
    {
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <returns></returns>
        public static bool DeleteScore(string ScoreID)
        {
            //删除参数信息
            string sql = "delete from KPI_Score ";
            if (ScoreID != "")
            {
                sql += " where ScoreID = '" + ScoreID + "'";
            }

            return DBAccess.GetRelation().ExecuteNonQuery(sql) > 0;
        }

        /// <summary>
        /// 判断名称的唯一性
        /// </summary>
        /// <param name="SeqName"></param>
        /// <param name="SeqID"></param>
        /// <returns></returns>
        public static int ScoreIDCounts(string ecid)
        {
            string sql = "select ScoreID from KPI_Score where ECID='"+ecid+"'";

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0].Rows.Count;
        }

        /// <summary>
        /// 得到所有子表数据
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDataTable()
        {
            string sql = "select * from KPI_Score";
            DataTable dataTable = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
            return dataTable;
        }

        /// <summary>
        /// 判断名称的唯一性
        /// </summary>
        /// <param name="ScoreName"></param>
        /// <param name="ScoreID"></param>
        /// <returns></returns>
        public static bool UpdateScoreXLineID(string ECID, int ScoreType, string XLineID)
        {
            string sql = "update KPI_Score set ScoreType={0}, XLineID='{1}' where ECID='{2}' ";
            sql = string.Format(sql, ScoreType, XLineID, ECID);
            
            //有可能还没有配置考核与得分区间,所以需要==0
            return DBAccess.GetRelation().ExecuteNonQuery(sql) >= 0;
        }

        /// <summary>
        /// 判断名称的唯一性
        /// </summary>
        /// <param name="ScoreName"></param>
        /// <param name="ScoreID"></param>
        /// <returns></returns>
        public static bool ScoreCalcExpExits(string ECID, string ScoreCalcExp)
        {
            string sql = "select count(1) from KPI_Score where  ECID='{0}' and Lower(ScoreCalcExp)='{1}' ";
            sql = string.Format(sql, ECID, ScoreCalcExp.ToLower());

            return int.Parse(DBAccess.GetRelation().ExecuteScalar(sql).ToString()) > 0;
        }

        ///// <summary>
        ///// 获得所有电厂ID、Name
        ///// </summary>
        ///// <param name="GroupID"></param>
        ///// <returns></returns>
        //public static DataTable GetScores()
        //{
        //    string sql = "select ScoreID[ID], ScoreName[Name] from KPI_Score";

        //    return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        //}

        ///// <summary>
        ///// 获得所有电厂ID、Name
        ///// </summary>
        ///// <param name="GroupID"></param>
        ///// <returns></returns>
        //public static string GetScoreID(string ScoreName)
        //{
        //    string sql = "select ScoreID[ID] from KPI_Score where ScoreIsValid=1 and ScoreName='" + ScoreName + "'";

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
        //public static double GetScoreValue(string ScoreName)
        //{
        //    string sql = "select ScoreValue from KPI_Score where ScoreName='" + ScoreName + "'";

        //    DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

        //    if (dt.Rows.Count != 1)
        //    {
        //        return 0;
        //    }
        //    else
        //    {
        //        return double.Parse(dt.Rows[0]["ScoreValue"].ToString());
        //    }
        //}     


        /// <summary>
        /// 通过主键得到实体对象
        /// </summary>
        /// <param name="ScoreID">主键</param>
        /// <returns></returns>
        public static KPI_ScoreEntity GetEntity(string ScoreID)
        {
            KPI_ScoreEntity entity = new KPI_ScoreEntity();

            string sql = "select * from KPI_Score where ScoreID='{0}'";
            sql = string.Format(sql, ScoreID);

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
        public static DataTable GetScoreList(string ecid)
        {
            string sql = @"select ScoreID, a.ECID, b.ECName, ScoreCalcExp, ScoreGainExp, ScoreOptimal, ScoreAlarm, ScoreIsValid, ScoreNote
                            from KPI_Score a
                            left outer join KPI_ECTag b on a.ECID=b.ECID
                            where b.ECID='{0}'
                            order by ScoreIndex ";

            sql = string.Format(sql, ecid);

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }
        

        /// <summary>
        /// 得到查询结果
        /// </summary>
        /// <param name="condition">条件字符串</param>
        /// <returns></returns>
        public static DataTable GetSearchList()
        {
            string sql = @"select *  from KPI_Score";

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 获得与主键对应的实体对象
        /// </summary>
        /// <returns></returns>
        public static List<KPI_ScoreEntity> GetAllEntity()
        {
            List<KPI_ScoreEntity> ltscs = new List<KPI_ScoreEntity>();

            string sql = "select * from KPI_Score";

            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                KPI_ScoreEntity entity = new KPI_ScoreEntity();
                entity.DrToMember(dr);

                ltscs.Add(entity);
            }

            return ltscs;
        }

    }
}
