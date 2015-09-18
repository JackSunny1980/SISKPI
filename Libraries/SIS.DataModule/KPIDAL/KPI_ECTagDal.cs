using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Runtime.InteropServices;

using SIS.DataEntity;
using SIS.DataAccess;
using SIS.DBControl;
using SIS.Loger;

namespace SIS.DataAccess {
	public class ECTagDal : DalBase<ECTagEntity> {

		/// <summary>
		/// 删除数据
		/// </summary>
		/// <returns></returns>
		public static bool DeleteTag(string ECID) {
			//
			string sql = "delete from KPI_ECTag ";
			if (ECID != "") {
				sql += " where ECID = '" + ECID + "'";
			}


			return DBAccess.GetRelation().ExecuteNonQuery(sql) > 0;
		}


		/// <summary>
		/// 判断经济指标代码是否唯一
		/// </summary>
		/// <param name="ECCode"></param>
		/// <param name="ECID"></param>
		/// <returns></returns>
		public static bool CodeExist(string ECCode, string ECID) {
			string sql = "select count(1) from KPI_ECTag where ECCode='{0}' ";
			sql = string.Format(sql, ECCode);

			if (ECID != "") {
				sql += " and ECID<>'" + ECID + "'";
			}

			return int.Parse(DBAccess.GetRelation().ExecuteScalar(sql).ToString()) > 0;

		}


		/// <summary>
		/// 根据条件删除经济指标集
		/// </summary>
		/// <param name="unitid">机组ID</param>
		/// <param name="seqid">设备集ID</param>
		/// <param name="kpiid">指标集ID</param>
		/// <param name="ecid">指标ID</param>
		/// <returns></returns>
		public static bool DeleteECTagForCondition(string unitid, string seqid, string kpiid, string ecid) {
			string sql = @"delete from KPI_ECTag where 1=1 {0} ";

			string condition = "";
			if (unitid != "") {
				condition += " and UnitID='" + unitid + "'";
			}
			if (seqid != "") {
				condition += " and SeqID='" + seqid + "'";
			}
			if (kpiid != "") {
				condition += " and KpiID='" + kpiid + "'";
			}
			if (ecid != "") {
				condition += " and ECID='" + ecid + "'";
			}

			sql = string.Format(sql, condition);

			return DBAccess.GetRelation().ExecuteNonQuery(sql) > 0;
		}

		/// <summary>
		/// 拷贝实体数据
		/// </summary>
		/// <returns></returns>
		public static bool CopyECTag(string ECID) {
            ECTagEntity et = ECTagDal.GetEntity(ECID);

			//NewID
			string newid = Guid.NewGuid().ToString();

			et.ECID = newid;
			et.ECCode = "Copy_" + et.ECCode;
			if (ALLDal.CodeExist(et.ECCode, "")) {
				return false;
			}

			et.ECName = "Copy_" + et.ECName;

			et.ECCreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
			et.ECModifyTime = et.ECCreateTime;

			ECTagDal.Insert(et);

			return true;

		}

		/// <summary>
		/// 得到区间值,字符串形式
		/// </summary>
		/// <returns></returns>
		public static String SetXLineXYZ(int xlinetype, DataTable dtnew, double dout) {
			string xlinexyz = "";

			if (xlinetype == 0) {
				xlinexyz = dout.ToString() + ";";
			}
			else if (xlinetype == 1) {
				//
				//
				for (int i = 0; i < dtnew.Rows.Count; i++) {
					string xlineexp = "";

					//第一列，index==0都是序号，不用保存
					for (int j = 1; j < dtnew.Columns.Count; j++) {
						xlineexp += dtnew.Rows[i][j].ToString();
						xlineexp += ",";
					}

					//最后一个,替换为;
					//xlineexp.TrimEnd(",".ToCharArray());
					//xlineexp.Remove(xlineexp.Length-1)
					xlinexyz += xlineexp.TrimEnd(",".ToCharArray()) + ";";
				}

			}
			else if (xlinetype == 2) {
				//获得Y列
				string y_xline = "";
				for (int i = 1; i < dtnew.Rows.Count; i++) {
					y_xline += dtnew.Rows[i][0].ToString();
					y_xline += ",";
				}

				//下面统一替换
				//y_xline = y_xline.TrimEnd(",".ToCharArray()) + ";";

				//按X
				for (int i = 0; i < dtnew.Rows.Count; i++) {
					string xlineexp = "";

					if (i == 0) {
						//第一列，index==0都是Y值，前面已处理
						for (int j = 1; j < dtnew.Columns.Count; j++) {
							xlineexp += dtnew.Rows[i][j].ToString();
							xlineexp += ",";
						}

						xlineexp = xlineexp.TrimEnd(",".ToCharArray()) + ";" + y_xline;
					}
					else {
						//第一列，index==0都是Y值，前面已处理
						for (int j = 1; j < dtnew.Columns.Count; j++) {
							xlineexp += dtnew.Rows[i][j].ToString();
							xlineexp += ",";
						}
					}

					//最后一个,替换为;
					//xlineexp.TrimEnd(",".ToCharArray());
					//xlineexp.Remove(xlineexp.Length-1)
					xlinexyz += xlineexp.TrimEnd(",".ToCharArray()) + ";";
				}
			}

			return xlinexyz;
		}


