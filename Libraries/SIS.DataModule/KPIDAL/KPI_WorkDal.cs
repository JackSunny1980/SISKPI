using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SIS.DBControl;
using SIS.DataAccess;
using SIS.DataEntity;


namespace SIS.DataAccess {
	public class KPI_WorkDal : DalBase<KPI_WorkEntity> {

		/// <summary>
		/// 删除数据
		/// </summary>
		/// <returns></returns>
		public static bool DeleteWork(string WorkID) {
			string sql = "delete KPI_Work ";
			if (WorkID != "") {
				sql += " where WorkID = '" + WorkID + "'";
			}

			return DBAccess.GetRelation().ExecuteNonQuery(sql) > 0;
		}


		/// <summary>
		/// 得到所有子表数据
		/// </summary>
		/// <returns></returns>
		public static DataTable GetDataTable() {
			string sql = "select * from KPI_Work";
			DataTable dataTable = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
			return dataTable;
		}

		/// <summary>
		/// 判断名称的唯一性
		/// </summary>
		/// <param name="WorkName"></param>
		/// <param name="WorkID"></param>
		/// <returns></returns>
		public static bool WorkNameExists(string WorkName, string WorkID) {
			string sql = "select count(1) from KPI_Work where WorkName='{0}' ";
			sql = string.Format(sql, WorkName);

			if (WorkID != "")
				sql = sql + " and WorkID <> '" + WorkID + "'";

			return int.Parse(DBAccess.GetRelation().ExecuteScalar(sql).ToString()) > 0;
		}

		/// <summary>
		/// 得到有效的定义
		/// </summary>
		/// <param name="WorkID">主键</param>
		/// <returns></returns>
		public static DataTable GetWorks() {
			string sql = "select WorkID[ID], WorkName[Name]  from KPI_Work where WorkIsValid=1";

			return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
		}


		/// <summary>
		/// 修改参数
		/// </summary>
		/// <param name="WorkID">主键</param>
		/// <param name="value">设定值</param>
		/// <returns></returns>
		public static bool Update(string WorkID, string WorkCode, string WorkDesc, string WorkIsValid, string WorkNote) {
			KPI_WorkEntity mEntity = new KPI_WorkEntity();

			//mEntity.WorkID = WorkID;
			////mEntity.WorkName = strName;
			//mEntity.WorkCode = WorkCode;
			//mEntity.WorkDesc = WorkDesc;
			//mEntity.WorkIsValid = int.Parse(WorkIsValid);
			//mEntity.WorkNote = WorkNote;

			////mEntity.WorkCreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
			//mEntity.WorkModifyTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");

			return KPI_WorkDal.Update(mEntity);
		}


		/// <summary>
		/// 通过主键得到实体对象
		/// </summary>
		/// <param name="WorkID">主键</param>
		/// <returns></returns>
		public static KPI_WorkEntity GetEntity(string WorkID) {
			KPI_WorkEntity Result = null;
			string sqlText = @"select * from KPI_Work where WorkID='{0}'";
			sqlText = string.Format(sqlText, WorkID);
			using (IDataReader reader = DBAccess.GetRelation().ExecuteReader(sqlText)) {
				Result = reader.FillEntity<KPI_WorkEntity>();
				reader.Close();
			}
			return Result;
			//KPI_WorkEntity entity = new KPI_WorkEntity();
			//string sql = "select * from KPI_Work where WorkID='{0}'";
			//sql = string.Format(sql, WorkID);
			//DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
			//if (dt.Rows.Count > 0) {
			//    entity.DrToMember(dt.Rows[0]);
			//}
			//return entity;
		}

		/// <summary>
		/// 通过时间获得当前值、当前班、开始时间、结束时间
		/// </summary>
		/// <param name="WorkID">主键</param>
		/// <returns></returns>
		public static bool GetShiftAndPeriod(string WorkID, string QueryTime,
			ref string ShiftName, ref string PeriodName,
			ref string StartTime, ref string EndTime) {
			ShiftName = "";
			PeriodName = "";
			StartTime = "";
			EndTime = "";

			KPI_WorkEntity entity = KPI_WorkDal.GetEntity(WorkID);

			DateTime dtQueryTime = DateTime.Parse(QueryTime);

			string strStartTime = entity.WorkStartTime;
			int nBaseShifts = entity.WorkBaseShifts;
			int nBaseDays = entity.WorkBaseDays;
			string strSequence = entity.WorkSequence;
			string strShift = entity.WorkShift;

			//判断是否查询时间超出范围
			if (DateTime.Parse(strStartTime) > dtQueryTime) {
				return false;
			}

			//int[] timearray = new int[6] { dtQueryTime.Year, dtQueryTime.Month, dtQueryTime.Day, dtQueryTime.Hour, 0, 0 };
			DateTime dtStartTime = dtQueryTime;
			DateTime dtEndTime = dtQueryTime;


			//获得值班顺序
			//strSequence = strSequence.Remove(strSequence.LastIndexOf('-'));
			string[] AllPeriods = strSequence.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);

