using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using SIS.DBControl;
using SIS.DataAccess;
using SIS.DataEntity;

using SIS.Loger;
using System.Data.SqlClient;

namespace SIS.DataAccess {
    public class ECSSSnapshotDal : DalBase<ECSSValueEntity> {
		////
		//需要好好检查所有函数

		/// <summary>
		/// 删除数据
		/// </summary>
		/// <returns></returns>
		public static bool DeleteTag(string ECID) {
			string sql = "delete from KPI_ECSSSnapshot ";
			if (ECID != "") {
				sql += " where ECID = '" + ECID + "'";
			}

			return DBAccess.GetRelation().ExecuteNonQuery(sql) > 0;
		}


		/// <summary>
		/// 得到所有子表数据
		/// </summary>
		/// <returns></returns>
		public static DataTable GetDataTable() {
			string sql = "select * from KPI_ECSSSnapshot";
			DataTable dataTable = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
			return dataTable;
		}

		/// <summary>
		/// 判断名称的唯一性
		/// </summary>
		/// <param name="TagName"></param>
		/// <param name="TagID"></param>
		/// <returns></returns>
		public static bool TagNameExists(string TagName, string TagID) {
			string sql = "select count(1) from KPI_ECSSSnapshot where TagName='{0}' ";
			sql = string.Format(sql, TagName);

			if (TagID != "")
				sql = sql + " and TagID <> '" + TagID + "'";

			return int.Parse(DBAccess.GetRelation().ExecuteScalar(sql).ToString()) > 0;
		}

