using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using SIS.DBControl;
using SIS.DataEntity;


namespace SIS.DataAccess {
	public class KPI_OverLimitRecordDal : IKPI_OverLimitRecordDal, IDisposable {

		private const string sql_GetUnitEntity = @"SELECT ku.UnitID,ku.UnitName FROM KPI_Unit AS ku";

		private const string pro_OverLimitRecord = "pro_OverLimitRecordPaging";
		private const string pro_SeachOverLimitRecord = "Proc_SeachOverLimitRecord";
		private const string pro_OverLimitStatisticsPaging = "pro_OverLimitStatisticsPaging";
		private const string pro_SeachOverLimitStatistics = "pro_SeachOverLimitStatistics";

		private const string param_PageSize = "@PageSize";
		private const string param_PageIndex = "@PageIndex";
		private const string param_RowCount = "@RowCount";
		private const string param_KpiName = "@KpiID";
		private const string param_UnitCode = "@UnitID";
		private const string param_StartTime = "@StartDate";
		private const string param_EndTime = "@EndDate";
		private RelaInterface m_DB;


		public KPI_OverLimitRecordDal() {
			m_DB = DBAccess.GetRelation();
		}
        public List<KPI_OverLimitRecordEntity> GetOverLimitRecordEntityList(DateTime startTime, DateTime endTime, int startIndex, int pageSize, out int totalCount) {
			SqlParameter[] parames = new SqlParameter[] {
               new SqlParameter(param_StartTime,SqlDbType.DateTime),
                new SqlParameter(param_EndTime,SqlDbType.DateTime),
                new SqlParameter(param_PageIndex,DbType.Int32),  
                new SqlParameter(param_PageSize,DbType.Int32),
                new SqlParameter(param_RowCount,DbType.Int32),
            };

			parames[0].Value = startTime;
			parames[1].Value = endTime;
			parames[2].Value = startIndex;
			parames[3].Value = pageSize;
			parames[4].Direction = ParameterDirection.Output;
			IDataReader reader = null;
			List<KPI_OverLimitRecordEntity> overLimitRecordEntityList = new List<KPI_OverLimitRecordEntity>();
			try {
				reader = m_DB.ExecuteReader(CommandType.StoredProcedure, pro_OverLimitRecord, parames);
				while (reader.Read()) {
					overLimitRecordEntityList.Add(new KPI_OverLimitRecordEntity {
						AlarmID = reader["AlarmID"].ToString(),
						TagID = reader["TagID"] == DBNull.Value ? string.Empty : reader["TagID"].ToString(),
						KpiName = reader["RealDesc"] == DBNull.Value ? string.Empty : reader["RealDesc"].ToString(),
						AlarmStartTime = reader["AlarmStartTime"] == DBNull.Value ? null : (DateTime?)reader["AlarmStartTime"],
						AlarmEndTime = reader["AlarmEndTime"] == DBNull.Value ? null : (DateTime?)reader["AlarmEndTime"],
						Duration = reader["Duration"] == DBNull.Value ? null : (int?)reader["Duration"],
						AlarmValue = reader["AlarmValue"] == DBNull.Value ? null : (decimal?)reader["AlarmValue"],
						StandardValue = reader["StandardValue"] == DBNull.Value ? null : (decimal?)reader["StandardValue"],
						Offset = reader["Offset"] == DBNull.Value ? null : (decimal?)reader["Offset"],
						AlarmType = reader["AlarmType"] == DBNull.Value ? null : (int?)reader["AlarmType"],
						Shift = reader["Shift"] == DBNull.Value ? string.Empty : reader["Shift"].ToString(),
						Period = reader["Period"] == DBNull.Value ? string.Empty : (String)reader["Period"],
						//ShiftName = reader["ShiftName"] == DBNull.Value ? string.Empty : reader["ShiftName"].ToString(),
						UnitID = reader["UnitID"] == DBNull.Value ? string.Empty : reader["UnitID"].ToString(),
						UnitName = reader["UnitName"] == DBNull.Value ? string.Empty : reader["UnitName"].ToString()
					});
				}

				reader.Close();
				if (parames[4].Value != DBNull.Value) {
					totalCount = Convert.ToInt32(parames[4].Value);
				}
				else {
					totalCount = 0;
				}

			}
			catch (Exception ex) {

				throw ex;
			}
			finally {
				if (!reader.IsClosed) {
					reader.Close();
				}
			}


			return overLimitRecordEntityList;

		}


