using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SIS.Loger;
using SIS.DataAccess;
using SIS.DataEntity;
using SIS.DBControl;

namespace SIS.Assistant.WS {

	public class WS_RaceSubMethod {

		private RelaInterface m_DB;
		private RTInterface m_RTDB;

		internal WS_RaceSubMethod() {
			m_DB = DBAccess.GetRelation();
			m_RTDB = DBAccess.GetRealTime();
		}
		/// <summary>
		/// 关系库是否连接正常
		/// </summary>
		/// <returns></returns>
		public bool RelationConnection() {
			return m_DB.Connection();
		}

		/// <summary>
		/// 实时库是否连接正常
		/// </summary>
		/// <returns></returns>
		public bool RealTimeConnection() {
			return m_RTDB.Connection();
		}

		/// <summary>
		/// 是否满足系统滤波计算条件
		/// </summary>
		/// <returns></returns>
		public bool CanRun() {
			if (!RelationConnection()) {
				LogUtil.LogMessage("关系库连接异常，终止指标考核计算");
				return false;
			}
			if (!RealTimeConnection()) {
				LogUtil.LogMessage("实时库连接异常，终止指标考核计算");
				return false;
			}
			return true;
		}

		public bool SnapshotCalc() {
			bool bGood = false;
			bool bIDL = false;
			//DataTable dtUnit = KPI_UnitDal.GetUnitIDs("");
			//if (dtUnit == null || dtUnit.Rows.Count <= 0) {
			//    return true;
			//}
			//else {
			List<KPI_UnitEntity> UnitList;
			using (KPI_UnitDal DataAccess = new KPI_UnitDal()) {
				UnitList = DataAccess.GetUnitIDs("");
			}

			foreach (KPI_UnitEntity Unit in UnitList) {
				//string UnitID = Unit.UnitID;
				//string UnitName = KPI_UnitDal.GetUnitName(UnitID);
				if (string.IsNullOrEmpty(Unit.UnitID)) {
					continue;
				}
				//DataTable tags = Race_TagDal.GetTags(Unit.UnitID);
				List<Race_TagEntity> RaceTagList;
				using (Race_TagDal DataAccess = new Race_TagDal()) {
					RaceTagList = DataAccess.GetTagList(Unit.UnitID);
				}
				//if (tags == null || tags.Rows.Count <= 0) {
				//    return true;
				//}
				if (RaceTagList.Count <= 0) return true;
				string WorkID = Unit.WorkID;
				string CurrentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
				string ShiftName = "";
				string PeriodName = "";
				string StartTime = "";
				string EndTime = "";
				bGood = KPI_WorkDal.GetShiftAndPeriod(WorkID, CurrentTime, ref ShiftName, ref PeriodName, ref StartTime, ref EndTime);
				if (!bGood) {
					return false;
				}
				DateTime dts = DateTime.Parse(StartTime);
				DateTime dte = DateTime.Parse(EndTime);
				//从当前值的开始时间到当前时间的统计
				DateTime dtc = DateTime.Parse(CurrentTime);
				//////////////////////////////////////////////////////////////////////////////////
				//是否需要补算上一班的数据
				//并将数据写入到Archive中
				TimeSpan tspan = dtc - dts;
				if (tspan.TotalMinutes < 10) {
					//刚好在交班时间内，需要将历史统计一下
					bIDL = true;
				}
				if (bIDL) {
					string HStartTime = dts.AddMinutes(-2 * tspan.TotalMinutes).ToString("yyyy-MM-dd HH:mm:ss");
					string HEndTime = dtc.ToString("yyyy-MM-dd HH:mm:ss");

					LastShiftCalc(Unit.UnitID, HStartTime, HEndTime);
				}
				////////////////////////////////////////////////////////////////////////
				//判断时间是否太小
				TimeSpan tscs = dtc - dts;
				if (tscs.TotalMinutes < 5) {
					//间隔太小，不计算
					continue;
				}
				////////////////////////////////////////////////////////////////////////
				//string PeriodID = KPI_PeriodDal.GetPeriodID(PeriodName);
				//string ShiftID = KPI_ShiftDal.GetShiftID(ShiftName);

				RTInterface RTDB = DBAccess.GetRealTime();
				//实时计算
				foreach (Race_TagEntity RaceTag  in RaceTagList) {
					//string TagID = tags.Rows[i]["TagID"].ToString();
					decimal TagValue = decimal.MinValue;
					//Race_TagEntity tentity = Race_TagDal.GetEntity(TagID);
					if (RaceTag.TagCalcExpType == 0) {
						TagValue = Convert.ToDecimal(RTDB.TagCalculatedData(RaceTag.TagCalcExp, dts, dtc, RaceTag.TagFilterExp, GetRaceTagCalcType(RaceTag.TagCalcType)));
						//Random rdm = new Random();
						//TagValue = rdm.NextDouble() * 100;
						if (TagValue == decimal.MinValue) {
							LogUtil.LogMessage(RaceTag.TagDesc + "数据出现错误!");
							continue;
						}
						TagValue = Convert.ToDecimal(TagValue) * RaceTag.TagFactor + RaceTag.TagOffset;
					}
					else if (RaceTag.TagCalcExpType == 1) {
						TagValue = Convert.ToDecimal(RTDB.ExpCalculatedData(RaceTag.TagCalcExp, dts, dtc, RaceTag.TagFilterExp, GetRaceTagCalcType(RaceTag.TagCalcType)));
						//Random rdm = new Random();
						//TagValue = rdm.NextDouble() * 100;

						if (TagValue == decimal.MinValue) {
							LogUtil.LogMessage(RaceTag.TagDesc + "数据出现错误!");
							continue;
						}
						TagValue = TagValue * RaceTag.TagFactor + RaceTag.TagOffset;
					}
					else if (RaceTag.TagCalcExpType == 2) {
						Random rdm = new Random();
						TagValue = Convert.ToDecimal(rdm.NextDouble() * 100);
						if (TagValue == decimal.MinValue) {
							LogUtil.LogMessage(RaceTag.TagDesc + "数据出现错误!");
							continue;
						}
						TagValue = TagValue * RaceTag.TagFactor + RaceTag.TagOffset;
					}

					//update and insert
					if (GetSnapshotExits(RaceTag.TagID, ShiftName)) {
						//update
						//Race_SnapshotEntity sse = new Race_SnapshotEntity();

						////sse.TagID = TagID;
						////sse.UnitID = UnitID;
						////sse.TagType = tentity.TagType;

						//sse.TagShift = ShiftID;
						//sse.TagPeriod = PeriodID;
						//sse.TagStartTime = StartTime;
						//sse.TagEndTime = EndTime;
						//sse.TagValue = TagValue;
						string sql = @"update Race_Snapshot 
                                        set TagPeriod='{0}', TagStartTime='{1}', 
                                                TagEndTime='{2}', TagValue={3} 
                                    where TagID='{4}' and TagShift='{5}'";

						sql = string.Format(sql, PeriodName, StartTime, EndTime, TagValue.ToString(), RaceTag.TagID, ShiftName);
						DBAccess.GetRelation().ExecuteNonQuery(sql);
					}
					else {
						//insert
						Race_SnapshotEntity sse = new Race_SnapshotEntity();
						sse.TagID = RaceTag.TagID;
						sse.UnitID = Unit.UnitID;
						sse.TagType = RaceTag.TagType;
						sse.TagShift = ShiftName;
						sse.TagPeriod = PeriodName;
						sse.TagStartTime = StartTime;
						sse.TagEndTime = EndTime;
						sse.TagValue = Convert.ToDouble(TagValue);
						Race_SnapshotDal.Insert(sse);
					}


					//update and insert
					if (GetArchiveExits(RaceTag.TagID, ShiftName, StartTime)) {
						//update
						//Race_ArchiveEntity sse = new Race_ArchiveEntity();

						////sse.TagID = TagID;
						////sse.UnitID = UnitID;
						////sse.TagType = tentity.TagType;

						//sse.TagShift = ShiftID;
						//sse.TagPeriod = PeriodID;
						//sse.TagStartTime = StartTime;
						//sse.TagEndTime = EndTime;
						//sse.TagValue = TagValue;
						string sql = @"update Race_Archive 
                                        set TagPeriod='{0}', TagEndTime='{1}', TagValue={2} 
                                    where TagID='{3}' and TagShift='{4}' and TagStartTime='{5}'";
						sql = string.Format(sql, PeriodName, EndTime, TagValue.ToString(), RaceTag.TagID, ShiftName, StartTime);
						DBAccess.GetRelation().ExecuteNonQuery(sql);

					}
					else {
						//insert
						Race_ArchiveEntity sse = new Race_ArchiveEntity();

						sse.TagID = RaceTag.TagID;
						sse.UnitID = Unit.UnitID;
						sse.TagType = RaceTag.TagType;
						sse.TagShift = ShiftName;
						sse.TagPeriod = PeriodName;
						sse.TagStartTime = StartTime;
						sse.TagEndTime = EndTime;
						sse.TagValue = Convert.ToDouble(TagValue);
						Race_ArchiveDal.Insert(sse);
					}
				}
			}
			return true;
		}


