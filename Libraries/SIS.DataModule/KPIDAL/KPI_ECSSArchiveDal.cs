using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;

using SIS.DBControl;
using SIS.DataAccess;
using SIS.DataEntity;

using SIS.Loger;

namespace SIS.DataAccess {

    public class ECSSArchiveDal : DalBase<ECSSValueEntity> {
		////
		//需要好好检查所有函数

		/// <summary>
		/// 删除数据
		/// </summary>
		/// <returns></returns>
		public static bool DeleteTag(string ECID, string ECTime) {
			string sql = "delete from KPI_ECSSArchive ";
			if (ECID != "") {
				sql += " where ECID = '" + ECID + "' and ECTime='" + ECTime + "'";
			}

			return DBAccess.GetRelation().ExecuteNonQuery(sql) > 0;
		}


		/// <summary>
		/// 得到相关时间段内的得分总和
		/// </summary>
		/// <returns></returns>
		public static double GetTotalScore(string strecid, string strshift, string strStartTime, string strEndTime) {
			string sql = @"select sum(ECScore) as sumvalue 
                        from KPI_ECSSArchive 
                        where ECID='{0}' and ECShift='{1}'  {2} ";

			string strfilter = "";
			if (strStartTime != "") {
				strfilter = " and ECTime>='" + strStartTime + "'";
			}

			if (strEndTime != "") {
				strfilter += " and ECTime<='" + strEndTime + "'";
			}

			sql = string.Format(sql, strecid, strshift, strfilter);

			DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

			double dresult = 0.0;
			if (dt.Rows.Count == 1 && dt.Rows[0]["sumvalue"].ToString() != "") {
				dresult = double.Parse(dt.Rows[0]["sumvalue"].ToString());
			}
			else {
				dresult = 0.0;
			}

			return dresult;
		}


		/// <summary>
		/// 得到相关时间段内的得分总和
		/// </summary>
		/// <returns></returns>
		public static DataTable GetTotalScoreByUnit(string unitid, string strshift, string strStartTime, string strEndTime) {
			string sql = @"select sum(ECScore) as SumScore, ECID
                        from KPI_ECSSArchive 
                        where UnitID='{0}' and ECShift='{1}' and ECIsRemove=0 {2} 
                        Group by ECID ";

			string strfilter = "";
			if (strStartTime != "") {
				strfilter = " and ECTime>='" + strStartTime + "'";
			}

			if (strEndTime != "") {
				strfilter += " and ECTime<='" + strEndTime + "'";
			}

			sql = string.Format(sql, unitid, strshift, strfilter);

			DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

			//double dresult = 0.0;
			//if (dt.Rows.Count == 1 && dt.Rows[0]["sumvalue"].ToString() != "")
			//{
			//    dresult = double.Parse(dt.Rows[0]["sumvalue"].ToString());
			//}
			//else
			//{
			//    dresult = 0.0;
			//}

			return dt;
		}



		/// <summary>
		/// 得到相关时间段内的记录
		/// </summary>
		/// <returns></returns>
		public static DataTable GetAllRecords(string strecid, string strStartTime, string strEndTime) {
			string sql = @"select * from KPI_ECSSArchive 
                        where ECID='{0}'  {1} 
                        order by ECTime DESC";

			string strfilter = "";
			if (strStartTime != "") {
				strfilter = " and ECTime>='" + strStartTime + "'";
			}

			if (strEndTime != "") {
				strfilter += " and ECTime<='" + strEndTime + "'";
			}

			sql = string.Format(sql, strecid, strfilter);

			return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
		}