		public List<KPI_UnitEntity> GetUnitEntityList() {
			List<KPI_UnitEntity> unitEntityList = new List<KPI_UnitEntity>();

			using (IDataReader dataReader = m_DB.ExecuteReader(sql_GetUnitEntity)) {
				while (dataReader.Read()) {
					unitEntityList.Add(new KPI_UnitEntity {
						UnitID = dataReader["UnitID"].ToString(),
						UnitName = dataReader["UnitName"] == DBNull.Value ? string.Empty : dataReader["UnitName"].ToString(),
					});
				}
			}

			return unitEntityList;
		}


		public List<KPI_OverLimitRecordEntity> SearchOverLimitRecord(int startIndex, int pageSize,
			string kpiName, string unityCode, String Shift, DateTime beginTime, DateTime endTime, out int totalCount) {
			totalCount = 0;
			SqlParameter[] parames = new SqlParameter[] {
                new SqlParameter("@KpiID",SqlDbType.VarChar),
                new SqlParameter("@UnitID",SqlDbType.VarChar),
				new SqlParameter("@Shift",SqlDbType.VarChar),
                new SqlParameter("@StartDate",SqlDbType.DateTime),
                new SqlParameter("@EndDate",SqlDbType.DateTime),
                new SqlParameter("@PageIndex",SqlDbType.Int),  
                new SqlParameter("@PageSize",SqlDbType.Int)
            };

			parames[0].Value = kpiName;
			parames[1].Value = unityCode;
			parames[2].Value = Shift;
			parames[3].Value = beginTime;
			parames[4].Value = endTime;
			parames[5].Value = startIndex;
			parames[6].Value = pageSize;
			IDataReader reader = null;
			List<KPI_OverLimitRecordEntity> overLimitRecordEntityList = new List<KPI_OverLimitRecordEntity>();
			try {
				reader = m_DB.ExecuteReader(CommandType.StoredProcedure, "Proc_SeachOverLimitRecord", parames);
				while (reader.Read()) {
					overLimitRecordEntityList.Add(new KPI_OverLimitRecordEntity {
						AlarmID = reader["AlarmID"].ToString(),
						TagID = reader["TagID"] == DBNull.Value ? string.Empty : reader["TagID"].ToString(),
						KpiName = reader["RealDesc"] == DBNull.Value ? string.Empty : reader["RealDesc"].ToString(),
						AlarmStartTime = reader["AlarmStartTime"] == DBNull.Value ? null : (DateTime?)reader["AlarmStartTime"],
						AlarmEndTime = reader["AlarmEndTime"] == DBNull.Value ? null : (DateTime?)reader["AlarmEndTime"],
						Duration = reader["Duration"] == DBNull.Value ? null : (int?)reader["Duration"],
						AlarmValue = reader["AlarmValue"] == DBNull.Value ? null : (decimal?)reader["AlarmValue"],
						StandardValue = reader["StandardValue"] == DBNull.Value ? null : (decimal?)reader["StandardValue"],
						Offset = reader["Offset"] == DBNull.Value ? null : (decimal?)reader["Offset"],
						AlarmType = reader["AlarmType"] == DBNull.Value ? null : (int?)reader["AlarmType"],
						Shift = reader["Shift"] == DBNull.Value ? string.Empty : reader["Shift"].ToString(),
						Period = reader["Period"] == DBNull.Value ? String.Empty : (String)reader["Period"],
						//ShiftName = reader["ShiftName"] == DBNull.Value ? string.Empty : reader["ShiftName"].ToString(),
						UnitID = reader["UnitID"] == DBNull.Value ? string.Empty : reader["UnitID"].ToString(),
						UnitName = reader["UnitName"] == DBNull.Value ? string.Empty : reader["UnitName"].ToString(),
						RealDesc = reader["RealDesc"] == DBNull.Value ? string.Empty : reader["RealDesc"].ToString(),
						SAName = reader["SAName"] == DBNull.Value ? string.Empty : reader["SAName"].ToString(),
						RealCode = reader["RealCode"] == DBNull.Value ? string.Empty : reader["RealCode"].ToString()
					});
				}
				reader.NextResult();
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			catch (Exception ex) {

				throw ex;
			}
			finally {
				if (!reader.IsClosed) {
					reader.Close();
				}
			}


			return overLimitRecordEntityList;
		}

		//Added by pyf 2013-09-13
        public List<KPI_OverLimitRecordEntity> GetOverLimitRecords(DateTime beginTime, DateTime endTime,
            String tagCode) {
            List<KPI_OverLimitRecordEntity> Result;
            SqlParameter[] parames = new SqlParameter[] {              
                new SqlParameter("@BeginTime",SqlDbType.DateTime),
                new SqlParameter("@EndTime",SqlDbType.DateTime),  
                new SqlParameter("@TagID",SqlDbType.VarChar),
            };

            parames[0].Value = Convert.ToDateTime(beginTime);
            parames[1].Value = Convert.ToDateTime(endTime);
            parames[2].Value = tagCode;
            String SqlText = @" --指定时间范围内的报警记录
                                SELECT A.* FROM KPI_OverLimitRecord A JOIN KPI_RealTag B ON A.TagID =B.RealID 
								WHERE A.AlarmStartTime>=@BeginTime AND A.AlarmEndTime<=@EndTime AND B.RealCode=@TagID
								UNION
                                --前一小时起报本轮解报且超限时间未超过1小时的报警记录
								SELECT A.* FROM KPI_OverLimitRecord A JOIN KPI_RealTag B ON A.TagID =B.RealID 
								WHERE  DATEDIFF(hh, A.AlarmStartTime, @BeginTime)=1 AND A.AlarmEndTime BETWEEN @BeginTime AND @EndTime AND A.Duration<3600
								       AND B.RealCode=@TagID ";
            int Hours = beginTime.Hour;//交接班
            if ((Hours == 1) || (Hours == 8) || (Hours == 17)) {
                SqlText = @"--指定时间范围内的报警记录
                            SELECT A.* FROM KPI_OverLimitRecord A JOIN KPI_RealTag B ON A.TagID =B.RealID 
							WHERE A.AlarmStartTime>=@BeginTime AND A.AlarmEndTime<=@EndTime AND B.RealCode=@TagID
                            UNION
                            --前一小时起报本轮解报且超限时间未超过1小时的报警记录
                            SELECT AlarmID,TagID,AlarmStartTime,AlarmEndTime,
                                    Duration=DATEDIFF(s,@BeginTime,AlarmEndTime),AlarmValue,
                                    StandardValue,Offset,AlarmType,Period,Shift,A.UnitID 
                            FROM KPI_OverLimitRecord A JOIN KPI_RealTag B ON A.TagID =B.RealID 
                            WHERE  DATEDIFF(hh, A.AlarmStartTime, @BeginTime)=1 AND A.AlarmEndTime BETWEEN @BeginTime AND @EndTime AND A.Duration<3600
                                    AND B.RealCode=@TagID ";
            }
            using (IDataReader Reader = m_DB.ExecuteReader(CommandType.Text, SqlText, parames)) {
                Result = Reader.FillGenericList<KPI_OverLimitRecordEntity>();
                Reader.Close();
            }
            return Result;
        }

        public List<KPI_OverLimitRecordEntity> GetShiftOverLimitRecords(DateTime shiftStartTime, DateTime shiftEndTime,
            String tagCode) {
            List<KPI_OverLimitRecordEntity> Result;
            SqlParameter[] parames = new SqlParameter[] {              
                new SqlParameter("@BeginTime",SqlDbType.DateTime),
                new SqlParameter("@EndTime",SqlDbType.DateTime),  
                new SqlParameter("@TagID",SqlDbType.VarChar),
            };

            parames[0].Value = shiftStartTime;
            parames[1].Value = shiftEndTime;
            parames[2].Value = tagCode;
            String SqlText = @" --前一值值班期间起报在本值值班期间未解报
								SELECT AlarmID,TagID,AlarmStartTime,AlarmEndTime,
									   Duration=DATEDIFF(s,@BeginTime,@EndTime),AlarmValue,
									   StandardValue,Offset,AlarmType,Period,Shift,A.UnitID
								FROM KPI_OverLimitRecord A JOIN KPI_RealTag B ON A.TagID =B.RealID 
								WHERE (A.AlarmStartTime<=@BeginTime) AND (A.AlarmEndTime IS NULL) AND (B.RealCode=@TagID)
								UNION
								--本值值班期间起报但未解报
								SELECT  AlarmID,TagID,AlarmStartTime,AlarmEndTime,
										Duration=DATEDIFF(s,A.AlarmStartTime,@EndTime),AlarmValue,
										StandardValue,Offset,AlarmType,Period,Shift,A.UnitID
								FROM KPI_OverLimitRecord A JOIN KPI_RealTag B ON A.TagID =B.RealID 
								WHERE (A.AlarmStartTime BETWEEN @BeginTime AND @EndTime) AND (A.AlarmEndTime IS NULL) AND (B.RealCode=@TagID)
								UNION
								--前一值值班期间起报在本值值班期间解报切超限时长小于1小时
								SELECT AlarmID,TagID,AlarmStartTime,AlarmEndTime,
									   Duration=DATEDIFF(s,@BeginTime,A.AlarmEndTime),AlarmValue,
									   StandardValue,Offset,AlarmType,Period,Shift,A.UnitID
								FROM KPI_OverLimitRecord A JOIN KPI_RealTag B ON A.TagID =B.RealID 
								WHERE (A.AlarmStartTime<=@BeginTime) AND (A.AlarmEndTime Between @BeginTime AND @EndTime) 
                                      AND (B.RealCode=@TagID) AND (A.Duration>=3600)
                                UNION
                                --超限时长超过1小时的报警记录
                                SELECT AlarmID,TagID,AlarmStartTime,AlarmEndTime, Duration,AlarmValue,								  
								   StandardValue,Offset,AlarmType,Period,Shift,A.UnitID
								FROM KPI_OverLimitRecord A JOIN KPI_RealTag B ON A.TagID =B.RealID 
								WHERE (A.AlarmStartTime>=@BeginTime) AND (A.AlarmEndTime<=@EndTime)  AND (Duration>=3600) AND (B.RealCode=@TagID)";
            using (IDataReader Reader = m_DB.ExecuteReader(CommandType.Text, SqlText, parames)) {
                Result = Reader.FillGenericList<KPI_OverLimitRecordEntity>();
                Reader.Close();
            }
            return Result;
        }

        public List<KPI_OverLimitRecordEntity> GetShiftOverLimitRecords(DateTime shiftStartTime, DateTime shiftEndTime,
            String tagCode, int alarmType) {
            List<KPI_OverLimitRecordEntity> Result;
            SqlParameter[] parames = new SqlParameter[] {              
                new SqlParameter("@BeginTime",DbType.DateTime),
                new SqlParameter("@EndTime",DbType.DateTime),  
                new SqlParameter("@TagID",DbType.String),
				new SqlParameter("@AlarmType",DbType.Int32)
            };


            parames[0].Value = shiftStartTime;
            parames[1].Value = shiftEndTime;
            parames[2].Value = tagCode;
            parames[3].Value = alarmType;
            String SqlText = @" --前一值值班期间起报在本值值班期间未解报
								SELECT AlarmID,TagID,AlarmStartTime,AlarmEndTime,
									   Duration=DATEDIFF(s,@BeginTime,@EndTime),AlarmValue,
									   StandardValue,Offset,AlarmType,Period,Shift,A.UnitID
								FROM KPI_OverLimitRecord A JOIN KPI_RealTag B ON A.TagID =B.RealID 
								WHERE (A.AlarmStartTime<=@BeginTime) AND (A.AlarmEndTime IS NULL) AND (B.RealCode=@TagID) AND (A.AlarmType=@AlarmType)
								UNION
								--本值值班期间起报但未解报
								SELECT  AlarmID,TagID,AlarmStartTime,AlarmEndTime,
										Duration=DATEDIFF(s,A.AlarmStartTime,@EndTime),AlarmValue,
										StandardValue,Offset,AlarmType,Period,Shift,A.UnitID
								FROM KPI_OverLimitRecord A JOIN KPI_RealTag B ON A.TagID =B.RealID 
								WHERE (A.AlarmStartTime BETWEEN @BeginTime AND @EndTime) AND (A.AlarmEndTime IS NULL) AND (B.RealCode=@TagID) AND (A.AlarmType=@AlarmType)
								UNION
								--前一值值班期间起报在本值值班期间解报且超限时间超过1小时的报警记录
								SELECT AlarmID,TagID,AlarmStartTime,AlarmEndTime,
									   Duration=DATEDIFF(s,@BeginTime,A.AlarmEndTime),AlarmValue,
									   StandardValue,Offset,AlarmType,Period,Shift,A.UnitID
								FROM KPI_OverLimitRecord A JOIN KPI_RealTag B ON A.TagID =B.RealID 
								WHERE (A.AlarmStartTime<=@BeginTime) AND (A.AlarmEndTime Between @BeginTime AND @EndTime) 
                                       AND (B.RealCode=@TagID) AND (A.AlarmType=@AlarmType) AND (A.Duration>=3600)
								UNION
								--超限时长超过1小时的报警记录
								SELECT AlarmID,TagID,AlarmStartTime,AlarmEndTime,
									   Duration,AlarmValue,
									   StandardValue,Offset,AlarmType,Period,Shift,A.UnitID
								FROM KPI_OverLimitRecord A JOIN KPI_RealTag B ON A.TagID =B.RealID 
								WHERE (A.AlarmStartTime>=@BeginTime) AND (A.AlarmEndTime<=@EndTime)  AND (Duration>=3600) AND (B.RealCode=@TagID) AND (A.AlarmType=@AlarmType)";
            using (IDataReader Reader = m_DB.ExecuteReader(CommandType.Text, SqlText, parames)) {
                Result = Reader.FillGenericList<KPI_OverLimitRecordEntity>();
                Reader.Close();
            }
            return Result;
        }


        public List<KPI_OverLimitRecordEntity> GetOverLimitRecords(DateTime beginTime, DateTime endTime,
            String tagCode, int alarmType) {

            List<KPI_OverLimitRecordEntity> Result;
            SqlParameter[] parames = new SqlParameter[] {              
                new SqlParameter("@BeginTime",DbType.DateTime),
                new SqlParameter("@EndTime",DbType.DateTime),  
                new SqlParameter("@TagID",DbType.String),
				new SqlParameter("@AlarmType",DbType.Int32)
            };


            parames[0].Value = Convert.ToDateTime(beginTime);
            parames[1].Value = Convert.ToDateTime(endTime);
            parames[2].Value = tagCode;
            parames[3].Value = alarmType;
            String SqlText = @"--给定时间范围内的报警记录
                               SELECT A.* FROM KPI_OverLimitRecord A JOIN KPI_RealTag B ON A.TagID =B.RealID 
								WHERE A.AlarmStartTime>=@BeginTime AND A.AlarmEndTime<=@EndTime 
								AND B.RealCode=@TagID AND A.AlarmType=@AlarmType
								UNION
                                --前一小时起报本轮解报且超限时间未超过1小时的报警记录
								SELECT A.* FROM KPI_OverLimitRecord A JOIN KPI_RealTag B ON A.TagID =B.RealID 
								WHERE  DATEDIFF(hh, A.AlarmStartTime, @BeginTime)=1 AND A.AlarmEndTime BETWEEN @BeginTime AND @EndTime AND A.Duration<3600
								AND B.RealCode=@TagID AND A.AlarmType=@AlarmType";
            int Hours = beginTime.Hour;//交接班
            if ((Hours == 1) || (Hours == 8) || (Hours == 17)) {
                SqlText = @"    SELECT A.* FROM KPI_OverLimitRecord A JOIN KPI_RealTag B ON A.TagID =B.RealID 
								WHERE A.AlarmStartTime>=@BeginTime AND A.AlarmEndTime<=@EndTime 
								AND B.RealCode=@TagID AND A.AlarmType=@AlarmType
								UNION
                                --前一小时起报本轮解报且超限时间未超过1小时的报警记录
								SELECT AlarmID,TagID,AlarmStartTime,AlarmEndTime,
									   Duration=DATEDIFF(s,@BeginTime,AlarmEndTime),AlarmValue,
									   StandardValue,Offset,AlarmType,Period,Shift,A.UnitID 
								FROM KPI_OverLimitRecord A JOIN KPI_RealTag B ON A.TagID =B.RealID 
								WHERE  DATEDIFF(hh, A.AlarmStartTime, @BeginTime)=1 AND A.AlarmEndTime BETWEEN @BeginTime AND @EndTime AND A.Duration<3600
								AND B.RealCode=@TagID AND A.AlarmType=@AlarmType";
            }
            using (IDataReader Reader = m_DB.ExecuteReader(CommandType.Text, SqlText, parames)) {
                Result = Reader.FillGenericList<KPI_OverLimitRecordEntity>();
                Reader.Close();
            }
            return Result;
        }

		/// <summary>
		/// 新增超限报警记录数据
		/// </summary>
		/// <param name="item">超限报警记录实体</param>        
		/// <returns>新增的数据行数</returns>
		public int AddOverLimitRecord(KPI_OverLimitRecordEntity item) {
			int Result = 0;
			string SqlText = @"Insert KPI_OverLimitRecord (AlarmID,TagID,AlarmStartTime,AlarmEndTime,
								Duration,AlarmValue,StandardValue,Offset,AlarmType,Period,Shift,UnitID) 
								Values (@AlarmID,@TagID,@AlarmStartTime,@AlarmEndTime,@Duration,@AlarmValue,
								@StandardValue,@Offset,@AlarmType,@Period,@Shift,@UnitID) ";
			IDbDataParameter AlarmIDParam = new SqlParameter("@AlarmID", item.AlarmID);
			IDbDataParameter TagIDParam = new SqlParameter("@TagID", item.TagID);
			IDbDataParameter AlarmStartTimeParam = new SqlParameter("@AlarmStartTime", item.AlarmStartTime);
			IDbDataParameter AlarmEndTimeParam = new SqlParameter("@AlarmEndTime", DBNull.Value);
			IDbDataParameter DurationParam = new SqlParameter("@Duration", DBNull.Value);
			IDbDataParameter AlarmValueParam = new SqlParameter("@AlarmValue", item.AlarmValue);
			IDbDataParameter StandardValueParam = new SqlParameter("@StandardValue", item.StandardValue);
			IDbDataParameter OffsetParam = new SqlParameter("@Offset", item.Offset);
			IDbDataParameter AlarmTypeParam = new SqlParameter("@AlarmType", item.AlarmType);
			IDbDataParameter PeriodParam = new SqlParameter("@Period", item.Period);
			IDbDataParameter ShiftParam = new SqlParameter("@Shift", item.Shift);
			IDbDataParameter UnitIDParam = new SqlParameter("@UnitID", item.UnitID);
			//RelaInterface DB = DBAccess.GetRelation();
			try {
				Result = m_DB.ExecuteNonQuery(SqlText, AlarmIDParam, TagIDParam, AlarmStartTimeParam,
					AlarmEndTimeParam, DurationParam, AlarmValueParam, StandardValueParam, OffsetParam,
					AlarmTypeParam, PeriodParam, ShiftParam, UnitIDParam);
			}
			catch (Exception ex) {
				throw ex;
			}
			return Result;
		}


