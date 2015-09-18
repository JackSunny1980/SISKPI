using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SIS.DBControl;
using SIS.DataAccess;
using SIS.DataEntity;
 

namespace SIS.DataAccess
{
    public class KPI_ShiftDal : DalBase<KPI_ShiftEntity>
    {

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <returns></returns>
        public static bool DeleteShift(string ShiftID)
        {
            //删除参数信息
            string sql = "delete from KPI_Shift ";
            if (ShiftID != "")
            {
                sql += " where ShiftID = '" + ShiftID + "'";
            }

            return DBAccess.GetRelation().ExecuteNonQuery(sql) > 0;
        }


        /// <summary>
        /// 得到所有子表数据
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDataTable()
        {
            string sql = "select * from KPI_Shift";
            DataTable dataTable = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
            return dataTable;
        }


        /// <summary>
        /// 得到所有子表数据
        /// </summary>
        /// <returns></returns>
        public static int ShiftIDCounts()
        {
            string sql = "select ShiftID from KPI_Shift";
            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0].Rows.Count;
        }
        /// <summary>
        /// 判断名称的唯一性
        /// </summary>
        /// <param name="ShiftName"></param>
        /// <param name="ShiftID"></param>
        /// <returns></returns>
        public static bool ShiftNameExists(string ShiftName, string ShiftID)
        {
            string sql = "select count(1) from KPI_Shift where 1=1 and ShiftName='{0}' ";
            sql = string.Format(sql, ShiftName);

            if (ShiftID != "")
                sql = sql + " and ShiftID <> '" + ShiftID + "'";

            return int.Parse(DBAccess.GetRelation().ExecuteScalar(sql).ToString()) > 0;
        }

        /// <summary>
        /// 获得名称
        /// </summary>
        /// <param name="ShiftName"></param>
        /// <param name="ShiftID"></param>
        /// <returns></returns>
        public static string GetShiftName(string ShiftCode)
        {
            string sql = "select ShiftCode, ShiftName from KPI_Shift where ShiftCode='{0}' ";
            sql = string.Format(sql, ShiftCode);

            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

            if(dt==null || dt.Rows.Count <=0)
            {
                return "";
            }else{

                return dt.Rows[0]["ShiftName"].ToString();
            }
        }


        public static string GetNextCode()
        {
            //
            //支持最多26个Code
            //
            string[] AllCodes = new string[26] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

            string sql = "select ShiftCode from KPI_Shift order by ShiftCode";
            int ncount = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0].Rows.Count;

            if (ncount <= 0)
            {
                return "A";
            }
            else if (ncount < 26)
            {
                return AllCodes[ncount];
            }
            else
            {
                //个位，十位，百位：units, tens, hundreds
                int units = 0;
                int tens = 0;

                if (ncount <= 0)
                {
                    //0
                    tens = 0;
                    units = 0;
                }
                else if (ncount % 26 > 0)
                {
                    //不存在十位数变化的情况
                    tens = (int)Math.Floor(ncount / 26.0);
                    units = ncount % 26;
                }
                else if (ncount % 26 == 0)
                {
                    //十位数变化的情况
                    tens = (int)Math.Floor(ncount / 26.0) + 1;
                    units = 0;
                }

                string newcode = AllCodes[tens] + AllCodes[units];

                return newcode;
            }

        }



        /// <summary>
        /// 获得名称
        /// </summary>
        /// <param name="ShiftName"></param>
        /// <param name="ShiftID"></param>
        /// <returns></returns>
        public static string GetShiftID(string ShiftName)
        {
            string sql = "select ShiftID, ShiftName from KPI_Shift where ShiftName='{0}' ";
            sql = string.Format(sql, ShiftName);

            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

            if (dt == null || dt.Rows.Count <= 0)
            {
                return "";
            }
            else
            {
                return dt.Rows[0]["ShiftID"].ToString();
            }
        }

       
        /// <summary>
        /// 得到有效的定义
        /// </summary>
        /// <param name="ShiftID">主键</param>
        /// <returns></returns>
        public static DataTable GetShifts()
        {
            string sql = "select ShiftID[ID], ShiftName[Name]  from KPI_Shift where ShiftIsValid=1";

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }


        /// <summary>
        /// 通过主键得到实体对象
        /// </summary>
        /// <param name="ShiftID">主键</param>
        /// <returns></returns>
        public static KPI_ShiftEntity GetEntity(string ShiftID)
        {
            KPI_ShiftEntity entity = new KPI_ShiftEntity();

            string sql = "select * from KPI_Shift where ShiftID='{0}'";
            sql = string.Format(sql, ShiftID);

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
        public static DataTable GetSearchList(string condition)
        {
            string sql = @"select * from KPI_Shift  
                            where 1=1 {0} order by ShiftCode ";

            sql = string.Format(sql, condition);

            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

            return dt;
        }

    }
}