			//strShift = strShift.Remove(strShift.LastIndexOf('-'));
			string[] AllShifts = strShift.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);

			TimeSpan tspan = DateTime.Parse(QueryTime) - DateTime.Parse(strStartTime);

			//当天班的序号
			int nDay = tspan.Days % nBaseDays;
			//当天班的顺序
			string[] strCurrentPeriod = new string[nBaseShifts];
			for (int i = 0; i < nBaseShifts; i++) {
				strCurrentPeriod[i] = AllPeriods[nDay * nBaseShifts + i];
			}

			decimal CurrentHour = Convert.ToDecimal(dtQueryTime.Hour + dtQueryTime.Minute / 60.0m);
			//DateTime dt
			bool bPeriod = false;
			bool bLastDay = false;

			int id = 0;
			for (id = 0; id < nBaseShifts; id++) {
				bPeriod = false;
				bLastDay = false;

				string strP = strCurrentPeriod[id];
				KPI_PeriodEntity pe = KPI_PeriodDal.GetEntity(strP);

				int nIDL = int.Parse(pe.PeriodIsIDL);

				if (pe.PeriodStartHour == -1) {
					continue;
				}

				if (nIDL == 0 && (CurrentHour >= pe.PeriodStartHour && CurrentHour < pe.PeriodEndHour)) {
					bPeriod = true;

					//班名称，开始时间，结束时间
					PeriodName = pe.PeriodName;

					//班开始结束时间的小时、分钟转换，考虑有半点交班的情况。
					int sh = (int)Math.Floor(pe.PeriodStartHour);
					int sm = (int)((pe.PeriodStartHour - sh) * 60);
					int eh = (int)Math.Floor(pe.PeriodEndHour);
					int em = (int)((pe.PeriodEndHour - eh) * 60);

					dtStartTime = new DateTime(dtQueryTime.Year, dtQueryTime.Month, dtQueryTime.Day, sh, sm, 0);
					dtEndTime = new DateTime(dtQueryTime.Year, dtQueryTime.Month, dtQueryTime.Day, eh, em, 0);

				}

				if (nIDL == 1 && (CurrentHour >= pe.PeriodStartHour && CurrentHour < 24)) {
					bPeriod = true;

					//班名称，开始时间，结束时间
					PeriodName = pe.PeriodName;


					//班开始结束时间的小时、分钟转换，考虑有半点交班的情况。
					int sh = (int)Math.Floor(pe.PeriodStartHour);
					int sm = (int)((pe.PeriodStartHour - sh) * 60);
					int eh = (int)Math.Floor(pe.PeriodEndHour);
					int em = (int)((pe.PeriodEndHour - eh) * 60);

					dtStartTime = new DateTime(dtQueryTime.Year, dtQueryTime.Month, dtQueryTime.Day, sh, sm, 0);
					DateTime dtNextTime = dtQueryTime.AddDays(1);
					dtEndTime = new DateTime(dtNextTime.Year, dtNextTime.Month, dtNextTime.Day, eh, em, 0);

				}

				if (nIDL == 1 && (CurrentHour >= 0 && CurrentHour < pe.PeriodEndHour)) {
					bPeriod = true;
					bLastDay = true;

					//班名称，开始时间，结束时间
					PeriodName = pe.PeriodName;


					//班开始结束时间的小时、分钟转换，考虑有半点交班的情况。
					int sh = (int)Math.Floor(pe.PeriodStartHour);
					int sm = (int)((pe.PeriodStartHour - sh) * 60);
					int eh = (int)Math.Floor(pe.PeriodEndHour);
					int em = (int)((pe.PeriodEndHour - eh) * 60);

					DateTime dtLastTime = dtQueryTime.AddDays(-1);
					dtStartTime = new DateTime(dtLastTime.Year, dtLastTime.Month, dtLastTime.Day, sh, sm, 0);
					dtEndTime = new DateTime(dtQueryTime.Year, dtQueryTime.Month, dtQueryTime.Day, eh, em, 0);

				}

				//已经获得开始时间和结束时间。
				if (bPeriod) {
					break;
				}

			}

