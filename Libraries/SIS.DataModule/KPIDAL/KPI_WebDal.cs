using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SIS.DBControl;
using SIS.DataAccess;
using SIS.DataEntity;
 

namespace SIS.DataAccess
{
    public class KPI_WebDal : DalBase<KPI_WebEntity>
    {

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <returns></returns>
        public static bool DeleteWebCode(string WebCode)
        {
            /////////////////////////////////////////////////////////////////
            //删除信息
            string sql = "delete from KPI_WebKey ";

            if (WebCode != "")
            {
                sql += " where WebCode = '" + WebCode + "'";
            }

            DBAccess.GetRelation().ExecuteNonQuery(sql);

            /////////////////////////////////////////////////////////////////
            //删除指导信息
            sql = "delete from KPI_Web ";

            if (WebCode != "")
            {
                sql += " where WebCode = '" + WebCode + "'";
            }

            return DBAccess.GetRelation().ExecuteNonQuery(sql)>=1;
        }


        /// <summary>
        /// 得到所有子表数据
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDataTable()
        {
            string sql = "select WebCode, WebDesc, WebType from KPI_Web";
            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }

        /// <summary>
        /// 判断名称的唯一性
        /// </summary>
        /// <param name="GroupName"></param>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public static bool WebCodeExists(string WebCode, string WebID)
        {
            string sql = "select count(1) from KPI_Web where 1=1 and WebCode='{0}' ";
            sql = string.Format(sql, WebCode);

            if (WebID != "")
                sql = sql + " and WebID <> '" + WebID + "'";

            return int.Parse(DBAccess.GetRelation().ExecuteScalar(sql).ToString()) > 0;
        }

        /// <summary>
        /// 得到有效的定义
        /// </summary>
        /// <param name="GroupID">主键</param>
        /// <returns></returns>
        public static DataTable GetKey(string keyid)
        {
            string sql = "select * from KPI_Web where KeyID='" + keyid + "'";

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }

        /// <summary>
        /// 获得当前定义数量
        /// </summary>
        /// <returns></returns>
        public static int WebIDCounts()
        {
            string sql = "select WebID from KPI_Web";

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0].Rows.Count;
        }

        /// <summary>
        /// 得到有效的定义
        /// </summary>
        /// <param name="GroupID">主键</param>
        /// <returns></returns>
        public static string GetWebID(string WebCode)
        {
            string sql = "select WebID from KPI_Web where WebCode='" + WebCode + "'";

            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["WebID"].ToString();
            }
            else
            {
                return "";
            }

        }

        /// <summary>
        /// 得到有效的定义
        /// </summary>
        /// <param name="GroupID">主键</param>
        /// <returns></returns>
        public static string GetWebDesc(string WebCode)
        {
            string sql = "select WebDesc from KPI_Web where WebCode='" + WebCode + "'";

            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["WebDesc"].ToString();
            }
            else
            {
                return "";
            }

        }

        /// <summary>
        /// 得到有效的定义
        /// </summary>
        /// <param name="GroupID">主键</param>
        /// <returns></returns>
        public static DataTable GetWebs(string webid)
        {
            string sql = "select distinct WebID, WebCode from KPI_Web where 1=1 ";
            string condition="";
            if(webid!="")
            {
                condition = " and WebID='" + webid +"'";
            }

            sql += condition;
            
            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

        }

        /// <summary>
        /// 得到有效的定义
        /// </summary>
        /// <param name="GroupID">主键</param>
        /// <returns></returns>
        public static String GetKeyName(string WebID)
        {
            string sql = "select WebCode from KPI_Web where WebID='" + WebID + "'";

            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
            if(dt.Rows.Count != 1)
            {
                return "";
            }else
            {
                return dt.Rows[0]["WebCode"].ToString();
            }
        }
        
        /// <summary>
        /// 修改参数
        /// </summary>
        /// <param name="KeyID">主键</param>
        /// <param name="value">设定值</param>
        /// <returns></returns>
        public static bool Update(string WebID, string WebDesc, string WebType, string WebNote)
        {
            KPI_WebEntity mEntity = new KPI_WebEntity();

            mEntity.WebID = WebID;
            //mEntity.WebCode = strName;
            mEntity.WebDesc = WebDesc;
            mEntity.WebType = int.Parse(WebType);
            mEntity.WebNote = WebNote;

            //mEntity.WebCreateTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            mEntity.WebModifyTime = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");

            return KPI_WebDal.Update(mEntity);
        }


        /// <summary>
        /// 通过主键得到实体对象
        /// </summary>
        /// <param name="KeyID">主键</param>
        /// <returns></returns>
        public static KPI_WebEntity GetEntity(string KeyID)
        {
            KPI_WebEntity entity = new KPI_WebEntity();

            string sql = "select * from KPI_Web where KeyID='{0}'";
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
        public static DataTable GetWebList()
        {
            string sql = @"select WebID, WebCode, WebDesc, WebType, WebNote
                            from KPI_Web ";

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }

        /// <summary>
        /// 得到查询结果
        /// </summary>
        /// <param name="condition">条件字符串</param>
        /// <returns></returns>
        public static DataTable GetAllWebList()
        {
            string sql = @"select WebID, WebCode, WebDesc, WebType, WebNote
                            from KPI_Web 
                            order by WebCreateTime";

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }

        /// <summary>
        /// 得到查询结果
        /// </summary>
        /// <param name="condition">条件字符串</param>
        /// <returns></returns>
        public static DataTable GetWebListbyType(string webtype)
        {
            string sql = @"select WebID, WebCode, WebDesc, WebType, WebNote
                            from KPI_Web 
                            where WebType={0}
                            order by WebCreateTime ";

            sql = string.Format(sql, webtype);

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }

        /// <summary>
        /// 得到查询结果
        /// </summary>
        /// <param name="condition">条件字符串</param>
        /// <returns></returns>
        public static DataTable GetSearchListNot(string condition)
        {
            string sql = @"select ParamID, ParamName, ParamDesc, ParamTag, ParamTagOpt, ParamBTag, ParamQTag, ParamBRate, ParamQRate, ParamEngunit, ParamIsMan
                            from Seek_Param 
                            where ParamID not in ( select KeyID from KPI_Web where 1=1 {0}) 
                            and 1=1 {1}";

            sql = string.Format(sql, condition, condition);

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }
             

        /// <summary>
        /// 根据机组GUID得到参数列表，并准备接受 实时值、最优值、偏差、煤耗；
        /// 不可控参数
        /// </summary>
        /// <param name="ConfigMainID">机组GUID主键</param>
        /// <returns></returns>
        public static DataTable GetTagListForValue(string UnitID)
        {
            string sql = @"select KeyID, KeyName, KeyDesc, KeyEngunit, KeyIsMan, KeyRealTag, KeyOptTag, KeyBTag, KeyQTag, KeyBRate, KeyQRate, 
                             ''BKColor, ''KeyRealValue, ''KeyOptValue, ''KeyDifValue, ''KeyPerValue, ''KeyBValue, ''KeyQValue
                            from KPI_Web 
                            where UnitID='{0}' 
                            order by KeyIsMan ";

            sql = string.Format(sql, UnitID);

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }

    }
}
