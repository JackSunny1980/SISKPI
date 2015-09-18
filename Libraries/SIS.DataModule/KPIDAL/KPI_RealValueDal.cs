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
    public class KPI_RealValueDal : DalBase<ValueEntity>
    {

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <returns></returns>
        public static bool DeleteTag(string RVID)
        {
            string sql = "delete from KPI_RealValue ";
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
            string sql = "delete from KPI_RealValue ";
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
        public static List<ValueEntity> GetValuesByTime(string UnitID)
        {
            List<ValueEntity> lrv = new List<ValueEntity>();

            string sql = @"select * from KPI_RealValue ";
            sql += " where UnitID = '" + UnitID + "'";


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
            string sql = "select * from KPI_RealValue";
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
            string sql = "select count(1) from KPI_RealValue where RealCode='{0}' and RealTime='{1}'";
            sql = string.Format(sql, RealCode, RealTime);


            return int.Parse(DBAccess.GetRelation().ExecuteScalar(sql).ToString()) > 0;
        }

        public static DataTable GetRealValue(string plantid="", string unitid="")
        {                       
            string sql = @"select a.RealID, a.RealCode, a.RealDesc,  a.RealEngunit, RealValue, RealTime
                        from KPI_RealValue a
                        right outer join KPI_Unit b on (a.UnitID = b.UnitID {0})
                        right outer join KPI_Plant c on (b.PlantID = c.PlantID  {1} )
                        order by RealTime desc, UnitCode  ";


            string condition1 = "";
            if (unitid != "")
            {
                condition1 += " and b.UnitID = '" + unitid + "'";
            }

            string condition2 = "";
            if (plantid != "")
            {
                condition2 = " and c.PlantID = '" + plantid + "'";
            }

            sql = string.Format(sql, condition1, condition2);

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];            
        }   
    }
}