		/// <summary>
		/// 得到区间值,表格形式
		/// </summary>
		/// <returns></returns>
		public static bool GetXLineXYZ(int xlinetype, string xlinexyz, out DataTable dtnew, out double dout) {
			dtnew = new DataTable();
			dout = 0;

			//解析
			//XLineType==0,定值;XLineXYZ=32;
			//XLineType==1,一维;XLineXYZ=175,350;25,26;26,27;
			//XLineType==2,一维;XLineXYZ=175,350;-30,0,30;-98,-98;-87,-87;-70,-70;
			//XLineType==3,三维;暂不适用
			string[] rows = xlinexyz.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

			if (rows.Length == 0) {
				dtnew = GetInitXYZ(xlinetype);

				return false;
			}

			if (xlinetype == 0) {
				dtnew = null;
				dout = double.Parse(rows[0].ToString());

				return true;
			}
			else if (xlinetype == 1) {
				if (rows.Length < 2) {
					return false;
				}

				string[] columns = rows[0].ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

				//生成DataTable表的列。
				dtnew.Columns.Add("aa");
				for (int i = 0; i < columns.Length; i++) {
					dtnew.Columns.Add(i.ToString());
				}

				//添加行。
				for (int i = 0; i < rows.Length; i++) {
					string[] values = rows[i].ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

					DataRow dr = dtnew.NewRow();

					if (i == 0) {
						dr[0] = "基准值";
						for (int j = 0; j < values.Length; j++) {
							dr[j + 1] = values[j];
						}
					}
					else {
						dr[0] = "a" + i.ToString();
						for (int j = 0; j < values.Length; j++) {
							dr[j + 1] = values[j];
						}

					}

					dtnew.Rows.Add(dr);
				}

				return true;
			}
			else if (xlinetype == 2) {
				if (rows.Length < 3) {
					dtnew = null;
					dout = 0;
					return true;
				}

				string[] columns = rows[0].ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
				//第一行为X行
				//第二行为Y列。

				//生成DataTable表的列。
				dtnew.Columns.Add("aa");
				for (int i = 0; i < columns.Length; i++) {
					dtnew.Columns.Add(i.ToString());
				}

				string[] y_valus = rows[1].ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

				//添加行。
				for (int i = 0; i < rows.Length; i++) {
					string[] x_values = rows[i].ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
					if (i == 0) {
						DataRow dr = dtnew.NewRow();
						dr[0] = "基准值";
						for (int j = 0; j < columns.Length; j++) {
							dr[j + 1] = x_values[j];
						}

						dtnew.Rows.Add(dr);
					}
					else if (i == 1) {
						//xyz中存放Y的值
						continue;
					}
					else {
						DataRow dr = dtnew.NewRow();
						dr[0] = y_valus[i - 2];
						for (int j = 0; j < columns.Length; j++) {
							dr[j + 1] = x_values[j];
						}

						dtnew.Rows.Add(dr);
					}

				}

				return true;
			}
			
			return false;
		}


