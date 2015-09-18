using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SIS.DBControl;
using SIS.DataAccess;
using SIS.DataEntity;

namespace SIS.DataAccess {
	public class KPI_UnitDal : DalBase<KPI_UnitEntity>, IDisposable {

		/// <summary>
		/// 删除数据
		/// </summary>
		/// <returns></returns>
		public static bool DeleteUnit(string UnitID) {
			string keyid = UnitID;
			//删除机组
			string sql = "delete from KPI_Unit where UnitID ='{0}'";
			sql = string.Format(sql, keyid);
			return DBAccess.GetRelation().ExecuteNonQuery(sql) > 0;
		}

		/// <summary>
		/// 得到所有子表数据
		/// </summary>
		/// <returns></returns>
		public static DataTable GetDataTable() {
			string sql = "select * from KPI_Unit";
			DataTable dataTable = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
			return dataTable;
		}

		/// <summary>
		/// 得到Unit
		/// </summary>
		/// <returns></returns>
		public static string GetMWTag(string UnitID) {
			string sql = "select UnitMWTag, UnitMW  from KPI_Unit where UnitID='" + UnitID + "'";

			DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

			if (dt.Rows.Count == 1) {
				return dt.Rows[0]["UnitMWTag"].ToString();
			}

			return "";
		}

		/// <summary>
		/// 得到Unit
		/// </summary>
		/// <returns></returns>
		public static string GetCalcCondition(string UnitID) {
			string sql = "select UnitMWTag, UnitMW  from KPI_Unit where UnitID='" + UnitID + "'";
			string condition = "";

			DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

			if (dt.Rows.Count == 1) {
				condition = "'" + dt.Rows[0]["UnitMWTag"].ToString() + "'>";
				condition += (double.Parse(dt.Rows[0]["UnitMW"].ToString()) * 0.3).ToString();

			}


			return condition;
		}

		/// <summary>
		/// 得到Unit
		/// </summary>
		/// <returns></returns>
		public static DataTable GetUnits(string PlantID) {
			string sql = @"select UnitID[ID], UnitDesc[Name]  from KPI_Unit 
                        where UnitIsValid=1 {0}
                        order by UnitDesc";

			string condition = "";

			if (PlantID != "") {
				condition = " and PlantID='" + PlantID + "'";
			}

			sql = string.Format(sql, condition);

			return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
		}