			if (bPeriod) {
				if (bLastDay) {
					//前一天的序号
					int nLastDay = (nDay + (nBaseDays - 1)) % nBaseDays;
					//前一天班的顺序
					string[] strLastPeriod = new string[nBaseShifts];
					for (int i = 0; i < nBaseShifts; i++) {
						strLastPeriod[i] = AllPeriods[nLastDay * nBaseShifts + i];
					}

					for (int i = 0; i < nBaseShifts; i++) {
						if (strCurrentPeriod[id] == strLastPeriod[i]) {
							ShiftName = KPI_ShiftDal.GetShiftName(AllShifts[i]);

							break;
						}
					}


				}
				else {
					ShiftName = KPI_ShiftDal.GetShiftName(AllShifts[id]);
				}

				StartTime = dtStartTime.ToString("yyy-MM-dd HH:mm:ss");
				EndTime = dtEndTime.ToString("yyy-MM-dd HH:mm:ss");
			}
			else {
				return false;
			}


			return true;
		}

		/// <summary>
		/// 通过时间获得当前值、当前班、开始时间、结束时间
		/// </summary>
		/// <param name="WorkID">主键</param>
		/// <returns></returns>
		public static string GetCurrentShift(string WorkID, string QueryTime) {
			//string QueryTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			string ShiftName = "";
			string PeriodName = "";
			string StartTime = "";
			string EndTime = "";

			bool bGood = GetShiftAndPeriod(WorkID, QueryTime, ref ShiftName, ref PeriodName, ref StartTime, ref EndTime);

			if (!bGood) {
				return "";
			}

			return ShiftName;

		}

		/// <summary>
		/// 通过时间变更小时数
		/// </summary>
		/// <param name="WorkID">主键</param>
		/// <returns></returns>
		public static double GetIDLHour(string WorkID) {
			double IDLHour = 1;

			KPI_WorkEntity entity = KPI_WorkDal.GetEntity(WorkID);

			int nBaseShifts = entity.WorkBaseShifts;
			string strSequence = entity.WorkSequence;

			//strSequence = strSequence.Remove(strSequence.LastIndexOf('-'));
			string[] AllPeriods = strSequence.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);

			//当天班的顺序
			//string[] strCurrentPeriod = new string[nBaseShifts];
			for (int i = 0; i < nBaseShifts; i++) {
				string strC = AllPeriods[nBaseShifts + i];

				KPI_PeriodEntity pd = KPI_PeriodDal.GetEntity(strC);

				int nIDL = int.Parse(pd.PeriodIsIDL);
				if (nIDL == 1) {
					IDLHour = Convert.ToDouble(pd.PeriodEndHour);
					break;
				}

			}

			return IDLHour;

		}



		/// <summary>
		/// 得到查询结果
		/// </summary>
		/// <param name="condition">条件字符串</param>
		/// <returns></returns>
		public static int GetBaseShifts(string WorkID) {
			string sql = @"select WorkBaseShifts from KPI_Work  
                            where 1=1 {0}";

			if (WorkID != "") {
				sql = string.Format(sql, " and WorkID='" + WorkID + "'");
			}

			DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

			int nShift = 0;

			if (dt != null && dt.Rows.Count > 0) {
				nShift = int.Parse(dt.Rows[0]["WorkBaseShifts"].ToString());
			}

			return nShift;
		}

		/// <summary>
		/// 得到查询结果
		/// </summary>
		/// <param name="condition">条件字符串</param>
		/// <returns></returns>
		public static string[] GetShift(string WorkID) {
			string sql = @"select WorkShift from KPI_Work  
                            where 1=1 {0}";

			if (WorkID != "") {
				sql = string.Format(sql, " and WorkID='" + WorkID + "'");
			}

			DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

			string strShift = "";

			if (dt != null && dt.Rows.Count > 0) {
				strShift = dt.Rows[0]["WorkShift"].ToString();
			}

			// strShift = strShift.Remove(strShift.LastIndexOf('-'));

			string[] AllShifts = strShift.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);

			return AllShifts;
		}


		/// <summary>
		/// 得到查询结果
		/// </summary>
		/// <param name="condition">条件字符串</param>
		/// <returns></returns>
		public static DataTable GetSearchList(string condition) {
			string sql = @"select * from KPI_Work  
                            where 1=1 {0}";

			sql = string.Format(sql, condition);

			DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

			return dt;
		}

		/// <summary>
		/// 得到查询结果
		/// </summary>
		/// <param name="condition">条件字符串</param>
		/// <returns></returns>
		public static DataTable GetWorkList() {
			string sql = "select * from KPI_Work ";

			return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
		}

	}
}
