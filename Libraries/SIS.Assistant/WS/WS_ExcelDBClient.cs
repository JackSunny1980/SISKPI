using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;

using SIS.Loger;
using SIS.DataAccess;
using SIS.DataEntity;
using SIS.DBControl;

using System.Data.OleDb;
using System.Data.SqlClient;
//using Oracle.DataAccess.Client;


namespace SIS.Assistant.WS
{
    /// <summary>
    /// 用于百万机组竞赛，从Excel中读取点清单然后根据点清单从PI数据库读取数据写入关系数据库
    /// </summary>
    public class WS_ExcelDBClient
    {
        public static string DBType;
        public static SqlConnection sqlConn;
        //public static OracleConnection oraConn;
        public static string connecionString;
        public static string BulkTable;

        static WS_ExcelDBClient()
        {
            DBType = System.Configuration.ConfigurationManager.AppSettings["Relation"].ToString().ToUpper();

            connecionString = System.Configuration.ConfigurationManager.AppSettings["connectionString"].ToString();

            if (DBType == "SQL")
            {
                sqlConn = new SqlConnection(connecionString);
            }
            else if (DBType == "ORACLE")
            {
                //oraConn = new OracleConnection(connecionString);
            }

            BulkTable = System.Configuration.ConfigurationManager.AppSettings["BulkTable"].ToString();
        }

        #region DataBase 连接与关闭

        public static bool Connection()
        {
            try
                {
                if (DBType == "SQL")
                {
                    SQLConnection();
                }
                else if (DBType == "ORACLE")
                {
                    ORAConnection();
                }

                return true;
            }
            catch (System.Exception ex)
            {
                LogUtil.LogMessage(ex.Message);
                return false;            	
            }


        }
        
        public static bool DisConnection()
        {
            try
            {
                if (DBType == "SQL")
                {
                    SQLDisConnection();
                }
                else if (DBType == "ORACLE")
                {
                    ORADisConnection();
                }

                return true;
            }
            catch (System.Exception ex)
            {
                LogUtil.LogMessage(ex.Message);
                return false;
            }


        }

        /// <summary>
        /// 关系库是否连接正常
        /// </summary>
        /// <returns></returns>
        public static bool SQLConnection()
        {
            if (sqlConn != null && sqlConn.State != ConnectionState.Open)
            {
                sqlConn.Open();
                return true;
            }
            return true;
        }

        /// <summary>
        /// 关系库是否连接正常
        /// </summary>
        /// <returns></returns>
        public static bool SQLDisConnection()
        {
            if (sqlConn.State ==  ConnectionState.Open)
            {
                sqlConn.Close();
                return true;
            }
            return true;
        }

        /// <summary>
        /// 关系库是否连接正常
        /// </summary>
        /// <returns></returns>
        public static bool ORAConnection()
        {
            //if (oraConn != null && oraConn.State != ConnectionState.Open)
            //{
            //    oraConn.Open();
            //    return true;
            //}
            return true;
        }


        /// <summary>
        /// 关系库是否连接正常
        /// </summary>
        /// <returns></returns>
        public static bool ORADisConnection()
        {
            //if (oraConn.State == ConnectionState.Open)
            //{
            //    oraConn.Close();
            //    return true;
            //}
            return true;
        }

        #endregion

        public static void BulkToDB(DataTable dt)
        {
            //SqlClient中有SqlBulkCopy, System.Data.OracleClient中却没有。
            //使用Oracle DataAcess Provider For dotnet中自带的OracleBulkCopy组件

            //
            if (DBType == "SQL")
            {                
                SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConn);
                bulkCopy.DestinationTableName = BulkTable;
                bulkCopy.BatchSize = dt.Rows.Count;

                try
                {
                    //sqlConn.Open();
                    if (dt != null && dt.Rows.Count != 0)
                        bulkCopy.WriteToServer(dt);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    //sqlConn.Close();
                    if (bulkCopy != null)
                        bulkCopy.Close();
                }
            }
            else if (DBType == "ORACLE")
            {
                //OracleBulkCopy bulkCopy = new OracleBulkCopy(oraConn);
                //bulkCopy.DestinationTableName = BulkTable;
                //bulkCopy.BatchSize = dt.Rows.Count;

                try
                {
                    //sqlConn.Open();
                    //if (dt != null && dt.Rows.Count != 0)
                    //    bulkCopy.WriteToServer(dt);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    //sqlConn.Close();
                    //if (bulkCopy != null)
                    //    bulkCopy.Close();
                }
            }

        }

        public static DataTable GetTableSchema()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]{  
            new DataColumn("TagID",typeof(string)),  
            new DataColumn("UnitCode",typeof(string)),  
            new DataColumn("TagCode",typeof(string)),  
            new DataColumn("TagType",typeof(string)),  
            new DataColumn("TagEngunit",typeof(string)),  
            new DataColumn("TagTime",typeof(string)),  
            new DataColumn("TagShift",typeof(string)),  
            new DataColumn("TagPeriod",typeof(string)),  
            new DataColumn("TagMW",typeof(double)),  
            new DataColumn("TagSnap",typeof(double)),  
            new DataColumn("TagTarget",typeof(double)),  
            new DataColumn("TagScore",typeof(double)),  
            new DataColumn("TagLength",typeof(double)),  
            new DataColumn("TagRemoved",typeof(int))});

            return dt;
        }  

        /// <summary>
        /// 是否满足系统滤波计算条件
        /// </summary>
        /// <returns></returns>
        public static bool DeleteData(string unitcode, string tagcode, string tagtime)
        {
            string sql = "delete from " + BulkTable + " where TagTime='"+ tagtime +"' ";
            int nrows = 0;

            if (unitcode != "")
            {
                sql += " and lower(UnitCode) like '%" + unitcode + "%'";
            }

            if (tagcode != "")
            {
                sql += " and lower(TagCode) like '%" + tagcode + "%'";
            }

            try
            {
                if (DBType == "SQL")
                {
                    SqlCommand cmd = new SqlCommand(sql, sqlConn);
                    nrows= cmd.ExecuteNonQuery();
                }
                else if (DBType == "ORACLE")
                {
                    //OracleCommand cmd = new OracleCommand(sql, oraConn);
                    //nrows= cmd.ExecuteNonQuery();
                }

                return true;

            }
            catch (Exception ex)
            {
                throw ex;               
            }
        }

     
    }
}
