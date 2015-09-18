using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SIS.DBControl;
using SIS.DataAccess;
using SIS.DataEntity;
 

namespace SIS.DataAccess
{
    public class KPI_TeamDal : DalBase<KPI_TeamEntity>
    {

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <returns></returns>
        public static bool DeleteTeam(string TeamID)
        {
            //删除参数信息
            string sql = "delete from KPI_Team ";
            if (TeamID != "")
            {
                sql += " where TeamID = '" + TeamID + "'";
            }

            return DBAccess.GetRelation().ExecuteNonQuery(sql) > 0;
        }


        /// <summary>
        /// 判断名称的唯一性
        /// </summary>
        /// <param name="SeqName"></param>
        /// <param name="SeqID"></param>
        /// <returns></returns>
        public static int TeamIDCounts()
        {
            string sql = "select TeamID from KPI_Team";

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0].Rows.Count;
        }


        /// <summary>
        /// 得到所有子表数据
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDataTable()
        {
            string sql = "select * from KPI_Team";
            DataTable dataTable = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
            return dataTable;
        }

        /// <summary>
        /// 获得名称
        /// </summary>
        /// <param name="TeamName"></param>
        /// <param name="TeamID"></param>
        /// <returns></returns>
        public static string GetTeamID(string TeamName)
        {
            string sql = "select TeamID, TeamName from KPI_Team where TeamName='{0}' ";
            sql = string.Format(sql, TeamName);

            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

            if (dt == null || dt.Rows.Count <= 0)
            {
                return "";
            }
            else
            {
                return dt.Rows[0]["TeamID"].ToString();
            }
        }


        /// <summary>
        /// 通过主键得到实体对象
        /// </summary>
        /// <param name="TeamID">主键</param>
        /// <returns></returns>
        public static KPI_TeamEntity GetEntity(string TeamID)
        {
            KPI_TeamEntity entity = new KPI_TeamEntity();

            string sql = "select * from KPI_Team where TeamID='{0}'";
            sql = string.Format(sql, TeamID);

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
        public static DataTable GetTeamList()
        {
            string sql = @"select TeamID, a.PlantID, a.ShiftID, a.PersonID, a.PositionID, 
                            TeamPersonID, TeamNote
                            from KPI_Team a 
                         left outer join KPI_Shift b on b.ShiftID =a.ShiftID
                         left outer join KPI_Person c on c.PersonID =a.PersonID
                         left outer join KPI_Position d on d.PositionID =a.PositionID
                         left outer join KPI_Plant e on e.PlantID =a.PlantID
                         order by b.ShiftName, d.PositionID";
            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }


        /// <summary>
        /// 得到查询结果
        /// </summary>
        /// <param name="condition">条件字符串</param>
        /// <returns></returns>
        public static DataTable GetTeamListByTime()
        {
            string sql = @"select TeamID, a.PlantID, a.ShiftID, a.PersonID, a.PositionID, 
                            TeamPersonID, TeamNote
                            from KPI_Team a 
                         left outer join KPI_Shift b on b.ShiftID =a.ShiftID
                         left outer join KPI_Person c on c.PersonID =a.PersonID
                         left outer join KPI_Position d on d.PositionID =a.PositionID
                         left outer join KPI_Plant e on e.PlantID =a.PlantID
                         order by a.TeamCreateTime";
            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }



        /// <summary>
        /// 得到查询结果
        /// </summary>
        /// <param name="condition">条件字符串</param>
        /// <returns></returns>
        public static DataTable GetSearchList(string condition)
        {
            string sql = @"select a.TeamID,  TeamName, ShiftName, PositionName, PositionWeight, 
                            ''ECScore, ''SASCore, ''AllScore, ''TeamSort
                            from KPI_Team  a
                            left outer join KPI_Position b on b.PositionID = a.PositionID
                            left outer join KPI_Team c on c.TeamID = a.TeamID
                            left outer join KPI_Shift d on d.ShiftID = c.ShiftID
                            where 1=1 {0}";

            sql = string.Format(sql, condition);

            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

            return dt;
        }

        /// <summary>
        /// 得到人员列表为导出使用
        /// </summary>
        /// <returns></returns>       
        public static DataTable GetPersonListForExcel()
        {
            string sql = @"select 'x'SelectX, PlantName, ShiftName, PersonCode, PersonName, PositionName, TeamNote
                            from KPI_Team a 
                         left outer join KPI_Shift b on b.ShiftID =a.ShiftID
                         left outer join KPI_Person c on c.PersonID =a.PersonID
                         left outer join KPI_Position d on d.PositionID =a.PositionID
                         left outer join KPI_Plant e on e.PlantID =a.PlantID
                         order by a.TeamCreateTime";

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

        }   

    }
}
