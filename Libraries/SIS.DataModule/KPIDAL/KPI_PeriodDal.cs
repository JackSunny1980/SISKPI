using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SIS.DBControl;
using SIS.DataAccess;
using SIS.DataEntity;


namespace SIS.DataAccess {

	public class KPI_PeriodDal : DalBase<KPI_PeriodEntity> {


		public static List<KPI_PeriodEntity> GetPeriodEntityList(){
			String sqlText =@"SELECT * FROM KPI_Period WHERE PeriodIsValid=1";
			IDataReader Reader = DBAccess.GetRelation().ExecuteReader(sqlText);
			try{
				return Reader.FillGenericList<KPI_PeriodEntity>();
			}
			finally{
				Reader.Close();
				Reader.Dispose();
			}
		}

		/// <summary>
		/// 删除数据
		/// </summary>
		/// <returns></returns>
		public static bool DeletePeriod(string PeriodID) {
			//删除信息
			string sql = "delete from KPI_Period ";
			if (PeriodID != "") {
				sql += " where PeriodID = '" + PeriodID + "'";
			}

			return DBAccess.GetRelation().ExecuteNonQuery(sql) > 0;
		}


		/// <summary>
		/// 得到所有子表数据
		/// </summary>
		/// <returns></returns>
		public static DataTable GetDataTable() {
			string sql = "select * from KPI_Period";
			DataTable dataTable = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
			return dataTable;
		}

		public static string GetNextCode() {
			//
			//支持最多26个Code
			//
			string[] AllCodes = new string[26] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

			string sql = "select PeriodCode from KPI_Period order by PeriodCode";
			int ncount = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0].Rows.Count;

			if (ncount <= 0) {
				return "A";
			}
			else if (ncount < 26) {
				return AllCodes[ncount];
			}
			else {
				//个位，十位，百位：units, tens, hundreds
				int units = 0;
				int tens = 0;

				if (ncount <= 0) {
					//0
					tens = 0;
					units = 0;
				}
				else if (ncount % 26 > 0) {
					//不存在十位数变化的情况
					tens = (int)Math.Floor(ncount / 26.0);
					units = ncount % 26;
				}
				else if (ncount % 26 == 0) {
					//十位数变化的情况
					tens = (int)Math.Floor(ncount / 26.0) + 1;
					units = 0;
				}

				string newcode = AllCodes[tens] + AllCodes[units];

				return newcode;
			}

		}



		/// <summary>
		/// 获得周期数量
		/// </summary>
		/// <returns></returns>
		public static int PeriodIDCounts() {
			string sql = "select PeriodID from KPI_Period";

			return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0].Rows.Count;
		}



		/// <summary>
		/// 判断名称的唯一性
		/// </summary>
		/// <param name="PeriodName"></param>
		/// <param name="PeriodID"></param>
		/// <returns></returns>
		public static bool PeriodNameExists(string PeriodName, string PeriodID) {
			string sql = "select count(1) from KPI_Period where 1=1 and PeriodName='{0}' ";
			sql = string.Format(sql, PeriodName);

			if (PeriodID != "")
				sql = sql + " and PeriodID <> '" + PeriodID + "'";

			return int.Parse(DBAccess.GetRelation().ExecuteScalar(sql).ToString()) > 0;
		}

		/// <summary>
		/// 得到有效的定义
		/// </summary>
		/// <param name="PeriodID">主键</param>
		/// <returns></returns>
		public static DataTable GetPeriods() {
			string sql = "select PeriodID[ID], PeriodName[Name]  from KPI_Period where PeriodIsValid=1";

			return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
		}

		/// <summary>
		/// 得到有效的定义
		/// </summary>
		/// <param name="PeriodID">主键</param>
		/// <returns></returns>
		public static string GetPeriodID(string PeriodName) {
			string sql = "select PeriodID  from KPI_Period where PeriodName='" + PeriodName + "'";

			DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

			if (dt == null || dt.Rows.Count <= 0) {
				return "";
			}
			else {
				return dt.Rows[0]["PeriodID"].ToString();
			}
		}

		/// <summary>
		/// 得到
		/// </summary>
		/// <param name="PeriodID">主键</param>
		/// <returns></returns>
		public static string GetPeriodName(string PeriodID) {
			string sql = "select PeriodName from KPI_Period where PeriodID='" + PeriodID + "'";

			DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

			if (dt == null || dt.Rows.Count <= 0) {
				return "";
			}
			else {
				return dt.Rows[0]["PeriodName"].ToString();
			}
		}

		/// <summary>
		/// 通过主键得到实体对象
		/// </summary>
		/// <param name="PeriodID">主键</param>
		/// <returns></returns>
		public static KPI_PeriodEntity GetEntity(string PeriodCode) {
			KPI_PeriodEntity entity = new KPI_PeriodEntity();
			string sql = "select * from KPI_Period where PeriodCode='{0}'";
			sql = string.Format(sql, PeriodCode);
			DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
			if (dt.Rows.Count > 0) {
				entity.DrToMember(dt.Rows[0]);
			}
			return entity;
		}


		/// <summary>
		/// 得到查询结果
		/// </summary>
		/// <param name="condition">条件字符串</param>
		/// <returns></returns>
		public static DataTable GetSearchList(string condition) {
			string sql = @"select PeriodID, PeriodName, PeriodDesc
                            from KPI_Period  
                            where 1=1 {0}";

			sql = string.Format(sql, condition);

			DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

			return dt;
		}

		/// <summary>
		/// 得到所有子表数据
		/// </summary>
		/// <returns></returns>
		public static DataTable GetPeriodList() {
			string sql = @"select PeriodID, PeriodCode, PeriodName, PeriodDesc, PeriodStartHour, PeriodEndHour, PeriodHours, PeriodIsIDL,  PeriodIsValid, PeriodNote
                            from KPI_Period  order by PeriodCode ";

			return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
		}

	}
}
