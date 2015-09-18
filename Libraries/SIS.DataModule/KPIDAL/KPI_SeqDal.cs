using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SIS.DBControl;
using SIS.DataAccess;
using SIS.DataEntity;
 

namespace SIS.DataAccess
{
    public class KPI_SeqDal : DalBase<KPI_SeqEntity>
    {
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <returns></returns>
        public static bool DeleteSeq(string SeqID)
        {
            //删除参数信息
            string sql = "delete from KPI_Seq ";
            if (SeqID != "")
            {
                sql += " where SeqID = '" + SeqID + "'";
            }

            return DBAccess.GetRelation().ExecuteNonQuery(sql) > 0;
        }

        /// <summary>
        /// 得到所有子表数据
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDataTable()
        {
            string sql = "select * from KPI_Seq";
            DataTable dataTable = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
            return dataTable;
        }


        /// <summary>
        /// 判断名称的唯一性
        /// </summary>
        /// <param name="SeqName"></param>
        /// <param name="SeqID"></param>
        /// <returns></returns>
        public static int SeqIDCounts()
        {
            string sql = "select SeqID from KPI_Seq";

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0].Rows.Count;
        }

        /// <summary>
        /// 判断名称的唯一性
        /// </summary>
        /// <param name="SeqName"></param>
        /// <param name="SeqID"></param>
        /// <returns></returns>
        public static bool SeqCodeExists(string SeqCode, string SeqID)
        {
            string sql = "select count(1) from KPI_Seq where 1=1 and SeqCode='{0}' ";
            sql = string.Format(sql, SeqCode);

            if (SeqID != "")
            {
                sql = sql + " and SeqID <> '" + SeqID + "'";
            }

            return int.Parse(DBAccess.GetRelation().ExecuteScalar(sql).ToString()) > 0;
        }

        /// <summary>
        /// 获得所有电厂ID、Name
        /// </summary>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public static DataTable GetSeqs()
        {
            string sql = "select SeqID[ID], SeqName[Name] from KPI_Seq where SeqIsValid=1 ";

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }

        /// <summary>
        /// 获得所有电厂ID、Name
        /// </summary>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public static string GetSeqID(string SeqName)
        {
            string sql = "select SeqID[ID] from KPI_Seq where SeqIsValid=1 and SeqName='" + SeqName + "'";

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
        /// <param name="SeqID">主键</param>
        /// <returns></returns>
        public static KPI_SeqEntity GetEntity(string SeqID)
        {
            KPI_SeqEntity entity = new KPI_SeqEntity();

            string sql = "select * from KPI_Seq where SeqID='{0}'";
            sql = string.Format(sql, SeqID);

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
            string sql = @"select SeqID, SeqCode, SeqName, SeqIndex, SeqDesc, SeqIsValid, SeqNote 
                            from KPI_Seq a 
                            where 1=1 {0}";

            sql = string.Format(sql, condition);

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 获得全部实体对象
        /// </summary>
        /// <returns></returns>
        public static List<KPI_SeqEntity> GetAllEntity()
        {
            List<KPI_SeqEntity> ltSeqs = new List<KPI_SeqEntity>();

            string sql = "select * from KPI_Seq";

            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                KPI_SeqEntity entity = new KPI_SeqEntity();
                entity.DrToMember(dr);

                ltSeqs.Add(entity);
            }

            return ltSeqs;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 获得有效实体对象
        /// </summary>
        /// <returns></returns>
        public static List<String> GetValidEntity()
        {
            List<String> ltSeqs = new List<String>();

            string sql = @"select * from KPI_Seq
                            where SeqIsValid=1";

            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                KPI_SeqEntity entity = new KPI_SeqEntity();
                entity.DrToMember(dr);

                ltSeqs.Add(entity.SeqID);
            }

            return ltSeqs;
        }

    }
}
