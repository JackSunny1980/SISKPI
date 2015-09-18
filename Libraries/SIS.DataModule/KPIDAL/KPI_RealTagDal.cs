using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SIS.DataEntity;
using SIS.DataAccess;
using SIS.DBControl;
using SIS.Loger;

using System.Runtime.InteropServices;
using System.Data.SqlClient;

namespace SIS.DataAccess {

    public class KPI_RealTagDal : DalBase<KPI_RealTagEntity> {

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <returns></returns>
        public static bool DeleteTag(string RealID) {
            //删除参数信息
            string sql = "delete from KPI_RealTag ";
            if (RealID != "") {
                sql += " where RealID = '" + RealID + "'";
            }


            return DBAccess.GetRelation().ExecuteNonQuery(sql) > 0;
        }

        //static bool flag = false; 

        /// <summary>
        /// 判断期望值输出测点的唯一性
        /// </summary>
        /// <param name="MainExpectedTagName"></param>
        /// <param name="MainID"></param>
        /// <returns></returns>
        public static bool CodeExist(string RealCode, string RealID) {
            string sql = "select count(1) from KPI_RealTag where RealCode='{0}'";
            sql = string.Format(sql, RealCode);

            if (RealID != "") {
                sql += " and RealID<>'" + RealID + "'";
            }


            return int.Parse(DBAccess.GetRelation().ExecuteScalar(sql).ToString()) > 0;

        }

        /// <summary>
        /// 判断名称的唯一性
        /// </summary>
        /// <param name="SeqName"></param>
        /// <param name="SeqID"></param>
        /// <returns></returns>
        public static int IDCounts(string UnitID) {
            string sql = "select RealID from KPI_RealTag";

            if (UnitID != "") {
                sql += "  where UnitID = '" + UnitID + "'";
            }

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0].Rows.Count;
        }


        /// <summary>
        /// 得到测点ID
        /// </summary>
        /// <returns></returns>       
        public static string GetID(string RealCode) {
            string sql = @"select RealID, RealCode from KPI_RealTag where RealCode='{0}'";

            sql = string.Format(sql, RealCode);

            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
            if (dt.Rows.Count > 0) {
                return dt.Rows[0]["RealID"].ToString();
            }
            else {
                return "";
            }
        }


        /// <summary>
        /// 得到测点Code
        /// </summary>
        /// <returns></returns>       
        public static string GetCode(string RealID) {
            string sql = @"select RealID, RealCode from KPI_RealTag where RealID='{0}'";

            sql = string.Format(sql, RealID);

            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
            if (dt.Rows.Count > 0) {
                return dt.Rows[0]["RealCode"].ToString();
            }
            else {
                return "";
            }
        }

        /// <summary>
        /// 得到测点配置主表
        /// </summary>
        /// <returns></returns>       
        public static DataTable GetTagLists(string UnitID) {
            string sql = @"select UnitName, * 
                            from KPI_RealTag a
                            left outer join KPI_Unit b on a.UnitID=b.UnitID";

            //用户选择哪个机组进行计算
            if (UnitID != "") {
                sql += " where a.UnitID='" + UnitID + "'";
            }

            //排序，按照输出标签的顺序
            //sql += " order by RealCode ";

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }

        /// <summary>
        /// 得到测点配置主表
        /// </summary>
        /// <returns></returns>       
        public static DataTable GetRealTagLists() {
            string sql = @"select RealID, RealCode, RealDesc,RealEngunit, ''RealValue 
                            from KPI_RealTag 
                            where RealDisplay>0
                            order by RealDisplay ";

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }

        /// <summary>
        /// 得到查询结果
        /// </summary>
        /// <param name="condition">条件字符串</param>
        /// <returns></returns>
        public static DataTable GetDisplayForSort() {
            string sql = @"select RealID, RealCode, RealDesc, RealDisplay 
                            from KPI_RealTag 
                            where RealDisplay<>'0'
                            order by RealDisplay";

            //sql = string.Format(sql, condition);

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }

        /// <summary>
        /// 得到用于基准的实时标签点
        /// </summary>
        /// <returns></returns>       
        public static DataTable GetRealXYZLists() {
            string sql = @"select RealCode[ID], RealDesc[Name]
                            from KPI_RealTag 
                            where RealXYZ='1'";

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }

