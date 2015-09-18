using SIS.DataEntity;
using SIS.DBControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SIS.DataAccess
{
    public class KPI_PersonScoreDal : IKPI_PersonScoreDal
    {
        private const string sql_SearchPersonScore = @"SELECT [PersonID],[CheckDate],[Score]
                                                      FROM [TMSISKPI].[dbo].[KPI_PersonScore]
                                                      WHERE PersonID=@PersonID  AND
                                                      (CONVERT(VARCHAR(10), CheckDate, 121) >= @StartTime AND CONVERT(VARCHAR(10),CheckDate, 121) <= @EndTime)";


        private const string param_PersonId = "@PersonID";
        private const string param_StartTime = "@StartTime";
        private const string param_EndTime = "@EndTime";
        private RelaInterface m_DB;


        public KPI_PersonScoreDal()
        {
            m_DB = DBAccess.GetRelation();
        }
        public List<KPI_PersonScore> SearchPersonScore(string personId, DateTime startTime, DateTime endTime)
        {
            SqlParameter[] parames = new SqlParameter[] {
                new SqlParameter(param_PersonId,DbType.String),  
               new SqlParameter(param_StartTime,SqlDbType.DateTime),
                new SqlParameter(param_EndTime,SqlDbType.DateTime)
            };

            parames[0].Value = personId;
            parames[1].Value = startTime;
            parames[2].Value = endTime;
            IDataReader reader = null;
            List<KPI_PersonScore> personScoreList = new List<KPI_PersonScore>();
            try
            {

                reader = m_DB.ExecuteReader(CommandType.Text, sql_SearchPersonScore, parames);

                while (reader.Read())
                {
                    personScoreList.Add(new KPI_PersonScore
                    {
                        PersonID = reader["PersonID"].ToString(),
                        CheckDate = (DateTime)reader["CheckDate"],
                        Score = reader["Score"] == DBNull.Value ? null : (decimal?)reader["Score"]
                    });
                }

                reader.Close();
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
            return personScoreList;
        }
    }
}