		/// <summary>
		/// 得到区间值,实体形式
		/// </summary>
		/// <returns></returns>
		public static List<KPI_XLineEntity> GetAllXLineEntity() {
			List<KPI_XLineEntity> ltxls = new List<KPI_XLineEntity>();
			string sql = @"SELECT ECID, ECCode, ECXLineType, ECXLineGetType, 
                            ECXLineXRealTag, ECXLineYRealTag, ECXLineZRealTag, ECXLineXYZ, ECCurveGroup
                            FROM KPI_ECTag WHERE ECIsValid=1  ORDER BY ECCode ";
			using (DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0]) {
				foreach (DataRow dr in dt.Rows) {
					string ecid = dr["ECID"].ToString();
					string eccode = dr["ECCode"].ToString();
					int xlinetype = int.Parse(dr["ECXLineType"].ToString());
					int xlinegettype = int.Parse(dr["ECXLineGetType"].ToString());
					string xlinexyz = dr["ECXLineXYZ"].ToString();

					DataTable dtXLine = new DataTable();
					double dout = 0.0;

					//XYZ Tag不加''，因为有默认机组负荷的情况下，为空。

					bool bResult = GetXLineXYZ(xlinetype, xlinexyz, out dtXLine, out dout);

					if (xlinetype == 0) {
						KPI_XLineEntity entity = new KPI_XLineEntity();
						entity.XLineID = Guid.NewGuid().ToString();
						entity.ECID = ecid;
						entity.ECCode = eccode;
						entity.ECCurveGroup = "";
						entity.XLineMonth = "";
						entity.XLineCoef = "a1";
						entity.XLineGet = xlinegettype;
						entity.XLineXBase = "";
						entity.XLineYBase = "";

						entity.XLineX = 0;
						entity.XLineY = 0;
						entity.XLineValue = dout;

						ltxls.Add(entity);
					}
					if (xlinetype == 1) {
						string xrealtag = dr["ECXLineXRealTag"].ToString();
						//xrealtag = KPI_RealTagDal.GetRealCode(xrealtag);
						//xrealtag = "'" + xrealtag + "'";

						for (int i = 1; i < dtXLine.Rows.Count; i++) {
							for (int j = 1; j < dtXLine.Columns.Count; j++) {
								KPI_XLineEntity entity = new KPI_XLineEntity();
								entity.XLineID = Guid.NewGuid().ToString();
								entity.ECID = ecid;
								entity.ECCode = eccode;
								entity.ECCurveGroup = "";
								entity.XLineMonth = "";
								entity.XLineCoef = "a" + i.ToString();
								entity.XLineGet = xlinegettype;
								entity.XLineXBase = xrealtag;
								entity.XLineYBase = "";
								entity.XLineX = double.Parse(dtXLine.Rows[0][j].ToString());
								entity.XLineY = 0;
								entity.XLineValue = double.Parse(dtXLine.Rows[i][j].ToString());

								ltxls.Add(entity);
							}
						}
					}
					if (xlinetype == 2) {
						//可以是默认的机组负荷点，所以为空。
						string xrealtag = dr["ECXLineXRealTag"].ToString();
						//xrealtag = KPI_RealTagDal.GetRealCode(xrealtag);
						//xrealtag = "'" + xrealtag + "'";

						string yrealtag = dr["ECXLineYRealTag"].ToString();
						//yrealtag = KPI_RealTagDal.GetRealCode(yrealtag);
						//yrealtag = "'" + yrealtag + "'";

						for (int i = 1; i < dtXLine.Rows.Count; i++) {
							for (int j = 1; j < dtXLine.Columns.Count; j++) {
								KPI_XLineEntity entity = new KPI_XLineEntity();
								entity.XLineID = Guid.NewGuid().ToString();
								entity.ECID = ecid;
								entity.ECCode = eccode;
								entity.ECCurveGroup = "";
								entity.XLineMonth = "";
								entity.XLineCoef = "a1";
								entity.XLineGet = xlinegettype;
								entity.XLineXBase = xrealtag;
								entity.XLineYBase = yrealtag;
								entity.XLineX = double.Parse(dtXLine.Rows[0][j].ToString());
								entity.XLineY = double.Parse(dtXLine.Rows[i][0].ToString());
								entity.XLineValue = double.Parse(dtXLine.Rows[i][j].ToString());

								ltxls.Add(entity);
							}
						}
					}
					//////////////////////////////////////////////////
					//==3忽略
					if (xlinetype == 4) {
						//在CurveDal中直接生成 XLineEntity
						string curvegroup = dr["ECCurveGroup"].ToString();
						List<KPI_XLineEntity> ltcurve = new List<KPI_XLineEntity>();
						//因为这里不用判断是否正确，故传0进入，判断条件始终不成立即可。
						//numbers
						ltcurve = CurveTagDal.GetAllXLineEntity(ecid, eccode, curvegroup, 0);
						ltxls.AddRange(ltcurve);
					}
				}
			}
			return ltxls;
		}