		/// <summary>
		/// 值际奖金查询
		/// </summary>
		/// <param name="plantid"></param>
		/// <param name="ecweb"></param>
		/// <param name="QueryTime"></param>
		/// <param name="QueryMoney"></param>
		/// <returns></returns>
		public static DataTable GetBonusForPlant(string plantid, string ecweb, DateTime QueryTime, double QueryMoney) {
			////////////////////////////////////////////////////////////
			//生成统计时间
			//QueryTime: 2013-04-01 01:00:00   <>  2013-05-01 01:00:00
			//ReportTime: 1,1   27,0 （每月日期,小时)
			string ReportTime = KPI_ConstantDal.GetECWebQueryTime(ecweb);
			string[] strRTs = ReportTime.Split(new string[] { ";", "," }, StringSplitOptions.RemoveEmptyEntries);
			double[] dMN = new double[2];

			//日期需要自动减1，否则会后延1天。
			if (strRTs.Length != 2) {
				dMN[0] = 0;
				dMN[1] = 1;
			}
			else {
				dMN[0] = double.Parse(strRTs[0]) - 1;
				dMN[1] = double.Parse(strRTs[1]);
			}

			DateTime dtST = DateTime.Now;
			DateTime dtET = DateTime.Now;

			if (dMN[0] > 10) {
				dtST = QueryTime.AddMonths(-1).AddDays(dMN[0]).AddHours(dMN[1]);
				dtET = dtST.AddMonths(1);
				//dtET = dtST.AddDays(dMN[0]).AddHours(dMN[1]);
			}
			else {
				dtST = QueryTime.AddDays(dMN[0]).AddHours(dMN[1]);
				dtET = dtST.AddMonths(1);
				//dtET = QueryTime.AddMonths(1).AddDays(dMN[0]).AddHours(dMN[1]);
			}
			//指标得分为负分时不计入总分
//            string sql = @"select '1'IID, c.UnitName, ECShift, Sum(ISNULL(a.ECScore,0)) AS TotalScore, ''ECBonus, '0'MGBonus, ''TotalBonus, '1'TotalSort
//                            from KPI_ECSSArchive a
//                            left outer join KPI_ECTag b on (a.ECID = b.ECID {0})
//                            right outer join KPI_Unit c on (b.UnitID = c.UnitID {1})
//                            left outer join KPI_Engunit d on b.EngunitID = d.EngunitID 
//                            where b.ECIsValid =1 and b.ECIsDisplay=1 and a.ECIsRemove=0  AND A.ECScore>0{2}
//                            Group by UnitName, ECShift
//                            order by c.UnitName, ECShift ";
			String SqlText = @"WITH KPIECScore AS(
								SELECT '1'IID, A.UnitID, Shift=ECShift,
								 SUM(ISNULL(a.ECScore,0)) AS ECScore, 
								 0.0 ECBonus, 0.0 MGBonus, 0.0 TotalBonus, '1'TotalSort
								FROM KPI_ECSSArchive a
								LEFT  JOIN KPI_ECTag b on (a.ECID = b.ECID )
								WHERE b.ECIsValid =1 and b.ECIsDisplay=1 and a.ECIsRemove=0  AND A.ECScore>0 {2}AND A.ECTime BETWEEN '{0}' AND '{1}'
								GROUP BY A.UnitID, ECShift)
								SELECT A.*,TotalScore=ISNULL(ECScore,0) + ISNULL(SAScore,0),
									  SAScore=ISNULL(B.SAScore,0),C.UnitName
                               FROM KPIECScore A LEFT JOIN(SELECT SUM(ISNULL(SASCore,0)) SAScore,Shift,B.UnitID FROM KPI_SATagValue A
									 JOIN KPI_SATag B ON A.SAID=B.SAID
								WHERE  A.CalcDateTime BETWEEN  '{0}' AND '{1}'
								GROUP BY Shift,UnitID) B ON A.UnitID=B.UnitID AND A.Shift=B.Shift
								JOIN KPI_Unit              C  ON A.UnitID =C.UnitID {3}
                                ORDER BY c.UnitName, Shift";

			string condition1 = "";
			if (ecweb != "") {
				ecweb = " and b.ECWeb = '" + ecweb + "'";
			}

			string condition2 = "";
			if (plantid != "") {
				condition2 = " and c.PlantID = '" + plantid + "'";
			}

			//日期
			//string condition3 = " and ECTime>='" + dtST.ToString("yyyy-MM-dd HH:mm:ss") + "'";
			//condition3 += " and ECTime<'" + dtET.ToString("yyyy-MM-dd HH:mm:ss") + "'";
			SqlText = string.Format(SqlText, dtST.ToString("yyyy-MM-dd HH:mm:ss"), dtET.ToString("yyyy-MM-dd HH:mm:ss"),condition1, condition2);
			DataTable dt = DBAccess.GetRelation().ExecuteDataset(SqlText).Tables[0];

			/////////////////////////////////////////////////////////////////////////


			double dSum_TotalScore = 0.0;
			//double Score = 0;

			//赋值个IID，
			for (int i = 0; i < dt.Rows.Count; i++) {
				if (dt.Rows[i]["TotalScore"] != null) {
					//Score = Convert.ToDouble(dt.Rows[i]["TotalScore"]);
					dSum_TotalScore += double.Parse(dt.Rows[i]["TotalScore"].ToString());
				}
				dt.Rows[i]["IID"] = i;
			}

			if (dSum_TotalScore == 0) {
				return dt;
			}

			///////////////////////////////////////////////////////////////////
			List<KPIVAL> KBS = new List<KPIVAL>();
			//奖金= 得分×总额/总得分
			for (int i = 0; i < dt.Rows.Count; i++) {
				if (dt.Rows[i]["TotalScore"] != null) {
					double ecBonus = QueryMoney * Convert.ToDouble(dt.Rows[i]["TotalScore"]) / dSum_TotalScore;
					dt.Rows[i]["ECBonus"] = ecBonus.ToString("0.000");
					dt.Rows[i]["MGBonus"] = "0";

					dt.Rows[i]["TotalBonus"] = ecBonus.ToString("0.000");

					KBS.Add(new KPIVAL(i, ecBonus));

					//TotalSort
				}
			}

			//排序
			KBS.Sort(new KPIVALComparer(KPIVALComparer.CompareType.VAL));
			//反转排序
			KBS.Reverse();

			for (int i = 0; i < KBS.Count; i++) {
				dt.Rows[KBS[i].IID]["TotalSort"] = (i + 1).ToString();
			}

			return dt;
		}



