using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIS.DataAccess;
using SIS.DataEntity;
using System.Data.SqlClient;
using System.Data;
using SIS.DBControl;


namespace SIS.DataAccess {

	public class KPI_SATagValueDal :  IDisposable {

		private RelaInterface DataBase; 

		#region 构造器

		public KPI_SATagValueDal() {
			DataBase = DBAccess.GetRelation();
		}

		#endregion

		#region 公共方法

		/// <summary>
		///获取所有快照数据
		/// </summary>
		public List<KPI_SATagValueEntity> GetKPI_SATagSnapshotValues(String Shift, DateTime ShiftStartTime,
			DateTime ShiftEndTime) {
			List<KPI_SATagValueEntity> Result = null;
			string SqlText = @"Proc_GetSATagSnapshotValue";
			IDbDataParameter[] parames = new SqlParameter[] {
                new SqlParameter("@ShiftStartDate",DbType.DateTime),
                new SqlParameter("@ShiftEndDate",DbType.DateTime),
                new SqlParameter("@Shift",DbType.String)                
            };
			parames[0].Value = ShiftStartTime;
			parames[1].Value = ShiftEndTime;
			parames[2].Value = Shift;
			using (IDataReader Reader = DataBase.ExecuteReader(CommandType.StoredProcedure, SqlText, parames)) {
				Result = Reader.FillGenericList<KPI_SATagValueEntity>();
			}
			return Result;			
		}


		/// <summary>
		/// 获取数据
		/// </summary>
		/// <param name="SAID"></param> 
		/// <param name="CalcDateTime"></param> 
		/// <param name="Shift"></param> 
		/// <returns>实体</returns>
		public List<KPI_SATagValueEntity> GetKPI_SATagValues(DateTime StartDate,DateTime EndDate,String Unit) {
			string SqlText = @"WITH SAScoreCTE AS(
								SELECT SAID,Shift,SAScore=SUM(SAScore),
										TotalCount=SUM(TotalCount),TotalDuration=SUM(TotalDuration) 
								FROM  KPI_SATagValue 
								WHERE CalcDateTime BETWEEN @StartDate AND @EndDate
								GROUP BY SAID,Shift)
								SELECT A.*,B.SAName FROM SAScoreCTE A JOIN KPI_SATag B 
								ON A.SAID=B.SAID 
								WHERE B.UnitID=@UnitID ORDER BY Shift";
			List<KPI_SATagValueEntity> Result = null;
			IDbDataParameter[] parames = new SqlParameter[] {
                new SqlParameter("@StartDate",DbType.DateTime),
                new SqlParameter("@EndDate",DbType.DateTime),
                new SqlParameter("@UnitID",DbType.String)                
            };
			parames[0].Value = StartDate;
			parames[1].Value = EndDate;
			parames[2].Value = Unit;
			using (IDataReader Reader = DataBase.ExecuteReader(CommandType.Text, SqlText, parames)) {
				Result = Reader.FillGenericList<KPI_SATagValueEntity>();
			}
			KPI_SATagValueEntity Sum = new KPI_SATagValueEntity();
			Sum.Shift = "合计";
			//Sum.SAName = "合计";
			Sum.SAScore = Result.Sum(p => p.SAScore);
			Sum.TotalCount = Result.Sum(p => p.TotalCount);
			Sum.TotalDuration = Result.Sum(p => p.TotalDuration);
			Result.Add(Sum);
			return Result;
		}

		/// <summary>
		/// 获取数据
		/// </summary>
		/// <param name="SAID"></param> 
		/// <param name="CalcDateTime"></param> 
		/// <param name="Shift"></param> 
		/// <returns>实体</returns>
		public KPI_SATagValueEntity GetKPI_SATagValue(string SAID, DateTime CalcDateTime, string Shift) {
			string SqlText = "SELECT * FROM  KPI_SATagValue WHERE SAID=@SAID AND CalcDateTime=@CalcDateTime AND Shift=@Shift ";
			KPI_SATagValueEntity Result = null;
			IDbDataParameter[] parames = new SqlParameter[] {
                new SqlParameter("@SAID",DbType.String),
                new SqlParameter("@CalcDateTime",DbType.DateTime),
                new SqlParameter("@Shift",DbType.String)                
            };
			parames[0].Value = SAID;
			parames[1].Value = CalcDateTime;
			parames[2].Value = Shift;
			using(IDataReader Reader= DataBase.ExecuteReader(CommandType.Text,SqlText,parames)){
				Result = Reader.FillEntity<KPI_SATagValueEntity>();
			}
			return Result;
		}

