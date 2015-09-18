using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace SIS.DBControl
{
    public interface RelaInterface
    {
        int ExecuteNonQuery( string commandText);
        DataSet ExecuteDataset(string commandText);
        object ExecuteScalar(string commandText);
        int ExecuteNonQuery(string commandText, params IDbDataParameter[] commandParameters);
		//Added by pyf 2013-08-06
		object ExecuteScalar(string commandTex,params IDbDataParameter[] commandParameters);
		IDataReader ExecuteReader(string commandText);
		IDataReader ExecuteReader(CommandType commandType, string commandText);
		IDataReader ExecuteReader(CommandType commandType, string commandText, params IDbDataParameter[] commandParameters);
		//End of Added.

        int ExecuteNonQuery(object trans, string commandText);
        DataSet ExecuteDataset(object trans, string commandText);
        object ExecuteScalar(object trans, string commandText);
        int ExecuteNonQuery(object trans, string commandText, params IDbDataParameter[] commandParameters);

        bool Connection();

        IDbConnection GetConnection();
        IDbTransaction GetTransaction();

        //Added by zjw 2013-11-05
		int ExecuteNonQuery(string commandText, CommandType commandType, params IDbDataParameter[] commandParameters);
        //End of Added.
    }
}