		/// <summary>
		/// 值际竞赛月报表查询
		/// </summary>
		/// <param name="plantid"></param>
		/// <param name="ecweb"></param>
		/// <param name="QueryTime"></param>
		/// <param name="QueryMoney"></param>
		/// <returns></returns>
		public static DataTable GetRaceForPlant(string plantid, string ecweb, DateTime QueryTime) {
			////////////////////////////////////////////////////////////
			//生成统计时间
			//QueryTime: 2013-04-01 01:00:00   <>  2013-05-01 01:00:00
			//ReportTime: 1,1   27,0 （每月日期,小时)
			//string ReportTime = KPI_ConstantDal.GetECWebQueryTime(ecweb);
			//string[] strRTs = ReportTime.Split(new string[] { ";", "," }, StringSplitOptions.RemoveEmptyEntries);
			//double[] dMN = new double[2];

			////日期需要自动减1，否则会后延1天。
			//if (strRTs.Length != 2)
			//{
			//    dMN[0] = 0;
			//    dMN[1] = 1;
			//}
			//else
			//{
			//    dMN[0] = double.Parse(strRTs[0]) - 1;
			//    dMN[1] = double.Parse(strRTs[1]);
			//}

			//DateTime dtST = DateTime.Now;
			//DateTime dtET = DateTime.Now;

			//if (dMN[0] > 10)
			//{
			//    dtST = QueryTime.AddMonths(-1).AddDays(dMN[0]).AddHours(dMN[1]);
			//    dtET = QueryTime.AddDays(dMN[0]).AddHours(dMN[1]);
			//}
			//else
			//{
			//    dtST = QueryTime.AddDays(dMN[0]).AddHours(dMN[1]);
			//    dtET = QueryTime.AddMonths(1).AddDays(dMN[0]).AddHours(dMN[1]);
			//}

			#region 查询月和年的总分

			//查询月
			DateTime dtST = QueryTime.AddHours(1);
			DateTime dtET = dtST.AddMonths(1);

			DataTable dtMonth = GetRaceForPlantQuery(plantid, ecweb, dtST, dtET);

			//查询年
			dtST = new DateTime(QueryTime.Year, 1, 1).AddHours(1);
			dtET = dtST.AddYears(1);

			DataTable dtYear = GetRaceForPlantQuery(plantid, ecweb, dtST, dtET);

			//dtMonth ,dtYear的结构为：
			//ECShift, UnitName, Sum(a.ECScore) AS UnitScore

			#endregion

			#region 按机组的合并

			///////////////////////////////////////////////////////////////////////////////////////////////////////
			//合并两个表
			int count = 0;

			//按机组的
			var linqUnit = from dm in dtMonth.AsEnumerable()
						   from dy in dtYear.AsEnumerable()
						   where (dm.Field<string>("Shift") == dy.Field<string>("Shift") && dm.Field<string>("UnitName") == dy.Field<string>("UnitName"))
						   orderby dm.Field<string>("Shift"), dm.Field<string>("UnitName")
						   select new {
							   //IID For Sort
							   IID = count++,
							   //dm
							   Shift = dm.Field<string>("Shift"),
							   UnitName = dm.Field<string>("UnitName"),
							   MonthScore = dm.Field<Decimal>("TotalScore"),
							   //dy
							   YearScore = dy.Field<Decimal>("TotalScore"),
							   MonthSort = 0,
							   YearSort = 0
						   };

			//新表结构如下：
			//IID, ECShift, UnitName, MonthScore, YearScore, MonthSort, YearSort

			if (linqUnit.Count() <= 0) {
				return null;
			}

			#endregion

			#region 按机组的月、年排序

			//排序
			List<KPIVAL> LMS = new List<KPIVAL>();
			List<KPIVAL> LYS = new List<KPIVAL>();

			for (int i = 0; i < linqUnit.Count(); i++) {
				//if (linqbitem.ElementAt(i).MonthScore != null)
				//{
				//}

				LMS.Add(new KPIVAL(i, (double)linqUnit.ElementAt(i).MonthScore));
				LYS.Add(new KPIVAL(i, (double)linqUnit.ElementAt(i).YearScore));

			}

			//排序
			LMS.Sort(new KPIVALComparer(KPIVALComparer.CompareType.VAL));
			//反转排序
			LMS.Reverse();


			//排序
			LYS.Sort(new KPIVALComparer(KPIVALComparer.CompareType.VAL));
			//反转排序
			LYS.Reverse();

			DataTable dtUnit = linqUnit.ToDataTable();

			for (int i = 0; i < dtUnit.Rows.Count; i++) {
				dtUnit.Rows[LMS[i].IID]["MonthSort"] = (i + 1).ToString();
				dtUnit.Rows[LYS[i].IID]["YearSort"] = (i + 1).ToString();
			}

			dtUnit.Columns.Remove("IID");

			#endregion

			#region 按值的合并

			///////////////////////////////////////////////////////////////////////////////////////////
			//按值的(合并机组)

			count = 0;

			var linqShit = from du in dtUnit.AsEnumerable()
						   group du by du["Shift"]
							   into g
							   select new {
								   //IID For Sort
								   IID = count++,
								   Shift = g.Key.ToString(),
								   MonthSumScore = g.Sum(p => double.Parse(p["MonthScore"].ToString())),
								   YearSumScore = g.Sum(p => double.Parse(p["YearScore"].ToString())),
								   MonthSumSort = 0,
								   YearSumSort = 0
							   };

			//新表如下
			//IID, ECShift, MonthSumScore, YearSumScore, MonthSumSort, YearSumSort

			#endregion

			#region 按值的月、年排序

			//排序
			LMS = new List<KPIVAL>();
			LYS = new List<KPIVAL>();

			for (int i = 0; i < linqShit.Count(); i++) {
				//if (linqbitem.ElementAt(i).MonthScore != null)
				//{
				//}

				LMS.Add(new KPIVAL(i, linqShit.ElementAt(i).MonthSumScore));
				LYS.Add(new KPIVAL(i, linqShit.ElementAt(i).YearSumScore));

			}

			//排序
			LMS.Sort(new KPIVALComparer(KPIVALComparer.CompareType.VAL));
			//反转排序
			LMS.Reverse();


			//排序
			LYS.Sort(new KPIVALComparer(KPIVALComparer.CompareType.VAL));
			//反转排序
			LYS.Reverse();

			DataTable dtShift = linqShit.ToDataTable();

			for (int i = 0; i < dtShift.Rows.Count; i++) {
				dtShift.Rows[LMS[i].IID]["MonthSumSort"] = (i + 1).ToString();
				dtShift.Rows[LYS[i].IID]["YearSumSort"] = (i + 1).ToString();
			}

			dtShift.Columns.Remove("IID");

			#endregion

			///////////////////////////////////////////////////////////////////////////////////////
			//dtUnit
			//ECShift, UnitName, MonthScore, YearScore, MonthSort, YearSort

			//dtShift
			//ECShift, MonthSumScore, YearSumScore, MonthSumSort, YearSumSort

			#region 最终对机组、值的两个表左连接

			DataTable dtAll = new DataTable();

			///////////////////////////////////////////////////////////////////////////////////////
			//合并
			var linqresult = from du in dtUnit.AsEnumerable()
							 join ds in dtShift.AsEnumerable() on du.Field<string>("Shift") equals ds.Field<string>("Shift")
							 orderby du.Field<string>("Shift"), du.Field<string>("UnitName")
							 select new {
								 //du
								 ECShift = du.Field<string>("Shift"),
								 UnitName = du.Field<string>("UnitName"),
								 MonthScore = du == null ? 0 : du.Field<decimal>("MonthScore"),
								 YearScore = du == null ? 0 : du.Field<decimal>("YearScore"),
								 MonthSort = du == null ? 0 : du.Field<int>("MonthSort"),
								 YearSort = du == null ? 0 : du.Field<int>("YearSort"),
								 //ds
								 MonthSumScore = ds == null ? 0 : ds.Field<double>("MonthSumScore"),
								 YearSumScore = ds == null ? 0 : ds.Field<double>("YearSumScore"),
								 MonthSumSort = ds == null ? 0 : ds.Field<int>("MonthSumSort"),
								 YearSumSort = ds == null ? 0 : ds.Field<int>("YearSumSort")
							 };

			//注意查看dtUnit\\dtShift 的各字段类型;;;;;

			#endregion

			//返回
			///////////////////////////////////////////////////////////////////////////////////////////
			dtAll = linqresult.ToDataTable();

			return dtAll;
		}


