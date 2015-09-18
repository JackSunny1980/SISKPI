using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIS.DataEntity;
using System.Data;
using System.Data.SqlClient;
using SIS.DBControl;

namespace SIS.DataAccess {

	public class ReportDal : System.IDisposable {

		private RelaInterface DataBase;
		private readonly string[] ecwebs = { "", "JJZB", "TLZB", "HXZB", "SMZB" };

		#region 构造器

		public ReportDal() {
			DataBase = DBAccess.GetRelation();
		}

		#endregion

		#region 公共方法

		/// <summary>
		/// 返回值得分与奖金
		/// </summary>
		/// <param name="yearMonth">年月</param>
		/// <param name="specialField">专业</param>
		/// <returns></returns>
		public List<ShiftScoreAndBonusEntity> GetShiftScoreAndBonus(string yearMonth, String specialField) {
			List<ShiftScoreAndBonusEntity> Result = new List<ShiftScoreAndBonusEntity>();
			string SqlText = @"WITH ShiftScoreCTE AS(
								SELECT YearMonth=CONVERT(varchar(7),CheckDate,120),UnitID,Shift,
								ECScore=SUM(ECScore),SASCore=SUM(SASCore),TotalScore=SUM(TotalScore) FROM KPI_ShiftScore 
								WHERE SpecialField=@SpecialField AND CONVERT(varchar(7),CheckDate,120) =@YearMonth
								GROUP BY CheckDate,UnitID,Shift),
								MonitorDurationCTE AS(
								SELECT UnitID,Shift,Duration=SUM(Duration) FROM KPI_MonitorDuration
								WHERE CONVERT(Varchar(7),CheckDate,120) =@YearMonth
								GROUP BY UnitID,Shift)
								SELECT B.*,AvgScore =TotalScore/Duration,A.Bonus,C.UnitName,
                                       DENSE_RANK() OVER(PARTITION BY A.UnitID ORDER BY TotalScore/Duration DESC) as OrderNo FROM KPI_ShiftBonus A JOIN(
									SELECT YearMonth,UnitID,Shift,
										   ECScore=SUM(ECScore),SASCore=SUM(SASCore),TotalScore=SUM(TotalScore)
									FROM ShiftScoreCTE GROUP BY YearMonth,UnitID,Shift) B 
								ON A.UnitID=B.UnitID AND A.CheckYearMonth=B.YearMonth AND A.Shift=B.Shift 
								JOIN KPI_Unit C ON A.UnitID=C.UnitID
                                JOIN MonitorDurationCTE D ON A.UnitID=D.UnitID AND A.Shift=D.Shift
								WHERE A.SpecialField=@SpecialField AND A.CheckYearMonth=@YearMonth
								ORDER BY UnitID,Shift";
			IDbDataParameter[] parames = new SqlParameter[] {              
                new SqlParameter("@SpecialField",DbType.String),
                new SqlParameter("@YearMonth",DbType.String)};
			parames[0].Value = specialField;
			parames[1].Value = yearMonth;
			using (IDataReader DataReader = DataBase.ExecuteReader(CommandType.Text, SqlText, parames)) {
				Result = DataReader.FillGenericList<ShiftScoreAndBonusEntity>();
				DataReader.Close();
			}
			//汇总
			ShiftScoreAndBonusEntity Total = new ShiftScoreAndBonusEntity();
			Total.UnitName = "合计";
			Total.ECScore = Result.Sum(p => p.ECScore);
			Total.SASCore = Result.Sum(p => p.SASCore);
			Total.TotalScore = Result.Sum(p => p.TotalScore);
			Total.Bonus = Result.Sum(p => p.Bonus);
			Result.Add(Total);
			return Result;
		}

