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
    public class KPI_ScorebookDal : IKPI_ScorebookDal
    {
        private const string sql_GetMonthNodeValue = @"SELECT ks.SysValue FROM  KPI_System AS ks
	                                                    WHERE  ks.[SysName] = @MonthNode AND ks.SysIsValid = '1'";

        private const string pro_ShiftScorebookStatistics = "pro_ShiftScorebookStatistics";
        private const string pro_PositionScorebookStatistics = "pro_PositionScorebookStatistics";

        private const string sql_GetUncartQuotaScorebook = @"SELECT kuqs.ShiftValue,kuqs.[Year],kuqs.[Month],
                                                                SUM(kuqs.Score) AS TotalScore
                                                                FROM KPI_UncartQuotaScorebook AS kuqs 
                                                                WHERE kuqs.[Year]=@SearchYear AND kuqs.[Month]=@StartMonth	   
                                                                GROUP BY kuqs.ShiftValue,kuqs.[Year],kuqs.[Month]    
                                                                ORDER BY TotalScore DESC";

        private const string sql_GetCoalElectricalQuotaScorebook = @"SELECT 
                                                                        kceqs.ShiftValue,kceqs.[Year], kceqs.[Month], 
                                                                        AVG((kceqs.ElectricalQuantity/(kceqs.CoalQuantity+kceqs.UnloadingQuantity+kceqs.SiloMaterial*0.4))) AS TotalCocal,
                                                                        SUM(kceqs.Score) AS TotalScore FROM KPI_CoalElectricalQuotaScorebook AS kceqs 
                                                                        WHERE kceqs.IsSetting=1 and kceqs.[Year]=@SearchYear AND kceqs.[Month]=@StartMonth	
                                                                        GROUP BY kceqs.ShiftValue,kceqs.[Year],kceqs.[Month]
                                                                        ORDER BY TotalScore DESC";

        private const string param_MonthNode = "@MonthNode";
        private const string param_MonthNodeValue = "@MonthNodeValue";
        private const string param_SearchYear = "@SearchYear";
        private const string param_StartMonth = "@StartMonth";
        private const string param_EndMonth = "@EndMonth";

        public string GetMonthNodeValue(string monthNodeName)
        {
            SqlParameter parame = new SqlParameter(param_MonthNode, SqlDbType.VarChar, 50);
            parame.Value = monthNodeName;

            object result = DBAccess.GetRelation().ExecuteScalar(sql_GetMonthNodeValue, parame);
            if (null == result)
                return string.Empty;

            return result.ToString();
        }

        public List<KPI_ScorebookEntity> GetShiftScorebook(string monthNode, string searchYear, string searchMonth)
        {
            List<KPI_ScorebookEntity> shiftScorebookList = new List<KPI_ScorebookEntity>();
            SqlParameter[] parames = new SqlParameter[] {
                new SqlParameter(param_MonthNodeValue,SqlDbType.VarChar,50),  
                new SqlParameter(param_SearchYear,SqlDbType.VarChar,50),
                new SqlParameter(param_StartMonth,SqlDbType.VarChar,5),
                new SqlParameter(param_EndMonth,SqlDbType.VarChar,5)
            };

            parames[0].Value = monthNode;
            parames[1].Value = searchYear;
            if (searchMonth == "1")
            {
                parames[2].Value = searchMonth;
            }
            else
            {
                parames[2].Value = (Convert.ToInt32(searchMonth) - 1).ToString();
            }
            parames[3].Value = searchMonth;

            using (IDataReader dataReader = DBAccess.GetRelation().ExecuteReader(CommandType.StoredProcedure, pro_ShiftScorebookStatistics, parames))
            {
                while (dataReader.Read())
                {
                    shiftScorebookList.Add(new KPI_ScorebookEntity
                    {
						//ShiftID = dataReader["ShiftID"].ToString(),
						//Year = Convert.ToInt32(dataReader["Year"]),
						//Month = Convert.ToInt32(dataReader["Month"]),
						//TotalScoreValue = Convert.ToDecimal(dataReader["TotalScoreValue"])

                    });
                }
            }

            return shiftScorebookList;
        }


        public List<KPI_ScorebookEntity> GetPositionScorebook(string monthNode, string searchYear, string searchMonth)
        {
            List<KPI_ScorebookEntity> positionScorebookList = new List<KPI_ScorebookEntity>();
            SqlParameter[] parames = new SqlParameter[] {
                new SqlParameter(param_MonthNodeValue,SqlDbType.VarChar,50),  
                new SqlParameter(param_SearchYear,SqlDbType.VarChar,50),
                new SqlParameter(param_StartMonth,SqlDbType.VarChar,5),
                new SqlParameter(param_EndMonth,SqlDbType.VarChar,5)
            };

            parames[0].Value = monthNode;
            parames[1].Value = searchYear;
            if (searchMonth == "1")
            {
                parames[2].Value = searchMonth;
            }
            else
            {
                parames[2].Value = (Convert.ToInt32(searchMonth) - 1).ToString();
            }
            parames[3].Value = searchMonth;

            using (IDataReader dataReader = DBAccess.GetRelation().ExecuteReader(CommandType.StoredProcedure, pro_PositionScorebookStatistics, parames))
            {
                while (dataReader.Read())
                {
                    positionScorebookList.Add(new KPI_ScorebookEntity
                    {
						//PositionID = dataReader["PositionID"].ToString(),
						//PositionName = dataReader["PositionName"].ToString(),
						//ShiftID = dataReader["ShiftID"].ToString(),
						//Year = Convert.ToInt32(dataReader["Year"]),
						//Month = Convert.ToInt32(dataReader["Month"]),
						//TotalScoreValue = Convert.ToDecimal(dataReader["TotalScoreValue"])

                    });
                }
            }

            return positionScorebookList;
        }


        public List<KPI_UncartQuotaScorebookEntity> GetUncartQuotaScorebook(int searchYear, int searchMonth)
        {
            List<KPI_UncartQuotaScorebookEntity> uncartQuotaScorebookEntityList = new List<KPI_UncartQuotaScorebookEntity>();
            SqlParameter[] parames = new SqlParameter[] {
                new SqlParameter(param_SearchYear,SqlDbType.Int), 
                new SqlParameter(param_StartMonth,SqlDbType.Int)
                
            };
            
            parames[0].Value = searchYear;
            parames[1].Value = searchMonth;

            using (IDataReader dataReader = DBAccess.GetRelation().ExecuteReader(CommandType.Text, sql_GetUncartQuotaScorebook, parames))
            {
                int ranking=1;
                while (dataReader.Read())
                {
                    uncartQuotaScorebookEntityList.Add(new KPI_UncartQuotaScorebookEntity
                    {
                       
                        ShiftValue = dataReader["ShiftValue"].ToString(),
                        Year = Convert.ToInt32(dataReader["Year"]),
                        Month = Convert.ToInt32(dataReader["Month"]),
                        TotalScore = Convert.ToDecimal(dataReader["TotalScore"]).ToString("N3"),
                        Ranking = ranking
                    });
                    ranking++;
                }
            }

            return uncartQuotaScorebookEntityList;
        }


        public List<CoalElectricalQuotaScoreEntity> GetCoalElectricalQuotaScoreEntity(int searchYear, int searchMonth)
        {
            List<CoalElectricalQuotaScoreEntity> coalElectricalQuotaScoreList = new List<CoalElectricalQuotaScoreEntity>();
            SqlParameter[] parames = new SqlParameter[] {
                new SqlParameter(param_SearchYear,SqlDbType.Int), 
                new SqlParameter(param_StartMonth,SqlDbType.Int)
                
            };

            parames[0].Value = searchYear;
            parames[1].Value = searchMonth;

            using (IDataReader dataReader = DBAccess.GetRelation().ExecuteReader(CommandType.Text, sql_GetCoalElectricalQuotaScorebook, parames))
            {
                int ranking = 1;
                while (dataReader.Read())
                {
                    coalElectricalQuotaScoreList.Add(new CoalElectricalQuotaScoreEntity
                    {
                       
                        ShiftValue = dataReader["ShiftValue"].ToString(),
                        Year = Convert.ToInt32(dataReader["Year"]),
                        Month = Convert.ToInt32(dataReader["Month"]),
                        TotalCocal=Convert.ToDecimal(dataReader["TotalCocal"]).ToString("N3"),
                        TotalScore =Convert.ToDecimal(dataReader["TotalScore"]).ToString("N3"),
                        Ranking = ranking
                    });
                    ranking++;
                }
            }

            return coalElectricalQuotaScoreList;
        }
    }
}
