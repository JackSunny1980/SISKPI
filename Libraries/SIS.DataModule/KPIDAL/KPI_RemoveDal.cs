using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using SIS.DBControl;
using SIS.DataAccess;
using SIS.DataEntity;
using System.Data.SqlClient;


namespace SIS.DataAccess {
	public class KPI_RemoveDal : DalBase<KPI_RemoveEntity> {
		/// <summary>
		/// 删除数据
		/// </summary>
		/// <returns></returns>
		public static bool DeleteRM(string RMID) {
			//删除参数信息
			string sql = "delete from KPI_Remove ";
			if (RMID != "") {
				sql += " where RMID = '" + RMID + "'";
			}

			return DBAccess.GetRelation().ExecuteNonQuery(sql) > 0;
		}

		/// <summary>
		/// 得到所有子表数据
		/// </summary>
		/// <returns></returns>
		public static DataTable GetDataTable() {
			string sql = "select * from KPI_Remove";
			DataTable dataTable = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
			return dataTable;
		}

		/// <summary>
		/// 通过主键得到实体对象
		/// </summary>
		/// <param name="RedoID">主键</param>
		/// <returns></returns>
		public static KPI_RemoveEntity GetEntity(string RMID) {
			KPI_RemoveEntity entity = new KPI_RemoveEntity();
			string sql = "select * from KPI_Remove where RMID='{0}'";
			sql = string.Format(sql, RMID);
			DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
			if (dt.Rows.Count > 0) {
				entity.DrToMember(dt.Rows[0]);
			}
			return entity;
		}


		/// <summary>
		/// 得到相关时间段内的记录
		/// </summary>
		/// <returns></returns>
		public static DataTable GetRecords(string strStartTime, string strEndTime) {
			string sql = @"select * from KPI_Remove 
                        where 1=1 {0} 
                        order by RMStartTime desc";

			string strfilter = "";

			if (strStartTime != "") {
				strfilter += " and RMStartTime>='" + strStartTime + "'";
			}

			if (strEndTime != "") {
				strfilter += " and RMStartTime<='" + strEndTime + "'";
			}

			sql = string.Format(sql, strfilter);

			return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
		}


		/// <summary>
		/// 标记删除相关时间段内的记录
		/// </summary>
		/// <returns></returns>
		public static bool RemoveOneRescord(string rmid) {
			KPI_RemoveEntity mEntity = GetEntity(rmid);				
			if (mEntity != null) {
				if (mEntity.RMType < 4) UpdateECSSArchive(mEntity);
				if (mEntity.RMType == 4) RemoveSATag(mEntity);
				mEntity.RMIsValid = 1;
				Update(mEntity);
			}
			//数据重算
            //DateTime Date = Convert.ToDateTime(mEntity.RMStartTime);
            //DateTime StartDate = new DateTime(Date.Year, Date.Month, 1);
            //DateTime EndDate = StartDate.AddMonths(1).AddSeconds(-5);	
            //Recalculate(StartDate, EndDate);
			return true;
		}


        public static void RecalcScoreAndBonus(DateTime StartDate, DateTime EndDate) {
			//sql = string.Format(sql, condition);
			IDbDataParameter[] Parameters = new SqlParameter[] {
				new SqlParameter("@StartDate",DbType.DateTime),
                new SqlParameter("@EndDate",DbType.DateTime)};
			Parameters[0].Value = StartDate;
			Parameters[1].Value = EndDate;
			DBAccess.GetRelation().ExecuteNonQuery("Proc_RecalcScoreAndBonus", CommandType.StoredProcedure, Parameters); ;
		}

		/// <summary>
		/// 标记删除相关时间段内的记录
		/// </summary>
		/// <returns></returns>
		public static bool RemoveMultiRescord(string[] rmids) {
			if (rmids.Length <= 0) {
				return true;
			}

            KPI_RemoveEntity Entity;
            List<DateTime> DateList = new List<DateTime>();
            foreach (string rmid in rmids) {
                Entity = GetEntity(rmid);
                DateList.Add(Convert.ToDateTime(Entity.RMStartTime));
                DateList.Add(Convert.ToDateTime(Entity.RMEndTime));
                RemoveOneRescord(rmid);
            }
            DateTime StartDate = DateList.Min();
            StartDate = new DateTime(StartDate.Year, StartDate.Month, 1);
            DateTime EndDate = DateList.Max();
            EndDate = new DateTime(EndDate.Year, EndDate.Month, 1);
            EndDate = EndDate.AddMonths(1).AddSeconds(-10);
            RecalcScoreAndBonus(StartDate, EndDate);
			return true;
		}

