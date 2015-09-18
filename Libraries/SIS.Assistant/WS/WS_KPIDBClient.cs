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
using System.Configuration;


namespace SIS.Assistant.WS {
	/// <summary>
	/// 方法
	/// </summary>
	public class WS_KPIDBClient : IDisposable {
		public string DBType;
		public SqlConnection sqlConn;
        //public OracleConnection oraConn;
		public string connectionString;


		public WS_KPIDBClient() {
			DBType = ConfigurationManager.AppSettings["Relation"].ToString().ToUpper();
			connectionString = ConfigurationManager.AppSettings["connectionString"].ToString();

			if (DBType == "SQL") {
				sqlConn = new SqlConnection(connectionString);
			}
			else if (DBType == "ORACLE") {
                //oraConn = new OracleConnection(connectionString);
			}

			//BulkTable = System.Configuration.ConfigurationManager.AppSettings["BulkTable"].ToString();
		}

		#region DataBase 连接与关闭

		public bool Connection() {
			try {
				if (DBType == "SQL") {
					SQLConnection();
				}
				else if (DBType == "ORACLE") {
					ORAConnection();
				}

				return true;
			}
			catch (System.Exception ex) {
				LogUtil.LogMessage(ex.Message);
				return false;
			}


		}

		public bool DisConnection() {
			try {
				if (DBType == "SQL") {
					SQLDisConnection();
				}
				else if (DBType == "ORACLE") {
					ORADisConnection();
				}

				return true;
			}
			catch (System.Exception ex) {
				LogUtil.LogMessage(ex.Message);
				return false;
			}


		}

		/// <summary>
		/// 关系库是否连接正常
		/// </summary>
		/// <returns></returns>
		public bool SQLConnection() {
			if (sqlConn != null && sqlConn.State != ConnectionState.Open) {
				sqlConn.Open();
				return true;
			}
			return true;
		}

		/// <summary>
		/// 关系库是否连接正常
		/// </summary>
		/// <returns></returns>
		public bool SQLDisConnection() {
			if (sqlConn.State == ConnectionState.Open) {
				sqlConn.Close();
				return true;
			}
			return true;
		}

		/// <summary>
		/// 关系库是否连接正常
		/// </summary>
		/// <returns></returns>
		public bool ORAConnection() {
            //if (oraConn != null && oraConn.State != ConnectionState.Open) {
            //    oraConn.Open();
            //    return true;
            //}
			return true;
		}


		/// <summary>
		/// 关系库是否连接正常
		/// </summary>
		/// <returns></returns>
		public bool ORADisConnection() {
            //if (oraConn.State == ConnectionState.Open) {
            //    oraConn.Close();
            //    return true;
            //}
			return true;
		}

		#endregion

		public void BulkToDB(DataTable dt, string BulkTable) {
			//SqlClient中有SqlBulkCopy, System.Data.OracleClient中却没有。
			//使用Oracle DataAcess Provider For dotnet中自带的OracleBulkCopy组件

			//
			if (DBType == "SQL") {
				//SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConn);
				SqlBulkCopy bulkCopy = new SqlBulkCopy(connectionString, SqlBulkCopyOptions.FireTriggers);
				bulkCopy.DestinationTableName = BulkTable;
				bulkCopy.BatchSize = dt.Rows.Count;
                bulkCopy.BulkCopyTimeout = 300;
				try {
					//sqlConn.Open();
					if (dt != null && dt.Rows.Count != 0)
						bulkCopy.WriteToServer(dt);
				}
				catch (Exception ex) {
					throw ex;
				}
				finally {
					//sqlConn.Close();
					if (bulkCopy != null)
						bulkCopy.Close();
				}
			}
			else if (DBType == "ORACLE") {
                //OracleBulkCopy bulkCopy = new OracleBulkCopy(oraConn);
                //bulkCopy.DestinationTableName = BulkTable;
                //bulkCopy.BatchSize = dt.Rows.Count;

                try {
                //    //sqlConn.Open();
                //    if (dt != null && dt.Rows.Count != 0)
                //        bulkCopy.WriteToServer(dt);
				}
				catch (Exception ex) {
					throw ex;
				}
				finally {
					//sqlConn.Close();
                    //if (bulkCopy != null)
                    //    bulkCopy.Close();
				}
			}

		}

