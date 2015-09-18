using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SIS.DBControl;
using SIS.DataAccess;
using SIS.DataEntity;


namespace SIS.DataAccess {
	public class KPI_RedoDal : DalBase<KPI_RedoEntity> {
		/// <summary>
		/// 删除数据
		/// </summary>
		/// <returns></returns>
		public static bool DeleteRD(string RDID) {
			//删除参数信息
			string sql = "delete from KPI_Redo ";
			if (RDID != "") {
				sql += " where RDID = '" + RDID + "'";
			}

			return DBAccess.GetRelation().ExecuteNonQuery(sql) > 0;
		}

		/// <summary>
		/// 得到所有子表数据
		/// </summary>
		/// <returns></returns>
		public static DataTable GetDataTable() {
			string sql = "select * from KPI_Redo";
			DataTable dataTable = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
			return dataTable;
		}



		/// <summary>
		/// 得到相关时间段内的记录
		/// </summary>
		/// <returns></returns>
		public static DataTable GetRecords(string strStartTime, string strEndTime) {
			string sql = @"select * from KPI_Redo 
                        where 1=1 {0} 
                        order by RDStartTime desc";

			string strfilter = "";

			if (strStartTime != "") {
				strfilter += " and RDStartTime>='" + strStartTime + "'";
			}

			if (strEndTime != "") {
				strfilter += " and RDStartTime<='" + strEndTime + "'";
			}

			sql = string.Format(sql, strfilter);

			return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
		}



		/// <summary>
		/// 通过主键得到实体对象
		/// </summary>
		/// <param name="RedoID">主键</param>
		/// <returns></returns>
		public static KPI_RedoEntity GetEntity(string RedoID) {
			KPI_RedoEntity entity = new KPI_RedoEntity();

			string sql = "select * from KPI_Redo where RDID='{0}'";
			sql = string.Format(sql, RedoID);

			DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
			if (dt.Rows.Count > 0) {
				entity.DrToMember(dt.Rows[0]);
			}

			return entity;
		}


		/// <summary>
		/// 获得与主键对应的实体对象
		/// </summary>
		/// <returns></returns>
		public static List<KPI_RedoEntity> GetAllEntity() {
			List<KPI_RedoEntity> Result = null;
			string sqlText = @"select * from KPI_Redo where RDIsValid=1 and RDIsCalced=0";
			using (IDataReader reader = DBAccess.GetRelation().ExecuteReader(sqlText)) {
				Result = reader.FillGenericList<KPI_RedoEntity>();
				reader.Close();
			}
			return Result;
			//查询有效且没有计算过的重算指标集合
			/*List<KPI_RedoEntity> ltRDs = new List<KPI_RedoEntity>();
			string SqlText = "select * from KPI_Redo where RDIsValid=1 and RDIsCalced=0 ";
			DataTable dt = DBAccess.GetRelation().ExecuteDataset(SqlText).Tables[0];
			foreach (DataRow dr in dt.Rows) {
				KPI_RedoEntity entity = new KPI_RedoEntity();
				entity.DrToMember(dr);
				ltRDs.Add(entity);
			}
			return ltRDs;*/
		}

		/// <summary>
		/// 获得与主键对应的实体对象
		/// </summary>
		/// <returns></returns>
		public static List<KPI_RedoEntity> GetRedoEntity() {
			List<KPI_RedoEntity> ltRDs = new List<KPI_RedoEntity>();

			//查询有效且没有计算过的重算指标集合
			string sql = "select * from KPI_Redo where RDIsValid=1 and RDIsCalced=0 ";

			DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
			foreach (DataRow dr in dt.Rows) {
				KPI_RedoEntity entity = new KPI_RedoEntity();
				entity.DrToMember(dr);

				ltRDs.Add(entity);
			}

			return ltRDs;
		}

		/// <summary>
		/// 标记删除相关时间段内的记录
		/// </summary>
		/// <returns></returns>
		public static bool ValidOneRescord(string rdid) {
			KPI_RedoEntity mEntity = GetEntity(rdid);

			if (mEntity != null) {
				mEntity.RDIsValid = 1;
				mEntity.RDIsCalced = 0;
				mEntity.RDCalcedTime = "";

				Update(mEntity);
			}


			return true;
		}


	}
}