		private static void RemoveSATag(KPI_RemoveEntity mEntity) {
			using (KPI_SATagValueDal DataAccess = new KPI_SATagValueDal()) {
				DateTime StartDate = Convert.ToDateTime(mEntity.RMStartTime);
				DateTime EndDate = Convert.ToDateTime(mEntity.RMEndTime);
				DataAccess.RemovedSATagValue(mEntity.RMKPIID, StartDate, EndDate);
			}
		}

		private static void RestoredSATag(KPI_RemoveEntity mEntity) {
			using (KPI_SATagValueDal DataAccess = new KPI_SATagValueDal()) {
				DateTime StartDate = Convert.ToDateTime(mEntity.RMStartTime);
				DateTime EndDate = Convert.ToDateTime(mEntity.RMEndTime);
				DataAccess.RestoredSATagValue(mEntity.RMKPIID, StartDate, EndDate);
			}
		}

		/// <summary>
		/// 标记删除相关时间段内的记录
		/// </summary>
		/// <returns></returns>
		public static bool UpdateECSSArchive(KPI_RemoveEntity mEntity) {
			int nValid = mEntity.RMIsValid;
			if (nValid == 1) {
				return true;
			}

			string sql = "Update KPI_ECSSArchive Set ECIsRemove=1 where 1=1 {0}";
			//////////////////////////////////////////////////////////////////////
			string condition = "";
			string kpiid = mEntity.RMKPIID;
			int rmtype = mEntity.RMType;
			if (rmtype == 0) {
				condition = " and UnitID = '" + kpiid + "'";
			}
			else if (rmtype == 1) {
				condition = " and SeqID = '" + kpiid + "'";
			}
			if (rmtype == 2) {
				condition = " and KpiID = '" + kpiid + "'";
			}
			if (rmtype == 3) {
				condition = " and ECID = '" + kpiid + "'";
			}
			condition += " and ECTime>='" + mEntity.RMStartTime + "'";
			condition += " and ECTime<='" + mEntity.RMEndTime + "'";
			///////////////////////////////////////////////////////////////
			sql = string.Format(sql, condition);
			DBAccess.GetRelation().ExecuteNonQuery(sql);
			return true;

		}


		/// <summary>
		/// 标记恢复相关时间段内的记录
		/// </summary>
		/// <returns></returns>
		public static bool RestoreOneRescord(string rmid) {
			KPI_RemoveEntity mEntity = GetEntity(rmid);	
			if (mEntity != null) {
				if (mEntity.RMType < 4) RestoreECSSArchive(mEntity);
				if (mEntity.RMType == 4) RestoredSATag(mEntity);  
				mEntity.RMIsValid = 0;
				Update(mEntity);
			}
            ////数据重算
            //DateTime Date = Convert.ToDateTime(mEntity.RMStartTime);
            //DateTime StartDate = new DateTime(Date.Year, Date.Month, 1);
            //DateTime EndDate = StartDate.AddMonths(1).AddSeconds(-5);
            //RecalcScoreAndBonus(StartDate, EndDate);
			return true;
		}

		/// <summary>
		/// 标记恢复相关时间段内的记录
		/// </summary>
		/// <returns></returns>
		public static bool RestoreECSSArchive(KPI_RemoveEntity mEntity) {
			int nValid = mEntity.RMIsValid;

			if (nValid == 0) {
				return true;
			}

			string sql = "Update KPI_ECSSArchive Set ECIsRemove=0 where 1=1 {0}";

			//////////////////////////////////////////////////////////////////////
			string condition = "";

			string kpiid = mEntity.RMKPIID;

			int rmtype = mEntity.RMType;
			if (rmtype == 0) {
				condition = " and UnitID = '" + kpiid + "'";
			}
			else if (rmtype == 1) {
				condition = " and SeqID = '" + kpiid + "'";
			}
			if (rmtype == 2) {
				condition = " and KpiID = '" + kpiid + "'";
			}
			if (rmtype == 3) {
				condition = " and ECID = '" + kpiid + "'";
			}

			condition += " and ECTime>='" + mEntity.RMStartTime + "'";
			condition += " and ECTime<'" + mEntity.RMEndTime + "'";

			///////////////////////////////////////////////////////////////

			sql = string.Format(sql, condition);

			DBAccess.GetRelation().ExecuteNonQuery(sql);

			return true;

		}



	}
}