		/// <summary>
		/// 更新超限报警记录数据
		/// </summary>
		/// <param name="item">超限报警记录实体</param>        
		/// <returns>更新的数据行数</returns>
		public int UpdateOverLimitRecord(KPI_OverLimitRecordEntity item) {
			int Result = 0;
			String SqlText = @"IF EXISTS(SELECT TAGID FROM KPI_OverLimitRecord 	Where (TagID=@TagID AND AlarmType=@AlarmType)  AND (AlarmEndTime IS NULL))
							  BEGIN
								Update KPI_OverLimitRecord Set AlarmEndTime=@AlarmEndTime,
																Duration=DATEDIFF(s,AlarmStartTime,@AlarmEndTime)  
																Where (TagID=@TagID AND AlarmType=@AlarmType)  AND (AlarmEndTime IS NULL)
							  END";
			IDbDataParameter[] parames = new SqlParameter[] {              
                new SqlParameter("@AlarmEndTime",DbType.DateTime),
                new SqlParameter("@TagID",DbType.String),  
                new SqlParameter("@AlarmType",DbType.String)};
			parames[0].Value = item.AlarmEndTime;
			parames[1].Value = item.TagID;
			parames[2].Value = item.AlarmType;
			//RelaInterface DB = DBAccess.GetRelation();
			try {
				Result = m_DB.ExecuteNonQuery(SqlText, parames);
			}
			catch (Exception ex) {
				throw ex;
			}
			return Result;
		}

