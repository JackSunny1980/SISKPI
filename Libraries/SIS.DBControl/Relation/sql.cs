using System;
using System.Collections.Generic;
using System.Text;
using SIS.DBControl;
using System.Data;
using System.Data.SqlClient;

namespace SIS.DBControl
{
    public class sql:RelaInterface
    {

        private static sql _mInstance = null;
        public static sql Instance()
        {
            if (_mInstance == null)
            {
                _mInstance = new sql();
            }
            return _mInstance;
        }

        #region RelaInterface 成员


        public int ExecuteNonQuery(string commandText)
        {
            //throw new Exception("The method or operation is not implemented.");
            return SqlHelper.ExecuteNonQuery(SqlHelper.MyConnectStr, CommandType.Text, commandText);
        }

        public System.Data.DataSet ExecuteDataset(string commandText)
        {
            //throw new Exception("The method or operation is not implemented.");
            return SqlHelper.ExecuteDataset(SqlHelper.MyConnectStr, CommandType.Text, commandText);
        }

        public object ExecuteScalar(string commandText)
        {
            //throw new Exception("The method or operation is not implemented.");
            return SqlHelper.ExecuteScalar(SqlHelper.MyConnectStr, CommandType.Text, commandText);
        }

        public int ExecuteNonQuery(object trans, string commandText)
        {
            //throw new Exception("The method or operation is not implemented.");
            return SqlHelper.ExecuteNonQuery((SqlTransaction)trans, CommandType.Text, commandText);
        }

        public DataSet ExecuteDataset(object trans, string commandText)
        {
            //throw new Exception("The method or operation is not implemented.");
            return SqlHelper.ExecuteDataset((SqlTransaction)trans, CommandType.Text, commandText);
        }

        public object ExecuteScalar(object trans, string commandText)
        {
            //throw new Exception("The method or operation is not implemented.");
            return SqlHelper.ExecuteScalar((SqlTransaction)trans, CommandType.Text, commandText);
        }

        #endregion

        #region RelaInterface 成员


        public bool Connection()
        {
            try
            {
                SqlConnection conn = new SqlConnection(SqlHelper.MyConnectStr);
                conn.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region RelaInterface 成员


        public IDbConnection GetConnection()
        {
            IDbConnection iDbConnection = new SqlConnection(SqlHelper.MyConnectStr);                
            return iDbConnection;
        }

        public IDbTransaction GetTransaction()
        {
            IDbConnection iDbConnection = GetConnection();
            IDbTransaction iDbTransaction = iDbConnection.BeginTransaction();
            return iDbTransaction;
        }

        #endregion

        #region RelaInterface 成员


        public int ExecuteNonQuery(string commandText, params IDbDataParameter[] commandParameters)
        {
            return SqlHelper.ExecuteNonQuery(SqlHelper.MyConnectStr, CommandType.Text, commandText, commandParameters);
        }

        public int ExecuteNonQuery(object trans, string commandText, params IDbDataParameter[] commandParameters)
        {
            return SqlHelper.ExecuteNonQuery((SqlTransaction)trans, CommandType.Text, commandText, commandParameters);
        }

        #endregion

		//Added by pyf 2013-08-06

		public object ExecuteScalar(string commandTex, params IDbDataParameter[] commandParameters) {
			return SqlHelper.ExecuteScalar(SqlHelper.MyConnectStr,CommandType.Text, commandTex, commandParameters);
		}
		public IDataReader ExecuteReader(string commandText) {
			return SqlHelper.ExecuteReader(SqlHelper.MyConnectStr, CommandType.Text, commandText);
		}

		public IDataReader ExecuteReader(CommandType commandType, string commandText) {
			return SqlHelper.ExecuteReader(SqlHelper.MyConnectStr, commandType, commandText);
		}
		public IDataReader ExecuteReader(CommandType commandType, string commandText, params IDbDataParameter[] commandParameters) {
			return SqlHelper.ExecuteReader(SqlHelper.MyConnectStr, commandType, commandText, commandParameters);
		}

		//End of Added.


		public int ExecuteNonQuery(string commandText, CommandType commandType, params IDbDataParameter[] commandParameters)
        {
            return SqlHelper.ExecuteNonQuery(SqlHelper.MyConnectStr, commandType, commandText, commandParameters);
        }
    }
}