		public List<ShiftScoreEntity> GetShiftScore(DateTime StartDate, DateTime EndDate, String specialField) {
			List<ShiftScoreEntity> Result;// = new List<ShiftScoreEntity>();
			EndDate = EndDate.AddDays(1).AddMinutes(-1);
			/*string ShiftScoreSql = @"WITH ShiftScoreCTE AS(
									SELECT UnitID,Shift,ECScore=SUM(ECScore),SASCore=SUM(SASCore),TotalScore=SUM(TotalScore) FROM KPI_ShiftScore 
									WHERE SpecialField=@SpecialField AND CheckDate BETWEEN @StartDate AND @EndDate
									GROUP BY UnitID,Shift),
									MonitorDurationCTE AS(
									SELECT UnitID,Shift,Duration=SUM(Duration) FROM KPI_MonitorDuration
									WHERE CheckDate BETWEEN @StartDate AND @EndDate
									GROUP BY UnitID,Shift)
									SELECT A.*,B.UnitName,TotalHours=C.Duration,AvgScore=A.TotalScore/C.Duration,
									DENSE_RANK() OVER(PARTITION BY A.UnitID ORDER BY TotalScore/Duration DESC) as OrderNo
									FROM ShiftScoreCTE  A JOIN KPI_Unit B ON A.UnitID=B.UnitID 
														  JOIN MonitorDurationCTE C ON A.UnitID=C.UnitID AND  A.Shift=C.Shift
									ORDER BY UnitID,Shift";*/
            string ShiftScoreSql = @"--机组各值得分
                                    WITH ShiftScoreCTE AS(
	                                    SELECT UnitID,Shift,ECScore=SUM(ECScore),SASCore=SUM(SASCore),TotalScore=SUM(TotalScore) FROM KPI_ShiftScore 
	                                    WHERE SpecialField=@SpecialField AND CheckDate BETWEEN @StartDate AND @EndDate
	                                    GROUP BY UnitID,Shift),
                                    --机组各值监盘时间
                                    MonitorDurationCTE AS(
	                                    SELECT UnitID,Shift,Duration=SUM(Duration) FROM KPI_MonitorDuration
	                                    WHERE CheckDate BETWEEN @StartDate AND @EndDate
	                                    GROUP BY UnitID,Shift),
                                    --机组各值平均分排名
                                    ShiftRankCTE AS(	
	                                    SELECT A.*,B.UnitName,TotalHours=C.Duration,AvgScore=A.TotalScore/C.Duration,
	                                    DENSE_RANK() OVER(PARTITION BY A.UnitID ORDER BY TotalScore/Duration DESC) as OrderNo
	                                    FROM ShiftScoreCTE  A JOIN KPI_Unit B ON A.UnitID=B.UnitID 
						                                      JOIN MonitorDurationCTE C ON A.UnitID=C.UnitID AND  A.Shift=C.Shift),
                                    --全厂各值监盘时间						  
                                    UnitMonitorDurationCTE AS(
	                                    SELECT '00' UnitID,Shift,Duration=SUM(Duration) FROM KPI_MonitorDuration
	                                    WHERE CheckDate BETWEEN @StartDate AND @EndDate AND UnitID='00'
	                                    GROUP BY Shift),
                                    --全厂各值得分情况	
                                    ShiftUnitScoreCTE AS(
	                                    SELECT '00' UnitID,Shift,ECScore=SUM(ECScore),SASCore=SUM(SASCore),TotalScore=SUM(TotalScore) FROM KPI_ShiftScore 
	                                    WHERE SpecialField=@SpecialField AND CheckDate BETWEEN @StartDate AND @EndDate
	                                    GROUP BY Shift),
                                    --全厂各值排名情况	
                                    UnitRankCTE AS(	
	                                    SELECT A.*,B.UnitName,TotalHours=C.Duration,AvgScore=A.TotalScore/C.Duration,
	                                    DENSE_RANK() OVER(PARTITION BY A.UnitID ORDER BY TotalScore/Duration DESC) as OrderNo
	                                    FROM ShiftUnitScoreCTE  A JOIN KPI_Unit B ON A.UnitID=B.UnitID 
						                                          JOIN UnitMonitorDurationCTE C ON A.UnitID=C.UnitID AND  A.Shift=C.Shift),
                                    --出力系数						 
                                    RateCET AS(
                                    SELECT '99' AS UnitID, A.Shift,ECScore=SUM(Score),SASCore=0,
                                    TotalScore=SUM(Score),UnitName='出力系数',TotalHours=SUM(C.Duration),AvgScore=SUM(Score)/SUM(C.Duration),
	                                    DENSE_RANK() OVER(ORDER BY SUM(Score)/SUM(C.Duration) DESC) as OrderNo
                                    FROM KPI_ECDayData A JOIN UnitMonitorDurationCTE C ON  A.Shift=C.Shift
                                    WHERE (ECID='10001' OR ECID='10002') AND A.CheckDate BETWEEN @StartDate AND @EndDate
                                    GROUP BY A.Shift),
                                    CTE AS(
                                    SELECT * FROM ShiftRankCTE
                                    UNION	
                                    SELECT*FROM UnitRankCTE)
                                    SELECT*FROM CTE ORDER BY UnitID,Shift";
			IDbDataParameter[] parames1 = new SqlParameter[] {              
                new SqlParameter("@StartDate",DbType.Date),
                new SqlParameter("@EndDate",DbType.Date),				
				new SqlParameter("@SpecialField",DbType.String)};
			parames1[0].Value = StartDate;
			parames1[1].Value = EndDate;
			parames1[2].Value = specialField;

			IDbDataParameter[] parames2 = new SqlParameter[] {              
                new SqlParameter("@StartDate",DbType.DateTime),
                new SqlParameter("@EndDate",DbType.DateTime),				
				new SqlParameter("@ecWeb",DbType.String)};
			parames2[0].Value = StartDate;
			parames2[1].Value = EndDate;
			parames2[2].Value = GetECWeb(specialField);			
			using (IDataReader DataReader = DataBase.ExecuteReader(CommandType.Text, ShiftScoreSql, parames1)) {
				Result = DataReader.FillGenericList<ShiftScoreEntity>();
				DataReader.Close();
			}			
			ShiftScoreEntity Total = new ShiftScoreEntity();
			Total.UnitName = "合计";
			Total.ECScore = Result.Sum(p => p.ECScore);
			Total.SASCore = Result.Sum(p => p.SASCore);
			Total.TotalScore = Result.Sum(p => p.TotalScore);
			Total.TotalHours = Result.Sum(p => p.TotalHours);
			if (Total.TotalHours > 0) Total.AvgScore = Math.Round(Total.TotalScore.Value / Total.TotalHours.Value, 2);
			Result.Add(Total);
			return Result;
		}