		/// <summary>
		/// 更新报警时间段内最大值
		/// </summary>
		/// <param name="item"></param>
		public void UpdateAlarmMaxValue(KPI_OverLimitRecordEntity item) {
			String SqlText = @"Update KPI_OverLimitRecord Set AlarmValue=@AlarmValue,Offset= @AlarmValue - StandardValue 
								WHERE (TagID=@TagID AND AlarmType=@AlarmType) AND (AlarmEndTime IS NULL) AND (AlarmValue<@AlarmValue) ";
			IDbDataParameter[] parames = new SqlParameter[] {              
                new SqlParameter("@AlarmValue",DbType.Decimal),
                new SqlParameter("@TagID",DbType.String),  
                new SqlParameter("@AlarmType",DbType.String)};
			parames[0].Value = item.AlarmValue;
			parames[1].Value = item.TagID;
			parames[2].Value = item.AlarmType;
			try {
				m_DB.ExecuteNonQuery(SqlText, parames);
			}
			catch (Exception ex) {
				throw ex;
			}
		}

		public bool ExistsOverLimitRecord(KPI_OverLimitRecordEntity item) {
			bool Result = false;
			string SqlText = @"SELECT TagID FROM KPI_OverLimitRecord
							   WHERE (TagID=@TagID AND AlarmType=@AlarmType)  AND (AlarmEndTime IS NULL)";
			IDbDataParameter TagIDParam = new SqlParameter("@TagID", item.TagID);
			IDbDataParameter AlarmTypeParam = new SqlParameter("@AlarmType", item.AlarmType);
			//RelaInterface DB = DBAccess.GetRelation();
			try {
				Object obj = m_DB.ExecuteScalar(SqlText, TagIDParam, AlarmTypeParam);
				Result = obj != null;
			}
			catch (Exception ex) {
				throw ex;
			}
			return Result;
		}