		/// <summary>
		/// 值际总分查询
		/// </summary>
		/// <param name="plantid"></param>
		/// <param name="ecweb"></param>
		/// <param name="QueryTime"></param>
		/// <param name="QueryMoney"></param>
		/// <returns></returns>
		public static DataTable GetRaceForPlantQuery(string plantid, string ecweb, DateTime dtST, DateTime dtET) {
			string SqlText = @"WITH KPIECScore AS(
							SELECT ECShift,A.UNITID,  SUM(ISNULL(A.ECScore,0)) AS UnitScore
							FROM KPI_ECSSArchive a
							LEFT JOIN KPI_ECTag b ON (a.ECID = b.ECID )
							WHERE b.ECIsValid =1 AND B.ECIsDisplay=1 AND A.ECIsRemove=0 {2} AND A.ECTime BETWEEN '{0}' AND '{1}'
							GROUP BY A.UnitID, ECShift)
							SELECT A.ECShift Shift,C.UnitName,ISNULL(A.UnitScore,0)+ISNULL(B.SAScore,0) TotalScore,
								   ISNULL(A.UnitScore,0) ECScore,ISNULL(B.SAScore,0) SAScore
							FROM KPIECScore A LEFT JOIN(
								SELECT SUM(ISNULL(SASCore,0)) SAScore,Shift,B.UnitID FROM KPI_SATagValue A
									 JOIN KPI_SATag B ON A.SAID=B.SAID
								WHERE  A.CalcDateTime BETWEEN  '{0}' AND '{1}'
								GROUP BY Shift,UnitID) B  ON A.ECShift =B.Shift AND A.UnitID=B.UnitID
							JOIN KPI_Unit              C  ON A.UnitID =C.UnitID {3}
							ORDER BY A.ECShift,A.UnitID";
			//order by ECShift, c.UnitName

			string condition1 = "";
			if (!string.IsNullOrEmpty(ecweb)) {
				condition1 = " AND B.ECWeb = '" + ecweb + "'";
			}

			string condition2 = "";
			if (!String.IsNullOrEmpty(plantid)) {
				condition2 = " AND  C.PlantID = '" + plantid + "'";
			}

