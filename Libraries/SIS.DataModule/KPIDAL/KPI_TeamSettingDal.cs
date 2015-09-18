using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIS.DataEntity;
using System.Data.SqlClient;
using System.Data;
using SIS.DBControl;

namespace SIS.DataAccess
{
    public class KPI_TeamSettingDal : IKPI_TeamSettingDal
    {

        private const string sql_PlantAll = "SELECT kp.PlantID,kp.PlantName FROM KPI_Plant AS kp WHERE kp.PlantIsValid=1";
        private const string sql_ShiftAll = "SELECT ks.ShiftID,ks.ShiftName FROM KPI_Shift AS ks WHERE ks.ShiftIsValid=1";
        private const string sql_PersonAll = @"SELECT kp.PersonID,kp.PositionID,kp2.PositionName, kp.PersonName FROM KPI_Person AS kp
                                                LEFT JOIN KPI_Position AS kp2 ON kp2.PositionID = kp.PositionID WHERE kp.PersonIsValid=1 AND kp2.PositionIsValid=1";
        private const string sql_TeamWithShift = "SELECT DISTINCT kt.ShiftID, ks.ShiftName,kt.PositionID FROM KPI_Team AS kt JOIN KPI_Shift AS ks ON ks.ShiftID = kt.ShiftID WHERE kt.PositionID=@PositionID";
        private const string sql_deleteTeamById = "DELETE FROM KPI_Team WHERE TeamID=@TeamID";
        private const string sql_deleteAllTeam = "DELETE FROM KPI_Team";


        private const string pro_TeamPaging = "pro_TeamPaging";
        private const string pro_InsertTeam = "pro_InsertTeamInfo";
        private const string pro_UpdateTeam = "pro_UpdateTeamInfo";

        private const string param_PageSize = "@PageSize";
        private const string param_PageIndex = "@PageIndex";
        private const string param_RowCount = "@RowCount";

        private const string param_TeamID = "@TeamID";
        private const string param_PlantID = "@PlantID";
        private const string param_ShiftID = "@ShiftID";
        private const string param_ShiftCode = "@ShiftCode";
        private const string param_PersonID = "@PersonID";
        private const string param_PositionID = "@PositionID";
        private const string param_TeamPersonID = "@TeamPersonID";
        private const string param_TeamNote = "@TeamNote";
        private const string param_Result = "@Result";