		/// <summary>
		/// 得到区间值,实体形式
		/// </summary>
		/// <returns></returns>
		public static bool CheckEntityForXline(ECTagEntity ltone) {
			try {
				List<KPI_XLineEntity> ltxls = new List<KPI_XLineEntity>();

				string ecid = ltone.ECID;
				string eccode = ltone.ECCode;
				int xlinetype = ltone.ECXLineType;
				int xlinegettype = ltone.ECXLineGetType;
				string xlinexyz = ltone.ECXLineXYZ;

				string ecscoreexp = ltone.ECScoreExp;

				DataTable dtXLine = new DataTable();
				double dout = 0.0;

				//XYZ Tag不加''，因为有默认机组负荷的情况下，为空。
				int anum = 0;
				if (ecscoreexp.Contains("@a8")) anum += 1;
				if (ecscoreexp.Contains("@a7")) anum += 1;
				if (ecscoreexp.Contains("@a6")) anum += 1;
				if (ecscoreexp.Contains("@a5")) anum += 1;
				if (ecscoreexp.Contains("@a4")) anum += 1;
				if (ecscoreexp.Contains("@a3")) anum += 1;
				if (ecscoreexp.Contains("@a2")) anum += 1;
				if (ecscoreexp.Contains("@a1")) anum += 1;

				bool bResult = GetXLineXYZ(xlinetype, xlinexyz, out dtXLine, out dout);

				if (xlinetype == 0) {
					KPI_XLineEntity entity = new KPI_XLineEntity();
					entity.XLineID = Guid.NewGuid().ToString();
					entity.ECID = ecid;
					entity.ECCode = eccode;
					entity.ECCurveGroup = "";
					entity.XLineMonth = "";
					entity.XLineCoef = "a1";
					entity.XLineGet = xlinegettype;
					entity.XLineXBase = "";
					entity.XLineYBase = "";

					entity.XLineX = 0;
					entity.XLineY = 0;
					entity.XLineValue = dout;

					if (anum > 1) {
						return false;
					}

					ltxls.Add(entity);
				}
				else if (xlinetype == 1) {
					string xrealtag = ltone.ECXLineXRealTag;
					//xrealtag = KPI_RealTagDal.GetRealCode(xrealtag);
					//xrealtag = "'" + xrealtag + "'";


					for (int i = 1; i < dtXLine.Rows.Count; i++) {
						for (int j = 1; j < dtXLine.Columns.Count; j++) {
							KPI_XLineEntity entity = new KPI_XLineEntity();
							entity.XLineID = Guid.NewGuid().ToString();
							entity.ECID = ecid;
							entity.ECCode = eccode;
							entity.ECCurveGroup = "";
							entity.XLineMonth = "";
							entity.XLineCoef = "a" + i.ToString();
							entity.XLineGet = xlinegettype;
							entity.XLineXBase = xrealtag;
							entity.XLineYBase = "";
							entity.XLineX = double.Parse(dtXLine.Rows[0][j].ToString());
							entity.XLineY = 0;
							entity.XLineValue = double.Parse(dtXLine.Rows[i][j].ToString());

							ltxls.Add(entity);
						}
					}

					if (anum > (dtXLine.Rows.Count - 1)) {
						return false;
					}

				}
				else if (xlinetype == 2) {
					//可以是默认的机组负荷点，所以为空。
					string xrealtag = ltone.ECXLineXRealTag;
					//xrealtag = KPI_RealTagDal.GetRealCode(xrealtag);
					//xrealtag = "'" + xrealtag + "'";

					string yrealtag = ltone.ECXLineYRealTag;
					//yrealtag = KPI_RealTagDal.GetRealCode(yrealtag);
					//yrealtag = "'" + yrealtag + "'";

					for (int i = 1; i < dtXLine.Rows.Count; i++) {
						for (int j = 1; j < dtXLine.Columns.Count; j++) {
							KPI_XLineEntity entity = new KPI_XLineEntity();
							entity.XLineID = Guid.NewGuid().ToString();
							entity.ECID = ecid;
							entity.ECCode = eccode;
							entity.ECCurveGroup = "";
							entity.XLineMonth = "";
							entity.XLineCoef = "a1";
							entity.XLineGet = xlinegettype;
							entity.XLineXBase = xrealtag;
							entity.XLineYBase = yrealtag;
							entity.XLineX = double.Parse(dtXLine.Rows[0][j].ToString());
							entity.XLineY = double.Parse(dtXLine.Rows[i][0].ToString());
							entity.XLineValue = double.Parse(dtXLine.Rows[i][j].ToString());

							ltxls.Add(entity);
						}
					}

					if (anum > 1) {
						return false;
					}
				}
				//////////////////////////////////////////////////
				//==3忽略
				else if (xlinetype == 4) {
					//在CurveDal中直接生成 XLineEntity
					string curvegroup = ltone.ECCurveGroup;

					List<KPI_XLineEntity> ltcurve = new List<KPI_XLineEntity>();

					ltcurve = CurveTagDal.GetAllXLineEntity(ecid, eccode, curvegroup, anum);

					if (ltcurve == null) {
						return false;
					}

					ltxls.AddRange(ltcurve);

				}

				return true;
			}
			catch (Exception ) {
				return false;

			}
		}


