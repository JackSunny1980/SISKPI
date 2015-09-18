using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SIS.DBControl;
using SIS.DataAccess;
using SIS.DataEntity;


namespace SIS.DataAccess {
    public class KpiDal : DalBase<KpiEntity> {
		/// <summary>
		/// 删除数据
		/// </summary>
		/// <returns></returns>
		public static bool DeleteKpi(string KpiID) {
			//删除参数信息
			string sql = "delete from KPI_Kpi ";
			if (KpiID != "") {
				sql += " where KpiID = '" + KpiID + "'";
			}

			return DBAccess.GetRelation().ExecuteNonQuery(sql) > 0;
		}

		/// <summary>
		/// 判断名称的唯一性
		/// </summary>
		/// <param name="SeqName"></param>
		/// <param name="SeqID"></param>
		/// <returns></returns>
		public static int KpiIDCounts() {
			string sql = "select KpiID from KPI_Kpi";

			return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0].Rows.Count;
		}

		/// <summary>
		/// 得到所有子表数据
		/// </summary>
		/// <returns></returns>
		public static DataTable GetDataTable() {
			string sql = "select * from KPI_Kpi";
			DataTable dataTable = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
			return dataTable;
		}

		/// <summary>
		/// 判断名称的唯一性
		/// </summary>
		/// <param name="SeqName"></param>
		/// <param name="SeqID"></param>
		/// <returns></returns>
		public static bool KpiCodeExists(string KpiCode, string KpiID) {
			string sql = "select count(1) from KPI_Kpi where 1=1 and KpiCode='{0}' ";
			sql = string.Format(sql, KpiCode);

			if (KpiID != "") {
				sql = sql + " and KpiID <> '" + KpiID + "'";
			}

			return int.Parse(DBAccess.GetRelation().ExecuteScalar(sql).ToString()) > 0;
		}

		/// <summary>
		/// 判断名称的唯一性
		/// </summary>
		/// <param name="KpiName"></param>
		/// <param name="KpiID"></param>
		/// <returns></returns>
		public static bool KpiNameExists(string KpiName, string KpiID) {
			string sql = "select count(1) from KPI_Kpi where 1=1 and KpiName='{0}' ";
			sql = string.Format(sql, KpiName);

			if (KpiID != "") {
				sql = sql + " and KpiID <> '" + KpiID + "'";
			}

			return int.Parse(DBAccess.GetRelation().ExecuteScalar(sql).ToString()) > 0;
		}

		/// <summary>
		/// 获得所有电厂ID、Name
		/// </summary>
		/// <param name="GroupID"></param>
		/// <returns></returns>
		public static DataTable GetKpis() {
			string sql = "select KpiID[ID], KpiName[Name] from KPI_Kpi where KpiIsValid=1 ";

			return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
		}

		/// <summary>
		/// 获得所有电厂ID、Name
		/// </summary>
		/// <param name="GroupID"></param>
		/// <returns></returns>
		public static string GetKpiID(string KpiName) {
			string sql = "select KpiID[ID] from KPI_Kpi where KpiIsValid=1 and KpiName='" + KpiName + "'";

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
		public static string GetKpiCode(string KpiID) {
			string sql = "select KpiCode from KPI_Kpi where KpiID='" + KpiID + "'";

			DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

			if (dt.Rows.Count != 1) {
				return "";
			}
			else {
				return dt.Rows[0]["KpiCode"].ToString();
			}


		}
		/// <summary>
		/// 通过主键得到实体对象
		/// </summary>
		/// <param name="KpiID">主键</param>
		/// <returns></returns>
        public static KpiEntity GetEntity(string KpiID) {
            KpiEntity entity = new KpiEntity();

			string sql = "select * from KPI_Kpi where KpiID='{0}'";
			sql = string.Format(sql, KpiID);

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
			string sql = @"select KpiID, KpiCode, KpiName, KpiIndex, KpiDesc, KpiIsValid, KpiNote 
                            from KPI_Kpi a 
                            where 1=1 {0}";

			sql = string.Format(sql, condition);

			return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
		}


		/////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// 获得所有实体对象
		/// </summary>
		/// <returns></returns>
        public static List<KpiEntity> GetAllEntity() {
            List<KpiEntity> ltKpis = new List<KpiEntity>();

			string sql = "select * from KPI_Kpi";

			DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
			foreach (DataRow dr in dt.Rows) {
                KpiEntity entity = new KpiEntity();
				entity.DrToMember(dr);

				ltKpis.Add(entity);
			}

			return ltKpis;
		}


		/// <summary>
		/// 获得有效实体对象
		/// </summary>
		/// <returns></returns>
		public static List<String> GetValidEntity() {
			List<String> ResultList = new List<String>();
			string SqlText = @"select KpiID from KPI_Kpi  where KpiIsValid=1";
			IDataReader Reader = DBAccess.GetRelation().ExecuteReader(SqlText);
			try {
				while (Reader.Read()) {
					ResultList.Add(Reader.GetString(0));
				}
			}
			finally {
				Reader.Close();
				Reader.Dispose();
			}
			return ResultList;
			//List<String> ltKpis = new List<String>();
			//string sql = @"select * from KPI_Kpi  where KpiIsValid=1";
			//DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
			//foreach (DataRow dr in dt.Rows) {
			//    KPI_KpiEntity entity = new KPI_KpiEntity();
			//    entity.DrToMember(dr);
			//    ltKpis.Add(entity.KpiID);
			//}
			//return ltKpis;
		}


	}
}