		/// <summary>
		/// 保存数据
		/// </summary>
		/// <param name="SATagValue">实体</param>        
		/// <returns>成功保存的行数</returns>
		public int SaveKPI_SATagValue(KPI_SATagValueEntity SATagValue) {
			int Result = 0;
			AddSATagSnapshotValue(SATagValue);
			if (Exists(SATagValue)) {
				Result = UpdateKPI_SATagValue(SATagValue);
			}
			else {
				Result = AddKPI_SATagValue(SATagValue);
			}
			return Result;
		}

		/// <summary>
		/// 删除数据
		/// </summary>
		/// <param name="SATagValue">实体</param>        
		/// <returns>删除的数据行数</returns>
		public int DeleteKPI_SATagValue(KPI_SATagValueEntity SATagValue) {
			int Result = 0;
			string SqlText = @"DELETE FROM KPI_SATagValue WHERE SAID=@SAID AND 
							   CalcDateTime=@CalcDateTime AND Shift=@Shift ";
			IDbDataParameter[] parames = new SqlParameter[] {
                new SqlParameter("@SAID",DbType.String),
                new SqlParameter("@CalcDateTime",DbType.DateTime),
                new SqlParameter("@Shift",DbType.String)                
            };
			parames[0].Value = SATagValue.SAID;
			parames[1].Value = SATagValue.CalcDateTime;
			parames[2].Value = SATagValue.Shift;

			Result = DataBase.ExecuteNonQuery(SqlText, parames);
			return Result;
		}

		/// <summary>
		/// 剔除指标
		/// </summary>
		/// <param name="SAID"></param>
		/// <param name="StartDate"></param>
		/// <param name="EndDate"></param>
		public void RemovedSATagValue(String SAID, DateTime StartDate, DateTime EndDate) {
			string SqlText = @"UPDATE KPI_SATagValue SET IsValid=0  WHERE SAID=@SAID AND 
							   CalcDateTime BETWEEN @StartDate AND @EndDate";
			IDbDataParameter[] parames = new SqlParameter[] {
                new SqlParameter("@SAID",DbType.String),
                new SqlParameter("@StartDate",DbType.DateTime),
                new SqlParameter("@EndDate",DbType.DateTime)                
            };
			parames[0].Value = SAID;
			parames[1].Value = StartDate;
			parames[2].Value = EndDate;
			DataBase.ExecuteNonQuery(SqlText, parames);			
		}


		/// <summary>
		/// 恢复被剔除的指标
		/// </summary>
		/// <param name="SAID"></param>
		/// <param name="StartDate"></param>
		/// <param name="EndDate"></param>
		public void RestoredSATagValue(String SAID, DateTime StartDate, DateTime EndDate) {
			string SqlText = @"UPDATE KPI_SATagValue SET IsValid=1  WHERE SAID=@SAID AND 
							   CalcDateTime BETWEEN @StartDate AND @EndDate";
			IDbDataParameter[] parames = new SqlParameter[] {
                new SqlParameter("@SAID",DbType.String),
                new SqlParameter("@StartDate",DbType.DateTime),
                new SqlParameter("@EndDate",DbType.DateTime)                
            };
			parames[0].Value = SAID;
			parames[1].Value = StartDate;
			parames[2].Value = EndDate;
			DataBase.ExecuteNonQuery(SqlText, parames);
		}
		#endregion

		#region 私有方法