		/// <summary>
		/// 得到区间值,初始化的
		/// </summary>
		/// <returns></returns>
		public static DataTable GetInitXYZ(int xlinetype) {
			DataTable dtnew = new DataTable();
			if (xlinetype == 0) {
			}
			else if (xlinetype == 1) {
				//生成DataTable表的列。
				dtnew.Columns.Add("aa");
				for (int i = 0; i < 2; i++) {
					dtnew.Columns.Add(i.ToString());
				}

				//添加行。
				DataRow dr = dtnew.NewRow();

				dr[0] = "基准值";
				for (int j = 0; j < 2; j++) {
					dr[j + 1] = "0.00";
				}

				dtnew.Rows.Add(dr);


				dr = dtnew.NewRow();

				dr[0] = "a1";
				for (int j = 0; j < 2; j++) {
					dr[j + 1] = "0.00";
				}

				dtnew.Rows.Add(dr);

			}
			else if (xlinetype == 2) {
				//生成DataTable表的列。
				dtnew.Columns.Add("aa");
				dtnew.Columns.Add("0");

				//添加行。
				DataRow dr = dtnew.NewRow();
				dr[0] = "基准值";
				dr[1] = "0";

				dtnew.Rows.Add(dr);

				dr = dtnew.NewRow();
				dr[0] = "0.00";
				dr[1] = "0.00";

				dtnew.Rows.Add(dr);

			}


			return dtnew;


			//解析
			//XLineType==0,定值;XLineXYZ=32;
			//XLineType==1,一维;XLineXYZ=175,350;25,26;26,27;
			//XLineType==2,一维;XLineXYZ=175,350;-30,0,30;-98,-98;-87,-87;-70,-70;
			//XLineType==3,三维;暂不适用
		}


		/// <summary>
		/// 得到得分计算
		/// </summary>
		/// <returns></returns>
		public static DataTable GetScoreExp(string scoreexp) {
			DataTable dtnew = new DataTable();

			//生成DataTable表的列。
			dtnew.Columns.Add("ScoreID");
			dtnew.Columns.Add("ScoreCalcExp");
			dtnew.Columns.Add("ScoreGainExp");
			dtnew.Columns.Add("ScoreOptimal");
			dtnew.Columns.Add("ScoreAlarm");
			dtnew.Columns.Add("ScoreIsValid");

			//解析
			//计算表达式,得分表达式,是否最优,是否报警,是否有效;
			string[] rows = scoreexp.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            int pos = scoreexp.IndexOf("IFF");
			if ((rows.Length > 0)&&(pos<0)) {

				//添加行。
				for (int i = 0; i < rows.Length; i++) {
					string[] x_values = rows[i].ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

					DataRow dr = dtnew.NewRow();
					dr[0] = i.ToString();
					dr[1] = x_values[0];
					dr[2] = x_values[1];
					dr[3] = x_values[2];
					dr[4] = x_values[3];
					dr[5] = x_values[4];

					dtnew.Rows.Add(dr);
				}
			}

			return dtnew;
		}


		/// <summary>
		/// 得到区间值,实体形式
		/// </summary>
		/// <returns></returns>
		public static List<KPI_ScoreEntity> GetAllScoreEntity() {
			List<KPI_ScoreEntity> Result = new List<KPI_ScoreEntity>();
			string SqlText = @"SELECT ECID, ECScoreExp FROM KPI_ECTag WHERE ECIsValid=1";
			IDataReader Reader = DBAccess.GetRelation().ExecuteReader(SqlText);
			try {
				DataTable dtScore;
				while (Reader.Read()) {
					string ecid = Reader.GetString(0);
					string scoreexp = Reader.GetString(1);
					dtScore = GetScoreExp(scoreexp);
					foreach (DataRow dr in dtScore.Rows) {
						KPI_ScoreEntity entity = new KPI_ScoreEntity();
						entity.ScoreID = Guid.NewGuid().ToString();
						entity.ECID = ecid;
						entity.ScoreCalcExp = dr["ScoreCalcExp"].ToString();
						entity.ScoreGainExp = dr["ScoreGainExp"].ToString();
						entity.ScoreOptimal = int.Parse(dr["ScoreOptimal"].ToString());
						entity.ScoreAlarm = int.Parse(dr["ScoreAlarm"].ToString());
						entity.ScoreIsValid = int.Parse(dr["ScoreIsValid"].ToString());
						Result.Add(entity);
					}
					dtScore.Rows.Clear();
					dtScore.Dispose();
				}
			}
			finally {
				Reader.Close();
				Reader.Dispose();
			}
			return Result;

			//DataTable dt = DBAccess.GetRelation().ExecuteDataset(SqlText).Tables[0];
			//foreach (DataRow dr in dt.Rows) {
			//    string scoreexp = dr["ECScoreExp"].ToString();
			//    string ecid = dr["ECID"].ToString();

			//    DataTable dtScore = GetScoreExp(scoreexp);
			//    for (int i = 0; i < dtScore.Rows.Count; i++) {
			//        KPI_ScoreEntity entity = new KPI_ScoreEntity();
			//        entity.ScoreID = Guid.NewGuid().ToString();
			//        entity.ECID = ecid;
			//        entity.ScoreCalcExp = dtScore.Rows[i]["ScoreCalcExp"].ToString();
			//        entity.ScoreGainExp = dtScore.Rows[i]["ScoreGainExp"].ToString();
			//        entity.ScoreOptimal = int.Parse(dtScore.Rows[i]["ScoreOptimal"].ToString());
			//        entity.ScoreAlarm = int.Parse(dtScore.Rows[i]["ScoreAlarm"].ToString());
			//        entity.ScoreIsValid = int.Parse(dtScore.Rows[i]["ScoreIsValid"].ToString());

			//        lts.Add(entity);
			//    }
			//}
			//return lts;
		}