		public DataTable GetTableSchema(string datatype) {
			DataTable dt = new DataTable();

			switch (datatype.ToLower()) {
				//KPI_RealValue
				case "tagvalue":

					dt.Columns.AddRange(new DataColumn[]{  
                    new DataColumn("RVID",typeof(string)),  
                    new DataColumn("UnitID",typeof(string)),  
                    new DataColumn("RealID",typeof(string)),  
                    new DataColumn("RealCode",typeof(string)),  
                    new DataColumn("RealDesc",typeof(string)),  
                    new DataColumn("RealEngunit",typeof(string)),  
                    new DataColumn("RealTime",typeof(string)),  
                    new DataColumn("RealValue",typeof(float)),  
                    new DataColumn("RealQulity",typeof(int)),});

					break;


				//KPI_ECSSArchive
				//KPI_ECSSSnapshot
				case "kpivalue":

					dt.Columns.AddRange(new DataColumn[]{  
                    new DataColumn("SSID",typeof(string)),  
                    new DataColumn("UnitID",typeof(string)),  
                    new DataColumn("SeqID",typeof(string)),  
                    new DataColumn("KpiID",typeof(string)),  
                    new DataColumn("ECID",typeof(string)),  
                    new DataColumn("ECName",typeof(string)),  
					new DataColumn("ECTX",typeof(String)),//added by pyf 2013-09-11
                    new DataColumn("ECTime",typeof(string)),  
                    new DataColumn("ECValue",typeof(Decimal)),  
                    new DataColumn("ECOpt",typeof(Decimal)),  
                    new DataColumn("ECOptExp",typeof(string)),  
                    new DataColumn("ECExpression",typeof(string)),
                    new DataColumn("ECScore",typeof(Decimal)),  
                    new DataColumn("ECQulity",typeof(int)), 
                    new DataColumn("ECPeriod",typeof(string)),  
                    new DataColumn("ECShift",typeof(string)),  
                    new DataColumn("ECIsRemove",typeof(int))});

					break;

			}

			return dt;
		}

		/// <summary>
		/// 删除该机组的数据记录
		/// </summary>
		/// <returns></returns>
		public bool DeleteData(string unitid, string realtime, string ectx, string BulkTable) {
			//删除ECSnapshot时, RealTime=""
			//删除RealValue时,
			string sql = "DELETE FROM " + BulkTable + " where 1=1 {0 } {1} {2}";
			int nrows = 0;
			string condition1 = "";
			string conditonn2 = "";
			string conditonn3 = "";

			if (unitid != "") {
				condition1 = " AND UnitID='" + unitid + "'";
			}
			if ((!string.IsNullOrEmpty(realtime)) && (BulkTable == "KPI_ArchiveValue") || (!string.IsNullOrEmpty(realtime)) && (BulkTable == "KPI_RealValue")) {
				conditonn2 = " AND RealTime='" + realtime + "'";
			}
			if ((!string.IsNullOrEmpty(realtime)) && (BulkTable == "KPI_ECSSArchive") || (!string.IsNullOrEmpty(realtime)) && (BulkTable == "KPI_ECSSSnapshot")) {
				conditonn2 = " AND ECTime='" + realtime + "'";
			}		

			if (ectx != "") {
				conditonn3 = " AND ECTX IN (" + ectx + ")";
			}

			sql = string.Format(sql, condition1, conditonn2, conditonn3);
			try {
				Connection();
				if (DBType == "SQL") {
					SqlCommand cmd = new SqlCommand(sql, sqlConn);
                    cmd.CommandTimeout = 300;
					nrows = cmd.ExecuteNonQuery();
				}
				else if (DBType == "ORACLE") {
                    //OracleCommand cmd = new OracleCommand(sql, oraConn);
                    //cmd.CommandTimeout = 300;
                    //nrows = cmd.ExecuteNonQuery();
				}
				return true;
			}
			catch (Exception ex) {
				throw ex;
				//return false;
			}
		}


		public void Dispose() {
			DisConnection();
		}
	}
}