		/// <summary>
		/// 得到查询结果
		/// </summary>
		/// <param name="condition">条件字符串</param>
		/// <param name="ecweb"></param>
		/// <param name="srvtime"></param>
		/// <param name="plantid"></param>
		/// <param name="unitid"></param>
		/// <returns></returns>
		public static DataTable GetSearchList(string ecweb, string srvtime, string plantid = "", string unitid = "") {
			StringBuilder sb = new StringBuilder();
			sb.Append(@"WITH KPIRealValueECT AS(
					SELECT a.ECID,  c.UnitID, c.UnitName, c.WorkID, a.ECName, d.EngunitName, b.ECOptimal, ECValue, 
					a.ECScore, ECQulity, ECTime, ECShift, ECOptExp,ECIndex,ECNote
					FROM KPI_ECSSSnapshot a   LEFT JOIN KPI_ECTag b on a.ECID = b.ECID 
						RIGHT JOIN KPI_Unit c on b.UnitID = c.UnitID 
						LEFT JOIN KPI_Engunit d on b.EngunitID = d.EngunitID 
					WHERE b.ECIsValid =1 AND b.ECIsDisplay=1 ");
			if (!String.IsNullOrEmpty(plantid)) {
				sb.Append("AND c.PlantID ='" + plantid + "' ");
			}

			if (!String.IsNullOrEmpty(unitid)) {
				sb.Append(" AND c.UnitID ='" + unitid + "' ");
			}
			if (!String.IsNullOrEmpty(ecweb)) {
				sb.Append(" AND  b.ECWeb='" + ecweb + "' ");
			}
			sb.Append(")");
			List<KPI_UnitEntity> unitList = KPI_UnitDal.GetAllEntity();
			string workid = KPI_UnitDal.GetWorkIDByCode(unitList[0].UnitCode);
			string ShiftName = "";
			string PeriodName = "";
			string StartTime = "";
			string EndTime = "";
			string strTime = KPI_SystemDal.GetKPISrvTime();
			bool bGood = KPI_WorkDal.GetShiftAndPeriod(workid, strTime, ref ShiftName, ref PeriodName, ref StartTime, ref EndTime);
			DateTime date = DateTime.Parse(StartTime);
			date = new DateTime(date.Year, date.Month, 1);
			string strMonthStart = date.ToString("yyyy-MM-dd 01:00:00");
			date = date.AddMonths(1);
			string strMonthEnd = date.ToString("yyyy-MM-dd 01:00:00");
			sb.Append(" SELECT A.*,B.ECScoreDay,C.ECScoreMonth FROM KPIRealValueECT A ");
            sb.AppendFormat(" LEFT JOIN (SELECT SUM(Score)  ECScoreDay, ECID FROM KPI_ECHourData " +
                "WHERE  Shift='{0}' AND CheckDate  BETWEEN '{1}' AND '{2}' " +
				"GROUP BY ECID) B ON A.ECID=B.ECID",ShiftName,StartTime,EndTime);
            sb.AppendFormat(" LEFT JOIN (SELECT SUM(Score)  ECScoreMonth, ECID FROM KPI_ECHourData " +
                "WHERE  Shift='{0}' AND CheckDate  BETWEEN '{1}' AND '{2}'  " +
				"GROUP BY ECID) C ON A.ECID=C.ECID", ShiftName, strMonthStart, strMonthEnd);
			sb.Append(" ORDER BY ECIndex,UNitName");
			String sqlText = sb.ToString();
			DataTable dt = DBAccess.GetRelation().ExecuteDataset(sqlText).Tables[0];
			return dt;
		}


		/// <summary>
		/// 得到查询结果
		/// </summary>
		/// <param name="condition">条件字符串</param>
		/// <returns></returns>
		public static DataTable GetSearchListTrend(string ecid, string stime, string etime) {
			string sql = @"select ECID, ECName, ECValue, ECTime, ECPeriod, ECShift
                        from KPI_ECSSSnapshot 
                        where ECID='{0}'
                        and CONVERT(varchar(20), ECTime, 120) > '{1}'
                        and CONVERT(varchar(20), ECTime, 120) < '{2}'
                        order by ECTime";

			sql = string.Format(sql, ecid, stime, etime);

			DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

			return dt;
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="UnitID"></param>
		/// <returns></returns>
		public static DataTable GetSearchListC(string UnitID) {
			DataTable dt = null;

			//            string WorkID = KPI_BaseDal.GetWorkID(UnitID);

			//            DataTable dtItems = KPI_TagDal.GetTags(UnitID);

			//            if (dtItems == null && dtItems.Rows.Count <= 0)
			//            {
			//                return dt;
			//            }

			//            string condition = "";
			//            for (int i = 0; i < dtItems.Rows.Count; i++)
			//            {
			//                string ItemID = dtItems.Rows[i]["TagID"].ToString();
			//                string ItemDesc = dtItems.Rows[i]["TagDesc"].ToString();
			//                string ItemEngunit = dtItems.Rows[i]["TagEngunit"].ToString();

			//                condition += "cast(avg(case TagID when '" + ItemID + "' then TagValue else null  end) as numeric(18,4)) as '" + ItemDesc + "[" + ItemEngunit + "]', ";

			//            }

			//            condition = condition.Remove(condition.LastIndexOf(','));

			//            string sql = @"select ShiftName As '运行值', {0}
			//                                from KPI_ECSSSnapshot
			//                                a left outer join KPI_Shift b on  a.TagShift = b.ShiftID
			//                                 group by ShiftName, ShiftID
			//                                order by b.ShiftID";

			//            sql = string.Format(sql, condition);

			//            dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

			return dt;
		}


		/// <summary>
		/// 得到相关时间段内的记录
		/// </summary>
		/// <returns></returns>
		public static DataTable GetOneRecord(string ecid) {
			string sql = @"select b.ECCode, b.ECName, ECCalcExp, ECExpression
                            from KPI_ECSSSnapshot a
                            left outer join KPI_ECTag b on a.ECID=b.ECID 
                            where a.ECID='{0}' ";
			sql = string.Format(sql, ecid);
			return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
		}

        /// <summary>
        /// 生成机组实时综合煤耗
        /// </summary>
        public static void GenerateSnapshotCoalConsumption(String UnitID, String UnitName, String CalcTime) {
            IDbDataParameter[] parames = new SqlParameter[] {        
                new SqlParameter("@UnitID",DbType.String),
                new SqlParameter("@UnitName",DbType.String),
                new SqlParameter("@CalcTime",DbType.String),
                new SqlParameter("@ECID",DbType.String),
                new SqlParameter("@ECTX",DbType.String)};

            String ECID = "0090";
            string ECTX = "GHND01_ZHMH";
            if (UnitID == "04") {
                ECID = "0091";
                ECTX = "GHND02_ZHMH";
            }
            parames[0].Value = UnitID;
            parames[1].Value = UnitName;
            parames[2].Value = CalcTime;
            parames[3].Value = ECID;
            parames[4].Value = ECTX;

            String SqlText = @" DELETE FROM KPI_ECSSSnapshot WHERE ECID=@ECID;
                                INSERT INTO KPI_ECSSSnapshot(SSID,UnitID,ECID,SeqID,ECName,ECTX,ECTime,ECValue,ECScore,ECPeriod,ECShift,ECIsRemove)
                                 SELECT NEWID() SSID,A.UnitiD,@ECID ECID,
                                 '52132c9a-28d3-4676-aa06-79960d813ddd' SeqID,
                                 @UnitName + '综合煤耗' ECName,
                                 @ECTX ECTX,ECTime,SUM(ISNULL(ECValue,0)) + 342.3 ECValue,
                                 0 ECScore, ECPeriod,ECShift,0 ECIsMove FROM KPI_ECSSSnapshot A JOIN KPI_ECTag B ON A.ECID = B.ECID
                                 WHERE B.ECWeb='ZHMH' AND A.UnitID=@UnitID AND A.ECTime=@CalcTime
                                 GROUP BY A.UnitID,ECTime,ECPeriod,ECShift";
            DBAccess.GetRelation().ExecuteNonQuery(SqlText, parames);
        }

        /// <summary>
        /// 生成机组历史综合煤耗
        /// </summary>
        public static void GenerateArchiveCoalConsumption(String UnitID, String UnitName, String CalcTime) {
            IDbDataParameter[] parames = new SqlParameter[] {        
                new SqlParameter("@UnitID",DbType.String),
                new SqlParameter("@UnitName",DbType.String),
                new SqlParameter("@CalcTime",DbType.String),
                new SqlParameter("@ECID",DbType.String),
                new SqlParameter("@ECTX",DbType.String)};
             
            String ECID = "0090";
            string ECTX = "GHND01_ZHMH";
            if (UnitID == "04") {
                ECID = "0091";
                ECTX = "GHND02_ZHMH";
            }
            parames[0].Value = UnitID;
            parames[1].Value = UnitName;
            parames[2].Value = CalcTime;  
            parames[3].Value = ECID;
            parames[4].Value = ECTX;
            String SqlText = @"DELETE FROM KPI_ECSSArchive WHERE ECID=@ECID AND ECTime=@CalcTime;
                               INSERT INTO KPI_ECSSArchive(SSID,UnitID,ECID,SeqID,ECName,ECTX,ECTime,ECValue,ECScore,ECPeriod,ECShift,ECIsRemove)
                                SELECT NEWID() SSID,A.UnitiD,@ECID ECID,
                                '52132c9a-28d3-4676-aa06-79960d813ddd' SeqID,
                                @UnitName + '综合煤耗' ECName,
                                @ECTX ECTX,ECTime,SUM(ISNULL(ECValue,0)) + 342.3 ECValue,
                                0 ECScore, ECPeriod,ECShift,0 ECIsMove FROM KPI_ECSSArchive A JOIN KPI_ECTag B ON A.ECID = B.ECID
                                WHERE B.ECWeb='ZHMH' AND A.UnitID=@UnitID AND A.ECTime=@CalcTime
                                GROUP BY A.UnitID,ECTime,ECPeriod,ECShift";
            DBAccess.GetRelation().ExecuteNonQuery(SqlText, parames);
        }

	}
}
