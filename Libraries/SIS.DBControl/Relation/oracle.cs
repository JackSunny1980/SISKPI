using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
//using SIS.DBControl;


namespace SIS.DBControl
{
    public class oracle:RelaInterface
    {

        private static oracle _mInstance = null;
        public static oracle Instance()
        {
            if (_mInstance == null)
            {
                _mInstance = new oracle();
            }
            return _mInstance;
        }

		//Added by pyf 2013-08-06
		public IDataReader ExecuteReader(string commandText) {
			return OracleHelper.ExecuteReader(OracleHelper.MyConnectStr, CommandType.Text, commandText);
		}

		public IDataReader ExecuteReader(CommandType commandType, string commandText) {
			return OracleHelper.ExecuteReader(OracleHelper.MyConnectStr, commandType, commandText);
		}
		public IDataReader ExecuteReader(CommandType commandType, string commandText, params IDbDataParameter[] commandParameters) {
			return OracleHelper.ExecuteReader(OracleHelper.MyConnectStr, commandType, commandText, (OracleParameter[])commandParameters);
		}
		public object ExecuteScalar(string commandTex, params IDbDataParameter[] commandParameters) {
			return OracleHelper.ExecuteScalar(OracleHelper.MyConnectStr, CommandType.Text, commandTex, commandParameters);
		}
		//End of Added.


        #region RelaInterface 成员

        public int ExecuteNonQuery(string commandText)
        {
            //throw new Exception("The method or operation is not implemented.");
            return OracleHelper.ExecuteNonQuery(OracleHelper.MyConnectStr, CommandType.Text, commandText);
        }

        public System.Data.DataSet ExecuteDataset(string commandText)
        {
            //throw new Exception("The method or operation is not implemented.");
            return OracleHelper.ExecuteDataset(OracleHelper.MyConnectStr, CommandType.Text, commandText);
        }

        public object ExecuteScalar(string commandText)
        {
            //throw new Exception("The method or operation is not implemented.");
            return OracleHelper.ExecuteScalar(OracleHelper.MyConnectStr, CommandType.Text, commandText);
        }

        public int ExecuteNonQuery(object trans, string commandText)
        {
            //throw new Exception("The method or operation is not implemented.");
            return OracleHelper.ExecuteNonQuery((OracleTransaction)trans, CommandType.Text, commandText);
        }

        public DataSet ExecuteDataset(object trans, string commandText)
        {
            //throw new Exception("The method or operation is not implemented.");
            return OracleHelper.ExecuteDataset((OracleTransaction)trans, CommandType.Text, commandText);
        }

        public object ExecuteScalar(object trans, string commandText)
        {
            //throw new Exception("The method or operation is not implemented.");
            return OracleHelper.ExecuteScalar((OracleTransaction)trans, CommandType.Text, commandText);
        }

        #endregion

        #region RelaInterface 成员


        public bool Connection()
        {
            try
            {
                OracleConnection conn = new OracleConnection(OracleHelper.MyConnectStr);
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
            IDbConnection iDbConnection = new OracleConnection(OracleHelper.MyConnectStr);
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
            return OracleHelper.ExecuteNonQuery(OracleHelper.MyConnectStr, CommandType.Text, commandText, (OracleParameter[])commandParameters);
        }

        public int ExecuteNonQuery(object trans, string commandText, params IDbDataParameter[] commandParameters)
        {
            return OracleHelper.ExecuteNonQuery((OracleTransaction)trans, CommandType.Text, commandText, (OracleParameter[])commandParameters);
        }

        #endregion


		public int ExecuteNonQuery(string commandText, CommandType commandType, params IDbDataParameter[] commandParameters)
        {
            throw new NotImplementedException();
        }
    }
}