		/// <summary>
		/// Race_Archive的操作
		/// </summary>
		/// <returns></returns>
		public static bool LastShiftCalc(string UnitID, string HStartTime, string HEndTime) {
			bool bGood = false;

			if (UnitID == "") {
				return false;
			}

			string UnitName = KPI_UnitDal.GetUnitName(UnitID);
			//DataTable tags = Race_TagDal.GetTags(UnitID);
			List<Race_TagEntity> RaceTagList;
			using (Race_TagDal DataAccess = new Race_TagDal()) {
				RaceTagList = DataAccess.GetTagList(UnitID);
			}
			if (RaceTagList.Count <= 0) return true;
			//if (tags == null || tags.Rows.Count <= 0) {
			//    return true;
			//}

			//delete
			string sql = @"delete Race_Archive where UnitID='{0}'  and  (TagStartTime>'{1}' and TagStartTime<'{2}') ";
			sql = string.Format(sql, UnitID, HStartTime, HEndTime);
			DBAccess.GetRelation().ExecuteNonQuery(sql);
			//insert
			string WorkID = KPI_UnitDal.GetWorkIDByID(UnitID);
			string ShiftName = "";
			string PeriodName = "";
			string StartTime = "";
			string EndTime = "";

			bGood = KPI_WorkDal.GetShiftAndPeriod(WorkID, HStartTime, ref ShiftName, ref PeriodName, ref StartTime, ref EndTime);
			if (!bGood) {
				return false;
			}
			DateTime dts = DateTime.Parse(StartTime);
			DateTime dte = DateTime.Parse(EndTime);
			//string PeriodID = KPI_PeriodDal.GetPeriodID(PeriodName);
			//string ShiftID = KPI_ShiftDal.GetShiftID(ShiftName);

			//历史计算
			foreach (Race_TagEntity RaceTag in RaceTagList) {
				//string TagID = tags.Rows[i]["TagID"].ToString();
				decimal TagValue = decimal.MinValue;
				RTInterface RTDB = DBAccess.GetRealTime();
				//Race_TagEntity tentity = Race_TagDal.GetEntity(RaceTag.TagID);
				if (RaceTag.TagCalcExpType == 0) {
					TagValue = Convert.ToDecimal(RTDB.TagCalculatedData(RaceTag.TagCalcExp, dts, dte, RaceTag.TagFilterExp, GetRaceTagCalcType(RaceTag.TagCalcType)));
					//Random rdm = new Random();
					//TagValue = rdm.NextDouble() * 100;

					if (TagValue == decimal.MinValue) {
						LogUtil.LogMessage(RaceTag.TagDesc + "数据出现错误!");
						continue;
					}
					TagValue = TagValue * RaceTag.TagFactor + RaceTag.TagOffset;

				}
				else if (RaceTag.TagCalcExpType == 1) {
					TagValue = Convert.ToDecimal(RTDB.ExpCalculatedData(RaceTag.TagCalcExp, dts, dte, RaceTag.TagFilterExp, GetRaceTagCalcType(RaceTag.TagCalcType)));
					//Random rdm = new Random();
					//TagValue = rdm.NextDouble() * 100;
					if (TagValue == decimal.MinValue) {
						LogUtil.LogMessage(RaceTag.TagDesc + "数据出现错误!");
						continue;
					}
					TagValue = TagValue * RaceTag.TagFactor + RaceTag.TagOffset;
				}
				else if (RaceTag.TagCalcExpType == 2) {
					Random rdm = new Random();
					TagValue = Convert.ToDecimal(rdm.NextDouble() * 100);
					if (TagValue == decimal.MinValue) {
						LogUtil.LogMessage(RaceTag.TagDesc + "数据出现错误!");
						continue;
					}
					TagValue = TagValue * RaceTag.TagFactor + RaceTag.TagOffset;
				}

				//update Snapshot
				if (GetSnapshotExits(RaceTag.TagID, ShiftName)) {
					//update
					//Race_SnapshotEntity sse = new Race_SnapshotEntity();

					////sse.TagID = TagID;
					////sse.UnitID = UnitID;
					////sse.TagType = tentity.TagType;

					//sse.TagShift = ShiftID;
					//sse.TagPeriod = PeriodID;
					//sse.TagStartTime = StartTime;
					//sse.TagEndTime = EndTime;
					//sse.TagValue = TagValue;

					sql = @"update Race_Snapshot
                                    set TagPeriod='{0}', TagStartTime='{1}', 
                                            TagEndTime='{2}', TagValue={3} 
                                where TagID='{4}' and TagShift='{5}'";
					sql = string.Format(sql, PeriodName, StartTime, EndTime, TagValue.ToString(), RaceTag.TagID, ShiftName);
					DBAccess.GetRelation().ExecuteNonQuery(sql);

				}
				else {
					//insert
					Race_SnapshotEntity sse = new Race_SnapshotEntity();
					sse.TagID = RaceTag.TagID;
					sse.UnitID = UnitID;
					sse.TagType = RaceTag.TagType;
					sse.TagShift = ShiftName;
					sse.TagPeriod = PeriodName;
					sse.TagStartTime = StartTime;
					sse.TagEndTime = EndTime;
					sse.TagValue = Convert.ToDouble(TagValue);
					Race_SnapshotDal.Insert(sse);

				}

				//insert Archive                   
				//insert
				Race_ArchiveEntity sae = new Race_ArchiveEntity();
				sae.TagID = RaceTag.TagID;
				sae.UnitID = UnitID;
				//  sae.TagType = tentity.TagType;
				sae.TagShift = ShiftName;
				sae.TagPeriod = PeriodName;
				sae.TagStartTime = StartTime;
				sae.TagEndTime = EndTime;
				sae.TagValue = Convert.ToDouble(TagValue);
				Race_ArchiveDal.Insert(sae);
			}

			return true;
		}