		/// <summary>
		/// 得到Unit
		/// </summary>
		/// <returns></returns>
		public List<KPI_UnitEntity> GetUnitIDs(string PlantID) {
			List<KPI_UnitEntity> Result = null;
			string sqlText = @"SELECT UnitID, UnitDesc, WorkID,UnitName  
                                FROM KPI_Unit WHERE UnitIsValid=1 ORDER BY UnitName";
			if (!String.IsNullOrEmpty(PlantID)) {
				sqlText = string.Format(@"SELECT UnitID, UnitDesc, WorkID ,UnitName   
                        FROM KPI_Unit WHERE UnitIsValid = 1 AND  PlantID='{0}' ORDER BY UnitName", PlantID);
			}
			using (IDataReader reader = DBAccess.GetRelation().ExecuteReader(sqlText)) {
				Result = reader.FillGenericList<KPI_UnitEntity>();
				reader.Close();
			}
			return Result;
			//            string sql = @"select UnitID, UnitDesc, WorkID  
			//                                from KPI_Unit 
			//                                where UnitIsValid=1 {0}
			//                                order by UnitName";
			//            string condition = "";
			//            if (PlantID != "") {
			//                condition = " and PlantID='" + PlantID + "'";
			//            }
			//            sql = string.Format(sql, condition);
			//            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
		}


		/// <summary>
		/// 得到Unit
		/// </summary>
		/// <returns></returns>
		public static int GetUnitNum(string PlantID) {
			string sql = "select count(UnitID)  from KPI_Unit where UnitIsValid=1";
			if (PlantID != "") {
				sql = sql + " and PlantID='" + PlantID + "'";
			}
			return int.Parse(DBAccess.GetRelation().ExecuteScalar(sql).ToString());
		}

		/// <summary>
		/// 得到Unit
		/// </summary>
		/// <returns></returns>
		public static string GetUnitID(string UnitName) {
			string sql = "select UnitID, UnitName  from KPI_Unit where UnitIsValid=1 and UnitName='" + UnitName + "'";
			DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
			if (dt.Rows.Count != 1) {
				return "";
			}
			else {
				return dt.Rows[0]["UnitID"].ToString();
			}

		}

		/// <summary>
		/// 得到Unit
		/// </summary>
		/// <returns></returns>
		public static string GetUnitIDByCode(string UnitCode) {
			string sql = "select UnitID, UnitName  from KPI_Unit where UnitIsValid=1 and UnitCode='" + UnitCode + "'";
			DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
			if (dt.Rows.Count != 1) {
				return "";
			}
			else {
				return dt.Rows[0]["UnitID"].ToString();
			}
		}


		/// <summary>
		/// 得到Unit
		/// </summary>
		/// <returns></returns>
		public static string GetUnitCode(string UnitID) {
			string sql = "select UnitCode  from KPI_Unit where UnitID='" + UnitID + "'";
			DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
			if (dt.Rows.Count != 1) {
				return "";
			}
			else {
				return dt.Rows[0]["UnitCode"].ToString();
			}

		}

		/// <summary>
		/// 得到Unit
		/// </summary>
		/// <returns></returns>
		public static string GetUnitName(string UnitID) {
			string sql = "select UnitID, UnitName  from KPI_Unit where UnitIsValid=1 and UnitID='" + UnitID + "'";

			DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

			if (dt.Rows.Count != 1) {
				return "";
			}
			else {
				return dt.Rows[0]["UnitName"].ToString();
			}

		}

		/// <summary>
		/// 得到Unit
		/// </summary>
		/// <returns></returns>
		public static string GetUnitDescFromID(string UnitID) {
			string sql = "select UnitID, UnitDesc  from KPI_Unit where UnitIsValid=1 and UnitID='" + UnitID + "'";
			DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
			if (dt.Rows.Count != 1) {
				return "";
			}
			else {
				return dt.Rows[0]["UnitDesc"].ToString();
			}
		}

		/// <summary>
		/// 得到Unit
		/// </summary>
		/// <returns></returns>
		public static string GetUnitDescFromName(string UnitName) {
			string sql = "select UnitID, UnitDesc  from KPI_Unit where UnitIsValid=1 and UnitName='" + UnitName + "'";
			DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
			if (dt.Rows.Count != 1) {
				return "";
			}
			else {
				return dt.Rows[0]["UnitDesc"].ToString();
			}

		}


		/// <summary>
		/// 得到Unit
		/// </summary>
		/// <returns></returns>
		public static DataTable GetUnitsFromName(string PlantName) {
			string condition = "";
			string sql = @"select UnitID, UnitIndex
                            from KPI_Unit a 
                            left outer join KPI_Plant b on a.PlantID=b.PlantID
                            where 1=1 {0} order by UnitIndex ";

			if (PlantName != "") {
				condition = " and b.PlantName = '" + PlantName + "'";
			}

			sql = string.Format(sql, condition);

			return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
		}


		/// <summary>
		/// 得到Unit
		/// </summary>
		/// <returns></returns>
		public static String GetWorkIDByCode(string UnitCode) {
			if (String.IsNullOrEmpty(UnitCode)) return "";
			String Result = "";
			string SqlText = @"select WorkID from KPI_Unit where UnitCode='{0}'";
			SqlText = string.Format(SqlText, UnitCode);
			object obj = DBAccess.GetRelation().ExecuteScalar(SqlText);
			if (obj != null) {
				Result = (String)obj;
			}
			return Result;
			//string sql = @"select WorkID from KPI_Unit where UnitCode='{0}'";
			//sql = string.Format(sql, UnitCode);
			//DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
			//if (dt.Rows.Count != 1) {
			//    return "";
			//}
			//else {
			//    return dt.Rows[0]["WorkID"].ToString();
			//}
		}

		/// <summary>
		/// 得到Unit
		/// </summary>
		/// <returns></returns>
		public static String GetWorkIDByID(string UnitID) {
			if (String.IsNullOrEmpty(UnitID)) return "";
			String Result = "";
			string SqlText = @"select WorkID from KPI_Unit where UnitID='{0}'";
			SqlText = string.Format(SqlText, UnitID);
			object obj = DBAccess.GetRelation().ExecuteScalar(SqlText);
			if (obj != null) {
				Result = (String)obj;
			}
			return Result;
			//string sql = @"select WorkID from KPI_Unit where UnitID='{0}'";
			//sql = string.Format(sql, UnitID);
			//DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
			//if (dt.Rows.Count != 1) {
			//    return "";
			//}
			//else {
			//    return dt.Rows[0]["WorkID"].ToString();
			//}
		}

		/// <summary>
		/// 判断名称的唯一性
		/// </summary>
		/// <param name="OutTagName"></param>
		/// <param name="ConfigMainID"></param>
		/// <returns></returns>
		public static bool UnitNameExists(string UnitName, string strID) {
			string sql = "select count(1) from KPI_Unit where 1=1 and UnitName='{0}' ";
			sql = string.Format(sql, UnitName);
			if (strID != "")
				sql = sql + " and UnitID <> '" + strID + "'";
			return int.Parse(DBAccess.GetRelation().ExecuteScalar(sql).ToString()) > 0;

		}

		/// <summary>
		/// 判断测点的唯一性
		/// </summary>
		/// <param name="OutTagName"></param>
		/// <param name="ConfigMainID"></param>
		/// <returns></returns>
		public static bool RunTagExists(string TagName, string strID) {
			string sql = "select count(1) from KPI_Unit where 1=1 and UnitRunTag='{0}' ";
			sql = string.Format(sql, TagName);
			if (strID != "")
				sql = sql + " and UnitID <> '" + strID + "'";
			return int.Parse(DBAccess.GetRelation().ExecuteScalar(sql).ToString()) > 0;

		}

		/// <summary>
		/// 判断测点的唯一性
		/// </summary>
		/// <param name="OutTagName"></param>
		/// <param name="ConfigMainID"></param>
		/// <returns></returns>
		public static bool MWTagExists(string TagName, string strID) {
			string sql = "select count(1) from KPI_Unit where 1=1 and UnitMWTag='{0}' ";
			sql = string.Format(sql, TagName);

			if (strID != "")
				sql = sql + " and UnitID <> '" + strID + "'";

			return int.Parse(DBAccess.GetRelation().ExecuteScalar(sql).ToString()) > 0;

		}

		/// <summary>
		/// 通过主键得到实体对象
		/// </summary>
		/// <param name="ConfigMainID">主键</param>
		/// <param name="SetSnapShot">是否设置测点实时值</param>
		/// <returns></returns>
		public static KPI_UnitEntity GetEntity(string UnitID) {
			KPI_UnitEntity Result = null;
			string sqlText = @"select * from KPI_Unit where UnitID='{0}'";
			sqlText = string.Format(sqlText, UnitID);
			using (IDataReader reader = DBAccess.GetRelation().ExecuteReader(sqlText)) {
				Result = reader.FillEntity<KPI_UnitEntity>();
				reader.Close();
			}
			return Result;
			//KPI_UnitEntity entity = new KPI_UnitEntity();
			//string sql = "select * from KPI_Unit where UnitID='{0}'";
			//sql = string.Format(sql, UnitID);
			//DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
			//if (dt.Rows.Count > 0)
			//    entity.DrToMember(dt.Rows[0]);
			//return entity;
		}

		/// <summary>
		/// 通过主键得到设备数量列表
		/// </summary>
		/// <param name="UnitID">主键</param>
		/// <returns></returns>
		public static string GetUnitMWMH(string UnitID) {
			string tag = "";

			string sql = "";
			DataTable dt;
			sql = @"select UnitMWTag from KPI_Unit where UnitID='{0}'";

			sql = string.Format(sql, UnitID);

			dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
			if (dt.Rows.Count == 1) {
				tag = dt.Rows[0]["UnitMWTag"].ToString();
			}

			sql = @"select UnitBRealTag, UnitBOptTag from Seek_Base where UnitID='{0}'";

			sql = string.Format(sql, UnitID);

			dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
			if (dt.Rows.Count == 1) {
				tag += "$" + dt.Rows[0]["UnitBRealTag"].ToString() + "$" + dt.Rows[0]["UnitBOptTag"].ToString();
			}

			return tag;

		}

		/// <summary>
		/// 通过主键得到设备数量列表
		/// </summary>
		/// <param name="UnitID">主键</param>
		/// <returns></returns>
		public static DataTable GetUnitInfor(string UnitID) {
			string sql = @"select UnitName,UnitDesc, UnitMW, UnitMWTag
                            from KPI_Unit where UnitID='{0}'";

			sql = string.Format(sql, UnitID);

			return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

		}

		/// <summary>
		/// 得到查询结果
		/// </summary>
		/// <param name="condition">条件字符串</param>
		/// <returns></returns>
		public static DataTable GetSearchList(string condition) {
			string sql = @"select UnitID,  PlantName,  UnitCode, UnitName, UnitDesc, UnitMW, UnitIsValid, UnitNote,
                            UnitIsKPI, UnitIsSnapshot,UnitIsSort
                            from KPI_Unit a 
                            left outer join KPI_Plant b on a.PlantID=b.PlantID  
                            where 1=1 {0} order by a.UnitName";

			sql = string.Format(sql, condition);
			return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// 获得与主键对应的实体对象
		/// </summary>
		/// <returns></returns>
		public static List<KPI_UnitEntity> GetAllEntity() {
			List<KPI_UnitEntity> Result = null;
			string sqlText = @"select * from KPI_Unit WHERE UnitIsValid=1";
			using (IDataReader reader = DBAccess.GetRelation().ExecuteReader(sqlText)) {
				Result = reader.FillGenericList<KPI_UnitEntity>();
				reader.Close();
			}
			return Result;
			//List<KPI_UnitEntity> ltUnits = new List<KPI_UnitEntity>();
			//string sql = "select * from KPI_Unit";
			//DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
			//foreach (DataRow dr in dt.Rows) {
			//    KPI_UnitEntity entity = new KPI_UnitEntity();
			//    entity.DrToMember(dr);

			//    ltUnits.Add(entity);
			//}
			//return ltUnits;
		}


		/////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// 获得有效的实体对象
		/// </summary>
		/// <returns></returns>
		public static List<KPI_UnitEntity> GetValidEntity() {
			List<KPI_UnitEntity> Result = null;
			string sqlText = @"select * from KPI_Unit 
                            where UnitIsValid=1 and UnitIsKPI=1";
			using (IDataReader reader = DBAccess.GetRelation().ExecuteReader(sqlText)) {
				Result = reader.FillGenericList<KPI_UnitEntity>();
				reader.Close();
			}
			return Result;
			//            List<KPI_UnitEntity> ltUnits = new List<KPI_UnitEntity>();
			//            string sql = @"select * from KPI_Unit 
			//                            where UnitIsValid=1 and UnitIsKPI=1";
			//            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
			//            foreach (DataRow dr in dt.Rows) {
			//                KPI_UnitEntity entity = new KPI_UnitEntity();
			//                entity.DrToMember(dr);
			//                ltUnits.Add(entity);
			//            }
			//            return ltUnits;
		}

        public static List<KPI_UnitEntity> GetUnitListByPlantCode(string PlantCode) {
            List<KPI_UnitEntity> Result = null;
            string sqlText = @"SELECT UnitID, UnitDesc, WorkID,UnitName  
                                FROM KPI_Unit WHERE UnitIsValid=1 ORDER BY UnitName";
            if (!String.IsNullOrEmpty(PlantCode)) {
                sqlText = string.Format(@"SELECT UnitID, UnitDesc, WorkID ,UnitName   
                                            FROM KPI_Unit A JOIN KPI_Plant B ON A.PlantID = B.PlantID
                                            WHERE UnitIsValid = 1 AND  B.PlantCode='{0}' 
                                            ORDER BY UnitIndex", PlantCode);
            }
            using (IDataReader reader = DBAccess.GetRelation().ExecuteReader(sqlText)) {
                Result = reader.FillGenericList<KPI_UnitEntity>();
                reader.Close();
            }
            return Result;
        }

		public void Dispose() {

		}
	}
}