		/// <summary>
		/// 添加快照数据
		/// </summary>
		/// <param name="SATagValue"></param>
		public void AddSATagSnapshotValue(KPI_SATagValueEntity SATagValue) {
			string SqlText = string.Format("DELETE KPI_SATagSnapshotValue WHERE  SAID='{0}'", SATagValue.SAID);
			DataBase.ExecuteNonQuery(SqlText);
			SqlText = @"Insert KPI_SATagSnapshotValue (Period,SAScore,TotalCount,TotalDuration,SAID,CalcDateTime,
								Shift) Values (@Period,@SAScore,@TotalCount,@TotalDuration,@SAID,@CalcDateTime,@Shift) ";
			IDbDataParameter[] parames = new SqlParameter[] {
				new SqlParameter("@Period",DbType.String),
				new SqlParameter("@SAScore",DbType.Decimal),
				new SqlParameter("@TotalCount",DbType.Int32),
				new SqlParameter("@TotalDuration",DbType.Decimal),
                new SqlParameter("@SAID",DbType.String),
                new SqlParameter("@CalcDateTime",DbType.DateTime),
                new SqlParameter("@Shift",DbType.String)                
            };
			parames[0].Value = SATagValue.Period;
			parames[1].Value = SATagValue.SAScore;
			parames[2].Value = SATagValue.TotalCount;
			parames[3].Value = SATagValue.TotalDuration;
			parames[4].Value = SATagValue.SAID;
			parames[5].Value = SATagValue.CalcDateTime;
			parames[6].Value = SATagValue.Shift;
			DataBase.ExecuteNonQuery(SqlText, parames);			
		}

		/// <summary>
		/// 新增数据
		/// </summary>
		/// <param name="SATagValue">实体</param>        
		/// <returns>新增的数据行数</returns>
		private int AddKPI_SATagValue(KPI_SATagValueEntity SATagValue) {
			int Result = 0;
			string SqlText = @"Insert KPI_SATagValue (Period,SAScore,TotalCount,TotalDuration,SAID,CalcDateTime,
								Shift) Values (@Period,@SAScore,@TotalCount,@TotalDuration,@SAID,@CalcDateTime,@Shift) ";
			IDbDataParameter[] parames = new SqlParameter[] {
				new SqlParameter("@Period",DbType.String),
				new SqlParameter("@SAScore",DbType.Decimal),
				new SqlParameter("@TotalCount",DbType.Int32),
				new SqlParameter("@TotalDuration",DbType.Decimal),
                new SqlParameter("@SAID",DbType.String),
                new SqlParameter("@CalcDateTime",DbType.DateTime),
                new SqlParameter("@Shift",DbType.String)                
            };
			parames[0].Value = SATagValue.Period;
			parames[1].Value = SATagValue.SAScore;
			parames[2].Value = SATagValue.TotalCount;
			parames[3].Value = SATagValue.TotalDuration;
			parames[4].Value = SATagValue.SAID;
			parames[5].Value = SATagValue.CalcDateTime;
			parames[6].Value = SATagValue.Shift;
			Result = DataBase.ExecuteNonQuery(SqlText, parames);
			return Result;
		}


		/// <summary>
		/// 更新数据
		/// </summary>
		/// <param name="SATagValue">实体</param>        
		/// <returns>更新的数据行数</returns>
		private int UpdateKPI_SATagValue(KPI_SATagValueEntity SATagValue) {
			int Result = 0;
			string SqlText = @"Update KPI_SATagValue Set SAScore=SAScore + @SAScore , 
								TotalCount=TotalCount + @TotalCount,TotalDuration=TotalDuration + @TotalDuration  
								Where SAID = @SAID and CalcDateTime = @CalcDateTime and Shift = @Shift ";
			IDbDataParameter[] parames = new SqlParameter[] {
				new SqlParameter("@Period",DbType.String),
				new SqlParameter("@SAScore",DbType.Decimal),
				new SqlParameter("@TotalCount",DbType.Int32),
				new SqlParameter("@TotalDuration",DbType.Decimal),
                new SqlParameter("@SAID",DbType.String),
                new SqlParameter("@CalcDateTime",DbType.DateTime),
                new SqlParameter("@Shift",DbType.String)                
            };
			parames[0].Value = SATagValue.Period;
			parames[1].Value = SATagValue.SAScore;
			parames[2].Value = SATagValue.TotalCount;
			parames[3].Value = SATagValue.TotalDuration;
			parames[4].Value = SATagValue.SAID;
			parames[5].Value = SATagValue.CalcDateTime;
			parames[6].Value = SATagValue.Shift;
			Result = DataBase.ExecuteNonQuery(SqlText, parames);			
			return Result;
		}


		/// <summary>
		/// 判断数据是否存在
		/// </summary>
		/// <param name="SATagValue">实体</param>        
		/// <returns>数据存在则返回true否则返回false</returns>
		private bool Exists(KPI_SATagValueEntity SATagValue) {
			bool Result = false;
			IDbDataParameter[] parames = new SqlParameter[] {
                new SqlParameter("@SAID",DbType.String),
                new SqlParameter("@CalcDateTime",DbType.DateTime),
                new SqlParameter("@Shift",DbType.String)                
            };
			parames[0].Value = SATagValue.SAID;
			parames[1].Value = SATagValue.CalcDateTime;
			parames[2].Value = SATagValue.Shift;

			string SqlText = "SELECT * FROM  KPI_SATagValue WHERE SAID=@SAID AND CalcDateTime=@CalcDateTime AND Shift=@Shift ";
			using (IDataReader Reader = DataBase.ExecuteReader(CommandType.Text,SqlText,parames)) {
				Result = Reader.Read();
			}
			return Result;
		}

		#endregion

		#region IDisposable 成员

		public void Dispose() {
			DataBase = null;
		}

		#endregion
	}
}
