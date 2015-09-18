using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIS.DataEntity;
using System.Data;
using System.Data.SqlClient;
using SIS.DBControl;

namespace SIS.DataAccess {
    public class ECHistoryDataAccess : IDisposable {

        #region 构造器

        public ECHistoryDataAccess() {

        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 返回经济指标小时数据
        /// </summary>
        /// <param name="ECID">指标编码</param>
        /// <param name="StartDate">开始日期</param>
        /// <param name="EndDate">结束日期</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="PageSize">页面大小</param>
        /// <param name="RecordCount">记录数</param>
        /// <returns></returns>
        public List<ECHistoryDataEntity> GetECHourData(String ECID, DateTime StartDate, DateTime EndDate,
            int PageIndex, int PageSize, out int RecordCount) {
            RelaInterface DataBase = DBAccess.GetRelation();
            List<ECHistoryDataEntity> Result = null;
            IDbDataParameter[] parames = new SqlParameter[] {
                new SqlParameter("@ECID",DbType.String),
                new SqlParameter("@StartDate",DbType.DateTime),
                new SqlParameter("@EndDate",DbType.DateTime),
			    new SqlParameter("@PageIndex",DbType.Int32),
                new SqlParameter("@PageSize",DbType.Int32)};
            parames[0].Value = ECID;
            parames[1].Value = StartDate;
            parames[2].Value = EndDate;
            parames[3].Value = PageIndex;
            parames[4].Value = PageSize;
            String CTE = @"WITH CTE AS(
                              SELECT A.*, B.ECName,C.EngunitName,ROW_NUMBER() OVER(ORDER BY CheckDate) OrderNo 
                              FROM KPI_ECHourData A JOIN KPI_ECTag   B ON A.ECID=B.ECID
                                                    JOIN KPI_Engunit C  ON B.EngunitID = C.EngunitID
                              WHERE A.ECID=@ECID AND CheckDate BETWEEN @StartDate AND @EndDate)";
            String RecordCountSql = CTE + "SELECT COUNT(ECID) FROM CTE";
            String SqlText = CTE + @"SELECT * FROM CTE WHERE OrderNo BETWEEN (@PageIndex -1)*@PageSize 
                                AND @PageIndex * @PageSize";
            RecordCount = Convert.ToInt32(DataBase.ExecuteScalar(RecordCountSql, parames));
            using (IDataReader Reader = DataBase.ExecuteReader(CommandType.Text, SqlText, parames)) {
                Result = Reader.FillGenericList<ECHistoryDataEntity>();
            }
            return Result;
        }

        /// <summary>
        /// 返回经济指标日数据
        /// </summary>
        /// <param name="ECID">指标编码</param>
        /// <param name="StartDate">开始日期</param>
        /// <param name="EndDate">结束日期</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="PageSize">页面大小</param>
        /// <param name="RecordCount">记录数</param>
        /// <returns></returns>
        public List<ECHistoryDataEntity> GetECDayData(String ECID, DateTime StartDate, DateTime EndDate,
            int PageIndex, int PageSize, out int RecordCount) {
            RelaInterface DataBase = DBAccess.GetRelation();
            List<ECHistoryDataEntity> Result = null;
            IDbDataParameter[] parames = new SqlParameter[] {
                new SqlParameter("@ECID",DbType.String),
                new SqlParameter("@StartDate",DbType.DateTime),
                new SqlParameter("@EndDate",DbType.DateTime),
			    new SqlParameter("@PageIndex",DbType.Int32),
                new SqlParameter("@PageSize",DbType.Int32)};
            parames[0].Value = ECID;
            parames[1].Value = StartDate;
            parames[2].Value = EndDate;
            parames[3].Value = PageIndex;
            parames[4].Value = PageSize;
            String CTE = @"WITH CTE AS(
                              SELECT A.*, B.ECName,C.EngunitName,ROW_NUMBER() OVER(ORDER BY CheckDate) OrderNo 
                              FROM KPI_ECDayData A JOIN KPI_ECTag   B ON A.ECID=B.ECID
                                                   JOIN KPI_Engunit C  ON B.EngunitID = C.EngunitID
                              WHERE A.ECID=@ECID AND CheckDate BETWEEN @StartDate AND @EndDate)";
            String RecordCountSql = CTE + "SELECT COUNT(ECID) FROM CTE";
            String SqlText = CTE + @"SELECT * FROM CTE WHERE OrderNo BETWEEN (@PageIndex -1)*@PageSize 
                                AND @PageIndex * @PageSize";
            RecordCount = Convert.ToInt32(DataBase.ExecuteScalar(RecordCountSql, parames));
            using (IDataReader Reader = DataBase.ExecuteReader(CommandType.Text, SqlText, parames)) {
                Result = Reader.FillGenericList<ECHistoryDataEntity>();
            }
            return Result;
        }

