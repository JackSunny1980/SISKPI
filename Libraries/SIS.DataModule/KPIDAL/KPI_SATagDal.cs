using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SIS.DataEntity;
using SIS.DataAccess;
using SIS.DBControl;
using SIS.Loger;

using System.Runtime.InteropServices;

namespace SIS.DataAccess {

	public class KPI_SATagDal : DalBase<KPI_SATagEntity>, IDisposable {

		private RelaInterface m_DB = DBAccess.GetRelation();
		/// <summary>
		/// 删除数据
		/// </summary>
		/// <returns></returns>
		public static bool DeleteTag(string SAID) {
			//删除参数信息
			//string sql = "delete from KPI_Security ";
			//if (SAID != "")
			//{
			//    sql += " where SAID = '" + SAID + "'";
			//}

			//DBAccess.GetRelation().ExecuteNonQuery(sql);

			//
			string sql = "delete from KPI_SATag ";
			if (SAID != "") {
				sql += " where SAID = '" + SAID + "'";
			}


			return DBAccess.GetRelation().ExecuteNonQuery(sql) > 0;
		}


		/// <summary>
		/// 判断期望值输出测点的唯一性
		/// </summary>
		/// <param name="MainExpectedTagName"></param>
		/// <param name="MainID"></param>
		/// <returns></returns>
		public static bool CodeExist(string SACode, string SAID) {
			string sql = "select count(1) from KPI_SATag where SACode='{0}' ";
			sql = string.Format(sql, SACode);

			if (SAID != "") {
				sql += " and SAID<>'" + SAID + "'";
			}

			return int.Parse(DBAccess.GetRelation().ExecuteScalar(sql).ToString()) > 0;

		}


		/// <summary>
		/// 拷贝实体数据
		/// </summary>
		/// <returns></returns>
		public static bool CopySATag(string SAID) {
			KPI_SATagEntity et = KPI_SATagDal.GetEntity(SAID);

			//NewID
			string newid = Guid.NewGuid().ToString();

			et.SAID = newid;

			et.SACreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
			et.SAModifyTime = et.SACreateTime;

			KPI_SATagDal.Insert(et);

			//得分信息
			string sql = "select ScoreID, SAID from KPI_Score where SAID = '" + SAID + "'";

			DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
			if (dt.Rows.Count <= 0) {
				return true;
			}
			else {
				foreach (DataRow dr in dt.Rows) {
					//
					KPI_SecurityEntity se = KPI_SecurityDal.GetEntity(dr["SecurityID"].ToString());

					se.SecurityID = Guid.NewGuid().ToString();
					se.SAID = newid;

					//se.SecurityCreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
					//se.SecurityModifyTime = se.SecurityCreateTime;

					//KPI_SecurityDal.Insert(se);
				}

			}


			return true;

		}

		//static bool flag = false; 

		/// <summary>
		/// 得到测点配置主表
		/// </summary>
		/// <returns></returns>       
		public static DataTable GetSAs() {
			string sql = @"select SAID[ID], SAName[Name] from KPI_SATag order by SAIndex";

			return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
		}

		/// <summary>
		/// 得到测点配置主表
		/// </summary>
		/// <returns></returns>       
		public static DataTable GetTagLists(string condition) {
			string sql = "select * from KPI_SATag ";

			//用户选择哪个机组进行计算
			if (!condition.Equals("")) {
				sql += " where " + condition;
			}

			//排序，按照输出标签的顺序
			sql += " order by SACode ";

			return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
		}

		/// <summary>
		/// 得到测点配置主表
		/// </summary>
		/// <returns></returns>       
		public static DataTable GetKPIs(string notid) {
			string sql = "select SACode[Code],SACode+'----'+SAName[Name] from KPI_SATag";
			if (notid != "") {
				sql += "where SAID<>'" + notid + "'";
			}

			return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
		}

		/// 得到测点配置主表
		/// </summary>
		/// <returns></returns>       
		public static DataTable GetKPIsLikeName(string notid, string name) {
			string sql = "select SACode[Code],SACode+'----'+SAName[Name] from KPI_SATag where 1=1 ";

			if (notid != "") {
				sql += " and SAID<>'" + notid + "'";
			}

			sql += " and SAName Like '%" + name + "%'";

			return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
		}

