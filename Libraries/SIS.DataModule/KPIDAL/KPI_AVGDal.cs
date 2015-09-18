using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SIS.DBControl;
using SIS.DataAccess;
using SIS.DataEntity;
 

namespace SIS.DataAccess
{
    public class AVGDal : DalBase<AVGEntity>
    {

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <returns></returns>
        public static bool DeleteKey(string KeyID)
        {
            //删除信息
            string sql = "delete from KPI_AVG where KeyID = '" + KeyID + "'";

            return DBAccess.GetRelation().ExecuteNonQuery(sql)>=1;
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <returns></returns>
        public static bool ClearKeys()
        {
            //删除信息
            string sql = "delete from KPI_AVG";

            return DBAccess.GetRelation().ExecuteNonQuery(sql) >= 1;
        }


        /// <summary>
        /// 得到所有子表数据
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDataTable()
        {
            string sql = "select * from KPI_AVG";
            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }

        /// <summary>
        /// 判断名称的唯一性
        /// </summary>
        /// <param name="GroupName"></param>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public static bool KeyExists(string ECCode)
        {
            string sql = "select count(1) from KPI_AVG where ECCode='{0}' ";
            sql = string.Format(sql, ECCode);
            
            return int.Parse(DBAccess.GetRelation().ExecuteScalar(sql).ToString()) > 0;
        }

        /// <summary>
        /// 得到有效的定义
        /// </summary>
        /// <param name="GroupID">主键</param>
        /// <returns></returns>
        public static DataTable GetKey(string keyid)
        {
            string sql = "select * from KPI_AVG where KeyID='" + keyid + "'";

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }


        
        /// <summary>
        /// 得到有效的定义
        /// </summary>
        /// <param name="GroupID">主键</param>
        /// <returns></returns>
        public static DataTable GetKeys()
        {
            string sql = @"select KeyID, ECName, KeyEngunit, KeyCalcType
                            from KPI_AVG 
                            order by KeyIndex ";

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

        }

        
        /// <summary>
        /// 通过主键得到实体对象
        /// </summary>
        /// <param name="KeyID">主键</param>
        /// <returns></returns>
        public static AVGEntity GetEntity(string KeyID)
        {
            AVGEntity entity = new AVGEntity();

            string sql = "select * from KPI_AVG where KeyID='{0}'";
            sql = string.Format(sql, KeyID);

            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                entity.DrToMember(dt.Rows[0]);
            }

            return entity;
        }

        /// <summary>
        /// 得到查询结果
        /// </summary>
        /// <param name="condition">条件字符串</param>
        /// <returns></returns>
        public static DataTable GetParams()
        {
            string sql = @"select KeyID, a.UnitID, KeyName, ParamTag
                        from KPI_AVG a, Seek_Param b, OPM_Unit c
                        where a.KeyID=b.ParamID and a.UnitID=c.UnitID and KeyIsValid=1
                        group by keyID,a.UnitID,KeyName,ParamTag
                        order by a.UnitID";

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }


        /// <summary>
        /// 得到查询结果
        /// </summary>
        /// <param name="condition">条件字符串</param>
        /// <returns></returns>
        public static DataTable GetKeyList()
        {
            //平均值报表只有此一张。

            string sql = @"select KeyID, ECCode, ECName, KeyEngunit, KeyTarget1, KeyTarget2, keyDesign, keyDiffMoney, KeyOptMoney, KeyIndex
                            from KPI_AVG
                            order by KeyIndex";


            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }

        /// <summary>
        /// 得到查询结果
        /// </summary>
        /// <param name="condition">条件字符串</param>
        /// <returns></returns>
        public static DataTable GetSearchListNot(string webid)
        {
            string sql = @"select ECID[ID], ECCode+'----'+ECName[Name], ECID,  ECCode, ECName, EngunitName
                            from KPI_ECTag a
                            left outer join KPI_Engunit b on a.EngunitID= b.EngunitID
                            where ECID not in ( select ECID from KPI_AVG where WebID='{0}') ";

            sql = string.Format(sql, webid);

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }
        
       
        /// <summary>
        /// 根据机组GUID得到参数列表，并准备接受 实时值、最优值、偏差、煤耗；
        /// 不可控参数
        /// </summary>
        /// <param name="ConfigMainID">机组GUID主键</param>
        /// <returns></returns>
        public static DataTable GetTagListForValue(string WebID, string ValueDate)
        {
            string sql = @"select a.KeyID, c.UnitDesc, a.KeyName, a.KeyEngunit,  
                            ValueDate, ''BKColor, ''BKGood,
                            ValueDayRMW, ''ValueDayS, ValueDayR, ''ValueDayD, ''ValueDayB, 
                            ValueMonthRMW, ''ValueMonthS, ValueMonthR, ''ValueMonthD, ''ValueMonthB, 
                            ''ValueSMonthR,
                            KeyIsCalc, XLineID, XLinePlotType, 
                            XLineType1Unit, XLineType2Unit, XLineType3Unit, XLineType4Unit
                            from KPI_AVG a
                            left join Ana_Value b on (a.KeyID=b.KeyID and ValueDate='{0}')
                            left join OPM_Unit c on a.UnitID=c.UnitID
                            where KeyIsValid=1 and a.WebID='{1}'
                            order by KeyIndex ";

            sql = string.Format(sql, ValueDate, WebID);

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }

        /// <summary>
        /// 根据机组GUID得到参数列表，并准备接受 实时值、最优值、偏差、煤耗；
        /// 不可控参数
        /// </summary>
        /// <param name="ConfigMainID">机组GUID主键</param>
        /// <returns></returns>
        public static DataTable GetKPIListForValue(string WebID, string ValueDate)
        {
            string sql = @"select a.KeyID, c.UnitDesc, a.KeyName, a.KeyEngunit,  
                            ValueDate, ''BKColor, ''BKGood,                            
                            ValueMonthRMW, ValueMonthR, 
                            ''ValueLMonthRMW, ''ValueLMonthR, ''ValueLMonthD, ''ValueLMonthB, 
                            ''ValueSMonthRMW, ''ValueSMonthR, ''ValueSMonthD, ''ValueSMonthB, 
                            KeyIsCalc,XLineType1Unit
                            from KPI_AVG a
                            left join Ana_Value b on (a.KeyID=b.KeyID and ValueDate='{0}')
                            left join OPM_Unit c on a.UnitID=c.UnitID
                            where KeyIsValid=1 and a.WebID='{1}'
                            order by KeyIndex ";

            sql = string.Format(sql, ValueDate, WebID);

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }

        /// <summary>
        /// 根据机组GUID得到参数列表，并准备接受 实时值、最优值、偏差、煤耗；
        /// 不可控参数
        /// </summary>
        /// <param name="ConfigMainID">机组GUID主键</param>
        /// <returns></returns>
        public static DataTable GetHistoryValue(string WebID, string ValueDate)
        {
            string sql = @"select a.KeyID, ValueDate,  ValueMonthRMW, ValueMonthR
                            from KPI_AVG a, Ana_Value b
                            where a.KeyID=b.KeyID and KeyIsValid=1 and a.WebID='{0}' and ValueDate='{1}'
                            order by KeyIndex ";

            sql = string.Format(sql, WebID, ValueDate);

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }

    }
}