        /// <summary>
        /// 指标对比分析
        /// </summary>
        /// <param name="UnitID"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="HStartDate"></param>
        /// <param name="HEndDate"></param>
        /// <returns></returns>
        public List<KPIContrastEntity> GetKPIContrast(String UnitID, DateTime StartDate, DateTime EndDate,
            DateTime HStartDate, DateTime HEndDate) {
            RelaInterface DataBase = DBAccess.GetRelation();
            IDbDataParameter[] parames = new SqlParameter[] {  
                new SqlParameter("@UnitID",DbType.String),
                new SqlParameter("@StartDate",DbType.DateTime),
                new SqlParameter("@EndDate",DbType.DateTime),
                new SqlParameter("@LStartDate",DbType.DateTime),
                new SqlParameter("@LEndDate",DbType.DateTime)};
            parames[0].Value = UnitID;
            parames[1].Value = StartDate;
            parames[2].Value = EndDate;
            parames[3].Value = HStartDate;
            parames[4].Value = HEndDate;
           
            String SqlText = @"WITH Shift1CTE AS(
                                SELECT ECID,AVG(ISNULL(AvgValue,0)) AvgValue,
                                DENSE_RANK() OVER(ORDER BY AVG(ISNULL(AvgValue,0)) DESC) as OrderNo
                                FROM KPI_ECDayData
                                WHERE Shift ='1' AND CheckDate BETWEEN @StartDate AND @EndDate
                                GROUP BY ECID),
                                Shift2CTE AS(
                                SELECT ECID,AVG(ISNULL(AvgValue,0)) AvgValue,
                                DENSE_RANK() OVER(ORDER BY AVG(ISNULL(AvgValue,0)) DESC) as OrderNo
                                FROM KPI_ECDayData
                                WHERE Shift ='2' AND CheckDate BETWEEN @StartDate AND @EndDate
                                GROUP BY ECID),
                                Shift3CTE AS(
                                SELECT ECID,AVG(ISNULL(AvgValue,0)) AvgValue,
                                DENSE_RANK() OVER(ORDER BY AVG(ISNULL(AvgValue,0)) DESC) as OrderNo
                                FROM KPI_ECDayData
                                WHERE Shift ='3' AND CheckDate BETWEEN @StartDate AND @EndDate
                                GROUP BY ECID),
                                Shift4CTE AS(
                                SELECT ECID,AVG(ISNULL(AvgValue,0)) AvgValue,
                                DENSE_RANK() OVER(ORDER BY AVG(ISNULL(AvgValue,0)) DESC) as OrderNo
                                FROM KPI_ECDayData
                                WHERE Shift ='4' AND CheckDate BETWEEN @StartDate AND @EndDate
                                GROUP BY ECID),
                                Shift5CTE AS(
                                SELECT ECID,AVG(ISNULL(AvgValue,0)) AvgValue,
                                DENSE_RANK() OVER(ORDER BY AVG(ISNULL(AvgValue,0)) DESC) as OrderNo
                                FROM KPI_ECDayData
                                WHERE Shift ='5' AND CheckDate BETWEEN @StartDate AND @EndDate
                                GROUP BY ECID),

