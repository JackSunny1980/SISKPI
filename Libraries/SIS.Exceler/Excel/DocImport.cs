using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.IO;
//using SIS.DBControl;
//using SISOPM.Excel;

namespace SIS.Exceler
{
    public class DocImport
    {
        public DataSet XlsImport(string newName,string table)
        {
            string connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + newName + ";Extended Properties=Excel 8.0;";
            OleDbConnection conn = new OleDbConnection(connStr);
            if (conn.State.ToString() == "Closed")
            {
                conn.Open();
            }
            OleDbDataAdapter oda = new OleDbDataAdapter("select * from [" + table + "$]", conn);
            DataSet ds = new DataSet();
            oda.Fill(ds);
            conn.Close();
            File.Delete(newName);

            //SqlServerHelper.TransferData(ds,table);

            return ds;
           
        }
    }
}