		/// <summary>
		/// 得到区间值,实体形式
		/// </summary>
		/// <returns></returns>
        public static bool CheckEntityForScore(ECTagEntity ltone) {
			try {
				List<KPI_ScoreEntity> lts = new List<KPI_ScoreEntity>();

				string scoreexp = ltone.ECScoreExp;
				string ecid = ltone.ECID;

				DataTable dtScore = GetScoreExp(scoreexp);

				for (int i = 0; i < dtScore.Rows.Count; i++) {
					KPI_ScoreEntity entity = new KPI_ScoreEntity();
					entity.ScoreID = Guid.NewGuid().ToString();
					entity.ECID = ecid;
					entity.ScoreCalcExp = dtScore.Rows[i]["ScoreCalcExp"].ToString();
					entity.ScoreGainExp = dtScore.Rows[i]["ScoreGainExp"].ToString();
					entity.ScoreOptimal = int.Parse(dtScore.Rows[i]["ScoreOptimal"].ToString());
					entity.ScoreAlarm = int.Parse(dtScore.Rows[i]["ScoreAlarm"].ToString());
					entity.ScoreIsValid = int.Parse(dtScore.Rows[i]["ScoreIsValid"].ToString());

					lts.Add(entity);
				}

				return true;

			}
			catch (Exception ) {
				return false;
			}

		}

		/// <summary>
		/// 得到区间值,实体形式
		/// </summary>
		/// <returns></returns>
        public static bool CheckEntityForOptimal(ECTagEntity ltone) {
			try {
				List<KPI_ScoreEntity> lts = new List<KPI_ScoreEntity>();

				string scoreexp = ltone.ECScoreExp;
				string ecid = ltone.ECID;

				int nOpt = 0;

				DataTable dtScore = GetScoreExp(scoreexp);

				for (int i = 0; i < dtScore.Rows.Count; i++) {
					KPI_ScoreEntity entity = new KPI_ScoreEntity();
					entity.ScoreID = Guid.NewGuid().ToString();
					entity.ECID = ecid;
					entity.ScoreCalcExp = dtScore.Rows[i]["ScoreCalcExp"].ToString();
					entity.ScoreGainExp = dtScore.Rows[i]["ScoreGainExp"].ToString();
					entity.ScoreOptimal = int.Parse(dtScore.Rows[i]["ScoreOptimal"].ToString());
					entity.ScoreAlarm = int.Parse(dtScore.Rows[i]["ScoreAlarm"].ToString());
					entity.ScoreIsValid = int.Parse(dtScore.Rows[i]["ScoreIsValid"].ToString());

					nOpt += entity.ScoreOptimal;

					lts.Add(entity);
				}

				//
				if (ltone.ECIsSnapshot == 1 && (nOpt <= 0 || nOpt > 1)) {
					return false;
				}

				return true;

			}
			catch (Exception ) {
				return false;
			}

		}


		/// <summary>
		/// 得到区间值,初始化的
		/// </summary>
		/// <returns></returns>
		public static DataTable GetInitScore() {
			DataTable dtnew = new DataTable();

			//生成DataTable表的列。
			dtnew.Columns.Add("ScoreID");
			dtnew.Columns.Add("ScoreCalcExp");
			dtnew.Columns.Add("ScoreGainExp");
			dtnew.Columns.Add("ScoreOptimal");
			dtnew.Columns.Add("ScoreAlarm");
			dtnew.Columns.Add("ScoreIsValid");

			return dtnew;
		}