		///// <summary>
		///// 通过主键得到实体对象
		///// </summary>
		///// <param name="MainID">主键</param>
		///// <param name="SetSnapShot">是否设置测点实时值</param>
		///// <returns></returns>
		//public static string GetTagID(string TagName, string UnitID)
		//{
		//    KPI_SATagEntity entity = new KPI_SATagEntity();
		//    string sql = "select * from KPI_SATag where TagName='{0}'";
		//    sql = string.Format(sql, TagName);

		//    if (UnitID != "")
		//    {
		//        sql += " and UnitID='" + UnitID + "' ";
		//    }

		//    DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
		//    if (dt.Rows.Count > 0)
		//        entity.DrToMember(dt.Rows[0]);

		//    return entity.TagID;

		//}

		/// <summary>
		/// 通过主键得到实体对象
		/// </summary>
		/// <param name="MainID">主键</param>
		/// <param name="SetSnapShot">是否设置测点实时值</param>
		/// <returns></returns>
		public static KPI_SATagEntity GetEntity(string SAID) {
			KPI_SATagEntity entity = new KPI_SATagEntity();
			string sql = "select * from KPI_SATag where SAID='{0}'";
			sql = string.Format(sql, SAID);

			DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
			if (dt.Rows.Count > 0)
				entity.DrToMember(dt.Rows[0]);

			return entity;

		}


		//        /// <summary>
		//        /// 得到查询结果
		//        /// </summary>
		//        /// <param name="condition">条件字符串</param>
		//        /// <returns></returns>
		//        public static DataTable GetSearchList(string condition)
		//        {
		//            string sql = @"select MainID, UnitName, MainOutTagName, MainOutTagDesc, '' SubTagName,
		//                            MainExpTagName, MainIsValid, MainIsError, '' color, '' AlarmInfo,
		//                            case MainOutTagType when 0 then '定值' when 1 then '开关量' when 2 then '必须量' when 3 then '计算量'  else '其他' end OutTagType,
		//                            case MainIsCheck when 0 then '否' when 1 then '是' else '否' end IsCheck,
		//                            case MainExpModel when 1 then '1-常数' when 2 then '2-曲线' when 3 then '3-测点' when 4 then '4-递推' else '5-无' end ExpModel
		//                            
		//                            from KPI_SATag a
		//                            left outer join OPM_Unit b on a.UnitID=b.UnitID  
		//                            where 1=1 {0} 
		//                            order by MainIsValid DESC, MainOutTagName ";

		//            sql = string.Format(sql, condition);
		//            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
		//            foreach (DataRow dr in dt.Rows)
		//            {
		//                //获得源标签点
		//                dr["SubTagName"] = Filter_TagSubDal.GetConfigTagSubString(dr["MainID"].ToString());

		//                //该记录是否有效
		//                if (dr["MainIsValid"].ToString() == "0")
		//                {
		//                    dr["color"] = "MistyRose";
		//                }                

		//                //该记录是否存在配置缺陷
		//                //if (Filter_AlarmDal.GetLastAlarmInfo(dr["MainID"].ToString()) != "")
		//                //{
		//                //    dr["color"] = "RoyalBlue";
		//                //}
		//                //else
		//                //{

		//                //}

		//                //dr["alarminfo"] = Filter_AlarmDal.GetLastAlarmInfo(dr["MainID"].ToString());
		//            }
		//            return dt;
		//        }


		//        /// <summary>
		//        /// 得到数据校验结果
		//        /// </summary>
		//        /// <param name="condition">条件字符串</param>
		//        /// <returns></returns>
		//        public static DataTable GetSearchList2(string condition)
		//        {
		//            string sql = @" select a.MainID, MainOutTagName, MainOutTagDesc, '' SubTagName, MainOutTagUnit, MainIsValid, 
		//                            ValueTimeStamp, ValueReal, ValueSnapShot, ValueExpected, ValueDeltaAbs, ValueDeltaPer, ValueMW, ''color 
		//                            
		//                            from (select * from KPI_SATag where 1=1 {0}) 
		//                            a  left outer join Filter_TagValue b on a.MainID=b.MainID  
		//                            order by ValueDeltaPer DESC, MainOutTagName";