        public List<KPI_TeamEntity> GetTeamList(int startIndex, int pageSize, ref int totalCount)
        {
            SqlParameter[] parames = new SqlParameter[] {
                new SqlParameter(param_PageIndex,SqlDbType.Int),  
                new SqlParameter(param_PageSize,SqlDbType.Int),
                new SqlParameter(param_RowCount,SqlDbType.Int),
            };

            parames[0].Value = startIndex;
            parames[1].Value = pageSize;
            parames[2].Direction = ParameterDirection.Output;
            IDataReader reader = null;
            List<KPI_TeamEntity> teamList = new List<KPI_TeamEntity>();

            try
            {

                reader = DBAccess.GetRelation().ExecuteReader(CommandType.StoredProcedure, pro_TeamPaging, parames);

                while (reader.Read())
                {
                    teamList.Add(new KPI_TeamEntity
                    {
                        TeamID = reader["TeamID"].ToString(),
                        PlantID = reader["PlantID"] == DBNull.Value ? string.Empty : reader["PlantID"].ToString(),
                        PlantName = reader["PlantName"] == DBNull.Value ? string.Empty : reader["PlantName"].ToString(),
                        ShiftID = reader["ShiftID"] == DBNull.Value ? string.Empty : reader["ShiftID"].ToString(),
                        ShiftName = reader["ShiftName"] == DBNull.Value ? string.Empty : reader["ShiftName"].ToString(),
                        PersonID = reader["PersonID"] == DBNull.Value ? string.Empty : reader["PersonID"].ToString(),
                        PersonName = reader["PersonName"] == DBNull.Value ? string.Empty : reader["PersonName"].ToString(),
                        PositionID = reader["PositionID"] == DBNull.Value ? string.Empty : reader["PositionID"].ToString(),
                        PositionName = reader["PositionName"] == DBNull.Value ? string.Empty : reader["PositionName"].ToString(),
                        TeamPersonID = reader["TeamPersonID"] == DBNull.Value ? string.Empty : reader["TeamPersonID"].ToString(),
                        TeamPersonName = reader["TeamPersonName"] == DBNull.Value ? string.Empty : reader["TeamPersonName"].ToString(),
                        TeamNote = reader["TeamNote"] == DBNull.Value ? string.Empty : reader["TeamNote"].ToString(),
                        TeamCreateTime = reader["CreateTime"] == DBNull.Value ? string.Empty : reader["CreateTime"].ToString(),
                        TeamModifyTime = reader["ModifyTime"] == DBNull.Value ? string.Empty : reader["ModifyTime"].ToString()
                    });
                }

                reader.Close();
                if (parames[2].Value != DBNull.Value)
                {
                    totalCount = Convert.ToInt32(parames[2].Value);
                }
                else
                {
                    totalCount = 0;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (!reader.IsClosed)
                {
                    reader.Close();
                }
            }


            return teamList;
        }

        public KPI_TeamEntity GetTeamEntityByID(string teamId)
        {
            throw new NotImplementedException();
        }

        public List<KPI_PlantEntity> GetPlantList()
        {
            List<KPI_PlantEntity> plantList = new List<KPI_PlantEntity>();

            using (IDataReader dataReader = DBAccess.GetRelation().ExecuteReader(sql_PlantAll))
            {
                while (dataReader.Read())
                {
                    plantList.Add(new KPI_PlantEntity
                    {
                        PlantID = dataReader["PlantID"].ToString(),
                        PlantName = dataReader["PlantName"] == DBNull.Value ? string.Empty : dataReader["PlantName"].ToString()
                    });
                }
            }
            return plantList;
        }

        public List<KPI_ShiftEntity> GetShiftList()
        {
            List<KPI_ShiftEntity> shiftList = new List<KPI_ShiftEntity>();

            using (IDataReader dataReader = DBAccess.GetRelation().ExecuteReader(sql_ShiftAll))
            {
                while (dataReader.Read())
                {
                    shiftList.Add(new KPI_ShiftEntity
                    {
                        ShiftID = dataReader["ShiftID"].ToString(),
                        ShiftName = dataReader["ShiftName"] == DBNull.Value ? string.Empty : dataReader["ShiftName"].ToString()
                    });
                }
            }
            return shiftList; ;
        }

        public List<KPI_PersonEntity> GetPersonList()
        {
            List<KPI_PersonEntity> personList = new List<KPI_PersonEntity>();

            using (IDataReader dataReader = DBAccess.GetRelation().ExecuteReader(sql_PersonAll))
            {
                while (dataReader.Read())
                {
                    personList.Add(new KPI_PersonEntity
                    {
                        PersonID = dataReader["PersonID"].ToString(),
                        PersonName = dataReader["PersonName"] == DBNull.Value ? string.Empty : dataReader["PersonName"].ToString(),
                        PositionID = dataReader["PositionID"] == DBNull.Value ? string.Empty : dataReader["PositionID"].ToString(),
                        PositionName = dataReader["PositionName"] == DBNull.Value ? string.Empty : dataReader["PositionName"].ToString()
                    });
                }
            }
            return personList;
        }

        public bool Insert(KPI_TeamEntity teamEntity)
        {
            if (null == teamEntity)
                throw new ArgumentNullException("teamEntity is Null");


            string shiftCode = teamEntity.ShiftName;

            foreach (KPI_TeamEntity item in GetTeamWithShiftByPosition(teamEntity.PositionID))
            {
                if (teamEntity.ShiftID!=item.ShiftID)
                {
                    shiftCode = shiftCode + "|" + item.ShiftName;
                }
            }

            SqlParameter[] parames = new SqlParameter[] {
                new SqlParameter(param_PlantID,SqlDbType.VarChar,50),
                new SqlParameter(param_ShiftID,SqlDbType.VarChar,50),
                new SqlParameter(param_ShiftCode,SqlDbType.VarChar),
                new SqlParameter(param_PersonID,SqlDbType.VarChar,50),
                new SqlParameter(param_PositionID,SqlDbType.VarChar,50),
                 new SqlParameter(param_TeamPersonID,SqlDbType.VarChar,50),
                new SqlParameter(param_TeamNote,SqlDbType.VarChar,50),
                new SqlParameter(param_Result,SqlDbType.VarChar,50)
            };

            parames[0].Value = teamEntity.PlantID;
            parames[1].Value = teamEntity.ShiftID;
            parames[2].Value = shiftCode;
            parames[3].Value = teamEntity.PersonID;
            parames[4].Value = teamEntity.PositionID;
            parames[5].Value = teamEntity.TeamPersonID;
            parames[6].Value = teamEntity.TeamNote;
            parames[7].Direction = ParameterDirection.Output;

            try
            {
                SqlHelper.ExecuteNonQuery(SqlHelper.MyConnectStr, CommandType.StoredProcedure, pro_InsertTeam, parames);

                if (parames[7].Value.ToString() == "")
                    return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return false;
        }

        public bool Update(KPI_TeamEntity teamEntity)
        {
            if (null == teamEntity)
                throw new ArgumentNullException("teamEntity is Null");

            string shiftCode = teamEntity.ShiftName;

            foreach (KPI_TeamEntity item in GetTeamWithShiftByPosition(teamEntity.PositionID))
            {
                if (teamEntity.ShiftID != item.ShiftID)
                {
                    shiftCode = shiftCode + "|" + item.ShiftName;
                }
            }

            SqlParameter[] parames = new SqlParameter[] {
                new SqlParameter(param_TeamID,SqlDbType.VarChar,50),
                new SqlParameter(param_PlantID,SqlDbType.VarChar,50),
                new SqlParameter(param_ShiftID,SqlDbType.VarChar,50),
                new SqlParameter(param_ShiftCode,SqlDbType.VarChar),
                new SqlParameter(param_PersonID,SqlDbType.VarChar,50),
                new SqlParameter(param_PositionID,SqlDbType.VarChar,50),
                 new SqlParameter(param_TeamPersonID,SqlDbType.VarChar,50),
                new SqlParameter(param_TeamNote,SqlDbType.VarChar,50),
                new SqlParameter(param_Result,SqlDbType.VarChar,50)
            };

            parames[0].Value = teamEntity.TeamID;
            parames[1].Value = teamEntity.PlantID;
            parames[2].Value = teamEntity.ShiftID;
            parames[3].Value = shiftCode;
            parames[4].Value = teamEntity.PersonID;
            parames[5].Value = teamEntity.PositionID;
            parames[6].Value = teamEntity.TeamPersonID;
            parames[7].Value = teamEntity.TeamNote;
            parames[8].Direction = ParameterDirection.Output;

            try
            {
                SqlHelper.ExecuteNonQuery(SqlHelper.MyConnectStr, CommandType.StoredProcedure, pro_UpdateTeam, parames);

                if (parames[8].Value.ToString() == "")
                    return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return false;
        }

        public bool Delete(string deleteId)
        {
            SqlParameter[] parame = new SqlParameter[]{
                new SqlParameter(param_TeamID, SqlDbType.VarChar,50)
            };
            parame[0].Value = deleteId;
            try
            {
                int result = DBAccess.GetRelation().ExecuteNonQuery(sql_deleteTeamById, parame);

                if (result == 0)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return true;
        }


        public List<KPI_TeamEntity> GetTeamWithShiftByPosition(string positionID)
        {
            SqlParameter parame = new SqlParameter(param_PositionID, SqlDbType.VarChar);
            parame.Value = positionID;
            List<KPI_TeamEntity> teamList = new List<KPI_TeamEntity>();

            try
            {
                using (IDataReader dataReader = DBAccess.GetRelation().ExecuteReader(CommandType.Text, sql_TeamWithShift, parame))
                {
                    while (dataReader.Read())
                    {
                        teamList.Add(new KPI_TeamEntity
                        {
                            ShiftName = dataReader["ShiftName"] == DBNull.Value ? string.Empty : dataReader["ShiftName"].ToString(),
                            ShiftID = dataReader["ShiftID"] == DBNull.Value ? string.Empty : dataReader["ShiftID"].ToString(),
                            PositionID = dataReader["PositionID"] == DBNull.Value ? string.Empty : dataReader["PositionID"].ToString(),
                        });
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return teamList;
        }


        public bool Delete()
        {

            try
            {
                int result = DBAccess.GetRelation().ExecuteNonQuery(sql_deleteTeamById);

                if (result == 0)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return true;
        }
    }
}