		/// <summary>
		/// 获得所有经济指标的ID, Name集, 供下拉列表用
		/// </summary>
		/// <returns></returns>       
		public static DataTable GetECs() {
			string sql = @"select ECID[ID], ECName[Name] from KPI_ECTag order by ECIndex";

			return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
		}

		/// <summary>
		/// 获得对应机组ID的所有经济指标的ID, Name集
		/// </summary>
		/// <returns></returns>       
		public static DataTable GetECByUnit(string UnitID) {
			string sql = @"select ECID[ID], ECName[Name] from KPI_ECTag where UnitID='{0}' order by ECIndex";
			sql = string.Format(sql, UnitID);

			return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
		}

		/// <summary>
		/// 获得对应机组ID的所有经济指标的ID, Name集
		/// </summary>
		/// <returns></returns>       
		public static String GetECIDByCode(string ECCode) {
			string sql = @"select ECID[ID] from KPI_ECTag where ECCode='{0}'";
			sql = string.Format(sql, ECCode);

			DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

			if (dt.Rows.Count == 1) {
				return dt.Rows[0]["ID"].ToString();
			}
			else {
				return "";
			}
		}

		/// <summary>
		/// 获得所有经济指标集，返回DataTable
		/// </summary>
		/// <returns></returns>       
		public static DataTable GetTagLists(string condition) {
			string sql = "select * from KPI_ECTag ";

			//用户选择哪个机组进行计算
			if (!condition.Equals("")) {
				sql += " where " + condition;
			}

			//排序，按照输出标签的顺序
			sql += " order by ECCode ";

			return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
		}

		/// <summary>
		/// 获得不包括自身的经济指标集, ECCode及ECName, 供算法配置界面使用
		/// </summary>
		/// <returns></returns>       
		public static DataTable GetKPIs(string notid) {
			//不等于：数字比较时 <>
			//       文字比较时 is not
			string sql = "select ECCode[Code],ECCode+'---'+ECName[Name] from KPI_ECTag";
			if (notid != "") {
				sql += " where ECID <> '" + notid + "'";
			}

			return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
		}

		/// <summary>
		/// 获得不包括自身、及按名称查询的经济指标集, ECCode及ECName, 供查询算法配置界面使用
		/// </summary>
		/// <param name="notid"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		public static DataTable GetKPIsLikeName(string notid, string name) {
			string sql = "select ECCode[Code],ECCode+'---'+ECName[Name] from KPI_ECTag where 1=1 ";

			if (notid != "") {
				sql += " and ECID<>'" + notid + "'";
			}

			sql += " and ECName Like '%" + name + "%'";

