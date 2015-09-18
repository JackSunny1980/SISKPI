using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SIS.DBControl;
using SIS.DataAccess;
using SIS.DataEntity;


namespace SIS.DataAccess {
    public class CycleDal : DalBase<CycleEntity> {
		/// <summary>
		/// 删除数据
		/// </summary>
		/// <returns></returns>
		public static bool DeleteCycle(string CycleID) {
			//删除参数信息
			string sql = "delete from KPI_Cycle ";
			if (CycleID != "") {
				sql += " where CycleID = '" + CycleID + "'";
			}

			return DBAccess.GetRelation().ExecuteNonQuery(sql) > 0;
		}

		/// <summary>
		/// 判断名称的唯一性
		/// </summary>
		/// <param name="SeqName"></param>
		/// <param name="SeqID"></param>
		/// <returns></returns>
		public static int CycleIDCounts() {
			string sql = "select CycleID from KPI_Cycle";

			return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0].Rows.Count;
		}

		/// <summary>
		/// 得到所有子表数据
		/// </summary>
		/// <returns></returns>
		public static DataTable GetDataTable() {
			string sql = "select * from KPI_Cycle";
			DataTable dataTable = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
			return dataTable;
		}

		/// <summary>
		/// 判断名称的唯一性
		/// </summary>
		/// <param name="CycleName"></param>
		/// <param name="CycleID"></param>
		/// <returns></returns>
		public static bool CycleNameExists(string CycleName, string CycleID) {
			string sql = "select count(1) from KPI_Cycle where CycleName='{0}' ";
			sql = string.Format(sql, CycleName);

			if (CycleID != "") {
				sql = sql + " and CycleID <> '" + CycleID + "'";
			}

			return int.Parse(DBAccess.GetRelation().ExecuteScalar(sql).ToString()) > 0;
		}

		/// <summary>
		/// 获得所有电厂ID、Name
		/// </summary>
		/// <param name="GroupID"></param>
		/// <returns></returns>
		public static DataTable GetCycles() {
			string sql = "select CycleID[ID], CycleName[Name] from KPI_Cycle";

			return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
		}

		/// <summary>
		/// 获得所有电厂ID、Name
		/// </summary>
		/// <param name="GroupID"></param>
		/// <returns></returns>
		public static string GetCycleID(string CycleName) {
			string sql = "select CycleID[ID] from KPI_Cycle where CycleName='" + CycleName + "'";

			DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

			if (dt.Rows.Count != 1) {
				return "";
			}
			else {
				return dt.Rows[0]["ID"].ToString();
			}
		}


		/// <summary>
		/// 获得所有电厂ID、Name
		/// </summary>
		/// <param name="GroupID"></param>
		/// <returns></returns>
		public static double GetCycleValue(string CycleName) {
			string sql = "select CycleValue from KPI_Cycle where CycleName='" + CycleName + "'";

			DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

			if (dt.Rows.Count != 1) {
				return 0;
			}
			else {
				return double.Parse(dt.Rows[0]["CycleValue"].ToString());
			}
		}


		/// <summary>
		/// 通过主键得到实体对象
		/// </summary>
		/// <param name="CycleID">主键</param>
		/// <returns></returns>
        public static List<CycleEntity> GetAllEntity() {
            List<CycleEntity> Result = null;
			string sqlText = @"select * from KPI_Cycle";
			using (IDataReader reader = DBAccess.GetRelation().ExecuteReader(sqlText)) {
                Result = reader.FillGenericList<CycleEntity>();
				reader.Close();
			}
			return Result;
			//List<KPI_CycleEntity> ltCycles = new List<KPI_CycleEntity>();
			//string sql = "select * from KPI_Cycle ";
			//DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
			//foreach (DataRow dr in dt.Rows) {
			//    KPI_CycleEntity entity = new KPI_CycleEntity();
			//    entity.DrToMember(dr);
			//    ltCycles.Add(entity);
			//}
			//return ltCycles;
		}


		/// <summary>
		/// 得到查询结果
		/// </summary>
		/// <param name="condition">条件字符串</param>
		/// <returns></returns>
		public static DataTable GetSearchList() {
			string sql = @"select *  from KPI_Cycle";

			return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
		}

	}
}