		public List<PersonScoreEntity> GetPersonScore(string yearMonth, String specialField) {
			if (string.IsNullOrEmpty(yearMonth)) return null;
			List<PersonScoreEntity> Result = new List<PersonScoreEntity>();
			int Year = Convert.ToInt32(yearMonth.Substring(0, 4));
			int Month = Convert.ToInt32(yearMonth.Substring(4, 2));
			DateTime StartDate = new DateTime(Year, Month, 1);
			DateTime EndDate = StartDate.AddMonths(1);
			string SqlText = @"WITH PersonScoreCTE AS(SELECT PersonID,SUM(ISNULL(Score,0)) Score FROM KPI_PersonScore
								WHERE CheckDate BETWEEN @StartDate AND @EndDate
								GROUP BY PersonID)
								SELECT  A.PersonID,PersonCode,PersonName,A.Shift,PositionName,B.PositionID,PositionWeight,C.Score,D.Bonus 
								FROM KPI_Person A JOIN KPI_Position    B ON A.PositionID=B.PositionID AND A.PersonIsValid='1'
												  LEFT JOIN PersonScoreCTE  C ON A.PersonID=C.PersonID
												  JOIN KPI_PersonBonus D ON A.PersonID=D.PersonID
								WHERE A.SpecialField =@SpecialField AND D.CheckYearMonth=@YearMonth
                                ORDER BY A.Shift,C.Score desc";
			IDbDataParameter[] parames = new SqlParameter[] {   
           		new SqlParameter("@StartDate",DbType.DateTime),
				new SqlParameter("@EndDate",DbType.DateTime),
                new SqlParameter("@SpecialField",DbType.String),
                new SqlParameter("@YearMonth",DbType.String)};
			parames[0].Value = StartDate;
			parames[1].Value = EndDate;
			parames[2].Value = specialField;
			parames[3].Value = yearMonth;
			using (IDataReader DataReader = DataBase.ExecuteReader(CommandType.Text, SqlText, parames)) {
				Result = DataReader.FillGenericList<PersonScoreEntity>();
				DataReader.Close();
			}
			PersonScoreEntity Sum = new PersonScoreEntity();
			Sum.Shift = "";
			Sum.PersonName = "合计";
			Sum.Score = Result.Sum(p => p.Score);
			Sum.Bonus = Result.Sum(p => p.Bonus);
			Result.Add(Sum);
			return Result;
		}


		public List<ShiftRaceEntity> GetShiftRaces(string yearMonth, String specialField) {
			List<ShiftRaceEntity> Result = new List<ShiftRaceEntity>();
			int Year = Convert.ToInt32(yearMonth.Substring(0, 4));
			int Month = Convert.ToInt32(yearMonth.Substring(4, 2));
			DateTime StartDate = new DateTime(Year, Month, 1);
			DateTime EndDate = StartDate.AddMonths(1).AddDays(-1);
			List<ShiftRaceEntity> ShiftMonthScore = GetShiftRaceScore(StartDate, EndDate, specialField);
			List<ShiftRaceEntity> UnitMonthScore = GetUnitRaceScore(StartDate, EndDate, specialField);
			StartDate = new DateTime(Year, 1, 1);
			EndDate = StartDate.AddYears(1);
			List<ShiftRaceEntity> ShiftYearScore = GetShiftRaceScore(StartDate, EndDate, specialField);
			List<ShiftRaceEntity> UnitYearScore = GetUnitRaceScore(StartDate, EndDate, specialField); ;
			var q = (from a in ShiftMonthScore
					 join b in ShiftYearScore on new { a.Shift, a.UnitID } equals new { b.Shift, b.UnitID }
					 select new ShiftRaceEntity {
						 Shift = a.Shift,
						 UnitID = a.UnitID,
						 UnitName = a.UnitName,
						 ShiftMonthScore = a.ShiftMonthScore,
						 ShiftMonthOrderNo = a.ShiftMonthOrderNo,
						 ShiftYearScore = b.ShiftMonthScore,
						 ShiftYearOrderNo = b.ShiftMonthOrderNo
					 }).ToList<ShiftRaceEntity>();//值月得分和年得分情况

			var p = (from a in UnitMonthScore
					 join b in UnitYearScore on a.Shift equals b.Shift
					 select new ShiftRaceEntity {
						 Shift = a.Shift,
						 UnitMonthScore = a.UnitMonthScore,
						 UnitMonthOrderNo = a.UnitMonthOrderNo,
						 UnitYearScore = b.UnitMonthScore,
						 UnitYearOrderNo = b.UnitMonthOrderNo
					 }).ToList<ShiftRaceEntity>();//机组月得分和年得分情况
			Result = (from a in q
					 join b in p on a.Shift equals b.Shift
					 select new ShiftRaceEntity {
						 Shift = a.Shift,
						 UnitID = a.UnitID,
						 UnitName = a.UnitName,
						 ShiftMonthScore = a.ShiftMonthScore,
						 ShiftMonthOrderNo = a.ShiftMonthOrderNo,
						 ShiftYearScore = a.ShiftYearScore,
						 ShiftYearOrderNo = a.ShiftYearOrderNo,
						 UnitMonthScore = b.UnitMonthScore,
						 UnitMonthOrderNo = b.UnitMonthOrderNo,
						 UnitYearScore = b.UnitYearScore,
						 UnitYearOrderNo = b.UnitYearOrderNo
					 }).ToList <ShiftRaceEntity>();

			return Result;
		}

        /// <summary>
        /// 返回全厂奖金情况
        /// </summary>
        /// <param name="YearMonth"></param>
        /// <returns></returns>
        public List<PlantBonusEntity> GetPlantBonus(String YearMonth) {
            List<PlantBonusEntity> Result;
            string SqlText = @"SELECT Shift,YearMonth,Unit1Bonus,Unit2Bonus,PlantBonus,
                                      PowerCapacity,TotalBonus,Achievement
                               FROM KPI_PlantBonus WHERE YearMonth=@YearMonth";
            IDbDataParameter[] parames = new SqlParameter[] { 
                new SqlParameter("@YearMonth",DbType.String)};
            parames[0].Value = YearMonth;
            using (IDataReader DataReader = DataBase.ExecuteReader(CommandType.Text, SqlText, parames)) {
                Result = DataReader.FillGenericList<PlantBonusEntity>();
                DataReader.Close();
            }
            return Result;
        }

		private List<ShiftRaceEntity> GetShiftRaceScore(DateTime startDate, DateTime endDate, String specialField) {
			List<ShiftRaceEntity> Result;
			string SqlText = @"WITH ShifMonthScoreCTE AS(
								SELECT UnitID,Shift,ShiftMonthScore=SUM(TotalScore) FROM KPI_ShiftScore
								WHERE CheckDate BETWEEN @StartDate AND @EndDate AND SpecialField=@SpecialField AND Shift<>'' AND UnitID<>'00'
								GROUP BY UnitID,Shift)
								SELECT A.*,B.UnitName,DENSE_RANK() over(order by ShiftMonthScore DESC) as ShiftMonthOrderNo FROM ShifMonthScoreCTE A
								JOIN KPI_Unit B ON A.UnitID=B.UnitID
								ORDER BY Shift,UnitID";
			IDbDataParameter[] parames = new SqlParameter[] {   
           		new SqlParameter("@StartDate",DbType.DateTime),
				new SqlParameter("@EndDate",DbType.DateTime),
                new SqlParameter("@SpecialField",DbType.String)};
			parames[0].Value = startDate;
			parames[1].Value = endDate;
			parames[2].Value = specialField;
			using (IDataReader DataReader = DataBase.ExecuteReader(CommandType.Text, SqlText, parames)) {
				Result = DataReader.FillGenericList<ShiftRaceEntity>();
				DataReader.Close();
			}
			return Result;
		}


		private List<ShiftRaceEntity> GetUnitRaceScore(DateTime startDate, DateTime endDate, String specialField) {
			List<ShiftRaceEntity> Result;
			string SqlText = @"WITH UnitMonthScoreCTE AS(
								SELECT Shift,UnitMonthScore=SUM(TotalScore) FROM KPI_ShiftScore
								WHERE CheckDate BETWEEN @StartDate AND @EndDate AND SpecialField=@SpecialField AND Shift<>'' AND UnitID<>'00'
								GROUP BY Shift)
								SELECT A.*,DENSE_RANK() over(order by UnitMonthScore DESC) as UnitMonthOrderNo FROM UnitMonthScoreCTE A
								ORDER BY Shift";
			IDbDataParameter[] parames = new SqlParameter[] {   
           		new SqlParameter("@StartDate",DbType.DateTime),
				new SqlParameter("@EndDate",DbType.DateTime),
                new SqlParameter("@SpecialField",DbType.String)};
			parames[0].Value = startDate;
			parames[1].Value = endDate;
			parames[2].Value = specialField;
			using (IDataReader DataReader = DataBase.ExecuteReader(CommandType.Text, SqlText, parames)) {
				Result = DataReader.FillGenericList<ShiftRaceEntity>();
				DataReader.Close();
			}
			return Result;
		}

		private String GetECWeb(string specialField) {
			if (string.IsNullOrEmpty(specialField)) return "";
			int index = Convert.ToInt32(specialField);
			if ((index > 0) && (index < 5)) return ecwebs[index];
			return "";
		}

		#endregion

		#region IDisposable 成员

		public void Dispose() {
			DataBase = null;
		}

		#endregion
	}
}