		/// <summary>
		/// Race_Archive的操作
		/// </summary>
		/// <returns></returns>
		public static bool HistoryCalc(DateTime dtSTime, DateTime dtETime) {
			bool bGood = false;
			//bool bIDL = false;

			//DataTable dtUnit = KPI_UnitDal.GetUnitIDs("");
			List<KPI_UnitEntity> UnitList;
			using (KPI_UnitDal DataAccess = new KPI_UnitDal()) {
				UnitList = DataAccess.GetUnitIDs("");
			}

			//if (dtUnit == null || dtUnit.Rows.Count <= 0) {
			//    return true;
			//}
			//else {
			//    for (int k = 0; k < dtUnit.Rows.Count; k++) {
			foreach (KPI_UnitEntity Unit in UnitList) {
				//string UnitID = dtUnit.Rows[k]["UnitID"].ToString();
				//string UnitName = KPI_UnitDal.GetUnitName(UnitID);

				string HStartTime = dtSTime.ToString("yyyy-MM-dd HH:mm:ss");
				string HEndTime = dtETime.ToString("yyyy-MM-dd HH:mm:ss");
				if (string.IsNullOrEmpty(Unit.UnitID)) {
					continue;
				}

				//DataTable tags = Race_TagDal.GetTags(Unit.UnitID);
				//if (tags == null || tags.Rows.Count <= 0) {
				//    return true;
				//}
				List<Race_TagEntity> RaceTagList;
				using (Race_TagDal DataAccess = new Race_TagDal()) {
					RaceTagList = DataAccess.GetTagList(Unit.UnitID);
				}
				if (RaceTagList.Count <= 0) return true;

				//delete
				string sql = @"delete Race_Archive where UnitID='{0}'  and  (TagStartTime>'{1}' and TagStartTime<'{2}') ";

				sql = string.Format(sql, Unit.UnitID, HStartTime, HEndTime);

				DBAccess.GetRelation().ExecuteNonQuery(sql);

				//insert
				string WorkID = KPI_UnitDal.GetWorkIDByID(Unit.UnitID);

				bool bCalc = true;
				while (bCalc) {
					string ShiftName = "";
					string PeriodName = "";
					string StartTime = "";
					string EndTime = "";

					bGood = KPI_WorkDal.GetShiftAndPeriod(WorkID, HStartTime, ref ShiftName, ref PeriodName, ref StartTime, ref EndTime);

					if (!bGood) {
						return false;
					}

					if (DateTime.Parse(EndTime) > DateTime.Parse(HEndTime)) {
						//While 循环结束
						bCalc = false;

						continue;
					}

					DateTime dts = DateTime.Parse(StartTime);
					DateTime dte = DateTime.Parse(EndTime);

					//string PeriodID = KPI_PeriodDal.GetPeriodID(PeriodName);
					//string ShiftID = KPI_ShiftDal.GetShiftID(ShiftName);

					//历史计算
					foreach (Race_TagEntity RaceTag in RaceTagList) {
						//string TagID = tags.Rows[i]["TagID"].ToString();
						decimal TagValue = decimal.MinValue;
						RTInterface RTDB = DBAccess.GetRealTime();
						//Race_TagEntity tentity = Race_TagDal.GetEntity(RaceTag.TagID);

						if (RaceTag.TagCalcExpType == 0) {
							TagValue = Convert.ToDecimal(RTDB.TagCalculatedData(RaceTag.TagCalcExp, dts, dte, RaceTag.TagFilterExp, GetRaceTagCalcType(RaceTag.TagCalcType)));
							//Random rdm = new Random();
							//TagValue = rdm.NextDouble() * 100;

							if (TagValue == decimal.MinValue) {
								LogUtil.LogMessage(RaceTag.TagDesc + "数据出现错误!");

								continue;
							}

							TagValue = TagValue * RaceTag.TagFactor + RaceTag.TagOffset;

						}
						else if (RaceTag.TagCalcExpType == 1) {
							TagValue = Convert.ToDecimal(RTDB.ExpCalculatedData(RaceTag.TagCalcExp, dts, dte, RaceTag.TagFilterExp, GetRaceTagCalcType(RaceTag.TagCalcType)));
							//Random rdm = new Random();
							//TagValue = rdm.NextDouble() * 100;

							if (TagValue == decimal.MinValue) {
								LogUtil.LogMessage(RaceTag.TagDesc + "数据出现错误!");

								continue;
							}

							TagValue = TagValue * RaceTag.TagFactor + RaceTag.TagOffset;
						}
						else if (RaceTag.TagCalcExpType == 2) {
							Random rdm = new Random();
							TagValue = Convert.ToDecimal(rdm.NextDouble() * 100);
							if (TagValue == decimal.MinValue) {
								LogUtil.LogMessage(RaceTag.TagDesc + "数据出现错误!");
								continue;
							}
							TagValue = TagValue * RaceTag.TagFactor + RaceTag.TagOffset;
						}
						//insert Archive                   
						//insert
						Race_ArchiveEntity sae = new Race_ArchiveEntity();
						sae.TagID = RaceTag.TagID;
						sae.UnitID = Unit.UnitID;
						sae.TagType = RaceTag.TagType;
						sae.TagShift = ShiftName;
						sae.TagPeriod = PeriodName;
						sae.TagStartTime = StartTime;
						sae.TagEndTime = EndTime;
						sae.TagValue = Convert.ToDouble(TagValue);
						Race_ArchiveDal.Insert(sae);
					}
					HStartTime = EndTime;
				}
			}
			return true;
		}