		/// <summary>
		/// 可靠性台账
		/// </summary>
		/// <param name="pageIndex">页号</param>
		/// <param name="pageSize">页面大小</param>
		/// <param name="startTime">开始时间</param>
		/// <param name="endTime">结束时间</param>
		/// <param name="totalRecordCount">总记录数</param>
		/// <returns></returns>
		public List<KPI_OverLimitRecordEntity> GetReliabilitys(int pageIndex, int pageSize,
			DateTime startTime, DateTime endTime, String TagID,String Shift, out int totalRecordCount) {
			List<KPI_OverLimitRecordEntity> Result;
			totalRecordCount = 0;
			String WHERE = "A.AlarmStartTime BETWEEN @StartDate AND @EndDate AND B.RealNote='1'";
			String CTE = @"WITH CTE 
								AS
								(SELECT A.*,B.RealCode,KpiName=B.RealDesc, ROW_NUMBER() OVER (ORDER BY AlarmStartTime )AS R 
								 FROM KPI_OverLimitRecord A JOIN KPI_RealTag B ON A.TagID =B.RealID 
								 WHERE {0})";
			if (!String.IsNullOrEmpty(TagID)) {
				WHERE += " AND A.TagID=@TagID ";	
			}

			if (!String.IsNullOrEmpty(Shift)) {
				WHERE += " AND A.Shift=@Shift ";
			}
			CTE = String.Format(CTE, WHERE);

			String SqlText = CTE + @" SELECT * FROM CTE WHERE R between (@pageindex - 1) * @pagesize + 1 and (@pageindex - 1) * @pagesize + @pagesize;" +
							  CTE + "SELECT COUNT(AlarmID) FROM CTE";

			IDbDataParameter[] parames = new SqlParameter[] {              
                new SqlParameter("@pageindex",DbType.Int32),
				new SqlParameter("@pagesize",DbType.Int32),
                new SqlParameter("@StartDate",DbType.DateTime),  
                new SqlParameter("@EndDate",DbType.DateTime),
			    new SqlParameter("@TagID",DbType.DateTime),
			    new SqlParameter("@Shift",DbType.String)};
			parames[0].Value = pageIndex;
			parames[1].Value = pageSize;
			parames[2].Value = startTime;
			parames[3].Value = endTime;
			parames[4].Value = TagID;
			parames[5].Value = Shift;
			using (IDataReader Reader = m_DB.ExecuteReader(CommandType.Text, SqlText, parames)) {
				Result = Reader.FillGenericList<KPI_OverLimitRecordEntity>();
				Reader.NextResult();
				Reader.Read();
				totalRecordCount = Reader.GetInt32(0);
				Reader.Close();
			}
			return Result;
		}

