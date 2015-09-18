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
	public class CurveTagDal : DalBase<CurveTagEntity> {

		/// <summary>
		/// 删除数据
		/// </summary>
		/// <returns></returns>
		public static bool DeleteTag(string CurveID) {
			//
			string sql = "delete from KPI_CurveTag ";
			if (CurveID != "") {
				sql += " where CurveID = '" + CurveID + "'";
			}


			return DBAccess.GetRelation().ExecuteNonQuery(sql) > 0;
		}

		/// <summary>
		/// 判断经济指标代码是否唯一
		/// </summary>
		/// <param name="CurveCode"></param>
		/// <param name="CurveID"></param>
		/// <returns></returns>
		public static bool CodeExist(string CurveCode, string CurveID) {
			string sql = "select count(1) from KPI_CurveTag where CurveCode='{0}' ";
			sql = string.Format(sql, CurveCode);

			if (CurveID != "") {
				sql += " and CurveID<>'" + CurveID + "'";
			}

			return int.Parse(DBAccess.GetRelation().ExecuteScalar(sql).ToString()) > 0;

		}



		/// <summary>
		/// 拷贝实体数据
		/// </summary>
		/// <returns></returns>
		public static bool CopyCurveTag(string CurveID) {
			CurveTagEntity et = CurveTagDal.GetEntity(CurveID);

			//NewID
			string newid = Guid.NewGuid().ToString();

			et.CurveID = newid;
			et.CurveCode = et.CurveCode + "_copy";
			if (ALLDal.CodeExist(et.CurveCode, "")) {
				return false;
			}

			et.CurveName = et.CurveName;

			et.CurveCreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
			et.CurveModifyTime = et.CurveCreateTime;

			CurveTagDal.Insert(et);

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
		public static List<KPI_XLineEntity> GetAllXLineEntity(string ecid, string eccode, string curvegroup, int numbers) {
			//
			//numbers 在查询时为0， 在校验时 为 系数数量。
			List<KPI_XLineEntity> ltxls = new List<KPI_XLineEntity>();

			string sql = @"select CurveID, CurveCode, CurveMonth, CurveType, CurveGetType, CurveXRealTag, CurveYRealTag, CurveZRealTag, CurveXYZ
                            from KPI_CurveTag
                            where CurveIsValid=1 {0}";
			string condition = " and CurveGroup='" + curvegroup + "'";

			sql = string.Format(sql, condition);

			DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

			foreach (DataRow dr in dt.Rows) {
				int xlinetype = int.Parse(dr["CurveType"].ToString());
				int xlinegettype = int.Parse(dr["CurveGetType"].ToString());
				string xlinemonth = dr["CurveMonth"].ToString();

				string xlinexyz = dr["CurveXYZ"].ToString();

				DataTable dtXLine = new DataTable();
				double dout = 0.0;

				bool bResult = GetXLineXYZ(xlinetype, xlinexyz, out dtXLine, out dout);

				if (xlinetype == 0) {
					KPI_XLineEntity entity = new KPI_XLineEntity();
					entity.XLineID = Guid.NewGuid().ToString();
					////////////////////////////////////////////////////////////////
					//notice
					entity.ECID = ecid;
					entity.ECCode = eccode;
					entity.ECCurveGroup = curvegroup;
					entity.XLineMonth = "," + xlinemonth + ",";
					entity.XLineCoef = "a1";
					entity.XLineGet = xlinegettype;
					entity.XLineXBase = "";
					entity.XLineYBase = "";

					entity.XLineX = 0;
					entity.XLineY = 0;
					entity.XLineValue = dout;

					if (numbers > 1) {
						return null;
					}

					ltxls.Add(entity);
				}
				else if (xlinetype == 1) {
					string xrealtag = dr["CurveXRealTag"].ToString();
					//xrealtag = KPI_RealTagDal.GetRealCode(xrealtag);
					//xrealtag = "'" + xrealtag + "'";

					for (int i = 1; i < dtXLine.Rows.Count; i++) {
						for (int j = 1; j < dtXLine.Columns.Count; j++) {
							KPI_XLineEntity entity = new KPI_XLineEntity();
							entity.XLineID = Guid.NewGuid().ToString();
							////////////////////////////////////////////////////////////////
							//notice
							entity.ECID = ecid;
							entity.ECCode = eccode;
							entity.ECCurveGroup = curvegroup;
							entity.XLineMonth = "," + xlinemonth + ",";
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

					if (numbers > (dtXLine.Rows.Count - 1)) {
						return null;
					}

				}
				else if (xlinetype == 2) {
					string xrealtag = dr["CurveXRealTag"].ToString();
					//xrealtag = KPI_RealTagDal.GetRealCode(xrealtag);
					//xrealtag = "'" + xrealtag + "'";

					string yrealtag = dr["CurveYRealTag"].ToString();
					//yrealtag = KPI_RealTagDal.GetRealCode(yrealtag);
					//yrealtag = "'" + yrealtag + "'";

					for (int i = 1; i < dtXLine.Rows.Count; i++) {
						for (int j = 1; j < dtXLine.Columns.Count; j++) {
							KPI_XLineEntity entity = new KPI_XLineEntity();
							entity.XLineID = Guid.NewGuid().ToString();
							////////////////////////////////////////////////////////////////
							//notice
							entity.ECID = ecid;
							entity.ECCode = eccode;
							entity.ECCurveGroup = curvegroup;
							entity.XLineMonth = "," + xlinemonth + ",";
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

					if (numbers > 1) {
						return null;
					}
				}
				break;
			}
			return ltxls;
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
		/// 获得所有经济指标的ID, Name集, 供下拉列表用
		/// </summary>
		/// <returns></returns>       
		public static DataTable GetCurvesByCode() {
			string sql = @"select CurveCode[Code], CurveCode+CurveName[Name] from KPI_CurveTag order by CurveIndex";

			return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
		}

		/// <summary>
		/// 获得所有经济指标的ID, Name集, 供下拉列表用
		/// </summary>
		/// <returns></returns>       
		public static DataTable GetCurvesByGroup() {
			string sql = @"select distinct CurveGroup[Group], CurveGroup[Name] 
                            from KPI_CurveTag 
                            order by [Group] ";

			return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
		}

		/// <summary>
		/// 获得所有经济指标的ID, Name集, 供下拉列表用
		/// </summary>
		/// <returns></returns>       
		public static DataTable GetCurvesByGroup(string group) {
			string sql = @"select distinct CurveGroup, CurveType, CurveGetType,  CurveXRealTag, CurveYRealTag, CurveZRealTag
                            from KPI_CurveTag ";

			return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
		}

		/// <summary>
		/// 获得对应机组ID的所有经济指标的ID, Name集
		/// </summary>
		/// <returns></returns>       
		public static String GetCurveIDByCode(string CurveCode) {
			string sql = @"select CurveID[ID] from KPI_CurveTag where CurveCode='{0}'";
			sql = string.Format(sql, CurveCode);

			DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

			if (dt.Rows.Count == 1) {
				return dt.Rows[0]["ID"].ToString();
			}
			else {
				return "";
			}
		}

		/// <summary>
		/// 获得记录个数
		/// </summary>
		/// <returns></returns>
		public static int CurveIDCounts() {
			string sql = "select CurveID from KPI_CurveTag";

			return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0].Rows.Count;
		}


		/// <summary>
		/// 获得所有经济指标集，返回DataTable
		/// </summary>
		/// <returns></returns>       
		public static DataTable GetTagLists(string condition) {
			string sql = "select * from KPI_CurveTag ";

			//用户选择哪个机组进行计算
			if (!condition.Equals("")) {
				sql += " where " + condition;
			}

			//排序，按照输出标签的顺序
			sql += " order by CurveCreateTime, CurveGroup";

			return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
		}

		/// <summary>
		/// 获得与主键对应的实体对象
		/// </summary>
		/// <param name="CurveID">主键</param>
		/// <returns></returns>
		public static CurveTagEntity GetEntity(string CurveID) {
			CurveTagEntity entity = new CurveTagEntity();

			string sql = "select * from KPI_CurveTag where CurveID='{0}'";
			sql = string.Format(sql, CurveID);

			DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
			if (dt.Rows.Count > 0)
				entity.DrToMember(dt.Rows[0]);

			return entity;

		}

		/// <summary>
		/// 为排序返回所有经济指标
		/// </summary>
		/// <returns></returns>
		public static DataTable GetSearchListForSort() {
			string sql = @"select CurveID, CurveCode, CurveName, CurveIndex from KPI_CurveTag  order by CurveIndex";

			//sql = string.Format(sql, condition);

			return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// 获得与主键对应的实体对象
		/// </summary>
		/// <returns></returns>
		public static List<CurveTagEntity> GetAllEntity() {
			List<CurveTagEntity> Result = null;
			string sqlText = @"select * from KPI_CurveTag";
			using (IDataReader reader = DBAccess.GetRelation().ExecuteReader(sqlText)) {
				Result = reader.FillGenericList<CurveTagEntity>();
				reader.Close();
			}
			return Result;
			//List<KPI_CurveTagEntity> ltCurves = new List<KPI_CurveTagEntity>();
			//string sql = "select * from KPI_CurveTag";
			//DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
			//foreach (DataRow dr in dt.Rows) {
			//    KPI_CurveTagEntity entity = new KPI_CurveTagEntity();
			//    entity.DrToMember(dr);

			//    ltCurves.Add(entity);
			//}
			//return ltCurves;
		}

		/// <summary>
		/// 获得与主键对应的实体对象
		/// </summary>
		/// <returns></returns>
		public static List<CurveTagEntity> GetValidEntity() {
			List<CurveTagEntity> ltCurves = new List<CurveTagEntity>();

			string sql = @"SELECT * FROM KPI_CurveTag WHERE CurveIsValid=1";

			DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
			foreach (DataRow dr in dt.Rows) {
				CurveTagEntity entity = new CurveTagEntity();
				entity.DrToMember(dr);

				ltCurves.Add(entity);
			}

			return ltCurves;
		}

		/// <summary>
		/// 得到测点配置主表
		/// </summary>
		/// <returns></returns>       
		public static DataTable GetTagListForExcel(string UnitID) {
			string sql = @"select 'x'SelectX, CurveCode, CurveName, CurveDesc, CurveGroup, CurveMonth, 
                                   CurveIndex, CurveIsValid, CurveType, CurveGetType, CurveXRealTag, CurveYRealTag, CurveZRealTag,
                                   CurveXYZ, CurveNote
                            from KPI_CurveTag
                            order by CurveIndex";


			return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
		}

	}
}