		/// <summary>
		/// Race_Snapshot And Race_Archive的操作
		/// </summary>
		/// <returns></returns>
		public bool UnitAnalysis(bool bsnap) {
			//当前时间
			DateTime dtTime = DateTime.Now;

			if (!bsnap) {
				//判断是否历史计算
				string strHistory = System.Configuration.ConfigurationManager.AppSettings["RHistory"];
				string[] strTime = strHistory.Split(new string[] { ";", "," }, StringSplitOptions.RemoveEmptyEntries);

				if (strTime.Length == 2) {
					//历史计算
					LogUtil.LogMessage("值际竞赛统计历史计算，开始时间：" + strTime[0] + "，结束时间：" + strTime[1]);

					DateTime dtStartTime = DateTime.Parse(strTime[0]);
					DateTime dtEndTime = DateTime.Parse(strTime[1]);

					if (dtStartTime > dtEndTime || dtStartTime >= dtTime) {
						LogUtil.LogMessage("值际竞赛统计历史计算，时间配置不正确！");

						return false;
					}
					else {
						//历史计算
						HistoryCalc(dtStartTime, dtEndTime);
					}
				}
			}
			else {
				//实时计算
				SnapshotCalc();
			}

			return true;
		}

		public static bool GetArchiveExits(string TagID, string ShiftID, string StartTime) {
			string sql = @"select count(TagID) 
                            from Race_Archive
                            where TagID='{0}' and TagShift='{1}' and TagStartTime='{2}' ";

			sql = string.Format(sql, TagID, ShiftID, StartTime);

			return int.Parse(DBAccess.GetRelation().ExecuteScalar(sql).ToString()) > 0;
		}

		public static bool GetSnapshotExits(string TagID, string ShiftID) {
			string sql = @"select count(TagID) 
                            from Race_Snapshot
                            where TagID='{0}' and TagShift='{1}'";

			sql = string.Format(sql, TagID, ShiftID);

			return int.Parse(DBAccess.GetRelation().ExecuteScalar(sql).ToString()) > 0;
		}

		public static SummaryType GetRaceTagCalcType(int nType) {
			SummaryType st = SummaryType.asAverage;

			switch (nType) {
				case 1:
					st = SummaryType.asMaximum;
					break;
				case 2:
					st = SummaryType.asMinimum;
					break;
				case 3:
					st = SummaryType.asAverage;
					break;

				default:
					st = SummaryType.asAverage;
					break;
			}

			return st;
		}

	}
}