			////日期
			//string condition3 = " and ECTime>='" + dtST.ToString("yyyy-MM-dd HH:mm:ss")
			//    + "' and ECTime<'" + dtET.ToString("yyyy-MM-dd HH:mm:ss") + "'";

			SqlText = string.Format(SqlText, dtST.ToString("yyyy-MM-dd HH:mm:ss"), 
				dtET.ToString("yyyy-MM-dd HH:mm:ss"), condition1, condition2);
			return DBAccess.GetRelation().ExecuteDataset(SqlText).Tables[0];

		}

		/// <summary>
		/// 值际平均得分查询
		/// </summary>
		/// <param name="plantid"></param>
		/// <param name="ecweb"></param>
		/// <param name="QueryTime"></param>
		/// <param name="QueryMoney"></param>
		/// <returns></returns>
		public static DataTable GetScoreForUnit(string unitid, string ecweb, DateTime dtST, DateTime dtET) {
			////////////////////////////////////////////////////////////            

			//查询总得分
			//            string sql = @"select '1'IID, c.UnitName, ECShift, Sum(a.ECScore) AS TotalScore, Count(a.ECScore) as TotalTime, ''AvgScore, '1'TotalSort
			//                            from KPI_ECSSArchive a
			//                            left outer join KPI_ECTag b on a.ECID = b.ECID 
			//                            right outer join KPI_Unit c on (b.UnitID = c.UnitID {0})
			//                            left outer join KPI_Engunit d on b.EngunitID = d.EngunitID 
			//                            where b.ECIsValid =1 and b.ECIsDisplay=1 and a.ECIsRemove=0 {1} {2}
			//                            Group by UnitName, ECShift
			//                            order by c.UnitName, ECShift ";

//            string sql = @"select '1'IID, UnitName, ECShift, Sum(ECScore) AS TotalScore, sum(cast(IID as int)) as TotalTime, ''AvgScore, '1'TotalSort
//                            from
//                            (
//                                select distinct a.ECTime, '1'IID, c.UnitName, ECShift, Sum(a.ECScore)  AS ECScore
//                                from KPI_ECSSArchive a
//                                left outer join KPI_ECTag b on a.ECID = b.ECID 
//                                right outer join KPI_Unit c on (b.UnitID = c.UnitID {0} )
//                                left outer join KPI_Engunit d on b.EngunitID = d.EngunitID 
//                                where b.ECIsValid =1 and b.ECIsDisplay=1 and a.ECIsRemove=0  {1} {2}
//                                  Group by c.UnitName, ECShift, ECTime
//                            ) ff
//                            Group by UnitName, ECShift
//                            order by UnitName, ECShift ";

//            string condition1 = "";
//            if (unitid != "") {
//                condition1 = " and c.unitid = '" + unitid + "'";
//            }

//            //日期
//            string condition2 = " and ECTime>='" + dtST.ToString("yyyy-MM-dd HH:mm:ss") + "'";
//            string condition3 = " and ECTime<'" + dtET.ToString("yyyy-MM-dd HH:mm:ss") + "'";

			//sql = string.Format(sql, condition1, condition2, condition3);
			String SqlText = @"WITH KPIECScore AS(
								SELECT 1 IID, UnitName, ECShift, SUM(ISNULL(ECScore,0))  ECScore, SUM(IID)  TotalTime, 0.0 AvgScore, 1 TotalSort
								FROM(SELECT a.ECTime, 1IID, c.UnitName, ECShift, Sum(a.ECScore)  AS ECScore
											FROM KPI_ECSSArchive a
											LEFT  JOIN KPI_ECTag b ON a.ECID = b.ECID 
											RIGHT  JOIN KPI_Unit c ON (b.UnitID = c.UnitID)
											LEFT  JOIN KPI_Engunit d ON b.EngunitID = d.EngunitID 
											WHERE b.ECIsValid =1 AND b.ECIsDisplay=1 AND a.ECIsRemove=0 AND A.UnitID='{0}' AND A.ECTime BETWEEN '{1}' AND '{2}'
											GROUP BY c.UnitName, ECShift, ECTime)tt
								Group by UnitName, ECShift)
								SELECT A.*,ISNULL(B.SAScore,0) SAScore,ISNULL(ECScore,0)+ISNULL(SAScore,0) TotalScore FROM KPIECScore A LEFT JOIN 
								(SELECT SUM(ISNULL(SASCore,0)) SAScore,Shift FROM KPI_SATagValue A
								 JOIN KPI_SATag B ON A.SAID=B.SAID
								 WHERE B.UnitID='{0}' AND A.CalcDateTime BETWEEN '{1}' AND '{2}'
								 GROUP BY Shift
								) B ON A.ECShift =B.Shift
								ORDER BY ECShift";
			SqlText = string.Format(SqlText, unitid, dtST.ToString("yyyy-MM-dd HH:mm:ss"), dtET.ToString("yyyy-MM-dd HH:mm:ss"));
			DataTable dt = DBAccess.GetRelation().ExecuteDataset(SqlText).Tables[0];
			//查询总监盘时间
			/////////////////////////////////////////////////////////////////////////
			//监盘时间有问题，是所有指标的次数，应该还除一下 指标的个数。
			/////////////////////////
			//平均得分= 得分/监盘时间/60 (分钟到小时)
			List<KPIVAL> KBS = new List<KPIVAL>();
			//赋值个IID，
			DataRowCollection Rows = dt.Rows;
			for (int i = 0; i < Rows.Count; i++) {
				double avgscore = 0.0;
				if (Rows[i]["TotalScore"] != null
					&& Rows[i]["TotalTime"] != null
					&& Convert.ToDouble(Rows[i]["TotalTime"]) > 0) {
						avgscore = Convert.ToDouble(dt.Rows[i]["TotalScore"])*60 / Convert.ToDouble(dt.Rows[i]["TotalTime"]);
				}
				avgscore = Math.Round(avgscore, 2);
				dt.Rows[i]["IID"] = i;
				dt.Rows[i]["AvgScore"] = avgscore;
				KBS.Add(new KPIVAL(i, avgscore));
			}
			//排序
			KBS.Sort(new KPIVALComparer(KPIVALComparer.CompareType.VAL));
			//反转排序
			KBS.Reverse();
			for (int i = 0; i < KBS.Count; i++) {
				dt.Rows[KBS[i].IID]["TotalSort"] = (i + 1).ToString();
			}
			return dt;
		}


		/// <summary>
		/// 按值分列的报表数据查询
		/// </summary>
		/// <param name="unitid"></param>
		/// <param name="WebCode"></param>
		/// <param name="STTime"></param>
		/// <param name="ETTime"></param>
		/// <returns></returns>
		public static DataTable GetForShiftValue(string unitid, string WebCode, string calctype, DateTime dtST, DateTime dtET) {
			////////////////////////////////////////////////////////////
			//生成统计时间

			//查询得分方法
			//得分、得分率、合格率、最大值、最小值、算数平均值、加权平均值、累计值、累计除法
			//default
            string sql = @"select a.ECName, ECShift+'值' As ECShift, KeyIndex, Sum(ECScore) AS ECScore 
                            from KPI_ECSSArchive
                            right outer join KPI_WebKey b on (a.ECID=b.ECID and b.WebCode='{0}' )
                            where ECIsRemove=0 {1}
                            Group by a.ECName, ECShift, KeyIndex 
                            Order by KeyIndex ";


			switch (calctype) {
				//得分
				case "0":
                    sql = @"select a.ECName, ECShift+'值' As ECShift, KeyIndex,  Sum(ECScore) AS ECScore 
                            from KPI_ECSSArchive a
                            right outer join KPI_WebKey b on (a.ECID=b.ECID and b.WebCode='{0}' )
                            where ECIsRemove=0 {1}
                            Group by a.ECName, ECShift, KeyIndex 
                            Order by KeyIndex ";
					break;

				//得分率
				case "1":
                    sql = @"select a.ECName, ECShift+'值' As ECShift, KeyIndex,  
                            (case ECIsSnapshot when 0 then 0 else 1.0*Sum(a.ECScore)/Count(a.ECScore)/ECWeight end) 'ECScore'
                            from KPI_ECSSArchive a
                            right outer join KPI_WebKey b on (a.ECID=b.ECID and b.WebCode='{0}' )
                            right outer join KPI_ECTag c on (a.ECID=c.ECID)
                            where ECIsRemove=0 {1}
                            Group by a.ECName, ECShift,ECWeight,ECIsSnapshot, KeyIndex 
                            Order by KeyIndex ";


					break;

				//合格率
				case "2":
                    sql = @"select a.ECName, ECShift+'值' As ECShift, KeyIndex,  
                            (case ECIsSnapshot when 0 then 0 else 1.0-Sum(a.ECQulity)/(1.00*Count(a.ECScore)) end) 'ECScore'
                            from KPI_ECSSArchive a
                            right outer join KPI_WebKey b on (a.ECID=b.ECID and b.WebCode='{0}' )
                            right outer join KPI_ECTag c on (a.ECID=c.ECID)
                            where ECIsRemove=0 {1}
                            Group by a.ECName, ECShift, ECIsSnapshot, KeyIndex 
                            Order by KeyIndex ";


					break;

				//最大值
				case "3":
                    sql = @"select a.ECName, ECShift+'值' As ECShift, KeyIndex,  Max(ECValue) AS ECValue 
                            from KPI_ECSSArchive a
                            right outer join KPI_WebKey b on (a.ECID=b.ECID and b.WebCode='{0}' )
                            where ECIsRemove=0 {1}
                            Group by a.ECName, ECShift, KeyIndex 
                            Order by KeyIndex ";
					break;

				//最小值
				case "4":
                    sql = @"select a.ECName, ECShift+'值' As ECShift, KeyIndex,  Min(ECValue) AS ECValue 
                            from KPI_ECSSArchive a
                            right outer join KPI_WebKey b on (a.ECID=b.ECID and b.WebCode='{0}' )
                            where ECIsRemove=0 {1}
                            Group by a.ECName, ECShift, KeyIndex 
                            Order by KeyIndex ";
					break;

				//算数平均值
				case "5":
                    sql = @"select a.ECName, ECShift+'值' As ECShift, KeyIndex,  AVG(ECValue) AS ECValue 
                            from KPI_ECSSArchive a
                            right outer join KPI_WebKey b on (a.ECID=b.ECID and b.WebCode='{0}' )
                            where ECIsRemove=0 {1}
                            Group by a.ECName, ECShift, KeyIndex 
                            Order by KeyIndex ";
					break;

				//加权平均值
				case "6":
                    sql = @"select a.ECName, ECShift+'值' As ECShift, KeyIndex,  AVG(ECValue) AS ECValue 
                            from KPI_ECSSArchive a
                            right outer join KPI_WebKey b on (a.ECID=b.ECID and b.WebCode='{0}' )
                            where ECIsRemove=0 {1}
                            Group by a.ECName, ECShift, KeyIndex 
                            Order by KeyIndex ";
					break;

				//累计值
				case "7":
                    sql = @"select a.ECName, ECShift+'值' As ECShift, KeyIndex,  SUM(ECValue) AS ECValue 
                            from KPI_ECSSArchive a
                            right outer join KPI_WebKey b on (a.ECID=b.ECID and b.WebCode='{0}' )
                            where ECIsRemove=0 {1}
                            Group by a.ECName, ECShift, KeyIndex
                            Order by KeyIndex ";
					break;

				//累计除法
				case "8":
					break;

				default:

					break;
			}

			//分机组
			//机组可以不用判断，因为指标已经获得了限制，不用判断。

			//日期
			string condition = " and a.UnitID='" + unitid + "' and ECTime>='" + dtST.ToString("yyyy-MM-dd HH:mm:ss") + "'" + " and ECTime<'" + dtET.ToString("yyyy-MM-dd HH:mm:ss") + "'";

			sql = string.Format(sql, WebCode, condition);

			DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

			return dt;
		}


		/// <summary>
		/// 按值分列的报表数据查询
		/// </summary>
		/// <param name="unitid"></param>
		/// <param name="WebCode"></param>
		/// <param name="STTime"></param>
		/// <param name="ETTime"></param>
		/// <returns></returns>
		public static DataTable GetForUnitValue(string shitname, string WebCode, string calctype, DateTime dtST, DateTime dtET) {
			////////////////////////////////////////////////////////////
			//生成统计时间

			//查询得分方法
			//得分、得分率、合格率、最大值、最小值、算数平均值、加权平均值、累计值、累计除法
			//default
			string sql = @"select a.ECName, UnitName, KeyIndex, Sum(ECScore) AS ECScore 
                            from KPI_ECSSArchive a
                            right outer join KPI_WebKey b on (a.ECID=b.ECID and b.WebCode='{0}' )
                            right outer join KPI_Unit b on (a.UnitID=b.UnitID)
                            where ECIsRemove=0 {0}
                            Group by a.ECName, ECShift, KeyIndex 
                            Order by KeyIndex ";

			switch (calctype) {
				//得分
				case "0":
					sql = @"select a.ECName, UnitName, KeyIndex,  Sum(ECScore) AS ECScore 
                            from KPI_ECSSArchive a
                            right outer join KPI_WebKey b on (a.ECID=b.ECID and b.WebCode='{0}' )
                            right outer join KPI_Unit c on (a.UnitID=c.UnitID)
                            where ECIsRemove=0 {1}
                            Group by a.ECName, UnitName, KeyIndex 
                            Order by KeyIndex ";
					break;

				//得分率
				case "1":
					sql = @"select a.ECName, UnitName, KeyIndex,  
                            (case ECIsSnapshot when 0 then 0 else 1.0*Sum(a.ECScore)/Count(a.ECScore)/ECWeight end) 'ECScore'
                            from KPI_ECSSArchive a
                            right outer join KPI_WebKey b on (a.ECID=b.ECID and b.WebCode='{0}' )
                            right outer join KPI_ECTag c on (a.ECID=c.ECID)
                            right outer join KPI_Unit d on (a.UnitID=d.UnitID)
                            where ECIsRemove=0 {1}
                            Group by a.ECName, UnitName, ECWeight, ECIsSnapshot, KeyIndex 
                            Order by KeyIndex ";


					break;

				//合格率
				case "2":
					sql = @"select a.ECName, UnitName, KeyIndex,  
                            (case ECIsSnapshot when 0 then 0 else 1.0-Sum(a.ECQulity)/(1.00*Count(a.ECScore)) end) 'ECScore'
                            from KPI_ECSSArchive a
                            right outer join KPI_WebKey b on (a.ECID=b.ECID and b.WebCode='{0}' )
                            right outer join KPI_ECTag c on (a.ECID=c.ECID)
                            right outer join KPI_Unit d on (a.UnitID=d.UnitID)
                            where ECIsRemove=0 {1}
                            Group by a.ECName, UnitName, ECIsSnapshot, KeyIndex 
                            Order by KeyIndex ";


					break;

				//最大值
				case "3":
					sql = @"select a.ECName, UnitName, KeyIndex,  Max(ECValue) AS ECValue 
                            from KPI_ECSSArchive a
                            right outer join KPI_WebKey b on (a.ECID=b.ECID and b.WebCode='{0}' )
                            right outer join KPI_Unit c on (a.UnitID=c.UnitID)
                            where ECIsRemove=0 {1}
                            Group by a.ECName, UnitName, KeyIndex 
                            Order by KeyIndex ";
					break;

				//最小值
				case "4":
					sql = @"select a.ECName, UnitName, KeyIndex,  Min(ECValue) AS ECValue 
                            from KPI_ECSSArchive a
                            right outer join KPI_WebKey b on (a.ECID=b.ECID and b.WebCode='{0}' )
                            right outer join KPI_Unit c on (a.UnitID=c.UnitID)
                            where ECIsRemove=0 {1}
                            Group by a.ECName, UnitName, KeyIndex 
                            Order by KeyIndex ";
					break;

				//算数平均值
				case "5":
					sql = @"select a.ECName, UnitName, KeyIndex,  AVG(ECValue) AS ECValue 
                            from KPI_ECSSArchive a
                            right outer join KPI_WebKey b on (a.ECID=b.ECID and b.WebCode='{0}' )
                            right outer join KPI_Unit c on (a.UnitID=c.UnitID)
                            where ECIsRemove=0 {1}
                            Group by a.ECName, UnitName, KeyIndex 
                            Order by KeyIndex ";
					break;

				//加权平均值
				case "6":
					sql = @"select a.ECName, UnitName, KeyIndex,  AVG(ECValue) AS ECValue 
                            from KPI_ECSSArchive a
                            right outer join KPI_WebKey b on (a.ECID=b.ECID and b.WebCode='{0}' )
                            right outer join KPI_Unit c on (a.UnitID=c.UnitID)
                            where ECIsRemove=0 {1}
                            Group by a.ECName, UnitName, KeyIndex 
                            Order by KeyIndex ";
					break;

				//累计值
				case "7":
					sql = @"select a.ECName, UnitName, KeyIndex, Sum(ECValue) AS ECValue 
                            from KPI_ECSSArchive a
                            right outer join KPI_WebKey b on (a.ECID=b.ECID and b.WebCode='{0}' )
                            right outer join KPI_Unit c on (a.UnitID=c.UnitID)
                            where ECIsRemove=0 {1}
                            Group by a.ECName, UnitName, KeyIndex 
                            Order by KeyIndex ";
					break;

				//累计除法
				case "8":
					break;

				default:

					break;
			}

			//值次
			string condition1 = "";
			if (shitname != "") {
				condition1 = " and ECShift='" + shitname + "' ";
			}

			//日期
			condition1 += "  and ECTime>='" + dtST.ToString("yyyy-MM-dd HH:mm:ss") + "'" + " and ECTime<'" + dtET.ToString("yyyy-MM-dd HH:mm:ss") + "'";

			sql = string.Format(sql, WebCode, condition1);

			DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

			return dt;
		}






		/// <summary>
		/// 平均值报表数据查询
		/// </summary>
		/// <param name="unitid"></param>
		/// <param name="WebCode"></param>
		/// <param name="STTime"></param>
		/// <param name="ETTime"></param>
		/// <returns></returns>
		public static DataTable GetForAvgValue(DateTime dtST, DateTime dtET) {
			//查询得分方法
			//平均值
			//default
			string sql = @"select a.ECCode, a.ECName, a.KeyEngunit, a.KeyDIffMoney, a.KeyOptMoney,
                              a.KeyDesign, AVG(b.ECValue) AS KeyAVG, ''KeyTarget,
                              ''KeyOptDiff, ''KeyOptPercent, ''KeyTarDiff, ''KeyTarPercent, ''KeyMoney,
                              KeyIndex
                              from KPI_AVG a
                              left outer join KPI_ECSSArchive b on (a.ECID=b.ECID )
                              where ECIsRemove=0 {0}
                              Group by a.ECCode, a.ECName, a.KeyEngunit, a.KeyDIffMoney, a.KeyOptMoney, a.KeyDesign, KeyIndex 
                              Order by KeyIndex ";

			//日期
			string condition1 = "  and ECTime>='" + dtST.ToString("yyyy-MM-dd HH:mm:ss") + "'" + " and ECTime<'" + dtET.ToString("yyyy-MM-dd HH:mm:ss") + "'";

			sql = string.Format(sql, condition1);

			DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

			return dt;
		}


	}


	//存储奖金的类
	public class KPIVAL {
		public KPIVAL() {
			this.IID = 0;
			this.VAL = 0;
		}

		public KPIVAL(int iid, double val) {
			this.IID = iid;
			this.VAL = val;
		}

		public int IID {
			get;
			set;
		}

		public double VAL {
			get;
			set;
		}
	}

	public class KPIVALComparer : IComparer<KPIVAL> {
		public enum CompareType {
			IID,
			VAL
		}

		private CompareType type;

		// 构造函数，根据type的值，判断按哪个字段排序
		public KPIVALComparer(CompareType type) {
			this.type = type;
		}

		#region IComparer<KPIVAL> 成员

		public int Compare(KPIVAL x, KPIVAL y) {
			switch (this.type) {
				case CompareType.VAL:
					return x.VAL.CompareTo(y.VAL);
				case CompareType.IID:
					return x.IID.CompareTo(y.IID);

				default:
					return x.VAL.CompareTo(y.VAL);
			}
		}

		#endregion

	}
}