		public List<KPI_OverLimitRecordEntity> GetReliabilityTotals(DateTime startTime, DateTime endTime,String Shift) {
			List<KPI_OverLimitRecordEntity> Result;
			String SqlText = @"SELECT A.Shift,TagID=B.RealID,KpiName=B.RealDesc,B.RealCode,Duration=SUM(ISNULL(A.Duration,0))
								FROM KPI_OverLimitRecord A JOIN KPI_RealTag B ON A.TagID =B.RealID 
								WHERE A.AlarmStartTime BETWEEN @StartDate AND @EndDate  AND B.RealNote='1'
								GROUP BY A.Shift,B.RealID,B.RealDesc,B.RealCode";
			if (!String.IsNullOrEmpty(Shift)) {
				SqlText = @"SELECT A.Shift,TagID=B.RealID,KpiName=B.RealDesc,B.RealCode,Duration=SUM(ISNULL(A.Duration,0))
								FROM KPI_OverLimitRecord A JOIN KPI_RealTag B ON A.TagID =B.RealID 
								WHERE A.AlarmStartTime BETWEEN @StartDate AND @EndDate  AND A.Shift=@Shift AND B.RealNote='1'
								GROUP BY A.Shift,B.RealID,B.RealDesc,B.RealCode";
			}

			IDbDataParameter[] parames = new SqlParameter[] { 
                new SqlParameter("@StartDate",DbType.DateTime),  
                new SqlParameter("@EndDate",DbType.DateTime),
			    new SqlParameter("@Shift",DbType.String)};
			parames[0].Value = startTime;
			parames[1].Value = endTime;
			parames[2].Value = Shift;
			using (IDataReader Reader = m_DB.ExecuteReader(CommandType.Text, SqlText, parames)) {
				Result = Reader.FillGenericList<KPI_OverLimitRecordEntity>();
				Reader.NextResult();
				Reader.Close();
			}
			return Result;
		}