			return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
		}


		/// <summary>
		/// 根据条件查询经济指标集
		/// </summary>
		/// <param name="unitid">机组ID</param>
		/// <param name="seqid">设备集ID</param>
		/// <param name="kpiid">指标集ID</param>
		/// <param name="ecid">指标ID</param>
		/// <returns></returns>
		public static String GetNameEngunit(string ECCode) {
			string sql = @"select ECCode, ECName, ECDesc, b.EngunitName
                            from KPI_ECTag a  
                            left outer join KPI_Engunit b on a.EngunitID = b.EngunitID
                            where 1=1 {0}  
                            order by ECIndex";

			string condition = "";
			if (ECCode != "") {
				condition += " and ECCode='" + ECCode + "'";
			}

			sql = string.Format(sql, condition);

			DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

			if (dt.Rows.Count == 1) {
				return dt.Rows[0]["ECName"].ToString() + "," + dt.Rows[0]["EngunitName"].ToString();
			}

			return "";
		}


		/// <summary>
		/// 获得与主键对应的实体对象
		/// </summary>
		/// <param name="ECID">主键</param>
		/// <returns></returns>
        public static ECTagEntity GetEntity(string ECID) {
            ECTagEntity entity = new ECTagEntity();
			string sql = "select * from KPI_ECTag where ECID='{0}'";
			sql = string.Format(sql, ECID);
			DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
			if (dt.Rows.Count > 0)
				entity.DrToMember(dt.Rows[0]);

			return entity;

		}


		/// <summary>
		/// 根据条件查询经济指标集
		/// </summary>
		/// <param name="unitid">机组ID</param>
		/// <param name="seqid">设备集ID</param>
		/// <param name="kpiid">指标集ID</param>
		/// <param name="ecid">指标ID</param>
		/// <returns></returns>
		public static DataTable GetSearchList(string unitid, string seqid, string kpiid, string ecid) {
			string sql = @"select ECID, ECCode, ECName, ECDesc, b.EngunitName, c.CycleName, ECWeb, ECIndex, ECIsValid, ECIsCalc, ECIsAsses, ECIsZero, ECIsDisplay, ECIsTotal,
                                  ECMaxValue, ECMinValue, ECWeight, ECIsSnapshot, ECIsSort
                            from KPI_ECTag a  
                            left outer join KPI_Engunit b on a.EngunitID = b.EngunitID
                            left outer join KPI_Cycle c on a.CycleID = c.CycleID
                            where 1=1 {0}  
                            order by ECIndex";

			string condition = "";
			if (unitid != "") {
				condition += " and UnitID='" + unitid + "'";
			}
			if (seqid != "") {
				condition += " and SeqID='" + seqid + "'";
			}
			if (kpiid != "") {
				condition += " and KpiID='" + kpiid + "'";
			}
			if (ecid != "") {
				condition += " and ECID='" + ecid + "'";
			}

			sql = string.Format(sql, condition);

			DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

			return dt;
		}


		/// <summary>
		/// 为排序返回所有经济指标
		/// </summary>
		/// <returns></returns>
		public static DataTable GetSearchListForSort() {
			string sql = @"select ECID, ECCode, ECName, ECIndex from KPI_ECTag  order by ECIndex";

			//sql = string.Format(sql, condition);

			return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// 获得与主键对应的实体对象
		/// </summary>
		/// <returns></returns>
        public static List<ECTagEntity> GetAllEntity() {
            List<ECTagEntity> ltECs = new List<ECTagEntity>();

			string sql = "select * from KPI_ECTag";

			DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
			foreach (DataRow dr in dt.Rows) {
                ECTagEntity entity = new ECTagEntity();
				entity.DrToMember(dr);

				ltECs.Add(entity);
			}

			return ltECs;
		}

		/// <summary>
		/// 获得与主键对应的实体对象
		/// </summary>
		/// <returns></returns>
        public static List<ECTagEntity> GetValidEntity() {
            List<ECTagEntity> Result = null;
			string sqlText = @"select * from KPI_ECTag where ECIsValid=1";
			using (IDataReader reader = DBAccess.GetRelation().ExecuteReader(sqlText)) {
                Result = reader.FillGenericList<ECTagEntity>();
				reader.Close();
			}
			return Result;
			//List<KPI_ECTagEntity> ltECs = new List<KPI_ECTagEntity>();
			//string sql = @"select * from KPI_ECTag  where ECIsValid=1";
			//DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
			//foreach (DataRow dr in dt.Rows) {
			//    KPI_ECTagEntity entity = new KPI_ECTagEntity();
			//    entity.DrToMember(dr);

			//    ltECs.Add(entity);
			//}
			//return ltECs;
		}

		/// <summary>
		/// 得到测点配置主表
		/// </summary>
		/// <returns></returns>       
		public static DataTable GetTagListForExcel(string UnitID) {
			string sql = @"select 'x'SelectX, ECCode, ECName, ECDesc, UnitName, SeqName, KpiName, EngunitName, CycleName,
                                   ECIndex, ECWeb, ECIsValid, ECIsCalc, ECIsAsses, ECIsZero, ECIsDisplay, ECIsTotal, 
                                   ECDesign, ECOptimal, ECMaxValue, ECMinValue, ECWeight, ECDenom, ECCalcClass,
                                   ECFilterExp, ECCalcExp, ECCalcDesc, 
                                   ECIsSnapshot, ECXLineType, ECXLineGetType, ECXLineXRealTag, ECXLineYRealTag, ECXLineZRealTag,
                                   ECXLineXYZ,ECScoreExp,ECCurveGroup,
                                   ECIsSort, ECType, ECSort, ECScore, ECExExp, ECExScore, ECNote
                            from KPI_ECTag a
                            left outer join KPI_Unit b on a.UnitID = b.UnitID
                            left outer join KPI_Seq c on a.SeqID = c.SeqID
                            left outer join KPI_Kpi d on a.KpiID = d.KpiID
                            left outer join KPI_Engunit e on a.EngunitID = e.EngunitID
                            left outer join KPI_Cycle f on a.CycleID = f.CycleID 
                            where 1=1 {0}
                            order by a.UnitID, ECCode";

			if (UnitID != "") {
				sql = string.Format(sql, " and a.UnitID='" + UnitID + "'");
			}
			else {
				sql = string.Format(sql, "");
			}

			return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
		}

	}
}