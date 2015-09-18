using System;
using System.Data;
using System.Configuration;
using System.Web; 

namespace SIS.DBControl
{
    /// <summary>
    /// DBAccess 的摘要说明
    /// </summary>
    public class DBAccess
    {
        public static RelaInterface GetRelation()
        {
			string type = ConfigurationManager.AppSettings["Relation"];
            return RelaAccess.Create(type);
        }

        public static RTInterface GetRealTime()
        {
			string type = ConfigurationManager.AppSettings["RealTime"];
            return InterfaceImp.Create(type);
        }

        public static RTInterface GetRealTime(bool close)
        {
			string type = ConfigurationManager.AppSettings["RealTime"];
           
            return InterfaceImp.Create(type,close);
        }
    }
}