                                LShift1CTE AS(
                                SELECT ECID,AVG(ISNULL(AvgValue,0)) AvgValue,
                                DENSE_RANK() OVER(ORDER BY AVG(ISNULL(AvgValue,0)) DESC) as OrderNo
                                FROM KPI_ECDayData
                                WHERE Shift ='1' AND CheckDate BETWEEN @LStartDate AND @LEndDate
                                GROUP BY ECID),
                                LShift2CTE AS(
                                SELECT ECID,AVG(ISNULL(AvgValue,0)) AvgValue,
                                DENSE_RANK() OVER(ORDER BY AVG(ISNULL(AvgValue,0)) DESC) as OrderNo
                                FROM KPI_ECDayData
                                WHERE Shift ='2' AND CheckDate BETWEEN @LStartDate AND @LEndDate
                                GROUP BY ECID),
                                LShift3CTE AS(
                                SELECT ECID,AVG(ISNULL(AvgValue,0)) AvgValue,
                                DENSE_RANK() OVER(ORDER BY AVG(ISNULL(AvgValue,0)) DESC) as OrderNo
                                FROM KPI_ECDayData
                                WHERE Shift ='3' AND CheckDate BETWEEN @LStartDate AND @LEndDate
                                GROUP BY ECID),
                                LShift4CTE AS(
                                SELECT ECID,AVG(ISNULL(AvgValue,0)) AvgValue,
                                DENSE_RANK() OVER(ORDER BY AVG(ISNULL(AvgValue,0)) DESC) as OrderNo
                                FROM KPI_ECDayData
                                WHERE Shift ='4' AND CheckDate BETWEEN @LStartDate AND @LEndDate
                                GROUP BY ECID),
                                LShift5CTE AS(
                                SELECT ECID,AVG(ISNULL(AvgValue,0)) AvgValue,
                                DENSE_RANK() OVER(ORDER BY AVG(ISNULL(AvgValue,0)) DESC) as OrderNo
                                FROM KPI_ECDayData
                                WHERE Shift ='5' AND CheckDate BETWEEN @LStartDate AND @LEndDate
                                GROUP BY ECID),

