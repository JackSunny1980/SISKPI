using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SIS.DBControl;
using SIS.DataAccess;
using SIS.DataEntity;
using System.Data.SqlClient;


namespace SIS.DataAccess {

    public class KPI_ConstantDal : DalBase<ConstantEntity> {


        #region Added by pyf 20150126

        public bool SaveConstant(ConstantEntity Constant) {
            if (Exists(Constant)) {
                return Update(Constant);
            }
            else {
                Constant.ConstantID = GetConstantID();
                Constant.ConstantValue = GetConstantValue();
                return Insert(Constant);
            }
        }

        public bool DeleteConstant(ConstantEntity Constant) {
            return true;
        }

        private bool Exists(ConstantEntity Constant) {
            string SqlText = "SELECT COUNT(ConstantID) FROM  KPI_Constant WHERE ConstantID=@ConstantID ";
            IDbDataParameter[] Parameters = new SqlParameter[] { 
                new SqlParameter("@ConstantID",SqlDbType.VarChar)};
            Parameters[0].Value = Constant.ConstantID;
            int count = (int)DBAccess.GetRelation().ExecuteScalar(SqlText, Parameters);
            return count > 0;
        }

        private String GetConstantID() {
            string SqlText = @"SELECT ISNULL(MAX(CONVERT(INT,ConstantID)),0)+1 FROM KPI_Constant WHERE ConstantCode='M'";
            return DBAccess.GetRelation().ExecuteScalar(SqlText) + "";
        }

        private String GetConstantValue() {
            string SqlText = @"SELECT ISNULL(MAX(CONVERT(INT,ConstantValue)),0)+1 FROM KPI_Constant WHERE ConstantCode='M'";
            return DBAccess.GetRelation().ExecuteScalar(SqlText) + ""; ;
        }

        #endregion

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <returns></returns>
        public static bool DeleteConstant(string ConstantID) {
            //删除参数信息
            string sql = "delete from KPI_Constant ";
            if (ConstantID != "") {
                sql += " where ConstantID = '" + ConstantID + "'";
            }
            return DBAccess.GetRelation().ExecuteNonQuery(sql) > 0;
        }

        /// <summary>
        /// 判断名称的唯一性
        /// </summary>
        /// <param name="SeqName"></param>
        /// <param name="SeqID"></param>
        /// <returns></returns>
        public static int ConstantIDCounts() {
            string sql = "select ConstantID from KPI_Constant";

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0].Rows.Count;
        }

        /// <summary>
        /// 得到所有子表数据
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDataTable() {
            string sql = "select * from KPI_Constant";
            DataTable dataTable = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
            return dataTable;
        }

        /// <summary>
        /// 判断名称的唯一性
        /// </summary>
        /// <param name="ConstantName"></param>
        /// <param name="ConstantID"></param>
        /// <returns></returns>
        public static bool ConstantNameExists(string ConstantName, string ConstantCode) {
            string sql = "select count(1) from KPI_Constant where 1=1 and ConstantName='{0}' ";
            sql = string.Format(sql, ConstantName);

            if (ConstantCode != "") {
                sql = sql + " and ConstantCode='" + ConstantCode + "'";
            }

            return int.Parse(DBAccess.GetRelation().ExecuteScalar(sql).ToString()) > 0;
        }

        /// <summary>
        /// 获得所有电厂ID、Name
        /// </summary>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public static DataTable GetConstants() {
            string sql = "select ConstantID[ID], ConstantName[Name] from KPI_Constant";

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }

        /// <summary>
        /// 获得所有电厂ID、Name
        /// </summary>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public static DataTable GetECWebs() {
            string sql = @"select ConstantName[Name], ConstantValue[Value]
                                  from KPI_Constant 
                                  where ConstantCode='kpiecweb'";

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }


        /// <summary>
        /// 获得所有电厂ID、Name
        /// </summary>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public static DataTable GetSAWebs() {
            string sql = @"select ConstantName[Name], ConstantValue[Value]
                                  from KPI_Constant 
                                  where ConstantCode='kpisaweb'";

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }

        /// <summary>
        /// 获得web的统计周期
        /// </summary>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public static string GetECWebQueryTime(string ConstantCode) {
            string sql = @"select ConstantValue
                                  from KPI_Constant 
                                  where ConstantCode='{0}'";

            sql = string.Format(sql, ConstantCode);

            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
            if (dt.Rows.Count == 1) {
                return dt.Rows[0]["ConstantValue"].ToString();
            }
            else {
                return "1,1";
            }
        }

        /// <summary>
        /// 获得所有电厂ID、Name
        /// </summary>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public static string GetConstantID(string ConstantName) {
            string sql = "select ConstantID[ID] from KPI_Constant where ConstantName='" + ConstantName + "'";

            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];

            if (dt.Rows.Count != 1) {
                return "";
            }
            else {
                return dt.Rows[0]["ID"].ToString();
            }


        }


        /// <summary>
        /// 通过主键得到实体对象
        /// </summary>
        /// <param name="ConstantID">主键</param>
        /// <returns></returns>
        public static ConstantEntity GetEntity(string ConstantID) {
            ConstantEntity entity = new ConstantEntity();
            string sql = "select * from KPI_Constant where ConstantID='{0}'";
            sql = string.Format(sql, ConstantID);
            DataTable dt = DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
            if (dt.Rows.Count > 0) {
                entity.DrToMember(dt.Rows[0]);
            }

            return entity;
        }


        /// <summary>
        /// 得到查询结果
        /// </summary>
        /// <param name="condition">条件字符串</param>
        /// <returns></returns>
        public static DataTable GetConstantList() {
            string sql = @"select ConstantID, ConstantCode, ConstantName, ConstantDesc, ConstantValue, ConstantNote 
                            from KPI_Constant ";

            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }

        public static DataTable GetManagementTags() {
            string sql = @"select ConstantID, ConstantCode, ConstantName, ConstantDesc, ConstantValue, ConstantNote 
                            from KPI_Constant WHERE ConstantCode='M' ";
            return DBAccess.GetRelation().ExecuteDataset(sql).Tables[0];
        }

    }
}