		//End of Added.


		public List<KPI_OverLimitRecordEntity> GetOverLimitRecordEntityList(String UnitID, String KPIID, String Shift,
			DateTime startTime, DateTime endTime, int PageIndex, int PageSize, out int totalCount) {
			totalCount = 0;
			SqlParameter[] parames = new SqlParameter[] {
				new SqlParameter("@KpiID",SqlDbType.VarChar),
				new SqlParameter("@UnitID",SqlDbType.VarChar),
				new SqlParameter("@Shift",SqlDbType.VarChar),
                new SqlParameter("@StartDate",SqlDbType.DateTime),
                new SqlParameter("@EndDate",SqlDbType.DateTime),
                new SqlParameter("@PageIndex",DbType.Int32),  
                new SqlParameter("@PageSize",DbType.Int32)};

			parames[0].Value = KPIID;
			parames[1].Value = UnitID;
			parames[2].Value = Shift;
			parames[3].Value = startTime;
			parames[4].Value = endTime;
			parames[5].Value = PageIndex;
			parames[6].Value = PageSize;

			IDataReader reader = null;
			List<KPI_OverLimitRecordEntity> Result = new List<KPI_OverLimitRecordEntity>();
			try {
				reader = m_DB.ExecuteReader(CommandType.StoredProcedure, "Proc_SeachOverLimits", parames);
				while (reader.Read()) {
					Result.Add(new KPI_OverLimitRecordEntity {
						RealDesc = reader["RealDesc"] == DBNull.Value ? string.Empty : reader["RealDesc"].ToString(),
						SAName = reader["SAName"] == DBNull.Value ? string.Empty : reader["SAName"].ToString(),
						AlarmType = reader["AlarmType"] == DBNull.Value ? null : (int?)reader["AlarmType"],
						Shift = reader["Shift"] == DBNull.Value ? null : reader["Shift"].ToString(),
						UnitName = reader["UnitName"] == DBNull.Value ? null : reader["UnitName"].ToString(),
						Duration = reader["TotalDuration"] == DBNull.Value ? null : (int?)reader["TotalDuration"],
						AlarmCount = reader["TotalCount"] == DBNull.Value ? 0 : (int)reader["TotalCount"]
					});
				}
				reader.NextResult();
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			catch (Exception ex) {
				throw ex;
			}
			finally {
				if (!reader.IsClosed) {
					reader.Close();
				}
			}
			return Result;
		}

		public List<KPI_OverLimitRecordEntity> SearchOverLimitRecord(int startIndex, int pageSize, string kpiName, string unityCode, string beginTime, string endTime, bool isStatistics, ref int totalCount) {
			//if (!isStatistics) {
			//    //return SearchOverLimitRecord(startIndex, pageSize, kpiName, unityCode, beginTime, endTime, ref totalCount);
			//}
			//else {
			SqlParameter[] parames = new SqlParameter[] {
                new SqlParameter(param_KpiName,SqlDbType.VarChar),
                new SqlParameter(param_UnitCode,SqlDbType.VarChar),
                new SqlParameter(param_StartTime,SqlDbType.DateTime),
                new SqlParameter(param_EndTime,SqlDbType.DateTime),
                new SqlParameter(param_PageIndex,SqlDbType.Int),  
                new SqlParameter(param_PageSize,SqlDbType.Int),
                new SqlParameter(param_RowCount,SqlDbType.Int),
            };

			parames[0].Value = kpiName;
			parames[1].Value = unityCode;
			parames[2].Value = Convert.ToDateTime(beginTime);
			parames[3].Value = Convert.ToDateTime(endTime);
			parames[4].Value = startIndex;
			parames[5].Value = pageSize;
			parames[6].Direction = ParameterDirection.Output;
			IDataReader reader = null;
			List<KPI_OverLimitRecordEntity> overLimitRecordEntityList = new List<KPI_OverLimitRecordEntity>();

			try {

				reader = m_DB.ExecuteReader(CommandType.StoredProcedure, pro_SeachOverLimitStatistics, parames);

				while (reader.Read()) {
					overLimitRecordEntityList.Add(new KPI_OverLimitRecordEntity {

						TagID = reader["TagID"] == DBNull.Value ? string.Empty : reader["TagID"].ToString(),
						KpiName = reader["RealDesc"] == DBNull.Value ? string.Empty : reader["RealDesc"].ToString(),
						AlarmType = reader["AlarmType"] == DBNull.Value ? null : (int?)reader["AlarmType"],
						Duration = reader["TotalDuration"] == DBNull.Value ? null : (int?)reader["TotalDuration"],
						AlarmCount = reader["TotalCount"] == DBNull.Value ? 0 : (int)reader["TotalCount"]
					});
				}

				reader.Close();
				if (parames[6].Value != DBNull.Value) {
					totalCount = Convert.ToInt32(parames[6].Value);
				}
				else {
					totalCount = 0;
				}

			}
			catch (Exception ex) {

				throw ex;
			}
			finally {
				if (!reader.IsClosed) {
					reader.Close();
				}
			}
			return overLimitRecordEntityList;
			//}
		}

		public void Dispose() {

		}
	}
}