                                CTE AS(
                                SELECT A.UnitID,A.ECID,A.ECName,A.ECIndex,
                                B.AvgValue Shift1Value,B.OrderNo Shift1Rank,
                                C.AvgValue Shift2Value,C.OrderNo Shift2Rank,
                                D.AvgValue Shift3Value,D.OrderNo Shift3Rank,
                                E.AvgValue Shift4Value,E.OrderNo Shift4Rank,
                                F.AvgValue Shift5Value,F.OrderNo Shift5Rank,

                                G.AvgValue Shift1HValue,G.OrderNo Shift1HRank,
                                H.AvgValue Shift2HValue,H.OrderNo Shift2HRank,
                                J.AvgValue Shift3HValue,J.OrderNo Shift3HRank,
                                K.AvgValue Shift4HValue,K.OrderNo Shift4HRank,
                                I.AvgValue Shift5HValue,I.OrderNo Shift5HRank
                                FROM  KPI_ECTag A LEFT JOIN Shift1CTE B ON A.ECID=B.ECID
                                                  LEFT JOIN Shift2CTE C ON A.ECID=C.ECID
                                                  LEFT JOIN Shift3CTE D ON A.ECID=D.ECID
                                                  LEFT JOIN Shift4CTE E ON A.ECID=E.ECID
                                                  LEFT JOIN Shift5CTE F ON A.ECID=F.ECID
                  
                                                  LEFT JOIN LShift1CTE G ON A.ECID=G.ECID
                                                  LEFT JOIN LShift2CTE H ON A.ECID=H.ECID
                                                  LEFT JOIN LShift3CTE J ON A.ECID=J.ECID
                                                  LEFT JOIN LShift4CTE K ON A.ECID=K.ECID
                                                  LEFT JOIN LShift5CTE I ON A.ECID=I.ECID
                                WHERE A.ECWeb='JJZB' AND A.ECIsSnapshot=1 AND UnitID=@UnitID)
                                SELECT * FROM CTE ORDER BY ECIndex";
            List<KPIContrastEntity> Result;           
            using (IDataReader Reader = DataBase.ExecuteReader(CommandType.Text, SqlText, parames)) {
                Result = Reader.FillGenericList<KPIContrastEntity>();
            }
            return Result;
        }

        /// <summary>
        /// 返回机组指标得分排名情况
        /// </summary>
        /// <param name="UnitID"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public List<KPIRankEntity> GetKPIRank(String UnitID, DateTime StartDate, DateTime EndDate) {
            RelaInterface DataBase = DBAccess.GetRelation();
            IDbDataParameter[] parames = new SqlParameter[] {  
                new SqlParameter("@UnitID",DbType.String),
                new SqlParameter("@StartDate",DbType.DateTime),
                new SqlParameter("@EndDate",DbType.DateTime)};
            parames[0].Value = UnitID;
            parames[1].Value = StartDate;
            parames[2].Value = EndDate;

            String SqlText = @"WITH MonitorDurationCTE AS(
	                            SELECT UnitID,Shift,Duration=SUM(Duration) FROM KPI_MonitorDuration
	                            WHERE CheckDate BETWEEN @StartDate AND @EndDate AND UnitID=@UnitID 
	                            GROUP BY UnitID,Shift),
                            KPIScoreCTE AS(
	                            SELECT A.UnitID,A.ECID,A.Shift,TotalScore=SUM(ISNULL(Score,0)) 
	                            FROM KPI_ECDayData A JOIN KPI_ECTag B ON A.ECID=B.ECID 
	                            WHERE  A.CheckDate BETWEEN @StartDate AND @EndDate AND A.UnitID=@UnitID AND 
	                                   B.ECWeb='JJZB' AND B.ECIsSnapshot=1
	                            GROUP BY A.UnitID,A.ECID,A.Shift),
                            KPIRankCTE AS(	
	                            SELECT A.*,TotalHours=C.Duration,AvgScore=A.TotalScore/C.Duration,
	                            DENSE_RANK() OVER(PARTITION BY A.ECID ORDER BY TotalScore/Duration DESC) as OrderNo
	                            FROM KPIScoreCTE  A  JOIN MonitorDurationCTE C ON A.UnitID=C.UnitID AND  A.Shift=C.Shift),
                            Shift1Rank AS(
                            SELECT  * FROM  KPIRankCTE  WHERE Shift=1),
                            Shift2Rank AS(
                            SELECT  * FROM  KPIRankCTE  WHERE Shift=2),
                            Shift3Rank AS(
                            SELECT  * FROM  KPIRankCTE  WHERE Shift=3),
                            Shift4Rank AS(
                            SELECT  * FROM  KPIRankCTE  WHERE Shift=4),
                            Shift5Rank AS(
                            SELECT  * FROM  KPIRankCTE  WHERE Shift=5),
                            CTE AS(
                                SELECT A.ECID,A.ECName,A.ECIndex,A.UnitID,
                                B.TotalScore Shift1Score,B.TotalHours Shift1Hours,B.AvgScore Shift1AvgScore, B.OrderNo Shift1Rank,
                                C.TotalScore Shift2Score,C.TotalHours Shift2Hours,C.AvgScore Shift2AvgScore, C.OrderNo Shift2Rank,
                                D.TotalScore Shift3Score,D.TotalHours Shift3Hours,D.AvgScore Shift3AvgScore, D.OrderNo Shift3Rank,
                                E.TotalScore Shift4Score,E.TotalHours Shift4Hours,E.AvgScore Shift4AvgScore, E.OrderNo Shift4Rank,
                                F.TotalScore Shift5Score,F.TotalHours Shift5Hours,F.AvgScore Shift5AvgScore, F.OrderNo Shift5Rank

                                FROM  KPI_ECTag A LEFT JOIN Shift1Rank B ON A.ECID=B.ECID
                                                  LEFT JOIN Shift2Rank C ON A.ECID=C.ECID
                                                  LEFT JOIN Shift3Rank D ON A.ECID=D.ECID
                                                  LEFT JOIN Shift4Rank E ON A.ECID=E.ECID
                                                  LEFT JOIN Shift5Rank F ON A.ECID=F.ECID                    
                                WHERE A.ECWeb='JJZB' AND A.ECIsSnapshot=1 AND A.UnitID=@UnitID )    
                            SELECT*FROM CTE";
            List<KPIRankEntity> Result;
            using (IDataReader Reader = DataBase.ExecuteReader(CommandType.Text, SqlText, parames)) {
                Result = Reader.FillGenericList<KPIRankEntity>();
            }
            return Result;
        }


        #endregion

        #region IDisposable
        public void Dispose() {

        }

        #endregion
    }
}
