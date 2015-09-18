using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SIS.DataEntity;
using SIS.DataAccess;
using SIS.DBControl;
using SIS.Loger;

using System.Runtime.InteropServices;

namespace SIS.DataAccess
{
    public class KPI_RaceTagDal : DalBase<KPI_RaceTagEntity>  
    {
        //static bool flag = false; 
   
        /// <summary>
        /// 判断期望值输出测点的唯一性
        /// </summary>
        /// <param name="MainExpectedTagName"></param>
        /// <param name="MainID"></param>
        /// <returns></returns>
        public static bool TagNameExist(string TagName)
        {
            string sql = "select count(1) from Race_Tag where TagName='{0}' ";
            sql = string.Format(sql, TagName);

            return int.Parse(DBAccess.GetRelation().ExecuteScalar(sql).ToString()) > 0;

        }

        /// <summary>
        /// 得到测点配置主表
        /// </summary>
        /// <returns></returns>       
        public static DataTable GetTags(string UnitID)
        {
            string sql = @"select TagID, TagName, TagDesc, TagEngunit from Race_Tag where 1=1 {0} order by TagIndex ";

            if (UnitID != "")
            {
                sql = string.Format(sql, " and UnitID='" + UnitID + "'");
            }

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }

        /// <summary>
        /// 得到测点配置主表
        /// </summary>
        /// <returns></returns>       
        public static DataTable GetTagLists(string condition)
        {
            string sql = "select * from Race_Tag ";
            
            //用户选择哪个机组进行计算
            if (!condition.Equals(""))
            {
                sql += " where " + condition;
            }

            //排序，按照输出标签的顺序
            sql += " order by TagName ";

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }

        /// <summary>
        /// 通过主键得到实体对象
        /// </summary>
        /// <param name="MainID">主键</param>
        /// <param name="SetSnapShot">是否设置测点实时值</param>
        /// <returns></returns>
        public static string GetTagID(string TagName, string UnitID)
        {
            KPI_RaceTagEntity entity = new KPI_RaceTagEntity();
            string sql = "select * from Race_Tag where TagName='{0}'";
            sql = string.Format(sql, TagName);

            if (UnitID != "")
            {
                sql += " and UnitID='" + UnitID + "' ";
            }

            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
            if (dt.Rows.Count > 0)
                entity.DrToMember(dt.Rows[0]);

            return entity.TagID;

        }

        /// <summary>
        /// 通过主键得到实体对象
        /// </summary>
        /// <param name="MainID">主键</param>
        /// <param name="SetSnapShot">是否设置测点实时值</param>
        /// <returns></returns>
        public static KPI_RaceTagEntity GetEntity(string TagID)
        {
            KPI_RaceTagEntity entity = new KPI_RaceTagEntity();
            string sql = "select * from Race_Tag where TagID='{0}'";
            sql = string.Format(sql, TagID);

            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
            if (dt.Rows.Count > 0)
                entity.DrToMember(dt.Rows[0]);

            return entity;

        }
        
        /// <summary>
        /// 通过主键得到实体对象
        /// </summary>
        /// <param name="MainID">主键</param>
        /// <param name="SetSnapShot">是否设置测点实时值</param>
        /// <returns></returns>
        public static bool Delete(string TagID)
        {
            KPI_RaceTagEntity entity = new KPI_RaceTagEntity();
            string sql = "select * from Race_Tag where TagID='{0}'";
            sql = string.Format(sql, TagID);

            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
            if (dt.Rows.Count > 0)
                entity.DrToMember(dt.Rows[0]);

            return KPI_RaceTagDal.Delete(entity);
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
//                            from Race_Tag a
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
//                            from (select * from Race_Tag where 1=1 {0}) 
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
        public static DataTable GetSearchList3(string condition)
        {
            string sql = @"select TagID, UnitName, UnitDesc, TagName, TagDesc, TagType, TagEngunit, TagIsValid, TagIndex,
                                    TagFilterExp, TagCalcExp, TagCalcExpType, TagCalcType, TagFactor, TagOffset
                            from Race_Tag  
                            a left outer join OPM_Unit b on a.UnitID = b.UnitID
                            where 1=1 {0}  
                            order by TagIndex";

            sql = string.Format(sql, condition);

            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
            
            return dt;
        }


        /// <summary>
        /// 得到测点配置主表
        /// </summary>
        /// <returns></returns>       
        public static DataTable GetTagListForExcel(string UnitID)
        {
            string sql = @"select 'x'SelectX, TagName, TagDesc, TagType, TagEngunit, TagIsValid, TagIndex,
                             TagFilterExp, TagCalcExp, TagCalcExpType, TagCalcType, TagFactor, TagOffset
                            from Race_Tag  
                            where UnitID='{0}' 
                            order by TagIndex";

            sql = string.Format(sql, UnitID);

            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

            return dt;
        }

    }
}  