		//            //case IsAlarm when 1 then '是' else '否' end IsAlarm 
		//            //IsAlarm DESC, 
		//            sql = string.Format(sql, condition);
		//            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
		//            foreach (DataRow dr in dt.Rows)
		//            {
		//                //源标签点信息
		//                dr["SubTagName"] = Filter_TagSubDal.GetConfigTagSubString(dr["MainID"].ToString());

		//                if (dr["ValueDeltaPer"].ToString().Equals(""))
		//                {
		//                    continue;
		//                }

		//                //误差大小
		//                if (Math.Abs(double.Parse(dr["ValueDeltaPer"].ToString()))>=10)
		//                {
		//                    dr["color"] = "Red";
		//                }else if (Math.Abs(double.Parse(dr["ValueDeltaPer"].ToString())) >= 5)
		//                {
		//                    dr["color"] = "Yellow";
		//                }

		//                //该记录是否存在配置缺陷
		//                //if (dr["IsError"].ToString() == "1")
		//                //{
		//                //    dr["color"] = "RoyalBlue";
		//                //}
		//            } 

		//            return dt;
		//        }


		/// <summary>
		/// 得到查询结果
		/// </summary>
		/// <param name="condition">条件字符串</param>
		/// <returns></returns>
		public static DataTable GetSearchList(string unitid, string seqid, string kpiid, string said) {
			string sql = @"select SAID, SACode, SAName, SADesc, b.EngunitName, c.CycleName, SAIndex, 
                            SAWeb, SAIsValid, SAIsCalc, SAIsDisplay, SAIsTotal                                      
                            from KPI_SATag a  
                            left outer join KPI_Engunit b on a.EngunitID = b.EngunitID
                            left outer join KPI_Cycle c on a.CycleID = c.CycleID
                            where 1=1 {0}  
                            order by SAIndex";

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
			if (said != "") {
				condition += " and SAID='" + said + "'";
			}

			sql = string.Format(sql, condition);

			DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

			return dt;
		}


		/// <summary>
		/// 得到查询结果
		/// </summary>
		/// <param name="condition">条件字符串</param>
		/// <returns></returns>
		public static DataTable GetSearchListForSort() {
			string sql = @"select SAID, SACode, SAName, SAIndex from KPI_SATag  order by SAIndex";

			//sql = string.Format(sql, condition);

			return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
		}

		//        /// <summary>
		//        /// 得到测点配置主表
		//        /// </summary>
		//        /// <returns></returns>       
		//        public static DataTable GetTagListForExcel(string UnitID)
		//        {
		//            string sql = @"select 'x'SelectX, TagName, TagDesc, TagType, TagEngunit, TagIsValid, TagIndex,
		//                             TagFilterExp, TagCalcExp, TagCalcExpType, TagCalcType, TagFactor, TagOffset
		//                            from KPI_SATag  
		//                            where UnitID='{0}' 
		//                            order by TagIndex";

		//            sql = string.Format(sql, UnitID);

		//            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

		//            return dt;
		//        }

		public List<KPI_SATagEntity> GetSATagList(string unitID) {
			List<KPI_SATagEntity> Result = null;
			string sqlText = @"SELECT A.* FROM KPI_SATag A  WHERE UnitID='{0}' AND SAIsValid=1";
			sqlText = string.Format(sqlText, unitID);
			using (IDataReader reader = m_DB.ExecuteReader(sqlText)) {
				Result = reader.FillGenericList<KPI_SATagEntity>();
				reader.Close();
			}
			return Result;
		}

		public List<KPI_SATagEntity> GetSATags() {
			List<KPI_SATagEntity> Result = null;
			string sqlText = @"SELECT * FROM KPI_SATag   WHERE SAIsValid=1 ORDER BY UnitID";			
			using (IDataReader reader = m_DB.ExecuteReader(sqlText)) {
				Result = reader.FillGenericList<KPI_SATagEntity>();
				reader.Close();
			}
			return Result;
		}

		public void Dispose() {

		}

	}
}