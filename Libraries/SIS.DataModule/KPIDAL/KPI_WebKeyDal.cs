using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SIS.DBControl;
using SIS.DataAccess;
using SIS.DataEntity;
 

namespace SIS.DataAccess
{
    public class KPI_WebKeyDal : DalBase<KPI_WebKeyEntity>
    {

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <returns></returns>
        public static bool DeleteKey(string KeyID)
        {
            //删除信息
            string sql = "delete from KPI_WebKey where KeyID = '" + KeyID + "'";

            return DBAccess.GetRelation().ExecuteNonQuery(sql)>=1;
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <returns></returns>
        public static bool ClearKeys(string WebCode)
        {
            //删除信息
            string sql = "delete from KPI_WebKey where WebCode = '" + WebCode + "'";

            return DBAccess.GetRelation().ExecuteNonQuery(sql) >= 1;
        }


        /// <summary>
        /// 得到所有子表数据
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDataTable()
        {
            string sql = "select * from KPI_WebKey";
            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }

        /// <summary>
        /// 判断名称的唯一性
        /// </summary>
        /// <param name="GroupName"></param>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public static bool KeyExists(string ECCode, string WebCode)
        {
            string sql = "select count(1) from KPI_WebKey where 1=1 and ECCode='{0}' ";
            sql = string.Format(sql, ECCode);

            if (WebCode != "")
                sql = sql + " and WebCode = '" + WebCode + "'";

            return int.Parse(DBAccess.GetRelation().ExecuteScalar(sql).ToString()) > 0;
        }


        /// <summary>
        /// 获得该页面集中的指标计算方法
        /// </summary>
        /// <param name="GroupName"></param>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public static string GetCalcType(string WebCode)
        {
            string sql = "select KeyCalcType from KPI_WebKey where 1=1 and WebCode='{0}' ";
            sql = string.Format(sql, WebCode);

            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["KeyCalcType"].ToString();
            }
            else
            {
                return "0";
            }

        }

        /// <summary>
        /// 得到有效的定义
        /// </summary>
        /// <param name="GroupID">主键</param>
        /// <returns></returns>
        public static DataTable GetKey(string keyid)
        {
            string sql = "select * from KPI_WebKey where KeyID='" + keyid + "'";

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }


        
        /// <summary>
        /// 得到有效的定义
        /// </summary>
        /// <param name="GroupID">主键</param>
        /// <returns></returns>
        public static DataTable GetKeys(string webid)
        {
            string sql = @"select KeyID, ECName, KeyEngunit, KeyCalcType
                            from KPI_WebKey 
                            where 1=1 {0} 
                            order by KeyIndex ";
            string condition = "";
            if (webid != "")
            {
                condition = " and WebID='" + webid + "'";
            }

            sql = string.Format(sql, condition);

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

        }

              
        /// <summary>
        /// 修改参数
        /// </summary>
        /// <param name="KeyID">主键</param>
        /// <param name="value">设定值</param>
        /// <returns></returns>
        public static bool Update(string KeyID, string ECName, string KeyIsValid, string KeyNote)
        {
            KPI_WebKeyEntity mEntity = new KPI_WebKeyEntity();

            mEntity.KeyID = KeyID;
            //mEntity.WebName = strName;
            mEntity.ECName = ECName;
            mEntity.KeyIsValid = int.Parse(KeyIsValid);
            mEntity.KeyNote = KeyNote;

            //mEntity.WebCreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            mEntity.KeyModifyTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");

            return KPI_WebKeyDal.Update(mEntity);
        }


        /// <summary>
        /// 通过主键得到实体对象
        /// </summary>
        /// <param name="KeyID">主键</param>
        /// <returns></returns>
        public static KPI_WebKeyEntity GetEntity(string KeyID)
        {
            KPI_WebKeyEntity entity = new KPI_WebKeyEntity();

            string sql = "select * from KPI_WebKey where KeyID='{0}'";
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
                        from KPI_WebKey a, Seek_Param b, OPM_Unit c
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
        public static DataTable GetKeyList(string WebCode)
        {
            string sql = @"select KeyID, ECCode, ECName, WebCode,  KeyEngunit, KeyCalcType, KeyIndex, KeyIsValid, KeyNote
                            from KPI_WebKey
                            where WebCode = '{0}'
                            order by KeyIndex";
            sql = string.Format(sql, WebCode);

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }

        /// <summary>
        /// 得到查询结果
        /// </summary>
        /// <param name="condition">条件字符串</param>
        /// <returns></returns>
        public static DataTable GetDisplayForSort(string WebCode)
        {
            string sql = @"select KeyID, ECCode, ECName, KeyIndex 
                            from KPI_WebKey 
                            where WebCode = '{0}'
                            order by KeyIndex ";

            sql = string.Format(sql, WebCode);

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
                            where ECID not in ( select ECID from KPI_WebKey where WebID='{0}') ";

            sql = string.Format(sql, webid);

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }


       /// <summary>
        /// 同步
        /// </summary>
        /// <param name="condition">条件字符串</param>
        /// <returns></returns>
        public static bool KeySync(string WebID)
        {
            try
            {
                string sql = @"select KeyID, b.ECID, b.ECCode, b.ECName, c.EngunitName
                            from KPI_WebKey a
                            left outer join KPI_ECTag b on a.ECID = b.ECID
                            left outer join KPI_Engunit c on b.EngunitID= c.EngunitID
                            where WebID='{0}'";

                sql = string.Format(sql, WebID);

                DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string keyid = dt.Rows[i]["KeyID"].ToString();

                    KPI_WebKeyEntity keyE = KPI_WebKeyDal.GetEntity(keyid);

                    keyE.ECCode = dt.Rows[i]["ECCode"].ToString();
                    keyE.ECName = dt.Rows[i]["ECName"].ToString();
                    keyE.KeyEngunit = dt.Rows[i]["EngunitName"].ToString();

                    keyE.KeyModifyTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");

                    KPI_WebKeyDal.Update(keyE);
                }
            }
            catch
            {
                return false;
            }
            return true;
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
                            from KPI_WebKey a
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
                            from KPI_WebKey a
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
                            from KPI_WebKey a, Ana_Value b
                            where a.KeyID=b.KeyID and KeyIsValid=1 and a.WebID='{0}' and ValueDate='{1}'
                            order by KeyIndex ";

            sql = string.Format(sql, WebID, ValueDate);

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }

    }
}