        /// <summary>
        /// 得到测点配置主表
        /// </summary>
        /// <returns></returns>       
        public static DataTable GetTAGs() {
            string sql = @"select RealCode[Code],RealCode+'----'+RealDesc+'--'+RealEngunit [Name]
                            from KPI_RealTag ";

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }

        /// <summary>
        /// 得到测点配置主表
        /// </summary>
        /// <returns></returns>       
        public static DataTable GetTAGsLikeName(string name) {
            string sql = @"select RealCode[Code],RealCode+'----'+RealDesc+'--'+RealEngunit [Name]
                            from KPI_RealTag where RealDesc Like '%{0}%'";
            sql = string.Format(sql, name);

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
        //    KPI_RealTagEntity entity = new KPI_RealTagEntity();
        //    string sql = "select * from KPI_RealTag where TagName='{0}'";
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
        public static KPI_RealTagEntity GetEntity(string RealID) {
            KPI_RealTagEntity entity = new KPI_RealTagEntity();
            string sql = "select * from KPI_RealTag where RealID='{0}'";
            sql = string.Format(sql, RealID);

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
        //                            from KPI_RealTag a
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
        //                            from (select * from KPI_RealTag where 1=1 {0}) 
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
        public static DataTable GetSearchList(string condition) {
            string sql = @"select ECID, ECCode, ECName, ECDesc, b.EngunitName, c.CycleName, ECWeb, ECIndex, ECIsValid, ECIsCalc, ECIsDisplay, ECIsTotal,
                                  ECMaxValue, ECMinValue,  ECIsSnapshot, ECIsSort
                            from KPI_RealTag a  
                            left outer join KPI_Engunit b on a.EngunitID = b.EngunitID
                            left outer join KPI_Cycle c on a.CycleID = c.CycleID
                            where 1=1 {0}  
                            order by ECIndex";

            sql = string.Format(sql, condition);

            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

            return dt;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 获得与主键对应的实体对象
        /// </summary>
        /// <returns></returns>
        public static List<KPI_RealTagEntity> GetAllEntity() {
            List<KPI_RealTagEntity> Result = null;
            string sqlText = @"select * from KPI_RealTag";
            using (IDataReader reader = DBAccess.GetRelation().ExecuteReader(sqlText)) {
                Result = reader.FillGenericList<KPI_RealTagEntity>();
                reader.Close();
            }
            return Result;
            //List<KPI_RealTagEntity> ltReals = new List<KPI_RealTagEntity>();
            //string sql = "select * from KPI_RealTag";
            //DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
            //foreach (DataRow dr in dt.Rows) {
            //    KPI_RealTagEntity entity = new KPI_RealTagEntity();
            //    entity.DrToMember(dr);
            //    ltReals.Add(entity);
            //}
            //return ltReals;
        }

        /// <summary>
        /// 得到测点表
        /// </summary>
        /// <returns></returns>       
        public static DataTable GetRealTagListForExcel() {
            string sql = @"select 'x'SelectX, RealCode, RealDesc, RealEngunit, RealMaxValue, 
                            RealMinValue, RealSnapshot, RealSort, RealDisplay, RealXYZ, RealNote
                            from KPI_RealTag
                            order by RealCode";

            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

            return dt;
        }

        /// <summary>
        /// 获得与主键对应的实体对象
        /// </summary>
        /// <returns></returns>
        public static List<KPI_RealTagEntity> GetRealTags(String UnitID) {
            List<KPI_RealTagEntity> Result = null;
            string sqlText = @"select * from KPI_RealTag WHERE UnitID=@UnitID";
            IDbDataParameter Param = new SqlParameter("@UnitID", UnitID);
            using (IDataReader reader = DBAccess.GetRelation().ExecuteReader(CommandType.Text, sqlText, Param)) {
                Result = reader.FillGenericList<KPI_RealTagEntity>();
                reader.Close();
            }
            return Result;
            //List<KPI_RealTagEntity> ltReals = new List<KPI_RealTagEntity>();
            //string sql = "select * from KPI_RealTag";
            //DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
            //foreach (DataRow dr in dt.Rows) {
            //    KPI_RealTagEntity entity = new KPI_RealTagEntity();
            //    entity.DrToMember(dr);
            //    ltReals.Add(entity);
            //}
            //return ltReals;
        }

    }
}